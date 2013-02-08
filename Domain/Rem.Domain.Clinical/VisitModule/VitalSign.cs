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
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// VitalSign is an <see cref="Activity">Activity</see> that defines collection of 
    /// Height,  expressed in Feet and inches.
    /// Weight,  expressed in lbs.
    /// Blood Preassure, sis[mmHg], dias[mmHg], List with Timestamp.
    /// Heart Rate [BPM] List with Timestamp.
    /// </summary>
    public class VitalSign : Activity
    {
        private readonly IList<BloodPressure> _bloodPressures;
        private readonly IList<HeartRate> _heartRates;
        private bool? _bmiFollowUpPlanIndicator;
        private bool? _dietaryConsultationOrderIndicator;
        private Height _height;
        private VitalSignPhysicalExamNotDoneReason _vitalSignPhysicalExamNotDoneReason;
        private double? _weightLbsMeasure;

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalSign"/> class.
        /// </summary>
        protected internal VitalSign ()
        {
            _bloodPressures = new List<BloodPressure> ();
            _heartRates = new List<HeartRate> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalSign"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal VitalSign (
            Visit visit,
            ActivityType activityType )
            : base ( visit, activityType )
        {
            _bloodPressures = new List<BloodPressure> ();
            _heartRates = new List<HeartRate> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalSign"/> class.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="provenance">The provenance.</param>
        /// <param name="activityDateTimeRange">The activity date time range.</param>
        protected internal VitalSign(ClinicalCase clinicalCase, ActivityType activityType, Provenance provenance, DateTimeRange activityDateTimeRange)
            : base(clinicalCase, activityType, provenance, activityDateTimeRange)
        {
            _bloodPressures = new List<BloodPressure> ();
            _heartRates = new List<HeartRate> ();
        }

        #region Public Properties

        /// <summary>
        /// Gets the height.
        /// </summary>
        public virtual Height Height
        {
            get { return _height; }
            private set { ApplyPropertyChange ( ref _height, () => Height, value ); }
        }

        /// <summary>
        /// Gets the weight pounds measure.
        /// </summary>
        public virtual double? WeightLbsMeasure
        {
            get { return _weightLbsMeasure; }
            private set { ApplyPropertyChange ( ref _weightLbsMeasure, () => WeightLbsMeasure, value ); }
        }

        /// <summary>
        /// Gets the blood pressures.
        /// </summary>
        public virtual IEnumerable<BloodPressure> BloodPressures
        {
            get { return _bloodPressures.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the heart rates.
        /// </summary>
        public virtual IEnumerable<HeartRate> HeartRates
        {
            get { return _heartRates.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a dietary consultation order.
        /// </summary>
        public virtual bool? DietaryConsultationOrderIndicator
        {
            get { return _dietaryConsultationOrderIndicator; }
            private set { ApplyPropertyChange ( ref _dietaryConsultationOrderIndicator, () => DietaryConsultationOrderIndicator, value ); }
        }

        /// <summary>
        /// Gets a boolean value indicating a body mass index (bmi) follow up plan.
        /// </summary>
        public virtual bool? BmiFollowUpPlanIndicator
        {
            get { return _bmiFollowUpPlanIndicator; }
            private set { ApplyPropertyChange ( ref _bmiFollowUpPlanIndicator, () => BmiFollowUpPlanIndicator, value ); }
        }

        /// <summary>
        /// Gets the vital sign physical exam not done reason.
        /// </summary>
        public virtual VitalSignPhysicalExamNotDoneReason VitalSignPhysicalExamNotDoneReason
        {
            get { return _vitalSignPhysicalExamNotDoneReason; }
            private set { ApplyPropertyChange ( ref _vitalSignPhysicalExamNotDoneReason, () => VitalSignPhysicalExamNotDoneReason, value ); }
        }

        #endregion

        #region Collection Methods

        /// <summary>
        /// Adds the blood pressure.
        /// </summary>
        /// <param name="bloodPressure">The blood pressure.</param>
        public virtual void AddBloodPressure ( BloodPressure bloodPressure )
        {
            Check.IsNotNull ( bloodPressure, "bloodPressure is required." );

            bloodPressure.VitalSign = this;
            _bloodPressures.Add ( bloodPressure );

            NotifyItemAdded ( () => BloodPressures, bloodPressure );
        }

        /// <summary>
        /// Removes the blood pressure.
        /// </summary>
        /// <param name="bloodPressure">The blood pressure.</param>
        public virtual void RemoveBloodPressure ( BloodPressure bloodPressure )
        {
            Check.IsNotNull ( bloodPressure, "bloodPressure is required." );

            _bloodPressures.Delete(bloodPressure);
            NotifyItemRemoved ( () => BloodPressures, bloodPressure );
        }

        /// <summary>
        /// Adds the heart rate.
        /// </summary>
        /// <param name="heartRate">The heart rate.</param>
        public virtual void AddHeartRate ( HeartRate heartRate )
        {
            Check.IsNotNull ( heartRate, "heartRate is required." );

            heartRate.VitalSign = this;
            _heartRates.Add ( heartRate );

            NotifyItemAdded ( () => HeartRates, heartRate );
        }

        /// <summary>
        /// Removes the heart rate.
        /// </summary>
        /// <param name="heartRate">The heart rate.</param>
        public virtual void RemoveHeartRate ( HeartRate heartRate )
        {
            Check.IsNotNull ( heartRate, "heartRate is required." );

            _heartRates.Delete(heartRate);
            NotifyItemRemoved ( () => HeartRates, heartRate );
        }

        #endregion

        /// <summary>
        /// Calculate Body Mass Index
        /// BMI Calculation:
        ///     http://healthcare.nist.gov/docs/170.302.f.2_BMI_v1.0.pdf 
        ///     also: http://www.phaster.com/unpretentious/bmi.html
        /// </summary>
        /// <returns>A double of the body mass index (BMI).</returns>
        public virtual double? CalculateBmi ()
        {
            double totalHeightInInches = Height == null ? 0.0 : Height.GetTotalHeightInInches ();
            double weightInLbs = _weightLbsMeasure == null ? 0 : _weightLbsMeasure.Value;

            double? bmi = null;
            if ( totalHeightInInches != 0 && weightInLbs != 0 )
            {
                bmi = CalculateBmi ( weightInLbs, totalHeightInInches );
            }

            return bmi;
        }

        /// <summary>
        /// Calculates the body mass index (BMI).
        /// </summary>
        /// <param name="weightInLbs">The weight in LBS.</param>
        /// <param name="totalHeightInInches">The total height in inches.</param>
        /// <returns>A double value of the body mass index (BMI).</returns>
        public static double CalculateBmi ( double weightInLbs, double totalHeightInInches )
        {
            return 703 * weightInLbs / Math.Pow ( totalHeightInInches, 2.0 );
        }

        /// <summary>
        /// Revises the height.
        /// </summary>
        /// <param name="height">The height.</param>
        public virtual void ReviseHeight ( Height height )
        {
            Check.IsNotNull ( height, "height is required." );

            Height = height;
        }

        /// <summary>
        /// Revises the weight.
        /// </summary>
        /// <param name="weightInLbs">The weight in LBS.</param>
        public virtual void ReviseWeight ( double? weightInLbs )
        {
            WeightLbsMeasure = weightInLbs;
        }

        /// <summary>
        /// Revises the dietary consultation order indicator.
        /// </summary>
        /// <param name="dietaryConsultationOrderIndicator">The dietary consultation order indicator.</param>
        public virtual void ReviseDietaryConsultationOrderIndicator ( bool? dietaryConsultationOrderIndicator )
        {
            DietaryConsultationOrderIndicator = dietaryConsultationOrderIndicator;
        }

        /// <summary>
        /// Revises the body mass index (BMI) follow up plan indicator.
        /// </summary>
        /// <param name="bmiFollowUpPlanIndicator">The bmi follow up plan indicator.</param>
        public virtual void ReviseBmiFollowUpPlanIndicator ( bool? bmiFollowUpPlanIndicator )
        {
            BmiFollowUpPlanIndicator = bmiFollowUpPlanIndicator;
        }

        /// <summary>
        /// Revises the vital sign physical exam not done reason.
        /// </summary>
        /// <param name="vitalSignPhysicalExamNotDoneReason">The vital sign physical exam not done reason.</param>
        public virtual void ReviseVitalSignPhysicalExamNotDoneReason ( VitalSignPhysicalExamNotDoneReason vitalSignPhysicalExamNotDoneReason )
        {
            VitalSignPhysicalExamNotDoneReason = vitalSignPhysicalExamNotDoneReason;
        }
    }
}