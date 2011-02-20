/*
Copyright (c) 2011, Pawel Szczurek
All rights reserved.


Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:


Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

Neither the name of the <ORGANIZATION> nor the names of its contributors may be used to endorse or promote products derived from this software without
specific prior written permission.


THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

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
