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

namespace Core.Blocks.Input
{
    public partial class ConstantOptionsWindow : OptionsWindow
    {
        public ConstantOptionsWindow()
        {
            InitializeComponent();            

            //drop down data
            List<Format> f = new List<Format>(Enum.GetValues(typeof(Format)) as IEnumerable<Format>);
            f.Remove(Format.NONE);
            FormatColumn.DataSource = f;
            FormatColumn.ValueType = typeof(Format);

            //default row
            dataGridView_Values.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView_Values_DefaultValuesNeeded);

            //clear new row when not selected
            dataGridView_Values.RowLeave += new DataGridViewCellEventHandler(dataGridView_Values_RowLeave);
        }

        void dataGridView_Values_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Values.Rows[e.RowIndex].IsNewRow)
            {
                dataGridView_Values.Rows[e.RowIndex].Cells[OutputNameColumn.Index].Value = null;
                dataGridView_Values.Rows[e.RowIndex].Cells[FormatColumn.Index].Value = null;
                dataGridView_Values.Rows[e.RowIndex].Cells[ValueColumn.Index].Value = null;
            }
        }

        void dataGridView_Values_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[OutputNameColumn.Index].Value = "";
            e.Row.Cells[FormatColumn.Index].Value = Format.FLOAT4;
            e.Row.Cells[ValueColumn.Index].Value = "0 0 0 0";
        }

        protected override void LoadFormData()
        {
            base.LoadFormData();

            dataGridView_Values.Rows.Clear();

            foreach (var o in BaseBlock.Outputs)
            {
                ValueBlockOutput oo = (ValueBlockOutput)o;

                int rowIndex = dataGridView_Values.Rows.Add();
                DataGridViewRow row = dataGridView_Values.Rows[rowIndex];

                row.Cells[OutputNameColumn.Index].Value = oo.Name;
                row.Cells[FormatColumn.Index].Value = oo.Format;
                row.Cells[ValueColumn.Index].Value = oo.Value.ToString();
            }

            dataGridView_Values.CurrentCell = null;
        }

        protected override void SaveFormData()
        {
            base.SaveFormData();

            //win sometimes forgot do send this
            dataGridView_Values.EndEdit();

            Dictionary<string, string> names = new Dictionary<string, string>();

            //add new
            foreach (DataGridViewRow row in dataGridView_Values.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string name = (string)row.Cells[OutputNameColumn.Index].Value;
                names.Add(name, name);

                ValueBlockOutput o = (ValueBlockOutput)BaseBlock.FindOutput(name);
                if (o == null)
                {
                    //create new
                    o = ((Constant)BaseBlock).CreateAndAddOutput(
                        (Format)row.Cells[FormatColumn.Index].Value,
                        name,
                        (string)row.Cells[ValueColumn.Index].Value);
                }
                else
                {
                    //assign
                    o.Name = name;
                    o.FromString((string)row.Cells[ValueColumn.Index].Value, (Format)row.Cells[FormatColumn.Index].Value);
                }
            }

            //remoe old
            foreach (var i in new List<BlockOutput>(BaseBlock.Outputs))
                if (!names.ContainsKey(i.Name))
                    BaseBlock.RemoveOutput(i);
        }

        protected override bool Valid()
        {
            //win sometimes forgot do send this
            dataGridView_Values.EndEdit();

            //unique names
            Dictionary<string, string> names = new Dictionary<string, string>();

            foreach (DataGridViewRow row in dataGridView_Values.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string name = (string)row.Cells[OutputNameColumn.Index].Value;
                if (name == null || name == "" || names.ContainsKey(name))
                {
                    MessageBox.Show("empty or duplicated names detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                names.Add(name, name);

                //BaseBlock.BlockManager.VariableManager.CheckIfNameIsAvailable(name);
            }

            return true;
        }
    }
}
