/*
Copyright (c) 2011, Pawel Szczurek
All rights reserved.


Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:


Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

Neither the name of the <ORGANIZATION> nor the names of its contributors may be used to endorse or promote products derived from this software without
specific prior written permission.


THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using System.Diagnostics;
using System.Globalization;
using Core.Var;

namespace Environment_OGL.Code
{
    public class GLSLBasicInstrutions : BasicInstructions
    {
        public string TranslateParameter(Variable d)
        {
            switch (d.Format)
            {
                case Format.TEXTURE: return "";// string.Format("texture {0};\n", d.Variable.Name);
                case Format.SAMPLER: return string.Format(
                   "uniform sampler2D {0};\n", d.Name.Substring(0, d.Name.Length - 8));
                case Format.FLOAT: return string.Format("uniform float {0};\n", d.Name);
                case Format.FLOAT2: return string.Format("uniform vec2 {0};\n", d.Name);
                case Format.FLOAT3: return string.Format("uniform vec3 {0};\n", d.Name);
                case Format.FLOAT4: return string.Format("uniform vec4 {0};\n", d.Name);
                case Format.FLOAT4X4: return string.Format("uniform mat4 {0};\n", d.Name);
            }

            throw new NotImplementedException();
        }

        public override string Translate(Core.CodeGeneration.Code.Instruction i)
        {
            if (i is CreateVariableInstruction)
                return Translate((CreateVariableInstruction)i);

            if (i is IfInstruction)
                return Translate((IfInstruction)i);

            throw new NotImplementedException();
        }

        string Translate(CreateVariableInstruction i)
        {
            VariableExpression l = (VariableExpression)i.AssignExpression.LeftExpression;

            if (i.AssignOnly)
                return string.Format("{0} = {1};\n", l.Variable.Name, Translate(i.AssignExpression.RightExpression));
            else
                return string.Format("{0} {1} = {2};\n", FormatToString(l.Variable.Format), l.Variable.Name, Translate(i.AssignExpression.RightExpression));
        }

        string Translate(IfInstruction i)
        {
            string s = "";

            if (i.DefinedOutputVariable != null)
                s += string.Format("{0} {1};\n\t", FormatToString(i.DefinedOutputVariable.Format), i.DefinedOutputVariable.Name);
            s += string.Format("if({0})\n\t\t{1}", Translate(i.Condition), Translate(i.IfTrue));
            if (i.IfFalse != null)
                s += "\telse\n\t\t" + Translate(i.IfFalse);

            return s;
        }

        string Translate(Expression e)
        {
            if (e is ConstExpression)
                return Translate((ConstExpression)e);

            if (e is VariableExpression)
                return Translate((VariableExpression)e);

            if (e is UnaryExpression)
                return Translate((UnaryExpression)e);

            if (e is BinaryExpression)
                return Translate((BinaryExpression)e);

            if (e is CallExpression)
                return Translate((CallExpression)e);

            if (e is SwizzleExpression)
                return Translate((SwizzleExpression)e);

            if (e is VectorConstructorExpression)
                return Translate((VectorConstructorExpression)e);

            throw new NotImplementedException();
        }

        string Translate(ConstExpression e)
        {
            if (e.Value is Vector1f)
                return ((Vector1f)e.Value).X.ToString(CultureInfo.InvariantCulture);

            if (e.Value is Vector2f)
            {
                Vector2f v = (Vector2f)e.Value;
                return string.Format("vec2({0}, {1})", v.X.ToString(CultureInfo.InvariantCulture), v.Y.ToString(CultureInfo.InvariantCulture));
            }

            if (e.Value is Vector3f)
            {
                Vector3f v = (Vector3f)e.Value;
                return string.Format("vec3({0}, {1}, {2})", v.X.ToString(CultureInfo.InvariantCulture), v.Y.ToString(CultureInfo.InvariantCulture), v.Z.ToString(CultureInfo.InvariantCulture));
            }

            if (e.Value is Vector4f)
            {
                Vector4f v = (Vector4f)e.Value;
                return string.Format("vec4({0}, {1}, {2}, {3})", v.X.ToString(CultureInfo.InvariantCulture), v.Y.ToString(CultureInfo.InvariantCulture), v.Z.ToString(CultureInfo.InvariantCulture), v.W.ToString(CultureInfo.InvariantCulture));
            }

            if (e.Value is Matrix44f)
            {
                Matrix44f m = (Matrix44f)e.Value;
                return string.Format("mat4({0},{1},{2},{3}, {4},{5},{6},{7}, {8},{9},{10},{11}, {12},{13},{14},{15})",
                    m.Column0.X.ToString(CultureInfo.InvariantCulture), m.Column0.Y.ToString(CultureInfo.InvariantCulture), m.Column0.Z.ToString(CultureInfo.InvariantCulture), m.Column0.W.ToString(CultureInfo.InvariantCulture),
                    m.Column1.X.ToString(CultureInfo.InvariantCulture), m.Column1.Y.ToString(CultureInfo.InvariantCulture), m.Column1.Z.ToString(CultureInfo.InvariantCulture), m.Column1.W.ToString(CultureInfo.InvariantCulture),
                    m.Column2.X.ToString(CultureInfo.InvariantCulture), m.Column2.Y.ToString(CultureInfo.InvariantCulture), m.Column2.Z.ToString(CultureInfo.InvariantCulture), m.Column2.W.ToString(CultureInfo.InvariantCulture),
                    m.Column3.X.ToString(CultureInfo.InvariantCulture), m.Column3.Y.ToString(CultureInfo.InvariantCulture), m.Column3.Z.ToString(CultureInfo.InvariantCulture), m.Column3.W.ToString(CultureInfo.InvariantCulture));
            }

            throw new NotImplementedException();
        }

        string Translate(VariableExpression e)
        {
            return e.Variable.Name;
        }

        string Translate(UnaryExpression e)
        {
            switch (e.Operator)
            {
                case UnaryExpression.Operators.Minus: return "-" + Translate(e.Expression);
            }

            throw new NotImplementedException();
        }

        string Translate(BinaryExpression e)
        {
            switch (e.Operator)
            {
                case BinaryExpression.Operators.Add: return "(" + Translate(e.LeftExpression) + " + " + Translate(e.RightExpression) + ")";
                case BinaryExpression.Operators.Sub: return "(" + Translate(e.LeftExpression) + " - " + Translate(e.RightExpression) + ")";
                case BinaryExpression.Operators.Mul: return "(" + Translate(e.LeftExpression) + " * " + Translate(e.RightExpression) + ")";
                case BinaryExpression.Operators.Div: return "(" + Translate(e.LeftExpression) + " / " + Translate(e.RightExpression) + ")";

                case BinaryExpression.Operators.Equal: return "(" + Translate(e.LeftExpression) + " == " + Translate(e.RightExpression) + ")";
                case BinaryExpression.Operators.Less: return "(" + Translate(e.LeftExpression) + " < " + Translate(e.RightExpression) + ")";
            }

            throw new NotImplementedException();
        }

        string Translate(CallExpression e)
        {
            switch (e.FunctionType)
            {
                case CallExpression.Function.SampleTexture2D: return string.Format("texture({1}, ({2}))", Translate(e.Parameters[0]), Translate(e.Parameters[1]), Translate(e.Parameters[2]));
                case CallExpression.Function.Sin: return string.Format("sin({0})", Translate(e.Parameters[0]));
                case CallExpression.Function.Cos: return string.Format("cos({0})", Translate(e.Parameters[0]));
                case CallExpression.Function.Dot: return string.Format("dot({0}, {1})", Translate(e.Parameters[0]), Translate(e.Parameters[1]));
                case CallExpression.Function.PositionTransform: return string.Format("({0} * {1})", Translate(e.Parameters[0]), Translate(e.Parameters[1]));
                case CallExpression.Function.Cross: return string.Format("cross({0}, {1})", Translate(e.Parameters[0]), Translate(e.Parameters[1]));
                case CallExpression.Function.Normalize: return string.Format("normalize({0})", Translate(e.Parameters[0]));
                case CallExpression.Function.Abs: return string.Format("abs({0})", Translate(e.Parameters[0]));
                case CallExpression.Function.Clamp: return string.Format("clamp({0}, {1}, {2})", Translate(e.Parameters[0]), Translate(e.Parameters[1]), Translate(e.Parameters[2]));
                case CallExpression.Function.Lerp: return string.Format("mix({0}, {1}, {2})", Translate(e.Parameters[0]), Translate(e.Parameters[1]), Translate(e.Parameters[2]));
                case CallExpression.Function.Length: return string.Format("length({0})", Translate(e.Parameters[0]));
                case CallExpression.Function.Pow: return string.Format("pow({0}, {1})", Translate(e.Parameters[0]), Translate(e.Parameters[1]));
            }

            throw new NotImplementedException();
        }

        string Translate(SwizzleExpression e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Translate(e.Expression)).Append(".");

            foreach (var s in e.Swizzle)
            {
                switch (s)
                {
                    case VectorMembers.X: sb.Append("x"); break;
                    case VectorMembers.Y: sb.Append("y"); break;
                    case VectorMembers.Z: sb.Append("z"); break;
                    case VectorMembers.W: sb.Append("w"); break;
                }
            }

            return sb.ToString();
        }

        string Translate(VectorConstructorExpression e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(FormatToString(e.Format)).Append('(');

            for (int i = 0; i < e.Values.Length; i++)
            {
                sb.Append(Translate(e.Values[i]));
                if (i + 1 != e.Values.Length)
                    sb.Append(", ");
                else
                    sb.Append(")");
            }

            return sb.ToString();
        }

        string FormatToString(Format f)
        {
            switch (f)
            {
                case Format.FLOAT: return "float";
                case Format.FLOAT2: return "vec2";
                case Format.FLOAT3: return "vec3";
                case Format.FLOAT4: return "vec4";
            }

            throw new NotImplementedException();
        }

        public void GenerateCode(ShaderCode sc, out string vs, out string gs, out string ps)
        {
            StringBuilder vssb = new StringBuilder();
            StringBuilder gssb = new StringBuilder();
            StringBuilder pssb = new StringBuilder();

            DefineExtra(sc, vssb, gssb, pssb);

            DefineParameters(sc, vssb, gssb, pssb);

            DefineStructs(sc, vssb, gssb, pssb);

            DefineVertexShader(sc, vssb);

            DefinePixelShader(sc, pssb);

            vs = vssb.ToString();
            gs = gssb.ToString();
            ps = pssb.ToString();
        }

        //------------

        void DefineExtra(ShaderCode sc, StringBuilder vssb, StringBuilder gssb, StringBuilder pssb)
        {
            //150
            vssb.Append("#version 150\n\n");
            gssb.Append("#version 150\n\n");
            pssb.Append("#version 150\n\n");

        }
        void DefineParameters(ShaderCode sc, StringBuilder vssb, StringBuilder gssb, StringBuilder pssb)
        {
            vssb.Append("\n");
            gssb.Append("\n");
            pssb.Append("\n");

            foreach (var d in sc.Parameters)
            {
                vssb.Append(TranslateParameter(d));
                gssb.Append(TranslateParameter(d));
                pssb.Append(TranslateParameter(d));
            }

            vssb.Append("\n");
            gssb.Append("\n");
            pssb.Append("\n");
        }
        void DefineStructs(ShaderCode sc, StringBuilder vssb, StringBuilder gssb, StringBuilder pssb)
        {
            //vs in

            vssb.AppendFormat("\n");
            foreach (var i in sc.VSInput)
            {
                vssb.AppendFormat("in {0} {2}{3};\n", FormatToString(i.Format), i.Name, i.Semantic, i.SemanticIndex);
            }
            vssb.AppendFormat("\n");

            //vs out, ps in
            vssb.AppendFormat("\n");
            pssb.AppendFormat("\n");
            foreach (var i in sc.VSToPS)
            {
                vssb.AppendFormat("out {0} vstops_{1};\n", FormatToString(i.Format), i.Name);
                pssb.AppendFormat("in  {0} vstops_{1};\n", FormatToString(i.Format), i.Name);

            }
            //vssb.AppendFormat("out vec4 gl_Position;\n");
            vssb.AppendFormat("\n");
            pssb.AppendFormat("\n");
            
            //ps out
            pssb.AppendFormat("\n");
            foreach (var i in sc.PSOutput)
            {
                pssb.AppendFormat("out {0} psout_{1};\n", FormatToString(i.Format), i.Name, i.SemanticIndex + 1);
            }
            pssb.AppendFormat("\n");
        }

        public void DefineVertexShader(ShaderCode sc, StringBuilder vssb)
        {
            vssb.Append("\nvoid main(){\n");

            //copy var from app
            foreach (var v in sc.VSInput)
                vssb.AppendFormat("\t{0} {1} = {2}{3};\n", FormatToString(v.Format), v.Name, v.Semantic, v.SemanticIndex);

            vssb.Append("\n");

            //instructions
            foreach (var i in sc.VSInstructions)
                vssb.Append("\t" + Translate(i));

            vssb.Append("\n");

            //send to ps
            vssb.Append("\n");
            foreach (var v in sc.VSToPS)
                vssb.AppendFormat("\tvstops_{0} = {0};\n", v.Name);
            vssb.AppendFormat("\tgl_Position = {0};\n", sc.OutputPosition.Name);
            vssb.Append("}\n\n");

        }

        public void DefinePixelShader(ShaderCode sc, StringBuilder pssb)
        {
            pssb.Append("\nvoid main()\n{\n");

            //variables from vs
            foreach (var v in sc.VSToPS)
                pssb.AppendFormat("\t{0} {1} = vstops_{1};\n", FormatToString(v.Format), v.Name);

            //instructions
            foreach (var i in sc.PSInstructions)
            {
                pssb.Append("\t" + Translate(i));
            }

            pssb.Append('\n');

            //copy to out
            pssb.Append('\n');
            foreach(var v in sc.PSOutput)
                pssb.AppendFormat("\tpsout_{0} = {0};\n", v.Name);

            pssb.Append("\n}\n");
        }
    }
}
