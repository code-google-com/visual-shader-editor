using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Basic
{
    public static class ListHelper
    {
        public static void AddUniqueRange<T>(List<T> l, IList<T> add)
        {
            //add unique
            foreach (var v in add)
                if (!l.Contains(v))
                    l.Add(v);
        }
    }
}
