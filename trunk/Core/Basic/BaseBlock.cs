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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml;
using Core.CodeGeneration;
using Core.Main;
using Core.Environment;
using Core.Var;
using Core.Blocks;

namespace Core.Basic
{
    public abstract class BaseBlock
    {
        static readonly string BLOCK_XML_OUTPUT_ELEMENT_NAME = "Output";
        static readonly string BLOCK_XML_INPUT_ELEMENT_NAME = "Input";
        static readonly string BLOCK_XML_ID_ATTRIBUTE_NAME = "Id";
        static readonly string BLOCK_XML_POSITION_ATTRIBUTE_NAME = "Position";
        static readonly string BLOCK_XML_BLOCK_COMMENT_ATTRIBUTE_NAME = "BlockComment";

        public event Action<BaseBlock> OnDataChanged;

        protected BaseBlock(BlockManager owner, IOptionsWindow w)
        {
            m_blockManager = owner;

            if (w == null)
                w = new OptionsWindow();

            m_options = w;
            m_options.SetBlock(this);
        }

        #region Input/Output

        public ReadOnlyCollection<BlockInput> Inputs
        {
            get { return m_inputs.AsReadOnly(); }
        }
        public ReadOnlyCollection<BlockOutput> Outputs
        {
            get { return m_outputs.AsReadOnly(); }
        }

        public BlockInput FindInput(string name)
        {
            foreach (var i in m_inputs)
                if (i.Name == name)
                    return i;

            return null;
        }
        public BlockOutput FindOutput(string name)
        {
            foreach (var i in m_outputs)
                if (i.Name == name)
                    return i;

            return null;
        }

        protected void AddInput(BlockInput bi)
        {
            bi.OnDataChanged += new Action<BlockIOBase>(bio_DataChanged);

            m_inputs.Add(bi);
            RaiseDataChanged();
        }
        protected void AddOutput(BlockOutput bo)
        {
            bo.OnDataChanged += new Action<BlockIOBase>(bio_DataChanged);

            m_outputs.Add(bo);
            RaiseDataChanged();
        }
        public void RemoveInput(BlockInput bi)
        {
            //remove connection
            if (bi.ConnectedTo != null)
                Disconnect(bi);

            m_inputs.Remove(bi);
            RaiseDataChanged();

            bi.OnDataChanged -= new Action<BlockIOBase>(bio_DataChanged);
            bi.Destroy();
        }
        public void RemoveOutput(BlockOutput bo)
        {
            //make connections list copy
            List<BlockInput> bil = new List<BlockInput>(bo.ConnectedTo);

            //remove connections
            foreach (var bi in bil)
                bi.Owner.Disconnect(bi);

            //remove output
            m_outputs.Remove(bo);
            RaiseDataChanged();

            bo.OnDataChanged += new Action<BlockIOBase>(bio_DataChanged);
            bo.Destroy();
        }

        protected virtual BlockInput CreateInput()
        {
            return new BlockInput(this, "");
        }
        protected virtual BlockOutput CreateOutput()
        {
            return new BlockOutput(this, Format.FLOAT, "");
        }

        public void Disconnect(BlockInput myInput)
        {
            Debug.Assert(myInput != null && Inputs.Contains(myInput));

            //disconnect old
            if (myInput.ConnectedTo != null)
            {
                BlockOutput output = myInput.ConnectedTo;

                Debug.Assert(output.IsConnectedTo(myInput));

                //set to null
                output.RemoveConnection(myInput);
                myInput.ConnectedTo = null;

                //events
                RaiseDataChanged();
                output.Owner.RaiseDataChanged();
            }
        }
        public void Disconnect(BlockOutput myOutput)
        {
            Debug.Assert(myOutput != null && Outputs.Contains(myOutput));

            if (myOutput.ConnectedTo != null && myOutput.ConnectedTo.Count > 0)
            {
                //make connections list copy
                List<BlockInput> bil = new List<BlockInput>(myOutput.ConnectedTo);

                //remove connections
                foreach (var bi in bil)
                    bi.Owner.Disconnect(bi);
            }
        }

        public void Connect(BlockInput myInput, BlockOutput output)
        {
            Debug.Assert(myInput != null && Inputs.Contains(myInput));
            Debug.Assert(output != null && output.Owner != null && output.Owner != this);

            //disconnect old
            Disconnect(myInput);

            //connect new

            //set
            myInput.ConnectedTo = output;
            output.AddConnection(myInput);

            //events
            RaiseDataChanged();
            if (output != null)
                output.Owner.RaiseDataChanged();
        }

        public void DisconnectAllInput()
        {
            foreach (var i in Inputs)
                if (i.ConnectedTo != null)
                    Disconnect(i);
        }
        public void DisconnectAllOutput()
        {
            foreach (var o in Outputs)
            {
                Disconnect(o);
            }
        }

        #endregion

        public virtual void SetShaderParameters(ICompiledShader s) { }
        internal protected abstract void GenerateCode(ShaderCodeGenerator sc);
        public virtual bool Singleton
        {
            get { return false; }
        }

        public Variable PreviewVariable
        {
            get
            {
                if (m_outputs.Count > 0)
                    return m_outputs[0].Variable;
                return null;
            }
        }
        public bool HaveOptions
        {
            get { return m_options != null; }
        }
        public void ShowOptions()
        {
            Debug.Assert(m_options != null);
            m_options.ShowOptions();
        }
        public string BlockName
        {
            get { return (Attribute.GetCustomAttribute(this.GetType(), typeof(BlockAttribute)) as BlockAttribute).Name; }
        }
        public Guid Guid
        {
            get { return m_guid; }
        }
        public BlockManager BlockManager
        {
            get { return m_blockManager; }
        }

        public Vector2f Position
        {
            get { return m_position; }
            set
            {
                if (m_position != value)
                {
                    m_position = value;
                    RaiseDataChanged();
                }
            }
        }
        public string BlockComment
        {
            get { return m_blockComment; }
            set
            {
                if (m_blockComment != value)
                {
                    m_blockComment = value;
                    RaiseDataChanged();
                }
            }
        }

        public virtual void Save(XmlElement node)
        {
            node.SetAttribute(BLOCK_XML_ID_ATTRIBUTE_NAME, Guid.ToString());

            {
                //save output
                foreach (var c in Outputs)
                {
                    XmlElement cElement = (XmlElement)node.AppendChild(node.OwnerDocument.CreateElement(BLOCK_XML_OUTPUT_ELEMENT_NAME));
                    c.Save(cElement);
                }

                //save input
                foreach (var c in Inputs)
                {
                    XmlElement cElement = (XmlElement)node.AppendChild(node.OwnerDocument.CreateElement(BLOCK_XML_INPUT_ELEMENT_NAME));
                    c.Save(cElement);
                }
            }

            node.SetAttribute(BLOCK_XML_POSITION_ATTRIBUTE_NAME, Position.ToString());
            node.SetAttribute(BLOCK_XML_BLOCK_COMMENT_ATTRIBUTE_NAME, BlockComment);
        }
        public virtual void Load(XmlElement node)
        {
            BlockRaiseDataChange();

            Clear();

            m_guid = new Guid(node.GetAttribute(BLOCK_XML_ID_ATTRIBUTE_NAME));

            {
                foreach (var n in node.ChildNodes)
                {
                    XmlElement nElement = n as XmlElement;
                    if (nElement == null)
                        continue;

                    if (nElement.Name == BLOCK_XML_OUTPUT_ELEMENT_NAME)
                    {
                        BlockOutput bo = CreateOutput();
                        bo.Load(nElement);
                        AddOutput(bo);
                    }

                    if (nElement.Name == BLOCK_XML_INPUT_ELEMENT_NAME)
                    {
                        BlockInput bi = CreateInput();
                        bi.Load(nElement);
                        AddInput(bi);
                    }
                }
            }

            Position = Vector2f.Parse(node.GetAttribute(BLOCK_XML_POSITION_ATTRIBUTE_NAME));
            BlockComment = node.GetAttribute(BLOCK_XML_BLOCK_COMMENT_ATTRIBUTE_NAME);

            UnBlockRaiseDataChanged();
        }

        internal void Destroy()
        {
            BlockRaiseDataChange();
            Clear();
        }

        protected void RaiseDataChanged()
        {
            if (m_raiseDataChangeBlockCounter > 0)
                return;

            DataChanged();

            if (OnDataChanged != null)
                OnDataChanged(this);
        }
        protected virtual void DataChanged()
        {
        }
        protected void BlockRaiseDataChange()
        {
            m_raiseDataChangeBlockCounter++;
        }
        protected void UnBlockRaiseDataChanged()
        {
            m_raiseDataChangeBlockCounter--;

            Debug.Assert(m_raiseDataChangeBlockCounter >= 0);

            if (m_raiseDataChangeBlockCounter == 0)
                RaiseDataChanged();
        }

        #region private

        void bio_DataChanged(BlockIOBase obj)
        {
            RaiseDataChanged();

            //inform connected inputs
            BlockOutput bo = obj as BlockOutput;
            if (bo != null)
            {
                foreach (var v in bo.ConnectedTo)
                    v.Owner.DataChanged();
            }
        }

        protected virtual void Clear()
        {
            BlockRaiseDataChange();

            BlockInput[] bi = m_inputs.ToArray();
            BlockOutput[] bo = m_outputs.ToArray();

            foreach (var i in bi)
                RemoveInput(i);

            foreach (var o in bo)
                RemoveOutput(o);

            UnBlockRaiseDataChanged();
        }

        readonly List<BlockInput> m_inputs = new List<BlockInput>();
        readonly List<BlockOutput> m_outputs = new List<BlockOutput>();
        Guid m_guid = Guid.NewGuid();

        BlockManager m_blockManager;
        IOptionsWindow m_options;
        Vector2f m_position;

        int m_raiseDataChangeBlockCounter = 0;

        string m_blockComment = "";

        #endregion
    }
}
