// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaffCollegeDegree.cs" company="">
//   
// </copyright>
// <summary>
//   The StaffCollegeDegree defines a college degree that is held by a staff.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The StaffCollegeDegree defines a college degree that is held by a staff.
    /// </summary>
    public class StaffCollegeDegree : StaffAggregateNodeBase, IAggregateNodeValueObject
    {
        private CollegeDegree _collegeDegree;
        private DateTime? _earnedDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffCollegeDegree"/> class.
        /// </summary>
        protected internal StaffCollegeDegree ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffCollegeDegree"/> class.
        /// </summary>
        /// <param name="collegeDegree">
        /// The college degree.
        /// </param>
        /// <param name="earnedDate">
        /// The earned date.
        /// </param>
        public StaffCollegeDegree ( CollegeDegree collegeDegree, DateTime? earnedDate )
        {
            Check.IsNotNull ( collegeDegree, () => CollegeDegree );
            _collegeDegree = collegeDegree;
            _earnedDate = earnedDate;
        }

        /// <summary>
        /// Gets the earned date.
        /// </summary>
        public virtual DateTime? EarnedDate
        {
            get { return _earnedDate; }
            private set { ApplyPropertyChange ( ref _earnedDate, () => EarnedDate, value ); }
        }

        /// <summary>
        /// Gets the college degree.
        /// </summary>
        [NotNull]
        public virtual CollegeDegree CollegeDegree
        {
            get
            {
                return _collegeDegree;
            }

            private set
            {
                Check.IsNotNull ( value, () => CollegeDegree );
                ApplyPropertyChange ( ref _collegeDegree, () => CollegeDegree, value );
            }
        }

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="staffCollegeDegree">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>
        public virtual bool ValuesEqual ( StaffCollegeDegree staffCollegeDegree )
        {
            if ( staffCollegeDegree == null )
            {
                return false;
            }

            var valuesEqual = Equals ( _collegeDegree.Key, staffCollegeDegree._collegeDegree.Key )
                              && Equals ( _earnedDate, staffCollegeDegree._earnedDate );
            return valuesEqual;
        }
    }
}
