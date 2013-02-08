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
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;
using VisitStatus = Rem.WellKnownNames.VisitModule.VisitStatus;

namespace Rem.Ria.PatientModule.Web.Service
{
    /// <summary>
    /// Class for handling update visit status request.
    /// </summary>
    public class UpdateVisitStatusRequestHandler :
        NHibernateSessionRequestHandler<UpdateVisitStatusRequest, UpdateVisitStatusResponse>
    {
        #region Constants and Fields

        private readonly IVisitRepository _visitRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVisitStatusRequestHandler"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        public UpdateVisitStatusRequestHandler ( IVisitRepository visitRepository )
        {
            _visitRepository = visitRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( UpdateVisitStatusRequest request )
        {
            var visitStatusUpdateDto = request.VisitStatusUpdateDto;

            var visit = _visitRepository.GetByKey ( visitStatusUpdateDto.VisitKey );
            if ( visit == null )
            {
                throw new ArgumentException ( "Cannot find Visit with key:" + visitStatusUpdateDto.VisitKey );
            }

            var wellKnownName = visitStatusUpdateDto.VisitStatus.WellKnownName;

            if ( visit.VisitStatus.WellKnownName != wellKnownName )
            {
                if ( wellKnownName == VisitStatus.Scheduled )
                {
                    visit.MarkVisitStatusAsScheduled ();
                }
                else if ( wellKnownName == VisitStatus.CheckedIn )
                {
                    visit.CheckIn ( visitStatusUpdateDto.UpdateDateTime );
                }
                else if ( wellKnownName == VisitStatus.NoShow )
                {
                    visit.MarkVisitStatusAsNoShow ();
                }
                else if ( wellKnownName == VisitStatus.Canceled )
                {
                    visit.Cancel ();
                }
                else
                {
                    throw new Exception ( "Invalid Visit Status: " + visitStatusUpdateDto.VisitStatus.Name );
                }
            }

            var response = CreateTypedResponse ();
            response.VisitStatusUpdateDto = visitStatusUpdateDto;

            return response;
        }

        #endregion
    }
}
