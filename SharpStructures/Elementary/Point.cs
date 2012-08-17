using System;

namespace SharpStructures.Elementary
{
    public struct Point<T> : IEquatable<Point<T>>
    {
        private readonly T _x;
        private readonly T _y;

        public Point(T x, T y)
        {
            _y = y;
            _x = x;
        }

        public T X
        {
            get { return _x; }
        }

        public T Y
        {
            get { return _y; }
        }

        #region IEquatable<Point<T>> Members

        public bool Equals(Point<T> other)
        {
            return this == other;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Point<T>)) return false;
            var other = (Point<T>) obj;
            return this._x.Equals(other._x) && this._y.Equals(other._y);
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", _x, _y);
        }

        public static bool operator ==(Point<T> p1, Point<T> p2)
        {
            return p1.X.Equals(p2.X) && p1.Y.Equals(p2.Y);
        }

        public static bool operator !=(Point<T> p1, Point<T> p2)
        {
            return !p1.X.Equals(p2.X) || !p1.Y.Equals(p2.Y);
        }
    }
}