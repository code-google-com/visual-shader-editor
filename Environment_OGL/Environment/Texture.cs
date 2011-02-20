using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment.Texture;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace Environment_OGL.Environment
{
    public class Texture : ITexture
    {
        public Texture(int sizeX, int sizeY, PixelInternalFormat f, bool mipMap)
        {
            m_file = "RenderTarget";
            m_mipMap = mipMap;

            int currentTextureId;
            GL.GetInteger(GetPName.TextureBinding2D, out currentTextureId);

            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out m_textureResource);
            GL.BindTexture(TextureTarget.Texture2D, m_textureResource);

            var e1 = GL.GetError();

            if (f == PixelInternalFormat.DepthComponent24)
                GL.TexImage2D(TextureTarget.Texture2D, 0, f, sizeX, sizeY, 0, OpenTK.Graphics.OpenGL.PixelFormat.DepthComponent, PixelType.UnsignedByte, IntPtr.Zero);
            else
                GL.TexImage2D(TextureTarget.Texture2D, 0, f, sizeX, sizeY, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            var e2 = GL.GetError();

            if (m_mipMap)
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            else
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            var e3 = GL.GetError();


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            var e4 = GL.GetError();

            if (m_mipMap)
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            //restore to default
            GL.BindTexture(TextureTarget.Texture2D, currentTextureId);

            var e5 = GL.GetError();
        }

        public void GenerateMipMap()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, m_textureResource);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public Texture(string fileName)
        :this(System.IO.File.OpenRead(fileName)){
        }

        public Texture(Stream s)
        {
            m_file = "stream";

            int currentTextureId;
            GL.GetInteger(GetPName.TextureBinding2D, out currentTextureId);

            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out m_textureResource);
            GL.BindTexture(TextureTarget.Texture2D, m_textureResource);

            Bitmap bitmap = new Bitmap(s);
            s.Close();
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

           // GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, 1);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);
            bitmap.Dispose();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            //restore to default
            GL.BindTexture(TextureTarget.Texture2D, currentTextureId);
        }

        public void Dispose()
        {
            GL.DeleteTexture(m_textureResource);
            m_textureResource = -1;
        }

        public int TextureResource
        {
            get { return m_textureResource; }
            set { m_textureResource = value; }
        }

        public string File
        {
            get { return m_file; }
        }

        int m_textureResource = -1;
        string m_file;
        bool m_mipMap;

    }
}
