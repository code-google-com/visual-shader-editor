using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public class ConstExpression : Expression
    {
        public static ConstExpression FromObject(object v)
        {
            ConstExpression ce;
            if (v is Vector1f)
                ce = new ConstExpression((Vector1f)v);
            else if (v is Vector2f)
                ce = new ConstExpression((Vector2f)v);
            else if (v is Vector3f)
                ce = new ConstExpression((Vector3f)v);
            else if (v is Vector4f)
                ce = new ConstExpression((Vector4f)v);
            else
                throw new NotImplementedException();

            return ce;
        }

        public ConstExpression(Vector1f v) { Format = Format.FLOAT; Value = v; }
        public ConstExpression(Vector2f v) { Format = Format.FLOAT2; Value = v; }
        public ConstExpression(Vector3f v) { Format = Format.FLOAT3; Value = v; }
        public ConstExpression(Vector4f v) { Format = Format.FLOAT4; Value = v; }
        public ConstExpression(Matrix44f v) { Format = Format.FLOAT4X4; Value = v; }

        protected override Format CalculateOutputFormat()
        {
            return Format;
        }

        public readonly Format Format;
        public readonly object Value;

        public override IList<Variable> ReadVariables
        {
            get { return new Variable[] { }; }
        }
        public override bool Linear
        {
            get { return true; }
        }
        public override bool Const
        {
            get { return true; }
        }
    }
}
