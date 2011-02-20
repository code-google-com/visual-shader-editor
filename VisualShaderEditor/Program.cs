using System;
using System.Drawing;
using System.Windows.Forms;
using Core.Environment;
using VisualShaderEditor.Windows;
using Core.Main;
using System.IO;
using System.Reflection;

namespace VisualShaderEditor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //select environment
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                StaticBase.CreateSingleton();
                StaticBase.Singleton.Log.Connect(new Action<Log.LogEntry>(LogCopy), true);
                StaticBase.Singleton.LoadPlugins();

                Application.Run(new Windows.SelectEnvironment());

                //run app
                if (StaticBase.Singleton.Environment != null)
                {
                    Application.Run(new Windows.MainWindow());
                }

                StaticBase.DestroySingleton();
            }
            catch (Exception e)
            {
                string m = "";
                while (e != null)
                {
                    m += string.Format("Exception: {0}\nSource: {1}\nStack: {2}\n\n", e.Message, e.Source, e.StackTrace);
                    e = e.InnerException;
                }

                m += "\nLog:\n" + Log;

                File.WriteAllText("Crash.txt", m);
            }
        }

        static void LogCopy(Log.LogEntry le)
        {
            Log += le.ToString();
        }

        static string Log = "";
    }
}
