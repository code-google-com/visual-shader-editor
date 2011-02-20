namespace Core.Blocks.Texture
{
    partial class SamplerWithTextureOptionsWindow
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
            this.textBox_TextureFileName = new System.Windows.Forms.TextBox();
            this.button_SelectTexture = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_BindName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File:";
            // 
            // textBox_TextureFileName
            // 
            this.textBox_TextureFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TextureFileName.Location = new System.Drawing.Point(16, 30);
            this.textBox_TextureFileName.Name = "textBox_TextureFileName";
            this.textBox_TextureFileName.ReadOnly = true;
            this.textBox_TextureFileName.Size = new System.Drawing.Size(556, 20);
            this.textBox_TextureFileName.TabIndex = 1;
            // 
            // button_SelectTexture
            // 
            this.button_SelectTexture.Location = new System.Drawing.Point(16, 57);
            this.button_SelectTexture.Name = "button_SelectTexture";
            this.button_SelectTexture.Size = new System.Drawing.Size(75, 23);
            this.button_SelectTexture.TabIndex = 2;
            this.button_SelectTexture.Text = "Select Texture";
            this.button_SelectTexture.UseVisualStyleBackColor = true;
            this.button_SelectTexture.Click += new System.EventHandler(this.button_SelectTexture_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "BindName:";
            // 
            // textBox_BindName
            // 
            this.textBox_BindName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_BindName.Location = new System.Drawing.Point(16, 124);
            this.textBox_BindName.Name = "textBox_BindName";
            this.textBox_BindName.Size = new System.Drawing.Size(556, 20);
            this.textBox_BindName.TabIndex = 4;
            // 
            // SamplerWithTextureOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.textBox_BindName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_SelectTexture);
            this.Controls.Add(this.textBox_TextureFileName);
            this.Controls.Add(this.label1);
            this.Name = "SamplerWithTextureOptionsWindow";
            this.Text = "SamplerWithTextureOptionsWindow";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.textBox_TextureFileName, 0);
            this.Controls.SetChildIndex(this.button_SelectTexture, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.textBox_BindName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_TextureFileName;
        private System.Windows.Forms.Button button_SelectTexture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_BindName;
    }
}