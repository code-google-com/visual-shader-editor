using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Environment.Texture
{
    public interface ITexture : IDisposable
    {
        string File { get; }
    }
}
