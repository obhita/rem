using System;
using Rem.Common.Domain;
using Rem.Common.Domain.NamingStrategy;
using Rem.Domain.AgencyModule;

namespace Rem.Domain.ClinicalCaseModule
{
    [NHibernateComponent]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class ProblemObservationInfo : IEquatable<ProblemObservationInfo>
    {
        private readonly Staff _observedByStaff;
        private readonly DateTime? _observedDate;

        private ProblemObservationInfo (Staff observedByStaff, DateTime? observedDate)
        {
            _observedByStaff = observedByStaff;
            _observedDate = observedDate;
        }

        public override bool Equals(object obj)
        {
            if ( ReferenceEquals ( null, obj ) ) return false;
            if ( ReferenceEquals ( this, obj ) ) return true;
            if ( obj.GetType () != typeof ( ProblemObservationInfo ) ) return false;
            return Equals ( ( ProblemObservationInfo ) obj );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( ProblemObservationInfo other )
        {
            if ( ReferenceEquals ( null, other ) ) return false;
            if ( ReferenceEquals ( this, other ) ) return true;
            return Equals ( other._observedByStaff, _observedByStaff ) && other._observedDate.Equals ( _observedDate );
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode ()
        {
            unchecked
            {
                return ( ( _observedByStaff != null ? _observedByStaff.GetHashCode () : 0 ) * 397 ) ^ ( _observedDate.HasValue ? _observedDate.Value.GetHashCode () : 0 );
            }
        }

        public static bool operator == ( ProblemObservationInfo left, ProblemObservationInfo right )
        {
            return Equals ( left, right );
        }

        public static bool operator != ( ProblemObservationInfo left, ProblemObservationInfo right )
        {
            return !Equals ( left, right );
        }
    }
}
