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
