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
using System.Diagnostics;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public class CreateVariableInstruction : Instruction
    {
        public CreateVariableInstruction(BinaryExpression assignExpression)
        {
            Debug.Assert(assignExpression.Operator == BinaryExpression.Operators.Assign);

            AssignExpression = assignExpression;
            AssignOnly = false;
        }

        public CreateVariableInstruction(BinaryExpression assignExpression, bool assignOnly)
        {
            Debug.Assert(assignExpression.Operator == BinaryExpression.Operators.Assign);
            Debug.Assert(assignExpression.LeftExpression is VariableExpression);

            AssignExpression = assignExpression;
            AssignOnly = assignOnly;
        }

        public readonly BinaryExpression AssignExpression;
        public readonly bool AssignOnly;

        public override IList<Variable> DefinedVariables
        {
            get
            {
                if (AssignOnly)
                    return new Variable[] { };
                else
                    return AssignExpression.LeftExpression.ReadVariables;
            }
        }
        public override IList<Variable> WritenVariables
        {
            get
            {
                return AssignExpression.LeftExpression.ReadVariables;
            }
        }
        public override IList<Variable> ReadVariables
        {
            get
            {
                return AssignExpression.RightExpression.ReadVariables;
            }
        }
        public override bool Linear
        {
            get { return AssignExpression.RightExpression.Linear; }
        }
        public override bool Const
        {
            get { return AssignExpression.RightExpression.Const; }
        }
    }
}
