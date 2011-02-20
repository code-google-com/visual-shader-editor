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
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Core.CodeGeneration;
using System.Diagnostics;
using System.Globalization;
using Core.CodeGeneration.Code;
using Core.Main;

namespace Core.Blocks.Input
{
    [Block(Name = "Constant", Path = "Input")]
    public class Constant : BaseBlock
    {
        public Constant(BlockManager owner)
            : base(owner, new ConstantOptionsWindow())
        {
        }

        protected override BlockOutput CreateOutput()
        {
            return new ValueBlockOutput(this, Format.FLOAT, "", new Vector1f(0));
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            foreach (var o in Outputs)
            {
                ValueBlockOutput cbo = (ValueBlockOutput)o;
                ConstExpression ce;
                switch (o.Format)
                {
                    case Format.FLOAT: ce = new ConstExpression((Vector1f)cbo.Value); break;
                    case Format.FLOAT2: ce = new ConstExpression((Vector2f)cbo.Value); break;
                    case Format.FLOAT3: ce = new ConstExpression((Vector3f)cbo.Value); break;
                    case Format.FLOAT4: ce = new ConstExpression((Vector4f)cbo.Value); break;
                    default: throw new Exception("wrong format");
                }

                sc.AddInstruction(new CreateVariableInstruction(
                    new BinaryExpression(BinaryExpression.Operators.Assign,
                        new VariableExpression(o.Variable), ce)));
            }
        }

        public ValueBlockOutput CreateAndAddOutput(Format f, string name, string v)
        {
            ValueBlockOutput o = (ValueBlockOutput)CreateOutput();
            o.Name = name;
            o.FromString(v, f);

            AddOutput(o);

            return o;
        }
    }
}
