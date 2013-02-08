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
    /// Class for handling save staff identifiers request.
    /// </summary>
    public class SaveStaffIdentifiersRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<StaffIdentifiersDto>, DtoResponse<StaffIdentifiersDto>, StaffIdentifiersDto, Staff>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveStaffIdentifiersRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveStaffIdentifiersRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
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
        protected override bool ProcessSingleAggregate ( StaffIdentifiersDto dto, Staff staff )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffIdentifierDto, Staff, StaffIdentifier> ( dto.Identifiers, staff, staff.Identifiers )
                .MapRemovedItem ( RemoveStaffIdentifier ).MapAddedItem ( AddStaffIdentifier ).MapChangedItem ( ChangeStaffIdentifier ).Map ();

            return _mappingResult;
        }

        private static void RemoveStaffIdentifier ( StaffIdentifierDto staffIdentifierDto, Staff staff, StaffIdentifier staffIdentifier )
        {
            staff.RemoveIdentifier ( staffIdentifier );
        }

        private void AddStaffIdentifier ( StaffIdentifierDto staffIdentifierDto, Staff staff )
        {
            var staffIdentifierType = _mappingHelper.MapLookupField<StaffIdentifierType> ( staffIdentifierDto.StaffIdentifierType );
            var staffIdentifier = new StaffIdentifier (
                staffIdentifierType, staffIdentifierDto.IdentifierNumber, new DateRange ( staffIdentifierDto.StartDate, staffIdentifierDto.EndDate ) );
            staff.AddIdentifier ( staffIdentifier );
        }

        private void ChangeStaffIdentifier ( StaffIdentifierDto staffIdentifierDto, Staff staff, StaffIdentifier staffIdentifier )
        {
            RemoveStaffIdentifier ( staffIdentifierDto, staff, staffIdentifier );
            AddStaffIdentifier ( staffIdentifierDto, staff );
        }

        #endregion
    }
}
