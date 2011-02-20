using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;

namespace Core.Helper
{
    public static class VectorHelper
    {
        public static object FromString(string v)
        {
            string[] s = v.Split(' ');

            switch (s.Length)
            {
                case 1: return Vector1f.Parse(v);
                case 2: return Vector2f.Parse(v);
                case 3: return Vector3f.Parse(v);
                case 4: return Vector4f.Parse(v);
                default: throw new Exception("wrong format");
            }
        }

        public static object FromString(string value, Format f)
        {
            switch (f)
            {
                case Format.FLOAT: return Vector1f.Parse(value); break;
                case Format.FLOAT2: return Vector2f.Parse(value); break;
                case Format.FLOAT3: return Vector3f.Parse(value); break;
                case Format.FLOAT4: return Vector4f.Parse(value); break;
                default: throw new Exception("wrong format");
            }
        }
    }
}
