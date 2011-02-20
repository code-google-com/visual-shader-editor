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
