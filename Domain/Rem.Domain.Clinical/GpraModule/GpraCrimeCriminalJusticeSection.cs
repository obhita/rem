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

using System.Collections.Generic;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraCrimeCriminalJusticeSection contains patient criminal justice information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraCrimeCriminalJusticeSection : GpraInterviewSectionBase
    {
        private GpraNonResponseType<int?> _arrestedCount;
        private GpraNonResponseType<int?> _arrestedDrugCount;
        private GpraNonResponseType<bool?> _awaitingTrialIndicator;
        private GpraNonResponseType<int?> _crimeCount;
        private GpraNonResponseType<int?> _nightsConfinedCount;
        private GpraNonResponseType<bool?> _paroleProbationIndicator;

        private GpraCrimeCriminalJusticeSection() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraCrimeCriminalJusticeSection"/> class.
        /// </summary>
        /// <param name="arrestedCount">The arrested count.</param>
        /// <param name="arrestedDrugCount">The arrested drug count.</param>
        /// <param name="awaitingTrialIndicator">The awaiting trial indicator.</param>
        /// <param name="crimeCount">The crime count.</param>
        /// <param name="nightsConfinedCount">The nights confined count.</param>
        /// <param name="paroleProbationIndicator">The parole probation indicator.</param>
        public GpraCrimeCriminalJusticeSection(GpraNonResponseType<int?> arrestedCount,
                                               GpraNonResponseType<int?> arrestedDrugCount,
                                               GpraNonResponseType<bool?> awaitingTrialIndicator,
                                               GpraNonResponseType<int?> crimeCount,
                                               GpraNonResponseType<int?> nightsConfinedCount,
                                               GpraNonResponseType<bool?> paroleProbationIndicator)
        {
            _arrestedCount = arrestedCount;
            _arrestedDrugCount = arrestedDrugCount;
            _awaitingTrialIndicator = awaitingTrialIndicator;
            _crimeCount = crimeCount;
            _nightsConfinedCount = nightsConfinedCount;
            _paroleProbationIndicator = paroleProbationIndicator;
        }

        /// <summary>
        /// Gets the arrested count.
        /// </summary>
        public virtual GpraNonResponseType<int?> ArrestedCount
        {
            get { return _arrestedCount; }
            private set { }
        }

        /// <summary>
        /// Gets the arrested drug count.
        /// </summary>
        public virtual GpraNonResponseType<int?> ArrestedDrugCount
        {
            get { return _arrestedDrugCount; }
            private set { }
        }

        /// <summary>
        /// Gets the nights confined count.
        /// </summary>
        public virtual GpraNonResponseType<int?> NightsConfinedCount
        {
            get { return _nightsConfinedCount; }
            private set { }
        }

        /// <summary>
        /// Gets the crime count.
        /// </summary>
        public virtual GpraNonResponseType<int?> CrimeCount
        {
            get { return _crimeCount; }
            private set { }
        }

        /// <summary>
        /// Gets the awaiting trial indicator.
        /// </summary>
        public virtual GpraNonResponseType<bool?> AwaitingTrialIndicator
        {
            get { return _awaitingTrialIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the parole probation indicator.
        /// </summary>
        public virtual GpraNonResponseType<bool?> ParoleProbationIndicator
        {
            get { return _paroleProbationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>A Dictionary&lt;string, IEnumerable&lt;string&gt;&gt;</returns>
        public override Dictionary<string, IEnumerable<string>> GetFiltersDictionary()
        {
            return new Dictionary<string, IEnumerable<string>>
                {
                    { PropertyUtil.ExtractPropertyName ( () => ArrestedCount ), GetPossibleGpraNonResponseWellKnownNames ( () => ArrestedCount ) },
                    { PropertyUtil.ExtractPropertyName ( () => ArrestedDrugCount ), GetPossibleGpraNonResponseWellKnownNames ( () => ArrestedDrugCount ) },
                    { PropertyUtil.ExtractPropertyName ( () => NightsConfinedCount ), GetPossibleGpraNonResponseWellKnownNames ( () => NightsConfinedCount ) },
                    { PropertyUtil.ExtractPropertyName ( () => CrimeCount ), GetPossibleGpraNonResponseWellKnownNames ( () => CrimeCount ) },
                    { PropertyUtil.ExtractPropertyName ( () => AwaitingTrialIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => AwaitingTrialIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => ParoleProbationIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => ParoleProbationIndicator ) }
                };
        }
    }
}