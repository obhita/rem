﻿#region License

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

using System.Collections.Generic;
using AutoMapper;
using Pillar.Common.Collections;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.Web.LocationEditor
{
    /// <summary>
    /// Factory for location contacts dto.
    /// </summary>
    public class LocationContactsDtoFactory : IKeyedDtoFactory<LocationContactsDto>
    {
        #region Constants and Fields

        private readonly ISessionProvider _sessionProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationContactsDtoFactory"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public LocationContactsDtoFactory ( ISessionProvider sessionProvider )
        {
            _sessionProvider = sessionProvider;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the keyed dto.
        /// </summary>
        /// <param name="key">The key to create.</param>
        /// <returns>A <see cref="Rem.Ria.AgencyModule.Web.LocationEditor.LocationContactsDto"/></returns>
        public LocationContactsDto CreateKeyedDto ( long key )
        {
            var location = _sessionProvider.GetSession ().Get<Location> ( key );
            var locationContactsDto =
                Mapper.Map<IList<LocationContact>, IList<LocationContactDto>> ( new List<LocationContact> ( location.LocationContacts ) );

            var dto = new LocationContactsDto
                {
                    LocationContacts = new SoftDeleteObservableCollection<LocationContactDto> ( locationContactsDto ),
                    Key = key
                };

            return dto;
        }

        #endregion
    }
}
