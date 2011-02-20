using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using SlimDX;
using Core.Helper;

namespace Environment_DX10.Environment
{
    public class Model : IModel
    {
        public Model(WorkSpace owner)
        {
            m_owner = owner;

            SphereModel m = new SphereModel();

            InputElements = new[] {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, m.PosOffset, 0),
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, m.TCoordOffset, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, m.ColorOffset, 0),
                new InputElement("NORMAL", 0, Format.R32G32B32_Float, m.NormalOffset, 0),
                new InputElement("BINORMAL", 0, Format.R32G32B32_Float, m.BinormalOffset, 0),
                new InputElement("TANGENT", 0, Format.R32G32B32_Float, m.TangentOffset, 0),
            };

            PrimitiveTopology = PrimitiveTopology.TriangleList;

            int stride = m.Stride;

            var stream = new DataStream(m.VerticesCount * m.Stride, true, true);
            for (int i = 0; i < m.VerticesCount; i++)
            {
                stream.Write(m.POSITON0[i]);
                stream.Write(m.TEXCOORD0[i]);
                stream.Write(m.COLOR0[i]);
                stream.Write(m.NORMAL0[i]);
                stream.Write(m.BINORMAL0[i]);
                stream.Write(m.TANGENT0[i]);
            }
            stream.Position = 0;
            m_vertices = new SlimDX.Direct3D10.Buffer(m_owner.MainDevice, stream, new BufferDescription()
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = m.VerticesCount * m.Stride,
                Usage = ResourceUsage.Default
            });
            stream.Dispose();

            VertexBuffers = new[] {
                new VertexBufferBinding(m_vertices, stride, 0)
            };

            var indexStream = new DataStream(m.IndicesCount * 4, true, true);
            indexStream.WriteRange(m.INDICES);
            indexStream.Position = 0;
            m_indices = new SlimDX.Direct3D10.Buffer(m_owner.MainDevice, indexStream, new BufferDescription()
            {
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = m.IndicesCount * 4,
                Usage = ResourceUsage.Default
            });
            indexStream.Dispose();

            VertexCount = m.VerticesCount;

            IndexCount = m.IndicesCount;
        }

        public readonly int VertexCount;
        public readonly int IndexCount;
        public readonly InputElement[] InputElements;
        public readonly PrimitiveTopology PrimitiveTopology;
        public readonly VertexBufferBinding[] VertexBuffers;

        readonly WorkSpace m_owner;
        SlimDX.Direct3D10.Buffer m_vertices;
        public SlimDX.Direct3D10.Buffer m_indices;
    }
}
