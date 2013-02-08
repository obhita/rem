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

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraFamilyLivingConditions class.
    /// </summary>
    public class GpraFamilyLivingConditionsDto : GpraDtoBase
    {
        #region Constants and Fields

        private GpraNonResponseTypeDto<int?> _childCount;
        private GpraNonResponseTypeDto<bool?> _childrenInChildProtecionIndicator;
        private GpraNonResponseTypeDto<int?> _childrenInChildProtectionCount;
        private GpraNonResponseTypeDto<bool?> _childrenIndicator;
        private GpraNonResponseTypeDto<LookupValueDto> _emotionalProblemsGpraEffectDueToDrugUse;
        private GpraNonResponseTypeDto<LookupValueDto> _giveUpImportantActivitiesGpraEffectDueToDrugUse;
        private GpraNonResponseTypeDto<LookupValueDto> _gpraHousingType;
        private GpraNonResponseTypeDto<LookupValueDto> _mostTimeGpraPlaceToLive;
        private string _otherHousingTypeSpecificationNote;
        private GpraNonResponseTypeDto<int?> _patientLostParentalRightsChildCount;
        private GpraNonResponseTypeDto<bool?> _pregnancyIndicator;
        private GpraNonResponseTypeDto<LookupValueDto> _stressGpraEffectDueToDrugUse;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question 6A: How many children do you have?
        /// </summary>
        /// <value>The child count.</value>
        public GpraNonResponseTypeDto<int?> ChildCount
        {
            get { return _childCount; }
            set { ApplyPropertyChange ( ref _childCount, () => ChildCount, value ); }
        }

        /// <summary>
        /// Question 6C: How many of your children are living with someone else due to a child protection court order?
        /// </summary>
        /// <value>The children in child protection count.</value>
        public GpraNonResponseTypeDto<int?> ChildrenInChildProtectionCount
        {
            get { return _childrenInChildProtectionCount; }
            set { ApplyPropertyChange ( ref _childrenInChildProtectionCount, () => ChildrenInChildProtectionCount, value ); }
        }

        /// <summary>
        /// Question 6B: Are any of your children living with someone else due to a child protection court order?
        /// </summary>
        /// <value>The children in child protection indicator.</value>
        public GpraNonResponseTypeDto<bool?> ChildrenInChildProtectionIndicator
        {
            get { return _childrenInChildProtecionIndicator; }
            set { ApplyPropertyChange ( ref _childrenInChildProtecionIndicator, () => ChildrenInChildProtectionIndicator, value ); }
        }

        /// <summary>
        /// Question 6: Do you have children?
        /// </summary>
        /// <value>The children indicator.</value>
        public GpraNonResponseTypeDto<bool?> ChildrenIndicator
        {
            get { return _childrenIndicator; }
            set { ApplyPropertyChange ( ref _childrenIndicator, () => ChildrenIndicator, value ); }
        }

        /// <summary>
        /// Question 4: During the past 30 days, has your use of alcohol or other drugs caused you to have emotional problems?
        /// </summary>
        /// <value>The emotional problems gpra effect due to drug use.</value>
        public GpraNonResponseTypeDto<LookupValueDto> EmotionalProblemsGpraEffectDueToDrugUse
        {
            get { return _emotionalProblemsGpraEffectDueToDrugUse; }
            set { ApplyPropertyChange ( ref _emotionalProblemsGpraEffectDueToDrugUse, () => EmotionalProblemsGpraEffectDueToDrugUse, value ); }
        }

        /// <summary>
        /// Question 3: During the past 30 days, has your use of alcohol or other drugs caused you to reduce or give up important activities?
        /// </summary>
        /// <value>The give up important activities gpra effect due to drug use.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GiveUpImportantActivitiesGpraEffectDueToDrugUse
        {
            get { return _giveUpImportantActivitiesGpraEffectDueToDrugUse; }
            set
            {
                ApplyPropertyChange (
                    ref _giveUpImportantActivitiesGpraEffectDueToDrugUse, () => GiveUpImportantActivitiesGpraEffectDueToDrugUse, value );
            }
        }

        /// <summary>
        /// Question 1(contd): If "Housed"
        /// </summary>
        /// <value>The type of the gpra housing.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GpraHousingType
        {
            get { return _gpraHousingType; }
            set { ApplyPropertyChange ( ref _gpraHousingType, () => GpraHousingType, value ); }
        }

        /// <summary>
        /// Question 1: In the past 30 days, where have you been living most of the time? [DO NOT READ RESPONSE OPTIONS TO CLIENT.]
        /// </summary>
        /// <value>The most time gpra place to live.</value>
        public GpraNonResponseTypeDto<LookupValueDto> MostTimeGpraPlaceToLive
        {
            get { return _mostTimeGpraPlaceToLive; }
            set { ApplyPropertyChange ( ref _mostTimeGpraPlaceToLive, () => MostTimeGpraPlaceToLive, value ); }
        }

        /// <summary>
        /// Question 1(contd): Other Housed (Specify)
        /// </summary>
        /// <value>The other housing type specification note.</value>
        public string OtherHousingTypeSpecificationNote
        {
            get { return _otherHousingTypeSpecificationNote; }
            set { ApplyPropertyChange ( ref _otherHousingTypeSpecificationNote, () => OtherHousingTypeSpecificationNote, value ); }
        }

        /// <summary>
        /// Question 6D: For how many of your children have you lost parental rights? [THE CLIENT'S PARENTAL RIGHTS WERE TERMINATED.]
        /// </summary>
        /// <value>The patient lost parental rights child count.</value>
        public GpraNonResponseTypeDto<int?> PatientLostParentalRightsChildCount
        {
            get { return _patientLostParentalRightsChildCount; }
            set { ApplyPropertyChange ( ref _patientLostParentalRightsChildCount, () => PatientLostParentalRightsChildCount, value ); }
        }

        /// <summary>
        /// Question 5: Are you currently Pregnant?
        /// </summary>
        /// <value>The pregnancy indicator.</value>
        public GpraNonResponseTypeDto<bool?> PregnancyIndicator
        {
            get { return _pregnancyIndicator; }
            set { ApplyPropertyChange ( ref _pregnancyIndicator, () => PregnancyIndicator, value ); }
        }

        /// <summary>
        /// Question 2: During the past 30 days, how stressful have things been for you because of your use of alcohol or other drugs?
        /// </summary>
        /// <value>The stress gpra effect due to drug use.</value>
        public GpraNonResponseTypeDto<LookupValueDto> StressGpraEffectDueToDrugUse
        {
            get { return _stressGpraEffectDueToDrugUse; }
            set { ApplyPropertyChange ( ref _stressGpraEffectDueToDrugUse, () => StressGpraEffectDueToDrugUse, value ); }
        }

        #endregion
    }
}
