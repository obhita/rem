using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHapi.Base.Parser;
using NHapi.Model.V251.Group;
using NHapi.Model.V251.Message;
using Rem.Infrastructure.Tests.Domain;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.PatientModule;

namespace Rem.Ria.PatientModule.Web.Tests.PatientDashboard
{
    [TestClass]
    public class Hl7ImmunizationFactoryTests : SafeHarborAgencyFixtureBase
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
        public void BuildHl7ImmunizationMessage_GivenActivity_Succeeds ()
        {
            var keyValues = new Dictionary<string, long>
                                {
                                    { HttpHandlerQueryStrings.ActivityKey, AlbertSmithPatientClinicalCaseOneVisitFourImmunizationTwo.Key },
                                };

            var hl7ImmunizationFactory = new Hl7ImmunizationFactory ( SessionProvider );
            string message = hl7ImmunizationFactory.GetHl7Message ( keyValues );
            var messageObject = new PipeParser().Parse(message);
            var vxuV04 = ( VXU_V04 ) messageObject;

            VXU_V04_ORDER order = vxuV04.GetORDER ();
            Assert.IsNotNull ( order );
        }
    }
}