namespace Sample.Domain.CustomerModule
{
    using System;

    public class Address : IEquatable<Address>
    {
        #region Constructors and Destructors

        public Address(
            string street1,
            string street2,
            string city,
            string state,
            string zipcode)
        {
            this.Street1 = street1;
            this.Street2 = street2;
            this.City = city;
            this.State = state;
            this.Zipcode = zipcode;
        }

        #endregion

        #region Public Properties

        public string City { get; private set; }

        public string State { get; private set; }

        public string Street1 { get; private set; }

        public string Street2 { get; private set; }

        public string Zipcode { get; private set; }

        #endregion

        #region Public Methods

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return
                Equals(other.Street1, this.Street1) &&
                Equals(other.Street2, this.Street2) &&
                Equals(other.City, this.City) &&
                Equals(other.State, this.State) &&
                Equals(other.Zipcode, this.Zipcode);
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
            if (obj.GetType() != typeof(Address))
            {
                return false;
            }
            return this.Equals((Address)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = (this.Street1 != null ? this.Street1.GetHashCode() : 0);
                result = (result * 397) ^ (this.Street2 != null ? this.Street2.GetHashCode() : 0);
                result = (result * 397) ^ (this.City != null ? this.City.GetHashCode() : 0);
                result = (result * 397) ^ (this.State != null ? this.State.GetHashCode() : 0);
                result = (result * 397) ^ (this.Zipcode != null ? this.Zipcode.GetHashCode() : 0);
                return result;
            }
        }

        #endregion

        #region Operators

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}