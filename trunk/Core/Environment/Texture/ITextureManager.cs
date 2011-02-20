using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Environment.Texture
{
    public interface ITextureManager
    {
        string ShowTextureBrowser();
        ITexture LoadTexture(string file);
        ITexture DefaultTexture { get; }
    }
}
