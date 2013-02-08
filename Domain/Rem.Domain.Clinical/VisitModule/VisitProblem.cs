using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Clinical.ClinicalCaseModule;

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

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// VisitProblem defines a patient problem that has been associated to a visit.
    /// </summary>
    public class VisitProblem : VisitAggregateNodeBase
    {
        private Problem _problem;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitProblem"/> class.
        /// </summary>
        protected internal VisitProblem ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitProblem"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="problem">The problem.</param>
        protected internal VisitProblem ( Visit visit, Problem problem ) : base ( visit )
        {
            Check.IsNotNull ( problem, "Problem is required." );
            _problem = problem;
        }

        /// <summary>
        /// Gets or sets the problem.
        /// </summary>
        /// <value>
        /// The problem.
        /// </value>
        [NotNull]
        public virtual Problem Problem
        {
            get
            {
                return _problem;
            }

            set
            {
                Check.IsNotNull ( value, "Problem is required." );
                ApplyPropertyChange ( ref _problem, () => Problem, value );
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Problem.ToString ();
        }
    }
}