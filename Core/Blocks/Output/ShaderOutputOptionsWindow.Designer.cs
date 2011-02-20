namespace Core.Blocks.Output
{
    partial class ShaderOutputOptionsWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_ColorOutput = new System.Windows.Forms.DataGridView();
            this.OutputNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormatColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SemanticColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IndexColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ColorOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_ColorOutput
            // 
            this.dataGridView_ColorOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_ColorOutput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_ColorOutput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_ColorOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ColorOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OutputNameColumn,
            this.FormatColumn,
            this.SemanticColumn,
            this.IndexColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_ColorOutput.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_ColorOutput.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_ColorOutput.Name = "dataGridView_ColorOutput";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_ColorOutput.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_ColorOutput.Size = new System.Drawing.Size(560, 409);
            this.dataGridView_ColorOutput.TabIndex = 1;
            // 
            // OutputNameColumn
            // 
            this.OutputNameColumn.HeaderText = "Output Name";
            this.OutputNameColumn.Name = "OutputNameColumn";
            // 
            // FormatColumn
            // 
            this.FormatColumn.HeaderText = "Format";
            this.FormatColumn.Name = "FormatColumn";
            // 
            // SemanticColumn
            // 
            this.SemanticColumn.HeaderText = "Semantic";
            this.SemanticColumn.Name = "SemanticColumn";
            // 
            // IndexColumn
            // 
            this.IndexColumn.HeaderText = "Index";
            this.IndexColumn.Name = "IndexColumn";
            // 
            // ShaderOutputOptionsWindow
            // 
            this.AcceptButton = null;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = null;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.dataGridView_ColorOutput);
            this.Name = "ShaderOutputOptionsWindow";
            this.Text = "ShaderOutputOptionsWindow";
            this.Controls.SetChildIndex(this.dataGridView_ColorOutput, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ColorOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_ColorOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutputNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn FormatColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn SemanticColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn IndexColumn;
    }
}