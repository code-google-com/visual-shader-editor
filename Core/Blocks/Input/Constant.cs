using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Core.CodeGeneration;
using System.Diagnostics;
using System.Globalization;
using Core.CodeGeneration.Code;
using Core.Main;

namespace Core.Blocks.Input
{
    [Block(Name = "Constant", Path = "Input")]
    public class Constant : BaseBlock
    {
        public Constant(BlockManager owner)
            : base(owner, new ConstantOptionsWindow())
        {
        }

        protected override BlockOutput CreateOutput()
        {
            return new ValueBlockOutput(this, Format.FLOAT, "", new Vector1f(0));
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            foreach (var o in Outputs)
            {
                ValueBlockOutput cbo = (ValueBlockOutput)o;
                ConstExpression ce;
                switch (o.Format)
                {
                    case Format.FLOAT: ce = new ConstExpression((Vector1f)cbo.Value); break;
                    case Format.FLOAT2: ce = new ConstExpression((Vector2f)cbo.Value); break;
                    case Format.FLOAT3: ce = new ConstExpression((Vector3f)cbo.Value); break;
                    case Format.FLOAT4: ce = new ConstExpression((Vector4f)cbo.Value); break;
                    default: throw new Exception("wrong format");
                }

                sc.AddInstruction(new CreateVariableInstruction(
                    new BinaryExpression(BinaryExpression.Operators.Assign,
                        new VariableExpression(o.Variable), ce)));
            }
        }

        public ValueBlockOutput CreateAndAddOutput(Format f, string name, string v)
        {
            ValueBlockOutput o = (ValueBlockOutput)CreateOutput();
            o.Name = name;
            o.FromString(v, f);

            AddOutput(o);

            return o;
        }
    }
}
