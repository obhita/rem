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

using AutoMapper;
using Rem.Domain.Clinical.GpraModule;
using Rem.Infrastructure.Service.DataTransferObject.Mapping;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// GpraNodeAutoMapperSetup class.
    /// </summary>
    public static class GpraNodeAutoMapperSetup
    {
        #region Public Methods

        /// <summary>
        /// Creates the map to gpra dto.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <returns>A <see cref="AutoMapper.IMappingExpression&lt;TSource,TDestination&gt;"/></returns>
        public static IMappingExpression<TSource, TDestination> CreateMapToGpraDto<TSource, TDestination> ()
            where TSource : GpraInterviewSectionBase
            where TDestination : GpraDtoBase
        {
            return AutoMapperSetup.CreateMapToEditableDto<TSource, TDestination> ()
                .ForMember ( dest => dest.GpraInterviewKey, opt => opt.Ignore () )
                .ForMember ( dest => dest.Key, opt => opt.Ignore () )
                .ForMember ( dest => dest.GpraNonResponseFilters, opt => opt.MapFrom ( src => src.GetFiltersDictionary () ) );
        }

        #endregion
    }
}
