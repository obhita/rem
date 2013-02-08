using System;
using Agatha.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Domain.Repository;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.PatientSearch;

namespace Rem.Ria.PatientModule.Web.Tests.PatientDashboard
{
    [TestClass]
    public class CreateNewPatientHandlerTests : PatientModuleTestFixture
    {
        protected override void OnSetup ()
        {
            base.OnSetup ();
            StructureMapContainer.Configure (
                s => s.For<IPatientFactory> ().Use ( new PatientFactory ( new PatientRepository ( SessionProvider ), new PatientUniqueIdentifierGenerator() ) ) );
        }

        [TestMethod]
        public void Handle_GivenValidPatientInfo_ReturnsPatientDtoThatWasSavedInDB ()
        {
            var patientRepository = new PatientRepository ( SessionProvider );
            var patientFactory = new PatientFactory(patientRepository, new PatientUniqueIdentifierGenerator());
            var dtoToDomainMappingHelper = new DtoToDomainMappingHelper ( new LookupValueRepository ( SessionProvider ) );
            var handler = new CreateNewPatientRequestHandler(dtoToDomainMappingHelper, patientFactory)
                              {
                                  SessionProvider = SessionProvider
                              };

            // When you do Agatha Request Handler testing, always declare the request as the base class type Agatha.Common.Request
            Request request = new CreateNewPatientRequest
                              {
                                  AgencyKey = SafeHarborAgency.Key,
                                  BirthDate = new DateTime ( 1983, 8, 29 ),
                                  FirstName = "John",
                                  Gender = new LookupValueDto
                                               {
                                                   WellKnownName = MaleGender.WellKnownName,
                                                   Key = MaleGender.Key,
                                                   Name = MaleGender.Name
                                               },
                                  LastName = "Smith",
                                  MiddleName = "Middle"
                              };

            var response = handler.Handle ( request ) as CreateNewPatientResponse;

            Assert.IsTrue ( response.PatientDto.Key > 0 );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void Handle_GivenInValidPatientInfo_ThrowsException ()
        {
            var patientRepository = new PatientRepository ( SessionProvider );
            var patientFactory = new PatientFactory(patientRepository, new PatientUniqueIdentifierGenerator());
            var dtoToDomainMappingHelper = new DtoToDomainMappingHelper ( new LookupValueRepository ( SessionProvider ) );
            var handler = new CreateNewPatientRequestHandler ( dtoToDomainMappingHelper, patientFactory )
                              {
                                  SessionProvider = SessionProvider
                              };

            // When you do Agatha Request Handler testing, always declare the request as the base class type Agatha.Common.Request
            Request request = new CreateNewPatientRequest
                                  {
                                      AgencyKey = SafeHarborAgency.Key
                                  };
            handler.Handle ( request );
        }
    }
}