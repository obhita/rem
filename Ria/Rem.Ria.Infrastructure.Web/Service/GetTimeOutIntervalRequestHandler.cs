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
using Agatha.ServiceLayer;
using Pillar.Common.Configuration;

namespace Rem.Ria.Infrastructure.Web.Service
{
    /// <summary>
    /// GetTimeOutIntervalRequestHandler retrieves logout timer configuration values.
    /// </summary>
    public class GetTimeOutIntervalRequestHandler :
        RequestHandler<GetTimeOutIntervalRequest, GetTimeOutIntervalResponse>
    {
        #region Constants and Fields

        private const string AutomaticLogoutIntervalMinutes = "AutomaticLogoutIntervalMinutes";
        private const string WarningLogoutIntervalSeconds = "WarningLogoutIntervalSeconds";
        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTimeOutIntervalRequestHandler"/> class.
        /// </summary>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        public GetTimeOutIntervalRequestHandler ( IConfigurationPropertiesProvider configurationPropertiesProvider )
        {
            _configurationPropertiesProvider = configurationPropertiesProvider;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the logout timer configuration values retrieval request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetTimeOutIntervalRequest request )
        {
            var automaticLogoutIntervalMinutesResult = _configurationPropertiesProvider.GetProperty ( AutomaticLogoutIntervalMinutes );
            int automaticTimeOutIntervalMinutes;

            if ( !int.TryParse ( automaticLogoutIntervalMinutesResult, out automaticTimeOutIntervalMinutes ) )
            {
                throw new ArgumentException ( "A valid automatic logout time interval could not be obtained from configuration." );
            }

            var warningLogoutIntervalSecondsResult = _configurationPropertiesProvider.GetProperty ( WarningLogoutIntervalSeconds );
            int warningTimeOutIntervalSeconds;

            if ( !int.TryParse ( warningLogoutIntervalSecondsResult, out warningTimeOutIntervalSeconds ) )
            {
                throw new ArgumentException ( "A valid warning logout time interval could not be obtained from configuration." );
            }

            var response = CreateTypedResponse ();
            response.AutomaticTimeOutIntervalMinutes = automaticTimeOutIntervalMinutes;
            response.WarningTimeOutIntervalSeconds = warningTimeOutIntervalSeconds;

            return response;
        }

        #endregion
    }
}
