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

using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.StaffEditor
{
    /// <summary>
    /// Class for handling save staff hr request.
    /// </summary>
    public class SaveStaffHrRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<StaffHRDto>, DtoResponse<StaffHRDto>, StaffHRDto, Staff>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveStaffHrRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveStaffHrRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
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
        protected override bool ProcessSingleAggregate ( StaffHRDto dto, Staff staff )
        {
            var supervisorStaff = dto.SupervisorStaff == null ? null : Session.Get<Staff> ( dto.SupervisorStaff.Key );
            var employmentType = _mappingHelper.MapLookupField<EmploymentType> ( dto.EmploymentType );

            staff.ReviseStaffHr (
                new StaffHrBuilder ().WithEmploymentType ( employmentType ).WithTitleName ( dto.TitleName ).WithSupervisorStaff ( supervisorStaff )
                .WithConfidentialNote ( dto.ConfidentialNote ) );

            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffChecklistItemDto, Staff, StaffChecklistItem> (
                    dto.StaffChecklist, staff, staff.StaffChecklist ).MapChangedItem ( ChangeStaffChecklistItem ).Map ();

            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffEventDto, Staff, StaffEvent> ( dto.StaffEvents, staff, staff.StaffEvents ).MapChangedItem (
                    ChangeStaffEvent ).Map ();

            return _mappingResult;
        }

        private void ChangeStaffChecklistItem ( StaffChecklistItemDto staffChecklistItemDto, Staff staff, StaffChecklistItem staffChecklistItem )
        {
            var staffChecklistItemType = _mappingHelper.MapLookupField<StaffChecklistItemType> ( staffChecklistItemDto.StaffChecklistItemType );

            staffChecklistItem.ReviseChecklistItemType ( staffChecklistItemType );
            staffChecklistItem.ReviseCheckedIndicator ( staffChecklistItemDto.CheckedIndicator.GetValueOrDefault () );
        }

        private void ChangeStaffEvent ( StaffEventDto staffEventDto, Staff staff, StaffEvent staffEvent )
        {
            var staffEventType = _mappingHelper.MapLookupField<StaffEventType> ( staffEventDto.StaffEventType );

            staffEvent.ReviseEventType ( staffEventType );
            staffEvent.ReviseEventDate ( staffEventDto.EventDate );
        }

        #endregion
    }
}
