using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Core.Main;
using Core.Environment.Texture;

namespace Core.Environment
{
    public interface IEnvironment : IDisposable
    {
        string ShowTextureBrowser(string lastSelection);
        //string Description { get; }
       // IPreview CreatePreview(Control c);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="fileName">file name, no extension</param>
        //ICompiledShader Export(BlockManager bm, string fileName);

       // ITextureManager TextureManager { get; }

       // void SetPreviewShader(ICompiledShader cs);

        IWorkSpace CreateWorkSpace(BlockManager bm, Control c);
    }
}
