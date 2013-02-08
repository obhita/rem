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

using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using Pillar.Common.Configuration;
using Pillar.Common.Utility;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.WebService;
using Thinktecture.IdentityServer.WebService;

namespace Rem.Ria.AgencyModule.Web.StaffEditor
{
    /// <summary>
    /// Class for handling create system account request.
    /// </summary>
    public class CreateSystemAccountRequestHandler : NHibernateSessionRequestHandler<CreateSystemAccountRequest, CreateSystemAccountResponse>
    {
        #region Constants and Fields

        private const string IdentityProviderNameConfigName = "IdentityProviderNameConfigName";
        private const string IdentityProviderUriConfigName = "IdentityProviderUriConfigName";
        private const string IdentityServerEndpointConfigName = "IdentityServerEndpointConfigurationName";
        private readonly IConfigurationPropertiesProvider _configProvider;
        private readonly ISystemAccountFactory _systemAcccountFactory;

        private string _identityProviderName;
        private string _identityProviderUri;
        private string _membershipServiceEndpointAddress;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSystemAccountRequestHandler"/> class.
        /// </summary>
        /// <param name="configProvider">The config provider.</param>
        /// <param name="systemAcccountFactory">The system acccount factory.</param>
        public CreateSystemAccountRequestHandler ( IConfigurationPropertiesProvider configProvider, ISystemAccountFactory systemAcccountFactory )
        {
            _configProvider = configProvider;
            _systemAcccountFactory = systemAcccountFactory;

            LoadConfiguration ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( CreateSystemAccountRequest request )
        {
            // Prepopulate the response with the information from the request.
            var response = new CreateSystemAccountResponse { SystemAccount = request.SystemAccount };

            var errors = new List<DataErrorInfo> ();

            var userExits = GetUserByUsername ( request.SystemAccount.Username );

            if ( userExits.Ok )
            {
                // Notify the UI the fact that the user already exists in Identity Server
                AddErrorToCollection (
                    "The username provided already exists in the Identity Provider.",
                    PropertyUtil.ExtractPropertyName ( () => request.SystemAccount.Username ),
                    errors );

                errors.ForEach ( response.SystemAccount.AddDataErrorInfo );

                return response;
            }
            else if ( userExits.ErrorCode == "1003" )
            {
                // If user not found, then it is ok to create a new user
                var result = CreateUser ( request.SystemAccount.Username, request.SystemAccount.EmailAddress );

                if ( result.Ok )
                {
                    var systemAccount = CreateSystemAccount (
                        result.User.NameIdentifier,
                        request.SystemAccount.DisplayName,
                        result.User.EmailAddress,
                        _identityProviderName,
                        _identityProviderUri );

                    var staff = Session.QueryOver<Staff> ().Where ( s => s.Key == request.StaffKey ).SingleOrDefault ();

                    staff.ReviseSystemAcount ( systemAccount );

                    var dto = Mapper.Map<SystemAccount, SystemAccountDto> ( systemAccount );

                    // Make sure the SystemAccount Key is being sent
                    dto.Key = systemAccount.Key;

                    response.SystemAccount = dto;
                }
                else
                {
                    AddErrorToCollection ( result.ErrorMessage, string.Empty, errors );
                    errors.ForEach ( response.SystemAccount.AddDataErrorInfo );
                }
            }
            Session.Flush ();

            return response;
        }

        #endregion

        #region Methods

        private static void AddErrorToCollection ( string message, string propertyName, List<DataErrorInfo> errors )
        {
            errors.Add ( new DataErrorInfo ( message, ErrorLevel.Error, new[] { propertyName } ) );
        }

        private SystemAccount CreateSystemAccount (
            string nameIdentifier, string displayName, string emailAddress, string identityProviderName, string identityProviderUri )
        {
            var newSystemAccount = _systemAcccountFactory.CreateSystemAccount (
                nameIdentifier, displayName, new EmailAddress ( emailAddress ), identityProviderName, identityProviderUri );

            return newSystemAccount;
        }

        private UserResult CreateUser ( string username, string email )
        {
            UserResult user;

            using ( var proxy = new DynamicWebServiceProxy<IMembershipService> ( _membershipServiceEndpointAddress ) )
            {
                // TODO: Do not send a password to allow for Random password generation at the Identity Provider side
                // The hardcoding of the password has been done to cicumvent the non-availability of an SMTP server
                user = proxy.Client.CreateUser ( username, email, "P@$$w0rd", string.Empty, string.Empty );
            }

            return user;
        }

        private UserResult GetUserByUsername ( string username )
        {
            UserResult user;

            //TODO: Try/Catch
            using ( var proxy = new DynamicWebServiceProxy<IMembershipService> ( _membershipServiceEndpointAddress ) )
            {
                user = proxy.Client.GetUserByUsername ( username );
            }

            return user;
        }

        private void LoadConfiguration ()
        {
            var cannedErrorMessage = "Unable to find {0} in the configuration Store";

            var configName = _configProvider.GetProperty ( IdentityServerEndpointConfigName );
            var identityProviderName = _configProvider.GetProperty ( IdentityProviderNameConfigName );
            var identityProviderUri = _configProvider.GetProperty ( IdentityProviderUriConfigName );

            _membershipServiceEndpointAddress = Check.IsNotNullOrWhitespaceAndAssign (
                configName, string.Format ( cannedErrorMessage, IdentityServerEndpointConfigName ) );

            _identityProviderName = Check.IsNotNullOrWhitespaceAndAssign (
                identityProviderName, string.Format ( cannedErrorMessage, IdentityProviderNameConfigName ) );

            _identityProviderUri = Check.IsNotNullOrWhitespaceAndAssign (
                identityProviderUri, string.Format ( cannedErrorMessage, IdentityProviderUriConfigName ) );
        }

        #endregion
    }
}
