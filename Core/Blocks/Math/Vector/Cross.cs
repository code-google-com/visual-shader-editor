using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration.Code;
using Core.CodeGeneration;
using Core.Main;

namespace Core.Blocks.Math.Vector
{
    [Block(Name = "Cross", Path = "Math/Vector")]
    public class Cross : BaseBlock
    {
        public Cross(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "Input0"));
            AddInput(new BlockInput(this, "Input1"));
            AddOutput(new BlockOutput(this, Format.FLOAT3, "Output"));
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.Cross,
                        InstructionHelper.ConvertInputTo(Format.FLOAT3, Inputs[0]), InstructionHelper.ConvertInputTo(Format.FLOAT3, Inputs[1])))));
        }
    }
}
