using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core.Main;

namespace VisualShaderEditor.Windows
{
    public partial class SelectEnvironment : Form
    {
        public SelectEnvironment()
        {
            InitializeComponent();

            for (int i = 0; i < StaticBase.Singleton.EnvironmentManager.PluginCount; i++)
            {
                comboBox_Environment.Items.Add(StaticBase.Singleton.EnvironmentManager.GetPluginDescription(i));
            }

            comboBox_Environment.SelectedIndex = 0;

            StaticBase.Singleton.Log.Connect(new Action<Log.LogEntry>(LogCopy), true);
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            if (comboBox_Environment.SelectedItem != null)
                StaticBase.Singleton.SelectEnvironment((EnvironmentManager.PluginDescription)comboBox_Environment.SelectedItem);
            else
                return;

            DialogResult = DialogResult.OK;
            StaticBase.Singleton.Log.Disconnect(new Action<Log.LogEntry>(LogCopy));
            Close();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            StaticBase.Singleton.Log.Disconnect(new Action<Log.LogEntry>(LogCopy));
            Close();
        }

        void LogCopy(Log.LogEntry le)
        {
            richTextBox_Log.AppendText(le.ToString());
            richTextBox_Log.ScrollToCaret();
            richTextBox_Log.Update();
        }
    }
}
