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

using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// AUDIT-C stands for 'Alcohol Use Disorders Identification Test - Consumption'
    /// </summary>
    public class AuditCDto : ActivityDto
    {
        #region Constants and Fields

        private int? _alcoholicDrinksPerDayNumber;
        private int? _auditCScore;
        private int? _howOftenYouDrinkNumber;
        private int? _howOftenYouHaveSixOrMoreDrinksNumber;
        private LookupValueDto _patientGender;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the alcoholic drinks per day number.
        /// </summary>
        /// <value>The alcoholic drinks per day number.</value>
        [DataMember]
        public int? AlcoholicDrinksPerDayNumber
        {
            get { return _alcoholicDrinksPerDayNumber; }
            set { ApplyPropertyChange ( ref _alcoholicDrinksPerDayNumber, () => AlcoholicDrinksPerDayNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the audit C score.
        /// </summary>
        /// <value>The audit C score.</value>
        public virtual int? AuditCScore
        {
            get { return _auditCScore; }
            set { ApplyPropertyChange ( ref _auditCScore, () => AuditCScore, value ); }
        }

        /// <summary>
        /// Gets or sets the how often you drink number.
        /// </summary>
        /// <value>The how often you drink number.</value>
        [DataMember]
        public int? HowOftenYouDrinkNumber
        {
            get { return _howOftenYouDrinkNumber; }
            set { ApplyPropertyChange ( ref _howOftenYouDrinkNumber, () => HowOftenYouDrinkNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the how often you have six or more drinks number.
        /// </summary>
        /// <value>The how often you have six or more drinks number.</value>
        [DataMember]
        public int? HowOftenYouHaveSixOrMoreDrinksNumber
        {
            get { return _howOftenYouHaveSixOrMoreDrinksNumber; }
            set { ApplyPropertyChange ( ref _howOftenYouHaveSixOrMoreDrinksNumber, () => HowOftenYouHaveSixOrMoreDrinksNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the patient gender.
        /// </summary>
        /// <value>The patient gender.</value>
        [DataMember]
        public LookupValueDto PatientGender
        {
            get { return _patientGender; }
            set { ApplyPropertyChange ( ref _patientGender, () => PatientGender, value ); }
        }

        #endregion
    }
}
