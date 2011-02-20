using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Basic
{
    public class BlockInput : BlockIOBase
    {
        public BlockInput(BaseBlock owner, string name) : base(owner, name) { }

        BlockOutput m_connectedTo;

        public BlockOutput ConnectedTo
        {
            get { return m_connectedTo; }
            internal set
            {
                m_connectedTo = value;
            }
        }
    }
}
