using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;

namespace Core.Var
{
    public class Variable
    {
        public Variable(string name, Format f)
        {
            Name = name;
            this.Format = f;
            Semantic = VerticesStreamSemantic.NONE;
            SemanticIndex = 0;
        }
        public Variable(string name, Format f, VerticesStreamSemantic s, int semanticIndex)
        {
            Name = name;
            this.Format = f;
            Semantic = s;
            SemanticIndex = semanticIndex;
        }

        public readonly string Name;
        public readonly Format Format;
        public readonly VerticesStreamSemantic Semantic;
        public readonly int SemanticIndex;

        public override string ToString()
        {
            return string.Format("[{0} {1}:{2}{3}]", Format, Name, Semantic, SemanticIndex);
        }
    }
}
