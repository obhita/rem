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
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraDischarge class.
    /// </summary>
    public class GpraDischargeDto : GpraDtoBase
    {
        #region Constants and Fields

        private DateTime? _gpraDischargeDate;
        private LookupValueDto _gpraDischargeStatus;
        private string _gpraDischargeStatusOtherDescription;
        private LookupValueDto _gpraDischargeTerminationReason;
        private string _gpraDischargeTerminationReasonOtherDescription;
        private bool? _gpraHivTestIndicator;
        private bool? _gpraReferToHivTestIndicator;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the gpra discharge date.
        /// </summary>
        /// <value>The gpra discharge date.</value>
        public virtual DateTime? GpraDischargeDate
        {
            get { return _gpraDischargeDate; }
            set { ApplyPropertyChange ( ref _gpraDischargeDate, () => GpraDischargeDate, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra discharge status.
        /// </summary>
        /// <value>The gpra discharge status.</value>
        public virtual LookupValueDto GpraDischargeStatus
        {
            get { return _gpraDischargeStatus; }
            set { ApplyPropertyChange ( ref _gpraDischargeStatus, () => GpraDischargeStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra discharge status other description.
        /// </summary>
        /// <value>The gpra discharge status other description.</value>
        public virtual string GpraDischargeStatusOtherDescription
        {
            get { return _gpraDischargeStatusOtherDescription; }
            set { ApplyPropertyChange ( ref _gpraDischargeStatusOtherDescription, () => GpraDischargeStatusOtherDescription, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra discharge termination reason.
        /// </summary>
        /// <value>The gpra discharge termination reason.</value>
        public virtual LookupValueDto GpraDischargeTerminationReason
        {
            get { return _gpraDischargeTerminationReason; }
            set { ApplyPropertyChange ( ref _gpraDischargeTerminationReason, () => GpraDischargeTerminationReason, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra discharge termination reason other description.
        /// </summary>
        /// <value>The gpra discharge termination reason other description.</value>
        public virtual string GpraDischargeTerminationReasonOtherDescription
        {
            get { return _gpraDischargeTerminationReasonOtherDescription; }
            set
            {
                ApplyPropertyChange (
                    ref _gpraDischargeTerminationReasonOtherDescription, () => GpraDischargeTerminationReasonOtherDescription, value );
            }
        }

        /// <summary>
        /// Gets or sets the gpra hiv test indicator.
        /// </summary>
        /// <value>The gpra hiv test indicator.</value>
        public virtual bool? GpraHivTestIndicator
        {
            get { return _gpraHivTestIndicator; }
            set { ApplyPropertyChange ( ref _gpraHivTestIndicator, () => GpraHivTestIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra refer to hiv test indicator.
        /// </summary>
        /// <value>The gpra refer to hiv test indicator.</value>
        public virtual bool? GpraReferToHivTestIndicator
        {
            get { return _gpraReferToHivTestIndicator; }
            set { ApplyPropertyChange ( ref _gpraReferToHivTestIndicator, () => GpraReferToHivTestIndicator, value ); }
        }

        #endregion
    }
}
