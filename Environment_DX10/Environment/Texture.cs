using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment.Texture;
using SlimDX.Direct3D10;

namespace Environment_DX10.Environment
{
    public class Texture : ITexture
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }



        public Resource D3DResurce;
        public ShaderResourceView ResurceView;

        public string File
        {
            get { return ""; }
        }
    }
}
