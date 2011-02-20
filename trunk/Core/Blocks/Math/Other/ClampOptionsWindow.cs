using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core.Basic;
using Core.Helper;

namespace Core.Blocks.Math.Other
{
    public partial class ClampOptionsWindow : OptionsWindow
    {
        public ClampOptionsWindow()
        {
            InitializeComponent();

            comboBox_MinConstFormat.Items.AddRange(FormatHelper.VectorFormatsOnly());
            comboBox_MaxConstFormat.Items.AddRange(FormatHelper.VectorFormatsOnly());
        }

        protected override void LoadFormData()
        {
            base.LoadFormData();

            Clamp c = ((Clamp)BaseBlock);

            object min = c.Min;
            object max = c.Max;

            if (min != null)
            {
                radioButton_MinConst.Checked = true;
                comboBox_MinConstFormat.SelectedItem = c.MinConstFormat;
                textBox_MinConstValue.Text = min.ToString();
            }
            else
            {
                radioButton_MinInput.Checked = true;
                comboBox_MinConstFormat.SelectedItem = Format.FLOAT;
            }

            if (max != null)
            {
                radioButton_MaxConst.Checked = true;
                comboBox_MaxConstFormat.SelectedItem = c.MaxConstFormat;
                textBox_MaxConstValue.Text = max.ToString();
            }
            else
            {
                radioButton_MaxInput.Checked = true;
                comboBox_MaxConstFormat.SelectedItem = Format.FLOAT;
            }

            radioButton_CheckedChanged(null, null);
        }

        protected override void SaveFormData()
        {
            base.SaveFormData();

            Clamp c = ((Clamp)BaseBlock);

            if (radioButton_MinConst.Checked)
                c.Min = VectorHelper.FromString(textBox_MinConstValue.Text, (Format)comboBox_MinConstFormat.SelectedItem);
            else
                c.Min = null;

            if (radioButton_MaxConst.Checked)
                c.Max = VectorHelper.FromString(textBox_MaxConstValue.Text, (Format)comboBox_MaxConstFormat.SelectedItem);
            else
                c.Max = null;
        }

        protected override bool Valid()
        {
            try
            {
                if (radioButton_MinConst.Checked)
                {
                    object o = VectorHelper.FromString(textBox_MinConstValue.Text, (Format)comboBox_MinConstFormat.SelectedItem);
                }

                if (radioButton_MaxConst.Checked)
                {
                    object o = VectorHelper.FromString(textBox_MaxConstValue.Text, (Format)comboBox_MaxConstFormat.SelectedItem);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_MinInput.Checked)
            {
                comboBox_MinConstFormat.Visible = false;
                textBox_MinConstValue.Visible = false;
            }
            else
            {
                comboBox_MinConstFormat.Visible = true;
                textBox_MinConstValue.Visible = true;
            }

            if (radioButton_MaxInput.Checked)
            {
                comboBox_MaxConstFormat.Visible = false;
                textBox_MaxConstValue.Visible = false;
            }
            else
            {
                comboBox_MaxConstFormat.Visible = true;
                textBox_MaxConstValue.Visible = true;
            }
        }
    }
}
