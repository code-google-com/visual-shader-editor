using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment.Texture;
using Core.Basic;
using Core.CodeGeneration;
using Core.Var;

namespace Core.Environment
{
    public interface ICompiledShader : IDisposable
    {
        void SetDebugOutput(Variable v);
        
        void SetParameter(string name, Vector1f v);
        void SetParameter(string name, Vector2f v);
        void SetParameter(string name, Vector3f v);
        void SetParameter(string name, Vector4f v);
        void SetParameter(string name, Matrix44f m);
        void SetTextureParameter(string name, string fileName);

        IWorkSpace Owner { get; }
    }
}
