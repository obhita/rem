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
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// SystemUsageEvent defines system usage event information.
    /// </summary>
    public class SystemUsageEvent : AuditableAggregateRootBase
    {
        #region Private members

        private EventType _eventType;
        private string _ipAddress;
        private SystemAccount _systemAccount;
        private DateTimeOffset _usageTimestamp;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemUsageEvent"/> class.
        /// </summary>
        protected internal SystemUsageEvent ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemUsageEvent"/> class.
        /// </summary>
        /// <param name="systemAccount">The system account.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="eventType">Type of the event.</param>
        protected internal SystemUsageEvent (
            SystemAccount systemAccount,
            string ipAddress,
            EventType eventType )
        {
            _systemAccount = systemAccount;
            _ipAddress = ipAddress;
            _eventType = eventType;
            _usageTimestamp = DateTimeOffset.UtcNow;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the system account.
        /// </summary>
        [NotNull]
        public virtual SystemAccount SystemAccount
        {
            get { return _systemAccount; }
            private set { ApplyPropertyChange ( ref _systemAccount, () => SystemAccount, value ); }
        }

        /// <summary>
        /// Gets the usage timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset UsageTimestamp
        {
            get { return _usageTimestamp; }
            private set { ApplyPropertyChange ( ref _usageTimestamp, () => UsageTimestamp, value ); }
        }

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        [NotNull]
        public virtual string IpAddress
        {
            get { return _ipAddress; }
            private set { ApplyPropertyChange ( ref _ipAddress, () => IpAddress, value ); }
        }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <value>
        /// The type of the event.
        /// </value>
        [NotNull]
        public virtual EventType EventType
        {
            get { return _eventType; }
            private set { ApplyPropertyChange ( ref _eventType, () => EventType, value ); }
        }

        #endregion
    }
}