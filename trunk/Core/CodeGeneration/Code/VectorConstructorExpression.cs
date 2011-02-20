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
    public class VectorConstructorExpression : Expression
    {
        public VectorConstructorExpression(Format f, params Expression[] exp)
        {
            //check input parameters
            int inputFormatSize = 0;
            for (int i = 0; i < exp.Length; i++)
                inputFormatSize += FormatHelper.Size(exp[i].OutputFormat);

            if (exp.Length > 4 || FormatHelper.Size(f) != inputFormatSize)
                throw new ArgumentException("expressions size do not match selected format");

            //set values
            Format = f;
            Values = exp;
        }

        protected override Format CalculateOutputFormat()
        {
            return Format;
        }

        public readonly Format Format;
        public readonly Expression[] Values;

        public override IList<Variable> ReadVariables
        {
            get
            {
                List<Variable> l = new List<Variable>();
                foreach (var e in Values)
                    l.AddRange(e.ReadVariables);
                return l;
            }
        }
        public override bool Linear
        {
            get
            {
                foreach (var e in Values)
                    if (!e.Linear)
                        return false;

                return true;
            }
        }
        public override bool Const
        {
            get
            {
                foreach (var e in Values)
                    if (!e.Const)
                        return false;

                return true;
            }
        }
    }
}
