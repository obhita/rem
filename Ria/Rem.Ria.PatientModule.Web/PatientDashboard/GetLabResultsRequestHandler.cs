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
using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Rem.Domain.Clinical.LabModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling get lab results request.
    /// </summary>
    public class GetLabResultsRequestHandler : NHibernateSessionRequestHandler<GetLabResultsRequest, GetLabResultsResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetLabResultsRequest request )
        {
            var response = CreateTypedResponse ();

            var labSpecimenKey = request.ActivityKey;
            var labSpecimenCriteria =
                DetachedCriteria.For<LabSpecimen> ().Add ( Restrictions.Eq ( Projections.Property<LabSpecimen> ( ls => ls.Key ), labSpecimenKey ) )
                    .CreateCriteria ( "LabTests", JoinType.LeftOuterJoin ).SetResultTransformer (
                        Transformers.DistinctRootEntity );
            var labTestCriteria =
                DetachedCriteria.For<LabTest> ().Add ( Restrictions.Eq ( "LabSpecimen.Key", labSpecimenKey ) ).CreateCriteria (
                    "LabResults",
                    JoinType.LeftOuterJoin );
            var multiCriteria = Session.CreateMultiCriteria ().Add ( labSpecimenCriteria ).Add ( labTestCriteria );

            var labSpecimenList = multiCriteria.List ();

            response.LabSpecimenDtos = new List<LabSpecimenDto> ();

            foreach ( ArrayList specimenList in labSpecimenList )
            {
                if ( specimenList[0] is LabSpecimen )
                {
                    var specimen = ( LabSpecimen )specimenList[0];
                    var labSpecimenDto = Mapper.Map<LabSpecimen, LabSpecimenDto> ( specimen );
                    response.LabSpecimenDtos.Add ( labSpecimenDto );
                }
            }

            return response;
        }

        #endregion
    }
}
