using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Basic
{
    public class BlockAttribute : Attribute
    {
        public string Name;
        public string Path = "root";
    }
}
