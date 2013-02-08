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
using System.Linq;
using Agatha.Common;
using AutoMapper;
using Pillar.Common.Extension;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling get all problems by clinical case request.
    /// </summary>
    public class GetAllProblemsByClinicalCaseRequestHandler :
        NHibernateSessionRequestHandler<GetAllProblemsByClinicalCaseRequest, GetAllProblemsByClinicalCaseResponse>
    {
        #region Constants and Fields

        private readonly IClinicalCaseRepository _clinicalCaseRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProblemsByClinicalCaseRequestHandler"/> class.
        /// </summary>
        /// <param name="clinicalCaseRepository">The clinical case repository.</param>
        public GetAllProblemsByClinicalCaseRequestHandler ( IClinicalCaseRepository clinicalCaseRepository )
        {
            _clinicalCaseRepository = clinicalCaseRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetAllProblemsByClinicalCaseRequest request )
        {
            var clinicalCaseKey = request.ClinicalCaseKey;

            // List of all Associated Problems
            var associatedProblems =
                _clinicalCaseRepository.GetAllAssociatedProblemByClinicalCaseKey ( clinicalCaseKey ).ToList ();

            // List of all Not Associated Problems 
            var notAssociatedProblems =
                _clinicalCaseRepository.GetAllNotAssociatedProblemsByClinicalCaseKey ( clinicalCaseKey ).ToList ();

            var associatedDtos = Mapper.Map<IList<Problem>, IList<ProblemDto>> ( associatedProblems );
            var notAssociatedDtos = Mapper.Map<IList<Problem>, IList<ProblemDto>> ( notAssociatedProblems );

            // Set the Associated Bit for each Associated Problem
            associatedDtos.ForEach ( d => d.AssociatedIndicator = true );

            // Set the Associated Bit for each notAssociated Problem
            notAssociatedDtos.ForEach ( d => d.AssociatedIndicator = false );

            // Merge the Results
            var results = new List<ProblemDto> ();
            results.AddRange ( associatedDtos );
            results.AddRange ( notAssociatedDtos );
            results = results.OrderBy ( d => d.ProblemCodeCodedConcept.DisplayName ).ToList ();

            var response = CreateTypedResponse ();
            response.ProblemDtos = results;

            return response;
        }

        #endregion
    }
}
