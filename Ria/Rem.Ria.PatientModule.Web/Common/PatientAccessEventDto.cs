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
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for PatientAccessEvent class.
    /// </summary>
    [DataContract]
    public class PatientAccessEventDto : AbstractDataTransferObject
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the created timestamp.
        /// </summary>
        /// <value>The created timestamp.</value>
        [DataMember]
        public DateTimeOffset CreatedTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>The note for the access event.</value>
        [DataMember]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the name of the patient access event type.
        /// </summary>
        /// <value>The name of the patient access event type.</value>
        [DataMember]
        public string PatientAccessEventTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>The name of the patient.</value>
        [DataMember]
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the audited context description.
        /// </summary>
        /// <value>
        /// The audited context description.
        /// </value>
        [DataMember]
        public string AuditedContextDescription { get; set; }

        /// <summary>
        /// Gets or sets the name of the aggregate root type.
        /// </summary>
        /// <value>
        /// The name of the aggregate root type.
        /// </value>
        [DataMember]
        public string AggregateRootTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the aggregate node type.
        /// </summary>
        /// <value>
        /// The name of the aggregate node type.
        /// </value>
        [DataMember]
        public string AggregateNodeTypeName { get; set; }

        #endregion
    }
}
