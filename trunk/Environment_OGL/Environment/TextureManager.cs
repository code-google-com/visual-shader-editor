using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment.Texture;
using System.Windows.Forms;
using OpenTK.Graphics;

namespace Environment_OGL.Environment
{
    public class TextureManager : ITextureManager
    {
        public TextureManager(WorkSpace owner)
        {
            m_openFileDialog = new OpenFileDialog();
            m_openFileDialog.CheckFileExists = true;
            m_openFileDialog.CheckPathExists = true;

            m_defaultTexture = new Texture("Content/NoTexture.jpg");
            BorderTexture = new Texture("Content/Border.png");
            ButtonTexture = new Texture("Content/Button.png");
        }

        public string ShowTextureBrowser()
        {
            if (m_openFileDialog.ShowDialog() == DialogResult.OK)
                return m_openFileDialog.FileName;

            return null;
            //throw new NotImplementedException();
        }

        public ITexture LoadTexture(string file)
        {
            Texture t = null;

            if (!m_loadedTextures.TryGetValue(file, out t))
            {
                if (file == "")
                    return m_defaultTexture;

                try
                {
                    t = new Texture(file);
                    m_loadedTextures.Add(file, t);
                }
                catch (Exception)
                {
                    return m_defaultTexture;
                }
            }

            return t;
        }

        OpenFileDialog m_openFileDialog;
        Texture m_defaultTexture;

        public ITexture DefaultTexture
        {
            get { return m_defaultTexture; }
        }

        public readonly Texture BorderTexture;
        public readonly Texture ButtonTexture;
        readonly Dictionary<string, Texture> m_loadedTextures = new Dictionary<string, Texture>();

    }
}
