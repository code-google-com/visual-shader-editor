using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using Core.Environment;
using Core.Basic;
using OpenTK.Graphics.OpenGL;

namespace Environment_OGL.Environment
{
    public class Font : Core.Helper.Font
    {
        public Font(WorkSpace owner)
        {
            m_owner = owner;

            MemoryStream ms = new MemoryStream();
            Bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;

            m_texture = new Texture(ms);
        }

        public void DrawString(ColorText t)
        {
            if (t.Text.Length == 0)
                return;

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, m_texture.TextureResource);
            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Vector2f charBegin = t.Position;
            GL.Begin(BeginMode.Quads);
            for (int i = 0; i < t.Text.Length; i++)
            {
                int charId = t.Text[i];
                Vector2f charEnd = charBegin + new Vector2f(t.Height / Aspect[charId], t.Height);

                GL.Color4(t.Color.X, t.Color.Y, t.Color.Z, t.Color.W);

                {
                    var p = m_owner.RescalePosition(charBegin);
                    var tc = TCoord[charId].Min;
                    GL.TexCoord2(tc.X, tc.Y);
                    GL.Vertex2(p.X, p.Y);
                }
                {
                    var p = m_owner.RescalePosition(new Vector2f(charEnd.X, charBegin.Y));
                    var tc = new Vector2f(TCoord[charId].Max.X, TCoord[charId].Min.Y);
                    GL.TexCoord2(tc.X, tc.Y);
                    GL.Vertex2(p.X, p.Y);
                }
                {
                    var p = m_owner.RescalePosition(charEnd);
                    var tc = TCoord[charId].Max;
                    GL.TexCoord2(tc.X, tc.Y);
                    GL.Vertex2(p.X, p.Y);
                }
                {
                    var p = m_owner.RescalePosition(new Vector2f(charBegin.X, charEnd.Y));
                    var tc = new Vector2f(TCoord[charId].Min.X, TCoord[charId].Max.Y);
                    GL.TexCoord2(tc.X, tc.Y);
                    GL.Vertex2(p.X, p.Y);
                }
                

                charBegin.X = charEnd.X;
            }
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Disable(EnableCap.Texture2D);

            GL.Disable(EnableCap.Blend);

        }

        public override void Dispose()
        {
            m_texture.Dispose();

            base.Dispose();
        }

        #region private

        readonly WorkSpace m_owner;
        readonly Texture m_texture;

        #endregion
    }
}
