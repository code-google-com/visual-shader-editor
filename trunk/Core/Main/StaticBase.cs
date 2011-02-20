using System;
using System.Collections.Generic;
using System.Text;
using Core.Environment;
using System.Diagnostics;

namespace Core.Main
{
    //TODO: rename
    public class StaticBase : IDisposable
    {
        public static void CreateSingleton()
        {
            Debug.Assert(m_singleton == null);
            m_singleton = new StaticBase();
        }
        public static void DestroySingleton()
        {
            Debug.Assert(m_singleton != null);
            m_singleton.Dispose();
            m_singleton = null;
        }
        public static StaticBase Singleton
        {
            get
            {
                Debug.Assert(m_singleton != null);
                return m_singleton; }
        }

        StaticBase()
        {
            BlockList.RegisterType(typeof(Core.Blocks.ControlFlow.If));

            BlockList.RegisterType(typeof(Core.Blocks.Input.Constant));
            BlockList.RegisterType(typeof(Core.Blocks.Input.VerticesStream));
            BlockList.RegisterType(typeof(Core.Blocks.Input.SystemParameter));
            BlockList.RegisterType(typeof(Core.Blocks.Input.UserParameter));

            BlockList.RegisterType(typeof(Core.Blocks.Math.Operators.Add));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Operators.Sub));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Operators.Mul));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Operators.Div));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Operators.Negative));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Operators.Pow));

            BlockList.RegisterType(typeof(Core.Blocks.Math.Other.OneMinusX));            
            BlockList.RegisterType(typeof(Core.Blocks.Math.Other.Abs));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Other.Lerp));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Other.Clamp));

            BlockList.RegisterType(typeof(Core.Blocks.Math.Scalar.Sinus));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Scalar.Cosinus));

            BlockList.RegisterType(typeof(Core.Blocks.Math.Vector.Dot));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Vector.VectorMix));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Vector.Cross));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Vector.Normalize));
            BlockList.RegisterType(typeof(Core.Blocks.Math.Vector.Length));

            BlockList.RegisterType(typeof(Core.Blocks.Position.TransformPosition));
            BlockList.RegisterType(typeof(Core.Blocks.Output.ShaderOutput));
            BlockList.RegisterType(typeof(Core.Blocks.Texture.SamplerWithTexture));
            BlockList.RegisterType(typeof(Core.Blocks.Special.VSForce));
        }

        public void LoadPlugins()
        {
            m_environmentManager.LoadPlugins();
            //  m_currentEnvironment = m_environmentManager.GetPluginDescription(0).CreatePlugin();     //dx10
            //  m_currentEnvironment = m_environmentManager.GetPluginDescription(1).CreatePlugin();     //dx9
            //m_currentEnvironment = m_environmentManager.GetPluginDescription(2).CreatePlugin();     //ogl
        }

        public event Action<IEnvironment> OnDestroyEnvironment;
        public event Action<IEnvironment> OnCreateEnvironment;

        public void SelectEnvironment(EnvironmentManager.PluginDescription pd)
        {
            //remove old
            if (m_currentEnvironment != null)
            {
                if (OnDestroyEnvironment != null)
                    OnDestroyEnvironment(m_currentEnvironment);

                m_currentEnvironment.Dispose();
                m_currentEnvironment = null;
            }

            //add new
            if (pd != null)
            {
                m_currentEnvironment = pd.CreatePlugin();
                if (OnCreateEnvironment != null)
                    OnCreateEnvironment(m_currentEnvironment);
            }
        }

        public IEnvironment Environment
        {
            get { return m_currentEnvironment; }
        }
        public readonly BlockList BlockList = new BlockList();
        public EnvironmentManager EnvironmentManager
        {
            get { return m_environmentManager; }
        }

        public void Dispose()
        {
            SelectEnvironment(null);
        }

        public readonly Log Log = new Log();

        #region private

        readonly EnvironmentManager m_environmentManager = new EnvironmentManager();
        IEnvironment m_currentEnvironment;
        static StaticBase m_singleton;

        #endregion

        
    }
}
