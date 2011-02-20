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
using Core.Basic;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Core.Var;

namespace Core.Main
{
    public class BlockManager
    {
        static readonly string MAIN_XML_ELEMENT_NAME = "VisualShaderEditorMaterial";

        static readonly string BLOCKS_XML_ELEMENT_NAME = "Blocks";
        static readonly string BLOCK_XML_ELEMENT_NAME = "Block";
        static readonly string BLOCK_XML_TYPE_ATTRIBUTE_NAME = "Type";

        static readonly string CONNECTIONS_XML_ELEMENT_NAME = "Connections";
        static readonly string CONNECTION_XML_ELEMENT_NAME = "Connection";
        static readonly string CONNECTION_XML_INPUT_ATTRIBUTE_NAME = "InputId";
        static readonly string CONNECTION_XML_OUTPUT_ATTRIBUTE_NAME = "OutputId";

        public enum Type{
            ShaderFile,
            UserBlock,
            TemplateBlock
        }

        public BlockManager(Type t, VariableManager vm, ProjectFile owner)
        {
            VariableManager = vm;
            Owner = owner;
        }

        public event Action<BlockManager> OnDataChanged;

        public BaseBlock AddBlock(string blockName)
        {
            BaseBlock b = StaticBase.Singleton.BlockList.CreateBlockByTypeName(this, blockName);
            m_blocks.Add(b);

            b.OnDataChanged += new Action<BaseBlock>(OnBlockChange);

            RaiseDataChanged();

            return b;
        }
        public void RemoveBlock(BaseBlock b)
        {
            Debug.Assert(b.BlockManager == this);

            b.OnDataChanged -= new Action<BaseBlock>(OnBlockChange);

            b.DisconnectAllOutput();
            b.DisconnectAllInput();

            bool r = m_blocks.Remove(b);
            Debug.Assert(r);

            b.Destroy();

            RaiseDataChanged();
        }

        public __Type FindByType<__Type>() where __Type : BaseBlock
        {
            foreach (var b in m_blocks)
                if (b.GetType() == typeof(__Type))
                    return (__Type)b;

            return null;
        }

        public void Clear()
        {
            m_blocks.Clear();
        }

        public readonly VariableManager VariableManager;

        public bool Load(XmlElement node)
        {
            m_blockRaiseDataChanged = true;
            Clear();

            //load from xml
            XmlElement mainElement = node;
            XmlElement blocksElement = mainElement[BLOCKS_XML_ELEMENT_NAME];
            XmlElement connectionsElement = mainElement[CONNECTIONS_XML_ELEMENT_NAME];

            //dictionary id -> object
            Dictionary<Guid, object> idToObj = new Dictionary<Guid, object>();

            //load blocks
            foreach (var xb in blocksElement.ChildNodes)
            {
                XmlElement bElement = xb as XmlElement;
                if (bElement == null || bElement.Name != BLOCK_XML_ELEMENT_NAME)
                    continue;

                //read blocks
                string name = bElement.GetAttribute(BLOCK_XML_TYPE_ATTRIBUTE_NAME);
                BaseBlock bb = AddBlock(name);
                bb.Load(bElement);

                //add block to gui list
                idToObj.Add(bb.Guid, bb);
                //add io to gui list
                foreach (var i in bb.Inputs)
                    idToObj.Add(i.Guid, i);
                foreach (var o in bb.Outputs)
                    idToObj.Add(o.Guid, o);
            }

            //load connections
            foreach (var xc in connectionsElement.ChildNodes)
            {
                XmlElement cElement = xc as XmlElement;
                if (cElement == null || cElement.Name != CONNECTION_XML_ELEMENT_NAME)
                    continue;

                //read connection
                Guid inputGuid = new Guid(cElement.GetAttribute(CONNECTION_XML_INPUT_ATTRIBUTE_NAME));
                Guid outputGuid = new Guid(cElement.GetAttribute(CONNECTION_XML_OUTPUT_ATTRIBUTE_NAME));

                BlockInput input = idToObj[inputGuid] as BlockInput;
                BlockOutput output = idToObj[outputGuid] as BlockOutput;

                input.Owner.Connect(input, output);
            }

            m_blockRaiseDataChanged = false;
            RaiseDataChanged();

            return true;
        }
        public bool Save(XmlElement node)
        {
            //save to xml
            XmlElement mainElement = node;
            XmlElement blocksElement = (XmlElement)mainElement.AppendChild(mainElement.OwnerDocument.CreateElement(BLOCKS_XML_ELEMENT_NAME));
            XmlElement connectionsElement = (XmlElement)mainElement.AppendChild(mainElement.OwnerDocument.CreateElement(CONNECTIONS_XML_ELEMENT_NAME));

            //save blocks
            for (int i = 0; i < m_blocks.Count; i++)
            {
                //save extra data: type
                XmlElement bElement = (XmlElement)blocksElement.AppendChild(mainElement.OwnerDocument.CreateElement(BLOCK_XML_ELEMENT_NAME));
                bElement.SetAttribute(BLOCK_XML_TYPE_ATTRIBUTE_NAME, m_blocks[i].GetType().AssemblyQualifiedName);

                m_blocks[i].Save(bElement);

                //save connections
                foreach (var c in m_blocks[i].Inputs)
                {
                    if (c.ConnectedTo != null)
                    {
                        XmlElement cElement = (XmlElement)connectionsElement.AppendChild(mainElement.OwnerDocument.CreateElement(CONNECTION_XML_ELEMENT_NAME));
                        cElement.SetAttribute(CONNECTION_XML_INPUT_ATTRIBUTE_NAME, c.Guid.ToString());
                        cElement.SetAttribute(CONNECTION_XML_OUTPUT_ATTRIBUTE_NAME, c.ConnectedTo.Guid.ToString());
                    }
                }
            }

            return true;
        }
        public bool Load(Stream str)
        {
            XmlDocument d = new XmlDocument();

            //load from stream
            d.Load(str);
            str.Close();

            //load from xml
            XmlElement mainElement = d.DocumentElement;

            return Load(mainElement);
        }
        public bool Save(Stream str)
        {
            XmlDocument d = new XmlDocument();
            d.AppendChild(d.CreateXmlDeclaration("1.0", "utf-8", ""));
            d.AppendChild(d.CreateElement(MAIN_XML_ELEMENT_NAME));

            //save to xml
            XmlElement mainElement = d.DocumentElement;
            bool ret = Save(mainElement);

            //save to stream
            d.Save(str);
            str.Close();

            return ret;
        }
        public bool Load(string fileName)
        {
            Clear();
            return Load(File.OpenRead(fileName));
        }
        public bool Save(string fileName)
        {
            File.Delete(fileName);
            return Save(File.OpenWrite(fileName));
        }

        public ReadOnlyCollection<BaseBlock> Blocks
        {
            get { return m_blocks.AsReadOnly(); }
        }

        public void MoveBlocks(Vector2f offset)
        {
            m_blockRaiseDataChanged = true;

            foreach (var bb in m_blocks)
                bb.Position += offset;

            m_blockRaiseDataChanged = false;

            RaiseDataChanged();
        }

        public readonly ProjectFile Owner;

        #region private

        void RaiseDataChanged()
        {
            if (m_blockRaiseDataChanged)
                return;

            if (OnDataChanged != null)
                OnDataChanged(this);
        }

        void OnBlockChange(BaseBlock me)
        {
            RaiseDataChanged();
        }

        bool m_blockRaiseDataChanged = false;
        readonly List<BaseBlock> m_blocks = new List<BaseBlock>();

        #endregion
    }
}
