namespace Core.Blocks.Math
{
    partial class VectorMixOptionsWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_OutputType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_XInputType = new System.Windows.Forms.ComboBox();
            this.comboBox_YInputType = new System.Windows.Forms.ComboBox();
            this.comboBox_ZInputType = new System.Windows.Forms.ComboBox();
            this.comboBox_WInputType = new System.Windows.Forms.ComboBox();
            this.textBox_OutputValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_YInputMember = new System.Windows.Forms.ComboBox();
            this.comboBox_ZInputMember = new System.Windows.Forms.ComboBox();
            this.comboBox_XInputMember = new System.Windows.Forms.ComboBox();
            this.comboBox_WInputMember = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_W = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Z = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_X = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_InputCount = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_W)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output Type:";
            // 
            // comboBox_OutputType
            // 
            this.comboBox_OutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_OutputType.FormattingEnabled = true;
            this.comboBox_OutputType.Location = new System.Drawing.Point(90, 45);
            this.comboBox_OutputType.Name = "comboBox_OutputType";
            this.comboBox_OutputType.Size = new System.Drawing.Size(121, 21);
            this.comboBox_OutputType.TabIndex = 3;
            this.comboBox_OutputType.SelectedIndexChanged += new System.EventHandler(this.comboBox_OutputType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Z:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(386, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "W:";
            // 
            // comboBox_XInputType
            // 
            this.comboBox_XInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_XInputType.FormattingEnabled = true;
            this.comboBox_XInputType.Location = new System.Drawing.Point(6, 38);
            this.comboBox_XInputType.Name = "comboBox_XInputType";
            this.comboBox_XInputType.Size = new System.Drawing.Size(121, 21);
            this.comboBox_XInputType.TabIndex = 8;
            this.comboBox_XInputType.SelectedIndexChanged += new System.EventHandler(this.comboBox_XInputType_SelectedIndexChanged);
            // 
            // comboBox_YInputType
            // 
            this.comboBox_YInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_YInputType.FormattingEnabled = true;
            this.comboBox_YInputType.Location = new System.Drawing.Point(133, 38);
            this.comboBox_YInputType.Name = "comboBox_YInputType";
            this.comboBox_YInputType.Size = new System.Drawing.Size(121, 21);
            this.comboBox_YInputType.TabIndex = 9;
            this.comboBox_YInputType.SelectedIndexChanged += new System.EventHandler(this.comboBox_YInputType_SelectedIndexChanged);
            // 
            // comboBox_ZInputType
            // 
            this.comboBox_ZInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ZInputType.FormattingEnabled = true;
            this.comboBox_ZInputType.Location = new System.Drawing.Point(260, 38);
            this.comboBox_ZInputType.Name = "comboBox_ZInputType";
            this.comboBox_ZInputType.Size = new System.Drawing.Size(121, 21);
            this.comboBox_ZInputType.TabIndex = 10;
            this.comboBox_ZInputType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ZInputType_SelectedIndexChanged);
            // 
            // comboBox_WInputType
            // 
            this.comboBox_WInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_WInputType.FormattingEnabled = true;
            this.comboBox_WInputType.Location = new System.Drawing.Point(387, 38);
            this.comboBox_WInputType.Name = "comboBox_WInputType";
            this.comboBox_WInputType.Size = new System.Drawing.Size(121, 21);
            this.comboBox_WInputType.TabIndex = 11;
            this.comboBox_WInputType.SelectedIndexChanged += new System.EventHandler(this.comboBox_WInputType_SelectedIndexChanged);
            // 
            // textBox_OutputValue
            // 
            this.textBox_OutputValue.Location = new System.Drawing.Point(272, 46);
            this.textBox_OutputValue.Name = "textBox_OutputValue";
            this.textBox_OutputValue.ReadOnly = true;
            this.textBox_OutputValue.Size = new System.Drawing.Size(248, 20);
            this.textBox_OutputValue.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Output:";
            // 
            // comboBox_YInputMember
            // 
            this.comboBox_YInputMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_YInputMember.FormattingEnabled = true;
            this.comboBox_YInputMember.Location = new System.Drawing.Point(133, 85);
            this.comboBox_YInputMember.Name = "comboBox_YInputMember";
            this.comboBox_YInputMember.Size = new System.Drawing.Size(121, 21);
            this.comboBox_YInputMember.TabIndex = 9;
            // 
            // comboBox_ZInputMember
            // 
            this.comboBox_ZInputMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ZInputMember.FormattingEnabled = true;
            this.comboBox_ZInputMember.Location = new System.Drawing.Point(260, 85);
            this.comboBox_ZInputMember.Name = "comboBox_ZInputMember";
            this.comboBox_ZInputMember.Size = new System.Drawing.Size(121, 21);
            this.comboBox_ZInputMember.TabIndex = 10;
            // 
            // comboBox_XInputMember
            // 
            this.comboBox_XInputMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_XInputMember.FormattingEnabled = true;
            this.comboBox_XInputMember.Location = new System.Drawing.Point(6, 85);
            this.comboBox_XInputMember.Name = "comboBox_XInputMember";
            this.comboBox_XInputMember.Size = new System.Drawing.Size(121, 21);
            this.comboBox_XInputMember.TabIndex = 8;
            // 
            // comboBox_WInputMember
            // 
            this.comboBox_WInputMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_WInputMember.FormattingEnabled = true;
            this.comboBox_WInputMember.Location = new System.Drawing.Point(387, 85);
            this.comboBox_WInputMember.Name = "comboBox_WInputMember";
            this.comboBox_WInputMember.Size = new System.Drawing.Size(121, 21);
            this.comboBox_WInputMember.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown_W);
            this.groupBox1.Controls.Add(this.numericUpDown_Z);
            this.groupBox1.Controls.Add(this.numericUpDown_Y);
            this.groupBox1.Controls.Add(this.numericUpDown_X);
            this.groupBox1.Controls.Add(this.comboBox_XInputType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBox_WInputType);
            this.groupBox1.Controls.Add(this.comboBox_YInputMember);
            this.groupBox1.Controls.Add(this.comboBox_YInputType);
            this.groupBox1.Controls.Add(this.comboBox_WInputMember);
            this.groupBox1.Controls.Add(this.comboBox_ZInputMember);
            this.groupBox1.Controls.Add(this.comboBox_ZInputType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox_XInputMember);
            this.groupBox1.Location = new System.Drawing.Point(12, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 155);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output Members";
            // 
            // numericUpDown_W
            // 
            this.numericUpDown_W.DecimalPlaces = 4;
            this.numericUpDown_W.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_W.Location = new System.Drawing.Point(387, 112);
            this.numericUpDown_W.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_W.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_W.Name = "numericUpDown_W";
            this.numericUpDown_W.Size = new System.Drawing.Size(121, 20);
            this.numericUpDown_W.TabIndex = 21;
            // 
            // numericUpDown_Z
            // 
            this.numericUpDown_Z.DecimalPlaces = 4;
            this.numericUpDown_Z.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Z.Location = new System.Drawing.Point(260, 112);
            this.numericUpDown_Z.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Z.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Z.Name = "numericUpDown_Z";
            this.numericUpDown_Z.Size = new System.Drawing.Size(121, 20);
            this.numericUpDown_Z.TabIndex = 21;
            // 
            // numericUpDown_Y
            // 
            this.numericUpDown_Y.DecimalPlaces = 4;
            this.numericUpDown_Y.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Y.Location = new System.Drawing.Point(133, 112);
            this.numericUpDown_Y.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Y.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Y.Name = "numericUpDown_Y";
            this.numericUpDown_Y.Size = new System.Drawing.Size(121, 20);
            this.numericUpDown_Y.TabIndex = 21;
            // 
            // numericUpDown_X
            // 
            this.numericUpDown_X.DecimalPlaces = 4;
            this.numericUpDown_X.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_X.Location = new System.Drawing.Point(6, 112);
            this.numericUpDown_X.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_X.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_X.Name = "numericUpDown_X";
            this.numericUpDown_X.Size = new System.Drawing.Size(121, 20);
            this.numericUpDown_X.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Input Count:";
            // 
            // comboBox_InputCount
            // 
            this.comboBox_InputCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_InputCount.FormattingEnabled = true;
            this.comboBox_InputCount.Location = new System.Drawing.Point(90, 73);
            this.comboBox_InputCount.Name = "comboBox_InputCount";
            this.comboBox_InputCount.Size = new System.Drawing.Size(121, 21);
            this.comboBox_InputCount.TabIndex = 20;
            this.comboBox_InputCount.SelectedIndexChanged += new System.EventHandler(this.comboBox_InputCount_SelectedIndexChanged);
            // 
            // VectorMixOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.comboBox_InputCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_OutputValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_OutputType);
            this.Name = "VectorMixOptionsWindow";
            this.Text = "VectorMixOptionsWindow";
            this.Controls.SetChildIndex(this.comboBox_OutputType, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.textBox_OutputValue, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.comboBox_InputCount, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_W)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_OutputType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_XInputType;
        private System.Windows.Forms.ComboBox comboBox_YInputType;
        private System.Windows.Forms.ComboBox comboBox_ZInputType;
        private System.Windows.Forms.ComboBox comboBox_WInputType;
        private System.Windows.Forms.TextBox textBox_OutputValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_YInputMember;
        private System.Windows.Forms.ComboBox comboBox_ZInputMember;
        private System.Windows.Forms.ComboBox comboBox_XInputMember;
        private System.Windows.Forms.ComboBox comboBox_WInputMember;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_InputCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_W;
        private System.Windows.Forms.NumericUpDown numericUpDown_Z;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_X;
    }
}