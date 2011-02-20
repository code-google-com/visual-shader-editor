using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core.Main;
using Core.Environment.Texture;

namespace Core.Blocks.Texture
{
    public partial class SamplerWithTextureOptionsWindow : OptionsWindow
    {
        public SamplerWithTextureOptionsWindow()
        {
            InitializeComponent();            
        }

        private void button_SelectTexture_Click(object sender, EventArgs e)
        {
            string s = StaticBase.Singleton.Environment.ShowTextureBrowser(null);

            textBox_TextureFileName.Text = (s == null) ? "<null>" : s;
        }

        protected override void LoadFormData()
        {
            base.LoadFormData();
            textBox_BindName.Text = ((SamplerWithTexture)BaseBlock).BindName;
            textBox_TextureFileName.Text = ((SamplerWithTexture)BaseBlock).TextureFileName;
        }

        protected override void SaveFormData()
        {
            base.SaveFormData();
            ((SamplerWithTexture)BaseBlock).BindName = textBox_BindName.Text;
            ((SamplerWithTexture)BaseBlock).TextureFileName = textBox_TextureFileName.Text;
        }

        protected override bool Valid()
        {
            return BaseBlock.BlockManager.VariableManager.CheckIfNameIsAvailable(textBox_BindName.Text, BaseBlock);
        }
    }
}
