using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core.Basic;
using Core.Blocks.Math.Vector;

namespace Core.Blocks.Math
{
    public partial class VectorMixOptionsWindow : OptionsWindow
    {
        static readonly string CONST_INPUT_NAME = "Const";
        static readonly string A_INPUT_NAME = "A";
        static readonly string B_INPUT_NAME = "B";
        static readonly string C_INPUT_NAME = "C";
        static readonly string D_INPUT_NAME = "D";

        public VectorMixOptionsWindow()
        {
            InitializeComponent();

            comboBox_OutputType.Items.AddRange(new object[] { Format.FLOAT, Format.FLOAT2, Format.FLOAT3, Format.FLOAT4 });
            comboBox_InputCount.Items.AddRange(new object[] { 1, 2, 3, 4 });

            object[] m = new object[] { VectorMembers.X, VectorMembers.Y, VectorMembers.Z, VectorMembers.W };

            comboBox_XInputMember.Items.AddRange(m);
            comboBox_XInputMember.SelectedIndex = 0;
            comboBox_YInputMember.Items.AddRange(m);
            comboBox_XInputMember.SelectedIndex = 0;
            comboBox_ZInputMember.Items.AddRange(m);
            comboBox_XInputMember.SelectedIndex = 0;
            comboBox_WInputMember.Items.AddRange(m);
            comboBox_XInputMember.SelectedIndex = 0;
        }

        protected override void LoadFormData()
        {
            base.LoadFormData();

            comboBox_OutputType.SelectedItem = m_owner.Outputs[0].Format;
            comboBox_InputCount.SelectedItem = m_owner.InputCount;

            //x
            {
                var x = m_owner.GetDataInput(VectorMembers.X);
                if (x.IsConst)
                {
                    comboBox_XInputType.SelectedItem = CONST_INPUT_NAME;
                    numericUpDown_X.Value = (decimal)x.ConstValue.X;
                }
                else
                {
                    comboBox_XInputType.SelectedItem = x.BlockInput.Name;
                    comboBox_XInputMember.SelectedItem = x.Member;
                }
            }

            //y
            {
                var y = m_owner.GetDataInput(VectorMembers.Y);
                if (y.IsConst)
                {
                    comboBox_YInputType.SelectedItem = CONST_INPUT_NAME;
                    numericUpDown_Y.Value = (decimal)y.ConstValue.X;
                }
                else
                {
                    comboBox_YInputType.SelectedItem = y.BlockInput.Name;
                    comboBox_YInputMember.SelectedItem = y.Member;
                }
            }

            //z
            {
                var z = m_owner.GetDataInput(VectorMembers.Z);
                if (z.IsConst)
                {
                    comboBox_ZInputType.SelectedItem = CONST_INPUT_NAME;
                    numericUpDown_Z.Value = (decimal)z.ConstValue.X;
                }
                else
                {
                    comboBox_ZInputType.SelectedItem = z.BlockInput.Name;
                    comboBox_ZInputMember.SelectedItem = z.Member;
                }
            }

            //w
            {
                var w = m_owner.GetDataInput(VectorMembers.W);
                if (w.IsConst)
                {
                    comboBox_WInputType.SelectedItem = CONST_INPUT_NAME;
                    numericUpDown_W.Value = (decimal)w.ConstValue.X;
                }
                else
                {
                    comboBox_WInputType.SelectedItem = w.BlockInput.Name;
                    comboBox_WInputMember.SelectedItem = w.Member;
                }
            }
        }

        protected override void SaveFormData()
        {
            base.SaveFormData();

            m_owner.SetOutputFormat((Format)comboBox_OutputType.SelectedItem);
            m_owner.InputCount = (int)comboBox_InputCount.SelectedItem;

            //x
            if ((string)comboBox_XInputType.SelectedItem == CONST_INPUT_NAME)
                m_owner.SetDataInput(VectorMembers.X, new VectorMix.MemberInputSelector((float)numericUpDown_X.Value));
            else
                m_owner.SetDataInput(VectorMembers.X, new VectorMix.MemberInputSelector(m_owner.FindInput((string)comboBox_XInputType.SelectedItem), (VectorMembers)comboBox_XInputMember.SelectedItem));

            //y
            if ((string)comboBox_YInputType.SelectedItem == CONST_INPUT_NAME)
                m_owner.SetDataInput(VectorMembers.Y, new VectorMix.MemberInputSelector((float)numericUpDown_Y.Value));
            else
                m_owner.SetDataInput(VectorMembers.Y, new VectorMix.MemberInputSelector(m_owner.FindInput((string)comboBox_YInputType.SelectedItem), (VectorMembers)comboBox_YInputMember.SelectedItem));

            //z
            if ((string)comboBox_ZInputType.SelectedItem == CONST_INPUT_NAME)
                m_owner.SetDataInput(VectorMembers.Z, new VectorMix.MemberInputSelector((float)numericUpDown_Z.Value));
            else
                m_owner.SetDataInput(VectorMembers.Z, new VectorMix.MemberInputSelector(m_owner.FindInput((string)comboBox_ZInputType.SelectedItem), (VectorMembers)comboBox_ZInputMember.SelectedItem));

            //w
            if ((string)comboBox_WInputType.SelectedItem == CONST_INPUT_NAME)
                m_owner.SetDataInput(VectorMembers.W, new VectorMix.MemberInputSelector((float)numericUpDown_W.Value));
            else
                m_owner.SetDataInput(VectorMembers.W, new VectorMix.MemberInputSelector(m_owner.FindInput((string)comboBox_WInputType.SelectedItem), (VectorMembers)comboBox_WInputMember.SelectedItem));
        }

        protected override bool Valid()
        {
            return true;
        }

        private void comboBox_OutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisability();
        }

        private void comboBox_InputCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetInputCount((int)comboBox_InputCount.SelectedItem);
        }

        void SetInputCount(int ic)
        {
            object[] it;
            switch(ic){
                case 1: it = new object[] { CONST_INPUT_NAME, A_INPUT_NAME }; break;
                case 2: it = new object[] { CONST_INPUT_NAME, A_INPUT_NAME, B_INPUT_NAME }; break;
                case 3: it = new object[] { CONST_INPUT_NAME, A_INPUT_NAME, B_INPUT_NAME, C_INPUT_NAME }; break;
                case 4: it = new object[] { CONST_INPUT_NAME, A_INPUT_NAME, B_INPUT_NAME, C_INPUT_NAME, D_INPUT_NAME }; break;
                default :
                    throw new ArgumentException("ic must be: 1 <= ic <= 4");
            }

            //save selected index
            int xs = comboBox_XInputType.SelectedIndex;
            int ys = comboBox_YInputType.SelectedIndex;
            int zs = comboBox_ZInputType.SelectedIndex;
            int ws = comboBox_WInputType.SelectedIndex;

            comboBox_XInputType.Items.Clear();
            comboBox_YInputType.Items.Clear();
            comboBox_ZInputType.Items.Clear();
            comboBox_WInputType.Items.Clear();

            comboBox_XInputType.Items.AddRange(it);
            comboBox_YInputType.Items.AddRange(it);
            comboBox_ZInputType.Items.AddRange(it);
            comboBox_WInputType.Items.AddRange(it);

            //restore selected index
            comboBox_XInputType.SelectedIndex = xs <= ic ? xs : 0;
            comboBox_YInputType.SelectedIndex = ys <= ic ? ys : 0;
            comboBox_ZInputType.SelectedIndex = zs <= ic ? zs : 0;
            comboBox_WInputType.SelectedIndex = ws <= ic ? ws : 0;
        }

        private void comboBox_XInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisability();
        }

        private void comboBox_YInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisability();
        }

        private void comboBox_ZInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisability();
        }

        private void comboBox_WInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisability();
        }

        public void UpdateVisability()
        {
            int size = FormatHelper.Size((Format)comboBox_OutputType.SelectedItem);

            //w
            if (size < 4)
            {
                comboBox_WInputType.Hide();
                comboBox_WInputMember.Hide();
                numericUpDown_W.Hide();
            }
            else
            {
                comboBox_WInputType.Show();

                if ((string)comboBox_WInputType.SelectedItem != CONST_INPUT_NAME)
                {
                    comboBox_WInputMember.Show();
                    numericUpDown_W.Hide();
                }
                else
                {
                    comboBox_WInputMember.Hide();
                    numericUpDown_W.Show();
                }
            }

            //z
            if (size < 3)
            {
                comboBox_ZInputType.Hide();
                comboBox_ZInputMember.Hide();
                numericUpDown_Z.Hide();
            }
            else
            {
                comboBox_ZInputType.Show();

                if ((string)comboBox_ZInputType.SelectedItem != CONST_INPUT_NAME)
                {
                    comboBox_ZInputMember.Show();
                    numericUpDown_Z.Hide();
                }
                else
                {
                    comboBox_ZInputMember.Hide();
                    numericUpDown_Z.Show();
                }
            }

            //y
            if (size < 2)
            {
                comboBox_YInputType.Hide();
                comboBox_YInputMember.Hide();
                numericUpDown_Y.Hide();
            }
            else
            {
                comboBox_YInputType.Show();

                if ((string)comboBox_YInputType.SelectedItem != CONST_INPUT_NAME)
                {
                    comboBox_YInputMember.Show();
                    numericUpDown_Y.Hide();
                }
                else
                {
                    comboBox_YInputMember.Hide();
                    numericUpDown_Y.Show();
                }
            }

            //x
            if ((string)comboBox_XInputType.SelectedItem != CONST_INPUT_NAME)
            {
                comboBox_XInputMember.Show();
                numericUpDown_X.Hide();
            }
            else
            {
                comboBox_XInputMember.Hide();
                numericUpDown_X.Show();
            }
        }

        //TODO: rename - > Owner
        VectorMix m_owner
        {
            get { return (VectorMix)BaseBlock; }
        }
    }
}
