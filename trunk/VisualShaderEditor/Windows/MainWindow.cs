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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core.Main;
using System.IO;
using System.Diagnostics;
using Core.Environment;
using Core.Basic;
using Core.WorkSpaceController;

namespace VisualShaderEditor.Windows
{
    public partial class MainWindow : Form
    {
        readonly static string NATIVE_BLOCKS_ROOT = "Native";
        readonly static string USER_BLOCKS_ROOT = "UserBlocks";

        class ProjectPage
        {
            internal ProjectFile ProjectFile;
            internal TabPage TabPage;
            internal IWorkSpace WorkSpace;
            internal Panel Panel;
            internal WorkSpaceController WorkSpaceController;
        }

        public MainWindow()
        {
            InitializeComponent();

            timer_AutoRefresh.Tick += new EventHandler(timer_AutoRefresh_Tick);

            StaticBase.Singleton.Log.Connect(new Action<Log.LogEntry>(OnLog), true);
            RefreshBlockTree();

            StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "Main window ready\n");
        }

        void timer_AutoRefresh_Tick(object sender, EventArgs e)
        {
            RedrawCurrentProjectPage();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //close old
            TryCloseProject(true);

            //create new
            if (saveFileDialog_SaveProject.ShowDialog() == DialogResult.OK)
            {
                string fullPath = saveFileDialog_SaveProject.FileName;
                string path = Path.GetDirectoryName(fullPath);
                string projectName = Path.GetFileName(fullPath);

                //add directory
                string folderPath = Path.Combine(path, Path.GetFileNameWithoutExtension(fullPath));
                string newFilePath = Path.Combine(folderPath, projectName);

                //create project
                m_project = new Project(newFilePath);
                StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "project create\n");
            }

            //refresh project tree
            RefreshProjectTree();
        }
        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //close old
            TryCloseProject(true);

            //open new
            if (openFileDialog_OpenProject.ShowDialog() == DialogResult.OK)
            {
                m_project = Project.Load(openFileDialog_OpenProject.FileName);
                StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "project opened\n");
            }

            //refresh project tree
            RefreshProjectTree();
        }
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrySaveProject();
        }
        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryCloseProject(true);
        }

        private void shaderToolStripMenuItem_Click_CreateShader(object sender, EventArgs e)
        {
            if (m_project == null)
                return;

            SimpleEditBox seb = new SimpleEditBox();
            if (seb.ShowDialog() == DialogResult.OK)
            {
                m_project.CreateShader(seb.Text, BlockManager.Type.ShaderFile);
            }

            SaveAll();
            RefreshProjectTree();
        }

        private void treeView_Project_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null)
                return;

            CreateOrSelectProjectFileTab((ProjectFile)e.Node.Tag);
        }

        #region project tab control

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //tabControl_ProjectFiles.
        }
        private void tabControl_ProjectFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawCurrentProjectPage();
        }
        void p_Paint(object sender, PaintEventArgs e)
        {
            ProjectPage pp = m_openedFiles.Find((x) => x.Panel == (Panel)sender);
            if (pp != null)
                RedrawProjectPage(pp);
        }
        private void toolStripButton2_Click_Refresh(object sender, EventArgs e)
        {
            RedrawCurrentProjectPage();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveAll();
            base.OnFormClosing(e);
        }

        #endregion

        #region private

        void OnLog(Log.LogEntry le)
        {
            if (InvokeRequired)
                Invoke(new Action<Log.LogEntry>(OnLog), le);
            else
            {
                bool global;
                Color color = Color.Black;
                string s = le.ToString();

                switch (le.Type)
                {
                    case Log.InfoType.GlobalInfo: color = Color.Black; global = true; break;
                    case Log.InfoType.GlobalWarning: color = Color.Orange; global = true; break;
                    case Log.InfoType.GlobalError: color = Color.Red; global = true; break;
                    case Log.InfoType.ShaderInfo: color = Color.Black; global = false; break;
                    case Log.InfoType.ShaderWarning: color = Color.Orange; global = false; break;
                    case Log.InfoType.ShaderError: color = Color.Red; global = false; break;
                    default:
                        throw new NotImplementedException();
                }

                lock (m_logLock)
                {
                    if (global)
                    {
                        richTextBox_Log.SelectionStart = richTextBox_Log.TextLength;
                        richTextBox_Log.SelectionColor = color;
                        richTextBox_Log.AppendText(s);
                        richTextBox_Log.ScrollToCaret();
                        richTextBox_Log.Update();
                    }
                    else
                    {
                        richTextBox_Shader.SelectionStart = richTextBox_Log.TextLength;
                        richTextBox_Shader.SelectionColor = color;
                        richTextBox_Shader.AppendText(s);
                        richTextBox_Shader.ScrollToCaret();
                        richTextBox_Shader.Update();
                    }
                }
            }
        }

        void TryCloseProject(bool save)
        {
            if(save)
                SaveAll();

            tabControl_ProjectFiles.TabPages.Clear();
            m_openedFiles.Clear();

            if (m_project != null)
            {
                m_project.Dispose();
                m_project = null;
                StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "project closed\n");
            }

            //refresh project tree
            RefreshProjectTree();
        }
        void TrySaveProject()
        {
            SaveAll();

            if (m_project != null)
                StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "project saved\n");

            //refresh project tree
            RefreshProjectTree();
        }
        void RefreshProjectTree()
        {
            treeView_Project.Nodes.Clear();

            if (m_project != null)
            {
                TreeNode root = treeView_Project.Nodes.Add(m_project.ProjectName, m_project.ProjectName);
                root.ImageIndex = 0;
                root.SelectedImageIndex = 0;
                root.ContextMenuStrip = contextMenuStrip_ProjectView_Root;

                foreach (var f in m_project.Files)
                {
                    string[] list = f.FullFileName.Split('/', '\\');
                    TreeNode currentNode = root;

                    //build tree
                    for (int i = 0; i < list.Length; i++)
                    {
                        //current node must be folder(Tag == null)
                        Debug.Assert(currentNode.Tag == null);

                        if (i + 1 == list.Length)
                        {
                            //add node
                            TreeNode tn = currentNode.Nodes.Add(list[i], list[i]);
                            tn.ContextMenuStrip = contextMenuStrip_ProjectView_Shader;
                            tn.Tag = f;
                            tn.ImageIndex = 1;
                            tn.SelectedImageIndex = 1;
                        }
                        else
                        {
                            //add path
                            TreeNode tn = currentNode.Nodes[list[i]];
                            if (tn == null)
                            {
                                currentNode = currentNode.Nodes.Add(list[i], list[i]);
                                currentNode.ContextMenuStrip = contextMenuStrip_ProjectView_Folder;
                                currentNode.ImageIndex = 0;
                                currentNode.SelectedImageIndex = 0;
                            }
                            else
                                currentNode = tn;
                        }
                    }
                }
            }

            treeView_Project.ExpandAll();
        }
        void RefreshBlockTree()
        {
            treeView_Blocks.Nodes.Clear();
            TreeNode native = treeView_Blocks.Nodes.Add(NATIVE_BLOCKS_ROOT, NATIVE_BLOCKS_ROOT);
            native.ImageIndex = 0;
            native.SelectedImageIndex = 0;
            TreeNode user = treeView_Blocks.Nodes.Add(USER_BLOCKS_ROOT, USER_BLOCKS_ROOT);
            user.ImageIndex = 0;
            user.SelectedImageIndex = 0;

            foreach (var kvp in StaticBase.Singleton.BlockList.PathBlockList)
            {
                string[] list = kvp.Key.Split('/', '\\');
                TreeNode currentNode = native;

                //build tree
                for (int i = 0; i < list.Length; i++)
                {
                    //current node must be folder(Tag == null)
                    Debug.Assert(currentNode.Tag == null);

                    if (i + 1 == list.Length)
                    {
                        //add node
                        TreeNode tn = currentNode.Nodes.Add(list[i], list[i]);
                        tn.Tag = kvp.Value;
                        tn.ImageIndex = 4;
                        tn.SelectedImageIndex = 4;
                    }
                    else
                    {
                        //add path
                        TreeNode tn = currentNode.Nodes[list[i]];
                        if (tn == null)
                        {
                            currentNode = currentNode.Nodes.Add(list[i], list[i]);
                            currentNode.ImageIndex = 0;
                            currentNode.SelectedImageIndex = 0;
                        }
                        else
                            currentNode = tn;
                    }
                }
            }

            treeView_Blocks.ExpandAll();
        }
        TabPage CreateOrSelectProjectFileTab(ProjectFile pf)
        {
            ProjectPage pp = m_openedFiles.Find((x) => x.ProjectFile == pf);
            if (pp == null )
            {
                pp = new ProjectPage();
                pp.ProjectFile = pf;
                pp.TabPage = new TabPage(pf.FileName);
                pp.TabPage.ToolTipText = pf.FullPath;
                CreateWorkSpaceForTabPage(pp);

                tabControl_ProjectFiles.TabPages.Add(pp.TabPage);
                m_openedFiles.Add(pp);
            }

            tabControl_ProjectFiles.SelectedTab = pp.TabPage;

            return pp.TabPage;
        }
        void CreateWorkSpaceForTabPage(ProjectPage pp)
        {
            //remove old
            if (pp.Panel != null)
            {
                pp.TabPage.Controls.Remove(pp.Panel);
                pp.Panel.Dispose();
                pp.Panel = null;
            }

            //add new
            pp.Panel = new Panel();
            pp.TabPage.Controls.Add(pp.Panel);
            pp.Panel.Dock = DockStyle.Fill;
            pp.Panel.Paint += new PaintEventHandler(p_Paint);
            pp.WorkSpace = StaticBase.Singleton.Environment.CreateWorkSpace(pp.ProjectFile.BlockManager, pp.Panel);
            pp.WorkSpaceController = new WorkSpaceController(pp.WorkSpace, pp.ProjectFile.BlockManager, pp.Panel, pp.ProjectFile);
        }        
        void RedrawCurrentProjectPage()
        {
            ProjectPage pp = m_openedFiles.Find((x) => x.TabPage == tabControl_ProjectFiles.SelectedTab);
            if (pp != null)
                RedrawProjectPage(pp);
        }
        void RedrawProjectPage(ProjectPage pp)
        {
            pp.WorkSpaceController.Update(true);
        }

        Project m_project;

        readonly List<ProjectPage> m_openedFiles = new List<ProjectPage>();
        readonly object m_logLock = new object();

        #endregion

        private void toolStripButton3_Click_SaveAll(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void treeView_Blocks_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ProjectPage pp = m_openedFiles.Find((x) => x.TabPage == tabControl_ProjectFiles.SelectedTab);

            if (pp != null && e.Node.Tag != null)
            {
                BaseBlock b = pp.ProjectFile.BlockManager.AddBlock(((Type)e.Node.Tag).AssemblyQualifiedName);
                b.Position = new Vector2f(10, 10);
            }

            RedrawCurrentProjectPage();
        }

        private void toolStripButton4_Click_DeleteShader(object sender, EventArgs e)
        {
            if (m_project == null)
                return;

            SimpleEditBox seb = new SimpleEditBox();
            if (treeView_Project.SelectedNode != null)
            {
                ProjectFile pf = treeView_Project.SelectedNode.Tag as ProjectFile;
                if (pf != null)
                {
                    m_project.DeleteShader(pf);
                }
            }

            SaveAll();
            RefreshProjectTree();
        }

        void SaveAll()
        {
            foreach (var op in m_openedFiles)
                op.ProjectFile.Save();

            if(m_project != null)
                m_project.Save();
        }

        private void disabledToolStripMenuItem_Click_AutoRefresh0FPS(object sender, EventArgs e)
        {
            timer_AutoRefresh.Enabled = false;
        }

        private void autoRefresh5fpsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer_AutoRefresh.Interval = 200;
            timer_AutoRefresh.Enabled = true;
        }

        private void autoRefresh10fpsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer_AutoRefresh.Interval = 100;
            timer_AutoRefresh.Enabled = true;
        }

        private void closeWithoutSavingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryCloseProject(false);
        }

        
    }
}
