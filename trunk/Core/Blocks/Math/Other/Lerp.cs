using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Main;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;

namespace Core.Blocks.Math.Other
{
    [Block(Name = "Lerp", Path = "Math/Other")]
    public class Lerp : BaseBlock
    {
        public Lerp(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "A"));
            AddInput(new BlockInput(this, "B"));
            AddInput(new BlockInput(this, "Alpha"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
            RaiseDataChanged();
        }
        protected override void DataChanged()
        {
            if (Inputs.Count == 3 && Outputs.Count == 1)
            {
                Outputs[0].Format = InstructionHelper.FindBigestInput(Inputs[0], Inputs[1]);
            }
            base.DataChanged();
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.Lerp,
                        InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[0]),
                        InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[1]),
                        InstructionHelper.ConvertInputTo(Format.FLOAT, Inputs[2])))));
        }
    }
}
