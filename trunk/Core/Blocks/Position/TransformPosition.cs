using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using Core.Main;
using System.Diagnostics;

namespace Core.Blocks.Position
{
    [Block(Name = "TransformPosition", Path = "Position")]
    public class TransformPosition : BaseBlock
    {
        public TransformPosition(BlockManager owner)
            : base(owner, null)
        {
            AddInput(new BlockInput(this, "Input"));
            AddInput(new BlockInput(this, "Matrix"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            Expression mtx;
            if (Inputs[1].ConnectedTo == null)
                mtx = new ConstExpression(Matrix44f.MakeIdentity());
            else
                mtx = new VariableExpression(Inputs[1].ConnectedTo.Variable);

            Debug.Assert(mtx.OutputFormat == Format.FLOAT4X4);

            Expression vector = InstructionHelper.ConvertInputTo(Format.FLOAT4, Inputs[0]);

            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.PositionTransform, mtx, vector))));
        }
    }
}
