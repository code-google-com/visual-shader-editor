using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core.Basic;

namespace Core.Blocks.Output
{
    public partial class ShaderOutputOptionsWindow : OptionsWindow
    {
        public ShaderOutputOptionsWindow()
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
            dataGridView_ColorOutput.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView_ColorOutput_DefaultValuesNeeded);

            //clear new row when not selected
            dataGridView_ColorOutput.RowLeave += new DataGridViewCellEventHandler(dataGridView_ColorOutput_RowLeave);
        }

        void dataGridView_ColorOutput_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_ColorOutput.Rows[e.RowIndex].IsNewRow)
            {
                dataGridView_ColorOutput.Rows[e.RowIndex].Cells[OutputNameColumn.Index].Value = null;
                dataGridView_ColorOutput.Rows[e.RowIndex].Cells[FormatColumn.Index].Value = null;
                dataGridView_ColorOutput.Rows[e.RowIndex].Cells[SemanticColumn.Index].Value = null;
                dataGridView_ColorOutput.Rows[e.RowIndex].Cells[IndexColumn.Index].Value = null;
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

            dataGridView_ColorOutput.Rows.Clear();

            foreach (var i in BaseBlock.Inputs)
            {
                SemanticBlockInput ii = (SemanticBlockInput)i;

                int rowIndex = dataGridView_ColorOutput.Rows.Add();
                DataGridViewRow row = dataGridView_ColorOutput.Rows[rowIndex];

                row.Cells[OutputNameColumn.Index].Value = ii.Name;
                row.Cells[FormatColumn.Index].Value = ii.Format;
                row.Cells[SemanticColumn.Index].Value = ii.Semantic;
                row.Cells[IndexColumn.Index].Value = ii.Index;
            }

            dataGridView_ColorOutput.CurrentCell = null;
        }

        protected override void SaveFormData()
        {
            base.SaveFormData();

            //win sometimes forgot do send this
            dataGridView_ColorOutput.EndEdit();

            Dictionary<string, string> names = new Dictionary<string, string>();

            //add new
            foreach (DataGridViewRow row in dataGridView_ColorOutput.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string name = (string)row.Cells[OutputNameColumn.Index].Value;
                names.Add(name, name);

                SemanticBlockInput i = (SemanticBlockInput)BaseBlock.FindInput(name);
                if (i == null)
                {
                    //create new
                    i = ((ShaderOutput)BaseBlock).CreateAndAddInput(
                        (Format)row.Cells[FormatColumn.Index].Value,
                        name,
                        (VerticesStreamSemantic)row.Cells[SemanticColumn.Index].Value,
                        (int)row.Cells[IndexColumn.Index].Value);
                }
                else
                {
                    //assign
                    i.Format = (Format)row.Cells[FormatColumn.Index].Value;
                    i.Semantic = (VerticesStreamSemantic)row.Cells[SemanticColumn.Index].Value;
                    i.Index = (int)row.Cells[IndexColumn.Index].Value;
                }
            }

            //remoe old
            foreach (var i in new List<BlockInput>(BaseBlock.Inputs))
                if (!names.ContainsKey(i.Name))
                    BaseBlock.RemoveInput(i);
        }

        protected override bool Valid()
        {
            //win sometimes forgot do send this
            dataGridView_ColorOutput.EndEdit();

            //unique names
            Dictionary<string, string> names = new Dictionary<string, string>();

            foreach (DataGridViewRow row in dataGridView_ColorOutput.Rows)
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
