using System;
using System.Diagnostics.CodeAnalysis;

namespace TDDStudies
{
    class Pair : IEquatable<Pair>
    {
        private string _from;
        private string _to;

        public Pair(string from, string to)
        {
            _from = from;
            _to = to;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Pair);
        }

        public bool Equals([AllowNull] Pair other)
        {
            return other != null &&
                   _from == other._from &&
                   _to == other._to;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_from, _to);
        }
    }
}
