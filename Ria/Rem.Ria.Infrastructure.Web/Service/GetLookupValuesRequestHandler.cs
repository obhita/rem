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
using System.Text.RegularExpressions;
using Agatha.Common;
using NHibernate.Criterion;
using NHibernate.Transform;
using Pillar.Domain.NHibernate.Extensions;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.Infrastructure.Web.Service
{
    /// <summary>
    /// Class for handling get lookup values request.
    /// </summary>
    public class GetLookupValuesRequestHandler :
        NHibernateSessionRequestHandler<GetLookupValuesRequest, GetLookupValuesResponse>
    {
        private readonly ILookupTypeService _lookupTypeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLookupValuesRequestHandler"/> class.
        /// </summary>
        /// <param name="lookupTypeService">The lookup type service.</param>
        public GetLookupValuesRequestHandler (ILookupTypeService lookupTypeService )
        {
            _lookupTypeService = lookupTypeService;
        }

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle(GetLookupValuesRequest request)
        {
            var lookupType = _lookupTypeService.GetLookupType ( request.Name );
            var response = CreateTypedResponse ();
            response.Name = request.Name;

            if ( string.IsNullOrEmpty ( request.DerivedDtoType ) )
            {
                response.LookupValues = GetLookupDto ( lookupType );
            }
            else
            {
                response.LookupValues = GetLookupDto ( lookupType, request.DerivedDtoType );
            }

            return response;
        }

        private IList<LookupValueDto> GetLookupDto(Type lookupType)
        {
            var criteria = Session.CreateCriteria(lookupType);

            criteria.SetProjection(
                Projections.ProjectionList()
                    .Add<LookupBase, LookupValueDto, long>(p => p.Key, pDto => pDto.Key)
                    .Add<LookupBase, LookupValueDto, string>(
                        p => p.WellKnownName,
                        pDto => pDto.WellKnownName)
                    .Add<LookupBase, LookupValueDto, string>(p => p.Name, pDto => pDto.Name)
                    .Add<LookupBase, LookupValueDto, string>(p => p.ShortName, pDto => pDto.ShortName)
                    .Add<LookupBase, LookupValueDto, int?>(
                        p => p.SortOrderNumber,
                        pDto => pDto.SortOrderNumber));

            criteria.SetResultTransformer(Transformers.AliasToBean(typeof(LookupValueDto)));

            criteria.AddOrder(Order.Asc(Projections.Property<LookupBase>(p => p.SortOrderNumber)))
                .AddOrder(Order.Asc(Projections.Property<LookupBase>(p => p.Name)));

            return criteria.List<LookupValueDto>();
        }


        private IList<LookupValueDto> GetLookupDto(Type lookupType, string derivedDtoType)
        {
            var dtoType = GetDerivedDtoType ( derivedDtoType );

            var criteria = Session.CreateCriteria ( lookupType );

            var lookupBases = criteria.List ();

            var unsortedResult = new List<LookupValueDto> ();

            for ( int i = 0; i < lookupBases.Count; i++ )
            {
                unsortedResult.Add ( ( LookupValueDto )AutoMapper.Mapper.Map ( lookupBases[i], lookupType, dtoType ) );
            }

            return new List<LookupValueDto> (
                from dto in unsortedResult
                orderby dto.SortOrderNumber, dto.Name
                select dto );
        }

        private Type GetDerivedDtoType(string derivedDtoType)
        {
            var types = new List<Type> ();

            var regex = new Regex ( "Rem.Ria.*.Web" );
            var assemblies = from assembly in AppDomain.CurrentDomain.GetAssemblies ()
                             where regex.IsMatch ( assembly.FullName )
                             select assembly;

            foreach ( var assembly in assemblies )
            {
                types.AddRange ( assembly.GetTypes () );
            }

            var query = from type in types
                        where ( type.Name == derivedDtoType && type.IsSubclassOf ( typeof( LookupValueDto ) ) )
                        select type;
            var lookupDtoType = query.SingleOrDefault ();

            if ( lookupDtoType == null )
            {
                throw new ArgumentException ( string.Format ( "Unknown LookupValueDto: {0}", derivedDtoType ) );
            }

            return lookupDtoType;
        }


        #endregion
    }
}
