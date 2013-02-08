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

using System.Linq;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.AgencyModule.Web.StaffEditor
{
    /// <summary>
    /// Class for handling save staff system roles request.
    /// </summary>
    public class SaveStaffSystemRolesRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<StaffSystemRolesDto>, DtoResponse<StaffSystemRolesDto>, StaffSystemRolesDto, Staff>
    {
        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="staff">The staff.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( StaffSystemRolesDto dto, Staff staff )
        {
            var mappingResult = true;

            mappingResult &= new AggregateNodeLookupCollectionMapper<StaffSystemRoleDto, Staff, StaffSystemRole> (
                dto.TaskGroupRoles,
                staff,
                staff.SystemRoles )
                .MapAddedItem ( AddTaskGroupSystemRole )
                .MapRemovedItem ( RemoveTaskGroupSystemRole )
                .FindCollectionEntity ( ( pr, key ) => staff.SystemRoles.FirstOrDefault ( r => r.Key == key ) )
                .Map ();

            mappingResult &= new AggregateNodeLookupCollectionMapper<StaffSystemRoleDto, Staff, StaffSystemRole> (
                dto.TaskRoles,
                staff,
                staff.SystemRoles )
                .MapAddedItem ( AddTaskSystemRole )
                .MapRemovedItem ( RemoveTaskSystemRole )
                .FindCollectionEntity ( ( pr, key ) => staff.SystemRoles.FirstOrDefault ( r => r.Key == key ) )
                .Map ();

            if ( dto.JobFunctionRole != null && dto.JobFunctionRole.SystemRole.Key > 0 )
            {
                var systemRole = Session.Load<SystemRole> ( dto.JobFunctionRole.SystemRole.Key );
                staff.ReviseJobFunctionRole ( systemRole );
            }
            else
            {
                staff.RemoveJobFunctionRole ();
            }

            return mappingResult;
        }

        private void AddTaskGroupSystemRole ( StaffSystemRoleDto dto, Staff staff )
        {
            var systemRole = Session.Load<SystemRole> ( dto.SystemRole.Key );
            staff.AssignTaskGroupRole ( systemRole );
        }

        private void AddTaskSystemRole ( StaffSystemRoleDto dto, Staff staff )
        {
            var systemRole = Session.Load<SystemRole> ( dto.SystemRole.Key );
            staff.AssignTaskRole ( systemRole );
        }

        private void RemoveTaskGroupSystemRole ( StaffSystemRoleDto dto, Staff staff, StaffSystemRole staffSystemRole )
        {
            staff.RemoveTaskGroupRole ( staffSystemRole.SystemRole );
        }

        private void RemoveTaskSystemRole ( StaffSystemRoleDto dto, Staff staff, StaffSystemRole staffSystemRole )
        {
            staff.RemoveTaskRole ( staffSystemRole.SystemRole );
        }

        #endregion
    }
}
