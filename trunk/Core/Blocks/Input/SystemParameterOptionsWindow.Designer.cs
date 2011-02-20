namespace Core.Blocks.Input
{
    partial class SystemParameterOptionsWindow
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
            this.comboBox_Variable = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBox_Variable
            // 
            this.comboBox_Variable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Variable.FormattingEnabled = true;
            this.comboBox_Variable.Location = new System.Drawing.Point(36, 33);
            this.comboBox_Variable.Name = "comboBox_Variable";
            this.comboBox_Variable.Size = new System.Drawing.Size(232, 21);
            this.comboBox_Variable.TabIndex = 2;
            // 
            // SystemParameterOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.comboBox_Variable);
            this.Name = "SystemParameterOptionsWindow";
            this.Text = "SystemParameterOptionsWindow";
            this.Controls.SetChildIndex(this.comboBox_Variable, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Variable;
    }
}