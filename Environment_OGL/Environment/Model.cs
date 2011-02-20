using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using OpenTK.Graphics.OpenGL;
using Core.Helper;

namespace Environment_OGL.Environment
{
    public class Model : IModel
    {
        public Model(WorkSpace owner)
        {
            m_owner = owner;

            GL.GenBuffers( 1, out m_vbo );
            GL.BindBuffer( BufferTarget.ArrayBuffer, m_vbo );

            GL.GenBuffers(1, out m_ibo);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ibo);

            /*float[] data = new float[]{

                0.9f, 0.9f, 0.9f, 1.0f,
                0.9f, -0.9f, 0.9f, 1.0f,
                -0.9f, -0.9f, 0.9f, 1.0f,

                -0.9f, -0.9f, 0.9f, 1.0f,
                -0.9f, 0.9f, 0.9f, 1.0f,
                0.9f, 0.9f, 0.9f, 1.0f,

            };

            GL.BufferData( BufferTarget.ArrayBuffer, (IntPtr) ( 6 * 16 ), data, BufferUsageHint.StaticDraw );*/

            BeginMode = BeginMode.Triangles;
            //Stride = 16;
            //VertexCount = 6;

            SphereModel m = new SphereModel();

            float[] data = new float[m.VerticesCount * (18 * 4)];

            int id=0;
            for (int v = 0; v < m.VerticesCount; v++)
            {
                data[id++] = m.POSITON0[v].X; data[id++] = m.POSITON0[v].Y; data[id++] = m.POSITON0[v].Z;
                data[id++] = m.TEXCOORD0[v].X; data[id++] = m.TEXCOORD0[v].Y;
                data[id++] = m.COLOR0[v].X; data[id++] = m.COLOR0[v].Y; data[id++] = m.COLOR0[v].Z; data[id++] = m.COLOR0[v].Z;
                data[id++] = m.NORMAL0[v].X; data[id++] = m.NORMAL0[v].Y; data[id++] = m.NORMAL0[v].Z;
                data[id++] = m.BINORMAL0[v].X; data[id++] = m.BINORMAL0[v].Y; data[id++] = m.BINORMAL0[v].Z;
                data[id++] = m.TANGENT0[v].X; data[id++] = m.TANGENT0[v].Y; data[id++] = m.TANGENT0[v].Z;
            }

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length), data, BufferUsageHint.StaticDraw);

            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(m.INDICES.Length * 4), m.INDICES, BufferUsageHint.StaticDraw);

            IndicesCount = m.INDICES.Length;

            Stride = 18 * 4;
            VertexCount = m.VerticesCount;
        }

        public readonly int PosOffset = 0;
        public readonly int TCoordOffset = 3*4;
        public readonly int ColorOffset = 5*4;
        public readonly int NormalOffset = 9*4;
        public readonly int BinormalOffset = 12*4;
        public readonly int TangentOffset = 15 * 4;

        public readonly int IndicesCount;


        public void Dispose()
        {
            GL.DeleteBuffers(1, ref m_vbo);
            m_vbo = 0;
            GL.DeleteBuffers(1, ref m_ibo);
            m_ibo = 0;
        }

        public readonly int VertexCount;
        public readonly int Stride;
        public readonly BeginMode BeginMode;

        uint m_vbo;
        uint m_ibo;

        public uint Vbo
        {
            get { return m_vbo; }
            set { m_vbo = value; }
        }
        public uint Ibo
        {
            get { return m_ibo; }
            set { m_ibo = value; }
        }

        readonly WorkSpace m_owner;
        //SlimDX.Direct3D10.Buffer m_vertices;
    }
}
