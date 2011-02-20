using System;
using System.Collections.Generic;
using System.Text;
using Core.Main;
using Core.Basic;
using Core.CodeGeneration.Code;
using Core.CodeGeneration;

namespace Core.Blocks.Math.Vector
{
    [Block(Name = "Length", Path = "Math/Vector")]
    public class Length : BaseBlock
    {
        public Length(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "Input"));
            AddOutput(new BlockOutput(this, Format.FLOAT, "Output"));
        }
        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.Length,
                        InstructionHelper.InputToExpression(Inputs[0], new Vector1f(0))))));
        }
    }
}
