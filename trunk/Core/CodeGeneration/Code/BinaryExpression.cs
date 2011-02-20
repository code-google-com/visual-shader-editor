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
using System.Diagnostics;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public class BinaryExpression : Expression
    {
        public enum Operators
        {
            Add,
            Sub,
            Mul,
            Div,

            Equal,
            NotEqual,
            Less,
            LessOrEqual,
            //Greater,
            //GreaterOrEqual,

            Assign,
        }

        public BinaryExpression(Operators op, Expression l, Expression r)
        {
            //check parameters
            if (op == Operators.Add || op == Operators.Sub || op == Operators.Mul || op == Operators.Div)
            {
                if (l.OutputFormat != r.OutputFormat && r.OutputFormat != Format.FLOAT)
                    throw new ArgumentException("parameters format don't mach");
            }
            else if (op == Operators.Equal || op == Operators.NotEqual || op == Operators.Assign)
            {
                if (l.OutputFormat != r.OutputFormat)
                    throw new ArgumentException("parameters format don't mach");
            }
            else if (op == Operators.Less || op == Operators.LessOrEqual)
            {
                if (l.OutputFormat != Format.FLOAT || r.OutputFormat != Format.FLOAT)
                    throw new ArgumentException("parameters format don't mach");
            }
            else
            {
                Debug.Fail("parameters not checked");
            }

            if (op == Operators.Assign)
            {
                Debug.Assert(l is VariableExpression);
            }

            //set data
            Operator = op;
            LeftExpression = l;
            RightExpression = r;
        }

        protected override Format CalculateOutputFormat()
        {
            if (Operator == Operators.Equal || Operator == Operators.NotEqual)
                return Format.FLOAT;

            return LeftExpression.OutputFormat;
        }

        public readonly Operators Operator;
        public readonly Expression LeftExpression;
        public readonly Expression RightExpression;

        public override IList<Variable> ReadVariables
        {
            get
            {
                List<Variable> l = new List<Variable>();
                if(Operator != Operators.Assign)
                    l.AddRange(LeftExpression.ReadVariables);
                l.AddRange(RightExpression.ReadVariables);
                return l;
            }
        }
        public override bool Linear
        {
            get
            {
                if (!LeftExpression.Linear || !RightExpression.Linear)
                    return false;

                switch (Operator)
                {
                    case Operators.Add: return true;
                    case Operators.Sub: return true;
                    case Operators.Mul: return true;
                    case Operators.Div: return true;

                    case Operators.Equal: return false;
                    case Operators.NotEqual: return false;
                    case Operators.Less: return false;
                    case Operators.LessOrEqual: return false;

                    case Operators.Assign: return true;
                }

                throw new NotImplementedException();
            }
        }
        public override bool Const
        {
            get
            {
                if (Operator == Operators.Assign)
                    return RightExpression.Const;

                return LeftExpression.Const && RightExpression.Const;
            }
        }
    }
}
