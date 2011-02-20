using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Main
{
    public class Log
    {
        public enum InfoType
        {
            GlobalInfo,
            GlobalWarning,
            GlobalError,

            ShaderInfo,
            ShaderWarning,
            ShaderError
        }

        public struct LogEntry
        {
            public LogEntry(DateTime time, InfoType it, string text)
            {
                Date = time;
                Type = it;
                Text = text;
            }
            public readonly DateTime Date;
            public readonly InfoType Type;
            public readonly string Text;

            public override string ToString()
            {
                return string.Format("[{0}][{1}] {2}", Date, Type, Text);
            }
        }

        public void Connect(Action<LogEntry> c, bool sendHistory)
        {
            m_connected.Add(c);

            if (sendHistory)
                for (int i = 0; i < m_text.Count; i++)
                    c(m_text[i]);
        }
        public void Disconnect(Action<LogEntry> c)
        {
            m_connected.Remove(c);
        }

        public void Write(InfoType it, string format, params object[] p)
        {
            LogEntry le = new LogEntry(DateTime.Now, it, string.Format(format, p));

            m_text.Add(le);

            for (int i = 0; i < m_connected.Count; i++)
                m_connected[i](le);
        }

        #region private

        readonly List<LogEntry> m_text = new List<LogEntry>();
        readonly List<Action<LogEntry>> m_connected = new List<Action<LogEntry>>();

        #endregion
    }
}
