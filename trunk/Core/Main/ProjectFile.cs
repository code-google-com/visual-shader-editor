/*
Copyright (c) 2011, Pawel Szczurek
All rights reserved.


Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:


Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

Neither the name of the <ORGANIZATION> nor the names of its contributors may be used to endorse or promote products derived from this software without
specific prior written permission.


THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Core.Var;

namespace Core.Main
{
    public class ProjectFile
    {
        public ProjectFile(Project owner, string filePath)
        {
            m_owner = owner;
            m_filePath = filePath;
            m_blockManager = new BlockManager(BlockManager.Type.ShaderFile, new VariableManager(), this);
            Load();
        }
        public ProjectFile(Project owner, string filePath, BlockManager.Type t)
        {
            m_owner = owner;
            m_filePath = filePath;
            m_blockManager = new BlockManager(t, new VariableManager(), this);
            Save();
        }

        public string FileName
        {
            get { return Path.GetFileNameWithoutExtension(m_filePath); }
        }
        public string FullFileName
        {
            get { return Path.Combine(Path.GetDirectoryName(m_filePath), Path.GetFileNameWithoutExtension(m_filePath)); }
        }
        public string FullPath
        {
            get { return Path.Combine(m_owner.ProjectPath, Path.GetFileName(m_filePath)); }
        }
        public string FilePath
        {
            get { return m_filePath; }
        }
        public string PreviewOutFile
        {
            get
            {
                Directory.CreateDirectory(Path.Combine(m_owner.ProjectPath, "Out/Preview/"));
                return Path.Combine(m_owner.ProjectPath, "Out/Preview/" + Path.GetFileNameWithoutExtension(m_filePath));
            }
        }
        public string ReleaseOutFile
        {
            get
            {
                Directory.CreateDirectory(Path.Combine(m_owner.ProjectPath, "Out/Release/"));
                return Path.Combine(m_owner.ProjectPath, "Out/Release/" + Path.GetFileNameWithoutExtension(m_filePath));
            }
        }

        public void Load()
        {
            FileStream str = m_owner.OpenRead(m_filePath);
            m_blockManager.Load(str);
            str.Close();
        }
        public void Save()
        {
            FileStream str = m_owner.OpenWrite(m_filePath);
            m_blockManager.Save(str);
            str.Close();
        }

        public BlockManager BlockManager
        {
            get { return m_blockManager; }
        }

        public Project Owner
        {
            get { return m_owner; }
        }

        #region private

        readonly Project m_owner;
        readonly string m_filePath;
        readonly BlockManager m_blockManager;

        #endregion
    }
}
