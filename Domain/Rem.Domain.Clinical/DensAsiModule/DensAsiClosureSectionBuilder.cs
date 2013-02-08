namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiClosureSectionBuilder provides a fluent interface for creating a DesAsiClosure section.
    /// </summary>
    public class DensAsiClosureSectionBuilder
    {
        private DensAsiTreatmentModality _mostAppropriateDensAsiTreatmentModality;
        private string _mostAppropriateDensAsiTreatmentModalityNote;
        private DensAsiNonResponseType<DensAsiIncompleteInterviewReason> _densAsiIncompleteInterviewReason;
        private string _densAsiIncompleteInterviewReasonNote;

        /// <summary>
        /// Assigns the most appropriate DensAsi treatment modality.
        /// </summary>
        /// <param name="mostAppropriateDensAsiTreatmentModality">The most appropriate dens asi treatment modality.</param>
        /// <returns>A DensAsiClosureSectionBuilder.</returns>
        public DensAsiClosureSectionBuilder WithMostAppropriateDensAsiTreatmentModality(DensAsiTreatmentModality mostAppropriateDensAsiTreatmentModality)
        {
            _mostAppropriateDensAsiTreatmentModality = mostAppropriateDensAsiTreatmentModality;
            return this;
        }

        /// <summary>
        /// Assigns the most appropriate DensAsi treatment modality note.
        /// </summary>
        /// <param name="mostAppropriateDensAsiTreatmentModalityNote">The most appropriate dens asi treatment modality note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiClosureSectionBuilder">A DensAsiClosureSectionBuilder</see></returns>
        public DensAsiClosureSectionBuilder WithMostAppropriateDensAsiTreatmentModalityNote(string mostAppropriateDensAsiTreatmentModalityNote)
        {
            _mostAppropriateDensAsiTreatmentModalityNote = mostAppropriateDensAsiTreatmentModalityNote;
            return this;
        }

        /// <summary>
        /// Assigns the DensAsi inncomplete interview reason.
        /// </summary>
        /// <param name="densAsiIncompleteInterviewReason">The dens asi incomplete interview reason.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiClosureSectionBuilder">A DensAsiClosureSectionBuilder</see></returns>
        public DensAsiClosureSectionBuilder WithDensAsiIncompleteInterviewReason(DensAsiNonResponseType<DensAsiIncompleteInterviewReason> densAsiIncompleteInterviewReason)
        {
            _densAsiIncompleteInterviewReason = densAsiIncompleteInterviewReason;
            return this;
        }

        /// <summary>
        /// Assigns the DensAsi incomplete interview reason note.
        /// </summary>
        /// <param name="densAsiIncompleteInterviewReasonNote">The dens asi incomplete interview reason.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiClosureSectionBuilder">A DensAsiClosureSectionBuilder</see></returns>
        public DensAsiClosureSectionBuilder WithDensAsiIncompleteInterviewReasonNote(string densAsiIncompleteInterviewReasonNote)
        {
            _densAsiIncompleteInterviewReasonNote = densAsiIncompleteInterviewReasonNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A DensAsiClosureSection.</returns>
        public DensAsiClosureSection Build()
        {
            return new DensAsiClosureSection(
                _mostAppropriateDensAsiTreatmentModality,
                _mostAppropriateDensAsiTreatmentModalityNote,
                _densAsiIncompleteInterviewReason,
                _densAsiIncompleteInterviewReasonNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiClosureSectionBuilder"/> to <see cref="DensAsiClosureSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiClosureSection(DensAsiClosureSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
