#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Data transfer object for DensAsiClosure class.
    /// </summary>
    public class DensAsiClosureDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private DensAsiNonResponseTypeDto<LookupValueDto> _densAsiIncompleteInterviewReason;
        private string _densAsiIncompleteInterviewReasonNote;
        private LookupValueDto _mostAppropriateDensAsiTreatmentModality;
        private string _mostAppropriateDensAsiTreatmentModalityNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: G12
        /// </summary>
        /// <value>The dens asi incomplete interview reason.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> DensAsiIncompleteInterviewReason
        {
            get { return _densAsiIncompleteInterviewReason; }
            set { ApplyPropertyChange ( ref _densAsiIncompleteInterviewReason, () => DensAsiIncompleteInterviewReason, value ); }
        }

        /// <summary>
        /// Question Number: G12
        /// </summary>
        /// <value>The dens asi incomplete interview reason note.</value>
        public string DensAsiIncompleteInterviewReasonNote
        {
            get { return _densAsiIncompleteInterviewReasonNote; }
            set { ApplyPropertyChange ( ref _densAsiIncompleteInterviewReasonNote, () => DensAsiIncompleteInterviewReasonNote, value ); }
        }

        /// <summary>
        /// Question Number: G50
        /// </summary>
        /// <value>The most appropriate dens asi treatment modality.</value>
        public LookupValueDto MostAppropriateDensAsiTreatmentModality
        {
            get { return _mostAppropriateDensAsiTreatmentModality; }
            set { ApplyPropertyChange ( ref _mostAppropriateDensAsiTreatmentModality, () => MostAppropriateDensAsiTreatmentModality, value ); }
        }

        /// <summary>
        /// Question Number: G50
        /// </summary>
        /// <value>The most appropriate dens asi treatment modality note.</value>
        public string MostAppropriateDensAsiTreatmentModalityNote
        {
            get { return _mostAppropriateDensAsiTreatmentModalityNote; }
            set { ApplyPropertyChange ( ref _mostAppropriateDensAsiTreatmentModalityNote, () => MostAppropriateDensAsiTreatmentModalityNote, value ); }
        }

        #endregion
    }
}
