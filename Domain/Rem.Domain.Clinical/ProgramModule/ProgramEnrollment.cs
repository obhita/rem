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
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ProgramModule
{
    /// <summary>
    /// ProgramEnrollment defines elements related to enrolling in a program.
    /// </summary>
    public class ProgramEnrollment : AuditableAggregateRootBase
    {
        #region Private fields
        
        private readonly ProgramOffering _programOffering;
        private readonly ClinicalCase _clinicalCase;
        private DateTime _enrollmentDate;
        private Staff _enrollingStaff;
        private DateTime? _disenrollmentDate;
        private string _commentsNote;
        private DisenrollReason _disenrollReason;
        private string _disenrollOtherReasonNote;
        private int? _daysOnWaitingListCount;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramEnrollment"/> class.
        /// </summary>
        protected internal ProgramEnrollment()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramEnrollment"/> class.
        /// </summary>
        /// <param name="programOffering">The program offering.</param>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="enrollmentDate">The enrollment date.</param>
        /// <param name="enrollingStaff">The enrolling staff.</param>
        protected internal ProgramEnrollment(ProgramOffering programOffering, ClinicalCase clinicalCase, DateTime enrollmentDate, Staff enrollingStaff)
            : this()
        {
            Check.IsNotNull(programOffering, "Program Offering is required.");
            Check.IsNotNull(clinicalCase, "Clinical Case is required.");
            Check.IsNotNull(enrollmentDate, "Enrollment Date is required.");
            Check.IsNotNull(enrollingStaff, "Enrolling Staff is required.");

            _programOffering = programOffering;
            _clinicalCase = clinicalCase;
            _enrollmentDate = enrollmentDate;
            _enrollingStaff = enrollingStaff;
        }

        #region Properties

        /// <summary>
        /// Gets the program offering.
        /// </summary>
        [NotNull]
        public virtual ProgramOffering ProgramOffering
        {
            get { return _programOffering; }
        }

        /// <summary>
        /// Gets the clinical case.
        /// </summary>
        [NotNull]
        public virtual ClinicalCase ClinicalCase
        {
            get { return _clinicalCase; }
        }

        /// <summary>
        /// Gets the enrollment date.
        /// </summary>
        [NotNull]
        public virtual DateTime EnrollmentDate
        {
            get { return _enrollmentDate; }
            private set { ApplyPropertyChange(ref _enrollmentDate, () => EnrollmentDate, value); }
        }

        /// <summary>
        /// Gets the enrolling staff.
        /// </summary>
        [NotNull]
        public virtual Staff EnrollingStaff
        {
            get { return _enrollingStaff; }
            private set { ApplyPropertyChange(ref _enrollingStaff, () => EnrollingStaff, value); }
        }

        /// <summary>
        /// Gets the disenrollment date.
        /// </summary>
        public virtual DateTime? DisenrollmentDate
        {
            get { return _disenrollmentDate; }
            private set { ApplyPropertyChange(ref _disenrollmentDate, () => DisenrollmentDate, value); }
        }

        /// <summary>
        /// Gets the comments note.
        /// </summary>
        public virtual string CommentsNote
        {
            get { return _commentsNote; }
            private set { ApplyPropertyChange(ref _commentsNote, () => CommentsNote, value); }
        }

        /// <summary>
        /// Gets the disenroll reason.
        /// </summary>
        public virtual DisenrollReason DisenrollReason
        {
            get { return _disenrollReason; }
            private set { ApplyPropertyChange(ref _disenrollReason, () => DisenrollReason, value); }
        }

        /// <summary>
        /// Gets the disenroll other reason note.
        /// </summary>
        public virtual string DisenrollOtherReasonNote
        {
            get { return _disenrollOtherReasonNote; }
            private set { ApplyPropertyChange(ref _disenrollOtherReasonNote, () => DisenrollOtherReasonNote, value); }
        }

        /// <summary>
        /// Gets the days on waiting list count.
        /// </summary>
        public virtual int? DaysOnWaitingListCount
        {
            get { return _daysOnWaitingListCount; }
            private set { ApplyPropertyChange(ref _daysOnWaitingListCount, () => DaysOnWaitingListCount, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Disenrolls the specified disenrollment date.
        /// </summary>
        /// <param name="disenrollmentDate">The disenrollment date.</param>
        /// <param name="disenrollReason">The disenroll reason.</param>
        /// <param name="disenrollOtherReasonNote">The disenroll other reason note.</param>
        public virtual void Disenroll(DateTime? disenrollmentDate, DisenrollReason disenrollReason, string disenrollOtherReasonNote = null)
        {
            Check.IsNotNull(disenrollmentDate, "Disenrollment Date is required.");
            Check.IsNotNull(disenrollReason, "Disenroll Reason is required.");
            if (disenrollReason.WellKnownName == WellKnownNames.ProgramModule.DisenrollReason.Other)
            {
                Check.IsNotNull ( disenrollOtherReasonNote, "Disenroll Other Reason Note is required." );
            }

            DomainRuleEngine.CreateRuleEngine ( this, "DisenrollRuleSet" )
                .WithContext ( disenrollmentDate )
                .WithContext ( disenrollReason )
                .WithContext ( disenrollOtherReasonNote )
                .Execute(() =>
                {
                    DisenrollmentDate = disenrollmentDate;
                    DisenrollReason = disenrollReason;
                    DisenrollOtherReasonNote = disenrollReason.WellKnownName == WellKnownNames.ProgramModule.DisenrollReason.Other ? disenrollOtherReasonNote : null;
                });
        }

        /// <summary>
        /// Revises the enrollment date.
        /// </summary>
        /// <param name="enrollmentDate">The enrollment date.</param>
        public virtual void ReviseEnrollmentDate(DateTime enrollmentDate)
        {
            DomainRuleEngine.CreateRuleEngine<ProgramEnrollment, DateTime> ( this, () => ReviseEnrollmentDate )
                .WithContext ( enrollmentDate )
                .WithContext ( EnrollingStaff )
                .Execute ( () => EnrollmentDate = enrollmentDate );
        }

        /// <summary>
        /// Revises the enrolling staff.
        /// </summary>
        /// <param name="enrollingStaff">The enrolling staff.</param>
        public virtual void ReviseEnrollingStaff(Staff enrollingStaff)
        {
            Check.IsNotNull(enrollingStaff, "Enrolling Staff is required.");

            DomainRuleEngine.CreateRuleEngine<ProgramEnrollment, Staff> ( this, () => ReviseEnrollingStaff )
                .WithContext ( enrollingStaff )
                .Execute ( () => EnrollingStaff = enrollingStaff );
        }

        /// <summary>
        /// Revises the comments.
        /// </summary>
        /// <param name="comments">The comments.</param>
        public virtual void ReviseComments(string comments)
        {
            DomainRuleEngine.CreateRuleEngine<ProgramEnrollment, string> ( this, () => ReviseComments )
                .WithContext ( comments )
                .Execute ( () => CommentsNote = comments );
        }

        /// <summary>
        /// Revises the days on waiting list count.
        /// </summary>
        /// <param name="daysOnWaitingListCount">The days on waiting list count.</param>
        public virtual void ReviseDaysOnWaitingListCount(int? daysOnWaitingListCount)
        {
            DomainRuleEngine.CreateRuleEngine<ProgramEnrollment, int?> ( this, () => ReviseDaysOnWaitingListCount )
                .WithContext ( daysOnWaitingListCount )
                .Execute ( () => DaysOnWaitingListCount = daysOnWaitingListCount );
        }

        #endregion
    }
}
