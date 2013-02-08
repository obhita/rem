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

using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.StaffEditor
{
    /// <summary>
    /// Class for handling save staff credentials request.
    /// </summary>
    public class SaveStaffCredentialsRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<StaffCredentialsDto>, DtoResponse<StaffCredentialsDto>, StaffCredentialsDto, Staff>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveStaffCredentialsRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveStaffCredentialsRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="staff">The staff.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( StaffCredentialsDto dto, Staff staff )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffCollegeDegreeDto, Staff, StaffCollegeDegree> (
                    dto.CollegeDegrees, staff, staff.CollegeDegrees ).MapRemovedItem ( RemoveStaffCollegeDegree ).MapAddedItem (
                        AddStaffCollegeDegree ).MapChangedItem ( ChangeStaffCollegeDegree ).Map ();

            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffLicenseDto, Staff, StaffLicense> (
                    dto.Licenses, staff, staff.Licenses ).MapRemovedItem ( RemoveStaffLicense ).MapAddedItem ( AddStaffLicense )
                    .MapChangedItem ( ChangeStaffLicense ).Map ();

            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffCertificationDto, Staff, StaffCertification> (
                    dto.Certifications, staff, staff.Certifications ).MapRemovedItem ( RemoveStaffCertification ).MapAddedItem (
                        AddStaffCertification ).MapChangedItem ( ChangeStaffCertification ).Map ();

            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffTrainingCourseDto, Staff, StaffTrainingCourse> (
                    dto.TrainingCourses, staff, staff.TrainingCourses ).MapRemovedItem ( RemoveStaffTrainingCourse ).MapAddedItem (
                        AddStaffTrainingCourse ).MapChangedItem ( ChangeStaffTrainingCourse ).Map ();

            return _mappingResult;
        }

        private void AddStaffCertification ( StaffCertificationDto dto, Staff staff )
        {
            var certification = _mappingHelper.MapLookupField<Certification> ( dto.Certification );
            var staffCertification = new StaffCertification ( certification, new DateRange ( dto.StartDate, dto.EndDate ) );
            staff.AddCertification ( staffCertification );
        }

        private void AddStaffCollegeDegree ( StaffCollegeDegreeDto dto, Staff staff )
        {
            var collegeDegree = _mappingHelper.MapLookupField<CollegeDegree> ( dto.CollegeDegree );
            var staffCollegeDegree = new StaffCollegeDegree ( collegeDegree, dto.EarnedDate );
            staff.AddCollegeDegree ( staffCollegeDegree );
        }

        private void AddStaffLicense ( StaffLicenseDto dto, Staff staff )
        {
            var license = _mappingHelper.MapLookupField<License> ( dto.License );
            var staffLicense = new StaffLicense ( license, new DateRange ( dto.StartDate, dto.EndDate ) );
            staff.AddLicense ( staffLicense );
        }

        private void AddStaffTrainingCourse ( StaffTrainingCourseDto dto, Staff staff )
        {
            var trainingCourse = _mappingHelper.MapLookupField<TrainingCourse> ( dto.TrainingCourse );
            var staffTrainingCourse = new StaffTrainingCourse ( trainingCourse, null, dto.CompletedDate );
            staff.AddTrainingCourse ( staffTrainingCourse );
        }

        private void ChangeStaffCertification ( StaffCertificationDto dto, Staff staff, StaffCertification staffCertification )
        {
            RemoveStaffCertification ( dto, staff, staffCertification );
            AddStaffCertification ( dto, staff );
        }

        private void ChangeStaffCollegeDegree ( StaffCollegeDegreeDto dto, Staff staff, StaffCollegeDegree staffCollegeDegree )
        {
            RemoveStaffCollegeDegree ( dto, staff, staffCollegeDegree );
            AddStaffCollegeDegree ( dto, staff );
        }

        private void ChangeStaffLicense ( StaffLicenseDto dto, Staff staff, StaffLicense staffLicense )
        {
            RemoveStaffLicense ( dto, staff, staffLicense );
            AddStaffLicense ( dto, staff );
        }

        private void ChangeStaffTrainingCourse ( StaffTrainingCourseDto dto, Staff staff, StaffTrainingCourse staffTrainingCourse )
        {
            RemoveStaffTrainingCourse ( dto, staff, staffTrainingCourse );
            AddStaffTrainingCourse ( dto, staff );
        }

        private void RemoveStaffCertification ( StaffCertificationDto dto, Staff staff, StaffCertification staffCertification )
        {
            staff.RemoveCertification ( staffCertification );
        }

        private void RemoveStaffCollegeDegree ( StaffCollegeDegreeDto dto, Staff staff, StaffCollegeDegree staffCollegeDegree )
        {
            staff.RemoveCollegeDegree ( staffCollegeDegree );
        }

        private void RemoveStaffLicense ( StaffLicenseDto dto, Staff staff, StaffLicense staffLicense )
        {
            staff.RemoveLicense ( staffLicense );
        }

        private void RemoveStaffTrainingCourse ( StaffTrainingCourseDto dto, Staff staff, StaffTrainingCourse staffTrainingCourse )
        {
            staff.RemoveTrainingCourse ( staffTrainingCourse );
        }

        #endregion
    }
}
