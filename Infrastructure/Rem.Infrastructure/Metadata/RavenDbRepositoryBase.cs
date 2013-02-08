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
using Pillar.Common.Metadata;
using Raven.Client;

namespace Rem.Infrastructure.Metadata
{
    /// <summary>
    /// Provides RavenDb repository services.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class RavenDbRepositoryBase<TEntity> : IRepository<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RavenDbRepositoryBase&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="documentSessionProvider">The document session provider.</param>
        protected RavenDbRepositoryBase(IDocumentSessionProvider documentSessionProvider)
        {
            if (documentSessionProvider == null)
            {
                throw new ArgumentNullException("documentSessionProvider");
            }

            Session = documentSessionProvider.GetDocumentSession ();

            if (Session == null)
            {
                throw new ArgumentException("documentSessionProvider should provide a session rather than a null value.");
            }
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        protected IDocumentSession Session { get; private set; }

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>An entity with the specific id.</returns>
        public TEntity GetById(long id)
        {
            var entity = Session.Load<TEntity>(id);
            return entity;
        }

        /// <summary>
        /// Makes the  entity persistent.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        public void MakePersistent(TEntity entity)
        {
            Session.Store(entity);
        }

        /// <summary>
        /// Makes the entity transient.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient(TEntity entity)
        {
            Session.Delete(entity);
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges ()
        {
            Session.SaveChanges ();
        }
    }
}
