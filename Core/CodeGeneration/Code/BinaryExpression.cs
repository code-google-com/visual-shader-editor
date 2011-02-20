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
