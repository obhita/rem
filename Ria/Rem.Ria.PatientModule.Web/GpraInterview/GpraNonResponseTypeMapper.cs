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
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.GpraModule;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Class for mapping gpra non response type.
    /// </summary>
    public static class GpraNonResponseTypeMapper
    {
        #region Public Methods

        /// <summary>
        /// Maps the type of to gpra non response.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="gpraNonResponseTypeDto">The gpra non response type dto.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.GpraModule.GpraNonResponseType&lt;T&gt;"/></returns>
        public static GpraNonResponseType<T> MapToGpraNonResponseType<T> (
            GpraNonResponseTypeDto<LookupValueDto> gpraNonResponseTypeDto, IDtoToDomainMappingHelper mappingHelper ) where T : LookupBase
        {
            var result = new GpraNonResponseType<T> ();

            if ( gpraNonResponseTypeDto != null )
            {
                if ( gpraNonResponseTypeDto.Value != null || gpraNonResponseTypeDto.GpraNonResponse != null )
                {
                    result = gpraNonResponseTypeDto.GpraNonResponse != null
                                 ? new GpraNonResponseType<T> (
                                       mappingHelper.MapLookupField<GpraNonResponse> ( gpraNonResponseTypeDto.GpraNonResponse ) )
                                 : new GpraNonResponseType<T> ( mappingHelper.MapLookupField<T> ( gpraNonResponseTypeDto.Value ) );
                }
            }
            else
            {
                throw new InvalidOperationException ( "GpraNonResponseTypeDto Cannot be Null." );
            }

            return result;
        }

        /// <summary>
        /// Maps the type of to gpra non response.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="gpraNonResponseTypeDto">The gpra non response type dto.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.GpraModule.GpraNonResponseType&lt;T&gt;"/></returns>
        public static GpraNonResponseType<T> MapToGpraNonResponseType<T> (
            GpraNonResponseTypeDto<T> gpraNonResponseTypeDto, IDtoToDomainMappingHelper mappingHelper )
        {
            var result = new GpraNonResponseType<T> ();

            if ( gpraNonResponseTypeDto != null )
            {
                if ( !typeof( T ).IsNullable () || gpraNonResponseTypeDto.Value != null || gpraNonResponseTypeDto.GpraNonResponse != null )
                {
                    result = gpraNonResponseTypeDto.GpraNonResponse != null
                                 ? new GpraNonResponseType<T> (
                                       mappingHelper.MapLookupField<GpraNonResponse> ( gpraNonResponseTypeDto.GpraNonResponse ) )
                                 : new GpraNonResponseType<T> ( gpraNonResponseTypeDto.Value );
                }
            }
            else
            {
                throw new InvalidOperationException ( "GpraNonResponseTypeDto Cannot be Null." );
            }

            return result;
        }

        #endregion
    }
}
