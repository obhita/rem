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

using Rem.Domain.Clinical.SbirtModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save PHQ9 request.
    /// </summary>
    public class SavePhq9RequestHandler : SaveDtoRequestHandlerBase<SaveDtoRequest<Phq9Dto>, DtoResponse<Phq9Dto>, Phq9Dto>
    {
        #region Methods

        /// <summary>
        /// Processes the specified PHQ9 dto.
        /// </summary>
        /// <param name="phq9Dto">The PHQ9 dto.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool Process ( Phq9Dto phq9Dto )
        {
            var phq9 = Session.Get<Phq9> ( phq9Dto.Key );

            phq9.ReviseAndCalculate (
                phq9Dto.LittleInterestInDoingThingsAnswerNumber,
                phq9Dto.FeelingDownAnswerNumber,
                phq9Dto.TroubleSleepingAnswerNumber,
                phq9Dto.FeelingTiredAnswerNumber,
                phq9Dto.PoorAppetiteAnswerNumber,
                phq9Dto.FeelingBadAboutSelfAnswerNumber,
                phq9Dto.TroubleConcentratingAnswerNumber,
                phq9Dto.ActingSluggishOrFidgityAnswerNumber,
                phq9Dto.ThoughtsOfHurtingSelfAnswerNumber,
                phq9Dto.HaveTheseProblemsAffectedYouAnswerNumber );

            return true;
        }

        #endregion
    }
}
