using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core.Basic;

namespace Core.Blocks
{
    public partial class OptionsWindow : Form, IOptionsWindow
    {
        public OptionsWindow()
        {
            InitializeComponent();

            m_okPoint = this.button_OK.Location - ClientSize;
            m_cancelPoint = this.button_Cancel.Location - ClientSize;
        }

        protected override void OnShown(EventArgs e)
        {
            //children may chave diferent size
            this.button_OK.Location = m_okPoint + ClientSize;
            this.button_Cancel.Location = m_cancelPoint + ClientSize;

            LoadFormData();
            base.OnShown(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult != DialogResult.Abort)
            {
                if (!Valid())
                {
                    MessageBox.Show("Error: probably some names are already in use by other blocks, or values don't mach selected format");
                    e.Cancel = true;
                }
                else
                    SaveFormData();
            }

            base.OnClosing(e);
        }

        protected virtual void LoadFormData()
        {
            //in editor BaseBlock == null
            if(BaseBlock != null)
                textBox_Comment.Text = BaseBlock.BlockComment;
        }
        protected virtual void SaveFormData()
        {
            //in editor BaseBlock == null
            if (BaseBlock != null)
                BaseBlock.BlockComment = textBox_Comment.Text;
        }
        protected virtual bool Valid()
        {
            return true;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public BaseBlock BaseBlock
        {
            get { return m_baseBlock; }
        }

        #region IOptionsWindow Members

        public void ShowOptions()
        {
            ShowDialog();
        }
        public void SetBlock(BaseBlock bb)
        {
            m_baseBlock = bb;
        }

        #endregion

        #region private

        Point m_okPoint;
        Point m_cancelPoint;
        BaseBlock m_baseBlock;

        #endregion
    }
}
