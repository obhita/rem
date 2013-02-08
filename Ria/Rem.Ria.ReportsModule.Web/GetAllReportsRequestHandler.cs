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
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Agatha.Common;
using Pillar.Common.Configuration;
using Rem.Domain.Clinical.ReportsModule;
using Rem.Infrastructure.Configuration;
using Rem.Infrastructure.Service;

namespace Rem.Ria.ReportsModule.Web
{
    /// <summary>
    /// Class for handling get all reports request.
    /// </summary>
    public class GetAllReportsRequestHandler : NHibernateSessionRequestHandler<GetAllReportsRequest, GetAllReportsResponse>
    {
        #region Constants and Fields

        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;
        private readonly IReportRepository _reportRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllReportsRequestHandler"/> class.
        /// </summary>
        /// <param name="reportRepository">The report repository.</param>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        public GetAllReportsRequestHandler ( IReportRepository reportRepository, IConfigurationPropertiesProvider configurationPropertiesProvider )
        {
            _reportRepository = reportRepository;
            _configurationPropertiesProvider = configurationPropertiesProvider;

            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetAllReportsRequest request )
        {
            var response = new GetAllReportsResponse ();
            var reports = _reportRepository.GetAllReports ().ToList ();
            var catalogItems = GetAllReportsFromSsrs ();
            var reportDtos = new List<ReportDto> ();

            for ( var index = 0; index < reports.Count; index++ )
            {
                ////TODO: Check how this works when report has paramaters.
                var index1 = index; //To avoid Access to modified closure.
                if ( catalogItems.Where ( ci => ci.Name == reports[index1].ResourceName ).Count () > 0 )
                {
                    reportDtos.Add (
                        new ReportDto
                            {
                                Key = reports[index1].Key,
                                Name = reports[index].Name,
                                Description = reports[index].Description,
                                ResourceName = reports[index].ResourceName
                            } );
                }
                else
                {
                    ////Report entry exists in database, but not present in SSRS.
                    reports.Remove ( reports[index] );
                    index--;
                }
            }

            response.Reports = reportDtos;
            response.ReportsRootUri = _configurationPropertiesProvider.GetProperty(SettingKeyNames.ReportsRootUri);

            return response;
        }

        #endregion

        #region Methods

        private static bool ValidateRemoteCertificate (
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors policyErrors
            )
        {
            var ssrsServerSslCertificateHashCode = "19F9AA0DCE2CBC6AB9D14E103F99364B496D05AD";
            return certificate.GetCertHashString () == ssrsServerSslCertificateHashCode || policyErrors == SslPolicyErrors.None;
        }

        private IEnumerable<CatalogItem> GetAllReportsFromSsrs ()
        {
            using (
                var ssrsClient = new ReportingService2005SoapClient (
                    "ReportingService2005Soap11", _configurationPropertiesProvider.GetProperty(SettingKeyNames.ReportServiceUrl)))
            {
                IEnumerable<CatalogItem> reportsList = null;

                if ( ssrsClient != null )
                {
                    ssrsClient.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Delegation;

                    //ssrsClient.ClientCredentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

                    //TODO: After migration of app server to rem domain, remqaidentity and remdemoidentity can be added 
                    //in SSRS under 'Browser' Role and CredentialCache.DefaultNetworkCredentials can be used without 
                    //checking for environment.
                    ssrsClient.ClientCredentials.Windows.ClientCredential = TempClassDuringMigration.NetworkCredential;

                    CatalogItem[] catalogItems;

                    ssrsClient.ListChildren("/" + _configurationPropertiesProvider.GetProperty(SettingKeyNames.ReportsRootUri), true, out catalogItems);

                    reportsList = catalogItems.Where ( i => i.Type == ItemTypeEnum.Report );
                }
                return reportsList;
            }
        }

        #endregion
    }
}
