using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Core.Basic;

namespace Core.Var
{
    public class VariableManager
    {
        class VarInfo
        {
            internal VarInfo(Variable v, BaseBlock owner)
            {
                Variable = v;
                Owners.Add(owner);
            }
            internal void AddOwner(BaseBlock owner)
            {
                Debug.Assert(!Owners.Contains(owner));
                Owners.Add(owner);
            }
            internal void RemoveOwner(BaseBlock owner)
            {
                bool r = Owners.Remove(owner);
                Debug.Assert(r);
            }
            internal readonly List<BaseBlock> Owners = new List<BaseBlock>();
            internal readonly Variable Variable;
        }

        public Variable CreateVariable(string name, Format f, VerticesStreamSemantic s, int semanticIndex, BaseBlock owner)
        {
            return CreateVariable(name, f, s, semanticIndex, owner, false);
        }
        public Variable CreateVariable(string name, Format f, VerticesStreamSemantic s, int semanticIndex, BaseBlock owner, bool allowReuse)
        {
            Variable v;

            if (!CheckIfNameIsAvailable(name))
            {
                Debug.Assert(allowReuse);

                var x = m_variableMap[name];
                v = x.Variable;
                x.AddOwner(owner);

                Debug.Assert(v.Name == name && v.Format == f && v.Semantic == s && v.SemanticIndex == semanticIndex);
            }
            else
            {
                v = new Variable(name, f, s, semanticIndex);
                m_variableMap.Add(name, new VarInfo(v, owner));
            }

            return v;
        }
        public Variable CreateVariable(Format f, BaseBlock owner)
        {
            string name = string.Format("Var_{0}", m_variableCounter++);
            Debug.Assert(CheckIfNameIsAvailable(name));

            Variable v = new Variable(name, f);
            m_variableMap.Add(name, new VarInfo(v, owner));

            return v;
        }
        public void DestroyVariable(Variable v, BaseBlock owner)
        {
            Debug.Assert(!CheckIfNameIsAvailable(v.Name));
            var x = m_variableMap[v.Name];
            x.RemoveOwner(owner);

            //last use? remove it
            if (x.Owners.Count == 0)
                m_variableMap.Remove(v.Name);
        }

        public bool CheckIfNameIsAvailable(string n)
        {
            return !m_variableMap.ContainsKey(n);
        }
        public bool CheckIfNameIsAvailable(string n, BaseBlock me)
        {
            if (m_variableMap.ContainsKey(n))
                return m_variableMap[n].Owners.Contains(me);

            return true;
        }
        public List<BaseBlock> FindOwner(string n)
        {
            VarInfo vi;
            if (m_variableMap.TryGetValue(n, out vi))
                return new List<BaseBlock>(vi.Owners);

            return null;
        }

        #region private

        int m_variableCounter = 0;

        readonly Dictionary<string, VarInfo> m_variableMap = new Dictionary<string, VarInfo>();

        #endregion
    }
}
