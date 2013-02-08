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

using System.Collections.Generic;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Billing.PayorModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Rem.WellKnownNames.PayorModule;

namespace Rem.Domain.Billing.BillingOfficeModule
{
    /// <summary>
    /// The class defines a billing office entity.
    /// </summary>
    public class BillingOffice : AuditableAggregateRootBase
    {
        #region Constants and Fields

        private readonly IList<BillingOfficeAddress> _addresses;
        private readonly IList<Payor> _payors;
        private readonly IList<PayorType> _payorTypes;
        private readonly IList<BillingOfficePhone> _phoneNumbers;
        private BillingOfficeProfile _profile;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOffice"/> class.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="administratorStaff">The administrator staff.</param>
        /// <param name="electronicTransmitterIdentificationNumber">The electronic transmitter identification number.</param>
        /// <param name="profile">The profile.</param>
        protected internal BillingOffice (
            Agency agency, Staff administratorStaff, string electronicTransmitterIdentificationNumber, BillingOfficeProfile profile )
            : this ()
        {
            Check.IsNotNull ( agency, () => Agency );
            Check.IsNotNull ( administratorStaff, () => AdministratorStaff );
            Check.IsNotNull ( electronicTransmitterIdentificationNumber, () => ElectronicTransmitterIdentificationNumber );
            Check.IsNotNull ( profile, () => Profile );
            Agency = agency;
            AdministratorStaff = administratorStaff;
            ElectronicTransmitterIdentificationNumber = electronicTransmitterIdentificationNumber;
            Profile = profile;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOffice"/> class.
        /// </summary>
        protected internal BillingOffice ()
        {
            _payors = new List<Payor> ();
            _addresses = new List<BillingOfficeAddress> ();
            _phoneNumbers = new List<BillingOfficePhone> ();
            _payorTypes = new List<PayorType> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        public virtual IEnumerable<BillingOfficeAddress> Addresses
        {
            get { return _addresses; }
            private set { }
        }

        /// <summary>
        /// Gets the administrator staff.
        /// </summary>
        [NotNull]
        public virtual Staff AdministratorStaff { get; private set; }

        /// <summary>
        /// Gets the agency.
        /// </summary>
        [NotNull]
        [Unique]
        public virtual Agency Agency { get; private set; }

        /// <summary>
        /// Gets the electronic transmitter identification number.
        /// </summary>
        /// <value>The electronic transmitter identification number.</value>
        [NotNull]
        public virtual string ElectronicTransmitterIdentificationNumber { get; private set; }

        /// <summary>
        /// Gets the payors.
        /// </summary>
        public virtual IEnumerable<Payor> Payors
        {
            get { return _payors; }
            private set { }
        }

        /// <summary>
        /// Gets the payor types.
        /// </summary>
        public virtual IEnumerable<PayorType>  PayorTypes
        {
            get { return _payorTypes; }
            private set { }
        }

        /// <summary>
        /// Gets the phone numbers.
        /// </summary>
        public virtual IEnumerable<BillingOfficePhone> PhoneNumbers
        {
            get { return _phoneNumbers; }
            private set { }
        }

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <value>The profile.</value>
        public virtual BillingOfficeProfile Profile
        {
            get { return _profile; }
            private set { ApplyPropertyChange ( ref _profile, () => Profile, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the Address.
        /// </summary>
        /// <param name="address">The address.</param>
        public virtual void AddAddress ( BillingOfficeAddress address )
        {
            Check.IsNotNull ( address, "Address is required." );
            address.BillingOffice = this;
            _addresses.Add ( address );
            NotifyItemAdded ( () => Addresses, address );
        }


        /// <summary>
        /// Adds the payor.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="electronicTransmitterIdentificationNumber">The electronic transmitter identification number.</param>
        /// <returns>A payor.</returns>
        public virtual Payor AddPayor(string name, string electronicTransmitterIdentificationNumber)
        {
            InitializeServices();
            var factory = IoC.CurrentContainer.Resolve<IPayorFactory>();
            var payor = factory.CreatePayor ( name, this, electronicTransmitterIdentificationNumber );
            _payors.Add ( payor );
            NotifyItemAdded ( () => Payors, payor );
            return payor;
        }


        /// <summary>
        /// Adds the type of the payor.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="billingForm">The billing form.</param>
        /// <returns>A payor type.</returns>
        public virtual PayorType AddPayorType(string name, BillingForm billingForm)
        {
            InitializeServices();
            var factory = IoC.CurrentContainer.Resolve<IPayorTypeFactory>();
            var payorType = factory.CreatePayorType(name, this, billingForm);
            _payorTypes.Add(payorType);
            NotifyItemAdded(() => PayorTypes, payorType);
            return payorType;
        }


        /// <summary>
        /// Adds the Phone Number.
        /// </summary>
        /// <param name="phone">The phone number.</param>
        public virtual void AddPhoneNumber ( BillingOfficePhone phone )
        {
            Check.IsNotNull ( phone, "Phone number is required." );
            phone.BillingOffice = this;
            _phoneNumbers.Add ( phone );
            NotifyItemAdded ( () => PhoneNumbers, phone );
        }

        /// <summary>
        /// Removes the address.
        /// </summary>
        /// <param name="address">The address.</param>
        public virtual void RemoveAddress ( BillingOfficeAddress address )
        {
            Check.IsNotNull ( address, "Address is required." );
            _addresses.Remove ( address );
            NotifyItemRemoved ( () => Addresses, address );
        }

        /// <summary>
        /// Removes the payor.
        /// </summary>
        /// <param name="payor">The payor.</param>
        public virtual void RemovePayor ( Payor payor )
        {
            Check.IsNotNull ( payor, "Payor is required." );
            _payors.Remove ( payor );
            NotifyItemRemoved ( () => Payors, payor );
        }


        /// <summary>
        /// Removes the type of the payor.
        /// </summary>
        /// <param name="payorType">Type of the payor.</param>
        public virtual void RemovePayorType(PayorType payorType)
        {
            Check.IsNotNull(payorType, "Payor Type is required.");
            _payorTypes.Remove(payorType);
            NotifyItemRemoved(() => PayorTypes, payorType);
        }

        /// <summary>
        /// Removes the phone number.
        /// </summary>
        /// <param name="phone">The phone number.</param>
        public virtual void RemovePhoneNumber ( BillingOfficePhone phone )
        {
            Check.IsNotNull ( phone, "Phone number is required." );
            _phoneNumbers.Remove ( phone );
            NotifyItemRemoved ( () => PhoneNumbers, phone );
        }

        /// <summary>
        /// Revises the administrator staff.
        /// </summary>
        /// <param name="administratorStaff">The administrator staff.</param>
        public virtual void ReviseAdministratorStaff ( Staff administratorStaff )
        {
            Check.IsNotNull ( administratorStaff, () => AdministratorStaff );
            AdministratorStaff = administratorStaff;
        }

        /// <summary>
        /// Revises the agency.
        /// </summary>
        /// <param name="agency">The agency.</param>
        public virtual void ReviseAgency ( Agency agency )
        {
            Check.IsNotNull ( agency, () => Agency );
            Agency = agency;
        }

        /// <summary>
        /// Revises the electronic transmitter identification number.
        /// </summary>
        /// <param name="electronicTransmitterIdentificationNumber">The electronic transmitter identification number.</param>
        public virtual void ReviseElectronicTransmitterIdentificationNumber ( string electronicTransmitterIdentificationNumber )
        {
            Check.IsNotNull ( electronicTransmitterIdentificationNumber, () => ElectronicTransmitterIdentificationNumber );
            ElectronicTransmitterIdentificationNumber = electronicTransmitterIdentificationNumber;
        }

        /// <summary>
        /// Revises the profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public virtual void ReviseProfile ( BillingOfficeProfile profile )
        {
            Check.IsNotNull ( profile, () => Profile );
            Profile = profile;
        }

        #endregion
    }
}
