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
