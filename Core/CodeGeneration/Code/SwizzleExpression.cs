using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public class SwizzleExpression : Expression
    {
        public SwizzleExpression(Expression e, params VectorMembers[] m)
        {
            //check parameters
            if (m.Length > 4)
                throw new ArgumentException("too many swizzle's");

            //swizzle dont work with float
            if (e.OutputFormat == Format.FLOAT)
                throw new ArgumentException("swizzle dont work with float");

            //no 'VectorMembers.None' inside 'm'
            for (int i = 0; i < m.Length; i++)
                if (m[i] == VectorMembers.None)
                    throw new ArgumentException("no 'VectorMembers.None' inside 'm'");


            //set data
            Expression = e;

            for (int i = 0; i < m.Length; i++)
                Swizzle[i] = m[i];
        }

        protected override Format CalculateOutputFormat()
        {
            if (Swizzle[3] != VectorMembers.None)
                return Format.FLOAT4;

            if (Swizzle[2] != VectorMembers.None)
                return Format.FLOAT3;

            if (Swizzle[1] != VectorMembers.None)
                return Format.FLOAT2;

            return Format.FLOAT;
        }

        public readonly Expression Expression;
        public readonly VectorMembers[] Swizzle = new VectorMembers[4]{VectorMembers.None,VectorMembers.None,VectorMembers.None,VectorMembers.None};

        public override IList<Variable> ReadVariables
        {
            get { return Expression.ReadVariables; }
        }
        public override bool Linear
        {
            get { return Expression.Linear; }
        }
        public override bool Const
        {
            get { return Expression.Const; }
        }
    }
}
