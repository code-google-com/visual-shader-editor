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
    public partial class VerticesStreamOptionsWindow : OptionsWindow
    {
        public VerticesStreamOptionsWindow()
        {
            InitializeComponent();

            //drop down data
            List<Format> f = new List<Format>(Enum.GetValues(typeof(Format)) as IEnumerable<Format>);
            f.Remove(Format.NONE);
            FormatColumn.DataSource = f;
            FormatColumn.ValueType = typeof(Format);

            List<VerticesStreamSemantic> os = new List<VerticesStreamSemantic>(Enum.GetValues(typeof(VerticesStreamSemantic)) as IEnumerable<VerticesStreamSemantic>);
            os.Remove(VerticesStreamSemantic.NONE);
            SemanticColumn.DataSource = os;
            SemanticColumn.ValueType = typeof(VerticesStreamSemantic);

            List<int> range = new List<int>();
            for (int i = 0; i < SemanticInfo.MAX_SEMANTIC_INDEX_VALUE; i++)
                range.Add(i);
            IndexColumn.DataSource = range;
            IndexColumn.ValueType = typeof(int);

            //default row
            dataGridView_VerticessSream.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView_ColorOutput_DefaultValuesNeeded);

            //clear new row when not selected
            dataGridView_VerticessSream.RowLeave += new DataGridViewCellEventHandler(dataGridView_ColorOutput_RowLeave);
        }

        void dataGridView_ColorOutput_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_VerticessSream.Rows[e.RowIndex].IsNewRow)
            {
                dataGridView_VerticessSream.Rows[e.RowIndex].Cells[OutputNameColumn.Index].Value = null;
                dataGridView_VerticessSream.Rows[e.RowIndex].Cells[FormatColumn.Index].Value = null;
                dataGridView_VerticessSream.Rows[e.RowIndex].Cells[SemanticColumn.Index].Value = null;
                dataGridView_VerticessSream.Rows[e.RowIndex].Cells[IndexColumn.Index].Value = null;
            }
        }

        void dataGridView_ColorOutput_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[OutputNameColumn.Index].Value = "";
            e.Row.Cells[FormatColumn.Index].Value = Format.FLOAT4;
            e.Row.Cells[SemanticColumn.Index].Value = VerticesStreamSemantic.COLOR;
            e.Row.Cells[IndexColumn.Index].Value = 0;
        }

        protected override void LoadFormData()
        {
            base.LoadFormData();

            dataGridView_VerticessSream.Rows.Clear();

            foreach (var o in BaseBlock.Outputs)
            {
                SemanticBlockOutput oo = (SemanticBlockOutput)o;

                int rowIndex = dataGridView_VerticessSream.Rows.Add();
                DataGridViewRow row = dataGridView_VerticessSream.Rows[rowIndex];

                row.Cells[OutputNameColumn.Index].Value = oo.Name;
                row.Cells[FormatColumn.Index].Value = oo.Format;
                row.Cells[SemanticColumn.Index].Value = oo.Semantic;
                row.Cells[IndexColumn.Index].Value = oo.Index;
            }

            dataGridView_VerticessSream.CurrentCell = null;
        }

        protected override void SaveFormData()
        {
            base.SaveFormData();

            //win sometimes forgot do send this
            dataGridView_VerticessSream.EndEdit();

            Dictionary<string, string> names = new Dictionary<string, string>();

            //add new
            foreach (DataGridViewRow row in dataGridView_VerticessSream.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string name = (string)row.Cells[OutputNameColumn.Index].Value;
                names.Add(name, name);

                SemanticBlockOutput o = (SemanticBlockOutput)BaseBlock.FindOutput(name);
                if (o == null)
                {
                    //create new
                    o = ((VerticesStream)BaseBlock).CreateAndAddOutput(
                        (Format)row.Cells[FormatColumn.Index].Value,
                        name,
                        (VerticesStreamSemantic)row.Cells[SemanticColumn.Index].Value,
                        (int)row.Cells[IndexColumn.Index].Value);
                }
                else
                {
                    //assign
                    o.Format = (Format)row.Cells[FormatColumn.Index].Value;
                    o.Semantic = (VerticesStreamSemantic)row.Cells[SemanticColumn.Index].Value;
                    o.Index = (int)row.Cells[IndexColumn.Index].Value;
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
            dataGridView_VerticessSream.EndEdit();

            //unique names
            Dictionary<string, string> names = new Dictionary<string, string>();

            foreach (DataGridViewRow row in dataGridView_VerticessSream.Rows)
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

                if (!BaseBlock.BlockManager.VariableManager.CheckIfNameIsAvailable(name, BaseBlock))
                    return false;
            }

            return true;
        }
    }
}
