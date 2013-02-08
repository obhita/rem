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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The CdsRuleFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.CdsRule">CdsRule</see>.
    /// </summary>
    public class CdsRuleFactory : ICdsRuleFactory
    {
        private readonly ICdsRuleRepository _cdsRuleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CdsRuleFactory"/> class.
        /// </summary>
        /// <param name="cdsRuleRepository">The CDS rule repository.</param>
        public CdsRuleFactory ( ICdsRuleRepository cdsRuleRepository)
        {
            _cdsRuleRepository = cdsRuleRepository;
        }

        #region ICdsRuleFactory Members

        /// <summary>
        /// Creates the CDS rule.
        /// </summary>
        /// <returns>A CdsRule.</returns>
        public CdsRule CreateCdsRule ()
        {
            var cdsRule = new CdsRule ();
            _cdsRuleRepository.MakePersistent ( cdsRule );
            return cdsRule;
        }

        /// <summary>
        /// Destroys the CDS rule.
        /// </summary>
        /// <param name="cdsRule">The CDS rule.</param>
        public void DestroyCdsRule ( CdsRule cdsRule )
        {
            _cdsRuleRepository.MakeTransient ( cdsRule );
        }

        #endregion
    }
}