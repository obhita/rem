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
using System.Linq;
using Agatha.Common;
using AutoMapper;
using Pillar.Common.Configuration;
using Pillar.Common.Utility;
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
    /// The link system account request handler.
    /// </summary>
    public class LinkSystemAccountRequestHandler : NHibernateSessionRequestHandler<LinkSystemAccountRequest, LinkSystemAccountResponse>
    {
        #region Constants and Fields

        private const string IdentityProviderNameConfigName = "IdentityProviderNameConfigName";
        private const string IdentityProviderUriConfigName = "IdentityProviderUriConfigName";
        private const string IdentityServerEndpointConfigName = "IdentityServerEndpointConfigurationName";
        private readonly IConfigurationPropertiesProvider _configProvider;
        private string _identityProviderName;
        private string _identityProviderUri;
        private string _membershipServiceEndpointAddress;

        #endregion

        ////private readonly IStaffRepository _staffRepository;
        ////private readonly ISystemAccountFactory _systemAcccountFactory;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkSystemAccountRequestHandler"/> class.
        /// </summary>
        /// <param name="configProvider">The config provider.</param>
        public LinkSystemAccountRequestHandler ( IConfigurationPropertiesProvider configProvider )
        {
            _configProvider = configProvider;
            LoadConfiguration ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( LinkSystemAccountRequest request )
        {
            // Prepopulate the response with the information from the request.
            var response = new LinkSystemAccountResponse { SystemAccount = request.SystemAccount };

            var systemAccount = response.SystemAccount;

            var errors = new List<DataErrorInfo> ();

            // 1: Retrieve the System Account by email address
            var sa =
                Session.QueryOver<SystemAccount> ().Where ( s => s.EmailAddress.Address == request.SystemAccount.EmailAddress ).SingleOrDefault ();

            // 1.a: If no system account was found, produce the proper response and error message, and return
            if ( sa == null )
            {
                AddErrorToCollection (
                    "The email address provided is not associated to a system account.",
                    PropertyUtil.ExtractPropertyName ( () => systemAccount.EmailAddress ),
                    errors );
            }
            else
            {
                // 2: Retrieve the Identity by using the SystemAccount's NameIdentifier 
                var identity = GetUserByUniqueIdentifier ( sa.Identifier );

                // 2.1: If an error occurred, report it
                if ( !identity.Ok )
                {
                    AddErrorToCollection ( identity.ErrorMessage, PropertyUtil.ExtractPropertyName ( () => systemAccount.Username ), errors );

                    return response;
                }
                else if ( identity.User == null )
                {
                    //// 2.2: If no matching identity was found

                    AddErrorToCollection (
                        "Unable to find identity associated to the system account.",
                        PropertyUtil.ExtractPropertyName ( () => systemAccount.Username ),
                        errors );
                }
                else if ( identity.User.Username != systemAccount.Username )
                {
                    //// 2.3: Otherwise associate the the system Account with the staff record

                    AddErrorToCollection (
                        "The identity username does not match.", PropertyUtil.ExtractPropertyName ( () => systemAccount.Username ), errors );
                }
                else
                {
                    var staff = Session.QueryOver<Staff> ().Where ( s => s.Key == request.StaffKey ).SingleOrDefault ();
                    staff.ReviseSystemAcount ( sa );

                    var dto = Mapper.Map<SystemAccount, SystemAccountDto> ( sa );

                    response.SystemAccount = dto;
                }
            }

            errors.ForEach ( response.SystemAccount.AddDataErrorInfo );

            return response;
        }

        #endregion

        #region Methods

        private static void AddErrorToCollection ( string message, string propertyName, List<DataErrorInfo> errors )
        {
            errors.Add ( new DataErrorInfo ( message, ErrorLevel.Error, new[] { propertyName } ) );
        }

        private UserResult GetUserByUniqueIdentifier ( string nameIdentifier )
        {
            var result = new UserResult ();

            // TODO: Try/Catch
            using ( var proxy = new DynamicWebServiceProxy<IMembershipService> ( _membershipServiceEndpointAddress ) )
            {
                // TODO: Implement a GetByNameIdentifier
                var users = proxy.Client.GetAllUsers ( 0, 100 );

                var user = users.Users.ToList ().Where ( u => u.NameIdentifier == nameIdentifier ).SingleOrDefault ();

                result.Ok = users.Ok;
                result.User = user;
                return result;
            }
        }

        private UserResult GetUserByUsername ( string username )
        {
            UserResult user;

            // TODO: Try/Catch
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
