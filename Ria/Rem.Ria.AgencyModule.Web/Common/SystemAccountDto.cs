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

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// DataTransferObject representing the System Account domain entity.
    /// </summary>
    public partial class SystemAccountDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private string _displayName;
        private string _emailAddress;
        private bool _enabledIndicator;
        private string _identityProviderName;
        private string _username;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [DataMember]
        public virtual string DisplayName
        {
            get { return _displayName; }
            set { ApplyPropertyChange ( ref _displayName, () => DisplayName, value ); }
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        [DataMember]
        public virtual string EmailAddress
        {
            get { return _emailAddress; }
            set { ApplyPropertyChange ( ref _emailAddress, () => EmailAddress, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enabled indicator].
        /// </summary>
        /// <value><c>true</c> if [enabled indicator]; otherwise, <c>false</c>.</value>
        [DataMember]
        public virtual bool EnabledIndicator
        {
            get { return _enabledIndicator; }
            set { ApplyPropertyChange ( ref _enabledIndicator, () => EnabledIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the identity provider.
        /// </summary>
        /// <value>The name of the identity provider.</value>
        [DataMember]
        public virtual string IdentityProviderName
        {
            get { return _identityProviderName; }
            set { ApplyPropertyChange ( ref _identityProviderName, () => IdentityProviderName, value ); }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [DataMember]
        public virtual string Username
        {
            get { return _username; }
            set { ApplyPropertyChange ( ref _username, () => Username, value ); }
        }

        #endregion
    }
}
