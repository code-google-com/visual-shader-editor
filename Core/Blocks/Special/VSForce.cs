using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Main;
using Core.CodeGeneration.Code;
using Core.CodeGeneration;

namespace Core.Blocks.Special
{
    [Block(Name = "VSForce", Path = "Special")]
    public class VSForce : BaseBlock
    {
        public VSForce(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "VS"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "PS"));
            DataChanged();
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            sc.AddInstruction(InstructionHelper.CopyInputToOutput(Outputs[0].Variable, Outputs[0].Format, Inputs[0], false));
            sc.ForceVSVariable(Outputs[0].Variable);
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
