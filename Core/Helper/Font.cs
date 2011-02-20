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
using System.Drawing;
using SystemFont = System.Drawing.Font;
using System.Drawing.Imaging;

namespace Core.Helper
{
    public class Font : IDisposable
    {
        static readonly float CHAR_SPACE = -4;
        static readonly int CHAR_COUNT = 390;
        static readonly int TEXTURE_SIZE = 1024;
        //static readonly SystemFont FONT = new SystemFont(new FontFamily("Times New Roman"), 37);
        static readonly SystemFont FONT = new SystemFont(FontFamily.GenericSerif, 37, FontStyle.Regular);

        public Font()
        {
            int stepSize = TEXTURE_SIZE / 16;
            Graphics g = Graphics.FromImage(m_bitmap);

            int x = 0;
            int y = 0;
            for (int i = 0; i < CHAR_COUNT; i++)
            {
                string c = new string(new[] { (char)i });
                int width = (int)g.MeasureString(c, FONT).Width;
                int height = (int)g.MeasureString(c, FONT).Height;

                //check space for next char
                if (x + width > TEXTURE_SIZE)
                {
                    x = 0;
                    y += stepSize;
                }

                //draw & save coord
                g.DrawString(c, FONT, Brushes.White, x, y);
                m_tCoord[i] = new Rectangle2f(x - CHAR_SPACE + 2, y, x + width + CHAR_SPACE * 2, y + height) / (float)TEXTURE_SIZE;
                m_aspect[i] = m_tCoord[i].Size.Y / m_tCoord[i].Size.X;

                x += width;
            }

            //var b = m_bitmap.LockBits(new Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            //System.Runtime.InteropServices.Marshal.Copy(m_rawBitmap, 0, bD.Scan0, m_rawBitmap.Length);

            //m_bitmap.UnlockBits(b);

           /* //draw check rect
            for (int i = 0; i < CHAR_COUNT; i++)
            {
                Rectangle2f r = m_tCoord[i];
                r *= (float)TEXTURE_SIZE;
                g.DrawLine(Pens.Blue, r.Min.X, r.Min.Y, r.Max.X, r.Max.Y);
                g.DrawRectangle(Pens.Blue, r.Min.X, r.Min.Y, r.Size.X, r.Size.Y);
            }*/

           // m_bitmap.Save("Font.bmp");
        }

        public virtual void Dispose()
        {
            m_bitmap.Dispose();
        }

        protected Bitmap Bitmap
        {
            get { return m_bitmap; }
        }
        protected Rectangle2f[] TCoord
        {
            get { return m_tCoord; }
        }
        protected float[] Aspect
        {
            get { return m_aspect; }
        }
        //protected 

        #region private

        readonly Rectangle2f[] m_tCoord = new Rectangle2f[CHAR_COUNT];
        readonly float[] m_aspect = new float[CHAR_COUNT];
        readonly Bitmap m_bitmap = new Bitmap(TEXTURE_SIZE, TEXTURE_SIZE, PixelFormat.Format32bppArgb);

        #endregion
    }
}
