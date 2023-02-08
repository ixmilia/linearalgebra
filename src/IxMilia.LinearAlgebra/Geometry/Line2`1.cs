using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Line2<T> : Matrix<T>
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

        public bool IsInfinity => Computer.IsZero(A) && Computer.IsZero(B) && Computer.IsOne(C);

        private Line2(IAlgebraicComputer<T> computer)
            : base(computer, 3, 1)
        {
        }

        public Line2(IAlgebraicComputer<T> computer, T a, T b, T c)
            : this(computer)
        {
            if (computer.IsZero(a) && computer.IsZero(b) && computer.IsZero(c))
            {
                throw new InvalidOperationException("The specified line is not valid");
            }

            A = a;
            B = b;
            C = c;
        }

        public bool ContainsPoint(Vector2<T> point)
        {
            var scalar = (Transpose * point).Scalar;
            return Computer.IsZero(scalar);
        }

        public bool IsParallelTo(Line2<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Line2.Intersection(this, other).IsInfinity;
        }
    }
}
