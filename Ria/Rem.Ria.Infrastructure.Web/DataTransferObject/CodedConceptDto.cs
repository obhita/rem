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
    /// Data transfer object for CodedConcept class.
    /// </summary>
    [DataContract]
    public partial class CodedConceptDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private string _codeSystemIdentifier;
        private string _codeSystemName;
        private string _codeSystemVersionNumber;
        private string _codedConceptCode;
        private string _displayName;
        private bool _nullFlavorIndicator;
        private string _originalDescription;

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

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [DataMember]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged ( () => DisplayName );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [null flavor indicator].
        /// </summary>
        /// <value><c>true</c> if [null flavor indicator]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool NullFlavorIndicator
        {
            get { return _nullFlavorIndicator; }
            set
            {
                _nullFlavorIndicator = value;
                RaisePropertyChanged ( () => NullFlavorIndicator );
            }
        }

        /// <summary>
        /// Gets or sets the original description.
        /// </summary>
        /// <value>The original description.</value>
        [DataMember]
        public string OriginalDescription
        {
            get { return _originalDescription; }
            set
            {
                _originalDescription = value;
                RaisePropertyChanged ( () => OriginalDescription );
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Equalses the specified coded concept dto.
        /// </summary>
        /// <param name="codedConceptDto">The coded concept dto.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public virtual bool Equals ( CodedConceptDto codedConceptDto )
        {
            if ( ReferenceEquals ( null, codedConceptDto ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, codedConceptDto ) )
            {
                return true;
            }
            if ( GetType () != codedConceptDto.GetType () )
            {
                return false;
            }

            var otherIsTransient = Equals ( codedConceptDto.CodedConceptCode, ( long )0 );
            var thisIsTransient = Equals ( CodedConceptCode, ( long )0 );

            if ( otherIsTransient && thisIsTransient )
            {
                return ReferenceEquals ( codedConceptDto, this );
            }

            return codedConceptDto.CodedConceptCode.Equals ( CodedConceptCode );
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( GetType () != obj.GetType () )
            {
                return false;
            }

            return Equals ( obj as CodedConceptDto );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode ()
        {
            return ( ( CodedConceptCode ?? string.Empty ).GetHashCode () * 397 ) ^ GetType ().GetHashCode ();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString ()
        {
            return DisplayName;
        }

        #endregion
    }
}
