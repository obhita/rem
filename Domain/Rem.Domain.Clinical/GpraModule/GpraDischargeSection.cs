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

using System;
using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraDischargeSection contains patient discharge information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraDischargeSection : GpraInterviewSectionBase
    {
        private readonly DateTime? _gpraDischargeDate;
        private readonly GpraDischargeStatus _gpraDischargeStatus;
        private readonly string _gpraDischargeStatusOtherDescription;
        private readonly GpraDischargeTerminationReason _gpraDischargeTerminationReason;
        private readonly string _gpraDischargeTerminationReasonOtherDescription;
        private readonly bool? _gpraHivTestIndicator;
        private readonly bool? _gpraReferToHivTestIndicator;

        private GpraDischargeSection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraDischargeSection"/> class.
        /// </summary>
        /// <param name="gpraDischargeDate">The gpra discharge date.</param>
        /// <param name="gpraDischargeStatus">The gpra discharge status.</param>
        /// <param name="gpraDischargeStatusOtherDescription">The gpra discharge status other description.</param>
        /// <param name="gpraDischargeTerminationReason">The gpra discharge termination reason.</param>
        /// <param name="gpraDischargeTerminationReasonOtherDescription">The gpra discharge termination reason other description.</param>
        /// <param name="gpraHivTestIndicator">The gpra hiv test indicator.</param>
        /// <param name="gpraReferToHivTestIndicator">The gpra refer to hiv test indicator.</param>
        public GpraDischargeSection(DateTime? gpraDischargeDate,
                                    GpraDischargeStatus gpraDischargeStatus,
                                    string gpraDischargeStatusOtherDescription,
                                    GpraDischargeTerminationReason gpraDischargeTerminationReason,
                                    string gpraDischargeTerminationReasonOtherDescription,
                                    bool? gpraHivTestIndicator,
                                    bool? gpraReferToHivTestIndicator)
        {
            _gpraDischargeDate = gpraDischargeDate;
            _gpraDischargeStatus = gpraDischargeStatus;
            _gpraDischargeStatusOtherDescription = gpraDischargeStatusOtherDescription;
            _gpraDischargeTerminationReason = gpraDischargeTerminationReason;
            _gpraDischargeTerminationReasonOtherDescription = gpraDischargeTerminationReasonOtherDescription;
            _gpraHivTestIndicator = gpraHivTestIndicator;
            _gpraReferToHivTestIndicator = gpraReferToHivTestIndicator;
        }

        /// <summary>
        /// Gets the Gpra discharge termination reason other description.
        /// </summary>
        public virtual string GpraDischargeTerminationReasonOtherDescription
        {
            get { return _gpraDischargeTerminationReasonOtherDescription; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra discharge termination reason.
        /// </summary>
        public virtual GpraDischargeTerminationReason GpraDischargeTerminationReason
        {
            get { return _gpraDischargeTerminationReason; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra discharge date.
        /// </summary>
        public virtual DateTime? GpraDischargeDate
        {
            get { return _gpraDischargeDate; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra discharge status.
        /// </summary>
        public virtual GpraDischargeStatus GpraDischargeStatus
        {
            get { return _gpraDischargeStatus; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra discharge status other description.
        /// </summary>
        public virtual string GpraDischargeStatusOtherDescription
        {
            get { return _gpraDischargeStatusOtherDescription; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating an HIV test.
        /// </summary>
        public virtual bool? GpraHivTestIndicator
        {
            get { return _gpraHivTestIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating refer to HIV test.
        /// </summary>
        public virtual bool? GpraReferToHivTestIndicator
        {
            get { return _gpraReferToHivTestIndicator; }
            private set { }
        }
    }
}