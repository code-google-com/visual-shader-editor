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
