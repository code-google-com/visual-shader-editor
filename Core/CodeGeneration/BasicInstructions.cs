using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.CodeGeneration.Code;

namespace Core.CodeGeneration
{
    public abstract class BasicInstructions
    {
        //public abstract string Translate(ShaderParameter d);
        public abstract string Translate(Instruction i);
    }
}
