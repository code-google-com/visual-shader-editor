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
