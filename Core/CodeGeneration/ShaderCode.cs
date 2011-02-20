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
using Core.Basic;
using Core.CodeGeneration.Code;
using System.Diagnostics;
using Core.Var;

namespace Core.CodeGeneration
{
    public class ShaderCode
    {
        public static readonly string DEBUG_VARIABLE_SELECTION_PARAMETER_NAME = "DebugOutputNumber";
        public static readonly string DEBUG_OUTPUT_NAME = "DebugOutput";

        public List<Variable> Parameters
        {
            get { return m_parameters; }
        }

        public int GetDebugId(Variable v)
        {
            int id;
            if (m_variableIdMap.TryGetValue(v, out id))
                return id;

            return -1;
        }
        public void AddDebugId(Variable v, int id)
        {
            Debug.Assert(!m_variableIdMap.ContainsKey(v));
            m_variableIdMap.Add(v, id);
        }

        public Variable OutputPosition;

        public readonly List<Variable> VSInput = new List<Variable>();
        public readonly List<Variable> VSToPS = new List<Variable>();
        public readonly List<Variable> PSOutput = new List<Variable>();

        public List<Instruction> VSInstructions = new List<Instruction>();
        public List<Instruction> PSInstructions = new List<Instruction>();

        #region private

        //TODO: rename -> variable debug id
        readonly Dictionary<Variable, int> m_variableIdMap = new Dictionary<Variable, int>();
        readonly List<Variable> m_parameters = new List<Variable>();

        #endregion
    }
}
