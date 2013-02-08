#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using AutoMapper;
using Pillar.Domain;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service.DataTransferObject;
using StructureMap.Attributes;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Provides a generic way to construct a KeyedDataTransferObject based on its Primary Key. Uses the [Auto]Mapper Configuration to map from the TEntity to the KeyedDataTransferObject specified in TDto. TODO: Future improvements could be an AutoMap Registry to close the generic, with a concrete &lt;TEntity,TDto&gt; KeyedDtoFactory, thus removing the need to create an class per Dto. Example: KeyedDtoFactoryRegistry.Register&lt;GpraFollowUp,GpraFollowUpDto&gt;() .Register&lt;GpraDischarge,GpraDischargeDto&gt;()
    /// </summary>
    /// <typeparam name="TEntity">
    /// The Type of Entity (Class derived from Rem.Common.Domain.Entity) 
    /// </typeparam>
    /// <typeparam name="TDto">
    /// Any Class derived from KeyedDataTransferObject 
    /// </typeparam>
    public abstract class KeyedDtoFactoryBase<TEntity, TDto> : IKeyedDtoFactory<TDto>
        where TDto : KeyedDataTransferObject
        where TEntity : Entity
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the session provider.
        /// </summary>
        /// <value> The session provider. </value>
        [SetterProperty]
        public ISessionProvider SessionProvider { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the keyed dto.
        /// </summary>
        /// <param name="key">The key of the dto to be created.</param>
        /// <returns>Returns the dto.</returns>
        public TDto CreateKeyedDto ( long key )
        {
            var entity = SessionProvider.GetSession ().Get<TEntity> ( key );
            TDto dto = Mapper.Map<TEntity, TDto> ( entity );
            return dto;
        }

        #endregion
    }
}
