namespace Core.Blocks.Math.Other
{
    partial class ClampOptionsWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_MinConstValue = new System.Windows.Forms.TextBox();
            this.comboBox_MinConstFormat = new System.Windows.Forms.ComboBox();
            this.radioButton_MinConst = new System.Windows.Forms.RadioButton();
            this.radioButton_MinInput = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_MaxConstValue = new System.Windows.Forms.TextBox();
            this.comboBox_MaxConstFormat = new System.Windows.Forms.ComboBox();
            this.radioButton_MaxConst = new System.Windows.Forms.RadioButton();
            this.radioButton_MaxInput = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_MinConstValue);
            this.groupBox1.Controls.Add(this.comboBox_MinConstFormat);
            this.groupBox1.Controls.Add(this.radioButton_MinConst);
            this.groupBox1.Controls.Add(this.radioButton_MinInput);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 72);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Min";
            // 
            // textBox_MinConstValue
            // 
            this.textBox_MinConstValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_MinConstValue.Location = new System.Drawing.Point(204, 43);
            this.textBox_MinConstValue.Name = "textBox_MinConstValue";
            this.textBox_MinConstValue.Size = new System.Drawing.Size(349, 20);
            this.textBox_MinConstValue.TabIndex = 3;
            // 
            // comboBox_MinConstFormat
            // 
            this.comboBox_MinConstFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MinConstFormat.FormattingEnabled = true;
            this.comboBox_MinConstFormat.Location = new System.Drawing.Point(77, 43);
            this.comboBox_MinConstFormat.Name = "comboBox_MinConstFormat";
            this.comboBox_MinConstFormat.Size = new System.Drawing.Size(121, 21);
            this.comboBox_MinConstFormat.TabIndex = 2;
            // 
            // radioButton_MinConst
            // 
            this.radioButton_MinConst.AutoSize = true;
            this.radioButton_MinConst.Location = new System.Drawing.Point(7, 44);
            this.radioButton_MinConst.Name = "radioButton_MinConst";
            this.radioButton_MinConst.Size = new System.Drawing.Size(52, 17);
            this.radioButton_MinConst.TabIndex = 1;
            this.radioButton_MinConst.Text = "Const";
            this.radioButton_MinConst.UseVisualStyleBackColor = true;
            this.radioButton_MinConst.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_MinInput
            // 
            this.radioButton_MinInput.AutoSize = true;
            this.radioButton_MinInput.Checked = true;
            this.radioButton_MinInput.Location = new System.Drawing.Point(7, 20);
            this.radioButton_MinInput.Name = "radioButton_MinInput";
            this.radioButton_MinInput.Size = new System.Drawing.Size(49, 17);
            this.radioButton_MinInput.TabIndex = 0;
            this.radioButton_MinInput.TabStop = true;
            this.radioButton_MinInput.Text = "Input";
            this.radioButton_MinInput.UseVisualStyleBackColor = true;
            this.radioButton_MinInput.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_MaxConstValue);
            this.groupBox2.Controls.Add(this.comboBox_MaxConstFormat);
            this.groupBox2.Controls.Add(this.radioButton_MaxConst);
            this.groupBox2.Controls.Add(this.radioButton_MaxInput);
            this.groupBox2.Location = new System.Drawing.Point(13, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(559, 72);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Max";
            // 
            // textBox_MaxConstValue
            // 
            this.textBox_MaxConstValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_MaxConstValue.Location = new System.Drawing.Point(204, 43);
            this.textBox_MaxConstValue.Name = "textBox_MaxConstValue";
            this.textBox_MaxConstValue.Size = new System.Drawing.Size(349, 20);
            this.textBox_MaxConstValue.TabIndex = 3;
            // 
            // comboBox_MaxConstFormat
            // 
            this.comboBox_MaxConstFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MaxConstFormat.FormattingEnabled = true;
            this.comboBox_MaxConstFormat.Location = new System.Drawing.Point(77, 43);
            this.comboBox_MaxConstFormat.Name = "comboBox_MaxConstFormat";
            this.comboBox_MaxConstFormat.Size = new System.Drawing.Size(121, 21);
            this.comboBox_MaxConstFormat.TabIndex = 2;
            // 
            // radioButton_MaxConst
            // 
            this.radioButton_MaxConst.AutoSize = true;
            this.radioButton_MaxConst.Location = new System.Drawing.Point(7, 44);
            this.radioButton_MaxConst.Name = "radioButton_MaxConst";
            this.radioButton_MaxConst.Size = new System.Drawing.Size(52, 17);
            this.radioButton_MaxConst.TabIndex = 1;
            this.radioButton_MaxConst.Text = "Const";
            this.radioButton_MaxConst.UseVisualStyleBackColor = true;
            this.radioButton_MaxConst.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_MaxInput
            // 
            this.radioButton_MaxInput.AutoSize = true;
            this.radioButton_MaxInput.Checked = true;
            this.radioButton_MaxInput.Location = new System.Drawing.Point(7, 20);
            this.radioButton_MaxInput.Name = "radioButton_MaxInput";
            this.radioButton_MaxInput.Size = new System.Drawing.Size(49, 17);
            this.radioButton_MaxInput.TabIndex = 0;
            this.radioButton_MaxInput.TabStop = true;
            this.radioButton_MaxInput.Text = "Input";
            this.radioButton_MaxInput.UseVisualStyleBackColor = true;
            this.radioButton_MaxInput.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // ClampOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "ClampOptionsWindow";
            this.Text = "ClampOptionsWindow";
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_MinConst;
        private System.Windows.Forms.RadioButton radioButton_MinInput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_MaxConst;
        private System.Windows.Forms.RadioButton radioButton_MaxInput;
        private System.Windows.Forms.TextBox textBox_MinConstValue;
        private System.Windows.Forms.ComboBox comboBox_MinConstFormat;
        private System.Windows.Forms.TextBox textBox_MaxConstValue;
        private System.Windows.Forms.ComboBox comboBox_MaxConstFormat;
    }
}