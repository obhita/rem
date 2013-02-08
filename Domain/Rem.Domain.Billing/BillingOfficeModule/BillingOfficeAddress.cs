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
    /// BillingOfficeAddress class.
    /// </summary>
    public class BillingOfficeAddress : BillingOfficeAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        #region Constants and Fields

        private Address _address;

        private BillingOfficeAddressType _billingOfficeAddressType;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOfficeAddress"/> class.
        /// </summary>
        /// <param name="billingOfficeAddressType">Type of the billing office address.</param>
        /// <param name="address">The address.</param>
        public BillingOfficeAddress (
            BillingOfficeAddressType billingOfficeAddressType,
            Address address )
        {
            Check.IsNotNull ( billingOfficeAddressType, "Billing office address type is required." );
            Check.IsNotNull ( address, "Address is required." );

            _billingOfficeAddressType = billingOfficeAddressType;
            _address = address;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOfficeAddress"/> class.
        /// </summary>
        protected internal BillingOfficeAddress ()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the address.
        /// </summary>
        public virtual Address Address
        {
            get { return _address; }
            private set { ApplyPropertyChange ( ref _address, () => Address, value ); }
        }

        /// <summary>
        /// Gets the type of the billing office address.
        /// </summary>
        public virtual BillingOfficeAddressType BillingOfficeAddressType
        {
            get { return _billingOfficeAddressType; }
            private set { ApplyPropertyChange ( ref _billingOfficeAddressType, () => BillingOfficeAddressType, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Valueses the equal.
        /// </summary>
        /// <param name="billingOfficeAddress">The billing office address.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public virtual bool ValuesEqual ( BillingOfficeAddress billingOfficeAddress )
        {
            if ( billingOfficeAddress == null )
            {
                return false;
            }

            bool valuesEqual =
                Equals ( BillingOfficeAddressType.Key, billingOfficeAddress.BillingOfficeAddressType.Key ) &&
                Equals ( Address, billingOfficeAddress.Address );

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

            return ValuesEqual ( obj as BillingOfficeAddress );
        }

        #endregion
    }
}
