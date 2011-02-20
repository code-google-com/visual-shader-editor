using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration;
using Core.Environment.Texture;
using Core.Main;
using System.Xml;
using Core.CodeGeneration.Code;
using Core.Var;
using System.Diagnostics;

namespace Core.Blocks.Texture
{
    [Block(Name = "SamplerWithTexture", Path = "Texture")]
    public class SamplerWithTexture : BaseBlock
    {
        public static readonly string BLOCK_XML_BINDNAME_ATTRIBUTE_NAME = "BindName";
        public static readonly string BLOCK_XML_FILENAME_ATTRIBUTE_NAME = "FileName";

        public SamplerWithTexture(BlockManager owner)
            : base(owner, new SamplerWithTextureOptionsWindow())
        {
            for (int i = 0; i < 1000; i++)
            {
                string name = string.Format("Texture{0}", i);
                if (owner.VariableManager.CheckIfNameIsAvailable(name))
                {
                    m_bindName = name;
                    break;
                }
            }

            UpdateVariables();

            AddInput(new BlockInput(this, "TCoord"));
            AddOutput(new BlockOutput(this, Format.FLOAT4, "Color"));
        }

        public override void Load(System.Xml.XmlElement node)
        {
            BlockRaiseDataChange();

            base.Load(node);
            BindName = node.GetAttribute(BLOCK_XML_BINDNAME_ATTRIBUTE_NAME);
            TextureFileName = BlockManager.Owner.Owner.ConvertResourcePathFromSaved(node.GetAttribute(BLOCK_XML_FILENAME_ATTRIBUTE_NAME));

            UnBlockRaiseDataChanged();
        }

        public override void Save(System.Xml.XmlElement node)
        {
            base.Save(node);

            node.SetAttribute(BLOCK_XML_BINDNAME_ATTRIBUTE_NAME, BindName);
            node.SetAttribute(BLOCK_XML_FILENAME_ATTRIBUTE_NAME, BlockManager.Owner.Owner.ConvertResourcePathForSave(TextureFileName));
        }

        public override void SetShaderParameters(Core.Environment.ICompiledShader s)
        {
            base.SetShaderParameters(s);

            s.SetTextureParameter(BindName, TextureFileName);
        }

        public string TextureFileName
        {
            get { return m_textureFileName; }
            set
            {
                if (m_textureFileName != value)
                {
                    m_textureFileName = value;
                    RaiseDataChanged();
                }
            }
        }
        public string BindName
        {
            get { return m_bindName; }
            set
            {
                if (m_bindName != value)
                {
                    m_bindName = value;
                    RaiseDataChanged();
                }
            }
        }

        protected internal override void GenerateCode(ShaderCodeGenerator sc)
        {
            //add definition            
            sc.AddParameter(m_texture);
            sc.AddParameter(m_sampler);

            //add instruction
            sc.AddInstruction(new CreateVariableInstruction(
                new BinaryExpression(BinaryExpression.Operators.Assign,
                    new VariableExpression(Outputs[0].Variable),
                    new CallExpression(CallExpression.Function.SampleTexture2D,
                        new VariableExpression(m_sampler), new VariableExpression(m_texture), InstructionHelper.ConvertInputTo(Format.FLOAT2, Inputs[0])))));
        }

        protected override void DataChanged()
        {
            UpdateVariables();

            base.DataChanged();
        }

        protected override void Clear()
        {
            if (m_texture != null)
            {
                BlockManager.VariableManager.DestroyVariable(m_texture, this);
                m_texture = null;
            }

            if (m_sampler != null)
            {
                BlockManager.VariableManager.DestroyVariable(m_sampler, this);
                m_sampler = null;
            }

            base.Clear();
        }

        #region private

        void UpdateVariables()
        {
            Debug.Assert(m_bindName != null && m_bindName != "");

            if (m_texture != null)
            {
                BlockManager.VariableManager.DestroyVariable(m_texture, this);
                m_texture = null;
            }

            if (m_sampler != null)
            {
                BlockManager.VariableManager.DestroyVariable(m_sampler, this);
                m_sampler = null;
            }

            m_texture = BlockManager.VariableManager.CreateVariable(BindName, Format.TEXTURE, VerticesStreamSemantic.NONE, 0, this);
            m_sampler = BlockManager.VariableManager.CreateVariable(BindName + "_Sampler", Format.SAMPLER, VerticesStreamSemantic.NONE, 0, this);
        }

        string m_textureFileName = "";
        string m_bindName = "";

        Variable m_sampler;
        Variable m_texture;

        #endregion
    }
}
