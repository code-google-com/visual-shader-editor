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
using SlimDX.Direct3D10;
using Core.CodeGeneration;
using Core.Main;
using Core.Var;
using Core.Basic;

namespace Environment_DX10.Environment
{
    public class CompiledEffect : ICompiledShader
    {
        public CompiledEffect(WorkSpace owner, string fxCode, ShaderCode sc)
        {
            m_owner = owner;

            string errors;
            Effect = Effect.FromString(m_owner.MainDevice, fxCode, "fx_4_0", ShaderFlags.None, EffectFlags.None, null, null, out errors);

            if (errors != null && errors != "")
                StaticBase.Singleton.Log.Write(Log.InfoType.ShaderError, "Shader Code error:\n{0}\n", errors);

            m_sc = sc;
        }

        public void SetTextureParameter(string name, string fileName)
        {
            Texture t = (Texture)m_owner.TextureManager.LoadTexture(fileName);
            var v = Effect.GetVariableByName(name);
            if (v != null)
            {
                var r = v.AsResource();
                if (r != null)
                    r.SetResource(t.ResurceView);
            }
        }
        public void SetDebugOutput(Variable variable)
        {
            float id = -1;

            if(variable != null)
                id = m_sc.GetDebugId(variable);

            var v = Effect.GetVariableByName(ShaderCode.DEBUG_VARIABLE_SELECTION_PARAMETER_NAME);
            //if (v != null)
            //{
                var r = v.AsScalar();
            //    if (r != null)
                    r.Set(id);
            //}
        }
        public void SetParameter(string name, Vector1f vec)
        {
            var v = Effect.GetVariableByName(name);
            if (v != null)
            {
                var r = v.AsScalar();
                if (r != null)
                    r.Set(vec.X);
            }
        }
        public void SetParameter(string name, Vector2f vec)
        {
            var v = Effect.GetVariableByName(name);
            if (v != null)
            {
                var r = v.AsVector();
                if (r != null)
                    r.Set(new SlimDX.Vector2(vec.X, vec.Y));
            }
        }
        public void SetParameter(string name, Vector3f vec)
        {
            var v = Effect.GetVariableByName(name);
            if (v != null)
            {
                var r = v.AsVector();
                if (r != null)
                    r.Set(new SlimDX.Vector3(vec.X, vec.Y, vec.Z));
            }
        }
        public void SetParameter(string name, Vector4f vec)
        {
            var v = Effect.GetVariableByName(name);
            if (v != null)
            {
                var r = v.AsVector();
                if (r != null)
                    r.Set(new SlimDX.Vector4(vec.X, vec.Y, vec.Z, vec.W));
            }
        }
        public void SetParameter(string name, Matrix44f mtx)
        {
            var v = Effect.GetVariableByName(name);
            if (v != null)
            {
                var r = v.AsMatrix();
                if (r != null)
                {
                    SlimDX.Matrix m = new SlimDX.Matrix();
                    m.set_Columns(0, new SlimDX.Vector4(mtx.Column0.X, mtx.Column0.Y, mtx.Column0.Z, mtx.Column0.W));
                    m.set_Columns(1, new SlimDX.Vector4(mtx.Column1.X, mtx.Column1.Y, mtx.Column1.Z, mtx.Column1.W));
                    m.set_Columns(2, new SlimDX.Vector4(mtx.Column2.X, mtx.Column2.Y, mtx.Column2.Z, mtx.Column2.W));
                    m.set_Columns(3, new SlimDX.Vector4(mtx.Column3.X, mtx.Column3.Y, mtx.Column3.Z, mtx.Column3.W));
                    r.SetMatrix(m);
                }
            }
        }

        public IWorkSpace Owner
        {
            get { return m_owner; }
        }
        public void Dispose()
        {
            Effect.Dispose();
        }

        public readonly Effect Effect;

        #region private

        readonly ShaderCode m_sc;
        readonly WorkSpace m_owner;

        #endregion
    }
}
