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
using Core.Main;
using Core.CodeGeneration;
using Core.CodeGeneration.Code;
using Core.Helper;

namespace Core.Blocks.Math.Other
{
    [Block(Name = "Clamp", Path = "Math/Other")]
    public class Clamp : BaseBlock
    {
        static readonly string BLOCK_XML_MIN_CONST_VALUE_ATTRIBUTE_NAME = "MinConstValue";
        static readonly string BLOCK_XML_MAX_CONST_VALUE_ATTRIBUTE_NAME = "MaxConstValue";

        public Clamp(BlockManager owner)
            : base(owner, new ClampOptionsWindow())
        {
            AddInput(new BlockInput(this, "Value"));
            AddInput(new BlockInput(this, "Min"));
            AddInput(new BlockInput(this, "Max"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Output"));
            RaiseDataChanged();
        }
        protected override void DataChanged()
        {
            if (Inputs.Count == 3 && Outputs.Count == 1)
            {
                //ignore min, max, only input value is important
                Outputs[0].Format = InstructionHelper.FindBigestInput(Inputs[0]);

                //disconnect disabled inputs
                if(m_min != null)
                    this.Disconnect(Inputs[1]);
                if(m_max != null)
                    this.Disconnect(Inputs[2]);
            }
            base.DataChanged();
        }

        public object Min
        {
            get { return m_min != null ? m_min.Value : null; }
            set
            {
                if (value != null)
                {
                    Inputs[1].Name = "Disabled";
                    m_min = ConstExpression.FromObject(value);
                }
                else
                {
                    Inputs[1].Name = "Min";
                    m_min = null;
                }
            }
        }
        public object Max
        {
            get { return m_max != null ? m_max.Value : null; }
            set
            {
                if (value != null)
                {
                    Inputs[2].Name = "Disabled";
                    m_max = ConstExpression.FromObject(value);
                }
                else
                {
                    Inputs[2].Name = "Max";
                    m_max = null;
                }
            }
        }
        public Format MinConstFormat
        {
            get { return m_min != null ? m_min.Format : Format.NONE; }
        }
        public Format MaxConstFormat
        {
            get { return m_max != null ? m_max.Format : Format.NONE; }
        }

        public override void Load(System.Xml.XmlElement node)
        {
            base.Load(node);

            if (node.GetAttributeNode(BLOCK_XML_MIN_CONST_VALUE_ATTRIBUTE_NAME) != null)
                Min = VectorHelper.FromString(node.GetAttribute(BLOCK_XML_MIN_CONST_VALUE_ATTRIBUTE_NAME));
            else
                Min = null;


            if (node.GetAttributeNode(BLOCK_XML_MAX_CONST_VALUE_ATTRIBUTE_NAME) != null)
                Max = VectorHelper.FromString(node.GetAttribute(BLOCK_XML_MAX_CONST_VALUE_ATTRIBUTE_NAME));
            else
                Max = null;
        }

        public override void Save(System.Xml.XmlElement node)
        {
            base.Save(node);

            object min = Min;
            if (min != null)
                node.SetAttribute(BLOCK_XML_MIN_CONST_VALUE_ATTRIBUTE_NAME, min.ToString());

            object max = Max;
            if (max != null)
                node.SetAttribute(BLOCK_XML_MAX_CONST_VALUE_ATTRIBUTE_NAME, max.ToString());
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            Expression min;
            Expression max;

            if (m_min != null)
                min = InstructionHelper.ConvertExpressionTo(Outputs[0].Format, m_min);
            else
                min = InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[1]);

            if (m_max != null)
                max = InstructionHelper.ConvertExpressionTo(Outputs[0].Format, m_max);
            else
                max = InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[2]);

            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.Clamp,
                        InstructionHelper.ConvertInputTo(Outputs[0].Format, Inputs[0]), min, max))));
        }

        #region private

        ConstExpression m_min;
        ConstExpression m_max;

        #endregion
    }
}
