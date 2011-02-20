using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration.Code;
using Core.CodeGeneration;
using Core.Main;

namespace Core.Blocks.Math.Other
{
    [Block(Name = "1-X", Path = "Math/Other")]
    public class OneMinusX : BaseBlock
    {
        public OneMinusX(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "X"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
            DataChanged();
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            ConstExpression ce;

            //create matching const
            switch (Outputs[0].Format)
            {
                case Format.FLOAT: ce = new ConstExpression(new Vector1f(1)); break;
                case Format.FLOAT2: ce = new ConstExpression(new Vector2f(1, 1)); break;
                case Format.FLOAT3: ce = new ConstExpression(new Vector3f(1, 1, 1)); break;
                case Format.FLOAT4: ce = new ConstExpression(new Vector4f(1, 1, 1, 1)); break;

                default:
                    throw new NotImplementedException();
            }

            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new BinaryExpression(BinaryExpression.Operators.Sub, ce, InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[0])))));
        }

        protected override void DataChanged()
        {
            if (Inputs.Count > 0 && Outputs.Count > 0)
            {
                //match output to input
                Expression e = InstructionHelper.InputToExpression(Inputs[0], new Vector1f(0));
                if (Outputs[0].Format != e.OutputFormat)
                    Outputs[0].Format = e.OutputFormat;
            }

            base.DataChanged();
        }
    }
}
