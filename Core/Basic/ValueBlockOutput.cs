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

namespace Core.Basic
{
    public class ValueBlockOutput : BlockOutput
    {
        static readonly string BLOCK_XML_IO_VALUE_ATTRIBUTE_NAME = "Value";

        public ValueBlockOutput(BaseBlock owner, Format t, string name, object value)
            : base(owner, t, name)
        {
            m_value = value;
        }
        public object Value
        {
            get { return m_value; }
        }

        public void FromString(string value, Format f)
        {
            switch (f)
            {
                case Format.FLOAT: m_value = Vector1f.Parse(value); break;
                case Format.FLOAT2: m_value = Vector2f.Parse(value); break;
                case Format.FLOAT3: m_value = Vector3f.Parse(value); break;
                case Format.FLOAT4: m_value = Vector4f.Parse(value); break;
                default: throw new Exception("wrong format");
            }

            Format = f;
        }

        public override void Save(System.Xml.XmlElement node)
        {
            base.Save(node);
            node.SetAttribute(BLOCK_XML_IO_VALUE_ATTRIBUTE_NAME, m_value.ToString());
        }
        public override void Load(System.Xml.XmlElement node)
        {
            base.Load(node);
            FromString(node.GetAttribute(BLOCK_XML_IO_VALUE_ATTRIBUTE_NAME), Format);
        }

        #region private

        object m_value;

        #endregion
    }
}
