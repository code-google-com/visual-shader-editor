using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using Core.Main;

namespace Core.Blocks.Math.Operators
{
    [Block(Name = "Negative", Path = "Math/Operators")]
    public class Negative : BaseBlock
    {
        public Negative(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "Input"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
            DataChanged();
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            sc.AddInstruction(new CreateVariableInstruction(new BinaryExpression(BinaryExpression.Operators.Assign,
                new VariableExpression(Outputs[0].Variable), new UnaryExpression(UnaryExpression.Operators.Minus, InstructionHelper.InputToExpression(Inputs[0], new Vector1f(0))))));
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
