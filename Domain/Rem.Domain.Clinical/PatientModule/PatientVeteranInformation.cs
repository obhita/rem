using System;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;
using Pillar.Domain.Primitives;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientVeteranInformation defines patient veteran information.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof( PropertyNameAsColumnNameNamingStrategy ) )]
    public class PatientVeteranInformation : IEquatable<PatientVeteranInformation>
    {
        private PatientVeteranInformation ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientVeteranInformation"/> class.
        /// </summary>
        /// <param name="disabilityDescription">
        /// The disability description.
        /// </param>
        /// <param name="disabilityPercentageValue">
        /// The disability percentage value.
        /// </param>
        /// <param name="haveCombatHistoryIndicator">
        /// The have Combat History Indicator.
        /// </param>
        /// <param name="haveServedInMilitaryIndicator">
        /// The have served in military indicator.
        /// </param>
        /// <param name="registeredVaHospitalName">
        /// The registered VA hospital name.
        /// </param>
        /// <param name="serviceDateRange">
        /// The service date range.
        /// </param>
        /// <param name="vaCaseNumber">
        /// The VA case number.
        /// </param>
        /// <param name="veteranDischargeStatus">
        /// The veteran discharge status.
        /// </param>
        /// <param name="veteranServiceBranch">
        /// The veteran service branch.
        /// </param>
        /// <param name="veteranStatus">
        /// The veteran status.
        /// </param>
        public PatientVeteranInformation (
            string disabilityDescription, 
            string disabilityPercentageValue,
            bool? haveCombatHistoryIndicator,
            bool? haveServedInMilitaryIndicator, 
            string registeredVaHospitalName, 
            DateRange serviceDateRange, 
            string vaCaseNumber, 
            VeteranDischargeStatus veteranDischargeStatus, 
            VeteranServiceBranch veteranServiceBranch, 
            VeteranStatus veteranStatus )
        {
            DisabilityDescription = disabilityDescription;
            DisabilityPercentageValue = disabilityPercentageValue;
            HaveCombatHistoryIndicator = haveCombatHistoryIndicator;
            HaveServedInMilitaryIndicator = haveServedInMilitaryIndicator;
            RegisteredVaHospitalName = registeredVaHospitalName;
            ServiceDateRange = serviceDateRange;
            VaCaseNumber = vaCaseNumber;
            VeteranDischargeStatus = veteranDischargeStatus;
            VeteranServiceBranch = veteranServiceBranch;
            VeteranStatus = veteranStatus;
        }

        /// <summary>
        /// Gets the disability description.
        /// </summary>
        public string DisabilityDescription { get; private set; }

        /// <summary>
        /// Gets the disability percentage value.
        /// </summary>
        public string DisabilityPercentageValue { get; private set; }

        /// <summary>
        /// Gets the have combat history indicator.
        /// </summary>
        public bool? HaveCombatHistoryIndicator { get; private set; }

        /// <summary>
        /// Gets the have served in military indicator.
        /// </summary>
        public bool? HaveServedInMilitaryIndicator { get; private set; }

        /// <summary>
        /// Gets the name of the registered va hospital.
        /// </summary>
        /// <value>
        /// The name of the registered va hospital.
        /// </value>
        public string RegisteredVaHospitalName { get; private set; }

        /// <summary>
        /// Gets the service date range.
        /// </summary>
        public DateRange ServiceDateRange { get; private set; }

        /// <summary>
        /// Gets the va case number.
        /// </summary>
        public string VaCaseNumber { get; private set; }

        /// <summary>
        /// Gets the veteran discharge status.
        /// </summary>
        public VeteranDischargeStatus VeteranDischargeStatus { get; private set; }

        /// <summary>
        /// Gets the veteran service branch.
        /// </summary>
        public VeteranServiceBranch VeteranServiceBranch { get; private set; }

        /// <summary>
        /// Gets the veteran status.
        /// </summary>
        public VeteranStatus VeteranStatus { get; private set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(PatientVeteranInformation left, PatientVeteranInformation right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(PatientVeteranInformation left, PatientVeteranInformation right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( PatientVeteranInformation other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.DisabilityDescription, DisabilityDescription ) && Equals ( other.DisabilityPercentageValue, DisabilityPercentageValue ) && other.HaveCombatHistoryIndicator.Equals ( HaveCombatHistoryIndicator ) && other.HaveServedInMilitaryIndicator.Equals ( HaveServedInMilitaryIndicator ) && Equals ( other.RegisteredVaHospitalName, RegisteredVaHospitalName ) && Equals ( other.ServiceDateRange, ServiceDateRange ) && Equals ( other.VaCaseNumber, VaCaseNumber ) && Equals ( other.VeteranDischargeStatus, VeteranDischargeStatus ) && Equals ( other.VeteranServiceBranch, VeteranServiceBranch ) && Equals ( other.VeteranStatus, VeteranStatus );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType () != typeof( PatientVeteranInformation ) )
            {
                return false;
            }
            return Equals ( ( PatientVeteranInformation )obj );
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
                int result = DisabilityDescription != null ? DisabilityDescription.GetHashCode () : 0;
                result = ( result * 397 ) ^ ( DisabilityPercentageValue != null ? DisabilityPercentageValue.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( HaveCombatHistoryIndicator.HasValue ? HaveCombatHistoryIndicator.Value.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( HaveServedInMilitaryIndicator.HasValue ? HaveServedInMilitaryIndicator.Value.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( RegisteredVaHospitalName != null ? RegisteredVaHospitalName.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( ServiceDateRange != null ? ServiceDateRange.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( VaCaseNumber != null ? VaCaseNumber.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( VeteranDischargeStatus != null ? VeteranDischargeStatus.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( VeteranServiceBranch != null ? VeteranServiceBranch.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( VeteranStatus != null ? VeteranStatus.GetHashCode () : 0 );
                return result;
            }
        }
    }
}
