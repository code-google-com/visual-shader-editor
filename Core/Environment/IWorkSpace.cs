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
using System.Windows.Forms;
using Core.Environment.Texture;
using Core.Basic;
using Core.CodeGeneration;
using Core.Main;

namespace Core.Environment
{
    public struct ColorLine
    {
        public Line2f Line;
        public Vector4f Color;
    }

    public struct ColorRectangle
    {
        public static ColorRectangle CreateBorder(Rectangle2f r, Vector4f c)
        {
            ColorRectangle cr;

            cr.Rectangle = r;
            cr.Color = c;
            cr.Preview = null;
            cr.Texture = TextureType.Border;

            return cr;
        }
        public static ColorRectangle CreateButton(Rectangle2f r, Vector4f c)
        {
            ColorRectangle cr;

            cr.Rectangle = r;
            cr.Color = c;
            cr.Preview = null;
            cr.Texture = TextureType.Button;

            return cr;
        }
        public static ColorRectangle CreatePreview(Rectangle2f r, IPreview p)
        {
            ColorRectangle cr;

            cr.Rectangle = r;
            cr.Color = new Vector4f(1,1,1,1);
            cr.Preview = p;
            cr.Texture = TextureType.Preview;

            return cr;
        }

        public Rectangle2f Rectangle;
        public Vector4f Color;
        public IPreview Preview;
        public enum TextureType
        {
            None,
            Border,
            Button,
            Preview
        }
        public TextureType Texture;
    }

    public struct ColorText
    {
        public string Text;
        public Vector2f Position;
        public float Height;
        public Vector4f Color;
    }

    public interface IWorkSpace
    {
        //--- Draw ---
        void BeginRender(Vector4f clearColor, float zoom);
        void EndRender();
        void DrawLines(IList<Line2f> lines, float size, Vector4f color);
        void DrawRectangles(IList<ColorRectangle> rects);
        void DrawTexts(IList<ColorText> texts);

        string PreviewFileName { set; }
        string ReleaseFileName { set; }

        IPreview CreatePreview(BaseBlock bb);
        ICompiledShader Compile(ShaderCode sc, string outFileName);

        ITextureManager TextureManager { get; }

        bool RefreshPreview();
        ICompiledShader PreviewShader { get; }

        BlockManager BlockManager { get; }
        Control Control { get; }

        ISystemParameters SystemParameters { get; }
    }
}
