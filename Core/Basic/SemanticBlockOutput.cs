using System;
using System.Collections.Generic;
using System.Text;
using Core.Var;

namespace Core.Basic
{
    public class SemanticBlockOutput : BlockOutput
    {
        public static readonly string BLOCK_XML_IO_SEMANTIC_ATTRIBUTE_NAME = "Semantic";
        public static readonly string BLOCK_XML_IO_INDEX_ATTRIBUTE_NAME = "Index";

        public SemanticBlockOutput(BaseBlock owner, Format f, string name, VerticesStreamSemantic s, int index)
            : base(owner, f, name)
        {
            m_semantic = s;
            m_index = index;

            UpdateVariable();
        }

        public override void Load(System.Xml.XmlElement node)
        {
            BlockRaiseDataChange();

            base.Load(node);
            Semantic = (VerticesStreamSemantic)Enum.Parse(typeof(VerticesStreamSemantic), node.GetAttribute(BLOCK_XML_IO_SEMANTIC_ATTRIBUTE_NAME));
            Index = int.Parse(node.GetAttribute(BLOCK_XML_IO_INDEX_ATTRIBUTE_NAME));

            UnBlockRaiseDataChanged();
        }
        public override void Save(System.Xml.XmlElement node)
        {
            base.Save(node);
            node.SetAttribute(BLOCK_XML_IO_SEMANTIC_ATTRIBUTE_NAME, m_semantic.ToString());
            node.SetAttribute(BLOCK_XML_IO_INDEX_ATTRIBUTE_NAME, m_index.ToString());
        }

        public VerticesStreamSemantic Semantic
        {
            get { return m_semantic; }
            set
            {
                if (m_semantic != value)
                {
                    m_semantic = value;
                    RaiseDataChanged();
                }
            }
        }
        public int Index
        {
            get { return m_index; }
            set
            {
                if (m_index != value)
                {
                    m_index = value;
                    RaiseDataChanged();
                }
            }
        }

        protected override Variable CreateVariable()
        {
            return Owner.BlockManager.VariableManager.CreateVariable(Name, Format, m_semantic, m_index, Owner);
        }

        #region private

        VerticesStreamSemantic m_semantic;
        int m_index;

        #endregion
    }
}
