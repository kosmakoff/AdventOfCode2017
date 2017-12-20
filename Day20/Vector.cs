using System;
using System.Collections.Generic;
using System.Text;

namespace Day20
{
    internal struct Vector
    {
        public long X { get; }
        public long Y { get; }
        public long Z { get; }

        public Vector(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(checked(a.X + b.X), checked(a.Y + b.Y), checked(a.Z + b.Z));
        }

        public static Vector operator *(Vector a, long number)
        {
            return new Vector(a.X * number, a.Y * number, a.Z * number);
        }

        public static Vector operator *(long number, Vector a)
        {
            return new Vector(a.X * number, a.Y * number, a.Z * number);
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        public override string ToString()
        {
            return $"<{X};{Y};{Z}>";
        }

        public long ManhattanDistance => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        private sealed class XYZEqualityComparer : IEqualityComparer<Vector>
        {
            public bool Equals(Vector x, Vector y)
            {
                return x.X == y.X && x.Y == y.Y && x.Z == y.Z;
            }

            public int GetHashCode(Vector obj)
            {
                unchecked
                {
                    var hashCode = obj.X.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Y.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Z.GetHashCode();
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<Vector> XYZComparer { get; } = new XYZEqualityComparer();
    }
}
