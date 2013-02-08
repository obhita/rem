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
using System.Linq;
using Pillar.Common.Extension;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// Defines the payor profile.
    /// </summary>
    public class Payor : AuditableAggregateRootBase
    {
        #region Constants and Fields

        private readonly IList<PayorAddress> _addresses;
        private readonly IList<PayorPhone> _phoneNumbers;
        private readonly IList<PayorTypeMember> _payorTypeMembers; 

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Payor"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="billingOffice">The billing office.</param>
        /// <param name="electronicTransmitterIdentificationNumber">The electronic transmitter identification number.</param>
        public Payor ( string name, BillingOffice billingOffice, string electronicTransmitterIdentificationNumber )
            : this ()
        {
            Check.IsNotNull ( name, () => Name );
            Check.IsNotNull ( billingOffice, () => BillingOffice );
            Check.IsNotNull ( electronicTransmitterIdentificationNumber, () => ElectronicTransmitterIdentificationNumber );

            Name = name;
            BillingOffice = billingOffice;
            ElectronicTransmitterIdentificationNumber = electronicTransmitterIdentificationNumber;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Payor"/> class.
        /// </summary>
        protected internal Payor ()
        {
            _addresses = new List<PayorAddress> ();
            _phoneNumbers = new List<PayorPhone> ();
            _payorTypeMembers = new List<PayorTypeMember> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the billing office.
        /// </summary>
        [NotNull]
        public virtual BillingOffice BillingOffice { get; private set; }

        /// <summary>
        /// Gets the payor type members.
        /// </summary>
        public virtual IEnumerable<PayorTypeMember> PayorTypeMembers
        {
            get { return _payorTypeMembers.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the primary payor type member.
        /// </summary>
        [NoneCascading]
        public virtual PayorTypeMember PrimaryPayorTypeMember { get; private set; }

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        public virtual IEnumerable<PayorAddress> Addresses
        {
            get { return _addresses.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the effective date range.
        /// </summary>
        public virtual DateRange EffectiveDateRange { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string ElectronicTransmitterIdentificationNumber { get; private set; }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public virtual EmailAddress EmailAddress { get; private set; }
        
        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string Name { get; private set; }

        /// <summary>
        /// Gets the payor identifier.
        /// </summary>
        public virtual string PayorIdentifier { get; private set; }
        
        /// <summary>
        /// Gets the phone numbers.
        /// </summary>
        public virtual IEnumerable<PayorPhone> PhoneNumbers
        {
            get { return _phoneNumbers.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the website address.
        /// </summary>
        public virtual string WebsiteAddress { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Revises the billing office.
        /// </summary>
        /// <param name="billingOffice">The billing office.</param>
        public virtual void ReviseBillingOffice ( BillingOffice billingOffice)
        {
            Check.IsNotNull ( billingOffice, () => BillingOffice );
            BillingOffice = billingOffice;
        }

        /// <summary>
        /// Adds the payor type member.
        /// </summary>
        /// <param name="payorType">Type of the payor.</param>
        public virtual void AddPayorTypeMember(PayorType payorType)
        {
            var payorTypeMember = new PayorTypeMember(payorType, this);
            _payorTypeMembers.Add(payorTypeMember);
            NotifyItemAdded(() => PayorTypeMembers, payorTypeMember);
        }

        /// <summary>
        /// Removes the payor type member.
        /// </summary>
        /// <param name="payorTypeMember">The payor type member.</param>
        public virtual void RemovePayorTypeMember(PayorTypeMember payorTypeMember)
        {
            Check.IsNotNull(payorTypeMember, "Payor type member is required.");

            var existingTypeMember = _payorTypeMembers.FirstOrDefault ( m => m.PayorType.Key == payorTypeMember.PayorType.Key );
            if (existingTypeMember != null)
            {
                _payorTypeMembers.Remove(existingTypeMember);
                NotifyItemRemoved(() => PayorTypeMembers, payorTypeMember);
            }
        }

        /// <summary>
        /// Revises the primary payor type member.
        /// </summary>
        /// <param name="payorType">Type of the payor.</param>
        public virtual void RevisePrimaryPayorTypeMember(PayorType payorType)
        {
            Check.IsNotNull ( payorType, "Primary Payor Type is required." );

            var payorTypeMember = _payorTypeMembers.FirstOrDefault ( m => m.PayorType.Key == payorType.Key );
            if (payorTypeMember != null)
            {
                PrimaryPayorTypeMember = payorTypeMember;
            }
        }

        /// <summary>
        /// Revises the email address.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        public virtual void ReviseEmailAddress ( EmailAddress emailAddress )
        {
            EmailAddress = emailAddress;
        }

        /// <summary>
        /// Adds the payor address.
        /// </summary>
        /// <param name="payorAddress">The payor address.</param>
        public virtual void AddPayorAddress ( PayorAddress payorAddress )
        {
            payorAddress.Payor = this;
            _addresses.Add ( payorAddress );
            NotifyItemAdded ( () => Addresses, payorAddress );
        }

        /// <summary>
        /// Adds the payor phone number.
        /// </summary>
        /// <param name="payorPhoneNumber">The payor phone number.</param>
        public virtual void AddPayorPhoneNumber ( PayorPhone payorPhoneNumber )
        {
            payorPhoneNumber.Payor = this;
            _phoneNumbers.Add ( payorPhoneNumber );
            NotifyItemAdded ( () => PhoneNumbers, payorPhoneNumber );
        }

        /// <summary>
        /// Removes the payor address.
        /// </summary>
        /// <param name="payorAddress">The payor address.</param>
        public virtual void RemovePayorAddress ( PayorAddress payorAddress )
        {
            Check.IsNotNull ( payorAddress, "Payor Address is required." );

            _addresses.Delete ( payorAddress );
            NotifyItemRemoved ( () => Addresses, payorAddress );
        }

        /// <summary>
        /// Removes the payor phone number.
        /// </summary>
        /// <param name="payorPhoneNumber">The payor phone number.</param>
        public virtual void RemovePayorPhoneNumber ( PayorPhone payorPhoneNumber )
        {
            Check.IsNotNull ( payorPhoneNumber, "Payor Phone Number is required." );

            _phoneNumbers.Delete ( payorPhoneNumber );
            NotifyItemRemoved ( () => PhoneNumbers, payorPhoneNumber );
        }
        

        /// <summary>
        /// Revises the effective date range.
        /// </summary>
        /// <param name="effectiveDateRange">The effective date range.</param>
        public virtual void ReviseEffectiveDateRange ( DateRange effectiveDateRange )
        {
            EffectiveDateRange = effectiveDateRange;
        }

        /// <summary>
        /// Revises the electronic transmitter identification number.
        /// </summary>
        /// <param name="electronicTransmitterIdentificationNumber">The electronic transmitter identification number.</param>
        public virtual void ReviseElectronicTransmitterIdentificationNumber ( string electronicTransmitterIdentificationNumber )
        {
            Check.IsNotNullOrWhitespace ( electronicTransmitterIdentificationNumber, () => ElectronicTransmitterIdentificationNumber );
            ElectronicTransmitterIdentificationNumber = electronicTransmitterIdentificationNumber;
        }

        /// <summary>
        /// Revises the name.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void ReviseName ( string name )
        {
            Name = name;
        }

        /// <summary>
        /// Revises the payor identifier.
        /// </summary>
        /// <param name="payorIdentifier">The payor identifier.</param>
        public virtual void RevisePayorIdentifier ( string payorIdentifier )
        {
            PayorIdentifier = payorIdentifier;
        }
        
        /// <summary>
        /// Revises the website address.
        /// </summary>
        /// <param name="websiteAddress">The website address.</param>
        public virtual void ReviseWebsiteAddress ( string websiteAddress )
        {
            WebsiteAddress = websiteAddress;
        }

        #endregion
    }
}
