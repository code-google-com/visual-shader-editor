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
