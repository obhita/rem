using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiClosureSection contains interview closing questions from the closing question section of the DensAsi. 
    /// </summary>
    [Component]
    public class DensAsiClosureSection : DensAsiInterviewSectionBase
    {
        #region Constants and Fields

        private readonly DensAsiNonResponseType<DensAsiIncompleteInterviewReason> _densAsiIncompleteInterviewReason;
        private readonly string _densAsiIncompleteInterviewReasonNote;
        private readonly DensAsiTreatmentModality _mostAppropriateDensAsiTreatmentModality;
        private readonly string _mostAppropriateDensAsiTreatmentModalityNote;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiClosureSection"/> class.
        /// </summary>
        /// <param name="mostAppropriateDensAsiTreatmentModality">The most appropriate treatment modality.</param>
        /// <param name="mostAppropriateDensAsiTreatmentModalityNote">The most appropriate treatment modality note.</param>
        /// <param name="densAsiIncompleteInterviewReason">The incomplete interview reason.</param>
        /// <param name="densAsiIncompleteInterviewReasonNote">The incomplete interview reason note.</param>
        public DensAsiClosureSection(
            DensAsiTreatmentModality mostAppropriateDensAsiTreatmentModality,
            string mostAppropriateDensAsiTreatmentModalityNote,
            DensAsiNonResponseType<DensAsiIncompleteInterviewReason> densAsiIncompleteInterviewReason,
            string densAsiIncompleteInterviewReasonNote)
        {
            if (densAsiIncompleteInterviewReason.DensAsiNonResponse != null
                &&
                !this.GetPossibleDensAsiNonResponseWellKnownNames(() => this.DensAsiIncompleteInterviewReason).Contains(
                    densAsiIncompleteInterviewReason.DensAsiNonResponse.WellKnownName))
            {
                throw new ArgumentException(
                    "DensAsiIncompleteInterviewReason DensAsiNonResponse value '" + densAsiIncompleteInterviewReason.DensAsiNonResponse.WellKnownName
                    + "' is not valid.");
            }
            this._mostAppropriateDensAsiTreatmentModality = mostAppropriateDensAsiTreatmentModality;
            this._mostAppropriateDensAsiTreatmentModalityNote = mostAppropriateDensAsiTreatmentModalityNote;
            this._densAsiIncompleteInterviewReason = densAsiIncompleteInterviewReason;
            this._densAsiIncompleteInterviewReasonNote = densAsiIncompleteInterviewReasonNote;
        }

        private DensAsiClosureSection()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiIncompleteInterviewReason">DensAsiIncompleteInterviewReason</see> 
        /// denoting the reason that the DensAsi interview was not completed.
        /// Question Number: G12
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiIncompleteInterviewReason> DensAsiIncompleteInterviewReason
        {
            get
            {
                return this._densAsiIncompleteInterviewReason;
            }

            private set
            {
            }
        }

        /// <summary>
        /// Gets the incomplete interview reason note.
        /// Question Number: G12.
        /// </summary>
        public virtual string DensAsiIncompleteInterviewReasonNote
        {
            get
            {
                return this._densAsiIncompleteInterviewReasonNote;
            }

            private set
            {
            }
        }

        /// <summary>
        /// Gets the most appropriate treatment modality.
        /// Question Number: G50.
        /// </summary>
        public virtual DensAsiTreatmentModality MostAppropriateDensAsiTreatmentModality
        {
            get
            {
                return this._mostAppropriateDensAsiTreatmentModality;
            }

            private set
            {
            }
        }

        /// <summary>
        /// Gets the most appropriate treatment modality note.
        /// Question Number: G50.
        /// </summary>
        public virtual string MostAppropriateDensAsiTreatmentModalityNote
        {
            get
            {
                return this._mostAppropriateDensAsiTreatmentModalityNote;
            }

            private set
            {
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public override Dictionary<string, IEnumerable<string>> GetFiltersDictionary()
        {
            return new Dictionary<string, IEnumerable<string>>
                {
                    {
                        PropertyUtil.ExtractPropertyName(() => this.DensAsiIncompleteInterviewReason),
                        this.GetPossibleDensAsiNonResponseWellKnownNames(() => this.DensAsiIncompleteInterviewReason)
                        }
                };
        }

        /// <summary>
        /// Gets the possible DensAsi non response well known names for this interview
        /// section.
        /// </summary>
        /// <typeparam name="TProperty">The property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see> list of WellKNownNames.
        /// </returns>
        public override IEnumerable<string> GetPossibleDensAsiNonResponseWellKnownNames<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            IEnumerable<string> possibleDensAsiNonResponseWellKnownNames;
            string propertyName = PropertyUtil.ExtractPropertyName(propertyExpression);

            if (propertyName == PropertyUtil.ExtractPropertyName(() => this.DensAsiIncompleteInterviewReason))
            {
                possibleDensAsiNonResponseWellKnownNames = new List<string> { WellKnownNames.DensAsiModule.DensAsiNonResponse.NotApplicable };
            }
            else
            {
                possibleDensAsiNonResponseWellKnownNames = base.GetPossibleDensAsiNonResponseWellKnownNames(propertyExpression);
            }

            return possibleDensAsiNonResponseWellKnownNames;
        }

        #endregion
    }
}