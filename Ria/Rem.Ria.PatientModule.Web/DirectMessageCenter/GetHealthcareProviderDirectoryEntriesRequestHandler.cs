using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using Agatha.ServiceLayer;
using NIEMAdapter;
using Rem.Ria.Infrastructure.Web.WebService;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// GetHealthcareProviderDirectoryEntriesRequestHandler class
    /// </summary>
    public class GetHealthcareProviderDirectoryEntriesRequestHandler :
        RequestHandler<GetHealthcareProviderDirectoryEntriesRequest, GetHealthcareProviderDirectoryEntriesResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The Response</returns>
        public override Response Handle ( GetHealthcareProviderDirectoryEntriesRequest request )
        {
            GetHealthcareProviderDirectoryEntriesResponse response = CreateTypedResponse ();
            try
            {
                HealthcareProviderDirectoryQueryResponse niemResponse = HandleCore ( request );

               var remResponse = niemResponse
                    .HealthcareProvider
                    .Where(p => p.IsHealthcareProfessional) // Filter Non Healthcare Provider entries, such as Organizations // From the response retrieve the list of providers
                    .Select( MapToREMDto ) // Map them to a REM Dto Entity
                    .ToList ();

                response.Providers = remResponse;
            }
            catch ( Exception exception )
            {
                // The Call to the Web Service failed
                response.Exception = new ExceptionInfo ( exception );
            }

            return response;
        }

        #endregion

        #region Methods

        private static HealthcareProviderDirectoryQueryRequest BuildNiemRequest ()
        {
            // One of the caveats of using generated POCO objects based on XSD Schemas. 
            // TODO:// Create an Anti-Corrosion Layer around the NIEM 
            var niemRequest = new HealthcareProviderDirectoryQueryRequest ();
            niemRequest.PersonName = new PersonNameType ();
            niemRequest.PersonName.PersonSurName = new List<string> ();
            niemRequest.PersonName.PersonSurName.Add ( "Freud" );
            return niemRequest;
        }

        private HealthcareProviderDirectoryQueryResponse HandleCore ( GetHealthcareProviderDirectoryEntriesRequest request )
        {
            using ( var proxy = new DynamicWebServiceProxy<IHealthQueryService> () )
            {
                HealthcareProviderDirectoryQueryRequest niemRequest = BuildNiemRequest ();

                HealthcareProviderDirectoryQueryResponse niemResponse = proxy.Client.Query ( niemRequest );
                return niemResponse;
            }
        }

        private HealthProviderEntryDto MapToREMDto ( HealthcareProvider niemEntry )
        {
            var result = new HealthProviderEntryDto
                {
                    FirstName = niemEntry.PersonGivenName,
                    LastName = niemEntry.PersonSurName,
                    IsHcProfessional = niemEntry.IsHealthcareProfessional,
                    OrganizationName = niemEntry.OrganizationName,
                    Specialization = (niemEntry.PersonRoleCategoryText != null && niemEntry.PersonRoleCategoryText.Count > 0) ? niemEntry.PersonRoleCategoryText[0] : null,
                    Mail = niemEntry.ElectronicAddressText,
                    TelephoneNumber = (niemEntry.TelephoneNumberFullID != null && niemEntry.TelephoneNumberFullID.Count > 0) ? niemEntry.TelephoneNumberFullID[0] : null
                };

            return result;
        }

        #endregion
    }
}
