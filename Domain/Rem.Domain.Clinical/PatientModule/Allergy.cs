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
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// An Allergy is a hypersensitivity to a substance that causes the body to react to any contact with that substance.
    /// </summary>
    public class Allergy : AuditableAggregateRootBase, IPatientAccessAuditable, IHasProvenance
    {
        #region Private Fields

        private readonly IList<AllergyReaction> _allergyReactions;
        private readonly Patient _patient;
        private CodedConcept _allergenCodedConcept;
        private AllergySeverityType _allergySeverityType;
        private AllergyStatus _allergyStatus;
        private AllergyType _allergyType;
        private DateRange _onsetDateRange;
        private Provenance _provenance;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Allergy"/> class.
        /// </summary>
        protected internal Allergy ()
        {
            _allergyReactions = new List<AllergyReaction> ();
        }

        internal Allergy ( Patient patient, AllergyStatus allergyStatus, CodedConcept allergenCodedConcept )
        {
            Check.IsNotNull ( patient, () => Patient );
            Check.IsNotNull ( allergyStatus, () => AllergyStatus );
            Check.IsNotNull ( allergenCodedConcept, () => AllergenCodedConcept );

            _allergyReactions = new List<AllergyReaction> ();
            _patient = patient;
            _allergyStatus = allergyStatus;
            _allergenCodedConcept = allergenCodedConcept;
        }

        internal Allergy(Patient patient, AllergyStatus allergyStatus, CodedConcept allergenCodedConcept, Provenance provenance)
            :this (patient, allergyStatus, allergenCodedConcept)
        {
            Check.IsNotNull ( provenance, () => Provenance );
            _provenance = provenance;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the patient.
        /// </summary>
        [NotNull]
        public virtual Patient Patient
        {
            get { return _patient; }
            private set { }
        }

        /// <summary>
        /// Gets the allergy status.
        /// </summary>
        [NotNull]
        public virtual AllergyStatus AllergyStatus
        {
            get { return _allergyStatus; }
            private set { ApplyPropertyChange ( ref _allergyStatus, () => AllergyStatus, value ); }
        }

        /// <summary>
        /// Gets the type of the allergy.
        /// </summary>
        /// <value>
        /// The type of the allergy.
        /// </value>
        public virtual AllergyType AllergyType
        {
            get { return _allergyType; }
            private set { ApplyPropertyChange ( ref _allergyType, () => AllergyType, value ); }
        }

        /// <summary>
        /// Gets the type of the allergy severity.
        /// </summary>
        /// <value>
        /// The type of the allergy severity.
        /// </value>
        public virtual AllergySeverityType AllergySeverityType
        {
            get { return _allergySeverityType; }
            private set { ApplyPropertyChange ( ref _allergySeverityType, () => AllergySeverityType, value ); }
        }

        /// <summary>
        /// Gets the allergen coded concept.
        /// </summary>
        [NotNull]
        public virtual CodedConcept AllergenCodedConcept
        {
            get { return _allergenCodedConcept; }
            private set { ApplyPropertyChange ( ref _allergenCodedConcept, () => AllergenCodedConcept, value ); }
        }

        /// <summary>
        /// Gets the allergy reactions.
        /// </summary>
        public virtual IEnumerable<AllergyReaction> AllergyReactions
        {
            get { return _allergyReactions.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the onset date range.
        /// </summary>
        public virtual DateRange OnsetDateRange
        {
            get { return _onsetDateRange; }
            private set { ApplyPropertyChange ( ref _onsetDateRange, () => OnsetDateRange, value ); }
        }

        #endregion

        /// <summary>
        /// Gets the provenance.
        /// </summary>
        public virtual Provenance Provenance
        {
            get { return _provenance; }
            private set { }
        }


        #region IPatientAccessAuditable Members

        /// <summary>
        /// Gets the audited patient.
        /// </summary>
        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return Patient; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1}", GetType().Name.SeparatePascalCaseWords(), ToString()); }
        }

        #endregion


        /// <summary>
        /// Gets the provenance.
        /// </summary>
        Provenance IHasProvenance.Provenance
        {
            get { return Provenance; }
        }

        /// <summary>
        /// Revises the allergy status.
        /// </summary>
        /// <param name="allergyStatus">The allergy status.</param>
        public virtual void ReviseAllergyStatus ( AllergyStatus allergyStatus )
        {
            Check.IsNotNull ( allergyStatus, "allergyStatus is required." );
            AllergyStatus = allergyStatus;
        }

        /// <summary>
        /// Revises the type of the allergy.
        /// </summary>
        /// <param name="allergyType">Type of the allergy.</param>
        public virtual void ReviseAllergyType ( AllergyType allergyType )
        {
            AllergyType = allergyType;
        }

        /// <summary>
        /// Revises the type of the allergy severity.
        /// </summary>
        /// <param name="allergySeverityType">Type of the allergy severity.</param>
        public virtual void ReviseAllergySeverityType ( AllergySeverityType allergySeverityType )
        {
            AllergySeverityType = allergySeverityType;
        }

        /// <summary>
        /// Revises the coded concept.
        /// </summary>
        /// <param name="allergenCodedConcept">The allergen coded concept.</param>
        public virtual void ReviseCodedConcept ( CodedConcept allergenCodedConcept )
        {
            Check.IsNotNull ( allergenCodedConcept, "allergenCodedConcept is required." );
            AllergenCodedConcept = allergenCodedConcept;
        }

        /// <summary>
        /// Revises the onset date range.
        /// </summary>
        /// <param name="onsetDateRange">The onset date range.</param>
        public virtual void ReviseOnsetDateRange ( DateRange onsetDateRange )
        {
            OnsetDateRange = onsetDateRange;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return _allergenCodedConcept.ToString ();
        }

        #region Collection Methods

        /// <summary>
        /// Adds the reaction.
        /// </summary>
        /// <param name="reaction">The reaction.</param>
        /// <returns>An AllergyReaction.
        /// </returns>
        public virtual AllergyReaction AddReaction ( Reaction reaction )
        {
            Check.IsNotNull ( reaction, "reaction is required." );

            var allergyReaction = new AllergyReaction ( reaction ) { Allergy = this };
            _allergyReactions.Add ( allergyReaction );
            NotifyItemAdded ( () => AllergyReactions, allergyReaction );

            return allergyReaction;
        }

        /// <summary>
        /// Deletes the reaction.
        /// </summary>
        /// <param name="allergyReaction">The allergy reaction.</param>
        public virtual void DeleteReaction ( AllergyReaction allergyReaction )
        {
            Check.IsNotNull(allergyReaction, "allergyReaction is required.");

            _allergyReactions.Delete ( allergyReaction);
            NotifyItemRemoved ( () => AllergyReactions, allergyReaction );
        }
        #endregion
    }
}