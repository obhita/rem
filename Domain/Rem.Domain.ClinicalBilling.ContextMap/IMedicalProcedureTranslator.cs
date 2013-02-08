using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// This interface translates procedure from clinical side to billing side medical procedure.
    /// </summary>
    public interface IMedicalProcedureTranslator
    {
        /// <summary>
        /// Translates the specified procedure.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <returns>A MedicalProcedure instance.</returns>
        MedicalProcedure Translate ( Procedure procedure );
    }
}
