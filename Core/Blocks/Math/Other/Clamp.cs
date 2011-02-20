using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Main;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using Core.Helper;

namespace Core.Blocks.Math.Other
{
    [Block(Name = "Clamp", Path = "Math/Other")]
    public class Clamp : BaseBlock
    {
        static readonly string BLOCK_XML_MIN_CONST_VALUE_ATTRIBUTE_NAME = "MinConstValue";
        static readonly string BLOCK_XML_MAX_CONST_VALUE_ATTRIBUTE_NAME = "MaxConstValue";

        public Clamp(BlockManager owner)
            : base(owner, new ClampOptionsWindow())
        {
            AddInput(new BlockInput(this, "Value"));
            AddInput(new BlockInput(this, "Min"));
            AddInput(new BlockInput(this, "Max"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
            RaiseDataChanged();
        }
        protected override void DataChanged()
        {
            if (Inputs.Count == 3 && Outputs.Count == 1)
            {
                //ignore min, max, only input value is important
                Outputs[0].Format = InstructionHelper.FindBigestInput(Inputs[0]);

                //disconnect disabled inputs
                if(m_min != null)
                    this.Disconnect(Inputs[1]);
                if(m_max != null)
                    this.Disconnect(Inputs[2]);
            }
            base.DataChanged();
        }

        public object Min
        {
            get { return m_min != null ? m_min.Value : null; }
            set
            {
                if (value != null)
                {
                    Inputs[1].Name = "Disabled";
                    m_min = ConstExpression.FromObject(value);
                }
                else
                {
                    Inputs[1].Name = "Min";
                    m_min = null;
                }
            }
        }
        public object Max
        {
            get { return m_max != null ? m_max.Value : null; }
            set
            {
                if (value != null)
                {
                    Inputs[2].Name = "Disabled";
                    m_max = ConstExpression.FromObject(value);
                }
                else
                {
                    Inputs[2].Name = "Max";
                    m_max = null;
                }
            }
        }
        public Format MinConstFormat
        {
            get { return m_min != null ? m_min.Format : Format.NONE; }
        }
        public Format MaxConstFormat
        {
            get { return m_max != null ? m_max.Format : Format.NONE; }
        }

        public override void Load(System.Xml.XmlElement node)
        {
            base.Load(node);

            if (node.GetAttributeNode(BLOCK_XML_MIN_CONST_VALUE_ATTRIBUTE_NAME) != null)
                Min = VectorHelper.FromString(node.GetAttribute(BLOCK_XML_MIN_CONST_VALUE_ATTRIBUTE_NAME));
            else
                Min = null;


            if (node.GetAttributeNode(BLOCK_XML_MAX_CONST_VALUE_ATTRIBUTE_NAME) != null)
                Max = VectorHelper.FromString(node.GetAttribute(BLOCK_XML_MAX_CONST_VALUE_ATTRIBUTE_NAME));
            else
                Max = null;
        }

        public override void Save(System.Xml.XmlElement node)
        {
            base.Save(node);

            object min = Min;
            if (min != null)
                node.SetAttribute(BLOCK_XML_MIN_CONST_VALUE_ATTRIBUTE_NAME, min.ToString());

            object max = Max;
            if (max != null)
                node.SetAttribute(BLOCK_XML_MAX_CONST_VALUE_ATTRIBUTE_NAME, max.ToString());
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            Expression min;
            Expression max;

            if (m_min != null)
                min = InstructionHelper.ConvertExpressionTo(Outputs[0].Format, m_min);
            else
                min = InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[1]);

            if (m_max != null)
                max = InstructionHelper.ConvertExpressionTo(Outputs[0].Format, m_max);
            else
                max = InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[2]);

            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.Clamp,
                        InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[0]), min, max))));
        }

        #region private

        ConstExpression m_min;
        ConstExpression m_max;

        #endregion
    }
}
