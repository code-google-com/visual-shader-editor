using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Main;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;

namespace Core.Blocks.Math.Operators
{
    [Block(Name = "Pow", Path = "Math/Operators")]
    public class Pow : BaseBlock
    {
        public Pow(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "Input"));
            AddInput(new BlockInput(this, "Power"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
            RaiseDataChanged();
        }
        protected override void DataChanged()
        {
            if (Inputs.Count == 2 && Outputs.Count == 1 && Inputs[0].ConnectedTo != null)
            {
                Outputs[0].Format = Inputs[0].ConnectedTo.Format;
            }
            base.DataChanged();
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.Pow,
                        InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[0]), InstructionHelper.ConvertInputTo(Format.FLOAT, Inputs[1])))));
        }
    }
}
