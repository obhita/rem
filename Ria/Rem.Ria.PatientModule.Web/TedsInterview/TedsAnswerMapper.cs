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
using Pillar.Common.Extension;
using Rem.Domain.Clinical.TedsModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Class for mapping TEDS non response type.
    /// </summary>
    public static class TedsAnswerMapper
    {
        #region Public Methods

        /// <summary>
        /// Maps to teds answer.
        /// </summary>
        /// <typeparam name="TLookupBase">The type of the lookup base.</typeparam>
        /// <typeparam name="TLookupValueDto">The type of the lookup value dto.</typeparam>
        /// <param name="tedsAnswerDto">The teds answer dto.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <returns>A <see cref="TedsAnswer{T}"/></returns>
        public static TedsAnswer<TLookupBase> MapToTedsAnswer<TLookupBase, TLookupValueDto> (
            TedsAnswerDto<TLookupValueDto> tedsAnswerDto, IDtoToDomainMappingHelper mappingHelper ) 
            where TLookupBase : LookupBase 
            where TLookupValueDto : LookupValueDto
        {
            TedsAnswer<TLookupBase> result = null;

            if ( tedsAnswerDto != null )
            {
                if ( tedsAnswerDto.Response != null || tedsAnswerDto.TedsNonResponse != null )
                {
                    result = tedsAnswerDto.TedsNonResponse != null
                                 ? new TedsAnswer<TLookupBase>(
                                       mappingHelper.MapLookupField<TedsNonResponse> ( tedsAnswerDto.TedsNonResponse ) )
                                 : new TedsAnswer<TLookupBase>(mappingHelper.MapLookupField<TLookupBase>(tedsAnswerDto.Response));
                }
            }
            else
            {
                throw new InvalidOperationException ( "TedsAnswerDto Cannot be Null." );
            }

            return result;
        }

        /// <summary>
        /// Maps the type of TedsAnswerDto to TedsAnswer.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="tedsAnswerDto">The teds non response type dto.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <returns>A <see cref="TedsAnswer{T}"/></returns>
        public static TedsAnswer<T> MapToTedsAnswer<T>(
           TedsAnswerDto<LookupValueDto> tedsAnswerDto, IDtoToDomainMappingHelper mappingHelper) where T : LookupBase
        {
            return MapToTedsAnswer<T, LookupValueDto>(tedsAnswerDto, mappingHelper);
        }

        /// <summary>
        /// Maps the type of to teds non response.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="tedsAnswerDto">The teds non response type dto.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <returns>A <see cref="TedsAnswer{T}"/></returns>
        public static TedsAnswer<T> MapToTedsAnswer<T> (
            TedsAnswerDto<T> tedsAnswerDto, IDtoToDomainMappingHelper mappingHelper )
        {
            TedsAnswer<T> result = null;

            if ( tedsAnswerDto != null )
            {
                if ( !typeof( T ).IsNullable () || tedsAnswerDto.Response != null || tedsAnswerDto.TedsNonResponse != null )
                {
                    result = tedsAnswerDto.TedsNonResponse != null
                                 ? new TedsAnswer<T> (
                                       mappingHelper.MapLookupField<TedsNonResponse> ( tedsAnswerDto.TedsNonResponse ) )
                                 : new TedsAnswer<T> ( tedsAnswerDto.Response );
                }
            }
            else
            {
                throw new InvalidOperationException ( "TedsAnswerDto Cannot be Null." );
            }

            return result;
        }

        #endregion
    }
}
