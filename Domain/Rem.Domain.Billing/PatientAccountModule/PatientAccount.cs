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

using System;
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.PatientAccountModule
{
    /// <summary>
    /// The class defines a patient account entity.
    /// </summary>
    public class PatientAccount : AuditableAggregateRootBase
    {
        private readonly IList<PatientAccountPhone> _phones;
        private readonly IList<PayorCoverage> _payorCoverages;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccount"/> class.
        /// </summary>
        protected PatientAccount()
        {
            _phones = new List<PatientAccountPhone> ();
            _payorCoverages = new List<PayorCoverage> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccount"/> class.
        /// </summary>
        /// <param name="billingOffice">The billing office.</param>
        /// <param name="medicalRecordNumber">The medical record number.</param>
        /// <param name="name">The name.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="homeAddress">The home address.</param>
        /// <param name="administrativeGender">The administrative gender.</param>
        protected internal PatientAccount ( BillingOffice billingOffice, long medicalRecordNumber, PersonName name, DateTime? birthDate, Address homeAddress, AdministrativeGender administrativeGender ) : this()
        {
            Check.IsNotNull ( billingOffice, "Billing office is required." );
            Check.IsNotNull ( name, "Name is required." );
            Check.IsNotNull ( medicalRecordNumber, "Medical record number is required." );
            Check.IsNotNull ( homeAddress, () => HomeAddress );
            Check.IsNotNull( administrativeGender, "Gender is required.");

            BillingOffice = billingOffice;
            MedicalRecordNumber = medicalRecordNumber;
            Name = name;
            BirthDate = birthDate;
            HomeAddress = homeAddress;
            AdministrativeGender = administrativeGender;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the billing office.
        /// </summary>
        [NotNull]
        public virtual BillingOffice BillingOffice { get; private set; }

        /// <summary>
        /// Gets the gender.
        /// </summary>
        [NotNull]
        public virtual AdministrativeGender AdministrativeGender { get; private set; }

        /// <summary>
        /// Gets the home address.
        /// </summary>
        public virtual Address HomeAddress { get; private set; }

        /// <summary>
        /// Gets the medical record number.
        /// </summary>
        [NotNull]
        [Unique]
        public virtual long MedicalRecordNumber { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual PersonName Name { get; private set; }

        /// <summary>
        /// Gets the birth date.
        /// </summary>
        public virtual DateTime? BirthDate { get; private set; }

        /// <summary>
        /// Gets the payor coverages.
        /// </summary>
        public virtual IEnumerable<PayorCoverage> PayorCoverages
        {
            get { return _payorCoverages.ToList ().AsReadOnly (); }
            private set {}
        }

        /// <summary>
        /// Gets the phones.
        /// </summary>
        public virtual IEnumerable<PatientAccountPhone> Phones
        {
            get { return _phones.ToList().AsReadOnly(); }
            private set { }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the name on the patient account.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void ReviseName ( PersonName name )
        {
            Check.IsNotNull ( name, "Name cannot be null." );
            Name = name;
        }

        /// <summary>
        /// Revises the home address.
        /// </summary>
        /// <param name="address">The address.</param>
        public virtual void ReviseHomeAddress(Address address)
        {
            HomeAddress = address;
        }

        /// <summary>
        /// Revises the phones.
        /// </summary>
        /// <param name="patientAccountPhones">The patient account phones.</param>
        public virtual void RevisePhones(IList<PatientAccountPhone> patientAccountPhones )
        {
            if (patientAccountPhones == null || patientAccountPhones.Count() == 0)
            {
                _phones.Clear();
            }
            else
            {
                if (_phones.Count == 0)
                {
                    foreach (var patientAccountPhone in patientAccountPhones)
                    {
                        patientAccountPhone.PatientAccount = this;
                        _phones.Add(patientAccountPhone);
                    }
                }
                else
                {
                    var phonesToBeRemoved = _phones.Where ( phone => patientAccountPhones.Where ( p => p.ValuesEqual ( phone ) ).Count () == 0 ).ToList ();
                    foreach ( var patientAccountPhone in phonesToBeRemoved )
                    {
                        _phones.Remove ( patientAccountPhone );
                    }

                    var phonesToBeAdded = patientAccountPhones.Where(phone => _phones.Where(p => p.ValuesEqual(phone)).Count() == 0).ToList();
                    foreach ( var patientAccountPhone in phonesToBeAdded )
                    {
                        patientAccountPhone.PatientAccount = this;
                        _phones.Add(patientAccountPhone);
                    }
                }
            }
        }

        /// <summary>
        /// Revises the payor coverages.
        /// </summary>
        /// <param name="payorCoverages">The payor coverages.</param>
        public virtual void RevisePayorCoverages(IList<PayorCoverage> payorCoverages)
        {
            if (payorCoverages == null || payorCoverages.Count() == 0)
            {
                _payorCoverages.Clear();
            }
            else
            {
                if (_payorCoverages.Count == 0)
                {
                    foreach (var payorCoverage in payorCoverages)
                    {
                        payorCoverage.PatientAccount = this;
                        _payorCoverages.Add(payorCoverage);
                    }
                }
                else
                {
                    var payorCoveragesToBeRemoved = _payorCoverages.Where(payorCoverage => payorCoverages.Where(p => p.ValuesEqual(payorCoverage)).Count() == 0).ToList();
                    foreach (var payorCoverage in payorCoveragesToBeRemoved)
                    {
                        _payorCoverages.Remove(payorCoverage);
                    }

                    var payorCoveragesToBeAdded = payorCoverages.Where(payorCoverage => _payorCoverages.Where(p => p.ValuesEqual(payorCoverage)).Count() == 0).ToList();
                    foreach (var payorCoverage in payorCoveragesToBeAdded)
                    {
                        payorCoverage.PatientAccount = this;
                        _payorCoverages.Add ( payorCoverage );
                    }
                }
            }
        }

        /// <summary>
        /// Revises the birth date.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        public virtual void ReviseBirthDate(DateTime? birthDate)
        {
            BirthDate = birthDate;
        }


        /// <summary>
        /// Revises the administrative gender.
        /// </summary>
        /// <param name="administrativeGender">The administrative gender.</param>
        public virtual  void ReviseAdministrativeGender ( AdministrativeGender administrativeGender)
        {
            Check.IsNotNull(administrativeGender, "Gender is required.");
            AdministrativeGender = administrativeGender;
        }
        #endregion
    }
}
