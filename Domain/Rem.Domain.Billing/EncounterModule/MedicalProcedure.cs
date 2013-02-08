using System;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.EncounterModule
{
    /// <summary>
    /// This class defines the procedure for visit/activity under coding context.
    /// </summary>
    [Component]
    public class MedicalProcedure : IEquatable<MedicalProcedure>
    {
        private MedicalProcedure()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalProcedure"/> class.
        /// </summary>
        /// <param name="procedureCode">The procedure code.</param>
        /// <param name="firstModifierCode">The first modifier code.</param>
        /// <param name="secondModifierCode">The second modifier code.</param>
        /// <param name="thirdModifierCode">The third modifier code.</param>
        /// <param name="fourthModifierCode">The fourth modifier code.</param>
        public MedicalProcedure(CodedConcept procedureCode, CodedConcept firstModifierCode, CodedConcept secondModifierCode, CodedConcept thirdModifierCode, CodedConcept fourthModifierCode)
        {
            Check.IsNotNull(procedureCode, "Procedure code is required.");

            ProcedureCode = procedureCode;
            FirstModifierCode = firstModifierCode;
            SecondModifierCode = secondModifierCode;
            ThirdModifierCode = thirdModifierCode;
            FourthModifierCode = fourthModifierCode;
        }

        /// <summary>
        /// Gets the procedure code.
        /// </summary>
        public CodedConcept ProcedureCode { get; private set; }

        /// <summary>
        /// Gets the first modifier code.
        /// </summary>
        public CodedConcept FirstModifierCode { get; private set; }

        /// <summary>
        /// Gets the second modifier code.
        /// </summary>
        public CodedConcept SecondModifierCode { get; private set; }

        /// <summary>
        /// Gets the third modifier code.
        /// </summary>
        public CodedConcept ThirdModifierCode { get; private set; }

        /// <summary>
        /// Gets the four modifier code.
        /// </summary>
        public CodedConcept FourthModifierCode { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType () != typeof( MedicalProcedure ) )
            {
                return false;
            }
            return Equals ( ( MedicalProcedure )obj );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( MedicalProcedure other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.ProcedureCode, ProcedureCode ) && Equals ( other.FirstModifierCode, FirstModifierCode ) && Equals ( other.SecondModifierCode, SecondModifierCode ) && Equals ( other.ThirdModifierCode, ThirdModifierCode ) && Equals ( other.FourthModifierCode, FourthModifierCode );
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
                int result = ( ProcedureCode != null ? ProcedureCode.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( FirstModifierCode != null ? FirstModifierCode.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( SecondModifierCode != null ? SecondModifierCode.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( ThirdModifierCode != null ? ThirdModifierCode.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( FourthModifierCode != null ? FourthModifierCode.GetHashCode () : 0 );
                return result;
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == ( MedicalProcedure left, MedicalProcedure right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator != ( MedicalProcedure left, MedicalProcedure right )
        {
            return !Equals ( left, right );
        }
    }
}
