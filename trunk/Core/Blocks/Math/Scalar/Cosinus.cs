using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration.Code;
using Core.CodeGeneration;
using Core.Main;

namespace Core.Blocks.Math.Scalar
{
    [Block(Name = "Cosinus", Path = "Math/Scalar")]
    public class Cosinus : BaseBlock
    {
        public Cosinus(BlockManager owner)
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
                    new CallExpression(CallExpression.Function.Cos, InstructionHelper.ConvertInputTo(Format.FLOAT, Inputs[0])))));
        }
    }
}
