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
