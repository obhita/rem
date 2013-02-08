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

using System.Collections;
using NHibernate.Criterion;
using Rem.Domain.Clinical.LabModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.LabModule.LabSpecimenType">LabSpecimenType</see>.
    /// </summary>
    public class LabSpecimenTypeRepository : NHibernateRepositoryBase<LabSpecimenType>, ILabSpecimenTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabSpecimenTypeRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public LabSpecimenTypeRepository ( ISessionProvider sessionProvider ) 
            : base ( sessionProvider )
        {
        }

        /// <summary>
        /// Gets a LabSpecimenType by coded concept code.
        /// </summary>
        /// <param name="codedConceptCode">The coded concept code.</param>
        /// <returns>
        /// A LabSpecimenType.
        /// </returns>
        public LabSpecimenType GetByCodedConceptCode(string codedConceptCode)
        {
            IList result = Session.CreateCriteria<LabSpecimenType>()
               .Add(Restrictions.Eq("CodedConceptCode", codedConceptCode))
               .SetMaxResults(1)
               .List();

            bool found = result != null && result.Count > 0;

            LabSpecimenType labSpecimenType = found ? result[0] as LabSpecimenType : null;

            return labSpecimenType;
        }

        /// <summary>
        /// Gets a LabSpecimenType by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A LabSpecimenType.</returns>
        public LabSpecimenType GetByKey ( long key )
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Saves a LabSpecimenType.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A LabSpecimenType.</returns>
        public LabSpecimenType MakePersistent ( LabSpecimenType entity )
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Deletes a LabSpecimenType.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( LabSpecimenType entity )
        {
            Helper.MakeTransient(entity);
        }
    }
}
