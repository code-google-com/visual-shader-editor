using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public class VariableExpression : Expression
    {
        public VariableExpression(Variable v)
        {
            Variable = v;
        }

        protected override Format CalculateOutputFormat()
        {
            return Variable.Format;
        }

        public readonly Variable Variable;

        public override IList<Variable> ReadVariables
        {
            get { return new Variable[] { Variable }; }
        }
        public override bool Linear
        {
            get { return true; }
        }
        public override bool Const
        {
            get { return false; }
        }
    }
}
