using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using SlimDX.Direct3D10;
using Core.Main;
using Core.CodeGeneration;
using System.IO;
using Environment_DX10.Code;
using System.Windows.Forms;
using SlimDX.DXGI;
using SlimDX;
using Device10 = SlimDX.Direct3D10.Device;
using Buffer10 = SlimDX.Direct3D10.Buffer;
using Font10 = SlimDX.Direct3D10.Font;
using Core.Basic;
using System.Drawing;
using Core.Helper;

namespace Environment_DX10.Environment
{
    public class WorkSpace : IWorkSpace
    {
        public WorkSpace(BlockManager bm, Control c)
        {
            m_blockManager = bm;
            //m_blockManager.OnAfterChange += new Action<BlockManager>(m_blockManager_OnAfterChange);

            m_control = c;
            m_control.Resize += new EventHandler(m_control_Resize);

            CreateDirect();
            m_textureManager = new TextureManager(m_device);
            string errors;
            m_basicEffect = Effect.FromMemory(m_device, Properties.Resources.BasicEffect, "fx_4_0", ShaderFlags.Debug, EffectFlags.None, null, null, out errors);

            m_model = new Model(this);
            m_font = new Font(this);

            m_params = new SystemParameters(this);
        }

        void m_control_Resize(object sender, EventArgs e)
        {
            //delete references
            m_device.ClearState();
            m_renderView.Dispose();
            m_backBuffer.Dispose();


            Result r = m_swapChain.ResizeBuffers(2, 0, 0, SlimDX.DXGI.Format.R8G8B8A8_UNorm, SwapChainFlags.None);
            if (r.IsFailure)
                throw new Exception();

            /* m_swapChain.Dispose();   
             var desc = new SwapChainDescription()
             {
                 BufferCount = 2,
                 ModeDescription = new ModeDescription(m_control.ClientSize.Width, m_control.ClientSize.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                 IsWindowed = true,
                 OutputHandle = m_control.Handle,
                 SampleDescription = new SampleDescription(1, 0),
                 SwapEffect = SwapEffect.Discard,
                 Usage = Usage.RenderTargetOutput
             };
             m_swapChain = new SwapChain(new Factory(), m_device, desc);

             //Stops Alt+enter from causing fullscreen skrewiness.
             Factory factory = m_swapChain.GetParent<Factory>();
             factory.SetWindowAssociation(m_control.Handle, WindowAssociationFlags.IgnoreAll);*/

            m_backBuffer = Texture2D.FromSwapChain<Texture2D>(m_swapChain, 0);
            m_renderView = new RenderTargetView(m_device, m_backBuffer);
        }

        public string PreviewFileName { set { m_previewFileName = value; } }
        public string ReleaseFileName { set { m_releaseFileName = value; } }

        void GenerateReleaseFile()
        {
            ShaderCode sc;
            CompiledEffect ce;

            try
            {
                sc = InstructionGenerator.GenerateRelease(m_blockManager);
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
                sc = InstructionGenerator.GenerateDebug(m_blockManager);
                ce = (CompiledEffect)Compile(sc, m_previewFileName);
            }
            catch (Exception)
            {
                StaticBase.Singleton.Log.Write(Log.InfoType.ShaderError, "Shader Generation Fail\n");
                return false;
            }

            //set parameters
            foreach (var b in m_blockManager.Blocks)
                b.SetShaderParameters(ce);

            if (m_previewEffect != null)
            {
                m_previewEffect.Dispose();
            }

            m_previewEffect = ce;

            //return ce;

            //disable blend
            m_device.ClearState();

            //----------
            EffectTechnique technique = m_previewEffect.Effect.GetTechniqueByIndex(0);
            m_device.InputAssembler.SetPrimitiveTopology(m_model.PrimitiveTopology);
            m_device.InputAssembler.SetVertexBuffers(0, m_model.VertexBuffers[0]);
            m_device.InputAssembler.SetIndexBuffer(m_model.m_indices, SlimDX.DXGI.Format.R32_UInt, 0);

            RasterizerState rs = RasterizerState.FromDescription(m_device, new RasterizerStateDescription()
            {
                CullMode = CullMode.None,
                FillMode = FillMode.Solid,
            });

            m_device.Rasterizer.State = rs;

            //------- pass

            try
            {

                EffectPass pass = technique.GetPassByIndex(0);
                InputLayout layout = new InputLayout(m_device, m_model.InputElements, pass.Description.Signature);
                m_device.InputAssembler.SetInputLayout(layout);

                //---------

                foreach (var p in m_previewList)
                {
                    // p.Shader = m_previewEffect;

                    //-----------------
                    p.Begin(m_previewEffect);
                    //for (int i = 0; i < technique.Description.PassCount; ++i)
                    {
                        pass.Apply();
                        //m_device.Draw(m_model.VertexCount, 0);
                        m_device.DrawIndexed(m_model.IndexCount, 0, 0);
                    }
                    p.End();


                    //------------
                    //p.Draw();
                }

                layout.Dispose();
            }
            catch (Exception e)
            {
                StaticBase.Singleton.Log.Write(Log.InfoType.ShaderError, "DX10 InputLayout creation fail, missing input signatures for shader\n");
            }

            m_device.ClearState();

            //string s = SlimDX.ObjectTable.ReportLeaks();

            return true;
        }

        Model m_model;

        public IPreview CreatePreview(Core.Basic.BaseBlock bb)
        {
            Preview p = new Preview(m_device, bb, this);
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
            get { return m_blockManager; }
        }

        public Control Control
        {
            get { return m_control; }
        }

        internal void RemovePreview(Preview p)
        {
            m_previewList.Remove(p);
        }

        #region private

        readonly List<Preview> m_previewList = new List<Preview>();
        readonly TextureManager m_textureManager;
        readonly BlockManager m_blockManager;
        CompiledEffect m_previewEffect;

        void CreateDirect()
        {
            m_device = new SlimDX.Direct3D10.Device(DeviceCreationFlags.None);//should be: DeviceCreationFlags.Debug but it cause crash sometimes

            var desc = new SwapChainDescription()
            {
                BufferCount = 2,
                ModeDescription = new ModeDescription(m_control.ClientSize.Width, m_control.ClientSize.Height, new Rational(60, 1), SlimDX.DXGI.Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = m_control.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            m_swapChain = new SwapChain(new Factory(), m_device, desc);

            //Stops Alt+enter from causing fullscreen skrewiness.
            Factory factory = m_swapChain.GetParent<Factory>();
            factory.SetWindowAssociation(m_control.Handle, WindowAssociationFlags.IgnoreAll);

            m_backBuffer = Texture2D.FromSwapChain<Texture2D>(m_swapChain, 0);
            m_renderView = new RenderTargetView(m_device, m_backBuffer);
        }

        readonly Control m_control;
        Device10 m_device;
        SwapChain m_swapChain;
        Texture2D m_backBuffer;
        RenderTargetView m_renderView;

        #endregion

        public Device10 MainDevice
        {
            get { return m_device; }
        }

        public ICompiledShader Compile(ShaderCode sc, string outFileName)
        {
            HLSLBasicInstrutions b = new HLSLBasicInstrutions();

            StaticBase.Singleton.Log.Write(Log.InfoType.ShaderInfo, "Begin Translating code to HLSL\n");
            string code = b.GenerateCode(sc);
            StaticBase.Singleton.Log.Write(Log.InfoType.ShaderInfo, "End Translating code to HLSL\n");

            if(outFileName != null && outFileName != "")
                File.WriteAllText(outFileName + ".fx", code);

            CompiledEffect ce = new CompiledEffect(this, code, sc);

            return ce;
        }


        //  #region IWorkSpace Members

        public void DrawLines(IList<Line2f> lines, float size, Vector4f color)
        {
            if (lines.Count == 0)
                return;

            int linesCount = lines.Count;
            int stride = 2 * 4;
            int bufferSize = linesCount * 2 * stride;

            var stream = new DataStream(bufferSize, true, true);
            for (int i = 0; i < linesCount; i++)
            {
                stream.Write(RescalePosition(lines[i].Point0));
                stream.Write(RescalePosition(lines[i].Point1));
            }
            stream.Position = 0;

            Buffer10 vertices = new Buffer10(m_device, stream, new BufferDescription()
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = bufferSize,
                Usage = ResourceUsage.Default
            });
            stream.Dispose();

            InputElement[] InputElements = new[] {
                new InputElement("POSITION", 0, SlimDX.DXGI.Format.R32G32_Float, 0, 0)
            };

            //--------------

            m_basicEffect.GetVariableByName("Color").AsVector().Set(new Vector4(color.X, color.Y, color.Z, color.W));

            EffectTechnique technique = m_basicEffect.GetTechniqueByName("ColorTechnique");

            m_device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.LineList);
            m_device.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertices, stride, 0));

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                EffectPass pass = technique.GetPassByIndex(i);

                ShaderSignature ss = pass.Description.Signature;
                InputLayout layout = new InputLayout(m_device, InputElements, ss);
                m_device.InputAssembler.SetInputLayout(layout);

                pass.Apply();
                m_device.Draw(linesCount * 2, 0);

                layout.Dispose();
            }

            vertices.Dispose();
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
            int stride = 32;
            int bufferSize = 2 * 3 * stride;
            var stream = new DataStream(bufferSize, true, true);

            stream.Write(RescalePosition(new Vector2f(rect.Rectangle.Min.X, rect.Rectangle.Min.Y)));
            stream.Write(new Vector2f(0, 0));
            stream.Write(rect.Color);
            stream.Write(RescalePosition(new Vector2f(rect.Rectangle.Max.X, rect.Rectangle.Min.Y)));
            stream.Write(new Vector2f(1, 0));
            stream.Write(rect.Color);
            stream.Write(RescalePosition(new Vector2f(rect.Rectangle.Min.X, rect.Rectangle.Max.Y)));
            stream.Write(new Vector2f(0,1));
            stream.Write(rect.Color);

            stream.Write(RescalePosition(new Vector2f(rect.Rectangle.Min.X, rect.Rectangle.Max.Y)));
            stream.Write(new Vector2f(0,1));
            stream.Write(rect.Color);
            stream.Write(RescalePosition(new Vector2f(rect.Rectangle.Max.X, rect.Rectangle.Min.Y)));
            stream.Write(new Vector2f(1,0));
            stream.Write(rect.Color);
            stream.Write(RescalePosition(new Vector2f(rect.Rectangle.Max.X, rect.Rectangle.Max.Y)));
            stream.Write(new Vector2f(1,1));
            stream.Write(rect.Color);

            stream.Position = 0;

            Buffer10 vertices = new SlimDX.Direct3D10.Buffer(m_device, stream, new BufferDescription()
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

           // m_basicEffect.GetVariableByName("Color").AsVector().Set(new Vector4(1, 1, 1, 1));

            switch (rect.Texture)
            {
                case ColorRectangle.TextureType.None: m_basicEffect.GetVariableByName("UseTexture").AsScalar().Set(0);
                    break;
                case ColorRectangle.TextureType.Border: m_basicEffect.GetVariableByName("UseTexture").AsScalar().Set(1);
                    m_basicEffect.GetVariableByName("Texture").AsResource().SetResource(((TextureManager)this.TextureManager).BorderTexture.ResurceView);
                    break;
                case ColorRectangle.TextureType.Button: m_basicEffect.GetVariableByName("UseTexture").AsScalar().Set(1);
                    m_basicEffect.GetVariableByName("Texture").AsResource().SetResource(((TextureManager)this.TextureManager).ButtonTexture.ResurceView);
                    break;
                case ColorRectangle.TextureType.Preview: m_basicEffect.GetVariableByName("UseTexture").AsScalar().Set(1);
                    m_basicEffect.GetVariableByName("Texture").AsResource().SetResource(((Preview)rect.Preview).BackBufferView);
                    break;
                
                default :
                    throw new NotImplementedException();
            }

            EffectTechnique technique;
            if(rect.Texture == ColorRectangle.TextureType.Preview)
                technique = m_basicEffect.GetTechniqueByName("QuadTechniqueNoBlend");
            else
                technique = m_basicEffect.GetTechniqueByName("QuadTechnique");

            m_device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            m_device.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertices, stride, 0));

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                EffectPass pass = technique.GetPassByIndex(i);

                ShaderSignature ss = pass.Description.Signature;
                InputLayout layout = new InputLayout(m_device, InputElements, ss);
                m_device.InputAssembler.SetInputLayout(layout);

                pass.Apply();
                m_device.Draw(6, 0);

                layout.Dispose();
            }

            vertices.Dispose();
        }

        public void BeginRender(Vector4f clearColor, float zoom)
        {
            m_zoom = zoom;

            m_device.ClearRenderTargetView(m_renderView, new Color4(clearColor.W, clearColor.X, clearColor.Y, clearColor.Z));

            m_device.OutputMerger.SetTargets(m_renderView);
            m_device.Rasterizer.SetViewports(new Viewport(0, 0, m_swapChain.Description.ModeDescription.Width, m_swapChain.Description.ModeDescription.Height, 0.0f, 1.0f));

        }
        public void EndRender()
        {
            m_swapChain.Present(0, PresentFlags.None);
            m_device.OutputMerger.SetTargets((RenderTargetView)null);
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
        internal Effect BasicEffect
        {
            get { return m_basicEffect; }
        }

        public ISystemParameters SystemParameters
        {
            get { return m_params; }
        }

        // #endregion

        #region private

        readonly Effect m_basicEffect;
        float m_zoom = 1;
        readonly Font m_font;
        readonly SystemParameters m_params;

        string m_previewFileName;
        string m_releaseFileName;

        #endregion
    }
}
