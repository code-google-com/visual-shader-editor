using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration;
using Core.Main;
using Core.Var;

namespace Core.Blocks.Input
{
    [Block(Name = "VerticesStream", Path="Input")]
    public class VerticesStream : BaseBlock
    {
        public VerticesStream(BlockManager owner)
            : base(owner, new VerticesStreamOptionsWindow())
        {
            AddOutput(new SemanticBlockOutput(this, Format.FLOAT4, "iPosition", VerticesStreamSemantic.POSITION, 0));
        }

        protected override BlockOutput CreateOutput()
        {
            return new SemanticBlockOutput(this, Format.FLOAT4, "", VerticesStreamSemantic.POSITION, 0);
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            foreach (var o in Outputs)
            {
                SemanticBlockOutput vso = (SemanticBlockOutput)o;

                sc.AddShaderInput(vso.Variable);
            }
        }

        public SemanticBlockOutput CreateAndAddOutput(Format f, string name, VerticesStreamSemantic s, int index)
        {
            SemanticBlockOutput o = (SemanticBlockOutput)CreateOutput();
            o.Name = name;
            o.Semantic = s;
            o.Index = index;

            AddOutput(o);

            return o;
        }

        public override bool Singleton
        {
            get { return true; }
        }
    }
}
