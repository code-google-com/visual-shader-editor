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
