using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace Core.Basic
{
   /* public struct Vector2i
    {
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X;
        public int Y;

        public override string ToString()
        {
            return string.Format("{0} {1}", X, Y);
        }

        public static Vector2i Parse(string s)
        {
            string[] ss = s.Split(' ');
            Vector2i v;
            v.X = int.Parse(ss[0], CultureInfo.InvariantCulture);
            v.Y = int.Parse(ss[1], CultureInfo.InvariantCulture);

            return v;
        }
    }*/

    public struct Vector1f
    {
        public Vector1f(float x)
        {
            X = x;
        }

        public float X;

        public override string ToString()
        {
            return string.Format("{0}", X.ToString(CultureInfo.InvariantCulture));
        }

        public static Vector1f Parse(string s)
        {
            Vector1f v;
            v.X = float.Parse(s, CultureInfo.InvariantCulture);

            return v;
        }
    }

    public struct Vector2f
    {
        public Vector2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X;
        public float Y;

        public override string ToString()
        {
            return string.Format("{0} {1}", X.ToString(CultureInfo.InvariantCulture), Y.ToString(CultureInfo.InvariantCulture));
        }

        public static Vector2f Parse(string s)
        {
            string[] ss = s.Split(' ');
            Vector2f v;
            v.X = float.Parse(ss[0], CultureInfo.InvariantCulture);
            v.Y = float.Parse(ss[1], CultureInfo.InvariantCulture);

            return v;
        }
        public static Vector2f operator *(Vector2f v0, float v)
        {
            v0.X *= v;
            v0.Y *= v;
            return v0;
        }
        public static Vector2f operator /(Vector2f v0, float v)
        {
            v0.X /= v;
            v0.Y /= v;
            return v0;
        }
        public static Vector2f operator *(Vector2f v0, Vector2f v1)
        {
            v0.X *= v1.X;
            v0.Y *= v1.Y;
            return v0;
        }
        public static Vector2f operator +(Vector2f v0, Vector2f v1)
        {
            v0.X += v1.X;
            v0.Y += v1.Y;
            return v0;
        }
        public static Vector2f operator -(Vector2f v0, Vector2f v1)
        {
            v0.X -= v1.X;
            v0.Y -= v1.Y;
            return v0;
        }

        public static bool operator ==(Vector2f v0, Vector2f v1)
        {
            return v0.X == v1.X && v0.Y == v1.Y;
        }
        public static bool operator !=(Vector2f v0, Vector2f v1)
        {
            return v0.X != v1.X || v0.Y != v1.Y;
        }
    }

    public struct Vector3f
    {
        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X;
        public float Y;
        public float Z;

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", X.ToString(CultureInfo.InvariantCulture), Y.ToString(CultureInfo.InvariantCulture), Z.ToString(CultureInfo.InvariantCulture));
        }

        public static Vector3f Parse(string s)
        {
            string[] ss = s.Split(' ');
            Vector3f v;
            v.X = float.Parse(ss[0], CultureInfo.InvariantCulture);
            v.Y = float.Parse(ss[1], CultureInfo.InvariantCulture);
            v.Z = float.Parse(ss[2], CultureInfo.InvariantCulture);

            return v;
        }

        public static Vector3f operator -(Vector3f v0, Vector3f v1)
        {
            v0.X -= v1.X;
            v0.Y -= v1.Y;
            v0.Z -= v1.Z;
            return v0;
        }
        public static Vector3f operator -(Vector3f v0)
        {
            v0.X = -v0.X;
            v0.Y = -v0.Y;
            v0.Z = -v0.Z;
            return v0;
        }

        public static Vector3f Cross(Vector3f v0, Vector3f v1)
        {
            return new Vector3f(v0.Y * v1.Z - v0.Z * v1.Y, v0.Z * v1.X - v0.X * v1.Z, v0.X * v1.Y - v0.Y * v1.X);
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Vector3f Normalize(Vector3f v)
        {
            float l = v.Length();

            if (l != 0.0f)
            {
                l = 1 / l;
                v.X *= l;
                v.Y *= l;
                v.Z *= l;
            }

            return v;
        }
    }

    public struct Vector4f
    {
        public Vector4f(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float X;
        public float Y;
        public float Z;
        public float W;

        public float this[int id]
        {
            get
            {
                if (id == 0)
                    return X;
                if (id == 1)
                    return Y;
                if (id == 2)
                    return Z;
                if (id == 3)
                    return W;

                throw new Exception("you should not be here");
            }

            set
            {
                if (id == 0)
                    X = value;
                if (id == 1)
                    Y = value;
                if (id == 2)
                    Z = value;
                if (id == 3)
                    W = value;

                if (id < 0 || id > 3)
                    throw new Exception("you should not be here");
            }
        }

        public static Vector4f operator +(Vector4f v0, Vector4f v1)
        {
            v0.X += v1.X;
            v0.Y += v1.Y;
            v0.Z += v1.Z;
            v0.W += v1.W;
            return v0;
        }
        public static Vector4f operator *(Vector4f v0, Vector4f v1)
        {
            v0.X *= v1.X;
            v0.Y *= v1.Y;
            v0.Z *= v1.Z;
            v0.W *= v1.W;
            return v0;
        }
        public static float Dot(Vector4f v0, Vector4f v1)
        {
            return v0.X * v1.X + v0.Y * v1.Y + v0.Z * v1.Z + v0.W * v1.W;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", X.ToString(CultureInfo.InvariantCulture), Y.ToString(CultureInfo.InvariantCulture), Z.ToString(CultureInfo.InvariantCulture), W.ToString(CultureInfo.InvariantCulture));
        }

        public static Vector4f Parse(string s)
        {
            string[] ss = s.Split(' ');
            Vector4f v;
            v.X = float.Parse(ss[0], CultureInfo.InvariantCulture);
            v.Y = float.Parse(ss[1], CultureInfo.InvariantCulture);
            v.Z = float.Parse(ss[2], CultureInfo.InvariantCulture);
            v.W = float.Parse(ss[3], CultureInfo.InvariantCulture);

            return v;
        }
    }

    public struct Matrix44f
    {
        public static Matrix44f MakeIdentity()
        {
            Matrix44f m;
            m.Column0 = new Vector4f(1, 0, 0, 0);
            m.Column1 = new Vector4f(0, 1, 0, 0);
            m.Column2 = new Vector4f(0, 0, 1, 0);
            m.Column3 = new Vector4f(0, 0, 0, 1);
            return m;
        }

        public Vector4f this[int id]
        {
            get
            {
                if (id == 0)
                    return Column0;
                if (id == 1)
                    return Column1;
                if (id == 2)
                    return Column2;
                if (id == 3)
                    return Column3;

                throw new Exception("you should not be here");
            }

            set
            {
                if (id == 0)
                    Column0 = value;
                if (id == 1)
                    Column1 = value;
                if (id == 2)
                    Column2 = value;
                if (id == 3)
                    Column3 = value;

                if (id < 0 || id > 3)
                    throw new Exception("you should not be here");
            }
        }

        public static Matrix44f operator *(Matrix44f m0, Matrix44f m1)
        {
            Matrix44f m = Matrix44f.MakeIdentity();

            for(int c=0; c<4; c++)
                for(int r=0; r<4; r++)
                    m.Set(c, r,
                        m0[0][r] * m1[c][0] +
                        m0[1][r] * m1[c][1] +
                        m0[2][r] * m1[c][2] +
                        m0[3][r] * m1[c][3]);

            //m0[c][0] * m1[0][r] + m0[c][1] * m1[1][r] + m0[c][2] * m1[2][r] + m0[c][3] * m1[3][r]

            return m;
        }

        public void Set(int c, int r, float v)
        {
            Vector4f col = this[c];
            col[r] = v;
            this[c] = col;
        }

        public static Matrix44f Perspective(float fov, float aspect, float zNear, float zFar)
        {
		    float range = (float)Math.Tan(fov / 2.0f) * zNear;	
		    float left = -range * aspect;
		    float right = range * aspect;
		    float bottom = -range;
		    float top = range;

            Matrix44f m = Matrix44f.MakeIdentity();
            m.Row0 = new Vector4f((2.0f * zNear) / (right - left), 0, 0, 0);
            m.Row1 = new Vector4f(0, (2.0f * zNear) / (top - bottom), 0, 0);
            m.Row2 = new Vector4f(0, 0, -(zFar + zNear) / (zFar - zNear), -1);
            m.Row3 = new Vector4f(0, 0, -(2.0f * zFar * zNear) / (zFar - zNear), 0);

		    return m;
	    }

        public static Matrix44f Translation(Vector3f v){
            Matrix44f m = Matrix44f.MakeIdentity();
            m.Row3 = new Vector4f(v.X, v.Y, v.Z, 1);
		    return m;
	    }

        public static Matrix44f LookAt(Vector3f Pos, Vector3f Target, Vector3f Up)
        {
            Vector3f F = Vector3f.Normalize(Pos - Target);
            Vector3f S = Vector3f.Normalize(Vector3f.Cross(Vector3f.Normalize(Up), F));
            Vector3f U = Vector3f.Normalize(Vector3f.Cross(F, S));

            Matrix44f rot;

            rot.Column0 = new Vector4f(S.X, S.Y, S.Z, 0);
            rot.Column1 = new Vector4f(U.X, U.Y, U.Z, 0);
            rot.Column2 = new Vector4f(F.X, F.Y, F.Z, 0);
            rot.Column3 = new Vector4f(0, 0, 0, 1);

            Matrix44f t = Matrix44f.Translation(-Pos);

	        return t * rot;
        }

        public Vector4f Column0;
        public Vector4f Column1;
        public Vector4f Column2;
        public Vector4f Column3;
        
        public Vector4f Row0
        {
            get { return new Vector4f(Column0[0], Column1[0], Column2[0], Column3[0]); }
            set { Column0[0] = value[0]; Column1[0] = value[1]; Column2[0] = value[2]; Column3[0] = value[3]; }
        }
        public Vector4f Row1
        {
            get { return new Vector4f(Column0[1], Column1[1], Column2[1], Column3[1]); }
            set { Column0[1] = value[0]; Column1[1] = value[1]; Column2[1] = value[2]; Column3[1] = value[3]; }
        }
        public Vector4f Row2
        {
            get { return new Vector4f(Column0[2], Column1[2], Column2[2], Column3[2]); }
            set { Column0[2] = value[0]; Column1[2] = value[1]; Column2[2] = value[2]; Column3[2] = value[3]; }
        }
        public Vector4f Row3
        {
            get { return new Vector4f(Column0[3], Column1[3], Column2[3], Column3[3]); }
            set { Column0[3] = value[0]; Column1[3] = value[1]; Column2[3] = value[2]; Column3[3] = value[3]; }
        }
    }

    public enum VectorMembers
    {
        None,
        X,
        Y,
        Z,
        W
    }

    public struct Line2f
    {
        public Line2f(float x0, float y0, float x1, float y1)
        {
            Point0 = new Vector2f(x0, y0);
            Point1 = new Vector2f(x1, y1);
        }
        public Line2f(Vector2f p0, Vector2f p1)
        {
            Point0 = p0;
            Point1 = p1;
        }

        public Vector2f Point0;
        public Vector2f Point1;
    }

    public struct Rectangle2f
    {
        public Rectangle2f(float x0, float y0, float x1, float y1)
        {
            Min = new Vector2f(x0, y0);
            Max = new Vector2f(x1, y1);
        }
        public Rectangle2f(Vector2f min, Vector2f max)
        {
            Min = min;
            Max = max;
        }

        public bool IsInside(Vector2f p)
        {
            Debug.Assert(Min.X <= Max.X && Min.Y <= Max.Y);
            return p.X >= Min.X && p.X <= Max.X && p.Y >= Min.Y && p.Y <= Max.Y;
        }

        public Vector2f Min;
        public Vector2f Max;
        public Vector2f Size
        {
            get { return Max - Min; }
        }

        public static Rectangle2f operator *(Rectangle2f r, float v)
        {
            r.Min *= v;
            r.Max *= v;
            return r;
        }
        public static Rectangle2f operator /(Rectangle2f r, float v)
        {
            r.Min /= v;
            r.Max /= v;
            return r;
        }
    }
}
