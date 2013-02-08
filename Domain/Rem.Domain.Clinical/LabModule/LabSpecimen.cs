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
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.LabModule
{ 
    /// <summary>
    /// LabSpecimen is an <see cref="Activity">Activity</see> that defines the collection of a patient laboratory specimen.
    /// </summary>
    public class LabSpecimen : Activity
    {
        private readonly IList<LabTest> _labTests;

        private LabSpecimenType _labSpecimenType;
        private DateTime? _labReceivedDate;
        private LabFacility _labFacility;
        private bool? _collectedHereIndicator;
        private string _testNotCompletedReasonDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSpecimen"/> class.
        /// </summary>
        protected LabSpecimen()
        {
            _labTests = new List<LabTest>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSpecimen"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal LabSpecimen(Visit visit, ActivityType activityType)
            : base(visit, activityType)
        {
            _labTests = new List<LabTest>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSpecimen"/> class.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="provenance">The provenance.</param>
        /// <param name="activityDateTimeRange">The activity date time range.</param>
        protected internal LabSpecimen(ClinicalCase clinicalCase, ActivityType activityType, Provenance provenance, DateTimeRange activityDateTimeRange)
            : base(clinicalCase, activityType, provenance, activityDateTimeRange)
        {
            _labTests = new List<LabTest>();
        }

        /// <summary>
        /// Gets the type of the lab specimen.
        /// </summary>
        /// <value>
        /// The type of the lab specimen.
        /// </value>
        public virtual LabSpecimenType LabSpecimenType
        {
            get { return _labSpecimenType; }
            private set { ApplyPropertyChange(ref _labSpecimenType, () => LabSpecimenType, value); }
        }

        /// <summary>
        /// Gets the lab received date.
        /// </summary>
        public virtual DateTime? LabReceivedDate
        {
            get { return _labReceivedDate; }
            private set { ApplyPropertyChange(ref _labReceivedDate, () => LabReceivedDate, value); }
        }

        /// <summary>
        /// Gets the lab facility.
        /// </summary>
        public virtual LabFacility LabFacility
        {
            get { return _labFacility; }
            private set { ApplyPropertyChange(ref _labFacility, () => LabFacility, value); }
        }

        /// <summary>
        /// Gets the test not completed reason description.
        /// </summary>
        public virtual string TestNotCompletedReasonDescription
        {
            get { return _testNotCompletedReasonDescription; }
            private set { ApplyPropertyChange(ref _testNotCompletedReasonDescription, () => TestNotCompletedReasonDescription, value); }
        }

        /// <summary>
        /// Gets the lab tests.
        /// </summary>
        public virtual IList<LabTest> LabTests
        {
            get { return _labTests.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the collected here indicator.
        /// </summary>
        public virtual bool? CollectedHereIndicator
        {
            get { return _collectedHereIndicator; }
            private set { ApplyPropertyChange(ref _collectedHereIndicator, () => CollectedHereIndicator, value); }
        }

        /// <summary>
        /// Revises the type of the lab specimen.
        /// </summary>
        /// <param name="labSpecimenType">Type of the lab specimen.</param>
        public virtual void ReviseLabSpecimenType(LabSpecimenType labSpecimenType)
        {
            Check.IsNotNull ( labSpecimenType, () => LabSpecimenType );
            LabSpecimenType = labSpecimenType;
        }

        /// <summary>
        /// Revises the lab facility.
        /// </summary>
        /// <param name="labFacility">The lab facility.</param>
        public virtual void ReviseLabFacility(LabFacility labFacility)
        {
            Check.IsNotNull(labFacility, () => LabFacility);
            LabFacility = labFacility;
        }

        /// <summary>
        /// Revises the lab received date.
        /// </summary>
        /// <param name="labReceivedDate">The lab received date.</param>
        public virtual void ReviseLabReceivedDate(DateTime? labReceivedDate)
        {
            LabReceivedDate = labReceivedDate;
        }

        /// <summary>
        /// Revises the collected here indicator.
        /// </summary>
        /// <param name="collectedHereIndicator">The collected here indicator.</param>
        public virtual void ReviseCollectedHereIndicator(bool? collectedHereIndicator)
        {
            CollectedHereIndicator = collectedHereIndicator;
        }

        /// <summary>
        /// Revises the test not completed reason description.
        /// </summary>
        /// <param name="testNotCompletedReasonDescription">The test not completed reason description.</param>
        public virtual void ReviseTestNotCompletedReasonDescription(string testNotCompletedReasonDescription)
        {
            TestNotCompletedReasonDescription = testNotCompletedReasonDescription;
        }

        /// <summary>
        /// Adds the lab test.
        /// </summary>
        /// <param name="labTestInfo">The lab test info.</param>
        /// <returns>A LabTest.</returns>
        public virtual LabTest AddLabTest(LabTestInfo labTestInfo)
        {
            var labTest = new LabTest { LabSpecimen = this };
            labTest.ReviseLabTestInfo ( labTestInfo );
            _labTests.Add(labTest);

            NotifyItemAdded(() => LabTests, labTest);
            return labTest;
        }

        /// <summary>
        /// Removes the lab test.
        /// </summary>
        /// <param name="labTest">The lab test.</param>
        public virtual void RemoveLabTest(LabTest labTest)
        {
            _labTests.Remove(labTest);
            NotifyItemRemoved(() => LabTests, labTest);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return LabSpecimenType == null
                       ? base.ToString ()
                       : LabSpecimenType.ToString ();
        }
    }
}
