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
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Core.CommonModule;
using Rem.WellKnownNames.PayorModule;

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// Defines the payor type.
    /// </summary>
    public class PayorType : AuditableAggregateRootBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayorType"/> class.
        /// </summary>
        protected internal PayorType ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="billingOffice">The billing office.</param>
        /// <param name="billingForm">The billing form.</param>
        public PayorType (string name, BillingOffice billingOffice, BillingForm billingForm )
            : this ()
        {
            Check.IsNotNullOrWhitespace ( name, () => Name );
            Check.IsNotNull ( billingOffice, () => BillingOffice );
            Check.IsNotNull ( billingForm, () => BillingForm );

            Name = name;
            BillingOffice = billingOffice;
            BillingForm = billingForm;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string Name { get; private set; }

        /// <summary>
        /// Gets the billing form.
        /// </summary>
        /// <value>The billing form.</value>
        [NotNull]
        public virtual BillingForm BillingForm { get; private set; }

        /// <summary>
        /// Gets the billing office.
        /// </summary>
        [NotNull]
        public virtual BillingOffice BillingOffice { get; private set; }

        /// <summary>
        /// Gets the health care claim837 setup.
        /// </summary>
        public virtual HealthCareClaim837Setup HealthCareClaim837Setup { get; private set; }

        /// <summary>
        /// Gets the submitter identifier.
        /// </summary>
        public virtual string SubmitterIdentifier { get; private set; }

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        public virtual Address BillingAddress { get; private set; }

        /// <summary>
        /// Gets the phone numbers.
        /// </summary>
        public virtual Phone BillingPhone { get; private set; }

        /// <summary>
        /// Gets the FTP address.
        /// </summary>
        public virtual Ftp BillingFtp { get; private set; }

        /// <summary>
        /// Revises the name.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void ReviseName(string name)
        {
            Check.IsNotNullOrWhitespace ( name, () => Name );
            Name = name;
        }

        /// <summary>
        /// Revises the billing form.
        /// </summary>
        /// <param name="billingForm">The billing form.</param>
        public virtual void ReviseBillingForm(BillingForm billingForm)
        {
            Check.IsNotNull ( billingForm, () => BillingForm );
            BillingForm = billingForm;
        }

        /// <summary>
        /// Revises the billing office.
        /// </summary>
        /// <param name="billingOffice">The billing office.</param>
        public virtual void ReviseBillingOffice(BillingOffice billingOffice)
        {
            Check.IsNotNull(billingOffice, () => BillingOffice);
            BillingOffice = billingOffice;
        }

        /// <summary>
        /// Revises the health care claim837 setup.
        /// </summary>
        /// <param name="healthCareClaim837Setup">The health care claim837 setup.</param>
        public virtual void ReviseHealthCareClaim837Setup(HealthCareClaim837Setup healthCareClaim837Setup)
        {
            HealthCareClaim837Setup = healthCareClaim837Setup;
        }

        /// <summary>
        /// Revises the submitter identifier.
        /// </summary>
        /// <param name="submitterIdentifier">The submitter identifier.</param>
        public virtual void ReviseSubmitterIdentifier(string submitterIdentifier)
        {
            SubmitterIdentifier = submitterIdentifier;
        }

        /// <summary>
        /// Revises the billing address.
        /// </summary>
        /// <param name="billingAddress">The billing address.</param>
        public virtual void ReviseBillingAddress(Address billingAddress)
        {
            BillingAddress = billingAddress;
        }

        /// <summary>
        /// Revises the billing phone number.
        /// </summary>
        /// <param name="billingPhone">The billing phone number.</param>
        public virtual void ReviseBillingPhone (Phone billingPhone)
        {
            BillingPhone = billingPhone;
        }

        /// <summary>
        /// Revises the FTP address.
        /// </summary>
        /// <param name="ftp">The FTP address.</param>
        public virtual void ReviseBillingFtpAddress ( Ftp ftp)
        {
            BillingFtp = ftp;
        }
    }
}
