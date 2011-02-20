using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment.Texture;
using SlimDX.Direct3D10;
using System.Windows.Forms;

namespace Environment_DX10.Environment
{
    public class TextureManager : ITextureManager
    {
        public TextureManager(Device d)
        {
            m_mainDevice = d;

            m_openFileDialog = new OpenFileDialog();
            m_openFileDialog.CheckFileExists = true;
            m_openFileDialog.CheckPathExists = true;

            m_defaultTexture = new Texture();
            //m_defaultTexture.D3DResurce = Texture2D.FromFile(m_mainDevice, "Content/NoTexture.jpg");
            m_defaultTexture.D3DResurce = Texture2D.FromMemory(m_mainDevice, Properties.Resources.NoTexture);
            m_defaultTexture.ResurceView = new ShaderResourceView(m_mainDevice, m_defaultTexture.D3DResurce);

            BorderTexture = new Texture();
            BorderTexture.D3DResurce = Texture2D.FromMemory(m_mainDevice, Properties.Resources.Border);
            BorderTexture.ResurceView = new ShaderResourceView(m_mainDevice, BorderTexture.D3DResurce);

            ButtonTexture = new Texture();
            ButtonTexture.D3DResurce = Texture2D.FromMemory(m_mainDevice, Properties.Resources.Button);
            ButtonTexture.ResurceView = new ShaderResourceView(m_mainDevice, ButtonTexture.D3DResurce);
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
            Texture t;
            if (!m_loadedTextures.TryGetValue(file, out t))
            {
                if (file == "")
                    return m_defaultTexture;

                try
                {
                    t = new Texture();
                    t.D3DResurce = Texture2D.FromFile(m_mainDevice, file);
                    t.ResurceView = new ShaderResourceView(m_mainDevice, t.D3DResurce);
                    m_loadedTextures.Add(file, t);
                }
                catch (Exception)
                {
                    return m_defaultTexture;
                }
            }

            return t;
        }

        Device m_mainDevice;
        OpenFileDialog m_openFileDialog;
        Texture m_defaultTexture;
        readonly Dictionary<string, Texture> m_loadedTextures = new Dictionary<string, Texture>();

        public readonly Texture BorderTexture;
        public readonly Texture ButtonTexture;

        public ITexture DefaultTexture
        {
            get { return m_defaultTexture; }
        }

    }
}
