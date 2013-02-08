using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap.Tests
{
    [TestClass]
    public class MedicalProcedureTranslatorTests
    {
        [TestMethod]
        public void Translate_ProcedureIsNull_ReturnsNullMedicalProcedure()
        {
            // Setup
            var fixture = new Fixture ().Customize ( new AutoMoqCustomization () );

            var sut = fixture.CreateAnonymous<MedicalProcedureTranslator> ();

            // Exercise
            var medicalProcedure = sut.Translate ( null );

            // Verify
            Assert.IsNull(medicalProcedure);
        }

        [TestMethod]
        public void Translate_ProcedureIsNotNull_ReturnsMedicalProcedureCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var procedure = new Mock<Procedure> ();
            var procedureCode = fixture.CreateAnonymous<CodedConcept> ();
            var firstModifierCode = fixture.CreateAnonymous<CodedConcept> ();
            var secondModifierCode = fixture.CreateAnonymous<CodedConcept>();
            var thirdModifierCode = fixture.CreateAnonymous<CodedConcept>();
            var fourthModifierCode = fixture.CreateAnonymous<CodedConcept>();
            procedure.SetupGet ( p => p.ProcedureCode ).Returns ( procedureCode );
            procedure.SetupGet ( p => p.FirstModifierCode ).Returns ( firstModifierCode );
            procedure.SetupGet ( p => p.SecondModifierCode ).Returns ( secondModifierCode );
            procedure.SetupGet ( p => p.ThirdModifierCode ).Returns ( thirdModifierCode );
            procedure.SetupGet ( p => p.FourthModifierCode ).Returns ( fourthModifierCode );

            var sut = fixture.CreateAnonymous<MedicalProcedureTranslator>();

            // Exercise
            var medicalProcedure = sut.Translate(procedure.Object);

            // Verify
            Assert.AreEqual( procedureCode, medicalProcedure.ProcedureCode );
            Assert.AreEqual(firstModifierCode, medicalProcedure.FirstModifierCode);
            Assert.AreEqual(secondModifierCode, medicalProcedure.SecondModifierCode);
            Assert.AreEqual(thirdModifierCode, medicalProcedure.ThirdModifierCode);
            Assert.AreEqual(fourthModifierCode, medicalProcedure.FourthModifierCode);
        }
    }
}
