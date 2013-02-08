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

using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.AgencyModule.Web.AgencyEditor
{
    /// <summary>
    /// Class for handling save agency faqs request.
    /// </summary>
    public class SaveAgencyFaqsRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<AgencyFaqsDto>, DtoResponse<AgencyFaqsDto>, AgencyFaqsDto, Agency>
    {
        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="agencyFaqsDto">The agency faqs dto.</param>
        /// <param name="agency">The agency.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( AgencyFaqsDto agencyFaqsDto, Agency agency )
        {
            var processSucceeded =
                new AggregateNodeCollectionMapper<AgencyFaqDto, Agency, AgencyFrequentlyAskedQuestion> (
                    agencyFaqsDto.AgencyFaqs, agency, agency.AgencyFrequentlyAskedQuestions ).MapRemovedItem ( RemoveAgencyAddress ).MapAddedItem (
                        AddAgencyAddress ).MapChangedItem ( ChangeAgencyAddress ).Map ();

            return processSucceeded;
        }

        private static void AddAgencyAddress ( AgencyFaqDto agencyFaqDto, Agency agency )
        {
            agency.AddFrequentlyAskedQuestion ( new AgencyFrequentlyAskedQuestion ( agencyFaqDto.QuestionNote, agencyFaqDto.AnswerNote ) );
        }

        private static void RemoveAgencyAddress (
            AgencyFaqDto agencyFaqDto, Agency agency, AgencyFrequentlyAskedQuestion agencyFrequentlyAskedQuestion )
        {
            agency.RemoveFrequentlyAskedQuestion ( agencyFrequentlyAskedQuestion );
        }

        private void ChangeAgencyAddress ( AgencyFaqDto agencyFaqDto, Agency agency, AgencyFrequentlyAskedQuestion agencyFrequentlyAskedQuestion )
        {
            RemoveAgencyAddress ( agencyFaqDto, agency, agencyFrequentlyAskedQuestion );
            AddAgencyAddress ( agencyFaqDto, agency );
        }

        #endregion
    }
}
