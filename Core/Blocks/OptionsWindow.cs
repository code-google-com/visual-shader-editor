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
