using Agatha.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientReminder;

namespace Rem.Ria.PatientModule.Web.Tests.PatientDashboard
{
    [TestClass]
    public class PatientReminderSearchRequestHandlerTests : PatientModuleTestFixture
    {
        [TestMethod]
        public void Handle_PatientReminderSearchRequest_Runs()
        {
            var handler = new PatientReminderSearchRequestHandler { SessionProvider = SessionProvider };

            Request request = new PatientReminderSearchRequest
                                  {
                                      PatientReminderCriteriaDto =
                                          new PatientReminderCriteriaDto
                                              {
                                                  Problem =
                                                      new ProblemDto { ProblemCodeCodedConcept = new CodedConceptDto { CodedConceptCode = "250.02" } }
                                              }
                                  };

            handler.Handle ( request );
        }
    }
}

