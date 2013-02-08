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

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Data transfer object for PatientAlias class.
    /// </summary>
    [DataContract]
    public class PatientAliasDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private LookupValueDto _aliasType;
        private string _firstName;
        private string _lastName;
        private string _middleName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set { ApplyPropertyChange ( ref _firstName, () => FirstName, value ); }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set { ApplyPropertyChange ( ref _lastName, () => LastName, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        [DataMember]
        public string MiddleName
        {
            get { return _middleName; }
            set { ApplyPropertyChange ( ref _middleName, () => MiddleName, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the patient alias.
        /// </summary>
        /// <value>The type of the patient alias.</value>
        [DataMember]
        public LookupValueDto PatientAliasType
        {
            get { return _aliasType; }
            set { ApplyPropertyChange ( ref _aliasType, () => PatientAliasType, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey { get; set; }

        #endregion
    }
}
