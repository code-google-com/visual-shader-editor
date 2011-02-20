using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using System.IO;
using Core.Blocks.Input;
using Core.CodeGeneration;
using Core.Basic;
using Core.Blocks.Output;
using Core.Environment.Texture;
using System.Windows.Forms;
using Core.Main;

namespace Environment_OGL.Environment
{
    [EnvironmentAttribute(Name = "Environment_OGL")]
    public class Environment : IEnvironment
    {
        public Environment()
        {
          //  m_mainDevice = new Device(DeviceCreationFlags.None);
         //   m_textureManager = new TextureManager(m_mainDevice);

            m_openFileDialog = new OpenFileDialog();
            m_openFileDialog.CheckFileExists = true;
            m_openFileDialog.CheckPathExists = true;
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
