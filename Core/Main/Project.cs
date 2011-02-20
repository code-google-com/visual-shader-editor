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
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using Core.Var;

namespace Core.Main
{
    public class Project : IDisposable
    {
        static readonly string MAIN_XML_ELEMENT_NAME = "VisualShaderEditorProject";
        static readonly string FILES_LIST_XML_ELEMENT_NAME = "Files";
        static readonly string FILE_XML_ELEMENT_NAME = "File";

        static readonly string FILE_PATH_ATTRIBUTE_NAME = "FilePath";


        public Project(string filePath)
            : this(filePath, true)
        {
        }

        public ProjectFile CreateShader(string filePath, BlockManager.Type t)
        {
            if (m_files.ContainsKey(filePath))
                throw new ArgumentException("file already exist");

            switch (t)
            {
                case BlockManager.Type.ShaderFile: filePath += ".vses"; break;
                case BlockManager.Type.UserBlock: filePath += ".vseub"; break;
                default:
                    throw new NotImplementedException();
            }

            ProjectFile pf = new ProjectFile(this, filePath, t);
            m_files.Add(filePath, pf);
            return pf;
        }
        public void DeleteShader(ProjectFile pf)
        {
            m_files.Remove(pf.FilePath);
            File.Delete(Path.Combine(m_projectPath, pf.FilePath));
        }
        public ProjectFile GetShader(string filePath)
        {
            ProjectFile pf;
            if (m_files.TryGetValue(filePath, out pf))
                return pf;

            return null;
        }
        public ReadOnlyCollection<ProjectFile> Files
        {
            get { return new ReadOnlyCollection<ProjectFile>(new List<ProjectFile>(m_files.Values)); }
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(m_fileName));
            File.Delete(m_fileName);
            FileStream str = File.OpenWrite(m_fileName);

            XmlDocument d = new XmlDocument();
            d.AppendChild(d.CreateXmlDeclaration("1.0", "utf-8", ""));
            d.AppendChild(d.CreateElement(MAIN_XML_ELEMENT_NAME));

            //save to xml
            XmlElement mainElement = d.DocumentElement;
            Save(mainElement);

            //save to stream
            d.Save(str);
            str.Close();
        }
        public static Project Load(string filePath)
        {
            Project pf = new Project(filePath, false);
            FileStream str = File.OpenRead(filePath);

            XmlDocument d = new XmlDocument();

            //load from stream
            d.Load(str);
            str.Close();

            //load from xml
            XmlElement mainElement = d.DocumentElement;
            pf.Load(mainElement);

            return pf;
        }

        internal FileStream OpenRead(string file)
        {
            return File.OpenRead(Path.Combine(m_projectPath, file));
        }
        internal FileStream OpenWrite(string file)
        {
            string fullPath = Path.Combine(m_projectPath, file);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            File.Delete(fullPath);
            return File.OpenWrite(fullPath);
        }
        internal string ProjectPath
        {
            get { return m_projectPath; }
        }

        public string ProjectName
        {
            get { return Path.GetFileNameWithoutExtension(m_fileName); }
        }

        public void Dispose()
        {
            //Save();
        }

        public string ConvertResourcePathForSave(string f)
        {
            string[] fileDirs = Path.GetFullPath(f).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string[] projectDirs = Path.GetDirectoryName(Path.GetFullPath(m_fileName)).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string outPath = "";

            int i = 0;
            for (i = 0; i < fileDirs.Length && i < projectDirs.Length; i++)
            {
                if (string.Compare(fileDirs[i], projectDirs[i], true) != 0)
                    break;
            }

            Debug.Assert(i != 0);

            for (int j = i; j < projectDirs.Length; j++)
                outPath += "../";

            for (int j = i; j < fileDirs.Length; j++)
                outPath += fileDirs[j] + '/';

            //remove last '/'
            outPath = outPath.Remove(outPath.Length - 1, 1);

            Debug.Assert(ConvertResourcePathFromSaved(outPath) == f);

            return outPath;
        }
        public string ConvertResourcePathFromSaved(string f)
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(m_fileName), f));
        }

        #region private

        void Save(XmlElement node)
        {
            XmlElement mainElement = node;
            XmlElement FilesElement = (XmlElement)mainElement.AppendChild(mainElement.OwnerDocument.CreateElement(FILES_LIST_XML_ELEMENT_NAME));

            //save files list
            foreach (var kvp in m_files)
            {
                XmlElement fElement = (XmlElement)FilesElement.AppendChild(mainElement.OwnerDocument.CreateElement(FILE_XML_ELEMENT_NAME));
                fElement.SetAttribute(FILE_PATH_ATTRIBUTE_NAME, kvp.Key);

                //save file too
                kvp.Value.Save();
            }
        }
        void Load(XmlElement node)
        {
            XmlElement mainElement = node;
            XmlElement filesElement = mainElement[FILES_LIST_XML_ELEMENT_NAME];

            //load files
            foreach (var xb in filesElement.ChildNodes)
            {
                XmlElement fElement = xb as XmlElement;
                if (fElement == null || fElement.Name != FILE_XML_ELEMENT_NAME)
                    continue;

                string filePath = fElement.GetAttribute(FILE_PATH_ATTRIBUTE_NAME);

                //load file
                m_files.Add(filePath, new ProjectFile(this, filePath));
            }
        }

        public Project(string filePath, bool create)
        {
            m_fileName = filePath;
            m_projectPath = Path.GetDirectoryName(filePath);

            if (create)
                Save();
        }

        readonly string m_fileName;
        readonly string m_projectPath;

        readonly List<Profile> m_profiles = new List<Profile>();
        readonly Dictionary<string, ProjectFile> m_files = new Dictionary<string, ProjectFile>();

        #endregion
    }
}
