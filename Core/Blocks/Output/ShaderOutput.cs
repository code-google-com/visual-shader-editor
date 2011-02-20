using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using Core.Main;
using Core.Var;

namespace Core.Blocks.Output
{
    [Block(Name = "ShaderOutput", Path = "Output")]
    public class ShaderOutput : BaseBlock
    {
        public ShaderOutput(BlockManager owner)
            : base(owner, new ShaderOutputOptionsWindow())
        {
            AddInput(new SemanticBlockInput(this, Format.FLOAT4, "oPosition", VerticesStreamSemantic.POSITION, 0));
            AddInput(new SemanticBlockInput(this, Format.FLOAT4, "oColor", VerticesStreamSemantic.COLOR, 0));
        }

        public SemanticBlockInput CreateAndAddInput(Format f, string name, VerticesStreamSemantic s, int index)
        {
            SemanticBlockInput i = (SemanticBlockInput)CreateInput();
            i.Name = name;
            i.Semantic = s;
            i.Index = index;

            AddInput(i);

            return i;
        }

        public override bool Singleton
        {
            get { return true; }
        }

        protected override BlockInput CreateInput()
        {
            return new SemanticBlockInput(this, Format.FLOAT4, "", VerticesStreamSemantic.COLOR, 0);
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                SemanticBlockInput sobi = (SemanticBlockInput)Inputs[i];

                if (sobi.Semantic == VerticesStreamSemantic.POSITION)
                    sc.ShaderOutputPosition = sobi.Variable;
                else
                    sc.AddShaderOutput(sobi.Variable);

                sc.AddInstruction(new CreateVariableInstruction(
                    new BinaryExpression(BinaryExpression.Operators.Assign,
                        new VariableExpression(sobi.Variable), InstructionHelper.ConvertInputTo(sobi.Format, Inputs[i])), true));
            }
        }
    }
}
