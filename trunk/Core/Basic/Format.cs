using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Basic
{
    public enum Format
    {
        NONE,

        FLOAT,
        FLOAT2,
        FLOAT3,
        FLOAT4,

        FLOAT2X2,
        FLOAT3X3,
        FLOAT4X4,

        TEXTURE,
        SAMPLER
    }

    public static class FormatHelper
    {
        public static bool IsFirstBigger(Format a, Format b)
        {
            return (int)a > (int)b;
        }

        public static int Size(Format f)
        {
            switch (f)
            {
                case Format.FLOAT: return 1;
                case Format.FLOAT2: return 2;
                case Format.FLOAT3: return 3;
                case Format.FLOAT4: return 4;
            }

            throw new NotImplementedException();
        }

        public static Format FromSize(int s)
        {
            switch (s)
            {
                case 1: return Format.FLOAT;
                case 2: return Format.FLOAT2;
                case 3: return Format.FLOAT3;
                case 4: return Format.FLOAT4;
            }

            throw new NotImplementedException();
        }

        public static object[] VectorFormatsOnly()
        {
            return new object[] { Format.FLOAT, Format.FLOAT2, Format.FLOAT3, Format.FLOAT4 };
        }
    }
}
