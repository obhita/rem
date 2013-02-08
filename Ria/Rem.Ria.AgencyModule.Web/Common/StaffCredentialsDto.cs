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

using System.Runtime.Serialization;
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for StaffCredentials class.
    /// </summary>
    [DataContract]
    public class StaffCredentialsDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private SoftDeleteObservableCollection<StaffCertificationDto> _certifications;
        private SoftDeleteObservableCollection<StaffCollegeDegreeDto> _collegeDegrees;
        private SoftDeleteObservableCollection<StaffLicenseDto> _licenses;
        private SoftDeleteObservableCollection<StaffTrainingCourseDto> _trainingCourses;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the certifications.
        /// </summary>
        /// <value>The certifications.</value>
        [DataMember]
        public SoftDeleteObservableCollection<StaffCertificationDto> Certifications
        {
            get { return _certifications; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _certifications, () => Certifications, value ); }
        }

        /// <summary>
        /// Gets or sets the college degrees.
        /// </summary>
        /// <value>The college degrees.</value>
        [DataMember]
        public SoftDeleteObservableCollection<StaffCollegeDegreeDto> CollegeDegrees
        {
            get { return _collegeDegrees; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _collegeDegrees, () => CollegeDegrees, value ); }
        }

        /// <summary>
        /// Gets or sets the licenses.
        /// </summary>
        /// <value>The licenses.</value>
        [DataMember]
        public SoftDeleteObservableCollection<StaffLicenseDto> Licenses
        {
            get { return _licenses; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _licenses, () => Licenses, value ); }
        }

        /// <summary>
        /// Gets or sets the training courses.
        /// </summary>
        /// <value>The training courses.</value>
        [DataMember]
        public SoftDeleteObservableCollection<StaffTrainingCourseDto> TrainingCourses
        {
            get { return _trainingCourses; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _trainingCourses, () => TrainingCourses, value ); }
        }

        #endregion
    }
}
