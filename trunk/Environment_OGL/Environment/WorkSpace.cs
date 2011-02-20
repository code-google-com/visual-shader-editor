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
using Core.Environment;
using Core.CodeGeneration;
using System.IO;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK.Platform;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using Environment_OGL.Code;
using Core.Main;
using Core.Basic;
using Core.Helper;

namespace Environment_OGL.Environment
{
    public class WorkSpace : IWorkSpace
    {
        public WorkSpace(BlockManager bm, Control c)
        {
            m_bm = bm;

            m_control = c;
            m_control.Resize += new EventHandler(m_control_Resize);

            m_wi = Utilities.CreateWindowsWindowInfo(c.Handle);
            m_mainContext = new GraphicsContext(new GraphicsMode(new ColorFormat(32), 24, 8, 0, new ColorFormat(0), 2), m_wi, 3, 0, GraphicsContextFlags.Debug);

            m_mainContext.MakeCurrent(m_wi);
            m_mainContext.LoadAll();

            m_textureManager = new TextureManager(this);

            m_model = new Model(this);
            m_font = new Font(this);

            m_params = new SystemParameters(this);

         //   m_blockManager.OnAfterChange += new Action<BlockManager>(m_blockManager_OnAfterChange);
        }

        public string PreviewFileName { set { m_previewFileName = value; } }
        public string ReleaseFileName { set { m_releaseFileName = value; } }

        void m_control_Resize(object sender, EventArgs e)
        {
            m_mainContext.MakeCurrent(m_wi);
        }

        void GenerateReleaseFile()
        {
            ShaderCode sc;
            CompiledEffect ce;

            try
            {
                sc = InstructionGenerator.GenerateRelease(m_bm);
                ce = (CompiledEffect)Compile(sc, m_releaseFileName);
            }
            catch (Exception)
            {
                StaticBase.Singleton.Log.Write(Log.InfoType.ShaderError, "Shader Generation Fail\n");
            }
        }

        public bool RefreshPreview()
        {
            GenerateReleaseFile();

            ShaderCode sc;
            CompiledEffect ce;

            try
            {
                sc = InstructionGenerator.GenerateDebug(m_bm);
                ce = (CompiledEffect)Compile(sc, m_previewFileName);
            }
            catch (Exception)
            {
                StaticBase.Singleton.Log.Write(Log.InfoType.ShaderError, "Shader Generation Fail\n");
                return false;
            }

            if (m_previewEffect != null)
            {
                m_previewEffect.Dispose();
            }

            m_previewEffect = ce;

            //return ce;

            //--------------

            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);

            foreach (var p in m_previewList)
            {
                //-----------------
                p.Begin(m_previewEffect);
                {
                    //set parameters
                    foreach (var b in m_bm.Blocks)
                        b.SetShaderParameters(ce);

                    int pos0 = GL.GetAttribLocation(m_previewEffect.m_program, "POSITION0");
                    int tc0 = GL.GetAttribLocation(m_previewEffect.m_program, "TEXCOORD0");
                    int c0 = GL.GetAttribLocation(m_previewEffect.m_program, "COLOR0");                    
                    int n0 = GL.GetAttribLocation(m_previewEffect.m_program, "NORMAL0");
                    int b0 = GL.GetAttribLocation(m_previewEffect.m_program, "BINORMAL0");
                    int t0 = GL.GetAttribLocation(m_previewEffect.m_program, "TANGENT0");

                    GL.BindBuffer(BufferTarget.ArrayBuffer, m_model.Vbo);
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_model.Ibo);

                     GL.EnableVertexAttribArray(pos0);                     
                     GL.VertexAttribPointer(pos0, 3, VertexAttribPointerType.Float, false, m_model.Stride, m_model.PosOffset);
                     GL.EnableVertexAttribArray(tc0);
                     GL.VertexAttribPointer(tc0, 2, VertexAttribPointerType.Float, false, m_model.Stride, m_model.TCoordOffset);
                     GL.EnableVertexAttribArray(c0);
                     GL.VertexAttribPointer(c0, 4, VertexAttribPointerType.Float, false, m_model.Stride, m_model.ColorOffset);
                     GL.EnableVertexAttribArray(n0);
                     GL.VertexAttribPointer(n0, 3, VertexAttribPointerType.Float, false, m_model.Stride, m_model.NormalOffset);
                     GL.EnableVertexAttribArray(b0);
                     GL.VertexAttribPointer(b0, 3, VertexAttribPointerType.Float, false, m_model.Stride, m_model.BinormalOffset);
                     GL.EnableVertexAttribArray(t0);
                     GL.VertexAttribPointer(t0, 3, VertexAttribPointerType.Float, false, m_model.Stride, m_model.TangentOffset);

                     GL.DrawElements( m_model.BeginMode, m_model.IndicesCount * 3, DrawElementsType.UnsignedInt, 0);

                     GL.DisableVertexAttribArray(pos0);
                     GL.DisableVertexAttribArray(tc0);
                     GL.DisableVertexAttribArray(c0);
                     GL.DisableVertexAttribArray(n0);
                     GL.DisableVertexAttribArray(b0);
                     GL.DisableVertexAttribArray(t0);

                     GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                     GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    //GL.UseProgram(0);

                  /*  GL.Begin(BeginMode.Quads);

                    GL.VertexAttrib4(c0, 0.0f, 0.0f, 1.0f, 0.0f);
                    GL.VertexAttrib2(tc0, -1.0f, -1.0f);
                    GL.VertexAttrib2(tc1, 0.0f, 0.0f);
                    GL.VertexAttrib4(pos0, -0.9f, -0.9f, 0.9f, 1.0f);

                    GL.VertexAttrib4(c0, 0.0f, 1.0f, 0.0f, 0.0f);
                    GL.VertexAttrib2(tc0, 1.0f, -1.0f);
                    GL.VertexAttrib2(tc1, 1.0f, 0.0f);
                    GL.VertexAttrib4(pos0, 0.9f, -0.9f, 0.9f, 1.0f);

                    GL.VertexAttrib4(c0, 1.0f, 0.0f, 0.0f, 0.0f);
                    GL.VertexAttrib2(tc0, 1.0f, 1.0f);
                    GL.VertexAttrib2(tc1, 1.0f, 1.0f);
                    GL.VertexAttrib4(pos0, 0.9f, 0.9f, 0.9f, 1.0f);

                    GL.VertexAttrib4(c0, 1.0f, 1.0f, 1.0f, 0.0f);
                    GL.VertexAttrib2(tc0, -1.0f, 1.0f);
                    GL.VertexAttrib2(tc1, 0.0f, 1.0f);
                    GL.VertexAttrib4(pos0, -0.9f, 0.9f, 0.9f, 1.0f);

                    GL.End();*/
                }
                p.End();
            }

            for (int i = 0; i < 8; i++)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i);
                GL.Disable(EnableCap.Texture2D);
            }

            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);

            //------------

            return true;
        }

        void m_blockManager_OnAfterChange(BlockManager obj)
        {
            RefreshPreview();
        }

        public IPreview CreatePreview(Core.Basic.BaseBlock bb)
        {
            Preview p = new Preview(m_mainContext, bb, this);
            m_previewList.Add(p);

            return p;
        }

        public ICompiledShader Compile()
        {
            throw new NotImplementedException();
        }

        public Core.Environment.Texture.ITextureManager TextureManager
        {
            get { return m_textureManager; }
        }

        public ICompiledShader PreviewShader
        {
            get { return m_previewEffect; }
        }

        public BlockManager BlockManager
        {
            get { return m_bm; }
        }

        public Control Control
        {
            get { return m_control; }
        }

        internal void RemovePreview(Preview p)
        {
            m_previewList.Remove(p);
        }

        public GraphicsContext MainContext
        {
            get { return m_mainContext; }
            set { m_mainContext = value; }
        }

        public ICompiledShader Compile(ShaderCode sc, string outFileName)
        {
            GLSLBasicInstrutions b = new GLSLBasicInstrutions();

            StaticBase.Singleton.Log.Write(Log.InfoType.ShaderInfo, "Begin Translating code to HLSL\n");
            string v, g, p;
            b.GenerateCode(sc, out v, out g, out p);
            StaticBase.Singleton.Log.Write(Log.InfoType.ShaderInfo, "End Translating code to HLSL\n");

            if (outFileName != null && outFileName != "")
            {
                //if (g != null)
                //    File.WriteAllText("x.fx", "\n[Vertex]\n\n" + v + "\n[Geometry]\n\n" + g + "\n[Pixel]\n\n" + p);
                //else
                //File.WriteAllText("x.fx", "\n[Vertex]\n\n" + v + "\n[Pixel]\n\n" + p);
                File.WriteAllText(outFileName + ".vert", v);
                File.WriteAllText(outFileName + ".frag", p);
            }

            CompiledEffect ce = new CompiledEffect(this, v, g, p, sc);

            return ce;
        }


        public void DrawLines(IList<Line2f> lines, float size, Vector4f color)
        {
            foreach (var l in lines)
            {
                var p0 = RescalePosition(l.Point0);
                var p1 = RescalePosition(l.Point1);

                GL.Begin(BeginMode.Lines);

                GL.Vertex2(p0.X, p0.Y);
                GL.Vertex2(p1.X, p1.Y);

                GL.End();
            }
        }
        public void DrawRectangles(IList<ColorRectangle> rects)
        {
            for (int i = 0; i < rects.Count; i++)
                DrawRectangle(rects[i]);
        }
        public void DrawTexts(IList<ColorText> texts)
        {
            for (int i = 0; i < texts.Count; i++)
                m_font.DrawString(texts[i]);
        }

        void DrawRectangle(ColorRectangle rect)
        {
            var min = RescalePosition(rect.Rectangle.Min);
            var max = RescalePosition(rect.Rectangle.Max);

            if (rect.Texture == ColorRectangle.TextureType.Preview)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, ((Preview)rect.Preview).RenderTarget.TextureResource);
                //GL.BindTexture(TextureTarget.Texture2D, ((Texture)m_textureManager.DefaultTexture).TextureResource);
            }

            GL.Begin(BeginMode.Quads);

            GL.Color4(rect.Color.X, rect.Color.Y, rect.Color.Z, rect.Color.W);

            GL.TexCoord2(0, 1);
            GL.Vertex2(min.X, min.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(max.X, min.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(max.X, max.Y);
            GL.TexCoord2(0, 0);
            GL.Vertex2(min.X, max.Y);

            GL.End();

            if (rect.Texture == ColorRectangle.TextureType.Preview)
            {
                GL.Disable(EnableCap.Texture2D);
            }
        }

        public void BeginRender(Vector4f clearColor, float zoom)
        {
            m_mainContext.MakeCurrent(m_wi);

            m_zoom = zoom;

            GL.Viewport(0, 0, m_control.Width, m_control.Height);

            GL.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W);
            GL.ClearDepth(1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.UseProgram(0);
        }
        public void EndRender()
        {
            m_mainContext.SwapBuffers();
        }

        Vector2f Scale
        {
            get { return new Vector2f((m_zoom * 2 / (float)m_control.Size.Width), (-m_zoom * 2 / (float)m_control.Size.Height)); }
        }

        internal Vector2f RescalePosition(Vector2f p)
        {
            p *= Scale;
            p += new Vector2f(-1, 1);
            return p;
        }

        public ISystemParameters SystemParameters
        {
            get { return m_params; }
        }

        #region private

        Control m_control;
        BlockManager m_bm;
        Model m_model;
        SystemParameters m_params;
        Font m_font;

        readonly List<Preview> m_previewList = new List<Preview>();
        readonly TextureManager m_textureManager;

        CompiledEffect m_previewEffect;
        GraphicsContext m_mainContext;
        IWindowInfo m_wi;
        float m_zoom;

        string m_previewFileName;
        string m_releaseFileName;

        #endregion

    }
}
