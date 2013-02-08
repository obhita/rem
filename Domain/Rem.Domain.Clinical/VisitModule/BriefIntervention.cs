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

using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// The BriefIntervention is an <see cref="Activity">Activity</see> in the form of
    /// brief counseling for a
    /// <see cref="Patient">Patient</see> in a given <see
    /// cref="Visit">Visit</see>.
    /// </summary>
    public class BriefIntervention : Activity
    {
        private bool? _nutritionCounselingIndicator;
        private bool? _physicalActivityCounselingIndicator;
        private bool? _tobaccoCessationCounselingIndicator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BriefIntervention"/> class.
        /// </summary>
        protected internal BriefIntervention ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BriefIntervention"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal BriefIntervention ( Visit visit, ActivityType activityType )
            : base ( visit, activityType )
        {
        }

        /// <summary>
        /// Gets the nutrition counseling indicator.
        /// </summary>
        public virtual bool? NutritionCounselingIndicator
        {
            get { return _nutritionCounselingIndicator; }
            private set { ApplyPropertyChange ( ref _nutritionCounselingIndicator, () => NutritionCounselingIndicator, value ); }
        }

        /// <summary>
        /// Gets the physical activity counseling indicator.
        /// </summary>
        public virtual bool? PhysicalActivityCounselingIndicator
        {
            get { return _physicalActivityCounselingIndicator; }
            private set { ApplyPropertyChange ( ref _physicalActivityCounselingIndicator, () => PhysicalActivityCounselingIndicator, value ); }
        }

        /// <summary>
        /// Gets the tobacco cessation counseling indicator.
        /// </summary>
        public virtual bool? TobaccoCessationCounselingIndicator
        {
            get { return _tobaccoCessationCounselingIndicator; }
            private set { ApplyPropertyChange ( ref _tobaccoCessationCounselingIndicator, () => TobaccoCessationCounselingIndicator, value ); }
        }

        /// <summary>
        /// Revises the nutrition counseling indicator.
        /// </summary>
        /// <param name="nutritionCounselingIndicator">The nutrition counseling indicator.</param>
        public virtual void ReviseNutritionCounselingIndicator( bool? nutritionCounselingIndicator)
        {
            NutritionCounselingIndicator = nutritionCounselingIndicator;
        }

        /// <summary>
        /// Revises the physical activity counseling indicator.
        /// </summary>
        /// <param name="physicalActivityCounselingIndicator">The physical activity counseling indicator.</param>
        public virtual void RevisePhysicalActivityCounselingIndicator(bool? physicalActivityCounselingIndicator)
        {
            PhysicalActivityCounselingIndicator = physicalActivityCounselingIndicator;
        }

        /// <summary>
        /// Revises the tobacco cessation counseling indicator.
        /// </summary>
        /// <param name="tobaccoCessationCounselingIndicator">The tobacco cessation counseling indicator.</param>
        public virtual void ReviseTobaccoCessationCounselingIndicator(bool? tobaccoCessationCounselingIndicator)
        {
            TobaccoCessationCounselingIndicator = tobaccoCessationCounselingIndicator;
        }
    }
}