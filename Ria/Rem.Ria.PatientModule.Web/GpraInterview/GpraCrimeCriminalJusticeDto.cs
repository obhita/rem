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

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraCrimeCriminalJustice class.
    /// </summary>
    public class GpraCrimeCriminalJusticeDto : GpraDtoBase
    {
        #region Constants and Fields

        private GpraNonResponseTypeDto<int?> _arrestedCount;
        private GpraNonResponseTypeDto<int?> _arrestedDrugCount;
        private GpraNonResponseTypeDto<bool?> _awaitingTrialIndicator;
        private GpraNonResponseTypeDto<int?> _crimeCount;
        private GpraNonResponseTypeDto<int?> _nightsConfinedCount;
        private GpraNonResponseTypeDto<bool?> _paroleProbationIndicator;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the arrested count.
        /// </summary>
        /// <value>The arrested count.</value>
        public GpraNonResponseTypeDto<int?> ArrestedCount
        {
            get { return _arrestedCount; }
            set { ApplyPropertyChange ( ref _arrestedCount, () => ArrestedCount, value ); }
        }

        /// <summary>
        /// Gets or sets the arrested drug count.
        /// </summary>
        /// <value>The arrested drug count.</value>
        public GpraNonResponseTypeDto<int?> ArrestedDrugCount
        {
            get { return _arrestedDrugCount; }
            set { ApplyPropertyChange ( ref _arrestedDrugCount, () => ArrestedCount, value ); }
        }

        /// <summary>
        /// Gets or sets the awaiting trial indicator.
        /// </summary>
        /// <value>The awaiting trial indicator.</value>
        public GpraNonResponseTypeDto<bool?> AwaitingTrialIndicator
        {
            get { return _awaitingTrialIndicator; }
            set { ApplyPropertyChange ( ref _awaitingTrialIndicator, () => AwaitingTrialIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the crime count.
        /// </summary>
        /// <value>The crime count.</value>
        public GpraNonResponseTypeDto<int?> CrimeCount
        {
            get { return _crimeCount; }
            set { ApplyPropertyChange ( ref _crimeCount, () => CrimeCount, value ); }
        }

        /// <summary>
        /// Gets or sets the nights confined count.
        /// </summary>
        /// <value>The nights confined count.</value>
        public GpraNonResponseTypeDto<int?> NightsConfinedCount
        {
            get { return _nightsConfinedCount; }
            set { ApplyPropertyChange ( ref _nightsConfinedCount, () => NightsConfinedCount, value ); }
        }

        /// <summary>
        /// Gets or sets the parole probation indicator.
        /// </summary>
        /// <value>The parole probation indicator.</value>
        public GpraNonResponseTypeDto<bool?> ParoleProbationIndicator
        {
            get { return _paroleProbationIndicator; }
            set { ApplyPropertyChange ( ref _paroleProbationIndicator, () => ParoleProbationIndicator, value ); }
        }

        #endregion
    }
}
