using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHapi.Base.Parser;
using Rem.Infrastructure.Tests.Domain;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.PatientModule;

namespace Rem.Ria.PatientModule.Web.Tests.PatientDashboard
{
    [TestClass]
    public class SyndromicSurveillanceTests : SafeHarborAgencyFixtureBase
    {
        protected override void OnSetup()
        {
            base.OnSetup();

            StructureMapContainer.Configure(
                x => x.Scan(scanner =>
                {
                    scanner.AssembliesFromApplicationBaseDirectory(p => (p.FullName == null) ? false : p.FullName.Contains("HL7Generator.Infrastructure"));
                    scanner.LookForRegistries();
                }));
        }

        [TestMethod]
        public void BuildSyndromicSurveillanceMessage_GivenProblemAndVisitKeys_Succeeds ()
        {
            var keyValues = new Dictionary<string, long>
                                {
                                    { HttpHandlerQueryStrings.ProblemKey, AlbertSmithPatientClinicalCaseOneProblemThree.Key },
                                };

            var syndromicSurveillanceFactory = new Hl7SyndromicSurveillanceFactory ( SessionProvider );
            var message = syndromicSurveillanceFactory.GetHl7Message ( keyValues );
            var messageObject = new PipeParser ().Parse ( message );
            var type = (NHapi.Model.V251.Message.ADT_A01)messageObject;

            Assert.AreEqual( 1, type.DG1RepetitionsUsed);
        }
    }
}