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

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// HeartRate is a vital sign that defines the number of heartbeats per unit of time, typically expressed as beats per minute (bpm).
    /// </summary>
    public class HeartRate : VitalSignAggregateNodeBase, IAggregateNodeValueObject
    {
        private int _beatsPerMinuteMeasure;
        private DateTimeOffset _effectiveTimestamp;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HeartRate"/> class.
        /// </summary>
        protected internal HeartRate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeartRate"/> class.
        /// </summary>
        /// <param name="beatsPerMinuteMeasure">The beats per minute measure.</param>
        public HeartRate ( int beatsPerMinuteMeasure )
        {
            _beatsPerMinuteMeasure = beatsPerMinuteMeasure;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the beats per minute measure.
        /// </summary>
        public virtual int BeatsPerMinuteMeasure
        {
            get { return _beatsPerMinuteMeasure; }
            private set { ApplyPropertyChange ( ref _beatsPerMinuteMeasure, () => BeatsPerMinuteMeasure, value ); }
        }

        /// <summary>
        /// Gets the effective timestamp.
        /// </summary>
        public virtual DateTimeOffset EffectiveTimestamp
        {
            get { return _effectiveTimestamp; }
            private set { ApplyPropertyChange ( ref _effectiveTimestamp, () => EffectiveTimestamp, value ); }
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format ( "{0} beats per minute", BeatsPerMinuteMeasure );
        }
    }
}