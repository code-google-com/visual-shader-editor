﻿/*
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
using Device10 = SlimDX.Direct3D10.Device;
using Buffer10 = SlimDX.Direct3D10.Buffer;
using Font10 = SlimDX.Direct3D10.Font;
using SlimDX.Direct3D10;
using System.IO;
using System.Drawing.Imaging;
using Core.Environment;
using SlimDX;
using Core.Basic;

namespace Environment_DX10.Environment
{
    public class Font : Core.Helper.Font
    {
        public Font(WorkSpace owner)
        {
            m_owner = owner;

            MemoryStream ms = new MemoryStream();
            Bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;

            m_texture = Texture2D.FromStream(m_owner.MainDevice, ms, (int)ms.Length);
            m_view = new ShaderResourceView(m_owner.MainDevice, m_texture);
        }

        public void DrawString(ColorText t)
        {
            if (t.Text.Length == 0)
                return;

            int len = t.Text.Length;
            int stride = 32;
            int bufferSize = len * 2 * 3 * stride;
            var stream = new DataStream(bufferSize, true, true);

            Vector2f charBegin = t.Position;
            for (int i = 0; i < t.Text.Length; i++)
            {
                int charId = t.Text[i];
                Vector2f charEnd = charBegin + new Vector2f(t.Height / Aspect[charId], t.Height);

                stream.Write(m_owner.RescalePosition(charBegin));
                stream.Write(TCoord[charId].Min);
                stream.Write(t.Color);
                stream.Write(m_owner.RescalePosition(new Vector2f(charEnd.X, charBegin.Y)));
                stream.Write(new Vector2f(TCoord[charId].Max.X, TCoord[charId].Min.Y));
                stream.Write(t.Color);
                stream.Write(m_owner.RescalePosition(new Vector2f(charBegin.X, charEnd.Y)));
                stream.Write(new Vector2f(TCoord[charId].Min.X, TCoord[charId].Max.Y));
                stream.Write(t.Color);

                stream.Write(m_owner.RescalePosition(new Vector2f(charBegin.X, charEnd.Y)));
                stream.Write(new Vector2f(TCoord[charId].Min.X, TCoord[charId].Max.Y));
                stream.Write(t.Color);
                stream.Write(m_owner.RescalePosition(new Vector2f(charEnd.X, charBegin.Y)));
                stream.Write(new Vector2f(TCoord[charId].Max.X, TCoord[charId].Min.Y));
                stream.Write(t.Color);
                stream.Write(m_owner.RescalePosition(charEnd));
                stream.Write(TCoord[charId].Max);
                stream.Write(t.Color);

                charBegin.X = charEnd.X;
            }

            stream.Position = 0;

            Buffer10 vertices = new SlimDX.Direct3D10.Buffer(m_owner.MainDevice, stream, new BufferDescription()
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = bufferSize,
                Usage = ResourceUsage.Default
            });
            stream.Dispose();

            InputElement[] InputElements = new[] {
                new InputElement("POSITION", 0, SlimDX.DXGI.Format.R32G32_Float, 0, 0),
                new InputElement("TEXCOORD", 0, SlimDX.DXGI.Format.R32G32_Float, 8, 0),
                new InputElement("COLOR", 0, SlimDX.DXGI.Format.R32G32B32A32_Float, 16, 0),
            };

            //--------------

            m_owner.BasicEffect.GetVariableByName("UseTexture").AsScalar().Set(1);
            m_owner.BasicEffect.GetVariableByName("Texture").AsResource().SetResource(m_view);

            EffectTechnique technique = m_owner.BasicEffect.GetTechniqueByName("QuadTechnique");

            m_owner.MainDevice.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            m_owner.MainDevice.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertices, stride, 0));

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                EffectPass pass = technique.GetPassByIndex(i);

                ShaderSignature ss = pass.Description.Signature;
                InputLayout layout = new InputLayout(m_owner.MainDevice, InputElements, ss);
                m_owner.MainDevice.InputAssembler.SetInputLayout(layout);

                pass.Apply();
                m_owner.MainDevice.Draw(len * 6, 0);

                layout.Dispose();
            }

            vertices.Dispose();
        }

        public override void Dispose()
        {
            m_owner.MainDevice.ClearState();

            m_view.Dispose();
            m_texture.Dispose();

            base.Dispose();
        }

        #region private

        readonly WorkSpace m_owner;
        readonly Texture2D m_texture;
        readonly ShaderResourceView m_view;

        #endregion
    }
}
