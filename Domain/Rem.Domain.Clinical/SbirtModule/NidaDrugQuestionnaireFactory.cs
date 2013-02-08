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
using Pillar.Domain.Event;
using Rem.Domain.Clinical.SbirtModule.Event;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// The NidaDrugQuestionnaireFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.SbirtModule.NidaDrugQuestionnaire">NidaDrugQuestionnaire</see>.
    /// </summary>
    public class NidaDrugQuestionnaireFactory : INidaDrugQuestionnaireFactory
    {
        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly INidaDrugQuestionnaireRepository _nidaDrugQuestionnaireRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NidaDrugQuestionnaireFactory"/> class.
        /// </summary>
        /// <param name="nidaDrugQuestionnaireRepository">The nida drug questionnaire repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public NidaDrugQuestionnaireFactory (
            INidaDrugQuestionnaireRepository nidaDrugQuestionnaireRepository,
            ILookupValueRepository lookupValueRepository )
        {
            _nidaDrugQuestionnaireRepository = nidaDrugQuestionnaireRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        #region INidaDrugQuestionnaireFactory Members

        /// <summary>
        /// If the visit does not have a NidaDrugQuestionnaire, then it returns a newly created NidaDrugQuestionnaire,
        /// else, it returns an existing one from the visit.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A NidaDrugQuestionnaire.</returns>
        public NidaDrugQuestionnaire CreateNidaDrugQuestionnaire ( Visit visit )
        {
            Check.IsNotNull(visit, "visit is required.");

            NidaDrugQuestionnaire nidaDrugQuestionnaire;
            var existingNidaDrugQuestionnaire = _nidaDrugQuestionnaireRepository.GetNidaDrugQuestionnaireInVisit ( visit.Key );

            if ( existingNidaDrugQuestionnaire != null )
            {
                nidaDrugQuestionnaire = existingNidaDrugQuestionnaire;
            }
            else
            {
                var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.NidaDrugQuestionnaire );
                nidaDrugQuestionnaire = new NidaDrugQuestionnaire ( visit, activityType );

                _nidaDrugQuestionnaireRepository.MakePersistent ( nidaDrugQuestionnaire );

                DomainEvent.Raise(new NidaDrugQuestionnaireCreatedEvent { NidaDrugQuestionnaire = nidaDrugQuestionnaire });
            }

            return nidaDrugQuestionnaire;
        }

        /// <summary>
        /// Destroys the NidaDrugQuestionnaire questionnaire.
        /// </summary>
        /// <param name="nidaDrugQuestionnaire">The Nida drug questionnaire.</param>
        public void DestroyNidaDrugQuestionnaire ( NidaDrugQuestionnaire nidaDrugQuestionnaire )
        {
            _nidaDrugQuestionnaireRepository.MakeTransient ( nidaDrugQuestionnaire );
        }

        #endregion

        #region Implementation of IActivityFactory

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateNidaDrugQuestionnaire ( visit );
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyNidaDrugQuestionnaire ( ( NidaDrugQuestionnaire ) activity );
        }

        #endregion
    }
}