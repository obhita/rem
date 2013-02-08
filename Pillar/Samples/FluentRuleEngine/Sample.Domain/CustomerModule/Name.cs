namespace Sample.Domain.CustomerModule
{
    using System;

    using Pillar.Common.Utility;

    public class Name : IEquatable<Name>
    {
        #region Constructors and Destructors

        public Name(string first, string last)
        {
            Check.IsNotNull(first, () => First);
            Check.IsNotNull(last, () => Last);

            this.First = first;
            this.Last = last;
        }

        #endregion

        #region Public Properties

        public string First { get; private set; }

        public string Last { get; private set; }

        #endregion

        public bool Equals(Name other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.First, this.First) && Equals(other.Last, this.Last);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(Name))
            {
                return false;
            }
            return Equals((Name)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.First != null ? this.First.GetHashCode() : 0) * 397) ^ (this.Last != null ? this.Last.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Name left, Name right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Name left, Name right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", First, Last);
        }
    }
}