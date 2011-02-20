using System;
using System.Collections.Generic;
using System.Text;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public abstract class Instruction
    {
        public abstract IList<Variable> DefinedVariables { get; }
        public abstract IList<Variable> WritenVariables { get; }
        public abstract IList<Variable> ReadVariables { get; }
        public abstract bool Linear { get; }
        public abstract bool Const { get; }
    }
}
