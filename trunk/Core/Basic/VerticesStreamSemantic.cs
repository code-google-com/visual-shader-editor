using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Basic
{
    public enum VerticesStreamSemantic
    {
        NONE,
        BINORMAL,
        BLENDINDICES,
        BLENDWEIGHT,
        COLOR,
        NORMAL,
        POSITION,
        PSIZE,
        TANGENT,
        TEXCOORD,

        DEPTH,  //pixel shader output only
        DEBUG,  //pixel shader output only
        //COLOR,  //pixel shader output
    }

    public static class SemanticInfo
    {
        public static readonly int MAX_SEMANTIC_INDEX_VALUE = 8;
    }
}
