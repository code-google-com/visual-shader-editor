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
using System.Diagnostics;

namespace Core.Main
{
    public class BlockList
    {
        [Obsolete]
        public class BlockEntry
        {
            internal BlockEntry(EntyType et, string name) 
            {
                Debug.Assert(et == EntyType.Folder);
                m_et = et;
                Name = name;            
            }
            internal BlockEntry(EntyType et, string name, Type t)
            {
                Debug.Assert(et == EntyType.Description);
                m_et = et;
                Name = name;
                Type = t;
            }

            public enum EntyType
            {
                Folder,
                Description
            }

            public EntyType EntryType
            {
                get { return m_et; }
            }
            public int ChildCount
            {
                get { return m_children.Count; }
            }
            public BlockEntry GetChild(int id)
            {
                return m_children[id];
            }
            public BlockEntry GetChild(string name)
            {
                for (int i = 0; i < m_children.Count; i++)
                {
                    if (m_children[i].Name == name)
                        return m_children[i];
                }

                return null;
            }
            internal void AddChild(BlockEntry be)
            {
                if (GetChild(be.Name) != null || EntryType != EntyType.Folder)
                    throw new Exception("name already in use or parent is not folder");

                m_children.Add(be);
            }

            public readonly string Name;
            public readonly Type Type;
            public string TypeName
            {
                get { return Type.AssemblyQualifiedName; }
            }

            #region private

            readonly EntyType m_et;
            readonly List<BlockEntry> m_children = new List<BlockEntry>();

            #endregion
        }

        public void RegisterType(Type t)
        {
            BlockAttribute atr = Attribute.GetCustomAttribute(t, typeof(BlockAttribute)) as BlockAttribute;
            string[] path = atr.Path.Split('/');

            //add
            m_blocks.Add(t.AssemblyQualifiedName, t);

            //add path to tree
            BlockEntry be = RootBlock;

            foreach (var s in path)
            {
                BlockEntry b = be.GetChild(s);
                if (b == null)
                {
                    b = new BlockEntry(BlockEntry.EntyType.Folder, s);
                    be.AddChild(b);
                }

                be = b;
            }

            //add item
            be.AddChild(new BlockEntry(BlockEntry.EntyType.Description, atr.Name, t));

            PathBlockList.Add(atr.Path + '/' + atr.Name, t);

        }
        public BaseBlock CreateBlockByTypeName(BlockManager owner, string name)
        {
            return (BaseBlock)m_blocks[name].Assembly.CreateInstance(m_blocks[name].FullName, false,
                System.Reflection.BindingFlags.CreateInstance, null, new object[] { owner }, null, null);
        }
        [Obsolete]
        public readonly BlockEntry RootBlock = new BlockEntry(BlockEntry.EntyType.Folder, "root");
        public readonly Dictionary<string, Type> PathBlockList = new Dictionary<string, Type>();

        #region private

        readonly Dictionary<string, Type> m_blocks = new Dictionary<string, Type>();

        #endregion
    }
}
