﻿#region License
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
using Rem.Domain.Clinical.ImmunizationModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.ImmunizationModule.Immunization">Immunization</see>.
    /// </summary>
    public class ImmunizationRepository : NHibernateRepositoryBase<Immunization>, IImmunizationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImmunizationRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public ImmunizationRepository ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        /// <summary>
        /// Gets an Immunization by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>An Immunization.</returns>
        public Immunization GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Saves an Immunization.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>An Immunization.</returns>
        public Immunization MakePersistent ( Immunization entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Deletes an Immunization.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( Immunization entity )
        {
            Helper.MakeTransient ( entity );
        }
    }
}