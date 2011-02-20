using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Core.Basic
{
    public abstract class BlockIOBase
    {
        static readonly string BLOCK_XML_IO_NAME_ATTRIBUTE_NAME = "Name";
        static readonly string BLOCK_XML_IO_ID_ATTRIBUTE_NAME = "Id";

        public event Action<BlockIOBase> OnDataChanged;

        public BlockIOBase(BaseBlock owner, string name)
        {
            Owner = owner;
            m_name = name;
        }

        public readonly BaseBlock Owner;
        public string Name
        {
            get { return m_name; }
            set
            {
                if (m_name != value)
                {
                    m_name = value;
                    RaiseDataChanged();
                }
            }
        }
        public Guid Guid
        {
            get { return m_guid; }
        }

        public virtual void Save(XmlElement node)
        {
            node.SetAttribute(BLOCK_XML_IO_ID_ATTRIBUTE_NAME, m_guid.ToString());
            node.SetAttribute(BLOCK_XML_IO_NAME_ATTRIBUTE_NAME, Name);            
        }
        public virtual void Load(XmlElement node)
        {
            m_guid = new Guid(node.GetAttribute(BLOCK_XML_IO_ID_ATTRIBUTE_NAME));
            Name = node.GetAttribute(BLOCK_XML_IO_NAME_ATTRIBUTE_NAME);
        }

        protected void RaiseDataChanged()
        {
            if (m_raiseDataChangeBlockCounter > 0)
                return;

            DataChanged();
            if (OnDataChanged != null)
                OnDataChanged(this);
        }
        protected virtual void DataChanged()
        {
        }

        internal virtual void Destroy()
        {
        }

        protected void BlockRaiseDataChange()
        {
            m_raiseDataChangeBlockCounter++;
        }
        protected void UnBlockRaiseDataChanged()
        {
            m_raiseDataChangeBlockCounter--;

            Debug.Assert(m_raiseDataChangeBlockCounter >= 0);

            if (m_raiseDataChangeBlockCounter == 0)
                RaiseDataChanged();
        }

        #region private

        string m_name;
        Guid m_guid = Guid.NewGuid();
        int m_raiseDataChangeBlockCounter = 0;

        #endregion
    }
}
