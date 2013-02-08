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

namespace Rem.Ria.AgencyModule.Web.StaffEditor
{
    /// <summary>
    /// Class for handling save location assignment request.
    /// </summary>
    public class SaveLocationAssignmentRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<StaffLocationAssignmentDto>, DtoResponse<StaffLocationAssignmentDto>, StaffLocationAssignmentDto, Staff>
    {
        #region Constants and Fields

        private bool _mappingResult = true;

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="staff">The staff.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( StaffLocationAssignmentDto dto, Staff staff )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffApprovedLocationDto, Staff, StaffLocationAssignment> (
                    dto.Locations, staff, staff.StaffLocationAssignments )
                    .MapRemovedItem ( RemoveApprovedLocation )
                    .MapAddedItem ( AddApprovedLocation )
                    .MapChangedItem ( ChangeApprovedLocation )
                    .Map ();

            return _mappingResult;
        }

        private void AddApprovedLocation ( StaffApprovedLocationDto staffApprovedLocationDto, Staff staff )
        {
            MapStaffApprovedLocation ( staffApprovedLocationDto, staff );
        }

        private void ChangeApprovedLocation (
            StaffApprovedLocationDto staffApprovedLocationDto, Staff staff, StaffLocationAssignment staffApprovedLocation )
        {
            MapStaffApprovedLocation ( staffApprovedLocationDto, staff );
        }

        private void MapStaffApprovedLocation ( StaffApprovedLocationDto staffApprovedLocationDto, Staff staff )
        {
            var location = Session.Get<Location> ( staffApprovedLocationDto.Location.Key );

            staff.AssignLocation ( location );
        }

        private void RemoveApprovedLocation (
            StaffApprovedLocationDto staffApprovedLocationDto, Staff staff, StaffLocationAssignment staffApprovedLocation )
        {
            staff.RemoveLocationAssignment ( staffApprovedLocation.Location );
        }

        #endregion
    }
}
