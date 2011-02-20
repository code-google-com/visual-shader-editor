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
    public partial class UserParameterOptionsWindow : OptionsWindow
    {
        public UserParameterOptionsWindow()
        {
            InitializeComponent();

            foreach (var e in Enum.GetValues(typeof(Format)))
                comboBox_Format.Items.Add(e);
        }

        protected override void LoadFormData()
        {
            base.LoadFormData();

            textBox_Name.Text = BaseBlock.Outputs[0].Name;
            comboBox_Format.SelectedItem = BaseBlock.Outputs[0].Format;
            textBox_Value.Text = ((ValueBlockOutput)BaseBlock.Outputs[0]).Value.ToString();
        }

        protected override void SaveFormData()
        {
            base.SaveFormData();

            var output = ((ValueBlockOutput)BaseBlock.Outputs[0]);
            output.Name = textBox_Name.Text;
            output.FromString(textBox_Value.Text, (Format)comboBox_Format.SelectedItem);
        }

        protected override bool Valid()
        {
            return BaseBlock.BlockManager.VariableManager.CheckIfNameIsAvailable(textBox_Name.Text, BaseBlock);
        }
    }
}
