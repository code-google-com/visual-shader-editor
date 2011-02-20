using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Main;
using Core.Var;
using System.Diagnostics;

namespace Core.Blocks.Input
{
    [Block(Name = "UserParameter", Path = "Input")]
    public class UserParameter : BaseBlock
    {
        public UserParameter(BlockManager owner)
            : base(owner, new UserParameterOptionsWindow())
        {
            AddOutput(new ValueBlockOutput(this, Format.FLOAT, "A", new Vector1f(1)));
        }

        public override void SetShaderParameters(Core.Environment.ICompiledShader s)
        {
            base.SetShaderParameters(s);

            Debug.Assert(Outputs.Count <= 1);

            switch (Outputs[0].Format)
            {
                case Format.FLOAT: s.SetParameter(Outputs[0].Name, (Vector1f)((ValueBlockOutput)Outputs[0]).Value); break;
                case Format.FLOAT2: s.SetParameter(Outputs[0].Name, (Vector2f)((ValueBlockOutput)Outputs[0]).Value); break;
                case Format.FLOAT3: s.SetParameter(Outputs[0].Name, (Vector3f)((ValueBlockOutput)Outputs[0]).Value); break;
                case Format.FLOAT4: s.SetParameter(Outputs[0].Name, (Vector4f)((ValueBlockOutput)Outputs[0]).Value); break;

                default:
                    throw new NotImplementedException();
            }
        }

        protected override BlockOutput CreateOutput()
        {
            return new ValueBlockOutput(this, Format.FLOAT, "A", new Vector1f(1));
        }

        protected internal override void GenerateCode(Core.CodeGeneration.ShaderCodeGenerator sc)
        {
            //TODO: add default value, Remove SetShaderParameters()
            sc.AddParameter(Outputs[0].Variable);
        }
    }
}
