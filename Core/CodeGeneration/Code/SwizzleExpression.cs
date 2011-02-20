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
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public class SwizzleExpression : Expression
    {
        public SwizzleExpression(Expression e, params VectorMembers[] m)
        {
            //check parameters
            if (m.Length > 4)
                throw new ArgumentException("too many swizzle's");

            //swizzle dont work with float
            if (e.OutputFormat == Format.FLOAT)
                throw new ArgumentException("swizzle dont work with float");

            //no 'VectorMembers.None' inside 'm'
            for (int i = 0; i < m.Length; i++)
                if (m[i] == VectorMembers.None)
                    throw new ArgumentException("no 'VectorMembers.None' inside 'm'");


            //set data
            Expression = e;

            for (int i = 0; i < m.Length; i++)
                Swizzle[i] = m[i];
        }

        protected override Format CalculateOutputFormat()
        {
            if (Swizzle[3] != VectorMembers.None)
                return Format.FLOAT4;

            if (Swizzle[2] != VectorMembers.None)
                return Format.FLOAT3;

            if (Swizzle[1] != VectorMembers.None)
                return Format.FLOAT2;

            return Format.FLOAT;
        }

        public readonly Expression Expression;
        public readonly VectorMembers[] Swizzle = new VectorMembers[4]{VectorMembers.None,VectorMembers.None,VectorMembers.None,VectorMembers.None};

        public override IList<Variable> ReadVariables
        {
            get { return Expression.ReadVariables; }
        }
        public override bool Linear
        {
            get { return Expression.Linear; }
        }
        public override bool Const
        {
            get { return Expression.Const; }
        }
    }
}
