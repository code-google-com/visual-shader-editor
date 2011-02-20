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
using Core.Environment;
using System.Diagnostics;

namespace Core.WorkSpaceController
{
    public class DrawHelper
    {
        public void Draw(IWorkSpace ws)
        {
            ws.DrawRectangles(m_rectangles);
            ws.DrawLines(m_connections, 2, new Vector4f(1, 1, 1, 1));
            ws.DrawTexts(m_texts);
        }
        public void Clear()
        {
            m_connections.Clear();
            m_rectangles.Clear();
            m_texts.Clear();
        }

        public void AddRectangle(ColorRectangle r)
        {
            m_rectangles.Add(r);
        }
        public void AddConnection(Line2f l)
        {
            m_connections.Add(l);
        }
        public void AddText(ColorText t)
        {
            Debug.Assert(t.Text != null);
            m_texts.Add(t);
        }

        #region private

        List<Line2f> m_connections = new List<Line2f>();
        readonly List<ColorRectangle> m_rectangles = new List<ColorRectangle>();
        readonly List<ColorText> m_texts = new List<ColorText>();

        #endregion
    }
}
