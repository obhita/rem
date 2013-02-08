#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using Pillar.Common;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.BillingOfficeModule
{
    /// <summary>
    /// BillingOfficePhone class.
    /// </summary>
    public class BillingOfficePhone : BillingOfficeAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        #region Constants and Fields

        private BillingOfficePhoneType _billingOfficePhoneType;
        private Phone _phone;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOfficePhone"/> class.
        /// </summary>
        /// <param name="billingOfficePhoneType">Type of the billing office phone.</param>
        /// <param name="phone">The phone number.</param>
        public BillingOfficePhone (
            BillingOfficePhoneType billingOfficePhoneType,
            Phone phone )
        {
            Check.IsNotNull ( billingOfficePhoneType, "Billing office phone type is required." );
            Check.IsNotNull ( phone, "Phone number is required." );

            _billingOfficePhoneType = billingOfficePhoneType;
            _phone = phone;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOfficePhone"/> class.
        /// </summary>
        protected internal BillingOfficePhone ()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type of the billing office phone.
        /// </summary>
        public virtual BillingOfficePhoneType BillingOfficePhoneType
        {
            get { return _billingOfficePhoneType; }
            private set { ApplyPropertyChange ( ref _billingOfficePhoneType, () => BillingOfficePhoneType, value ); }
        }

        /// <summary>
        /// Gets the Phone.
        /// </summary>
        public virtual Phone Phone
        {
            get { return _phone; }
            private set { ApplyPropertyChange ( ref _phone, () => Phone, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Valueses the equal.
        /// </summary>
        /// <param name="billingOfficePhone">The billing office phone.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public virtual bool ValuesEqual ( BillingOfficePhone billingOfficePhone )
        {
            if ( billingOfficePhone == null )
            {
                return false;
            }

            bool valuesEqual =
                Equals ( BillingOfficePhoneType.Key, billingOfficePhone.BillingOfficePhoneType.Key ) &&
                Equals ( Phone, billingOfficePhone.Phone );

            return valuesEqual;
        }

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>A boolean denoting equality of the values.</returns>
        public virtual bool ValuesEqual ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( GetType () != obj.GetType () )
            {
                return false;
            }

            return ValuesEqual ( obj as BillingOfficePhone );
        }

        #endregion
    }
}
