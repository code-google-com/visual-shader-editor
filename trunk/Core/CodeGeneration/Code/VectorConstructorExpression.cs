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
