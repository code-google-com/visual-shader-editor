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
