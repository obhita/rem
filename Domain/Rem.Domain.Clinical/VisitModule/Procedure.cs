using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pillar.Common.Utility;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// This class defines the procedure for visit/activity under coding context.
    /// </summary>
    public class Procedure : CodingContextAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Procedure"/> class.
        /// </summary>
        protected Procedure()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Procedure"/> class.
        /// </summary>
        /// <param name="codingContext">The coding context.</param>
        /// <param name="procedureType">Type of the procedure.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="procedureCode">The procedure code.</param>
        /// <param name="unitCount">The unit count.</param>
        protected internal Procedure(CodingContext codingContext, ProcedureType procedureType, Activity activity, CodedConcept procedureCode, UnitCount unitCount)
            : base(codingContext)
        {
            if (procedureType == ProcedureType.Activity)
            {
                Check.IsNotNull(activity, "Activity is required for activity procedure type.");
            }

            Check.IsNotNull(procedureCode, "Procedure code is required.");

            CodingContext = codingContext;
            ProcedureType = procedureType;
            Activity = activity;
            ProcedureCode = procedureCode;
            BillingUnitCount = unitCount;
        }

        /// <summary>
        /// Gets the type of the procedure.
        /// </summary>
        /// <value>
        /// The type of the procedure.
        /// </value>
        public virtual ProcedureType ProcedureType { get; private set; }

        /// <summary>
        /// Gets the activity.
        /// </summary>
        public virtual Activity Activity { get; private set; }

        /// <summary>
        /// Gets the procedure code.
        /// </summary>
        public virtual CodedConcept ProcedureCode { get; private set; }

        /// <summary>
        /// Gets the unit count.
        /// </summary>
        public virtual UnitCount BillingUnitCount { get; private set; }

        /// <summary>
        /// Gets the first modifier code.
        /// </summary>
        public virtual CodedConcept FirstModifierCode { get; private set; }

        /// <summary>
        /// Gets the second modifier code.
        /// </summary>
        public virtual CodedConcept SecondModifierCode { get; private set; }

        /// <summary>
        /// Gets the third modifier code.
        /// </summary>
        public virtual CodedConcept ThirdModifierCode { get; private set; }

        /// <summary>
        /// Gets the four modifier code.
        /// </summary>
        public virtual CodedConcept FourthModifierCode { get; private set; }
    }
}
