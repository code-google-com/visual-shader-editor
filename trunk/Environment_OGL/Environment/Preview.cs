using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using System.Windows.Forms;
using Core.CodeGeneration;
using OpenTK.Graphics;
using OpenTK.Platform;
using OpenTK.Graphics.OpenGL;

namespace Environment_OGL.Environment
{
    public class Preview : IPreview
    {
        static int RENDER_TARGET_SIZE = 256;

        internal Preview(GraphicsContext d, Core.Basic.BaseBlock bb, WorkSpace owner)
        {
            m_bb = bb;

            m_renderTarget = new Texture(RENDER_TARGET_SIZE, RENDER_TARGET_SIZE, PixelInternalFormat.Rgba8, true);
            var e0 = GL.GetError();
            m_renderTargetDepth = new Texture(RENDER_TARGET_SIZE, RENDER_TARGET_SIZE, PixelInternalFormat.DepthComponent24, false);
            var e1 = GL.GetError();

            GL.GenFramebuffers(1, out m_fbo);
        }

        internal void Begin(CompiledEffect ce)
        {
            GL.UseProgram(ce.m_program);

            ce.SetDebugOutput(m_bb.Outputs.Count > 0 ? m_bb.Outputs[0].Variable : null);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, m_fbo);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, m_renderTarget.TextureResource, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, m_renderTargetDepth.TextureResource, 0);
            DrawBuffersEnum[] fb = new[] { DrawBuffersEnum.ColorAttachment0 };
            GL.DrawBuffers(1, fb);

            var c = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

            GL.Viewport(0, 0, 256, 256);

            GL.ClearColor(0, 0, 0, 0);
            GL.ClearDepth(1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            
        }
        internal void End()
        {
            GL.UseProgram(0);

            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, 0, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, 0, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            GL.BindTexture(TextureTarget.Texture2D, m_renderTarget.TextureResource);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            m_renderTarget.Dispose();
            m_renderTargetDepth.Dispose();
            GL.DeleteRenderbuffers(1, ref m_fbo);
        }

        public Texture RenderTarget
        {
            get
            {
                return m_renderTarget;
            }
        }

        Texture m_renderTarget;
        Texture m_renderTargetDepth;
        int m_fbo;

        Core.Basic.BaseBlock m_bb;

    }
}
