using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Main;
using System.Diagnostics;

namespace Core.CodeGeneration
{
    public class InstructionListBuilder
    {
        public InstructionListBuilder(BlockManager bm)
        {
            m_bm = bm;

            BuildList();
        }

        public List<BaseBlock> List
        {
            get { return m_list; }
        }

        void BuildList()
        {
            BuildOutputConnectionCouterList();

            //add block witout output connection
            foreach (var kvp in m_outputConnectionCouter)
                if (kvp.Value == 0)
                    m_addQueue.Enqueue(kvp.Key);

            //add next objects
            while (m_addQueue.Count > 0)
                AddToList(m_addQueue.Dequeue());

            //check if all have been copied
            Debug.Assert(m_list.Count == m_bm.Blocks.Count);
            foreach (var kvp in m_outputConnectionCouter)
                Debug.Assert(m_list.Contains(kvp.Key));

            //check if all have connection count == 0
            foreach (var kvp in m_outputConnectionCouter)
                Debug.Assert(kvp.Value == 0);

            //reverse
            m_list.Reverse();

            Debug.Assert(m_list.Count == m_bm.Blocks.Count);
        }

        int ConnectedOutputCount(BaseBlock b)
        {
            int count = 0;
            foreach (var o in b.Outputs)
                count += o.ConnectedTo.Count;

            return count;
        }

        void AddToList(BaseBlock b)
        {
            //only once
            Debug.Assert(!m_addOnlyOnce.ContainsKey(b));

            //add
            m_addOnlyOnce.Add(b, b);
            m_list.Add(b);

            //decrase connections to others
            foreach (var i in b.Inputs)
            {
                if (i.ConnectedTo != null)
                {
                    BaseBlock connectedTo = i.ConnectedTo.Owner;

                    m_outputConnectionCouter[connectedTo]--;
                    Debug.Assert(m_outputConnectionCouter[connectedTo] >= 0);

                    if (m_outputConnectionCouter[connectedTo] == 0)
                        m_addQueue.Enqueue(connectedTo);
                }
            }
        }

        void BuildOutputConnectionCouterList()
        {
            foreach (var b in m_bm.Blocks)
                m_outputConnectionCouter.Add(b, ConnectedOutputCount(b));
        }

        #region private

        readonly BlockManager m_bm;

        //output list
        readonly List<BaseBlock> m_list = new List<BaseBlock>();
        readonly Dictionary<BaseBlock, BaseBlock> m_addOnlyOnce = new Dictionary<BaseBlock, BaseBlock>();

        //queue
        readonly Queue<BaseBlock> m_addQueue = new Queue<BaseBlock>();
        readonly Dictionary<BaseBlock, int> m_outputConnectionCouter = new Dictionary<BaseBlock, int>();

        #endregion
    }
}
