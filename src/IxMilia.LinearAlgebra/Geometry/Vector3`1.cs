using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Vector3<T> : Matrix<T>
    {
        public T X
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public T Y
        {
            get { return this[1, 0]; }
            set { this[1, 0] = value; }
        }

        public T Z
        {
            get { return this[2, 0]; }
            set { this[2, 0] = value; }
        }

        public T W
        {
            get { return this[3, 0]; }
            set { this[3, 0] = value; }
        }

        public T LengthSquared => Computer.Add(Computer.Square(X), Computer.Square(Y), Computer.Square(Z));
        
        public T Length => Computer.SquareRoot(LengthSquared);

        public bool IsInfinity => Computer.IsZero(W);

        private Vector3(IAlgebraicComputer<T> computer)
            : base(computer, 4, 1)
        {
        }

        public Vector3(IAlgebraicComputer<T> computer, T x, T y, T z)
            : this(computer, x, y, z, computer.One)
        {
        }

        public Vector3(IAlgebraicComputer<T> computer, T x, T y, T z, T w)
            : this(computer)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public void TryNormalize()
        {
            if (Computer.IsZero(W))
            {
                return;
            }

            X = Computer.Divide(X, W);
            Y = Computer.Divide(Y, W);
            Z = Computer.Divide(Z, W);
            W = Computer.One;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public static Vector3<T> operator +(Vector3<T> v1, Vector3<T> v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            var computer = v1.Computer;
            return new Vector3<T>(computer, computer.Add(v1.X, v2.X), computer.Add(v1.Y, v2.Y), computer.Add(v1.Z, v2.Z), computer.Add(v1.W, v2.W));
        }

        public static Vector3<T> operator -(Vector3<T> v1, Vector3<T> v2)
        {
            return v1 + (-v2);
        }

        public static Vector3<T> operator -(Vector3<T> v1)
        {
            var computer = v1.Computer;
            return (v1 * computer.Negate(computer.One)).AsVector3();
        }
    }
}
