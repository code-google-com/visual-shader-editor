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
    public class CallExpression : Expression
    {
        public enum Function
        {
            SampleTexture2D,
            Sin,
            Cos,
            Dot,
            PositionTransform,
            Cross,
            Normalize,
            Abs,
            Clamp,
            Lerp,
            Length,
            Pow,
        }

        public CallExpression(Function f, params Expression[] parameters)
        {
            FunctionType = f;
            Parameters = parameters;
        }

        protected override Core.Basic.Format CalculateOutputFormat()
        {
            switch (FunctionType)
            {
                case Function.SampleTexture2D: return Format.FLOAT4;
                case Function.Sin: return Format.FLOAT;
                case Function.Cos: return Format.FLOAT;
                case Function.Dot: return Format.FLOAT;
                case Function.PositionTransform: return Format.FLOAT4;
                case Function.Cross: return Parameters[0].OutputFormat;
                case Function.Normalize: return Parameters[0].OutputFormat;
                case Function.Abs: return Parameters[0].OutputFormat;
                case Function.Clamp: return Parameters[0].OutputFormat;
                case Function.Lerp: return Parameters[0].OutputFormat;
                case Function.Length: return Format.FLOAT;
                case Function.Pow: return Parameters[0].OutputFormat;
            }

            throw new NotImplementedException();
        }

        public readonly Function FunctionType;
        public readonly Expression[] Parameters;

        public override IList<Variable> ReadVariables
        {
            get
            {
                List<Variable> l = new List<Variable>();
                foreach (var e in Parameters)
                    l.AddRange(e.ReadVariables);
                return l;
            }
        }
        public override bool Linear
        {
            get
            {
                foreach (var e in Parameters)
                    if (!e.Linear)
                        return false;

                switch (FunctionType)
                {
                    case Function.SampleTexture2D: return false;
                    case Function.Sin: return false;
                    case Function.Cos: return false;
                    case Function.Dot: return false;
                    case Function.PositionTransform: return true;
                    case Function.Cross: return false;
                    case Function.Normalize: return false;
                    case Function.Abs: return true;
                    case Function.Clamp: return false;
                    case Function.Lerp: return true;
                    case Function.Length: return false;
                    case Function.Pow: return false;
                }

                throw new NotImplementedException();
            }
        }
        public override bool Const
        {
            get
            {
                foreach (var e in Parameters)
                    if (!e.Const)
                        return false;

                return true;
            }
        }
    }
}
