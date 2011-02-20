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
