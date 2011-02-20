using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using System.Xml;
using System.Globalization;
using Core.Main;

namespace Core.Blocks.Math.Vector
{
    [Block(Name = "VectorMix", Path = "Math/Vector")]
    public class VectorMix : BaseBlock
    {
        public static readonly string BLOCK_XML_INPUT_MEMBER_INPUT_SELECTOR_NODE_NAME = "MemberInputSelector";
        public static readonly string BLOCK_XML_VECTOR_MEMBER_ID_ATTRIBUTE_NAME = "Id";
        public static readonly string BLOCK_XML_CONST_VALUE_ATTRIBUTE_NAME = "ConstValue";
        public static readonly string BLOCK_XML_VECTOR_MEMBER_ATTRIBUTE_NAME = "VectorMember";
        public static readonly string BLOCK_XML_INPUT_NAME_ATTRIBUTE_NAME = "InputName";

        public struct MemberInputSelector
        {
            public MemberInputSelector(BlockInput bi, VectorMembers m)
            {
                BlockInput = bi;
                Member = m;
                ConstValue = new Vector1f(0);
            }
            public MemberInputSelector(float c)
            {
                BlockInput = null;
                Member = VectorMembers.None;
                ConstValue = new Vector1f(c);
            }
            public MemberInputSelector(Vector1f c)
            {
                BlockInput = null;
                Member = VectorMembers.None;
                ConstValue = c;
            }

            public readonly BlockInput BlockInput;
            public readonly VectorMembers Member;
            public readonly Vector1f ConstValue;

            public bool IsConst
            {
                get { return BlockInput == null; }
            }
        }

        public VectorMix(BlockManager owner)
            : base(owner, new VectorMixOptionsWindow())
        {
            AddInput(new BlockInput(this, "A"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));

            //set data
            SetDataInput(VectorMembers.X, new MemberInputSelector(Inputs[0], VectorMembers.X));
            SetDataInput(VectorMembers.Y, new MemberInputSelector(Inputs[0], VectorMembers.Y));
            SetDataInput(VectorMembers.Z, new MemberInputSelector(Inputs[0], VectorMembers.Z));
            SetDataInput(VectorMembers.W, new MemberInputSelector(Inputs[0], VectorMembers.W));
        }

        public override void Load(System.Xml.XmlElement node)
        {
            base.Load(node);

            foreach (var n in node.ChildNodes)
            {
                XmlElement nElement = n as XmlElement;
                if (nElement == null)
                    continue;

                if (nElement.Name == BLOCK_XML_INPUT_MEMBER_INPUT_SELECTOR_NODE_NAME)
                {
                    int id = int.Parse(nElement.GetAttribute(BLOCK_XML_VECTOR_MEMBER_ID_ATTRIBUTE_NAME));
                    Vector1f constValue = Vector1f.Parse(nElement.GetAttribute(BLOCK_XML_CONST_VALUE_ATTRIBUTE_NAME));
                    VectorMembers vm = (VectorMembers)Enum.Parse(typeof(VectorMembers), nElement.GetAttribute(BLOCK_XML_VECTOR_MEMBER_ATTRIBUTE_NAME));
                    BlockInput bi = FindInput(nElement.GetAttribute(BLOCK_XML_INPUT_NAME_ATTRIBUTE_NAME));

                    m_memberSelector[id] = (bi != null) ? new MemberInputSelector(bi, vm) : new MemberInputSelector(constValue);
                }
            }

        }

        public override void Save(System.Xml.XmlElement node)
        {
            base.Save(node);

            for (int i = 0; i < m_memberSelector.Length; i++ )
            {
                XmlElement misElement = (XmlElement)node.AppendChild(node.OwnerDocument.CreateElement(BLOCK_XML_INPUT_MEMBER_INPUT_SELECTOR_NODE_NAME));
                misElement.SetAttribute(BLOCK_XML_VECTOR_MEMBER_ID_ATTRIBUTE_NAME, i.ToString());
                misElement.SetAttribute(BLOCK_XML_CONST_VALUE_ATTRIBUTE_NAME, m_memberSelector[i].ConstValue.ToString());
                misElement.SetAttribute(BLOCK_XML_VECTOR_MEMBER_ATTRIBUTE_NAME, m_memberSelector[i].Member.ToString());
                misElement.SetAttribute(BLOCK_XML_INPUT_NAME_ATTRIBUTE_NAME, m_memberSelector[i].BlockInput != null ? m_memberSelector[i].BlockInput.Name : "");
            }
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            VectorConstructorExpression vce;
            switch (Outputs[0].Format)
            {
                case Format.FLOAT: vce = new VectorConstructorExpression(Outputs[0].Format, CreateExpression(0)); break;
                case Format.FLOAT2: vce = new VectorConstructorExpression(Outputs[0].Format, CreateExpression(0), CreateExpression(1)); break;
                case Format.FLOAT3: vce = new VectorConstructorExpression(Outputs[0].Format, CreateExpression(0), CreateExpression(1), CreateExpression(2)); break;
                case Format.FLOAT4: vce = new VectorConstructorExpression(Outputs[0].Format, CreateExpression(0), CreateExpression(1), CreateExpression(2), CreateExpression(3)); break;
                default: throw new Exception("wrong format");
            }

            sc.AddInstruction(new CreateVariableInstruction(new BinaryExpression(BinaryExpression.Operators.Assign, new VariableExpression(Outputs[0].Variable), vce)));

        }

        public Expression CreateExpression(int memberId)
        {
            if (m_memberSelector[memberId].IsConst)
                return new ConstExpression(m_memberSelector[memberId].ConstValue);
            return new SwizzleExpression(InstructionHelper.ConvertInputTo(Format.FLOAT4, m_memberSelector[memberId].BlockInput), m_memberSelector[memberId].Member);
        }

        public int InputCount
        {
            get { return Inputs.Count; }
            set
            {
                //remove if more
                while (value < Inputs.Count)
                    RemoveInput(Inputs[Inputs.Count - 1]);

                //add new
                if (Inputs.Count == 1 && value >= 2)
                    AddInput(new BlockInput(this, "B"));

                if (Inputs.Count == 2 && value >= 3)
                    AddInput(new BlockInput(this, "C"));

                if (Inputs.Count == 3 && value >= 4)
                    AddInput(new BlockInput(this, "D"));
            }
        }

        public void SetOutputFormat(Format f)
        {
            Outputs[0].Format = f;
        }

        public void SetDataInput(VectorMembers vmo, MemberInputSelector mis)
        {
            switch (vmo)
            {
                case VectorMembers.X: m_memberSelector[0] = mis; break;
                case VectorMembers.Y: m_memberSelector[1] = mis; break;
                case VectorMembers.Z: m_memberSelector[2] = mis; break;
                case VectorMembers.W: m_memberSelector[3] = mis; break;
                default :
                    throw new ArgumentException("wrong vmo arg");
            }
        }

        public MemberInputSelector GetDataInput(VectorMembers vmo)
        {
            switch (vmo)
            {
                case VectorMembers.X: return m_memberSelector[0];
                case VectorMembers.Y: return m_memberSelector[1];
                case VectorMembers.Z: return m_memberSelector[2];
                case VectorMembers.W: return m_memberSelector[3];
                default:
                    throw new ArgumentException("wrong vmo arg");
            }
        }

        #region private

        MemberInputSelector[] m_memberSelector = new MemberInputSelector[4];

        #endregion
    }
}
