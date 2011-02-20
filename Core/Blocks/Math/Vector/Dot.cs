using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration.Code;
using Core.CodeGeneration;
using Core.Main;

namespace Core.Blocks.Math.Vector
{
    [Block(Name = "Dot", Path = "Math/Vector")]
    public class Dot : BaseBlock
    {
        public Dot(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "Input0"));
            AddInput(new BlockInput(this, "Input1"));
            AddOutput(new BlockOutput(this, Format.FLOAT, "Output"));
        }
        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            //find bigger
            Expression e0, e1;
            Format f = InstructionHelper.BinaryOperatorVectorSecondFloatExpressions(Inputs[0], Inputs[1], out e0, out e1);

            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.Dot,
                        InstructionHelper.ConvertInputTo(f, Inputs[0]), InstructionHelper.ConvertInputTo(f, Inputs[1])))));
        }
    }
}
