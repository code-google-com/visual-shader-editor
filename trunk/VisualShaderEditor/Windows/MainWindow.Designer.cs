namespace VisualShaderEditor.Windows
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.shaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeView_Project = new System.Windows.Forms.TreeView();
            this.imageList_Tree = new System.Windows.Forms.ImageList(this.components);
            this.tabControl_ProjectFiles = new System.Windows.Forms.TabControl();
            this.tabControl_Log = new System.Windows.Forms.TabControl();
            this.tabPage_Main = new System.Windows.Forms.TabPage();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.tabPage_Shader = new System.Windows.Forms.TabPage();
            this.richTextBox_Shader = new System.Windows.Forms.RichTextBox();
            this.treeView_Blocks = new System.Windows.Forms.TreeView();
            this.statusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.openFileDialog_OpenProject = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_SaveProject = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip_ProjectView_Shader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_ProjectView_Folder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addShaderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserBlockToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_ProjectView_Root = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addShaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTemplateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeWithoutSavingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Main = new System.Windows.Forms.ToolStrip();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.disabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoRefresh5fpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoRefresh10fpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_AutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl_Log.SuspendLayout();
            this.tabPage_Main.SuspendLayout();
            this.tabPage_Shader.SuspendLayout();
            this.contextMenuStrip_ProjectView_Shader.SuspendLayout();
            this.contextMenuStrip_ProjectView_Folder.SuspendLayout();
            this.contextMenuStrip_ProjectView_Root.SuspendLayout();
            this.menuStrip_Main.SuspendLayout();
            this.toolStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.toolStripMenuItem2,
            this.shaderToolStripMenuItem,
            this.userBlockToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(126, 6);
            // 
            // shaderToolStripMenuItem
            // 
            this.shaderToolStripMenuItem.Name = "shaderToolStripMenuItem";
            this.shaderToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.shaderToolStripMenuItem.Text = "Shader";
            this.shaderToolStripMenuItem.Click += new System.EventHandler(this.shaderToolStripMenuItem_Click_CreateShader);
            // 
            // userBlockToolStripMenuItem
            // 
            this.userBlockToolStripMenuItem.Name = "userBlockToolStripMenuItem";
            this.userBlockToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.userBlockToolStripMenuItem.Text = "User Block";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Image = global::VisualShaderEditor.Properties.Resources.saveHS;
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.closeProjectToolStripMenuItem.Text = "Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(140, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // profilesToolStripMenuItem
            // 
            this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            this.profilesToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.profilesToolStripMenuItem.Text = "Profiles";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeView_Blocks);
            this.splitContainer1.Size = new System.Drawing.Size(777, 428);
            this.splitContainer1.SplitterDistance = 567;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl_Log);
            this.splitContainer2.Size = new System.Drawing.Size(567, 428);
            this.splitContainer2.SplitterDistance = 318;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeView_Project);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl_ProjectFiles);
            this.splitContainer3.Size = new System.Drawing.Size(567, 318);
            this.splitContainer3.SplitterDistance = 169;
            this.splitContainer3.TabIndex = 0;
            // 
            // treeView_Project
            // 
            this.treeView_Project.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Project.ImageIndex = 0;
            this.treeView_Project.ImageList = this.imageList_Tree;
            this.treeView_Project.Location = new System.Drawing.Point(0, 0);
            this.treeView_Project.Name = "treeView_Project";
            this.treeView_Project.SelectedImageIndex = 0;
            this.treeView_Project.Size = new System.Drawing.Size(169, 318);
            this.treeView_Project.TabIndex = 0;
            this.treeView_Project.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Project_NodeMouseDoubleClick);
            // 
            // imageList_Tree
            // 
            this.imageList_Tree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Tree.ImageStream")));
            this.imageList_Tree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Tree.Images.SetKeyName(0, "Folder_Open.png");
            this.imageList_Tree.Images.SetKeyName(1, "Shader.png");
            this.imageList_Tree.Images.SetKeyName(2, "UserBlock.png");
            this.imageList_Tree.Images.SetKeyName(3, "Template.png");
            this.imageList_Tree.Images.SetKeyName(4, "Gear.png");
            // 
            // tabControl_ProjectFiles
            // 
            this.tabControl_ProjectFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_ProjectFiles.Location = new System.Drawing.Point(0, 0);
            this.tabControl_ProjectFiles.Name = "tabControl_ProjectFiles";
            this.tabControl_ProjectFiles.SelectedIndex = 0;
            this.tabControl_ProjectFiles.ShowToolTips = true;
            this.tabControl_ProjectFiles.Size = new System.Drawing.Size(394, 318);
            this.tabControl_ProjectFiles.TabIndex = 0;
            this.tabControl_ProjectFiles.SelectedIndexChanged += new System.EventHandler(this.tabControl_ProjectFiles_SelectedIndexChanged);
            // 
            // tabControl_Log
            // 
            this.tabControl_Log.Controls.Add(this.tabPage_Main);
            this.tabControl_Log.Controls.Add(this.tabPage_Shader);
            this.tabControl_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Log.ItemSize = new System.Drawing.Size(42, 18);
            this.tabControl_Log.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Log.Name = "tabControl_Log";
            this.tabControl_Log.SelectedIndex = 0;
            this.tabControl_Log.Size = new System.Drawing.Size(567, 106);
            this.tabControl_Log.TabIndex = 0;
            // 
            // tabPage_Main
            // 
            this.tabPage_Main.Controls.Add(this.richTextBox_Log);
            this.tabPage_Main.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Main.Name = "tabPage_Main";
            this.tabPage_Main.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Main.Size = new System.Drawing.Size(559, 80);
            this.tabPage_Main.TabIndex = 1;
            this.tabPage_Main.Text = "Main Log";
            this.tabPage_Main.UseVisualStyleBackColor = true;
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Log.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.ReadOnly = true;
            this.richTextBox_Log.Size = new System.Drawing.Size(553, 74);
            this.richTextBox_Log.TabIndex = 0;
            this.richTextBox_Log.Text = "";
            this.richTextBox_Log.WordWrap = false;
            // 
            // tabPage_Shader
            // 
            this.tabPage_Shader.Controls.Add(this.richTextBox_Shader);
            this.tabPage_Shader.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Shader.Name = "tabPage_Shader";
            this.tabPage_Shader.Size = new System.Drawing.Size(559, 80);
            this.tabPage_Shader.TabIndex = 2;
            this.tabPage_Shader.Text = "Shader Log";
            this.tabPage_Shader.UseVisualStyleBackColor = true;
            // 
            // richTextBox_Shader
            // 
            this.richTextBox_Shader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Shader.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_Shader.Name = "richTextBox_Shader";
            this.richTextBox_Shader.ReadOnly = true;
            this.richTextBox_Shader.Size = new System.Drawing.Size(559, 80);
            this.richTextBox_Shader.TabIndex = 0;
            this.richTextBox_Shader.Text = "";
            // 
            // treeView_Blocks
            // 
            this.treeView_Blocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Blocks.ImageIndex = 0;
            this.treeView_Blocks.ImageList = this.imageList_Tree;
            this.treeView_Blocks.Location = new System.Drawing.Point(0, 0);
            this.treeView_Blocks.Name = "treeView_Blocks";
            this.treeView_Blocks.SelectedImageIndex = 0;
            this.treeView_Blocks.Size = new System.Drawing.Size(206, 428);
            this.treeView_Blocks.TabIndex = 0;
            this.treeView_Blocks.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Blocks_NodeMouseDoubleClick);
            // 
            // statusStrip_Main
            // 
            this.statusStrip_Main.Location = new System.Drawing.Point(0, 483);
            this.statusStrip_Main.Name = "statusStrip_Main";
            this.statusStrip_Main.Size = new System.Drawing.Size(777, 22);
            this.statusStrip_Main.TabIndex = 3;
            this.statusStrip_Main.Text = "statusStrip1";
            // 
            // openFileDialog_OpenProject
            // 
            this.openFileDialog_OpenProject.Filter = "Project|*.vseproj|All files|*.*";
            // 
            // saveFileDialog_SaveProject
            // 
            this.saveFileDialog_SaveProject.DefaultExt = "vseproj";
            this.saveFileDialog_SaveProject.Filter = "Project|*.vseproj|All files|*.*";
            // 
            // contextMenuStrip_ProjectView_Shader
            // 
            this.contextMenuStrip_ProjectView_Shader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.toolStripMenuItem4,
            this.removeToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip_ProjectView_Shader.Name = "contextMenuStrip_ProjectView_Shader";
            this.contextMenuStrip_ProjectView_Shader.Size = new System.Drawing.Size(118, 76);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(114, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::VisualShaderEditor.Properties.Resources.DeleteHS;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::VisualShaderEditor.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // contextMenuStrip_ProjectView_Folder
            // 
            this.contextMenuStrip_ProjectView_Folder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addShaderToolStripMenuItem1,
            this.addUserBlockToolStripMenuItem1,
            this.addTemplateToolStripMenuItem,
            this.addFolderToolStripMenuItem1,
            this.toolStripMenuItem5,
            this.renameToolStripMenuItem1,
            this.toolStripMenuItem6,
            this.removeToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.contextMenuStrip_ProjectView_Folder.Name = "contextMenuStrip_ProjectView_Folder";
            this.contextMenuStrip_ProjectView_Folder.Size = new System.Drawing.Size(155, 170);
            // 
            // addShaderToolStripMenuItem1
            // 
            this.addShaderToolStripMenuItem1.Image = global::VisualShaderEditor.Properties.Resources.Shader;
            this.addShaderToolStripMenuItem1.Name = "addShaderToolStripMenuItem1";
            this.addShaderToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.addShaderToolStripMenuItem1.Text = "Add Shader";
            // 
            // addUserBlockToolStripMenuItem1
            // 
            this.addUserBlockToolStripMenuItem1.Image = global::VisualShaderEditor.Properties.Resources.UserBlock;
            this.addUserBlockToolStripMenuItem1.Name = "addUserBlockToolStripMenuItem1";
            this.addUserBlockToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.addUserBlockToolStripMenuItem1.Text = "Add User Block";
            // 
            // addTemplateToolStripMenuItem
            // 
            this.addTemplateToolStripMenuItem.Image = global::VisualShaderEditor.Properties.Resources.Template;
            this.addTemplateToolStripMenuItem.Name = "addTemplateToolStripMenuItem";
            this.addTemplateToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.addTemplateToolStripMenuItem.Text = "Add Template";
            // 
            // addFolderToolStripMenuItem1
            // 
            this.addFolderToolStripMenuItem1.Image = global::VisualShaderEditor.Properties.Resources.NewFolderHS;
            this.addFolderToolStripMenuItem1.Name = "addFolderToolStripMenuItem1";
            this.addFolderToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.addFolderToolStripMenuItem1.Text = "Add Folder";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(151, 6);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(151, 6);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Image = global::VisualShaderEditor.Properties.Resources.DeleteHS;
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.removeToolStripMenuItem1.Text = "Remove";
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Image = global::VisualShaderEditor.Properties.Resources.delete;
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            // 
            // contextMenuStrip_ProjectView_Root
            // 
            this.contextMenuStrip_ProjectView_Root.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addShaderToolStripMenuItem,
            this.addUserBlockToolStripMenuItem,
            this.addTemplateToolStripMenuItem1,
            this.addFolderToolStripMenuItem});
            this.contextMenuStrip_ProjectView_Root.Name = "contextMenuStrip_ProjectView_Root";
            this.contextMenuStrip_ProjectView_Root.Size = new System.Drawing.Size(155, 92);
            // 
            // addShaderToolStripMenuItem
            // 
            this.addShaderToolStripMenuItem.Image = global::VisualShaderEditor.Properties.Resources.Shader;
            this.addShaderToolStripMenuItem.Name = "addShaderToolStripMenuItem";
            this.addShaderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.addShaderToolStripMenuItem.Text = "Add Shader";
            // 
            // addUserBlockToolStripMenuItem
            // 
            this.addUserBlockToolStripMenuItem.Image = global::VisualShaderEditor.Properties.Resources.UserBlock;
            this.addUserBlockToolStripMenuItem.Name = "addUserBlockToolStripMenuItem";
            this.addUserBlockToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.addUserBlockToolStripMenuItem.Text = "Add User Block";
            // 
            // addTemplateToolStripMenuItem1
            // 
            this.addTemplateToolStripMenuItem1.Image = global::VisualShaderEditor.Properties.Resources.Template;
            this.addTemplateToolStripMenuItem1.Name = "addTemplateToolStripMenuItem1";
            this.addTemplateToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.addTemplateToolStripMenuItem1.Text = "Add Template";
            // 
            // addFolderToolStripMenuItem
            // 
            this.addFolderToolStripMenuItem.Image = global::VisualShaderEditor.Properties.Resources.NewFolderHS;
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.addFolderToolStripMenuItem.Text = "Add Folder";
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Size = new System.Drawing.Size(777, 24);
            this.menuStrip_Main.TabIndex = 4;
            this.menuStrip_Main.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem1,
            this.saveProjectToolStripMenuItem1,
            this.closeProjectToolStripMenuItem1,
            this.closeWithoutSavingToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Image = global::VisualShaderEditor.Properties.Resources.Generic_Document;
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem1
            // 
            this.openProjectToolStripMenuItem1.Image = global::VisualShaderEditor.Properties.Resources.Folder_Open;
            this.openProjectToolStripMenuItem1.Name = "openProjectToolStripMenuItem1";
            this.openProjectToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.openProjectToolStripMenuItem1.Text = "Open Project";
            this.openProjectToolStripMenuItem1.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem1
            // 
            this.saveProjectToolStripMenuItem1.Image = global::VisualShaderEditor.Properties.Resources.saveHS;
            this.saveProjectToolStripMenuItem1.Name = "saveProjectToolStripMenuItem1";
            this.saveProjectToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.saveProjectToolStripMenuItem1.Text = "Save Project";
            this.saveProjectToolStripMenuItem1.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem1
            // 
            this.closeProjectToolStripMenuItem1.Name = "closeProjectToolStripMenuItem1";
            this.closeProjectToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.closeProjectToolStripMenuItem1.Text = "Close Project";
            this.closeProjectToolStripMenuItem1.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // closeWithoutSavingToolStripMenuItem
            // 
            this.closeWithoutSavingToolStripMenuItem.Name = "closeWithoutSavingToolStripMenuItem";
            this.closeWithoutSavingToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.closeWithoutSavingToolStripMenuItem.Text = "Close Without Saving";
            this.closeWithoutSavingToolStripMenuItem.Click += new System.EventHandler(this.closeWithoutSavingToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(184, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            // 
            // toolStrip_Main
            // 
            this.toolStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripButton4,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripDropDownButton1});
            this.toolStrip_Main.Location = new System.Drawing.Point(0, 24);
            this.toolStrip_Main.Name = "toolStrip_Main";
            this.toolStrip_Main.Size = new System.Drawing.Size(777, 25);
            this.toolStrip_Main.TabIndex = 5;
            this.toolStrip_Main.Text = "toolStrip1";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::VisualShaderEditor.Properties.Resources.Shader;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Create New Shader";
            this.toolStripButton5.Click += new System.EventHandler(this.shaderToolStripMenuItem_Click_CreateShader);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::VisualShaderEditor.Properties.Resources.delete;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Delete Selected Shader";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click_DeleteShader);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::VisualShaderEditor.Properties.Resources.SaveAllHS;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Save All";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click_SaveAll);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Refresh";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click_Refresh);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disabledToolStripMenuItem,
            this.autoRefresh5fpsToolStripMenuItem,
            this.autoRefresh10fpsToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "Auto Refresh";
            // 
            // disabledToolStripMenuItem
            // 
            this.disabledToolStripMenuItem.Name = "disabledToolStripMenuItem";
            this.disabledToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.disabledToolStripMenuItem.Text = "Auto Refresh: 0fps";
            this.disabledToolStripMenuItem.Click += new System.EventHandler(this.disabledToolStripMenuItem_Click_AutoRefresh0FPS);
            // 
            // autoRefresh5fpsToolStripMenuItem
            // 
            this.autoRefresh5fpsToolStripMenuItem.Name = "autoRefresh5fpsToolStripMenuItem";
            this.autoRefresh5fpsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.autoRefresh5fpsToolStripMenuItem.Text = "Auto Refresh: 5fps";
            this.autoRefresh5fpsToolStripMenuItem.Click += new System.EventHandler(this.autoRefresh5fpsToolStripMenuItem_Click);
            // 
            // autoRefresh10fpsToolStripMenuItem
            // 
            this.autoRefresh10fpsToolStripMenuItem.Name = "autoRefresh10fpsToolStripMenuItem";
            this.autoRefresh10fpsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.autoRefresh10fpsToolStripMenuItem.Text = "Auto Refresh: 10fps";
            this.autoRefresh10fpsToolStripMenuItem.Click += new System.EventHandler(this.autoRefresh10fpsToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 505);
            this.Controls.Add(this.toolStrip_Main);
            this.Controls.Add(this.statusStrip_Main);
            this.Controls.Add(this.menuStrip_Main);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.tabControl_Log.ResumeLayout(false);
            this.tabPage_Main.ResumeLayout(false);
            this.tabPage_Shader.ResumeLayout(false);
            this.contextMenuStrip_ProjectView_Shader.ResumeLayout(false);
            this.contextMenuStrip_ProjectView_Folder.ResumeLayout(false);
            this.contextMenuStrip_ProjectView_Root.ResumeLayout(false);
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.toolStrip_Main.ResumeLayout(false);
            this.toolStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabControl tabControl_ProjectFiles;
        private System.Windows.Forms.TabControl tabControl_Log;
        private System.Windows.Forms.TabPage tabPage_Main;
        private System.Windows.Forms.TreeView treeView_Project;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem profilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog_OpenProject;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_SaveProject;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem shaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userBlockToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.TabPage tabPage_Shader;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_ProjectView_Shader;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_ProjectView_Folder;
        private System.Windows.Forms.ToolStripMenuItem addShaderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addUserBlockToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_ProjectView_Root;
        private System.Windows.Forms.ToolStripMenuItem addShaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addUserBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList_Tree;
        private System.Windows.Forms.ToolStripMenuItem addTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTemplateToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.RichTextBox richTextBox_Shader;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem disabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoRefresh5fpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoRefresh10fpsToolStripMenuItem;
        private System.Windows.Forms.Timer timer_AutoRefresh;
        private System.Windows.Forms.ToolStripMenuItem closeWithoutSavingToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView_Blocks;
    }
}