using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Core.CodeGeneration;
using System.Xml;
using Core.Var;

namespace Core.Basic
{
    public class BlockOutput : BlockIOBase
    {
        static readonly string BLOCK_XML_IO_FORMAT_ATTRIBUTE_NAME = "Format";

        public BlockOutput(BaseBlock owner, Format f, string name)
            : base(owner, name)
        {
            m_format = f;
            UpdateVariable();
        }

        public Format Format
        {
            get { return m_format; }
            set
            {
                if (m_format != value)
                {
                    m_format = value;
                    RaiseDataChanged();
                }
            }
        }

        public ReadOnlyCollection<BlockInput> ConnectedTo
        {
            get { return m_connectedTo.AsReadOnly(); }
        }
        public bool IsConnectedTo(BlockInput bi)
        {
            return m_connectedTo.Contains(bi);
        }
        internal void AddConnection(BlockInput bi)
        {
            Debug.Assert(!m_connectedTo.Contains(bi));
            m_connectedTo.Add(bi);
        }
        internal void RemoveConnection(BlockInput bi)
        {
            Debug.Assert(m_connectedTo.Contains(bi));
            m_connectedTo.Remove(bi);
        }

        public override void Save(XmlElement node)
        {
            base.Save(node);
            node.SetAttribute(BLOCK_XML_IO_FORMAT_ATTRIBUTE_NAME, Format.ToString());
        }
        public override void Load(XmlElement node)
        {
            base.Load(node);
            Format = (Core.Basic.Format)Enum.Parse(typeof(Core.Basic.Format), node.GetAttribute(BLOCK_XML_IO_FORMAT_ATTRIBUTE_NAME));
        }

        public Variable Variable
        {
            get { return m_variable; }
        }
        protected virtual Variable CreateVariable()
        {
            return Owner.BlockManager.VariableManager.CreateVariable(Format, Owner);
        }

        protected override void DataChanged()
        {
            UpdateVariable();
            base.DataChanged();
        }
        internal override void Destroy()
        {
            if (m_variable != null)
            {
                Owner.BlockManager.VariableManager.DestroyVariable(m_variable, Owner);
                m_variable = null;
            }

            base.Destroy();
        }

        protected void UpdateVariable()
        {
            if (m_variable != null)
            {
                Owner.BlockManager.VariableManager.DestroyVariable(m_variable, Owner);
                m_variable = null;
            }

            m_variable = CreateVariable();
        }

        #region private

        readonly List<BlockInput> m_connectedTo = new List<BlockInput>();
        Format m_format;
        Variable m_variable;

        #endregion
    }
}
