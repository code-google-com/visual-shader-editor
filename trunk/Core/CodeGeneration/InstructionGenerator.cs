using System;
using System.Collections.Generic;
using System.Text;
using Core.Main;
using Core.Blocks.Input;
using Core.Blocks.Output;
using Core.Basic;
using Core.CodeGeneration.Code;

namespace Core.CodeGeneration
{
    public class InstructionGenerator
    {
        public static ShaderCode GenerateDebug(BlockManager blockManager)
        {
            StaticBase.Singleton.Log.Write(Log.InfoType.ShaderInfo, "Begin Creating Debug Shader Code\n");

            ShaderCodeGenerator sc = new ShaderCodeGenerator();

            InstructionListBuilder ilb = new InstructionListBuilder(blockManager);
            List<BaseBlock> l = ilb.List;

            //generate code
            foreach (var bb in l)
                bb.GenerateCode(sc);

            //generate debug output
            ShaderCode sc2 = sc.Finish(true);

            StaticBase.Singleton.Log.Write(Log.InfoType.ShaderInfo, "End Creating Debug Shader Code\n");

            return sc2;
        }
        public static ShaderCode GenerateRelease(BlockManager blockManager)
        {
            StaticBase.Singleton.Log.Write(Log.InfoType.ShaderInfo, "Begin Creating Release Shader Code\n");

            ShaderCodeGenerator sc = new ShaderCodeGenerator();

            InstructionListBuilder ilb = new InstructionListBuilder(blockManager);
            List<BaseBlock> l = ilb.List;

            //generate code
            foreach (var bb in l)
                bb.GenerateCode(sc);

            ShaderCode sc2 = sc.Finish(false);

            StaticBase.Singleton.Log.Write(Log.InfoType.ShaderInfo, "End Creating Release Shader Code\n");

            return sc2;
        }
    }
}
