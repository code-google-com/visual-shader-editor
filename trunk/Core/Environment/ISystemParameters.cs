using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;

namespace Core.Environment
{
    public enum Vector1Parameter
    {
        Time,
    }

    public enum Vector3Parameter
    {
        CameraPosition,
        CameraForward,
        CameraUp,
        CameraRight,

        LightColor,
        LightPosition,
        LightForward,
        LightAttenuation,

        AmbientColor
    }

    public enum Matrix44Parameter
    {
        Model,
        View,
        Projection,
    }

    public interface ISystemParameters
    {
        Vector1f GetParameter(Vector1Parameter p);
        Vector3f GetParameter(Vector3Parameter p);
        Matrix44f GetParameter(Matrix44Parameter p);
    }
}
