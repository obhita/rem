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
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for PatientOtherConsiderations class.
    /// </summary>
    public class PatientOtherConsiderationsDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private SoftDeleteObservableCollection<LookupValueDto> _disabilities;
        private bool? _interpreterNeededIndicator;
        private LookupValueDto _language;
        private string _note;
        private bool? _paperFileIndicator;
        private LookupValueDto _smokingStatus;
        private SoftDeleteObservableCollection<LookupValueDto> _specialNeeds;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the disabilities.
        /// </summary>
        /// <value>The disabilities.</value>
        [DataMember]
        public SoftDeleteObservableCollection<LookupValueDto> Disabilities
        {
            get { return _disabilities; }
            set { ApplyPropertyChange ( ref _disabilities, () => Disabilities, value ); }
        }

        /// <summary>
        /// Gets or sets the interpreter needed indicator.
        /// </summary>
        /// <value>The interpreter needed indicator.</value>
        [DataMember]
        public bool? InterpreterNeededIndicator
        {
            get { return _interpreterNeededIndicator; }
            set { ApplyPropertyChange ( ref _interpreterNeededIndicator, () => InterpreterNeededIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [DataMember]
        public LookupValueDto Language
        {
            get { return _language; }
            set { ApplyPropertyChange ( ref _language, () => Language, value ); }
        }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>The note for the considerations.</value>
        [DataMember]
        public string Note
        {
            get { return _note; }
            set { ApplyPropertyChange ( ref _note, () => Note, value ); }
        }

        /// <summary>
        /// Gets or sets the paper file indicator.
        /// </summary>
        /// <value>The paper file indicator.</value>
        [DataMember]
        public bool? PaperFileIndicator
        {
            get { return _paperFileIndicator; }
            set { ApplyPropertyChange ( ref _paperFileIndicator, () => PaperFileIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the smoking status.
        /// </summary>
        /// <value>The smoking status.</value>
        [DataMember]
        public LookupValueDto SmokingStatus
        {
            get { return _smokingStatus; }
            set { ApplyPropertyChange ( ref _smokingStatus, () => SmokingStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the special needs.
        /// </summary>
        /// <value>The special needs.</value>
        [DataMember]
        public SoftDeleteObservableCollection<LookupValueDto> SpecialNeeds
        {
            get { return _specialNeeds; }
            set { ApplyPropertyChange ( ref _specialNeeds, () => SpecialNeeds, value ); }
        }

        #endregion
    }
}
