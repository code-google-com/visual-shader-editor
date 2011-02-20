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
