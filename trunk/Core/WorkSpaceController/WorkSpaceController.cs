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
using Core.Environment;
using Core.Basic;
using System.Windows.Forms;
using System.Diagnostics;
using Core.Main;

namespace Core.WorkSpaceController
{
    public class WorkSpaceController
    {
        static float ZOOM_MIN = 0.2f;
        static float ZOOM_MAX = 10.0f;
        static float ZOOM_SPEED = 0.1f;
        static float ZOOM_DEFAULT = 1.3f;
        static Vector2f BLOCK_SIZE = new Vector2f(50, 100);

        public WorkSpaceController(IWorkSpace ws, BlockManager bm, Panel p, ProjectFile pf)
        {
            m_ws = ws;
            m_bm = bm;
            m_p = p;
            m_projectFile = pf;

            ws.PreviewFileName = pf.PreviewOutFile;
            ws.ReleaseFileName = pf.ReleaseOutFile;

            m_p.MouseDown += new MouseEventHandler(m_p_MouseDown);
            m_p.MouseUp += new MouseEventHandler(m_p_MouseUp);
            m_p.MouseMove += new MouseEventHandler(m_p_MouseMove);
            m_p.MouseWheel += new MouseEventHandler(m_p_MouseWheel);
            m_p.MouseEnter += new EventHandler(m_p_MouseEnter);
            m_p.MouseDoubleClick += new MouseEventHandler(m_p_DoubleClick);
            m_p.MouseClick += new MouseEventHandler(m_p_MouseClick);

            m_bm.OnDataChanged += new Action<BlockManager>(m_bm_OnAfterChange);

            //refresh
            RefreshBlockList();
        }
        
        void m_bm_OnAfterChange(BlockManager obj)
        {
            RefreshBlockList();
            m_needRefresh = true;
        }

        void m_p_MouseEnter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            m_p.Focus();
        }
        void m_p_MouseWheel(object sender, MouseEventArgs e)
        {
            Vector2f mouse0 = new Vector2f(e.X / m_zoom, e.Y / m_zoom);

            if (e.Delta > 0)
                m_zoom += m_zoom * ZOOM_SPEED;
            else
                m_zoom -= m_zoom * ZOOM_SPEED;

            if (m_zoom < ZOOM_MIN)
                m_zoom = ZOOM_MIN;
            if (m_zoom > ZOOM_MAX)
                m_zoom = ZOOM_MAX;

            Vector2f mouse1 = new Vector2f(e.X / m_zoom, e.Y / m_zoom);

            m_bm.MoveBlocks(mouse1 - mouse0);

            Draw();
        }
        void m_p_MouseMove(object sender, MouseEventArgs e)
        {
            Vector2f mouse = new Vector2f(e.X / m_zoom, e.Y / m_zoom);

            if (e.Button == MouseButtons.Left)
            {
                Vector2f delta = mouse - m_lastMousePosition;
                //delta *= 1 / m_zoom;

                Block b = FindBlockUnderMouse(m_lastMousePosition);
                if (b != null)
                {
                    b.Position += delta;
                }
                else
                {
                    //foreach (var bb in m_bm.Blocks)
                    //    bb.Position += delta;
                    m_bm.MoveBlocks(delta);
                }

                Draw();
            }

            m_lastMousePosition = mouse;
        }
        void m_p_MouseUp(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void m_p_MouseDown(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void m_p_DoubleClick(object sender, MouseEventArgs e)
        {
            Vector2f mouse = new Vector2f(e.X / m_zoom, e.Y / m_zoom);

            if (e.Button == MouseButtons.Left)
            {
                Block b = FindBlockUnderMouse(mouse);
                if (b != null)
                    b.LeftMouseDoubleClick(mouse);
                Draw();
            }
        }
        void m_p_MouseClick(object sender, MouseEventArgs e)
        {
            Vector2f mouse = new Vector2f(e.X / m_zoom, e.Y / m_zoom);

            if (e.Button == MouseButtons.Left)
            {
                Block b = FindBlockUnderMouse(mouse);
                if (b != null)
                    b.LeftMouseClick(mouse);                
            }

            if (e.Button == MouseButtons.Right)
            {
                m_firstConnectionBio = null;
            }

            Draw();
        }

        internal void TryConnect(Block b, BlockIOBase bio)
        {
            if (m_firstConnectionBio == null || m_firstConnectionBio.Owner == bio.Owner)
            {
                m_firstConnectionBio = bio;
            }
            else
            {
                if (m_firstConnectionBio is BlockInput)
                {
                    if (bio is BlockInput)
                    {
                        m_firstConnectionBio = bio;
                    }
                    else
                    {
                        m_firstConnectionBio.Owner.Connect((BlockInput)m_firstConnectionBio, (BlockOutput)bio);
                        m_firstConnectionBio = null;
                    }
                }
                else
                {
                    if (bio is BlockInput)
                    {
                        bio.Owner.Connect((BlockInput)bio, (BlockOutput)m_firstConnectionBio);
                        m_firstConnectionBio = null;
                    }
                    else
                    {
                        m_firstConnectionBio = bio;
                    }
                }
            }
        }

        public void Update(bool forceRedraw)
        {
            if (m_needRefresh || forceRedraw)
            {
                m_needRefresh = false;
                Draw();
            }
        }

        void Draw()
        {
            m_ws.RefreshPreview();

            m_ws.BeginRender(new Core.Basic.Vector4f(0.3f, 0.3f, 0.3f, 0), m_zoom);
            foreach (var b in m_blocks)
            {
                b.Value.Draw(m_drawHelper);
            }

            m_drawHelper.Draw(m_ws);

           // m_ws.DrawRectangles(new[] { new Rectangle2f(5,5, m_p.Size.Width - 10, m_p.Size.Height - 10) }, new Vector4f(1, 1, 1, 1));
           // m_ws.DrawRectangles(new[] { new Rectangle2f(m_lastMousePosition, m_lastMousePosition + new Vector2f(10,10)) }, new Vector4f(1, 1, 1, 1));

            //m_ws.DrawRectangles(new[] { new Rectangle2f(m_lastMousePosition, m_lastMousePosition + new Vector2f(10, 10)) }, new Vector4f(1, 1, 1, 1));
           // m_drawHelper.AddText(new ColorText() { Text = "O", Position = m_lastMousePosition, Height = 10, Color = new Vector4f(1, 1, 1, 1) });

            m_ws.EndRender();

            m_drawHelper.Clear();
        }

        public BlockIOBase FirstConnectionBio
        {
            get { return m_firstConnectionBio; }
        }
        public IWorkSpace WorkSpace
        {
            get { return m_ws; }
        }

        #region private

        readonly IWorkSpace m_ws;
        readonly BlockManager m_bm;
        Panel m_p;

        float m_zoom = ZOOM_DEFAULT;

        void RefreshBlockList()
        {
            Dictionary<Block, object> blockInUse = new Dictionary<Block, object>();

            //enumerate blocks
            foreach (var bb in m_bm.Blocks)
            {
                Block b;
                if (!m_blocks.TryGetValue(bb, out b))
                {
                    //add new
                    b = new Block(bb, this);
                    m_blocks.Add(bb, b);
                }

                blockInUse.Add(b, null);
            }

            //remove unused
            List<BaseBlock> blocksToBeRemoved = new List<BaseBlock>();
            foreach (var kvp in m_blocks)
                if (!blockInUse.ContainsKey(kvp.Value))
                    blocksToBeRemoved.Add(kvp.Key);

            foreach (var bb in blocksToBeRemoved)
            {
                m_blocks[bb].Dispose();
                m_blocks.Remove(bb);

                m_lastBlockUnderMouse = null;
                m_firstConnectionBio = null;
            }
        }
        Block FindBlockUnderMouse(Vector2f mousePosition)
        {
            Block b = null;

            //try last
            if (m_lastBlockUnderMouse != null)
            {
                if (m_lastBlockUnderMouse.IsMouseInside(mousePosition))
                {
                    return m_lastBlockUnderMouse;
                }
            }

            //find other
            foreach (var bb in m_blocks)
            {
                if (bb.Value.IsMouseInside(mousePosition))
                {
                    b = bb.Value;
                    m_lastBlockUnderMouse = b;
                    break;
                }
            }

            return b;
        }

        Block m_lastBlockUnderMouse;
        Vector2f m_lastMousePosition;
        readonly Dictionary<BaseBlock, Block> m_blocks = new Dictionary<BaseBlock, Block>();
        readonly DrawHelper m_drawHelper = new DrawHelper();

        BlockIOBase m_firstConnectionBio = null;

        bool m_needRefresh = true;

        ProjectFile m_projectFile;

        #endregion
    }
}
