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
    public class IfInstruction : Instruction
    {
        public IfInstruction(Expression condition, Instruction code) : this(condition, code, null, null) { }
        public IfInstruction(Expression condition, Instruction code, Variable defineOutVariable) : this(condition, code, null, defineOutVariable){}
        public IfInstruction(Expression condition, Instruction ifTrue, Instruction ifFalse) : this(condition, ifTrue, ifFalse, null) { }
        public IfInstruction(Expression condition, Instruction ifTrue, Instruction ifFalse, Variable defineOutVariable)
        {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
            DefinedOutputVariable = defineOutVariable;
        }

        public readonly Expression Condition;
        public readonly Instruction IfTrue;
        public readonly Instruction IfFalse;
        public readonly Variable DefinedOutputVariable;

        public override IList<Variable> DefinedVariables
        {
            get
            {
                List<Variable> l = new List<Variable>();

                if(DefinedOutputVariable != null)
                    l.Add(DefinedOutputVariable);

                ListHelper.AddUniqueRange(l, IfTrue.DefinedVariables);
                if (IfFalse != null)
                    ListHelper.AddUniqueRange(l, IfFalse.DefinedVariables);
                return l;
            }
        }
        public override IList<Variable> WritenVariables
        {
            get
            {
                List<Variable> l = new List<Variable>();
                l.AddRange(IfTrue.WritenVariables);
                if (IfFalse != null)
                    ListHelper.AddUniqueRange(l, IfFalse.WritenVariables);
                return l;
            }
        }
        public override IList<Variable> ReadVariables
        {
            get
            {
                List<Variable> l = new List<Variable>();
                l.AddRange(Condition.ReadVariables);
                ListHelper.AddUniqueRange(l, IfTrue.ReadVariables);
                if (IfFalse != null)
                    ListHelper.AddUniqueRange(l, IfFalse.ReadVariables);
                return l;
            }
        }
        public override bool Linear
        {
            get { return false; }
        }
        public override bool Const
        {
            get { return false; }
        }
    }
}
