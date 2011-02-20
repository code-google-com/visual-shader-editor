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
