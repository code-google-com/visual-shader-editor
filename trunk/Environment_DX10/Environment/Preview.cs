using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using SlimDX.Windows;
using Device = SlimDX.Direct3D10.Device;
using System.Windows.Forms;
using Core.CodeGeneration;
using Core.Basic;

namespace Environment_DX10.Environment
{
    public class Preview : IPreview
    {
        static readonly SlimDX.DXGI.Format TEXTURE_FORMAT = SlimDX.DXGI.Format.R32G32B32A32_Float;
        static readonly int TEXTURE_SIZE = 256;

        internal Preview(Device d, Core.Basic.BaseBlock bb, WorkSpace owner)
        {
            m_bb = bb;
            m_device = d;
            m_owner = owner;

            var desc = new Texture2DDescription()
            {
                ArraySize = 1,
                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                Format = TEXTURE_FORMAT,
                Height = TEXTURE_SIZE,
                Width = TEXTURE_SIZE,
                MipLevels = (int)Math.Log(TEXTURE_SIZE, 2),
                OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default
            };

            m_backBuffer = new Texture2D(m_device, desc);
            m_renderView = new RenderTargetView(m_device, m_backBuffer);
            m_resourceView = new ShaderResourceView(m_device, m_backBuffer);

            var descD = new Texture2DDescription()
            {
                ArraySize = 1,
                BindFlags = BindFlags.DepthStencil,
                Format = SlimDX.DXGI.Format.D32_Float,
                Height = TEXTURE_SIZE,
                Width = TEXTURE_SIZE,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default
            };
            m_backBufferDepth = new Texture2D(m_device, descD);
            m_dsv = new DepthStencilView(m_device, m_backBufferDepth);
            
            m_viewport = new Viewport(0, 0, m_backBuffer.Description.Width, m_backBuffer.Description.Height, 0.0f, 1.0f);
        }
        internal void Begin(CompiledEffect ce)
        {
            ce.SetDebugOutput(m_bb.Outputs.Count > 0 ? m_bb.Outputs[0].Variable : null);

            m_device.OutputMerger.SetTargets(m_dsv, m_renderView);
            m_device.Rasterizer.SetViewports(m_viewport);

            m_device.ClearRenderTargetView(m_renderView, new Color4(0, 0, 0));
            m_device.ClearDepthStencilView(m_dsv, DepthStencilClearFlags.Depth, 1, 0);
        }
        internal void End()
        {
            m_device.OutputMerger.SetTargets((RenderTargetView)null);
            m_device.GenerateMips(m_resourceView);
        }

        public void Dispose()
        {
            m_owner.RemovePreview(this);

            //remove internal pointers
            m_device.ClearState();

            m_renderView.Dispose();
            m_backBuffer.Dispose();
            m_resourceView.Dispose();
        }

        internal ShaderResourceView BackBufferView
        {
            get { return m_resourceView; }
        }

        #region private

        readonly Device m_device;
        readonly Texture2D m_backBuffer;
        readonly Texture2D m_backBufferDepth;
        readonly RenderTargetView m_renderView;
        readonly ShaderResourceView m_resourceView;
        readonly DepthStencilView m_dsv;
        readonly Viewport m_viewport;

        readonly BaseBlock m_bb;
        readonly WorkSpace m_owner;

        #endregion

    }
}
