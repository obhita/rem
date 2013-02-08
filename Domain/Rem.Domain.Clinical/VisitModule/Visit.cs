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
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// Visit is an <see cref="Appointment">Appointment</see> between a patient and a care provider.  
    /// </summary>
    public class Visit : Appointment, IPatientAccessAuditable
    {
        private readonly IList<Activity> _activities;
        private readonly ClinicalCase _clinicalCase;
        private readonly IList<VisitProblem> _problems;
        private DateTime? _checkedInDateTime;
        private string _cptCode;

        private Location _serviceLocation;
        private string _name;
        private VisitStatus _visitStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="Visit"/> class.
        /// </summary>
        protected internal Visit ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Visit"/> class.
        /// </summary>
        /// <param name="staff">The staff.</param>
        /// <param name="appointmentDateTimeRange">The appointment date time range.</param>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="visitStatus">The visit status.</param>
        /// <param name="placeOfService">The place of service.</param>
        /// <param name="name">The name.</param>
        /// <param name="cptCode">The CPT code.</param>
        protected internal Visit (Staff staff, 
            DateTimeRange appointmentDateTimeRange,
            ClinicalCase clinicalCase,
            VisitStatus visitStatus,
            Location placeOfService,
            string name,
            string cptCode ): base(staff, appointmentDateTimeRange)
        {
            Check.IsNotNull ( clinicalCase, "Clinical case is required." );
            Check.IsNotNull ( visitStatus, "Visit status is required." );
            Check.IsNotNull ( placeOfService, "Location is required." );
            Check.IsNotNull ( name, "Name is required." );
            Check.IsNotNull ( cptCode, "CptCode is required." );

            if ( visitStatus.WellKnownName != WellKnownNames.VisitModule.VisitStatus.Scheduled )
            {
                throw new ArgumentException ( "Visits must always be created as scheduled visits." );
            }

            _clinicalCase = clinicalCase;
            _visitStatus = visitStatus;
            _serviceLocation = placeOfService;
            _name = name;
            _cptCode = cptCode;
            _activities = new List<Activity> ();
            _problems = new List<VisitProblem> ();
        }

        /// <summary>
        /// Gets the clinical case.
        /// </summary>
        [NotNull]
        public virtual ClinicalCase ClinicalCase
        {
            get { return _clinicalCase; }
            private set { }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets the CPT code.
        /// </summary>
        [NotNull]
        public virtual string CptCode
        {
            get { return _cptCode; }
            private set { ApplyPropertyChange ( ref _cptCode, () => CptCode, value ); }
        }

        /// <summary>
        /// Gets the visit status.
        /// </summary>
        [NotNull]
        public virtual VisitStatus VisitStatus
        {
            get { return _visitStatus; }
            private set { ApplyPropertyChange(ref _visitStatus, () => VisitStatus, value); }
        }

        /// <summary>
        /// Gets the checked in date time.
        /// </summary>
        public virtual DateTime? CheckedInDateTime
        {
            get { return _checkedInDateTime; }
            private set { ApplyPropertyChange ( ref _checkedInDateTime, () => CheckedInDateTime, value ); }
        }

        /// <summary>
        /// Gets the service location.
        /// </summary>
        [NotNull]
        public virtual Location ServiceLocation
        {
            get { return _serviceLocation; }
            private set { ApplyPropertyChange(ref _serviceLocation, () => ServiceLocation, value); }
        }

        /// <summary>
        /// Gets the activities.
        /// </summary>
        public virtual IEnumerable<Activity> Activities
        {
            get { return _activities.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the problems.
        /// </summary>
        public virtual IEnumerable<VisitProblem> Problems
        {
            get { return _problems.ToList ().AsReadOnly (); }
            private set { }
        }

        #region IPatientAccessAuditable Members

        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return ClinicalCase.Patient; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1}", GetType().Name.SeparatePascalCaseWords(), ToString()); }
        }

        #endregion

        /// <summary>
        /// Adds the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public virtual void AddActivity(Activity activity)
        {
            _activities.Add(activity);
            NotifyItemAdded(() => Activities, activity);
        }

        /// <summary>
        /// Deletes the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public virtual void DeleteActivity ( Activity activity )
        {
            if ( _activities.Contains ( activity ) )
            {
                _activities.Remove ( activity );
            }
            else
            {
                throw new ArgumentException ( "Activity not found." );
            }

            var activityRepository = IoC.CurrentContainer.Resolve<IActivityRepository>();
            activityRepository.MakeTransient ( activity );

            NotifyItemRemoved ( () => Activities, activity );
        }

        /// <summary>
        /// Adds the problem.
        /// </summary>
        /// <param name="problem">The problem.</param>
        /// <returns>A VisitProblem.</returns>
        public virtual VisitProblem AddProblem ( Problem problem )
        {
            var visitProblem = new VisitProblem ( this, problem );
            _problems.Add ( visitProblem );

            NotifyItemAdded ( () => Problems, visitProblem );

            return visitProblem;
        }

        /// <summary>
        /// Deletes the problem.
        /// </summary>
        /// <param name="problem">The problem.</param>
        public virtual void DeleteProblem ( VisitProblem problem )
        {
            _problems.Delete(problem);
            NotifyItemRemoved ( () => Problems, problem );
        }

        /// <summary>
        /// Associates the problems.
        /// </summary>
        /// <param name="problems">The problems.</param>
        public virtual void AssociateProblems ( IEnumerable<Problem> problems )
        {
            foreach ( var problem in problems )
            {
                var prob = problem;

                var visitProblem = _problems.SingleOrDefault ( p => p.Problem.Key == prob.Key );

                if ( visitProblem == null )
                {
                    var newVisitProblem = new VisitProblem ( this, problem );
                    _problems.Add ( newVisitProblem );

                    NotifyItemAdded ( () => Problems, newVisitProblem );
                }
            }
        }

        /// <summary>
        /// Changes the Service location.
        /// </summary>
        /// <param name="location">The service location.</param>
        public virtual void ChangeServiceLocation ( Location location )
        {
            Check.IsNotNull ( location, "Location is required." );

            ServiceLocation = location;
        }

        /// <summary>
        /// Disassociates the problem.
        /// </summary>
        /// <param name="problem">The problem.</param>
        public virtual void DisassociateProblem(Problem problem)
        {
            var visitProblem = _problems.SingleOrDefault ( p => p.Problem.Key == problem.Key );

            if ( visitProblem != null )
            {
                _problems.Delete(visitProblem);
                NotifyItemRemoved ( () => Problems, visitProblem );
            }
        }

        /// <summary>
        /// Checks in the Visit.
        /// </summary>
        /// <param name="checkedInDateTime">The checked in date time.</param>
        public virtual void CheckIn ( DateTime checkedInDateTime )
        {
            InitializeServices ();

            if ( VisitStatus.WellKnownName == WellKnownNames.VisitModule.VisitStatus.CheckedIn )
            {
                throw new InvalidOperationException( "Patient is already checked in." );
            }

            var visitStatusRepository = IoC.CurrentContainer.Resolve<IVisitStatusRepository> ();
            VisitStatus = visitStatusRepository.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.CheckedIn );
            CheckedInDateTime = checkedInDateTime;
        }

        /// <summary>
        /// Marks the visit status as scheduled.
        /// </summary>
        public virtual void MarkVisitStatusAsScheduled ()
        {
            InitializeServices ();

            if ( VisitStatus.WellKnownName == WellKnownNames.VisitModule.VisitStatus.Scheduled )
            {
                throw new InvalidOperationException("Patient is already scheduled.");
            }

            var visitStatusRepository = IoC.CurrentContainer.Resolve<IVisitStatusRepository> ();
            VisitStatus = visitStatusRepository.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled );
        }

        /// <summary>
        /// Marks the visit status as no show.
        /// </summary>
        public virtual void MarkVisitStatusAsNoShow ()
        {
            InitializeServices ();

            if ( VisitStatus.WellKnownName == WellKnownNames.VisitModule.VisitStatus.NoShow )
            {
                throw new InvalidOperationException("Patient is already set to no show.");
            }

            var visitStatusRepository = IoC.CurrentContainer.Resolve<IVisitStatusRepository> ();
            VisitStatus = visitStatusRepository.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.NoShow );
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public virtual void Cancel()
        {
            InitializeServices();

            if (VisitStatus.WellKnownName == WellKnownNames.VisitModule.VisitStatus.Canceled)
            {
                throw new InvalidOperationException("Patient is already set to canceled.");
            }

            var visitStatusRepository = IoC.CurrentContainer.Resolve<IVisitStatusRepository>();
            VisitStatus = visitStatusRepository.GetByWellKnownName(WellKnownNames.VisitModule.VisitStatus.Canceled);
        }

        /// <summary>
        /// Renames the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void Rename(string name)
        {
            Name = name;
        }


        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return string.Format (
                "'{0}' on '{1}'",
                Name,
                AppointmentDateTimeRange );
        }

        #endregion
    }
}
