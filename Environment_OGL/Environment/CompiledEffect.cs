using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using Core.CodeGeneration;
using OpenTK.Graphics.OpenGL;
using Core.Var;
using Core.Main;
using Core.Basic;

namespace Environment_OGL.Environment
{
    public class CompiledEffect : ICompiledShader
    {
        public CompiledEffect(WorkSpace owner, string vs, string gs, string ps, ShaderCode sc)
        {
            m_owner = owner;
            m_sc = sc;

            int id = 0;
            m_textureUnits = new Dictionary<string, int>();
            foreach (var v in sc.Parameters)
                if (v.Format == Format.TEXTURE)
                    m_textureUnits.Add(v.Name, id++);

            m_vs = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(m_vs, vs);
            GL.CompileShader(m_vs);

            // m_gs = GL.CreateShader(ShaderType.GeometryShader);
            //GL.ShaderSource(m_gs, gs);
            //GL.CompileShader(m_gs);

            m_ps = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(m_ps, ps);
            GL.CompileShader(m_ps);

            m_program = GL.CreateProgram();
            GL.AttachShader(m_program, m_vs);
            //GL.AttachShader(m_program, m_gs);
            GL.AttachShader(m_program, m_ps);
            GL.LinkProgram(m_program);

            string info;
            GL.GetProgramInfoLog(m_program, out info);

            if (info != null && info != "")
                StaticBase.Singleton.Log.Write(Log.InfoType.ShaderError, "Shader Code error:\n{0}\n", info);
        }

        public void SetTextureParameter(string name, string fileName)
        {
            //GL.UseProgram(m_program);
            Texture t = (Texture)m_owner.TextureManager.LoadTexture(fileName);

            int tu = m_textureUnits[name];

            int loc = GL.GetUniformLocation(m_program, name);
            GL.Uniform1(loc, tu);

            GL.ActiveTexture(TextureUnit.Texture0 + tu);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, t.TextureResource);

            GL.ActiveTexture(TextureUnit.Texture0);
        }

        public void SetDebugOutput(Variable variable)
        {
            float id = -1;

            if (variable != null)
                id = m_sc.GetDebugId(variable);

            int loc = GL.GetUniformLocation(m_program, ShaderCode.DEBUG_VARIABLE_SELECTION_PARAMETER_NAME);
            GL.Uniform1(loc, id);
        }

        public void SetParameter(string name, Vector1f vec)
        {
            //GL.UseProgram(m_program);
            int loc = GL.GetUniformLocation(m_program, name);
            GL.Uniform1(loc, vec.X);
        }

        public void SetParameter(string name, Vector2f vec)
        {
            //GL.UseProgram(m_program);
            int loc = GL.GetUniformLocation(m_program, name);
            GL.Uniform2(loc, vec.X, vec.Y);
        }

        public void SetParameter(string name, Vector3f vec)
        {
            //GL.UseProgram(m_program);
            int loc = GL.GetUniformLocation(m_program, name);
            GL.Uniform3(loc, vec.X, vec.Y, vec.Z);
        }

        public void SetParameter(string name, Vector4f vec)
        {
            //GL.UseProgram(m_program);
            int loc = GL.GetUniformLocation(m_program, name);
            GL.Uniform4(loc, vec.X, vec.Y, vec.Z, vec.W);
        }

        public void SetParameter(string name, Matrix44f mtx)
        {
            //GL.UseProgram(m_program);
            int loc = GL.GetUniformLocation(m_program, name);

            //gl use row major, i use column + transpose
            OpenTK.Matrix4 m = new OpenTK.Matrix4(
                mtx.Column0.X, mtx.Column0.Y, mtx.Column0.Z, mtx.Column0.W,
                    mtx.Column1.X, mtx.Column1.Y, mtx.Column1.Z, mtx.Column1.W,
                    mtx.Column2.X, mtx.Column2.Y, mtx.Column2.Z, mtx.Column2.W,
                    mtx.Column3.X, mtx.Column3.Y, mtx.Column3.Z, mtx.Column3.W
                );

            GL.UniformMatrix4(loc, true, ref m);
        }

        public IWorkSpace Owner
        {
            get { return m_owner; }
        }
        public void Dispose()
        {
            GL.DeleteShader(m_vs);
            GL.DeleteShader(m_gs);
            GL.DeleteShader(m_ps);
            GL.DeleteProgram(m_program);
        }

        #region private

        readonly ShaderCode m_sc;
        readonly WorkSpace m_owner;

        int m_vs;
        int m_gs;
        int m_ps;
        public int m_program;

        readonly Dictionary<string, int> m_textureUnits;

        #endregion
    }
}
