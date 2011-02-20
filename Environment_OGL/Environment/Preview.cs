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
