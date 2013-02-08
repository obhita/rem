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
using Pillar.Common.Extension;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// The ClinicalCase defines an entity that encapsulates multiple patient to service provider interactions in a clinical setting.
    /// </summary>
    public class ClinicalCase : AuditableAggregateRootBase, IPatientAccessAuditable
    {
        #region Private Fields

        private readonly IList<Problem> _problems;
        private readonly IList<Visit> _visits;
        private readonly IList<Activity> _importedActivities;
        private readonly IList<ClinicalCasePriorityPopulation> _priorityPopulations;
        private readonly IList<ClinicalCaseSignedComment> _signedComments;
        private readonly IList<ClinicalCaseSpecialInitiative> _specialInitiatives;
        
        private readonly long _clinicalCaseNumber;
        private readonly Patient _patient;

        private ClinicalCaseProfile _clinicalCaseProfile;

        private ClinicalCaseStatus _clinicalCaseStatus;
        private ClinicalCaseCloseInfo _clinicalCaseCloseInfo;
        private ClinicalCaseAdmission _clinicalCaseAdmission;
        private ClinicalCaseDischarge _clinicalCaseDischarge;

        private string _clinicalCaseNote;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCase"/> class.
        /// </summary>
        protected internal ClinicalCase ()
        {
            _signedComments = new List<ClinicalCaseSignedComment> ();
            _priorityPopulations = new List<ClinicalCasePriorityPopulation> ();
            _specialInitiatives = new List<ClinicalCaseSpecialInitiative> ();
            _visits = new List<Visit> ();
            _problems = new List<Problem> ();
            _importedActivities = new List<Activity> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCase"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="clinicalCaseProfile">The clinical case profile.</param>
        /// <param name="caseNumber">The case number.</param>
        protected internal ClinicalCase (
            Patient patient,
            ClinicalCaseProfile clinicalCaseProfile,
            long caseNumber )
        {
            Check.IsNotNull ( patient, "Patient is required." );

            _patient = patient;
            _clinicalCaseProfile = clinicalCaseProfile;
            _clinicalCaseNumber = caseNumber;

            _signedComments = new List<ClinicalCaseSignedComment> ();
            _priorityPopulations = new List<ClinicalCasePriorityPopulation> ();
            _specialInitiatives = new List<ClinicalCaseSpecialInitiative> ();
            _visits = new List<Visit> ();
            _problems = new List<Problem> ();
            _importedActivities = new List<Activity>();
        }

        #endregion

        #region Public Properties

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
        /// Gets the clinical case number.
        /// </summary>
        public virtual long ClinicalCaseNumber
        {
            get { return _clinicalCaseNumber; }
            private set { }
        }

        /// <summary>
        /// Gets the clinical case status.
        /// </summary>
        public virtual ClinicalCaseStatus ClinicalCaseStatus
        {
            get { return _clinicalCaseStatus; }
            private set { ApplyPropertyChange(ref _clinicalCaseStatus, () => ClinicalCaseStatus, value); }
        }

        /// <summary>
        /// Gets the clinical case profile.
        /// </summary>
        public virtual ClinicalCaseProfile ClinicalCaseProfile
        {
            get { return _clinicalCaseProfile; }
            private set { ApplyPropertyChange(ref _clinicalCaseProfile, () => ClinicalCaseProfile, value); }
        }

        /// <summary>
        /// Gets the clinical case close info.
        /// </summary>
        public virtual ClinicalCaseCloseInfo ClinicalCaseCloseInfo
        {
            get { return _clinicalCaseCloseInfo; }
            private set { ApplyPropertyChange(ref _clinicalCaseCloseInfo, () => ClinicalCaseCloseInfo, value); }
        }

        /// <summary>
        /// Gets the clinical case admission.
        /// </summary>
        public virtual ClinicalCaseAdmission ClinicalCaseAdmission
        {
            get { return _clinicalCaseAdmission; }
            private set { ApplyPropertyChange(ref _clinicalCaseAdmission, () => ClinicalCaseAdmission, value); }
        }

        /// <summary>
        /// Gets the clinical case discharge.
        /// </summary>
        public virtual ClinicalCaseDischarge ClinicalCaseDischarge
        {
            get { return _clinicalCaseDischarge; }
            private set { ApplyPropertyChange(ref _clinicalCaseDischarge, () => ClinicalCaseDischarge, value); }
        }

        /// <summary>
        /// Gets the clinical case note.
        /// </summary>
        public virtual string ClinicalCaseNote
        {
            get { return _clinicalCaseNote; }
            private set { ApplyPropertyChange ( ref _clinicalCaseNote, () => ClinicalCaseNote, value ); }
        }

        /// <summary>
        /// Gets the priority populations.
        /// </summary>
        public virtual IEnumerable<ClinicalCasePriorityPopulation> PriorityPopulations
        {
            get { return _priorityPopulations.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the special initiatives.
        /// </summary>
        public virtual IEnumerable<ClinicalCaseSpecialInitiative> SpecialInitiatives
        {
            get { return _specialInitiatives; }
            private set { }
        }

        /// <summary>
        /// Gets the signed comments.
        /// </summary>
        public virtual IEnumerable<ClinicalCaseSignedComment> SignedComments
        {
            get { return _signedComments.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the visits.
        /// </summary>
        public virtual IEnumerable<Visit> Visits
        {
            get { return _visits.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the imported activities.
        /// </summary>
        public virtual IEnumerable<Activity> ImportedActivities
        {
            get { return _importedActivities.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the problems.
        /// </summary>
        public virtual IEnumerable<Problem> Problems
        {
            get { return _problems.ToList ().AsReadOnly (); }
            private set { }
        }

        #endregion

        #region IPatientAccessAuditable Members

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
        /// Revises the clinical case profile.
        /// </summary>
        /// <param name="clinicalCaseProfile">The clinical case profile.</param>
        public virtual void ReviseClinicalCaseProfile(ClinicalCaseProfile clinicalCaseProfile)
        {
            Check.IsNotNull(clinicalCaseProfile, "Clinical Case Profile is required.");

            DomainRuleEngine.CreateRuleEngine<ClinicalCase, ClinicalCaseProfile>(this, () => ReviseClinicalCaseProfile)
                .WithContext(clinicalCaseProfile)
                .WithContext(ClinicalCaseCloseInfo)
                .Execute(() => ClinicalCaseProfile = clinicalCaseProfile);
        }

        /// <summary>
        /// Closes the specified clinical case close info.
        /// </summary>
        /// <param name="clinicalCaseCloseInfo">The clinical case close info.</param>
        public virtual void Close(ClinicalCaseCloseInfo clinicalCaseCloseInfo)
        {
            Check.IsNotNull(clinicalCaseCloseInfo, "Clinical Case Close Info is required.");

            DomainRuleEngine.CreateRuleEngine<ClinicalCase, ClinicalCaseCloseInfo>(this, () => Close)
                .WithContext(clinicalCaseCloseInfo)
                .WithContext(ClinicalCaseProfile)
                .Execute(() =>
                {
                    //TODO: Update the ClinicalCaseStatus
                    ClinicalCaseCloseInfo = clinicalCaseCloseInfo;
                });
        }

        /// <summary>
        /// Admits the specified clinical case admission.
        /// </summary>
        /// <param name="clinicalCaseAdmission">The clinical case admission.</param>
        public virtual void Admit(ClinicalCaseAdmission clinicalCaseAdmission)
        {
            Check.IsNotNull(clinicalCaseAdmission, "Clinical Case Admission is required.");

            DomainRuleEngine.CreateRuleEngine<ClinicalCase, ClinicalCaseAdmission>(this, () => Admit)
                .WithContext(clinicalCaseAdmission)
                .Execute(() =>
                {
                    //TODO: Update the ClinicalCaseStatus
                    ClinicalCaseAdmission = clinicalCaseAdmission;
                });
        }

        /// <summary>
        /// Discharges the specified clinical case discharge.
        /// </summary>
        /// <param name="clinicalCaseDischarge">The clinical case discharge.</param>
        public virtual void Discharge(ClinicalCaseDischarge clinicalCaseDischarge)
        {
            Check.IsNotNull(clinicalCaseDischarge, "Clinical Case Discharge is required.");

            DomainRuleEngine.CreateRuleEngine<ClinicalCase, ClinicalCaseDischarge>(this, () => Discharge)
                .WithContext(clinicalCaseDischarge)
                .Execute(() =>
                {
                    //TODO: Update the ClinicalCaseStatus
                    ClinicalCaseDischarge = clinicalCaseDischarge;
                });
        }


        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="clinicalCaseStatus">The clinical case status.</param>
        public virtual void UpdateStatus(ClinicalCaseStatus clinicalCaseStatus)
        {
            // TODO: Shouldn't have this method.
            ClinicalCaseStatus = clinicalCaseStatus;
        }

        #region Collection Methods

        /// <summary>
        /// Adds the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public virtual void AddActivity(Activity activity)
        {
            Check.IsNotNull(activity, "Activity is required.");
            _importedActivities.Add(activity);
            NotifyItemAdded(() => ImportedActivities, activity);
        }

        /// <summary>
        /// Deletes the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public virtual void DeleteActivity(Activity activity)
        {
            Check.IsNotNull(activity, "Activity is required.");
            if (_importedActivities.Contains(activity))
            {
                _importedActivities.Remove(activity);
            }
            else
            {
                throw new ArgumentException("Activity not found.");
            }

            var activityRepository = IoC.CurrentContainer.Resolve<IActivityRepository>();
            activityRepository.MakeTransient(activity);

            NotifyItemRemoved(() => ImportedActivities, activity);
        }


        /// <summary>
        /// Adds the signed comment.
        /// </summary>
        /// <param name="signedComment">The signed comment.</param>
        public virtual void AddSignedComment (ClinicalCaseSignedComment signedComment )
        {
            Check.IsNotNull(signedComment, "Clinical case signed comment is required.");

            signedComment.ClinicalCase = this;
            _signedComments.Add ( signedComment );
            NotifyItemAdded ( () => SignedComments, signedComment );
        }

        /// <summary>
        /// Deletes the signed comment.
        /// </summary>
        /// <param name="signedComment">The signed comment.</param>
        public virtual void DeleteSignedComment(ClinicalCaseSignedComment signedComment)
        {
            Check.IsNotNull(signedComment, "Clinical case signed comment is required.");

            _signedComments.Delete(signedComment);
            NotifyItemRemoved(() => SignedComments, signedComment);
        }

        /// <summary>
        /// Adds the priority population.
        /// </summary>
        /// <param name="priorityPopulation">The priority population.</param>
        public virtual void AddPriorityPopulation(
            ClinicalCasePriorityPopulation priorityPopulation)
        {
            Check.IsNotNull(priorityPopulation, "Clinical case priority population is required.");

            priorityPopulation.ClinicalCase = this;
            _priorityPopulations.Add(priorityPopulation);
            NotifyItemAdded(() => PriorityPopulations, priorityPopulation);
        }

        /// <summary>
        /// Removes the priority population.
        /// </summary>
        /// <param name="priorityPopulation">The priority population.</param>
        public virtual void RemovePriorityPopulation ( ClinicalCasePriorityPopulation priorityPopulation )
        {
            Check.IsNotNull(priorityPopulation, "Clinical case priority population is required.");

            _priorityPopulations.Delete(priorityPopulation);
            NotifyItemRemoved ( () => PriorityPopulations, priorityPopulation );
        }

        /// <summary>
        /// Adds the special initiative.
        /// </summary>
        /// <param name="specialInitiative">The special initiative.</param>
        public virtual void AddSpecialInitiative (
            ClinicalCaseSpecialInitiative specialInitiative )
        {
            Check.IsNotNull(specialInitiative, "Clinical case special initiative is required.");

            specialInitiative.ClinicalCase = this;
            _specialInitiatives.Add(specialInitiative);
            NotifyItemAdded(() => SpecialInitiatives, specialInitiative);
        }

        /// <summary>
        /// Removes the special initiative.
        /// </summary>
        /// <param name="specialInitiative">The special initiative.</param>
        public virtual void RemoveSpecialInitiative(ClinicalCaseSpecialInitiative specialInitiative)
        {
            Check.IsNotNull(specialInitiative, "Clinical case special initiative is required.");

            _specialInitiatives.Delete(specialInitiative);
            NotifyItemRemoved(() => SpecialInitiatives, specialInitiative);
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return string.Format("Clinical Case {0}", ClinicalCaseNumber);
        }
    }
}
