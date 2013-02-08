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

using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for PatientLegalStatus class.
    /// </summary>
    public class PatientLegalStatusDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private LookupValueDto _citizenshipCountry;
        private LookupValueDto _custodialStatus;
        private LookupValueDto _immigrationStatus;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the citizenship country.
        /// </summary>
        /// <value>The citizenship country.</value>
        [DataMember]
        public LookupValueDto CitizenshipCountry
        {
            get { return _citizenshipCountry; }
            set { ApplyPropertyChange ( ref _citizenshipCountry, () => CitizenshipCountry, value ); }
        }

        /// <summary>
        /// Gets or sets the custodial status.
        /// </summary>
        /// <value>The custodial status.</value>
        [DataMember]
        public LookupValueDto CustodialStatus
        {
            get { return _custodialStatus; }
            set { ApplyPropertyChange ( ref _custodialStatus, () => CustodialStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the immigration status.
        /// </summary>
        /// <value>The immigration status.</value>
        [DataMember]
        public LookupValueDto ImmigrationStatus
        {
            get { return _immigrationStatus; }
            set { ApplyPropertyChange ( ref _immigrationStatus, () => ImmigrationStatus, value ); }
        }

        #endregion
    }
}
