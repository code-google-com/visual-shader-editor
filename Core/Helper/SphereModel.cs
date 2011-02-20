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

namespace Core.Helper
{
    public class SphereModel
    {
        public readonly Vector3f[] POSITON0;
        public readonly Vector2f[] TEXCOORD0;
        public readonly Vector4f[] COLOR0;
        public readonly Vector3f[] NORMAL0;
        public readonly Vector3f[] BINORMAL0;
        public readonly Vector3f[] TANGENT0;

        public readonly int[] INDICES;

        public readonly int VerticesCount;
        public readonly int IndicesCount;

        public readonly int RingsCount = 20;
        public readonly int SegmentCount = 20;
        public readonly float Radius = 1;

        public readonly int Stride = 18 * 4;

        public readonly int PosOffset = 0;
        public readonly int TCoordOffset = 3 * 4;
        public readonly int ColorOffset = 5 * 4;
        public readonly int NormalOffset = 9 * 4;
        public readonly int BinormalOffset = 12 * 4;
        public readonly int TangentOffset = 15 * 4;

        public SphereModel()
        {
            VerticesCount = (RingsCount + 1) * (SegmentCount + 1);
            IndicesCount = (RingsCount) * (SegmentCount+1) * 6;

            //generate array
            POSITON0 = new Vector3f[VerticesCount];
            TEXCOORD0 = new Vector2f[VerticesCount];
            COLOR0 = new Vector4f[VerticesCount];
            NORMAL0 = new Vector3f[VerticesCount];
            BINORMAL0 = new Vector3f[VerticesCount];
            TANGENT0 = new Vector3f[VerticesCount];

            INDICES = new int[IndicesCount];

            GenerateMesh();
        }

        void GenerateMesh()
        {
            Random r = new Random();

            //generate mesh
            float fDeltaRingAngle = (float)(Math.PI / RingsCount);
            float fDeltaSegAngle = (float)(2 * Math.PI / SegmentCount);
            int indexId = 0;
            int verticeId = 0;

            // Generate the group of rings for the sphere
            for (int ring = 0; ring <= RingsCount; ring++)
            {
                float r0 = Radius * (float)Math.Sin(ring * fDeltaRingAngle);
                float y0 = Radius * (float)Math.Cos(ring * fDeltaRingAngle);

                // Generate the group of segments for the current ring
                for (int seg = 0; seg <= SegmentCount; seg++)
                {
                    float x0 = r0 * (float)Math.Sin(seg * fDeltaSegAngle);
                    float z0 = r0 * (float)Math.Cos(seg * fDeltaSegAngle);

                    // Add one vertex to the strip which makes up the sphere
                    POSITON0[verticeId].X = x0;
                    POSITON0[verticeId].Y = y0;
                    POSITON0[verticeId].Z = z0;

                    NORMAL0[verticeId] = Vector3f.Normalize(POSITON0[verticeId]);

                    TEXCOORD0[verticeId].X = (float)seg / (float)SegmentCount;
                    TEXCOORD0[verticeId].Y = (float)ring / (float)RingsCount;

                    COLOR0[verticeId] = new Vector4f((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());

                    TANGENT0[verticeId] = Vector3f.Cross(new Vector3f(0, 1, 0), NORMAL0[verticeId]);
                    BINORMAL0[verticeId] = Vector3f.Cross(NORMAL0[verticeId], TANGENT0[verticeId]);

                    if (ring != RingsCount)
                    {
                        // each vertex (except the last) has six indices pointing to it
                        INDICES[indexId++] = verticeId + SegmentCount + 1;
                        INDICES[indexId++] = verticeId;
                        INDICES[indexId++] = verticeId + SegmentCount;
                        INDICES[indexId++] = verticeId + SegmentCount + 1;
                        INDICES[indexId++] = verticeId + 1;
                        INDICES[indexId++] = verticeId;
                    }
                    verticeId++;
                } // end for seg
            } // end for ring
            int i = 0;
        }
    }
}
