using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using System.Diagnostics;
using Core.CodeGeneration.Code;
using Core.Var;
using Core.Main;

namespace Core.CodeGeneration
{
    public class ShaderCodeGenerator
    {
        public void AddShaderInput(Variable v)
        {
            Debug.Assert(v.Semantic != VerticesStreamSemantic.DEPTH && v.Semantic != VerticesStreamSemantic.DEBUG && v.Semantic != VerticesStreamSemantic.NONE);

            m_shaderInput.Add(v);
        }
        public void AddShaderOutput(Variable v)
        {
            if (v.Semantic != VerticesStreamSemantic.COLOR)
            {
                Debug.Assert(v.Semantic == VerticesStreamSemantic.DEPTH || v.Semantic == VerticesStreamSemantic.DEBUG);
                Debug.Assert(v.SemanticIndex == 0);
            }

            m_shaderOutput.Add(v);
        }
        public Variable ShaderOutputPosition
        {
            set
            {
                Debug.Assert(m_sc.OutputPosition == null);
                Debug.Assert(value.Semantic == VerticesStreamSemantic.POSITION && value.SemanticIndex == 0);
                m_sc.OutputPosition = value;
                m_instructions.Insert(0, InstructionHelper.CreateVariableWithDefaultValue(value, 0));
            }
        }

        public void AddInstruction(Code.Instruction i)
        {
            m_instructions.Add(i);
        }
        public void AddParameter(Variable p)
        {
            if (!m_sc.Parameters.Contains(p))
                m_sc.Parameters.Add(p);
            else
                StaticBase.Singleton.Log.Write(Log.InfoType.ShaderWarning, "Parameter already defined: \"{0}\" reusing existing\n", p.Name);
        }
        public void ForceVSVariable(Variable v)
        {
            m_userForcedVSVariable.Add(v);
        }

        Variable OutputVar(Instruction i)
        {
            Variable outVar = null;

            //try with defined
            Debug.Assert(i.DefinedVariables.Count <= 1);
            if (i.DefinedVariables.Count > 0)
                outVar = i.DefinedVariables[0];

            //try write var
            if (outVar == null)
            {
                Debug.Assert(i.WritenVariables.Count == 1);
                outVar = i.WritenVariables[0];
            }

            return outVar;
        }
        void AddVSInstruction(Instruction i)
        {
            foreach (var v in i.DefinedVariables)
                m_vsDefinedVars.Add(v, null);

            m_sc.VSInstructions.Add(i);
        }
        void AddPSInstruction(Instruction i)
        {
            foreach (var v in i.DefinedVariables)
                m_psDefinedVars.Add(v, null);
            foreach (var v in i.ReadVariables)
            {
                if (!m_psUsedVars.ContainsKey(v))
                    m_psUsedVars.Add(v, null);
            }
            foreach (var v in i.WritenVariables)
            {
                if (!m_psUsedVars.ContainsKey(v))
                    m_psUsedVars.Add(v, null);
            }

            m_sc.PSInstructions.Add(i);
        }

        void BuildDebugVariableMap()
        {
            int id = 1;

            foreach (var v in m_vsDefinedVars)
                m_sc.AddDebugId(v.Key, id++);

            foreach (var v in m_psDefinedVars)
                m_sc.AddDebugId(v.Key, id++);

            foreach (var v in m_sc.Parameters)
                m_sc.AddDebugId(v, id++);
        }
        void BuildForcedVariables()
        {
            //ps force
            foreach (var v in m_shaderOutput.FindAll((x) => x.Semantic != VerticesStreamSemantic.POSITION))
                m_psForce.Add(v, null);

            //vs force
            Queue<Variable> vsQueue = new Queue<Variable>();

            Variable positionVar = m_shaderOutput.Find((x) => x.Semantic == VerticesStreamSemantic.POSITION);
            Dictionary<Variable, List<Variable>> dep = BuildDependencyList();

            if (positionVar != null)
                vsQueue.Enqueue(positionVar);

            foreach (var v in m_userForcedVSVariable)
                vsQueue.Enqueue(v);

            while (vsQueue.Count > 0)
            {
                Variable v = vsQueue.Dequeue();
                if (!m_vsForce.ContainsKey(v))
                {
                    //add this
                    m_vsForce.Add(v, null);

                    //enqueue deps
                    Debug.Assert(dep.ContainsKey(v));
                    foreach (var d in dep[v])
                        vsQueue.Enqueue(d);
                }
            }
        }
        void GenerateDebugData()
        {
            BuildDebugVariableMap();

            //create debug vars
            Variable selector = new Variable(ShaderCode.DEBUG_VARIABLE_SELECTION_PARAMETER_NAME, Format.FLOAT);
            AddParameter(selector);

            Variable debugOutput = new Variable(ShaderCode.DEBUG_OUTPUT_NAME, Format.FLOAT4, VerticesStreamSemantic.DEBUG, 0);
            m_sc.VSInstructions.Add(InstructionHelper.CreateVariableWithDefaultValue(debugOutput, 0));
            m_sc.VSToPS.Add(debugOutput);

            //add debug instructions to vs
            foreach (var v in m_vsDefinedVars)
                m_sc.VSInstructions.Add(InstructionHelper.DebugInstruction(selector, m_sc.GetDebugId(v.Key), debugOutput, v.Key));

            //add debug instructions to ps
            foreach (var v in m_psDefinedVars)
                m_sc.PSInstructions.Add(InstructionHelper.DebugInstruction(selector, m_sc.GetDebugId(v.Key), debugOutput, v.Key));

            //add parameters to ps
            foreach (var v in m_sc.Parameters)
            {
                if (v.Format == Format.FLOAT || v.Format == Format.FLOAT2 || v.Format == Format.FLOAT3 || v.Format == Format.FLOAT4)
                    m_sc.PSInstructions.Add(InstructionHelper.DebugInstruction(selector, m_sc.GetDebugId(v), debugOutput, v));
            }

            //remove standard output
            foreach (var v in m_sc.PSOutput)
                m_sc.PSInstructions.Insert(0, InstructionHelper.CreateVariableWithDefaultValue(v, 0));
            m_sc.PSOutput.Clear();

            //add debug output
            m_sc.PSOutput.Add(debugOutput);
        }
        void SplitCode()
        {
            foreach (var i in m_instructions)
            {
                //select output var
                Variable outVar = OutputVar(i);

                //cant be forced to ps & vs
                Debug.Assert(!(m_vsForce.ContainsKey(outVar) && m_psForce.ContainsKey(outVar)));

                bool allowVS = i.Linear;

                if (m_psForce.ContainsKey(outVar))
                    allowVS = false;

                //check instruction dependency
                foreach (var v in i.ReadVariables)
                {
                    if (!m_vsDefinedVars.ContainsKey(v) && !m_sc.Parameters.Contains(v))
                    {
                        Debug.Assert(!m_vsForce.ContainsKey(outVar));
                        allowVS = false;
                    }
                }

                if (allowVS || m_vsForce.ContainsKey(outVar))
                    AddVSInstruction(i);
                else
                    AddPSInstruction(i);
            }
        }
        void PostSplitOptimizations()
        {
            //move const to ps

            //build const list
            Dictionary<Variable, Instruction> constVars = new Dictionary<Variable, Instruction>();
            foreach (var i in m_sc.VSInstructions)
            {
                //add if const
                if (i.Const)
                {
                    foreach (var d in i.DefinedVariables)
                        constVars.Add(d, i);

                    continue;
                }

                //remove if writen to
                foreach (var d in i.WritenVariables)
                    constVars.Remove(d);
            }

            //remove coonst from "vs to ps" list
            for (int i = 0; i < m_sc.VSToPS.Count; i++)
            {
                if (constVars.ContainsKey(m_sc.VSToPS[i]))
                {
                    m_sc.PSInstructions.Insert(0, constVars[m_sc.VSToPS[i]]);
                    m_sc.VSToPS.RemoveAt(i);
                    i--;
                }
            }

        }

        Dictionary<Variable, List<Variable>> BuildDependencyList()
        {
            Dictionary<Variable, List<Variable>> dep = new Dictionary<Variable, List<Variable>>();

            foreach (var d in m_sc.Parameters)
                dep.Add(d, new List<Variable>());

            foreach (var i in m_shaderInput)
                dep.Add(i, new List<Variable>());

            foreach (var i in m_instructions)
            {
                Variable outVar = null;

                //try with defined
                Debug.Assert(i.DefinedVariables.Count <= 1);
                if (i.DefinedVariables.Count > 0)
                    outVar = i.DefinedVariables[0];

                //try write var
                if (outVar == null)
                {
                    Debug.Assert(i.WritenVariables.Count == 1);
                    outVar = i.WritenVariables[0];
                }

                //add dep
                if (!dep.ContainsKey(outVar))
                    dep.Add(outVar, new List<Variable>(i.ReadVariables));
                else
                    ListHelper.AddUniqueRange(dep[outVar], i.ReadVariables);
            }

            return dep;
        }

        public ShaderCode Finish(bool generateDebug)
        {
            BuildForcedVariables();

            //copy vs input
            foreach (var si in m_shaderInput)
            {
                m_sc.VSInput.Add(si);
                m_vsDefinedVars.Add(si, null);
            }

            //copy ps output
            foreach (var so in m_shaderOutput)
            {
                m_sc.PSOutput.Add(so);
                m_psDefinedVars.Add(so, null);
            }

            //split code
            SplitCode();

            //connect vs to ps
            foreach (var kvp in m_psUsedVars)
            {
                if (!m_psDefinedVars.ContainsKey(kvp.Key) && !m_sc.Parameters.Contains(kvp.Key))
                    m_sc.VSToPS.Add(kvp.Key);
            }

            PostSplitOptimizations();

            if (generateDebug)
                GenerateDebugData();

            return m_sc;
        }

        #region private

        readonly ShaderCode m_sc = new ShaderCode();

        readonly List<Code.Instruction> m_instructions = new List<Core.CodeGeneration.Code.Instruction>();

        readonly List<Variable> m_shaderInput = new List<Variable>();
        readonly List<Variable> m_shaderOutput = new List<Variable>();

        readonly Dictionary<Variable, object> m_vsDefinedVars = new Dictionary<Variable, object>();
        readonly Dictionary<Variable, object> m_psDefinedVars = new Dictionary<Variable, object>();

        readonly Dictionary<Variable, object> m_vsForce = new Dictionary<Variable, object>();
        readonly Dictionary<Variable, object> m_psForce = new Dictionary<Variable, object>();

        readonly Dictionary<Variable, object> m_psUsedVars = new Dictionary<Variable, object>();

        readonly List<Variable> m_userForcedVSVariable = new List<Variable>();

        #endregion
    }
}
