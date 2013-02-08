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

using Agatha.Common;
using AutoMapper;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.QuickPickers
{
    /// <summary>
    /// Class for handling create new agency request.
    /// </summary>
    public class CreateNewAgencyRequestHandler :
        NHibernateSessionRequestHandler<CreateNewAgencyRequest, DtoResponse<AgencyDisplayNameDto>>
    {
        #region Constants and Fields

        private readonly IAgencyFactory _agencyFactory;
        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewAgencyRequestHandler"/> class.
        /// </summary>
        /// <param name="agencyFactory">The agency factory.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        public CreateNewAgencyRequestHandler ( IAgencyFactory agencyFactory, IDtoToDomainMappingHelper mappingHelper )
        {
            _agencyFactory = agencyFactory;
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( CreateNewAgencyRequest request )
        {
            var legalName = request.AgencyName;
            var agencyType = _mappingHelper.MapLookupField<AgencyType> ( request.AgencyType );
            var agencyProfile =
                new AgencyProfileBuilder ().WithAgencyType ( agencyType ).WithAgencyName (
                    new AgencyNameBuilder ().WithLegalName ( legalName ).Build () );

            var agency = _agencyFactory.CreateAgency ( agencyProfile );
            var agencyDto = Mapper.Map<Agency, AgencyDisplayNameDto> ( agency );

            var response = CreateTypedResponse ();
            response.DataTransferObject = agencyDto;

            return response;
        }

        #endregion
    }
}
