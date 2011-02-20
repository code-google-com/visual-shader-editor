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
using Core.CodeGeneration.Code;
using Core.Basic;
using Core.Var;
using System.Diagnostics;

namespace Core.CodeGeneration
{
    public static class InstructionHelper
    {
        public static float DEFAULT_VALUE = 0;

        public static Expression InputToExpression(BlockInput bi, Format defaultFormat, float defaultValue)
        {
            //if not connected
            if (bi.ConnectedTo == null)
            {
                switch (defaultFormat)
                {
                    case Format.FLOAT: return new ConstExpression(new Vector1f(defaultValue));
                    case Format.FLOAT2: return new ConstExpression(new Vector2f(defaultValue, defaultValue));
                    case Format.FLOAT3: return new ConstExpression(new Vector3f(defaultValue, defaultValue, defaultValue));
                    case Format.FLOAT4: return new ConstExpression(new Vector4f(defaultValue, defaultValue, defaultValue, defaultValue));

                    default: throw new ArgumentException("wrong format");
                }
            }

            return ConvertVariableTo(bi.ConnectedTo.Variable.Format, bi.ConnectedTo.Variable, defaultValue);
        }
        public static Expression ConvertInputTo(Format f, BlockInput bi)
        {
            return ConvertInputTo(f, bi, DEFAULT_VALUE);
        }
        public static Expression ConvertInputTo(Format f, BlockInput bi, float defaultValue)
        {
            //if not connected
            if (bi.ConnectedTo == null)
            {
                switch (f)
                {
                    case Format.FLOAT: return new ConstExpression(new Vector1f(defaultValue));
                    case Format.FLOAT2: return new ConstExpression(new Vector2f(defaultValue, defaultValue));
                    case Format.FLOAT3: return new ConstExpression(new Vector3f(defaultValue, defaultValue, defaultValue));
                    case Format.FLOAT4: return new ConstExpression(new Vector4f(defaultValue, defaultValue, defaultValue, defaultValue));

                    default: throw new ArgumentException("wrong format");
                }
            }

            return ConvertVariableTo(f, bi.ConnectedTo.Variable, defaultValue);
        }
        public static Expression ConvertVariableTo(Format f, Variable v)
        {
            return ConvertVariableTo(f, v, DEFAULT_VALUE);
        }
        public static Expression ConvertVariableTo(Format f, Variable v, float defaultValue)
        {
            return ConvertExpressionTo(f, new VariableExpression(v), defaultValue);
        }
        public static Expression ConvertExpressionTo(Format f, Expression e)
        {
            return ConvertExpressionTo(f, e, DEFAULT_VALUE);
        }
        public static Expression ConvertExpressionTo(Format f, Expression e, float defaultValue)
        {
            //if the same format as input
            if (e.OutputFormat == f)
                return e;

            //if convert to smaller vector
            if (FormatHelper.IsFirstBigger(e.OutputFormat, f))
            {
                switch (f)
                {
                    case Format.FLOAT: return new SwizzleExpression(e, VectorMembers.X);
                    case Format.FLOAT2: return new SwizzleExpression(e, VectorMembers.X, VectorMembers.Y);
                    case Format.FLOAT3: return new SwizzleExpression(e, VectorMembers.X, VectorMembers.Y, VectorMembers.Z);
                    default:
                        throw new Exception("you should not be here");
                }
            }

            //convert float to vector
            if (e.OutputFormat == Format.FLOAT)
            {
                switch (f)
                {
                    case Format.FLOAT2: return new VectorConstructorExpression(f, e, e);
                    case Format.FLOAT3: return new VectorConstructorExpression(f, e, e, e);
                    case Format.FLOAT4: return new VectorConstructorExpression(f, e, e, e, e);
                    default:
                        throw new Exception("you should not be here");
                }
            }

            //convert to bigger vector
            int sizeDifference = FormatHelper.Size(f) - FormatHelper.Size(e.OutputFormat);
            ConstExpression ce = null;

            switch (sizeDifference)
            {
                case 1: ce = new ConstExpression(new Vector1f(defaultValue)); break;
                case 2: ce = new ConstExpression(new Vector2f(defaultValue, defaultValue)); break;
                //case 3: ce = new ConstExpression(new Vector3f(defaultValue, defaultValue, defaultValue)); break;  //convert float to vector
                default:
                    throw new Exception("you should not be here");
            }

            return new VectorConstructorExpression(f, e, ce);
        }

        public static Instruction BinaryOperatorVector(BinaryExpression.Operators o, Variable outVariable, BlockInput b0, BlockInput b1, float secondDefaultValue)
        {
            return new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(outVariable),
                    new BinaryExpression(o, InstructionHelper.ConvertInputTo(outVariable.Format, b0), InstructionHelper.ConvertInputTo(outVariable.Format, b1, secondDefaultValue))));
        }
        public static Instruction BinaryOperatorVector(BinaryExpression.Operators o, Variable outVariable, BlockInput b0, BlockInput b1)
        {
            return BinaryOperatorVector(o, outVariable, b0, b1, DEFAULT_VALUE);
        }
        public static Format BinaryOperatorVectorSecondFloatExpressions(BlockInput b0, BlockInput b1, out Expression e0, out Expression e1)
        {
            e0 = InstructionHelper.InputToExpression(b0, Format.FLOAT, 0);
            e1 = InstructionHelper.InputToExpression(b1, Format.FLOAT, 0);

            //first must be bigger
            bool swaped = false;
            if (FormatHelper.IsFirstBigger(e1.OutputFormat, e0.OutputFormat))
            {
                Expression h = e0;
                e0 = e1;
                e1 = h;
                swaped = true;
            }

            //second must be float1 or equal
            if (e1.OutputFormat != Format.FLOAT && e0.OutputFormat != e1.OutputFormat)
            {
                e1 = InstructionHelper.ConvertInputTo(e0.OutputFormat, swaped ? b0 : b1);
            }

            return e0.OutputFormat;
        }
        public static Instruction BinaryOperatorVectorSecondFloat(BinaryExpression.Operators o, Variable outVariable, BlockInput b0, BlockInput b1)
        {
            Expression e0;
            Expression e1;
            BinaryOperatorVectorSecondFloatExpressions(b0, b1, out e0, out e1);

            Debug.Assert(e0.OutputFormat == outVariable.Format);

            //generate
            return new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(outVariable),
                    new BinaryExpression(o, e0, e1)));
        }
        public static Instruction CopyInputToOutput(Variable outVariable, Format convertInputToFormat, BlockInput input, bool assignOnly)
        {
            return new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(outVariable),
                    InstructionHelper.ConvertInputTo(convertInputToFormat, input)), assignOnly);
        }
        public static Instruction CreateVariableWithDefaultValue(Variable outVariable, float v)
        {
            ConstExpression ce = null;

            switch (outVariable.Format)
            {
                case Format.FLOAT: ce = new ConstExpression(new Vector1f(v)); break;
                case Format.FLOAT2: ce = new ConstExpression(new Vector2f(v, v)); break;
                case Format.FLOAT3: ce = new ConstExpression(new Vector3f(v, v, v)); break;
                case Format.FLOAT4: ce = new ConstExpression(new Vector4f(v, v, v, v)); break;

                default: throw new NotImplementedException();
            }

            return new CreateVariableInstruction(new BinaryExpression(BinaryExpression.Operators.Assign, new VariableExpression(outVariable), ce), false);
        }

        public static Expression InputToExpression(BlockInput b, Vector1f defaultValue)
        {
            if (b.ConnectedTo == null)
                return new ConstExpression(defaultValue);

            return ConvertVariableTo(b.ConnectedTo.Format, b.ConnectedTo.Variable);
        }
        public static Expression InputToExpression(BlockInput b, Vector2f defaultValue)
        {
            if (b.ConnectedTo == null)
                return new ConstExpression(defaultValue);

            return ConvertVariableTo(b.ConnectedTo.Format, b.ConnectedTo.Variable);
        }
        public static Expression InputToExpression(BlockInput b, Vector3f defaultValue)
        {
            if (b.ConnectedTo == null)
                return new ConstExpression(defaultValue);

            return ConvertVariableTo(b.ConnectedTo.Format, b.ConnectedTo.Variable);
        }
        public static Expression InputToExpression(BlockInput b, Vector4f defaultValue)
        {
            if (b.ConnectedTo == null)
                return new ConstExpression(defaultValue);

            return ConvertVariableTo(b.ConnectedTo.Format, b.ConnectedTo.Variable);
        }

        public static Format FindBigestInput(params BlockInput[] bi)
        {
            //output format
            Expression e;
            Format f = Format.FLOAT;

            foreach (var i in bi)
            {
                e = InstructionHelper.InputToExpression(i, Format.FLOAT, 0);
                if (FormatHelper.IsFirstBigger(e.OutputFormat, f))
                    f = e.OutputFormat;
            }

            return f;
        }

        public static Instruction CreateVariable(Variable outVar, Variable inVar)
        {
            return new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(outVar),
                    new VariableExpression(inVar)), false);
        }

        public static Instruction DebugInstruction(Variable selector, int debugId, Variable debugOutput, Variable currentVar)
        {
            //if
            return new IfInstruction(
                new BinaryExpression(BinaryExpression.Operators.Equal, new VariableExpression(selector), new ConstExpression(new Vector1f(debugId))),

                    //assign
                        new CreateVariableInstruction(
                            new BinaryExpression(BinaryExpression.Operators.Assign,
                                new VariableExpression(debugOutput), InstructionHelper.ConvertVariableTo(Format.FLOAT4, currentVar)), true));
        }
    }
}
