using System;
using System.Collections.Generic;
using System.Text;
using Core.Basic;
using Core.Main;
using Core.Var;
using System.Diagnostics;
using Core.CodeGeneration.Code;
using Core.Environment;

namespace Core.Blocks.Input
{
    [Block(Name = "SystemParameter", Path = "Input")]
    public class SystemParameter : BaseBlock
    {
        public class Parameter
        {
            public string Name;
            public Format Format;
            public Action<ICompiledShader> Upload;
        }

        public static readonly List<Parameter> Parameters = new List<Parameter>(new Parameter[]{
            new Parameter(){Name = "Model", Format = Format.FLOAT4X4,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("Model", x.Owner.SystemParameters.GetParameter(Matrix44Parameter.Model)))},
            new Parameter(){Name = "ModelView", Format = Format.FLOAT4X4,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("ModelView", x.Owner.SystemParameters.GetParameter(Matrix44Parameter.Model) * x.Owner.SystemParameters.GetParameter(Matrix44Parameter.View)))},
            new Parameter(){Name = "ModelViewProjection", Format = Format.FLOAT4X4,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("ModelViewProjection", x.Owner.SystemParameters.GetParameter(Matrix44Parameter.Model) * x.Owner.SystemParameters.GetParameter(Matrix44Parameter.View) * x.Owner.SystemParameters.GetParameter(Matrix44Parameter.Projection)))},
            new Parameter(){Name = "View", Format = Format.FLOAT4X4,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("View", x.Owner.SystemParameters.GetParameter(Matrix44Parameter.View)))},
            new Parameter(){Name = "ViewProjection", Format = Format.FLOAT4X4,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("ViewProjection", x.Owner.SystemParameters.GetParameter(Matrix44Parameter.View) * x.Owner.SystemParameters.GetParameter(Matrix44Parameter.Projection)))},
            new Parameter(){Name = "Projection", Format = Format.FLOAT4X4,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("Projection", x.Owner.SystemParameters.GetParameter(Matrix44Parameter.Projection)))},

            new Parameter(){Name = "Time", Format = Format.FLOAT,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("Time", x.Owner.SystemParameters.GetParameter(Vector1Parameter.Time)))},

            new Parameter(){Name = "SinTime", Format = Format.FLOAT,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("SinTime", new Vector1f((float)System.Math.Sin(x.Owner.SystemParameters.GetParameter(Vector1Parameter.Time).X))))},

            new Parameter(){Name = "CameraPositon", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("CameraPositon", x.Owner.SystemParameters.GetParameter(Vector3Parameter.CameraPosition)))},
            new Parameter(){Name = "CameraUp", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("CameraUp", x.Owner.SystemParameters.GetParameter(Vector3Parameter.CameraUp)))},
            new Parameter(){Name = "CameraForward", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("CameraForward", x.Owner.SystemParameters.GetParameter(Vector3Parameter.CameraForward)))},
            new Parameter(){Name = "CameraRight", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("CameraRight", x.Owner.SystemParameters.GetParameter(Vector3Parameter.CameraRight)))},

            new Parameter(){Name = "LightColor", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("LightColor", x.Owner.SystemParameters.GetParameter(Vector3Parameter.LightColor)))},
            new Parameter(){Name = "LightAttenuation", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("LightAttenuation", x.Owner.SystemParameters.GetParameter(Vector3Parameter.LightAttenuation)))},
            new Parameter(){Name = "LightPosition", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("LightPosition", x.Owner.SystemParameters.GetParameter(Vector3Parameter.LightPosition)))},
            new Parameter(){Name = "LightForward", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("LightForward", x.Owner.SystemParameters.GetParameter(Vector3Parameter.LightForward)))},
            new Parameter(){Name = "AmbientColor", Format = Format.FLOAT3,
                Upload = new Action<ICompiledShader>((x)=>x.SetParameter("AmbientColor", x.Owner.SystemParameters.GetParameter(Vector3Parameter.AmbientColor)))},

            //new Parameter(){Name = "LightShadowMap", Format = Format.TEXTURE},
            //new Parameter(){Name = "LightColorMap", Format = Format.TEXTURE},
            //new Parameter(){Name = "EnvironmentCubeMap", Format = Format.TEXTURE},
        });

        public class SystemParameterOutput : BlockOutput
        {
            public SystemParameterOutput(BaseBlock owner, Format f, string name)
                : base(owner, f, name)
            {
            }

            protected override Variable CreateVariable()
            {
                //ignore output format, always use correct one
                return Owner.BlockManager.VariableManager.CreateVariable(Name, Parameters.Find((x)=>x.Name == Name).Format, VerticesStreamSemantic.NONE, 0, Owner, true);
            }
        }

        public SystemParameter(BlockManager owner)
            : base(owner, new SystemParameterOptionsWindow())
        {
            AddOutput(new SystemParameterOutput(this, Format.FLOAT, "Time"));
        }

        public override void SetShaderParameters(Core.Environment.ICompiledShader s)
        {
            base.SetShaderParameters(s);

            foreach (var p in Parameters)
                p.Upload(s);
        }

        protected override BlockOutput CreateOutput()
        {
            return new SystemParameterOutput(this, Format.FLOAT4, "Time");
        }

        protected internal override void GenerateCode(Core.CodeGeneration.ShaderCodeGenerator sc)
        {
            sc.AddParameter(Outputs[0].Variable);
        }

        public string CurrentParameterName
        {
            get { return Outputs[0].Name; }
            set
            {
                Outputs[0].Name = value;
                Outputs[0].Format = Parameters.Find((x) => x.Name == value).Format;
            }
        }
    }
}
