using Pillar.Common.Utility;
using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// This class translates procedure from clinical side to billing side medical procedure.
    /// </summary>
    public class MedicalProcedureTranslator : IMedicalProcedureTranslator
    {
        /// <summary>
        /// Translates the specified procedure.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <returns>
        /// A MedicalProcedure instance.
        /// </returns>
        public MedicalProcedure Translate(Procedure procedure)
        {
            if (procedure == null)
            {
                return null;
            }

            var medicalProcedure = new MedicalProcedure (
                procedure.ProcedureCode,
                procedure.FirstModifierCode,
                procedure.SecondModifierCode,
                procedure.ThirdModifierCode,
                procedure.FourthModifierCode );

            return medicalProcedure;
        }
    }
}