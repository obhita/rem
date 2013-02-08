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

using Rem.Domain.Clinical.PatientModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Defines repository services for the <see cref="T:Rem.Domain.Clinical.PatientModule.CdsRule">CdsRule</see>.
    /// </summary>
    public class CdsRuleRepository : NHibernateRepositoryBase<CdsRule>, ICdsRuleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CdsRuleRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public CdsRuleRepository ( ISessionProvider sessionProvider ) 
            : base ( sessionProvider )
        {
        }

        /// <summary>
        /// Gets a CdsRule by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A CdsRule.</returns>
        public CdsRule GetByKey(long key)
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Saves a CdsRule.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A CdsRule.</returns>
        public CdsRule MakePersistent(CdsRule entity)
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Deletes a CdsRule.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MakeTransient(CdsRule entity)
        {
            Helper.MakeTransient(entity);
        }
    }
}
