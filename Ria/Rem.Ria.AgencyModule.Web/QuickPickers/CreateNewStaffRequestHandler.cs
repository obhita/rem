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
using Agatha.Common;
using AutoMapper;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.AgencyModule.Web.QuickPickers
{
    /// <summary>
    /// Class for handling create new staff request.
    /// </summary>
    public class CreateNewStaffRequestHandler : NHibernateSessionRequestHandler<CreateNewStaffRequest, CreateNewStaffResponse>
    {
        #region Constants and Fields

        private readonly IAgencyRepository _agencyRepository;
        private readonly IStaffFactory _staffFactory;
        private readonly IStaffRepository _staffRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewStaffRequestHandler"/> class.
        /// </summary>
        /// <param name="agencyRepository">The agency repository.</param>
        /// <param name="staffRepository">The staff repository.</param>
        /// <param name="staffFactory">The staff factory.</param>
        public CreateNewStaffRequestHandler ( IAgencyRepository agencyRepository, IStaffRepository staffRepository, IStaffFactory staffFactory )
        {
            _agencyRepository = agencyRepository;
            _staffRepository = staffRepository;
            _staffFactory = staffFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( CreateNewStaffRequest request )
        {
            var agencyKey = request.AgencyKey;
            var firstName = request.FirstName;
            var middleInitial = request.MiddleInitial;
            var lastName = request.LastName;

            var agency = _agencyRepository.GetByKey ( agencyKey );
            if ( agency == null )
            {
                throw new ArgumentException ( string.Format ( "Agency Key: {0} was not found.", agencyKey ) );
            }

            var staffDto = new StaffNameDto { FirstName = firstName, MiddleInitial = middleInitial, LastName = lastName };

            var duplicateStaff = _staffRepository.FindDuplicateStaff ( firstName, middleInitial, lastName );
            if ( duplicateStaff != null )
            {
                staffDto.AddDataErrorInfo (
                    string.IsNullOrEmpty ( middleInitial )
                        ? new DataErrorInfo ( string.Format ( "Duplicate Staff {0} {1}. Please enter M.I.", firstName, lastName ), ErrorLevel.Error )
                        : new DataErrorInfo (
                              string.Format ( "Cannot add Duplicate Staff {0} {1} {2}.", firstName, middleInitial, lastName ), ErrorLevel.Error ) );
            }
            else
            {
                var staff = _staffFactory.CreateStaff (
                    agency,
                    new StaffProfileBuilder ().WithStaffName (
                        new PersonNameBuilder ().WithFirst ( firstName ).WithLast ( lastName ).WithMiddle ( middleInitial ) ) );

                staffDto = Mapper.Map<Staff, StaffNameDto> ( staff );
            }

            var response = CreateTypedResponse ();
            response.StaffNameDto = staffDto;

            return response;
        }

        #endregion
    }
}
