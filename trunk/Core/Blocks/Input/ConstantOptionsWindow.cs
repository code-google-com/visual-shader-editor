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
