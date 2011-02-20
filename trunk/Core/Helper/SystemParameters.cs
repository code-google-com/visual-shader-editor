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
using Core.Basic;

namespace Core.Helper
{
    public class SystemParameters : ISystemParameters
    {
        public SystemParameters(IWorkSpace owner)
        {
            m_owner = owner;
        }

        public Core.Basic.Vector1f GetParameter(Vector1Parameter p)
        {
            switch (p)
            {
                case Vector1Parameter.Time: return new Vector1f((float)(DateTime.Now - m_start).TotalSeconds);
            }

            throw new NotImplementedException();
        }

        public Core.Basic.Vector3f GetParameter(Vector3Parameter p)
        {
            switch (p)
            {
                case Vector3Parameter.AmbientColor: return new Vector3f(0, 0, 0);
                case Vector3Parameter.CameraForward: return Vector3f.Normalize(CameraTarget - CameraPosition);
                case Vector3Parameter.CameraPosition: return CameraPosition;
                case Vector3Parameter.CameraRight: return Vector3f.Normalize(Vector3f.Cross(CameraUp, Vector3f.Normalize(CameraTarget - CameraPosition)));
                case Vector3Parameter.CameraUp: return CameraUp;
                case Vector3Parameter.LightAttenuation: return new Vector3f(1, 1, 0);
                case Vector3Parameter.LightColor: return new Vector3f(1, 0, 0);
                case Vector3Parameter.LightForward: return new Vector3f(0, 0, 0);
                case Vector3Parameter.LightPosition: return new Vector3f((float)Math.Sin(Time * LightSpeed) * LightDistance, (float)Math.Cos(Time * LightSpeed) * LightDistance, 0);
            }

            throw new NotImplementedException();
        }

        float Time
        {
            get{return (float)(DateTime.Now - m_start).TotalSeconds;}
        }

        Vector3f CameraPosition = new Vector3f(1, 1, 1);
        Vector3f CameraTarget = new Vector3f(0, 0, 0);
        Vector3f CameraUp = Vector3f.Normalize(new Vector3f(-1, -1, 1));
        float LightSpeed = 0.7f;
        float LightDistance = 4;

        public Core.Basic.Matrix44f GetParameter(Matrix44Parameter p)
        {
            Matrix44f v = Matrix44f.LookAt(CameraPosition, CameraTarget, CameraUp);
            //Matrix44f v = Matrix44f.LookAt(new Vector3f(0, -2, 4), new Vector3f(0, 0, 0), new Vector3f(0, 0, 1));
            Matrix44f px = Matrix44f.Perspective(1.4f, 1, 0.001f, 10.0f);

           /*  SlimDX.Matrix vv = SlimDX.Matrix.LookAtRH(new SlimDX.Vector3(1, 2, 3), new SlimDX.Vector3(0.5f, 0, 0.5f), new SlimDX.Vector3(0.5f, 0.5f, 0));
             SlimDX.Matrix pp = SlimDX.Matrix.PerspectiveFovRH(1.4f, 1, 0.001f, 10.0f);

             //v.Column2.W = -3.4641f;
             //v.Column3 = new Vector4f(0, 0, 0, 1);

             Matrix44f vp = v * px;


             SlimDX.Matrix vvpp = vv * pp;*/

            switch (p)
            {
                case Matrix44Parameter.Model: return Matrix44f.MakeIdentity();
                case Matrix44Parameter.View: return v;
                case Matrix44Parameter.Projection: return px;
            }

            throw new NotImplementedException();
        }

        #region private

        readonly IWorkSpace m_owner;
        readonly DateTime m_start = DateTime.Now;

        #endregion
    }
}
