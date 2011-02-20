using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using Core.Main;
using System.IO;
using Core.Blocks.Input;
using Core.CodeGeneration;
using Core.Basic;
using Core.Blocks.Output;
using SlimDX.Direct3D10;
using Core.Environment.Texture;
using System.Windows.Forms;

namespace Environment_DX10.Environment
{
    [EnvironmentAttribute(Name = "Environment_DX10")]
    public class Environment : IEnvironment
    {
        public Environment()
        {
          //  m_mainDevice = new Device(DeviceCreationFlags.None);
         //   m_textureManager = new TextureManager(m_mainDevice);

            m_openFileDialog = new OpenFileDialog();
            m_openFileDialog.CheckFileExists = true;
            m_openFileDialog.CheckPathExists = true;

           // SlimDX.Configuration.DetectDoubleDispose = true;
          //  SlimDX.Configuration.EnableObjectTracking = true;
          //  SlimDX.Configuration.ThrowOnError = true;
           // SlimDX.ObjectTable.

        }

        public IWorkSpace CreateWorkSpace(BlockManager bm, Control c)
        {
            return new WorkSpace(bm, c);
        }

        void IDisposable.Dispose()
        {
            //throw new NotImplementedException();
        }

        public string ShowTextureBrowser(string lastSelection)
        {
            if (m_openFileDialog.ShowDialog() == DialogResult.OK)
                return m_openFileDialog.FileName;

            return null;
            //throw new NotImplementedException();
        }

        OpenFileDialog m_openFileDialog;

    }
}
