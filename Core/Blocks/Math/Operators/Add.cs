using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using System.Collections.ObjectModel;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using Core.Main;

namespace Core.Blocks.Math.Operators
{
    [Block(Name = "Add", Path="Math/Operators")]
    public class Add : BaseBlock
    {
        public Add(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "Input0"));
            AddInput(new BlockInput(this, "Input1"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
            RaiseDataChanged();
        }
        protected override void DataChanged()
        {
            if (Inputs.Count == 2 && Outputs.Count == 1)
            {
                Expression e0, e1;
                Outputs[0].Format = InstructionHelper.BinaryOperatorVectorSecondFloatExpressions(Inputs[0], Inputs[1], out e0, out e1);
            }
            base.DataChanged();
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            sc.AddInstruction(InstructionHelper.BinaryOperatorVectorSecondFloat(BinaryExpression.Operators.Add, Outputs[0].Variable, Inputs[0], Inputs[1]));
        }
    }
}
