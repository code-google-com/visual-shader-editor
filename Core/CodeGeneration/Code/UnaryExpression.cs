using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public class UnaryExpression : Expression
    {
        public enum Operators
        {
            Minus
        }

        public UnaryExpression(Operators op, Expression exp)
        {
            Operator = op;
            Expression = exp;
        }

        protected override Format CalculateOutputFormat()
        {
            return Expression.OutputFormat;
        }

        public readonly Operators Operator;
        public readonly Expression Expression;

        public override IList<Variable> ReadVariables
        {
            get { return Expression.ReadVariables; }
        }
        public override bool Linear
        {
            get
            {
                switch (Operator)
                {
                    case Operators.Minus: return true;
                }

                throw new NotImplementedException();
            }
        }
        public override bool Const
        {
            get { return Expression.Const; }
        }
    }
}
