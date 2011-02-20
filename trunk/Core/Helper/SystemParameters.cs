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
