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
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for VitalSign class.
    /// </summary>
    public class VitalSignDto : ActivityDto
    {
        #region Constants and Fields

        private SoftDeleteObservableCollection<BloodPressureDto> _bloodPressures;
        private bool? _bmiFollowUpPlanIndicator;
        private bool? _dietaryConsultationOrderIndicator;
        private SoftDeleteObservableCollection<HeartRateDto> _heartRates;
        private int? _heightFeetMeasure;
        private double? _heightInchesMeasure;
        private LookupValueDto _vitalSignPhysicalExamNotDoneReason;
        private double? _weightLbsMeasure;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalSignDto"/> class.
        /// </summary>
        public VitalSignDto ()
        {
            _bloodPressures = new SoftDeleteObservableCollection<BloodPressureDto> ();
            _heartRates = new SoftDeleteObservableCollection<HeartRateDto> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the blood pressures.
        /// </summary>
        /// <value>The blood pressures.</value>
        [DataMember]
        public SoftDeleteObservableCollection<BloodPressureDto> BloodPressures
        {
            get { return _bloodPressures; }
            set { ApplyPropertyChange ( ref _bloodPressures, () => BloodPressures, value ); }
        }

        /// <summary>
        /// Gets or sets the bmi follow up plan indicator.
        /// </summary>
        /// <value>The bmi follow up plan indicator.</value>
        [DataMember]
        public bool? BmiFollowUpPlanIndicator
        {
            get { return _bmiFollowUpPlanIndicator; }
            set { ApplyPropertyChange ( ref _bmiFollowUpPlanIndicator, () => BmiFollowUpPlanIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the dietary consultation order indicator.
        /// </summary>
        /// <value>The dietary consultation order indicator.</value>
        [DataMember]
        public bool? DietaryConsultationOrderIndicator
        {
            get { return _dietaryConsultationOrderIndicator; }
            set { ApplyPropertyChange ( ref _dietaryConsultationOrderIndicator, () => DietaryConsultationOrderIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the heart rates.
        /// </summary>
        /// <value>The heart rates.</value>
        [DataMember]
        public SoftDeleteObservableCollection<HeartRateDto> HeartRates
        {
            get { return _heartRates; }
            set { ApplyPropertyChange ( ref _heartRates, () => HeartRates, value ); }
        }

        /// <summary>
        /// Gets or sets the height feet measure.
        /// </summary>
        /// <value>The height feet measure.</value>
        [DataMember]
        public int? HeightFeetMeasure
        {
            get { return _heightFeetMeasure; }
            set { ApplyPropertyChange ( ref _heightFeetMeasure, () => HeightFeetMeasure, value ); }
        }

        /// <summary>
        /// Gets or sets the height inches measure.
        /// </summary>
        /// <value>The height inches measure.</value>
        [DataMember]
        public double? HeightInchesMeasure
        {
            get { return _heightInchesMeasure; }
            set { ApplyPropertyChange ( ref _heightInchesMeasure, () => HeightInchesMeasure, value ); }
        }

        /// <summary>
        /// Gets or sets the vital sign physical exam not done reason.
        /// </summary>
        /// <value>The vital sign physical exam not done reason.</value>
        [DataMember]
        public LookupValueDto VitalSignPhysicalExamNotDoneReason
        {
            get { return _vitalSignPhysicalExamNotDoneReason; }
            set { ApplyPropertyChange ( ref _vitalSignPhysicalExamNotDoneReason, () => VitalSignPhysicalExamNotDoneReason, value ); }
        }

        /// <summary>
        /// Gets or sets the weight LBS measure.
        /// </summary>
        /// <value>The weight LBS measure.</value>
        [DataMember]
        public double? WeightLbsMeasure
        {
            get { return _weightLbsMeasure; }
            set { ApplyPropertyChange ( ref _weightLbsMeasure, () => WeightLbsMeasure, value ); }
        }

        #endregion
    }
}
