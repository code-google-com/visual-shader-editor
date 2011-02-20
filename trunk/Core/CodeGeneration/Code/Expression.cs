using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Var;

namespace Core.CodeGeneration.Code
{
    public abstract class Expression
    {
        protected abstract Format CalculateOutputFormat();
        public Format OutputFormat
        {
            get
            {
                if (m_outputFormatDirty)
                {
                    m_outputFormat = CalculateOutputFormat();
                    m_outputFormatDirty = false;
                }

                return m_outputFormat;
            }
        }

        bool m_outputFormatDirty = true;
        Format m_outputFormat;

        public abstract IList<Variable> ReadVariables { get; }
        public abstract bool Linear { get; }
        public abstract bool Const { get; }
    }
}
