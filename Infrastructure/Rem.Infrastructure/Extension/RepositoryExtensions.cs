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
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Infrastructure.Extension
{
    /// <summary>
    /// RepositoryExtensions class.
    /// </summary>
    public static class RepositoryExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the by key or throw.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="key">The key of the entity to get.</param>
        /// <param name="aggregateName">Name of the aggregate.</param>
        /// <returns>An instance of the entity.</returns>
        public static T GetByKeyOrThrow<T> ( this IRepository<T> repository, long key, string aggregateName )
            where T : IAggregateRoot
        {
            var aggregateRoot = repository.GetByKey ( key );
            if ( aggregateRoot == null )
            {
                throw new InvalidOperationException ( string.Format ( "{0} was not found." ) );
            }
            return aggregateRoot;
        }

        #endregion
    }
}
