namespace Core.Blocks.Input
{
    partial class ConstantOptionsWindow
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
            this.dataGridView_Values = new System.Windows.Forms.DataGridView();
            this.OutputNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormatColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Values)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_Values
            // 
            this.dataGridView_Values.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Values.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Values.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Values.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OutputNameColumn,
            this.FormatColumn,
            this.ValueColumn});
            this.dataGridView_Values.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_Values.Name = "dataGridView_Values";
            this.dataGridView_Values.Size = new System.Drawing.Size(560, 409);
            this.dataGridView_Values.TabIndex = 1;
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
            // ValueColumn
            // 
            this.ValueColumn.HeaderText = "Value";
            this.ValueColumn.Name = "ValueColumn";
            // 
            // ConstantOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.dataGridView_Values);
            this.Name = "ConstantOptionsWindow";
            this.Text = "ConstantOptionsWindows";
            this.Controls.SetChildIndex(this.dataGridView_Values, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Values)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Values;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutputNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn FormatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;

    }
}