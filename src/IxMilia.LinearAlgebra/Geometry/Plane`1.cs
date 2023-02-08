using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Plane<T> : Matrix<T>
    {
        public T A
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public T B
        {
            get { return this[1, 0]; }
            set { this[1, 0] = value; }
        }

        public T C
        {
            get { return this[2, 0]; }
            set { this[2, 0] = value; }
        }

        public T D
        {
            get { return this[3, 0]; }
            set { this[3, 0] = value; }
        }

        public Vector3<T> Normal
        {
            get
            {
                var normal = new Vector3<T>(Computer, A, B, C, D);
                normal.TryNormalize();
                return normal;
            }
        }

        public T Norm => Normal.Length;

        public T DistanceFromOrigin
        {
            get
            {
                var norm = Norm;
                if (Computer.IsZero(norm))
                {
                    throw new InvalidOperationException("The infinity plane has no distance from the origin");
                }

                return Computer.Divide(D, norm);
            }
        }

        public Plane(IAlgebraicComputer<T> computer)
            : base(computer, 4, 1)
        {
        }

        public Plane(IAlgebraicComputer<T> computer, T a, T b, T c, T d)
            : this(computer)
        {
            if (computer.IsZero(a) && computer.IsZero(b) && computer.IsZero(c))
            {
                throw new InvalidOperationException($"At least one coefficient ({nameof(a)}, {nameof(b)}, or {nameof(c)}) must be non-zero");
            }

            A = a;
            B = b;
            C = c;
            D = d;
        }

        public bool ContainsPoint(Vector3<T> point)
        {
            return Computer.IsZero((Transpose * point).Scalar);
        }
    }
}
