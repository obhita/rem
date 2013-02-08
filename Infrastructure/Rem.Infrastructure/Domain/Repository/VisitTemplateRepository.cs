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
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.VisitModule.VisitTemplate">VisitTemplate</see>.
    /// </summary>
    public class VisitTemplateRepository : NHibernateRepositoryBase<VisitTemplate>, IVisitTemplateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisitTemplateRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public VisitTemplateRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        /// <summary>
        /// Gets the VisitTemplate by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A VisitTemplate.</returns>
        public VisitTemplate GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Saves a VisitTemplate.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A VisitTemplate.</returns>
        public VisitTemplate MakePersistent ( VisitTemplate entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Deletes a VisitTemplate.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( VisitTemplate entity )
        {
            Helper.MakeTransient ( entity );
        }

        #region Implementation of IVisitTemplateRepository

        /// <summary>
        /// Gets the visit templates by agency.
        /// </summary>
        /// <param name="agencyKey">The agency key.</param>
        /// <returns>
        /// A IList&lt;VisitTemplate&gt;
        /// </returns>
        public IList<VisitTemplate> GetVisitTemplatesByAgency ( long agencyKey )
        {
            var visitTemplates = from visitTemplate in Session.Query<VisitTemplate> ()
                                 where visitTemplate.Agency.Key == agencyKey
                                 select visitTemplate;

            return visitTemplates.ToList ();
        }

        #endregion
    }
}