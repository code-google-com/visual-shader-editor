using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public class CallExpression : Expression
    {
        public enum Function
        {
            SampleTexture2D,
            Sin,
            Cos,
            Dot,
            PositionTransform,
            Cross,
            Normalize,
            Abs,
            Clamp,
            Lerp,
            Length,
            Pow,
        }

        public CallExpression(Function f, params Expression[] parameters)
        {
            FunctionType = f;
            Parameters = parameters;
        }

        protected override Core.Basic.Format CalculateOutputFormat()
        {
            switch (FunctionType)
            {
                case Function.SampleTexture2D: return Format.FLOAT4;
                case Function.Sin: return Format.FLOAT;
                case Function.Cos: return Format.FLOAT;
                case Function.Dot: return Format.FLOAT;
                case Function.PositionTransform: return Format.FLOAT4;
                case Function.Cross: return Parameters[0].OutputFormat;
                case Function.Normalize: return Parameters[0].OutputFormat;
                case Function.Abs: return Parameters[0].OutputFormat;
                case Function.Clamp: return Parameters[0].OutputFormat;
                case Function.Lerp: return Parameters[0].OutputFormat;
                case Function.Length: return Format.FLOAT;
                case Function.Pow: return Parameters[0].OutputFormat;
            }

            throw new NotImplementedException();
        }

        public readonly Function FunctionType;
        public readonly Expression[] Parameters;

        public override IList<Variable> ReadVariables
        {
            get
            {
                List<Variable> l = new List<Variable>();
                foreach (var e in Parameters)
                    l.AddRange(e.ReadVariables);
                return l;
            }
        }
        public override bool Linear
        {
            get
            {
                foreach (var e in Parameters)
                    if (!e.Linear)
                        return false;

                switch (FunctionType)
                {
                    case Function.SampleTexture2D: return false;
                    case Function.Sin: return false;
                    case Function.Cos: return false;
                    case Function.Dot: return false;
                    case Function.PositionTransform: return true;
                    case Function.Cross: return false;
                    case Function.Normalize: return false;
                    case Function.Abs: return true;
                    case Function.Clamp: return false;
                    case Function.Lerp: return true;
                    case Function.Length: return false;
                    case Function.Pow: return false;
                }

                throw new NotImplementedException();
            }
        }
        public override bool Const
        {
            get
            {
                foreach (var e in Parameters)
                    if (!e.Const)
                        return false;

                return true;
            }
        }
    }
}
