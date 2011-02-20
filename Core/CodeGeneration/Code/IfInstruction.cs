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
