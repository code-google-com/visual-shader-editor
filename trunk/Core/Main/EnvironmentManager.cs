/*
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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Core.Environment;

namespace Core.Main
{
    public class EnvironmentManager
    {
        public static readonly string PLUGIN_DIRECTORY = ".";//"./Plugins";

        public class PluginDescription
        {
            internal PluginDescription(string name, Type t)
            {
                Name = name;
                Type = t;
            }

            public readonly string Name;
            public IEnvironment CreatePlugin()
            {
                return (IEnvironment)Type.Assembly.CreateInstance(Type.FullName);
            }
            internal readonly Type Type;

            public override string ToString()
            {
                return string.Format("Environment: \"{0}\" [{1}", Name, Type.FullName);
            }
        }

        public void LoadPlugins()
        {
            StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "Begin Loading Plugins\n");
            StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "CurrentDirectory:\"{0}\"\n", Directory.GetCurrentDirectory());
            StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "Plugin Dir:\"{0}\"\n", PLUGIN_DIRECTORY);

            string[] files = Directory.GetFiles(PLUGIN_DIRECTORY);

            foreach (var f in files)
            {
                //prepare file name
                string fullFileName = Path.Combine(Directory.GetCurrentDirectory(), f);
                StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "Loading File:\"{0}\"\n", fullFileName);

                //try load
                Type[] types;
                try
                {
                    Assembly a = Assembly.LoadFile(fullFileName);
                    types = a.GetExportedTypes();
                }
                catch (Exception e)
                {
                    StaticBase.Singleton.Log.Write(Log.InfoType.GlobalError, "Loading Fail:{0}\n", e.Message);
                    continue;
                }

                //find plugin class
                StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "Loading...Done\n");
                StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "Begin Scaning for plugins\n");
                foreach (Type t in types)
                {
                    EnvironmentAttribute atr = Attribute.GetCustomAttribute(t, typeof(EnvironmentAttribute)) as EnvironmentAttribute;

                    if (atr != null)
                    {
                        StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "Plugin Found:\"{0}\" Class:\"{1}\"\n", atr.Name, t.AssemblyQualifiedName);
                        m_plugins.Add(new PluginDescription(atr.Name, t));
                    }
                }
                StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "End Scaning for plugins\n");
            }

            StaticBase.Singleton.Log.Write(Log.InfoType.GlobalInfo, "End Loading Plugins\n");
        }

        public int PluginCount
        {
            get { return m_plugins.Count; }
        }

        public PluginDescription GetPluginDescription(int id)
        {
            return m_plugins[id];
        }

        readonly List<PluginDescription> m_plugins = new List<PluginDescription>();
    }
}
