using System;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Clinical.DensAsiModule.NamingStrategy;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// <see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiNonResponseType`1">DensAsiNonResponseType&lt;T&gt;</see> provides a list of possible 
    /// answer values when a direct answer is not provided for the question by the patient. 
    /// </summary>
    /// <typeparam name="T">The non response type.</typeparam>
    [Component]
    [ComponentNamingStrategy(typeof(DensAsiNonResponseTypeNamingStrategy))]
    public struct DensAsiNonResponseType<T> : IEquatable<DensAsiNonResponseType<T>>
    {
        private readonly DensAsiNonResponse _densAsiNonResponse;
        private readonly T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiNonResponseType&lt;T&gt;"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public DensAsiNonResponseType(T value)
        {
            Check.IsNotNull(value, "Value cannot be null.");
            _value = value;
            _densAsiNonResponse = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiNonResponseType&lt;T&gt;"/> struct.
        /// </summary>
        /// <param name="densAsiNonResponse">The dens asi non response.</param>
        public DensAsiNonResponseType(DensAsiNonResponse densAsiNonResponse)
        {
            Check.IsNotNull(densAsiNonResponse, "DensAsiNonResponse cannot equal null.");
            _densAsiNonResponse = densAsiNonResponse;
            _value = (T)typeof(T).GetDefault();
        }

        /// <summary>
        /// Gets the T value.
        /// </summary>
        public T Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Gets the DensAsi non response.
        /// </summary>
        public DensAsiNonResponse DensAsiNonResponse
        {
            get
            {
                return _densAsiNonResponse;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        [IgnoreMapping]
        public bool HasValue
        {
            get
            {
                return !Equals(_value, default(T));
            }
        }

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var ret = string.Empty;

            if (!Equals(Value, typeof(T).GetDefault()))
            {
                ret = Value.ToString();
            }
            else if (DensAsiNonResponse != null)
            {
                ret = DensAsiNonResponse.Name;
            }

            return ret;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return (this as IEquatable<DensAsiNonResponseType<T>>).Equals((DensAsiNonResponseType<T>)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 0;
            if (!Equals(Value, typeof(T).GetDefault()))
            {
                hash ^= Value.GetHashCode();
            }
            else if (DensAsiNonResponse != null)
            {
                hash ^= DensAsiNonResponse.GetHashCode();
            }
            return hash;
        }

        #endregion

        #region IEquatable<DensAsiNonResponseType<T>> interface

        bool IEquatable<DensAsiNonResponseType<T>>.Equals(DensAsiNonResponseType<T> other)
        {
            if (GetType() != other.GetType())
            {
                return false;
            }

            if (Equals(Value, other.Value) && Equals(DensAsiNonResponse, other.DensAsiNonResponse))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
