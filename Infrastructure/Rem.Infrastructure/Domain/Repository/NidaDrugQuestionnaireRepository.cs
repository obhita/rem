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
using System.Linq;
using NHibernate.Linq;
using Rem.Domain.Clinical.SbirtModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.SbirtModule.NidaDrugQuestionnaire">NidaDrugQuestionnaire</see>.
    /// </summary>
    public class NidaDrugQuestionnaireRepository : NHibernateRepositoryBase<NidaDrugQuestionnaire>, INidaDrugQuestionnaireRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NidaDrugQuestionnaireRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public NidaDrugQuestionnaireRepository ( ISessionProvider sessionProvider ) : base ( sessionProvider )
        {
        }

        #region INidaDrugQuestionnaireRepository members

        /// <summary>
        /// Gets a NidaDrugQuestionnaire by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A NidaDrugQuestionnaire.</returns>
        public NidaDrugQuestionnaire GetByKey ( long key )
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Saves a NidaDrugQuestionnaire.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A NidaDrugQuestionnaire.</returns>
        public NidaDrugQuestionnaire MakePersistent ( NidaDrugQuestionnaire entity )
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Deletes a NidaDrugQuestionnaire.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( NidaDrugQuestionnaire entity )
        {
            Helper.MakeTransient(entity);
        }

        #endregion

        #region Implementation of INidaDrugQuestionnaireRepository

        /// <summary>
        /// Gets the Nida Drug Questionnaire in visit.
        /// </summary>
        /// <param name="visitKey">The visit key.</param>
        /// <returns>
        /// A <see cref="T:Rem.Domain.Clinical.SbirtModule.NidaDrugQuestionnaire">NidaDrugQuestionnaire</see>.
        /// </returns>
        public NidaDrugQuestionnaire GetNidaDrugQuestionnaireInVisit(long visitKey)
        {
            //NOTE : There can be only one 'Nida Drug Questionnaire' per visit.
            return Session.Query<NidaDrugQuestionnaire>().Where(d => d.Visit.Key == visitKey).FirstOrDefault();
        }

        /// <summary>
        /// Determinss if the Nida Drug Questionnaire exist for a specified visit.
        /// </summary>
        /// <param name="visitKey">The visit key.</param>
        /// <returns>
        /// <c>True</c> if NidaDrugQuestionnaire exists.
        /// </returns>
        public bool DoesNidaDrugQuestionnaireExistForVisit(long visitKey)
        {
            var isNidaDrugQuestionnaireAlreadyExisted = Session.Query<NidaDrugQuestionnaire>().Any(x =>
                                                                    x.Visit.Key == visitKey
                                                                    && x.ActivityType.WellKnownName == WellKnownNames.VisitModule.ActivityType.NidaDrugQuestionnaire);
            return isNidaDrugQuestionnaireAlreadyExisted;
        }

        #endregion
    }
}