/*
Copyright (c) 2011, Pawel Szczurek
All rights reserved.


Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:


Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

Neither the name of the <ORGANIZATION> nor the names of its contributors may be used to endorse or promote products derived from this software without
specific prior written permission.


THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Text;
using Core.Var;

namespace Core.Basic
{
    public class SemanticBlockInput : BlockInput
    {
        public static readonly string BLOCK_XML_IO_SEMANTIC_ATTRIBUTE_NAME = "Semantic";
        public static readonly string BLOCK_XML_IO_INDEX_ATTRIBUTE_NAME = "Index";
        public static readonly string BLOCK_XML_IO_FORMAT_ATTRIBUTE_NAME = "Format";

        public SemanticBlockInput(BaseBlock owner, Format f, string name, VerticesStreamSemantic s, int index)
            : base(owner, name)
        {
            m_semantic = s;
            m_index = index;
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

        public override void Load(System.Xml.XmlElement node)
        {
            BlockRaiseDataChange();

            base.Load(node);
            Semantic = (VerticesStreamSemantic)Enum.Parse(typeof(VerticesStreamSemantic), node.GetAttribute(BLOCK_XML_IO_SEMANTIC_ATTRIBUTE_NAME));
            Index = int.Parse(node.GetAttribute(BLOCK_XML_IO_INDEX_ATTRIBUTE_NAME));
            Format = (Core.Basic.Format)Enum.Parse(typeof(Core.Basic.Format), node.GetAttribute(BLOCK_XML_IO_FORMAT_ATTRIBUTE_NAME));

            UnBlockRaiseDataChanged();
        }
        public override void Save(System.Xml.XmlElement node)
        {
            base.Save(node);
            node.SetAttribute(BLOCK_XML_IO_SEMANTIC_ATTRIBUTE_NAME, m_semantic.ToString());
            node.SetAttribute(BLOCK_XML_IO_INDEX_ATTRIBUTE_NAME, m_index.ToString());
            node.SetAttribute(BLOCK_XML_IO_FORMAT_ATTRIBUTE_NAME, Format.ToString());
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

        public Variable Variable
        {
            get { return m_variable; }
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

        #region private

        void UpdateVariable()
        {
            if (m_variable != null)
            {
                Owner.BlockManager.VariableManager.DestroyVariable(m_variable, Owner);
                m_variable = null;
            }

            m_variable = Owner.BlockManager.VariableManager.CreateVariable(Name, Format, m_semantic, m_index, Owner);
        }

        VerticesStreamSemantic m_semantic;
        int m_index;
        Format m_format;
        Variable m_variable;

        #endregion
    }
}
