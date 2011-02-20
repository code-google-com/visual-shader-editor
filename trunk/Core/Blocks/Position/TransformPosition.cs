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
