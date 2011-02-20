using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Core.Blocks.Input
{
    public partial class SystemParameterOptionsWindow : OptionsWindow
    {
        public SystemParameterOptionsWindow()
        {
            InitializeComponent();

            foreach(var v in SystemParameter.Parameters)
                comboBox_Variable.Items.Add(v.Name);
        }

        protected override void LoadFormData()
        {
            base.LoadFormData();

            comboBox_Variable.SelectedItem = ((SystemParameter)BaseBlock).CurrentParameterName;
        }

        protected override void SaveFormData()
        {
            ((SystemParameter)BaseBlock).CurrentParameterName = (string)comboBox_Variable.SelectedItem;

            base.SaveFormData();
        }

        protected override bool Valid()
        {
            return true;
        }
    }
}
