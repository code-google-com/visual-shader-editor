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
using Core.Environment;

namespace Core.WorkSpaceController
{
    public class Block : IDisposable
    {
        #region Configuration

        static Vector2f BLOCK_SIZE = new Vector2f(50, 0);
        static Vector4f BLOCK_COLOR = new Vector4f(0.9f, 0.9f, 0.9f, 1);
        static float BLOCK_BOTTOM_SIZE = 10;

        static Vector2f TITLE_POSITION = new Vector2f(5, 4);
        static Vector4f TITLE_COLOR = new Vector4f(0, 0, 0, 1);
        static float TITLE_HEIGHT = 8;

        static Vector2f PREVIEW_OFFSET = new Vector2f(4, 14);
        static Vector2f PREVIEW_SIZE = new Vector2f(42, 42);

        static Vector2f EXIT_BUTTON_OFFSET = new Vector2f(35, 5);
        static Vector2f EXIT_BUTTON_SIZE = new Vector2f(10, 5);
        static Vector4f EXIT_BUTTON_COLOR = new Vector4f(1, 0, 0, 1);

        static Vector2f OUTPUT_OFFSET = new Vector2f(0, 61);
        static Vector2f INPUT_OFFSET = new Vector2f(26, 61);
        static Vector2f IO_STRIDE = new Vector2f(0, 10);
        static Vector2f IO_SIZE = new Vector2f(24, 8);
        static Vector4f IO_COLOR = new Vector4f(0.7f, 0.7f, 0.7f, 1);
        static Vector4f IO_SELECTED_COLOR = new Vector4f(0.3f, 0.3f, 1, 1);
        static Vector2f IO_TEXT_OFFSET = new Vector2f(2, 1);
        static float IO_TEXT_HEIGHT = 6;
        static Vector4f IO_TEXT_COLOR = new Vector4f(0,0,0,1);

        static Vector2f OUTPUT_DISCONNECT_OFFSET = new Vector2f(0, 61);
        static Vector2f INPUT_DISCONNECT_OFFSET = new Vector2f(26 + 18, 61);
        static Vector4f IO_DISCONNECT_COLOR = new Vector4f(1, 0, 0, 1);
        static Vector2f IO_DISCONNECT_SIZE = new Vector2f(6, 4);

        static Vector2f COMMENT_OFFSET = new Vector2f(0, -10);
        static float COMMENT_TEXT_HEIGHT = 10;
        static Vector4f COMMENT_TEXT_COLOR = new Vector4f(10, 10, 0, 1);

        #endregion

        public Block(BaseBlock bb, WorkSpaceController owner)
        {
            m_bb = bb;
            m_owner = owner;
            m_p = m_owner.WorkSpace.CreatePreview(m_bb);
        }

        public Vector2f Position
        {
            get { return m_bb.Position; }
            set { m_bb.Position = value; }
        }
        public void Draw(DrawHelper dh)
        {
            Vector2f pos = new Vector2f(m_bb.Position.X, m_bb.Position.Y);

            dh.AddRectangle(ColorRectangle.CreateBorder(BlockRectangle, BLOCK_COLOR));
            dh.AddRectangle(ColorRectangle.CreateBorder(ExitButtonRect, EXIT_BUTTON_COLOR));

            dh.AddText(new ColorText() { Text = m_bb.BlockName, Position = pos + TITLE_POSITION, Height = TITLE_HEIGHT, Color = TITLE_COLOR });

            //comment
            dh.AddText(new ColorText() { Text = m_bb.BlockComment, Position = pos + COMMENT_OFFSET, Height = COMMENT_TEXT_HEIGHT, Color = COMMENT_TEXT_COLOR });

            //output
            for (int i = 0; i < m_bb.Outputs.Count; i++)
            {
                if(m_owner.FirstConnectionBio == m_bb.Outputs[i])
                    dh.AddRectangle(ColorRectangle.CreateBorder(OutputRectangle(i), IO_SELECTED_COLOR));
                else
                    dh.AddRectangle(ColorRectangle.CreateBorder(OutputRectangle(i), IO_COLOR));

                dh.AddRectangle(ColorRectangle.CreateButton(OutputDisconnectRectangle(i), IO_DISCONNECT_COLOR));
                dh.AddText(new ColorText() { Text = m_bb.Outputs[i].Name, Position = OutputRectangle(i).Min + IO_TEXT_OFFSET, Height = IO_TEXT_HEIGHT, Color = IO_TEXT_COLOR });
            }

            //input
            for (int i = 0; i < m_bb.Inputs.Count; i++)
            {
                if (m_owner.FirstConnectionBio == m_bb.Inputs[i])
                    dh.AddRectangle(ColorRectangle.CreateBorder(InputRectangle(i), IO_SELECTED_COLOR));
                else
                    dh.AddRectangle(ColorRectangle.CreateBorder(InputRectangle(i), IO_COLOR));

                dh.AddRectangle(ColorRectangle.CreateButton(InputDisconnectRectangle(i), IO_DISCONNECT_COLOR));
                dh.AddText(new ColorText() { Text = m_bb.Inputs[i].Name, Position = InputRectangle(i).Min + IO_TEXT_OFFSET, Height = IO_TEXT_HEIGHT, Color = IO_TEXT_COLOR });
            }

            for (int i = 0; i < m_bb.Inputs.Count; i++)
                if (m_bb.Inputs[i].ConnectedTo != null)
                    dh.AddConnection(new Line2f(CalculatePosition(m_bb.Inputs[i]), CalculatePosition(m_bb.Inputs[i].ConnectedTo)));

            //preview
            dh.AddRectangle(ColorRectangle.CreatePreview(new Rectangle2f(pos + PREVIEW_OFFSET, pos + PREVIEW_OFFSET + PREVIEW_SIZE), m_p));
        }

        public bool IsMouseInside(Vector2f pos)
        {
            return BlockRectangle.IsInside(pos);
        }
        public void LeftMouseClick(Vector2f pos)
        {
            if(ExitButtonRect.IsInside(pos))
                m_bb.BlockManager.RemoveBlock(m_bb);

            for (int i = 0; i < m_bb.Outputs.Count; i++)
            {
                //try disconnect
                if (OutputDisconnectRectangle(i).IsInside(pos))
                    m_bb.Disconnect(m_bb.Outputs[i]);
                else
                {
                    //try connect
                    if(OutputRectangle(i).IsInside(pos))
                        m_owner.TryConnect(this, m_bb.Outputs[i]);
                }
            }

            for (int i = 0; i < m_bb.Inputs.Count; i++)
            {
                //try disconnect
                if (InputDisconnectRectangle(i).IsInside(pos))
                    m_bb.Disconnect(m_bb.Inputs[i]);
                else
                {
                    //try connect
                    if (InputRectangle(i).IsInside(pos))
                        m_owner.TryConnect(this, m_bb.Inputs[i]);
                }
            }
        }
        public void LeftMouseDoubleClick(Vector2f pos)
        {
            if (m_bb.HaveOptions)
                m_bb.ShowOptions();
        }

        public void Dispose()
        {
            m_p.Dispose();
        }

        #region private

        Rectangle2f BlockRectangle
        {
            get
            {
                Vector2f pos = new Vector2f(m_bb.Position.X, m_bb.Position.Y);
                Vector2f end = pos + new Vector2f(BLOCK_SIZE.X, PREVIEW_OFFSET.Y + PREVIEW_SIZE.Y);

                //find lovest i/o
                if (m_bb.Outputs.Count > 0 && OutputRectangle(m_bb.Outputs.Count - 1).Max.Y > end.Y)
                    end.Y = OutputRectangle(m_bb.Outputs.Count - 1).Max.Y;
                if (m_bb.Inputs.Count > 0 && InputRectangle(m_bb.Inputs.Count - 1).Max.Y > end.Y)
                    end.Y = InputRectangle(m_bb.Inputs.Count - 1).Max.Y;

                end.Y += BLOCK_BOTTOM_SIZE;
                
                return new Rectangle2f(pos, end);
            }
        }
        Rectangle2f ExitButtonRect
        {
            get
            {
                Vector2f pos = new Vector2f(m_bb.Position.X, m_bb.Position.Y);
                return new Rectangle2f(pos + EXIT_BUTTON_OFFSET, pos + EXIT_BUTTON_OFFSET + EXIT_BUTTON_SIZE);
            }
        }
        Rectangle2f OutputRectangle(int i)
        {
            Vector2f pos = new Vector2f(m_bb.Position.X, m_bb.Position.Y);
            return new Rectangle2f(pos + OUTPUT_OFFSET + IO_STRIDE * i, pos + OUTPUT_OFFSET + IO_SIZE + IO_STRIDE * i);
        }
        Rectangle2f InputRectangle(int i)
        {
            Vector2f pos = new Vector2f(m_bb.Position.X, m_bb.Position.Y);
            return new Rectangle2f(pos + INPUT_OFFSET + IO_STRIDE * i, pos + INPUT_OFFSET + IO_SIZE + IO_STRIDE * i);
        }
        Rectangle2f OutputDisconnectRectangle(int i)
        {
            Vector2f pos = new Vector2f(m_bb.Position.X, m_bb.Position.Y);
            return new Rectangle2f(pos + OUTPUT_DISCONNECT_OFFSET + IO_STRIDE * i, pos + OUTPUT_DISCONNECT_OFFSET + IO_STRIDE * i + IO_DISCONNECT_SIZE);
        }
        Rectangle2f InputDisconnectRectangle(int i)
        {
            Vector2f pos = new Vector2f(m_bb.Position.X, m_bb.Position.Y);
            return new Rectangle2f(pos + INPUT_DISCONNECT_OFFSET + IO_STRIDE * i, pos + INPUT_DISCONNECT_OFFSET + IO_STRIDE * i + IO_DISCONNECT_SIZE);
        }

        Vector2f CalculatePosition(BlockIOBase bio)
        {
            Vector2f p = bio.Owner.Position;
            p.Y += OUTPUT_OFFSET.Y + (IO_SIZE.Y / 2);

            BlockOutput o = bio as BlockOutput;
            if (o != null)
            {
                for (int i = 0; i < bio.Owner.Outputs.Count; i++)
                {
                    if (bio.Owner.Outputs[i] == bio)
                        break;

                    p += IO_STRIDE;
                }
            }
            else
            {
                BlockInput bi = bio as BlockInput;
                p.X += BLOCK_SIZE.X;

                for (int i = 0; i < bio.Owner.Inputs.Count; i++)
                {
                    if (bio.Owner.Inputs[i] == bio)
                        break;

                    p += IO_STRIDE;
                }
            }

            return p;
        }

        readonly BaseBlock m_bb;
        readonly IPreview m_p;
        readonly WorkSpaceController m_owner;

        #endregion

    }
}
