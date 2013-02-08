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

using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling associate problems with visit request.
    /// </summary>
    public class AssociateProblemsWithVisitRequestHandler :
        NHibernateSessionRequestHandler<AssociateProblemsWithVisitRequest, AssociateProblemsWithVisitResponse>
    {
        #region Constants and Fields

        private readonly IProblemRepository _problemRepository;
        private readonly IVisitRepository _visitRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociateProblemsWithVisitRequestHandler"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        /// <param name="problemRepository">The problem repository.</param>
        public AssociateProblemsWithVisitRequestHandler (
            IVisitRepository visitRepository, IProblemRepository problemRepository )
        {
            _visitRepository = visitRepository;
            _problemRepository = problemRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( AssociateProblemsWithVisitRequest request )
        {
            var problemDtos = request.ProblemDtos;
            var visitKey = request.VisitKey;

            IList<ProblemDto> results = new List<ProblemDto> ();

            var visit = _visitRepository.GetByKey ( visitKey );
            if ( visit != null )
            {
                var problemList = new List<Problem> ();
                foreach ( var problemDto in problemDtos )
                {
                    var problem = _problemRepository.GetByKey ( problemDto.Key );
                    if ( problem != null )
                    {
                        problemList.Add ( problem );
                    }
                }

                if ( problemDtos.Count > 0 )
                {
                    visit.AssociateProblems ( problemList );
                }

                var visitDto = Mapper.Map<Visit, VisitDto> ( visit );
                results = visitDto.Problems;
            }

            var response = CreateTypedResponse ();
            response.ProblemDtos = results;

            return response;
        }

        #endregion
    }
}
