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

using Pillar.Domain.Event;

namespace Rem.Domain.Core.AgencyModule.Event
{
    /// <summary>
    /// AgencyProfileChangedEventHandler handles when an Agency's Legal Name changes. The old legal name is added to that agency's aliases.
    /// </summary>
    public class AgencyProfileChangedEventHandler : IDomainEventHandler<AgencyProfileChangedEvent>
    {
        #region Implementation of IDomainEventHandler<in AgencyProfileChangedEvent>

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="agencyNameChangedEvent">
        /// The agency name changed event.
        /// </param>
        public void Handle ( AgencyProfileChangedEvent agencyNameChangedEvent )
        {
            if (agencyNameChangedEvent.OldAgencyProfile.AgencyName.LegalName != agencyNameChangedEvent.Agency.AgencyProfile.AgencyName.LegalName)
            {
                agencyNameChangedEvent.Agency.AddAgencyAlias(new AgencyAlias(agencyNameChangedEvent.OldAgencyProfile.AgencyName.LegalName, null));
            }
        }

        #endregion
    }
}