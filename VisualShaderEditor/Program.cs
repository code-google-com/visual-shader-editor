﻿/*
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
