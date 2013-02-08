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

namespace Rem.Ria.Infrastructure.Web.DataTransferObject
{
    /// <summary>
    /// Data transfer object for CodedConceptLookupValue class.
    /// </summary>
    [DataContract]
    public class CodedConceptLookupValueDto : LookupValueDto
    {
        #region Constants and Fields

        private string _codeSystemIdentifier;
        private string _codeSystemName;
        private string _codeSystemVersionNumber;
        private string _codedConceptCode;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the code system identifier.
        /// </summary>
        /// <value>The code system identifier.</value>
        [DataMember]
        public string CodeSystemIdentifier
        {
            get { return _codeSystemIdentifier; }
            set
            {
                _codeSystemIdentifier = value;
                RaisePropertyChanged ( () => CodeSystemIdentifier );
            }
        }

        /// <summary>
        /// Gets or sets the name of the code system.
        /// </summary>
        /// <value>The name of the code system.</value>
        [DataMember]
        public string CodeSystemName
        {
            get { return _codeSystemName; }
            set
            {
                _codeSystemName = value;
                RaisePropertyChanged ( () => CodeSystemName );
            }
        }

        /// <summary>
        /// Gets or sets the code system version number.
        /// </summary>
        /// <value>The code system version number.</value>
        [DataMember]
        public string CodeSystemVersionNumber
        {
            get { return _codeSystemVersionNumber; }
            set
            {
                _codeSystemVersionNumber = value;
                RaisePropertyChanged ( () => CodeSystemVersionNumber );
            }
        }

        /// <summary>
        /// Gets or sets the coded concept code.
        /// </summary>
        /// <value>The coded concept code.</value>
        [DataMember]
        public string CodedConceptCode
        {
            get { return _codedConceptCode; }
            set
            {
                _codedConceptCode = value;
                RaisePropertyChanged ( () => CodedConceptCode );
            }
        }

        #endregion
    }
}
