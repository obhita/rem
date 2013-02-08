// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Staff.cs" company="">
//   
// </copyright>
// <summary>
//   The Staff defines basic attributes of a staff member.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The Staff defines basic attributes of a staff member.
    /// </summary>
    public class Staff : AuditableAggregateRootBase
    {
        private readonly IList<StaffAddress> _addresses;
        private readonly Agency _agency;
        private readonly IList<StaffCertification> _certifications;
        private readonly IList<StaffCollegeDegree> _collegeDegrees;
        private readonly IList<StaffIdentifier> _identifiers;
        private readonly IList<StaffLanguage> _languages;
        private readonly IList<StaffLicense> _licenses;
        private readonly IList<StaffPhone> _phoneNumbers;
        private readonly IList<StaffChecklistItem> _staffChecklist;
        private readonly IList<StaffEvent> _staffEvents;
        private readonly IList<StaffLocationAssignment> _staffLocationAssignments;
        private readonly IList<StaffSystemRole> _systemRoles;
        private readonly IList<StaffTrainingCourse> _trainingCourses;
        private StaffEmergencyContact _emergencyContact;
        private Location _primaryLocation;
        private StaffHr _staffHr;
        private StaffPhoto _staffPhoto;
        private StaffProfile _staffProfile;
        private SystemAccount _systemAccount;
        private StaffDirectAddressCredential _directAddressCredential;


        /// <summary>
        /// Initializes a new instance of the <see cref="Staff"/> class.
        /// </summary>
        protected internal Staff ()
        {
            _addresses = new List<StaffAddress> ();
            _identifiers = new List<StaffIdentifier> ();
            _phoneNumbers = new List<StaffPhone> ();
            _systemRoles = new List<StaffSystemRole> ();
            _collegeDegrees = new List<StaffCollegeDegree> ();
            _licenses = new List<StaffLicense> ();
            _certifications = new List<StaffCertification> ();
            _trainingCourses = new List<StaffTrainingCourse> ();
            _languages = new List<StaffLanguage> ();
            _staffChecklist = new List<StaffChecklistItem> ();
            _staffEvents = new List<StaffEvent> ();
            _staffLocationAssignments = new List<StaffLocationAssignment> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Staff"/> class.
        /// </summary>
        /// <param name="agency">
        /// The agency.
        /// </param>
        /// <param name="staffProfile">
        /// The staff profile.
        /// </param>
        protected internal Staff ( Agency agency, StaffProfile staffProfile )
            : this ()
        {
            Check.IsNotNull ( agency, () => Agency );
            Check.IsNotNull ( staffProfile, () => StaffProfile );

            _agency = agency;
            _staffProfile = staffProfile;
        }

        /// <summary>
        /// Gets the Agency.
        /// </summary>
        [NotNull]
        public virtual Agency Agency
        {
            get { return _agency; }
            private set { }
        }

        /// <summary>
        /// Gets the primary location.
        /// </summary>
        public virtual Location PrimaryLocation
        {
            get { return _primaryLocation; }
            private set { ApplyPropertyChange ( ref _primaryLocation, () => PrimaryLocation, value ); }
        }

        /// <summary>
        /// Gets the staff location Assignments.
        /// </summary>
        public virtual IEnumerable<StaffLocationAssignment> StaffLocationAssignments
        {
            get { return _staffLocationAssignments.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the staff profile.
        /// </summary>
        [NotNull]
        public virtual StaffProfile StaffProfile
        {
            get { return _staffProfile; }
            private set { ApplyPropertyChange ( ref _staffProfile, () => StaffProfile, value ); }
        }

        /// <summary>
        /// Gets the staffHr.
        /// </summary>
        [NotNull]
        public virtual StaffHr StaffHr
        {
            get { return _staffHr; }
            private set { ApplyPropertyChange ( ref _staffHr, () => StaffHr, value ); }
        }

        /// <summary>
        /// Gets the staff photo.
        /// </summary>
        public virtual StaffPhoto StaffPhoto
        {
            get { return _staffPhoto; }
            private set { }
        }

        /// <summary>
        /// Gets the emergency contact.
        /// </summary>
        public virtual StaffEmergencyContact EmergencyContact
        {
            get { return _emergencyContact; }
            private set { ApplyPropertyChange ( ref _emergencyContact, () => EmergencyContact, value ); }
        }

        /// <summary>
        /// Gets the system account.
        /// </summary>
        public virtual SystemAccount SystemAccount
        {
            get { return _systemAccount; }
            private set { ApplyPropertyChange ( ref _systemAccount, () => SystemAccount, value ); }
        }


        /// <summary>
        /// Gets the system account.
        /// </summary>
        public virtual StaffDirectAddressCredential DirectAddressCredential
        {
            get { return _directAddressCredential; }
            private set { ApplyPropertyChange(ref _directAddressCredential, () => DirectAddressCredential, value); }
        }


        /// <summary>
        /// Gets the addresses.
        /// </summary>
        public virtual IEnumerable<StaffAddress> Addresses
        {
            get { return _addresses.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the identifiers.
        /// </summary>
        public virtual IEnumerable<StaffIdentifier> Identifiers
        {
            get { return _identifiers.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the phone numbers.
        /// </summary>
        public virtual IEnumerable<StaffPhone> PhoneNumbers
        {
            get { return _phoneNumbers.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the system roles.
        /// </summary>
        public virtual IEnumerable<StaffSystemRole> SystemRoles
        {
            get { return _systemRoles.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the college degrees.
        /// </summary>
        public virtual IEnumerable<StaffCollegeDegree> CollegeDegrees
        {
            get { return _collegeDegrees.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the licenses.
        /// </summary>
        public virtual IEnumerable<StaffLicense> Licenses
        {
            get { return _licenses.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the certifications.
        /// </summary>
        public virtual IEnumerable<StaffCertification> Certifications
        {
            get { return _certifications.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the training courses.
        /// </summary>
        public virtual IEnumerable<StaffTrainingCourse> TrainingCourses
        {
            get { return _trainingCourses.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        public virtual IEnumerable<StaffLanguage> Languages
        {
            get { return _languages.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the staff events.
        /// </summary>
        public virtual IEnumerable<StaffEvent> StaffEvents
        {
            get { return _staffEvents.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the staff checklist.
        /// </summary>
        public virtual IEnumerable<StaffChecklistItem> StaffChecklist
        {
            get { return _staffChecklist.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Revise job function role.
        /// </summary>
        /// <param name="jobFunctionRole">
        /// The job function role.
        /// </param>
        public virtual void ReviseJobFunctionRole ( SystemRole jobFunctionRole )
        {
            CheckRoleAssignment ( jobFunctionRole, SystemRoleType.JobFunction );

            var existingJobFunctionRoles =
                ( from sr in _systemRoles where sr.SystemRole.SystemRoleType == SystemRoleType.JobFunction select sr ).ToList ();

            foreach ( var existingJobFunctionRole in existingJobFunctionRoles )
            {
                _systemRoles.Remove ( existingJobFunctionRole );
            }

            var staffSystemRole = new StaffSystemRole ( jobFunctionRole ) { Staff = this };
            _systemRoles.Add ( staffSystemRole );

            NotifyItemAdded ( () => SystemRoles, staffSystemRole );
        }

        /// <summary>
        /// Remove job function role.
        /// </summary>
        public virtual void RemoveJobFunctionRole ()
        {
            var existingJobFunctionRoles =
                ( from sr in _systemRoles where sr.SystemRole.SystemRoleType == SystemRoleType.JobFunction select sr ).ToList ();

            foreach ( var existingJobFunctionRole in existingJobFunctionRoles )
            {
                _systemRoles.Remove ( existingJobFunctionRole );
            }
        }

        /// <summary>
        /// Assign task group role.
        /// </summary>
        /// <param name="taskGroupRole">
        /// The task group role.
        /// </param>
        public virtual void AssignTaskGroupRole ( SystemRole taskGroupRole )
        {
            AssignSystemRole ( taskGroupRole, SystemRoleType.TaskGroup );
        }

        /// <summary>
        /// Remove task group role.
        /// </summary>
        /// <param name="taskGroupRole">
        /// The task group role.
        /// </param>
        public virtual void RemoveTaskGroupRole ( SystemRole taskGroupRole )
        {
            RemoveSystemRole ( taskGroupRole, SystemRoleType.TaskGroup );
        }

        /// <summary>
        /// Assign task role.
        /// </summary>
        /// <param name="taskRole">
        /// The task role.
        /// </param>
        public virtual void AssignTaskRole ( SystemRole taskRole )
        {
            AssignSystemRole ( taskRole, SystemRoleType.Task );
        }

        /// <summary>
        /// Remove task role.
        /// </summary>
        /// <param name="taskRole">
        /// The task role.
        /// </param>
        public virtual void RemoveTaskRole ( SystemRole taskRole )
        {
            RemoveSystemRole ( taskRole, SystemRoleType.Task );
        }

        /// <summary>
        /// Add checklist item.
        /// </summary>
        /// <param name="staffChecklist">
        /// The staff checklist.
        /// </param>
        public virtual void AddChecklistItem ( StaffChecklistItem staffChecklist )
        {
            staffChecklist.Staff = this;
            _staffChecklist.Add ( staffChecklist );

            NotifyItemAdded ( () => StaffChecklist, staffChecklist );
        }

        /// <summary>
        /// Remove checklist item.
        /// </summary>
        /// <param name="staffChecklistItem">
        /// The staff checklist item.
        /// </param>
        public virtual void RemoveChecklistItem ( StaffChecklistItem staffChecklistItem )
        {
            _staffChecklist.Remove ( staffChecklistItem );
            NotifyItemRemoved ( () => StaffChecklist, staffChecklistItem );
        }

        /// <summary>
        /// Assign location.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        public virtual void AssignLocation ( Location location )
        {
            Check.IsNotNull ( location, "Location is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, Location> ( this, () => AssignLocation )
                .WithContext ( location )
                .Execute(() =>
                {
                    var staffLocationAssignment = new StaffLocationAssignment(location) { Staff = this };
                    _staffLocationAssignments.Add(staffLocationAssignment);
                    NotifyItemAdded(() => StaffLocationAssignments, staffLocationAssignment);
                });
        }

        /// <summary>
        /// Remove location assignment.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        public virtual void RemoveLocationAssignment ( Location location )
        {
            Check.IsNotNull ( location, "Location is required." );

            for ( var index = 0; index < _staffLocationAssignments.Count; index++ )
            {
                if ( _staffLocationAssignments[index].Staff.Key != Key || _staffLocationAssignments[index].Location.Key != location.Key )
                {
                    continue;
                }

                var staffLocationAssignmentToBeRemoved = _staffLocationAssignments[index];
                _staffLocationAssignments.Remove ( staffLocationAssignmentToBeRemoved );
                NotifyItemRemoved ( () => StaffLocationAssignments, staffLocationAssignmentToBeRemoved );
                index--;
            }
        }

        /// <summary>
        /// Add event.
        /// </summary>
        /// <param name="staffEvent">
        /// The staff event.
        /// </param>
        protected internal virtual void AddEvent ( StaffEvent staffEvent )
        {
            staffEvent.Staff = this;
            _staffEvents.Add ( staffEvent );

            NotifyItemAdded ( () => StaffEvents, staffEvent );
        }

        /// <summary>
        /// Remove event.
        /// </summary>
        /// <param name="staffEvent">
        /// The staff event.
        /// </param>
        public virtual void RemoveEvent ( StaffEvent staffEvent )
        {
            _staffEvents.Remove ( staffEvent );
            NotifyItemRemoved ( () => StaffEvents, staffEvent );
        }

        /// <summary>
        /// Assign system role.
        /// </summary>
        /// <param name="systemRole">
        /// The system role.
        /// </param>
        /// <param name="systemRoleType">
        /// The system role type.
        /// </param>
        private void AssignSystemRole ( SystemRole systemRole, SystemRoleType systemRoleType )
        {
            CheckRoleAssignment ( systemRole, systemRoleType );

            var staffSystemRole = new StaffSystemRole ( systemRole ) { Staff = this };
            _systemRoles.Add ( staffSystemRole );

            NotifyItemAdded ( () => SystemRoles, staffSystemRole );
        }

        /// <summary>
        /// Removes the system role.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        /// <param name="systemRoleType">Type of the system role.</param>
        private void RemoveSystemRole ( SystemRole systemRole, SystemRoleType systemRoleType )
        {
            Check.IsNotNull ( systemRole, "System role is required." );

            if ( systemRole.SystemRoleType != systemRoleType )
            {
                throw new ArgumentException ( string.Format ( ( string )"The type of the role is not a {0} role. ", ( object )systemRoleType ) );
            }

            StaffSystemRole existingSystemRole = ( from sa in _systemRoles where sa.SystemRole.Key == systemRole.Key select sa ).FirstOrDefault ();
            if ( existingSystemRole != null )
            {
                _systemRoles.Remove ( existingSystemRole );
            }

            NotifyItemRemoved ( () => SystemRoles, existingSystemRole );
        }

        /// <summary>
        /// Checks the role assignment.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        /// <param name="systemRoleType">Type of the system role.</param>
        private void CheckRoleAssignment ( SystemRole systemRole, SystemRoleType systemRoleType )
        {
            Check.IsNotNull ( systemRole, "System role is required." );

            if ( systemRole.SystemRoleType != systemRoleType )
            {
                throw new ArgumentException ( string.Format ( ( string )"The type of the role is not a {0} role. ", ( object )systemRoleType ) );
            }

            StaffSystemRole existingStaffSystemRole =
                ( from sr in _systemRoles where sr.SystemRole.Key == systemRole.Key select sr ).FirstOrDefault ();
            if ( existingStaffSystemRole != null )
            {
                throw new InvalidOperationException("The same has already been Assigned.");
            }
        }

        /// <summary>
        /// Add address.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public virtual void AddAddress ( StaffAddress address )
        {
            Check.IsNotNull ( address, "Address is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, StaffAddress> ( this, () => AddAddress )
                .WithContext ( address )
                .Execute(() =>
                {
                    address.Staff = this;
                    _addresses.Add(address);
                    NotifyItemAdded(() => Addresses, address);
                });
        }

        /// <summary>
        /// Remove address.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public virtual void RemoveAddress ( StaffAddress address )
        {
            Check.IsNotNull ( address, "Address is required." );
            _addresses.Remove ( address );
            NotifyItemRemoved ( () => Addresses, address );
        }

        /// <summary>
        /// Add phone.
        /// </summary>
        /// <param name="phone">
        /// The phone.
        /// </param>
        public virtual void AddPhone ( StaffPhone phone )
        {
            Check.IsNotNull ( phone, "Phone is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, StaffPhone> ( this, () => AddPhone )
                .WithContext ( phone )
                .Execute(() =>
                {
                    phone.Staff = this;
                    _phoneNumbers.Add(phone);
                    NotifyItemAdded(() => PhoneNumbers, phone);
                });
        }

        /// <summary>
        /// Remove phone.
        /// </summary>
        /// <param name="phone">
        /// The phone.
        /// </param>
        public virtual void RemovePhone ( StaffPhone phone )
        {
            Check.IsNotNull ( phone, "Phone is required." );
            _phoneNumbers.Remove ( phone );
            NotifyItemRemoved ( () => PhoneNumbers, phone );
        }

        /// <summary>
        /// Add identifier.
        /// </summary>
        /// <param name="identifier">
        /// The identifier.
        /// </param>
        public virtual void AddIdentifier ( StaffIdentifier identifier )
        {
            Check.IsNotNull ( identifier, "Identifier is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, StaffIdentifier> ( this, () => AddIdentifier )
                .WithContext ( identifier )
                .Execute(() =>
                {
                    identifier.Staff = this;
                    _identifiers.Add(identifier);
                    NotifyItemAdded(() => Identifiers, identifier);
                });
        }

        /// <summary>
        /// Remove identifier.
        /// </summary>
        /// <param name="identifier">
        /// The identifier.
        /// </param>
        public virtual void RemoveIdentifier ( StaffIdentifier identifier )
        {
            Check.IsNotNull ( identifier, "Identifier is required." );
            _identifiers.Remove ( identifier );
            NotifyItemRemoved ( () => Identifiers, identifier );
        }

        /// <summary>
        /// Revise staff profile.
        /// </summary>
        /// <param name="staffProfile">
        /// The staff profile.
        /// </param>
        public virtual void ReviseStaffProfile ( StaffProfile staffProfile )
        {
            Check.IsNotNull ( staffProfile, () => StaffProfile );

            StaffProfile = staffProfile;
        }

        /// <summary>
        /// The revise the staff hr.
        /// </summary>
        /// <param name="staffHr">
        /// The staff hr.
        /// </param>
        public virtual void ReviseStaffHr ( StaffHr staffHr )
        {
            Check.IsNotNull ( staffHr, () => StaffHr );

            StaffHr = staffHr;
        }

        /// <summary>
        /// Add college degree.
        /// </summary>
        /// <param name="collegeDegree">
        /// The college degree.
        /// </param>
        public virtual void AddCollegeDegree ( StaffCollegeDegree collegeDegree )
        {
            Check.IsNotNull ( collegeDegree, "CollegeDegree is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, StaffCollegeDegree> ( this, () => AddCollegeDegree )
                .WithContext ( collegeDegree )
                .Execute(() =>
                {
                    collegeDegree.Staff = this;
                    _collegeDegrees.Add(collegeDegree);
                    NotifyItemAdded(() => CollegeDegrees, collegeDegree);
                });
        }

        /// <summary>
        /// Removes the college degree.
        /// </summary>
        /// <param name="collegeDegree">
        /// The college degree.
        /// </param>
        public virtual void RemoveCollegeDegree ( StaffCollegeDegree collegeDegree )
        {
            Check.IsNotNull ( collegeDegree, "CollegeDegree is required." );
            _collegeDegrees.Remove ( collegeDegree );
            NotifyItemRemoved ( () => Certifications, collegeDegree );
        }

        /// <summary>
        /// The add license.
        /// </summary>
        /// <param name="license">
        /// The license.
        /// </param>
        public virtual void AddLicense ( StaffLicense license )
        {
            Check.IsNotNull ( license, "License is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, StaffLicense> ( this, () => AddLicense )
                .WithContext ( license )
                .Execute(() =>
                {
                    license.Staff = this;
                    _licenses.Add(license);
                    NotifyItemAdded(() => Licenses, license);
                });
        }

        /// <summary>
        /// Removes the license.
        /// </summary>
        /// <param name="license">
        /// The license.
        /// </param>
        public virtual void RemoveLicense ( StaffLicense license )
        {
            _licenses.Remove ( license );
            NotifyItemRemoved ( () => Licenses, license );
        }

        /// <summary>
        /// Sets the primary location.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        public virtual void SetPrimaryLocation ( Location location )
        {
            DomainRuleEngine.CreateRuleEngine<Staff, Location> ( this, () => SetPrimaryLocation )
                .WithContext ( location )
                .Execute ( () => PrimaryLocation = location );
        }

        /// <summary>
        /// Adds the certification.
        /// </summary>
        /// <param name="certification">
        /// The certification.
        /// </param>
        public virtual void AddCertification ( StaffCertification certification )
        {
            Check.IsNotNull ( certification, "Certification is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, StaffCertification> ( this, () => AddCertification )
                .WithContext ( certification )
                .Execute(() =>
                {
                    certification.Staff = this;
                    _certifications.Add(certification);
                    NotifyItemAdded(() => Certifications, certification);
                });
        }

        /// <summary>
        /// Removes the certification.
        /// </summary>
        /// <param name="certification">
        /// The certification.
        /// </param>
        public virtual void RemoveCertification ( StaffCertification certification )
        {
            _certifications.Remove ( certification );
            NotifyItemRemoved ( () => Certifications, certification );
        }

        /// <summary>
        /// Adds the training course.
        /// </summary>
        /// <param name="trainingCourse">
        /// The training course.
        /// </param>
        public virtual void AddTrainingCourse ( StaffTrainingCourse trainingCourse )
        {
            Check.IsNotNull ( trainingCourse, "Training course is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, StaffTrainingCourse> ( this, () => AddTrainingCourse )
                .WithContext ( trainingCourse )
                .Execute(() =>
                {
                    trainingCourse.Staff = this;
                    _trainingCourses.Add(trainingCourse);
                    NotifyItemAdded(() => TrainingCourses, trainingCourse);
                });
        }

        /// <summary>
        /// Removes the training course.
        /// </summary>
        /// <param name="trainingCourse">
        /// The training course.
        /// </param>
        public virtual void RemoveTrainingCourse ( StaffTrainingCourse trainingCourse )
        {
            _trainingCourses.Remove ( trainingCourse );
            NotifyItemRemoved ( () => TrainingCourses, trainingCourse );
        }

        /// <summary>
        /// Revises the system acount.
        /// </summary>
        /// <param name="systemAccount">
        /// The system account.
        /// </param>
        public virtual void ReviseSystemAcount ( SystemAccount systemAccount )
        {
            SystemAccount = systemAccount;
        }

        /// <summary>
        /// Creates the photo.
        /// </summary>
        /// <param name="picture">
        /// The picture.
        /// </param>
        /// <returns>
        /// A StaffPhoto.
        /// </returns>
        public virtual StaffPhoto CreatePhoto ( byte[] picture )
        {
            var staffPhoto = new StaffPhoto ( picture );
            _staffPhoto = staffPhoto;
            return staffPhoto;
        }

        /// <summary>
        /// Revises the staff profile info.
        /// </summary>
        /// <param name="staffProfile">
        /// The staff profile.
        /// </param>
        public virtual void ReviseStaffProfileInfo ( StaffProfile staffProfile )
        {
            Check.IsNotNull ( staffProfile, () => StaffProfile );
            StaffProfile = staffProfile;
        }

        /// <summary>
        /// Adds the language.
        /// </summary>
        /// <param name="language">
        /// The language.
        /// </param>
        public virtual void AddLanguage ( StaffLanguage language )
        {
            Check.IsNotNull ( language, "Language is required." );

            DomainRuleEngine.CreateRuleEngine<Staff, StaffLanguage> ( this, () => AddLanguage )
                .WithContext ( language )
                .Execute(() =>
                {
                    language.Staff = this;
                    _languages.Add(language);
                    NotifyItemAdded(() => Languages, language);
                });
        }

        /// <summary>
        /// Removes the language.
        /// </summary>
        /// <param name="language">
        /// The language.
        /// </param>
        public virtual void RemoveLanguage ( StaffLanguage language )
        {
            Check.IsNotNull ( language, "Language is required." );
            _languages.Remove ( language );
            NotifyItemRemoved ( () => Languages, language );
        }

        #region Overrides 

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A string.
        /// </returns>
        public override string ToString ()
        {
            return _staffProfile.StaffName.Complete;
        }

        #endregion
    }
}
