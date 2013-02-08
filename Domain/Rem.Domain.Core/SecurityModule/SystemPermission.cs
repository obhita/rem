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

using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// SystemPermission defines an access right that can be attached to a system object. 
    /// </summary>
    public class SystemPermission : AuditableAggregateRootBase
    {
        private string _description;
        private string _displayName;
        private string _wellKnownName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemPermission"/> class.
        /// </summary>
        protected SystemPermission ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemPermission"/> class.
        /// </summary>
        /// <param name="wellKnownName">Name of the well known.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="description">The description.</param>
        protected internal SystemPermission ( string wellKnownName, string displayName, string description )
        {
            Check.IsNotNullOrWhitespace ( wellKnownName, () => WellKnownName );
            Check.IsNotNullOrWhitespace ( displayName, () => DisplayName );
            Check.IsNotNullOrWhitespace ( description, () => Description );

            _wellKnownName = wellKnownName;
            _displayName = displayName;
            _description = description;
        }

        /// <summary>
        /// Gets the name of the well known.
        /// </summary>
        /// <value>
        /// The name of the well known.
        /// </value>
        [NotNull]
        public virtual string WellKnownName
        {
            get { return _wellKnownName; }
            private set { ApplyPropertyChange ( ref _wellKnownName, () => WellKnownName, value ); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        [NotNull]
        public virtual string DisplayName
        {
            get { return _displayName; }
            private set { ApplyPropertyChange ( ref _displayName, () => DisplayName, value ); }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        [NotNull]
        public virtual string Description
        {
            get { return _description; }
            private set { ApplyPropertyChange ( ref _description, () => Description, value ); }
        }
    }
}