using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using Core.Main;

namespace Core.Blocks.ControlFlow
{
    [Block(Name = "If", Path = "ControlFlow")]
    public class If : BaseBlock
    {
        public If(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "A"));
            AddInput(new BlockInput(this, "B"));
            AddInput(new BlockInput(this, "A < B"));
            AddInput(new BlockInput(this, "A = B"));
            AddInput(new BlockInput(this, "A > B"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
        }

        protected override void DataChanged()
        {
            if (Inputs.Count == 5 && Outputs.Count == 1)
            {
                Outputs[0].Format = InstructionHelper.FindBigestInput(Inputs[2], Inputs[3], Inputs[4]);
            }
            base.DataChanged();
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            sc.AddInstruction(

                //if A < B
                new IfInstruction(new BinaryExpression(BinaryExpression.Operators.Less,
                    InstructionHelper.ConvertInputTo(Format.FLOAT, Inputs[0]), InstructionHelper.ConvertInputTo(Format.FLOAT, Inputs[1])),
                    InstructionHelper.CopyInputToOutput(Outputs[0].Variable, Outputs[0].Format, Inputs[2], true),

                    //else if A == B
                    new IfInstruction(new BinaryExpression(BinaryExpression.Operators.Equal,
                    InstructionHelper.ConvertInputTo(Format.FLOAT, Inputs[0]), InstructionHelper.ConvertInputTo(Format.FLOAT, Inputs[1])),
                    InstructionHelper.CopyInputToOutput(Outputs[0].Variable, Outputs[0].Format, Inputs[3], true),

                    //else
                    InstructionHelper.CopyInputToOutput(Outputs[0].Variable, Outputs[0].Format, Inputs[4], true)),

                    //out variable
                    Outputs[0].Variable)
            );
        }
    }
}
