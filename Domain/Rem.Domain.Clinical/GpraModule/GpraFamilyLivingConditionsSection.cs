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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraFamilyLivingConditionsSection contains patient living conditions information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraFamilyLivingConditionsSection : GpraInterviewSectionBase
    {
        private readonly GpraNonResponseType<int?> _childCount;
        private readonly GpraNonResponseType<bool?> _childrenInChildProtectionIndicator;
        private readonly GpraNonResponseType<int?> _childrenInChildProtectionCount;
        private readonly GpraNonResponseType<bool?> _childrenIndicator;
        private readonly GpraNonResponseType<GpraEffectDueToDrugUse> _emotionalProblemsGpraEffectDueToDrugUse;
        private readonly GpraNonResponseType<GpraEffectDueToDrugUse> _giveUpImportantActivitiesGpraEffectDueToDrugUse;
        private readonly GpraNonResponseType<GpraHousingType> _gpraHousingType;
        private readonly GpraNonResponseType<GpraPlaceToLive> _mostTimeGpraPlaceToLive;
        private readonly string _otherHousingTypeSpecificationNote;
        private readonly GpraNonResponseType<int?> _patientLostParentalRightsChildCount;
        private readonly GpraNonResponseType<bool?> _pregnancyIndicator;
        private readonly GpraNonResponseType<GpraEffectDueToDrugUse> _stressGpraEffectDueToDrugUse;

        private GpraFamilyLivingConditionsSection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraFamilyLivingConditionsSection"/> class.
        /// </summary>
        /// <param name="childCount">The child count.</param>
        /// <param name="childrenInChildProtectionIndicator">The children in child protection indicator.</param>
        /// <param name="childrenInChildProtectionCount">The children in child protection count.</param>
        /// <param name="childrenIndicator">The children indicator.</param>
        /// <param name="emotionalProblemsGpraEffectDueToDrugUse">The emotional problems gpra effect due to drug use.</param>
        /// <param name="giveUpImportantActivitiesGpraEffectDueToDrugUse">The give up important activities gpra effect due to drug use.</param>
        /// <param name="gpraHousingType">Type of the gpra housing.</param>
        /// <param name="mostTimeGpraPlaceToLive">The most time gpra place to live.</param>
        /// <param name="otherHousingTypeSpecificationNote">The other housing type specification note.</param>
        /// <param name="patientLostParentalRightsChildCount">The patient lost parental rights child count.</param>
        /// <param name="pregnancyIndicator">The pregnancy indicator.</param>
        /// <param name="stressGpraEffectDueToDrugUse">The stress gpra effect due to drug use.</param>
        public GpraFamilyLivingConditionsSection(GpraNonResponseType<int?> childCount,
                                                 GpraNonResponseType<bool?> childrenInChildProtectionIndicator,
                                                 GpraNonResponseType<int?> childrenInChildProtectionCount,
                                                 GpraNonResponseType<bool?> childrenIndicator,
                                                 GpraNonResponseType<GpraEffectDueToDrugUse> emotionalProblemsGpraEffectDueToDrugUse,
                                                 GpraNonResponseType<GpraEffectDueToDrugUse> giveUpImportantActivitiesGpraEffectDueToDrugUse,
                                                 GpraNonResponseType<GpraHousingType> gpraHousingType,
                                                 GpraNonResponseType<GpraPlaceToLive> mostTimeGpraPlaceToLive,
                                                 string otherHousingTypeSpecificationNote,
                                                 GpraNonResponseType<int?> patientLostParentalRightsChildCount,
                                                 GpraNonResponseType<bool?> pregnancyIndicator,
                                                 GpraNonResponseType<GpraEffectDueToDrugUse> stressGpraEffectDueToDrugUse)
        {
            if (gpraHousingType.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => GpraHousingType).Contains(gpraHousingType.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Gpra Housing Type.", gpraHousingType.GpraNonResponse.Name), "GpraHousingType");
            }
            _childCount = childCount;
            _childrenInChildProtectionIndicator = childrenInChildProtectionIndicator;
            _childrenInChildProtectionCount = childrenInChildProtectionCount;
            _childrenIndicator = childrenIndicator;
            _emotionalProblemsGpraEffectDueToDrugUse = emotionalProblemsGpraEffectDueToDrugUse;
            _giveUpImportantActivitiesGpraEffectDueToDrugUse = giveUpImportantActivitiesGpraEffectDueToDrugUse;
            _gpraHousingType = gpraHousingType;
            _mostTimeGpraPlaceToLive = mostTimeGpraPlaceToLive;
            _otherHousingTypeSpecificationNote = otherHousingTypeSpecificationNote;
            _patientLostParentalRightsChildCount = patientLostParentalRightsChildCount;
            _pregnancyIndicator = pregnancyIndicator;
            _stressGpraEffectDueToDrugUse = stressGpraEffectDueToDrugUse;
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraPlaceToLive">GpraPlaceToLive</see>
        /// denoting
        /// Question 1: In the past 30 days, where have you been living most of the time? [DO NOT READ RESPONSE OPTIONS TO CLIENT.]
        /// </summary>
        public virtual GpraNonResponseType<GpraPlaceToLive> MostTimeGpraPlaceToLive
        {
            get { return _mostTimeGpraPlaceToLive; }
            private set { }
        }

        /// <summary>
        /// Gets the <see cref="T:Rem.Domain.Clinical.GpraModule.GpraHousingType">GpraHousingType</see>
        /// denoting
        /// Question 1(contd): If "Housed"
        /// </summary>
        public virtual GpraNonResponseType<GpraHousingType> GpraHousingType
        {
            get { return _gpraHousingType; }
            private set { }
        }

        /// <summary>
        /// Gets the other housing type specification note.
        /// Question 1(contd): Other Housed (Specify)
        /// </summary>
        public virtual string OtherHousingTypeSpecificationNote
        {
            get { return _otherHousingTypeSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraEffectDueToDrugUse">GpraEffectDueToDrugUse</see>
        /// denoting
        /// Question 2: During the past 30 days, how stressful have things been for you because of your use of alcohol or other drugs? 
        /// </summary>
        public virtual GpraNonResponseType<GpraEffectDueToDrugUse> StressGpraEffectDueToDrugUse
        {
            get { return _stressGpraEffectDueToDrugUse; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraEffectDueToDrugUse">GpraEffectDueToDrugUse</see>
        /// denoting
        /// Question 3: During the past 30 days, has your use of alcohol or other drugs caused you to reduce or give up important activities? 
        /// </summary>
        public virtual GpraNonResponseType<GpraEffectDueToDrugUse> GiveUpImportantActivitiesGpraEffectDueToDrugUse
        {
            get { return _giveUpImportantActivitiesGpraEffectDueToDrugUse; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraEffectDueToDrugUse">GpraEffectDueToDrugUse</see>
        /// denoting
        /// Question 4: During the past 30 days, has your use of alcohol or other drugs caused you to have emotional problems?
        /// </summary>
        public virtual GpraNonResponseType<GpraEffectDueToDrugUse> EmotionalProblemsGpraEffectDueToDrugUse
        {
            get { return _emotionalProblemsGpraEffectDueToDrugUse; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating pregnancy.
        /// Question 5: Are you currently Pregnant?
        /// </summary>
        public virtual GpraNonResponseType<bool?> PregnancyIndicator
        {
            get { return _pregnancyIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient has children.
        /// Question 6: Do you have children? 
        /// </summary>
        public virtual GpraNonResponseType<bool?> ChildrenIndicator
        {
            get { return _childrenIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the child count.
        /// Question 6A: How many children do you have?
        /// </summary>
        public virtual GpraNonResponseType<int?> ChildCount
        {
            get { return _childCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if children are under child protection.
        /// Question 6B: Are any of your children living with someone else due to a child protection court order?
        /// </summary>
        public virtual GpraNonResponseType<bool?> ChildrenInChildProtectionIndicator
        {
            get { return _childrenInChildProtectionIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the children in child protection count.
        /// Question 6C: How many of your children are living with someone else due to a child protection court order?
        /// </summary>
        public virtual GpraNonResponseType<int?> ChildrenInChildProtectionCount
        {
            get { return _childrenInChildProtectionCount; }
            private set { }
        }

        /// <summary>
        /// Gets the patient lost parental rights child count.
        /// Question 6D: For how many of your children have you lost parental rights? [THE CLIENT'S PARENTAL RIGHTS WERE TERMINATED.]
        /// </summary>
        public virtual GpraNonResponseType<int?> PatientLostParentalRightsChildCount
        {
            get { return _patientLostParentalRightsChildCount; }
            private set { }
        }


        /// <summary>
        /// Gets the possible DensAsi non response well known names for this interview section.
        /// <remarks>NotAnswered is included in this base class because it is used in most Nonresponse lists.</remarks>
        /// </summary>
        /// <typeparam name="TProperty">The property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see> list of WellKNownNames.
        /// </returns>
        public override IEnumerable<string> GetPossibleGpraNonResponseWellKnownNames<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            IEnumerable<string> possibleGpraNonResponseWellKnownNames = base.GetPossibleGpraNonResponseWellKnownNames ( propertyExpression );

            return possibleGpraNonResponseWellKnownNames;
        }

        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public override Dictionary<string, IEnumerable<string>> GetFiltersDictionary ()
        {
            return new Dictionary<string, IEnumerable<string>>
                {
                    { PropertyUtil.ExtractPropertyName ( () => GpraHousingType ), GetPossibleGpraNonResponseWellKnownNames ( () => GpraHousingType ) },
                };
        }
    }
}