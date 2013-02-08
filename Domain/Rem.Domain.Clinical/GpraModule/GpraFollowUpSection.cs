#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraFollowUpSection contains patient follow up information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraFollowUpSection : GpraInterviewSectionBase
    {
        private readonly GpraFollowUpStatus _gpraFollowUpStatus;
        private readonly string _gpraFollowUpStatusOtherDescription;
        private readonly bool? _patientReceivingServicesIndicator;

        private GpraFollowUpSection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraFollowUpSection"/> class.
        /// </summary>
        /// <param name="gpraFollowUpStatus">The gpra follow up status.</param>
        /// <param name="gpraFollowUpStatusOtherDescription">The gpra follow up status other description.</param>
        /// <param name="patientReceivingServicesIndicator">The patient receiving services indicator.</param>
        public GpraFollowUpSection(GpraFollowUpStatus gpraFollowUpStatus,
                                   string gpraFollowUpStatusOtherDescription,
                                   bool? patientReceivingServicesIndicator)
        {
            _gpraFollowUpStatus = gpraFollowUpStatus;
            _gpraFollowUpStatusOtherDescription = gpraFollowUpStatusOtherDescription;
            _patientReceivingServicesIndicator = patientReceivingServicesIndicator;
        }

        /// <summary>
        /// Gets the Gpra follow up status.
        /// </summary>
        public virtual GpraFollowUpStatus GpraFollowUpStatus
        {
            get { return _gpraFollowUpStatus; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra follow up status other description.
        /// </summary>
        public virtual string GpraFollowUpStatusOtherDescription
        {
            get { return _gpraFollowUpStatusOtherDescription; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is receiving services.
        /// </summary>
        public virtual bool? PatientReceivingServicesIndicator
        {
            get { return _patientReceivingServicesIndicator; }
            private set { }
        }
    }
}