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

using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// NidaDrugQuestionnaireViewModelRuleCollection class.
    /// </summary>
    public class NidaDrugQuestionnaireViewModelRuleCollection : AbstractRuleCollection<NidaDrugQuestionnaireViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NidaDrugQuestionnaireViewModelRuleCollection"/> class.
        /// </summary>
        public NidaDrugQuestionnaireViewModelRuleCollection ()
        {
            var otherDrug1Error = new DataErrorInfo (
                "Other Drug Type name is required, when an Answer is provided for the same.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<NidaDrugQuestionnaireDto, object> ( dto => dto.OtherDrug1TypeName ) );
            NewRule ( () => OtherDrug1MustHaveDescription )
                .RunForProperty ( s => s.EditingDto.OtherDrug1TypeName )
                .RunForProperty ( s => s.EditingDto.OtherDrug1AnswerNumber )
                .When ( s => s.EditingDto.OtherDrug1AnswerNumber != null && string.IsNullOrWhiteSpace ( s.EditingDto.OtherDrug1TypeName ) )
                .ThenReportRuleViolation ( otherDrug1Error.Message )
                .Then ( s => s.EditingDto.TryAddDataErrorInfo ( otherDrug1Error ) )
                .ElseThen ( s => s.EditingDto.RemoveDataErrorInfo ( otherDrug1Error ) );

            var otherDrug2Error = new DataErrorInfo (
                "Other Drug Type name is required, when an Answer is provided for the same.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<NidaDrugQuestionnaireDto, object> ( dto => dto.OtherDrug2TypeName ) );
            NewRule ( () => OtherDrug2MustHaveDescription )
                .RunForProperty ( s => s.EditingDto.OtherDrug2TypeName )
                .RunForProperty ( s => s.EditingDto.OtherDrug2AnswerNumber )
                .When ( s => s.EditingDto.OtherDrug2AnswerNumber != null && string.IsNullOrWhiteSpace ( s.EditingDto.OtherDrug2TypeName ) )
                .ThenReportRuleViolation ( otherDrug2Error.Message )
                .Then ( s => s.EditingDto.TryAddDataErrorInfo ( otherDrug2Error ) )
                .ElseThen ( s => s.EditingDto.RemoveDataErrorInfo ( otherDrug2Error ) );

            var otherDrug3Error = new DataErrorInfo (
                "Other Drug Type name is required, when an Answer is provided for the same.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<NidaDrugQuestionnaireDto, object> ( dto => dto.OtherDrug3TypeName ) );
            NewRule ( () => OtherDrug3MustHaveDescription )
                .RunForProperty ( s => s.EditingDto.OtherDrug3TypeName )
                .RunForProperty ( s => s.EditingDto.OtherDrug3AnswerNumber )
                .When ( s => s.EditingDto.OtherDrug3AnswerNumber != null && string.IsNullOrWhiteSpace ( s.EditingDto.OtherDrug3TypeName ) )
                .ThenReportRuleViolation ( otherDrug3Error.Message )
                .Then ( s => s.EditingDto.TryAddDataErrorInfo ( otherDrug3Error ) )
                .ElseThen ( s => s.EditingDto.RemoveDataErrorInfo ( otherDrug3Error ) );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the other drug1 must have description.
        /// </summary>
        /// <value>The other drug1 must have description.</value>
        public IRule OtherDrug1MustHaveDescription { get; set; }

        /// <summary>
        /// Gets or sets the other drug2 must have description.
        /// </summary>
        /// <value>The other drug2 must have description.</value>
        public IRule OtherDrug2MustHaveDescription { get; set; }

        /// <summary>
        /// Gets or sets the other drug3 must have description.
        /// </summary>
        /// <value>The other drug3 must have description.</value>
        public IRule OtherDrug3MustHaveDescription { get; set; }

        #endregion
    }
}
