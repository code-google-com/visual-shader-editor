using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Core.Environment
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
        }

        public void LoadPlugins()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Begin Loading Plugins\n");
            sb.AppendFormat("CurrentDirectory:\"{0}\"\nPlugin Dir:\"{1}\"\n", Directory.GetCurrentDirectory(), PLUGIN_DIRECTORY);

            string[] files = Directory.GetFiles(PLUGIN_DIRECTORY);

            foreach (var f in files)
            {
                //prepare file name
                string fullFileName = Path.Combine(Directory.GetCurrentDirectory(), f);
                sb.AppendFormat("Loading File:\"{0}\"\n", fullFileName);

                //try load
                Type[] types;
                try
                {
                    Assembly a = Assembly.LoadFile(fullFileName);
                    types = a.GetExportedTypes();
                }
                catch (Exception e)
                {
                    sb.AppendFormat("Loading Fail:{0}\n", e.Message);
                    continue;
                }

                //find plugin class
                sb.AppendFormat("Loading...Done\nScaning for plugins\n");
                foreach (Type t in types)
                {
                    EnvironmentAttribute atr = Attribute.GetCustomAttribute(t, typeof(EnvironmentAttribute)) as EnvironmentAttribute;

                    if (atr != null)
                    {
                        sb.AppendFormat("Plugin Found:\"{0}\" Class:\"{1}\"\n", atr.Name, t.AssemblyQualifiedName);
                        m_plugins.Add(new PluginDescription(atr.Name, t));
                    }
                }
            }

            sb.AppendFormat("End Loading Plugins\n");
            m_loadLog = sb.ToString();
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
        string m_loadLog;

        public string LoadLog
        {
            get { return m_loadLog; }
        }
    }
}
