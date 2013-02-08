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
using Pillar.Common.Metadata;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.DensAsiInterview;
using Rem.WellKnownNames.DensAsiModule;

namespace Rem.Ria.PatientModule.DensAsiInterview
{
    /// <summary>
    /// Contains client side business rules for DensAsi Interview.
    /// </summary>
    public class DensAsiInterviewViewModelRuleCollection : AbstractRuleCollection<DensAsiInterviewViewModel>
    {
        #region Constants and Fields

        private const string LessThan30ErrorMsg = "Enter a number between 0 and 30.";
        private const string LessThan99999ErrorMsg = "The value must be less than or equal 99999.";
        private const string LessThan99ErrorMsg = "The value must be less than or equal 99.";
        private const string LessThan99YearsErrorMsg = "The year and month combination cannot exceed 99 years and 11 months.";
        private const double TimeSpanUpperLimitDays = 36469.587; // 99 years and 11 months
        private static readonly TimeSpan TimeSpanUpperLimit = new TimeSpan ( Convert.ToInt32 ( Math.Round ( TimeSpanUpperLimitDays, 0 ) ), 0, 0, 0 );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiInterviewViewModelRuleCollection"/> class.
        /// </summary>
        public DensAsiInterviewViewModelRuleCollection ()
        {
            BuildDensAsiClosureRules ();
            BuildDensAsiDrugAlcoholUseRules ();
            BuildDensAsiDsmIvRules ();
            BuildDensAsiEmploymentStatusRules ();
            BuildDensAsiFamilySocialRelationshipsRules ();
            BuildDensAsiLegalStatusRules ();
            BuildDensAsiMedicalStatusRules ();
            BuildDensAsiPatientProfileRules ();
            BuildDensAsiPsychiatricStatusRules ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// D19. Gets or sets the alcohol abuse treatment count less than99 rule.
        /// </summary>
        /// <value>
        /// The alcohol abuse treatment count less than99 rule.
        /// </value>
        public IPropertyRule AlcoholAbuseTreatmentCountLessThan99Rule { get; set; }

        /// <summary>
        /// D21. Gets or sets the alcohol detox treatment only count less than99 rule.
        /// </summary>
        /// <value>
        /// The alcohol detox treatment only count less than99 rule.
        /// </value>
        public IPropertyRule AlcoholDetoxTreatmentOnlyCountLessThan99Rule { get; set; }

        /// <summary>
        /// D21. Gets or sets the alcohol detox treatment only count must be zero rule.
        /// </summary>
        /// <remarks>If D19 = 0, then D21 is N</remarks>
        /// <value>
        /// The alcohol detox treatment only count must be N rule.
        /// </value>
        public IRule AlcoholDetoxTreatmentOnlyCountMustBeNRule { get; set; }

        /// <summary>
        /// D17.  Gets or sets the alcohol dt count less than99 rule.
        /// </summary>
        /// <value>
        /// The alcohol dt count less than 99 rule.
        /// </value>
        public IPropertyRule AlcoholDtCountLessThan99Rule { get; set; }

        /// <summary>
        /// D2. Gets or sets the alcohol intoxication in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The alcohol intoxication in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule AlcoholIntoxicationInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D26. Gets or sets the alcohol problem in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The alcohol problem in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule AlcoholProblemInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D9. Gets or sets the amphetamines in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The amphetamines in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule AmphetaminesInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D1. Gets or sets any alcohol use in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// Any alcohol use in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule AnyAlcoholUseInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// L17. Gets or sets the arrest charges resulted in convictions count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrest charges resulted in convictions count less than99 rule.
        /// </value>
        public IPropertyRule ArrestChargesResultedInConvictionsCountLessThan99Rule { get; set; }

        /// <summary>
        /// L11. Gets or sets the arrested charged arson count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged arson count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedArsonCountLessThan99Rule { get; set; }

        /// <summary>
        /// L10. Gets or sets the arrested charged assault count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged assault count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedAssaultCountLessThan99Rule { get; set; }

        /// <summary>
        /// L8. Gets or sets the arrested charged burglary larcency count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged burglary larcency count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedBurglaryLarcencyCountLessThan99Rule { get; set; }

        /// <summary>
        /// L15. Gets or sets the arrested charged contempt of count count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged contempt of count count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedContemptOfCountCountLessThan99Rule { get; set; }

        /// <summary>
        /// L5. Gets or sets the arrested charged drug charges count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged drug charges count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedDrugChargesCountLessThan99Rule { get; set; }

        /// <summary>
        /// L6. Gets or sets the arrested charged forgery count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged forgery count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedForgeryCountLessThan99Rule { get; set; }

        /// <summary>
        /// L13. Gets or sets the arrested charged homicide manslaughter count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged homicide manslaughter count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedHomicideManslaughterCountLessThan99Rule { get; set; }

        /// <summary>
        /// L16. Gets or sets the arrested charged other count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged other count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedOtherCountLessThan99Rule { get; set; }

        /// <summary>
        /// L4. Gets or sets the arrested charged probation parole violation count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged probation parole violation count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedProbationParoleViolationCountLessThan99Rule { get; set; }

        /// <summary>
        /// L14. Gets or sets the arrested charged prostitution count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged prostitution count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedProstitutionCountLessThan99Rule { get; set; }

        /// <summary>
        /// L12. Gets or sets the arrested charged rape count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged rape count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedRapeCountLessThan99Rule { get; set; }

        /// <summary>
        /// L9. Gets or sets the arrested charged robbery count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged robbery count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedRobberyCountLessThan99Rule { get; set; }

        /// <summary>
        /// L3. Gets or sets the arrested charged shoplifting count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged shoplifting count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedShopliftingCountLessThan99Rule { get; set; }

        /// <summary>
        /// L7. Gets or sets the arrested charged weapons offense count less than99 rule.
        /// </summary>
        /// <value>
        /// The arrested charged weapons offense count less than99 rule.
        /// </value>
        public IPropertyRule ArrestedChargedWeaponsOffenseCountLessThan99Rule { get; set; }

        /// <summary>
        /// E5.Gets or sets the automobile availablefor use indicator must be no rule.
        /// </summary>
        /// <remarks> If E4 = No, then E5 must be No.</remarks>
        /// <value>
        /// The automobile availablefor use indicator must be no rule.
        /// </value>
        public IRule AutomobileAvailableforUseIndicatorMustBeNoRule { get; set; }

        /// <summary>
        /// D6. Gets or sets the barbiturates in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The barbiturates in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule BarbituratesInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D10. Gets or sets the cannabis in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The cannabis in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule CannabisInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// L18. Gets or sets the charged with disorderly conduct count less than99 rule.
        /// </summary>
        /// <value>
        /// The charged with disorderly conduct count less than99 rule.
        /// </value>
        public IPropertyRule ChargedWithDisorderlyConductCountLessThan99Rule { get; set; }

        /// <summary>
        /// L19. Gets or sets the charged with driving while intoxicated count less than99 rule.
        /// </summary>
        /// <value>
        /// The charged with driving while intoxicated count less than99 rule.
        /// </value>
        public IPropertyRule ChargedWithDrivingWhileIntoxicatedCountLessThan99Rule { get; set; }

        /// <summary>
        /// L20. Gets or sets the charged with major driving violations count less than99 rule.
        /// </summary>
        /// <value>
        /// The charged with major driving violations count less than99 rule.
        /// </value>
        public IPropertyRule ChargedWithMajorDrivingViolationsCountLessThan99Rule { get; set; }

        /// <summary>
        /// F11. Gets or sets the close friends count less than9 rule.
        /// </summary>
        /// <value>
        /// The close friends count less than9 rule.
        /// </value>
        public IPropertyRule CloseFriendsCountLessThan9Rule { get; set; }

        /// <summary>
        /// D8. Gets or sets the cocaine in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The cocaine in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule CocaineInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// F31. Gets or sets the conflicts with others in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The conflicts with others in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule ConflictsWithOthersInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// E9. Gets or sets the contribution constitute majority of your support indicator rule.
        /// </summary>
        /// <remarks>If E8 is No, then E9 cannot be Yes. If E8 is Yes, then E9 must be Yes or No.</remarks>
        /// <value>
        /// The contribution constitute majority of your support indicator rule.
        /// </value>
        public IRule ContributionConstituteMajorityOfYourSupportIndicatorRule { get; set; }

        /// <summary>
        /// E8. Gets or sets the contribution of someone to support indicator must be yes rule.
        /// </summary>
        /// <remarks>If E16 > 0, then E8 must be Yes.</remarks>
        /// <value>
        /// The contribution of someone to support indicator must be yes rule.
        /// </value>
        public IRule ContributionOfSomeoneToSupportIndicatorMustBeYesRule { get; set; }

        /// <summary>
        /// E18. Gets or sets the dependent people count less than9 rule.
        /// </summary>
        /// <value>
        /// The dependent people count less than9 rule.
        /// </value>
        public IPropertyRule DependentPeopleCountLessThan9Rule { get; set; }

        /// <summary>
        /// D20. Gets or sets the drug abuse treatment count less than99 rule.
        /// </summary>
        /// <value>
        /// The drug abuse treatment count less than99 rule.
        /// </value>
        public IPropertyRule DrugAbuseTreatmentCountLessThan99Rule { get; set; }

        /// <summary>
        /// D22. Gets or sets the drug detox treatment only count less than99 rule.
        /// </summary>
        /// <value>
        /// The drug detox treatment only count less than99 rule.
        /// </value>
        public IPropertyRule DrugDetoxTreatmentOnlyCountLessThan99Rule { get; set; }

        /// <summary>
        /// D22. Gets or sets the drug detox treatment only count must be N rule.
        /// </summary>
        /// <remarks>If D20 = 0, then D22 must be N.</remarks>
        /// <value>
        /// The drug detox treatment only count must be N rule.
        /// </value>
        public IRule DrugDetoxTreatmentOnlyCountMustBeNRule { get; set; }

        /// <summary>
        /// D27. Gets or sets the drug problem in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The drug problem in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule DrugProblemInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// E19. Gets or sets the employment problems day count great than30 rule.
        /// </summary>
        /// <value>
        /// The employment problems day count great than30 rule.
        /// </value>
        public IPropertyRule EmploymentProblemsDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D16. Gets or sets the end of problematic substance abstinence month count must be N rule.
        /// </summary>
        /// <remarks>If D15 = 0, then D16 = N.</remarks>
        /// <value>
        /// The end of problematic substance abstinence month count must be N rule.
        /// </value>
        public IRule EndOfProblematicSubstanceAbstinenceMonthCountMustBeNRule { get; set; }

        /// <summary>
        /// D11. Gets or sets the hallucinogens in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The hallucinogens in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule HallucinogensInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D3. Gets or sets the heroin in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The heroin in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule HeroinInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// M1. Gets or sets the hopitalized for medical problems count zero rule.
        /// </summary>
        /// <remarks> If M1 = 0, then M2 must be N/A.</remarks>
        /// <value>
        /// The hopitalized for medical problems count zero rule.
        /// </value>
        public IRule HopitalizedForMedicalProblemsCountZeroRule { get; set; }

        /// <summary>
        /// L27. Gets or sets the illegal activity in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The illegal activity in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule IllegalActivityInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// E17. Gets or sets the illegal amount less than99999 rule.
        /// </summary>
        /// <value>
        /// The illegal amount less than99999 rule.
        /// </value>
        public IPropertyRule IllegalAmountLessThan99999Rule { get; set; }

        /// <summary>
        /// L26. Gets or sets the incarcerated in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The incarcerated in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule IncarceratedInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// L21. Gets or sets the incarceration in life month count less than99 rule.
        /// </summary>
        /// <value>
        /// The incarceration in life month count less than99 rule.
        /// </value>
        public IPropertyRule IncarcerationInLifeMonthCountLessThan99Rule { get; set; }

        /// <summary>
        /// D12. Gets or sets the inhalants in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The inhalants in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule InhalantsInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// L22. Gets or sets the last incarceration length month count less than99 rule.
        /// </summary>
        /// <value>
        /// The last incarceration length month count less than99 rule.
        /// </value>
        public IPropertyRule LastIncarcerationLengthMonthCountLessThan99Rule { get; set; }

        /// <summary>
        /// G20 .Gets or sets the last thirty days controlled environment day count less than30 rule.
        /// </summary>
        /// <value>
        /// The last thirty days controlled environment day count less than30 rule.
        /// </value>
        public IPropertyRule LastThirtyDaysControlledEnvironmentDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// G20. Gets or sets the last thirty days controlled environment day count must be N rule.
        /// </summary>
        /// <remarks>G20 must be N if G19 is No.</remarks>
        /// <value>
        /// The last thirty days controlled environment day count must be N rule.
        /// </value>
        public IRule LastThirtyDaysControlledEnvironmentDayCountMustBeNRule { get; set; }

        /// <summary>
        /// E16. Gets or sets the mate family friends amount less than99999 rule.
        /// </summary>
        /// <value>
        /// The mate family friends amount less than99999 rule.
        /// </value>
        public IPropertyRule MateFamilyFriendsAmountLessThan99999Rule { get; set; }

        /// <summary>
        /// M6 and M7: Gets or sets the medical problems day count cannot be zero rule.
        /// </summary>
        /// <remarks>If M6 = 0, M7 cannot > 0.</remarks>
        /// <value>
        /// The medical problems day count cannot be zero rule.
        /// </value>
        public IRule MedicalProblemsDayCountCannotBeZeroRule { get; set; }

        /// <summary>
        /// M6. Gets or sets the medical problems day count great than30 rule.
        /// </summary>
        /// <value>
        /// The medical problems day count great than30 rule.
        /// </value>
        public IPropertyRule MedicalProblemsDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D4. Gets or sets the methadone in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The methadone in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule MethadoneInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D23. Gets or sets the money spent on alcohol in last thirty days amount less than9999 rule.
        /// </summary>
        /// <value>
        /// The money spent on alcohol in last thirty days amount less than9999 rule.
        /// </value>
        public IPropertyRule MoneySpentOnAlcoholInLastThirtyDaysAmountLessThan99999Rule { get; set; }

        /// <summary>
        /// D24. Gets or sets the money spent on drugs in last thirty days amount less than99999 rule.
        /// </summary>
        /// <value>
        /// The money spent on drugs in last thirty days amount less than99999 rule.
        /// </value>
        public IPropertyRule MoneySpentOnDrugsInLastThirtyDaysAmountLessThan99999Rule { get; set; }

        /// <summary>
        /// D13. Gets or sets the more than one substance per day in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The more than one substance per day in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule MoreThanOneSubstancePerDayInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// E12. Gets or sets the net income amount less than 99999 rule.
        /// </summary>
        /// <value>
        /// The net income amount less than99999 rule.
        /// </value>
        public IPropertyRule NetIncomeAmountLessThan99999Rule { get; set; }

        /// <summary>
        /// G101 -G103. Gets or sets the othe religion rule.
        /// </summary>
        /// <remarks>Only when G18 is Yes, then one of G101 to G103 can be Yes. </remarks>
        /// <value>
        /// The othe religion rule.
        /// </value>
        public IRule OtheReligionRule { get; set; }

        /// <summary>
        /// D5. Gets or sets the other opiates in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The other opiates in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule OtherOpiatesInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D7. Gets or sets the other sedatives in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The other sedatives in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule OtherSedativesInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D25. Gets or sets the outpatient treatment in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The outpatient treatment in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule OutpatientTreatmentInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// D18. Gets or sets the overdosed on drugs count less than99 rule.
        /// </summary>
        /// <value>
        /// The overdosed on drugs count less than99 rule.
        /// </value>
        public IPropertyRule OverdosedOnDrugsCountLessThan99Rule { get; set; }

        /// <summary>
        /// E15. Gets or sets the pension benefits social security amount cannot be zero rule.
        /// </summary>
        /// <remarks>E15 cannot be zero if M5 is Yes.</remarks>
        /// <value>
        /// The pension benefits social security amount cannot be zero rule.
        /// </value>
        public IRule PensionBenefitsSocialSecurityAmountCannotBeZeroRule { get; set; }

        /// <summary>
        /// E15. Gets or sets the pension benefits social security amount less than99999 rule.
        /// </summary>
        /// <value>
        /// The pension benefits social security amount less than99999 rule.
        /// </value>
        public IPropertyRule PensionBenefitsSocialSecurityAmountLessThan99999Rule { get; set; }

        /// <summary>
        /// P12. Gets or sets the psychological problems last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The psychological problems last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule PsychologicalProblemsLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// P2. Gets or sets the psychological treatment as outpatient count less than99 rule.
        /// </summary>
        /// <value>
        /// The psychological treatment as outpatient count less than99 rule.
        /// </value>
        public IPropertyRule PsychologicalTreatmentAsOutpatientCountLessThan99Rule { get; set; }

        /// <summary>
        /// P1. Gets or sets the psychological treatment in hospital count less than99 rule.
        /// </summary>
        /// <value>
        /// The psychological treatment in hospital count less than99 rule.
        /// </value>
        public IPropertyRule PsychologicalTreatmentInHospitalCountLessThan99Rule { get; set; }

        /// <summary>
        /// F30. Gets or sets the serious family conflicts in last thirty days day count less than30 rule.
        /// </summary>
        /// <value>
        /// The serious family conflicts in last thirty days day count less than30 rule.
        /// </value>
        public IPropertyRule SeriousFamilyConflictsInLastThirtyDaysDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// E13. Gets or sets the unemployment compensation amount less than 99999 rule.
        /// </summary>
        /// <value>
        /// The unemployment compensation amount less than99999 rule.
        /// </value>
        public IPropertyRule UnemploymentCompensationAmountLessThan99999Rule { get; set; }

        /// <summary>
        /// E14. Gets or sets the welfare amount less than99999 rule.
        /// </summary>
        /// <value>
        /// The welfare amount less than99999 rule.
        /// </value>
        public IPropertyRule WelfareAmountLessThan99999Rule { get; set; }

        /// <summary>
        /// E11. Gets or sets the work in last thirty days paid day count less than 30 rule.
        /// </summary>
        /// <value>
        /// The work in last thirty days paid day count less than 30.
        /// </value>
        public IPropertyRule WorkInLastThirtyDaysPaidDayCountLessThan30Rule { get; set; }

        /// <summary>
        /// M2. Gets or sets the years and months after last hospitalization for physical problem time span less than99 years rule.
        /// </summary>
        /// <value>
        /// The years and months after last hospitalization for physical problem time span less than99 years rule.
        /// </value>
        public IRule YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanLessThan99YearsRule { get; set; }

        /// <summary>
        /// G14. Gets or sets the years and months at current address time span less than99 years rule.
        /// </summary>
        /// <value>
        /// The years and months at current address time span less than99 years rule.
        /// </value>
        public IRule YearsAndMonthsAtCurrentAddressTimeSpanLessThan99YearsRule { get; set; }

        /// <summary>
        /// E1. Gets or sets the years and months education completed time span less than99 years rule.
        /// </summary>
        /// <value>
        /// The years and months education completed time span less than99 years rule.
        /// </value>
        public IRule YearsAndMonthsEducationCompletedTimeSpanLessThan99YearsRule { get; set; }

        /// <summary>
        /// F5. Gets or sets the years and months in living arrangement type time span less than99 years rule.
        /// </summary>
        /// <value>
        /// The years and months in living arrangement type time span less than99 years rule.
        /// </value>
        public IRule YearsAndMonthsInLivingArrangementTypeTimeSpanLessThan99YearsRule { get; set; }

        /// <summary>
        /// E6. Gets or sets the years and months of longest full time job time span less than99 years rule.
        /// </summary>
        /// <value>
        /// The years and months of longest full time job time span less than99 years rule.
        /// </value>
        public IRule YearsAndMonthsOfLongestFullTimeJobTimeSpanLessThan99YearsRule { get; set; }

        /// <summary>
        /// F2. Gets or sets the years and months with marital status time span for single rule.
        /// </summary>
        /// <remarks>If F1 = Never married - single, F2 should have the number of years from age 18.</remarks>
        /// <value>
        /// The years and months with marital status time span for single rule.
        /// </value>
        public IRule YearsAndMonthsWithMaritalStatusTimeSpanForSingleRule { get; set; }

        /// <summary>
        /// F2. Gets or sets the years and months with marital status time span less than99 years rule.
        /// </summary>
        /// <value>
        /// The years and months with marital status time span less than99 years rule.
        /// </value>
        public IRule YearsAndMonthsWithMaritalStatusTimeSpanLessThan99YearsRule { get; set; }

        #endregion

        #region Methods

        private static bool TimeSpanOverflow ( DensAsiNonResponseTypeDto<TimeSpan?> timeSpan )
        {
            return timeSpan.HasValue() && timeSpan > TimeSpanUpperLimit;
        }

        private void BuildDensAsiClosureRules ()
        {
        }

        private void BuildDensAsiDrugAlcoholUseRules ()
        {
            var drugDetoxTreatmentOnlyCountMustBeNError = new DataErrorInfo (
                "D22 must be N/A if D20 = 0.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.DrugDetoxTreatmentOnlyCount ) );

            NewRule ( () => DrugDetoxTreatmentOnlyCountMustBeNRule )
                .RunForProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.DrugDetoxTreatmentOnlyCount )
                .RunForProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.DrugAbuseTreatmentCount )
                .When (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.DrugAbuseTreatmentCount.Value == 0 &&
                         ( s.EditingDto.DensAsiDrugAlcoholUse.DrugDetoxTreatmentOnlyCount.NonResponse == null ||
                           s.EditingDto.DensAsiDrugAlcoholUse.DrugDetoxTreatmentOnlyCount.NonResponse.WellKnownName
                           != DensAsiNonResponse.NotApplicable )
                )
                .ThenReportRuleViolation ( drugDetoxTreatmentOnlyCountMustBeNError.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( drugDetoxTreatmentOnlyCountMustBeNError ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( drugDetoxTreatmentOnlyCountMustBeNError ) );

            var alcoholDetoxTreatmentOnlyCountMustBeNError = new DataErrorInfo (
                "D21 must be N/A if D19 = 0.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.AlcoholDetoxTreatmentOnlyCount ) );

            NewRule ( () => AlcoholDetoxTreatmentOnlyCountMustBeNRule )
                .RunForProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AlcoholDetoxTreatmentOnlyCount )
                .RunForProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AlcoholAbuseTreatmentCount )
                .When (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.AlcoholAbuseTreatmentCount.Value == 0 &&
                         ( s.EditingDto.DensAsiDrugAlcoholUse.AlcoholDetoxTreatmentOnlyCount.NonResponse == null ||
                           s.EditingDto.DensAsiDrugAlcoholUse.AlcoholDetoxTreatmentOnlyCount.NonResponse.WellKnownName
                           != DensAsiNonResponse.NotApplicable )
                )
                .ThenReportRuleViolation ( alcoholDetoxTreatmentOnlyCountMustBeNError.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( alcoholDetoxTreatmentOnlyCountMustBeNError ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( alcoholDetoxTreatmentOnlyCountMustBeNError ) );

            var endOfProblematicSubstanceAbstinenceMonthCountMustBeNError = new DataErrorInfo (
                "D16 must be either N/A or 0 if D15 = 0.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.EndOfProblematicSubstanceAbstinenceMonthCount ) );

            NewRule ( () => EndOfProblematicSubstanceAbstinenceMonthCountMustBeNRule )
                .RunForProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.EndOfProblematicSubstanceAbstinenceMonthCount )
                .RunForProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.VoluntaryAbstinenceFromProblematicSubstanceMonthCount )
                .When (
                    s =>
                        {
                            if ( s.EditingDto.DensAsiDrugAlcoholUse.VoluntaryAbstinenceFromProblematicSubstanceMonthCount.Value == 0 )
                            {
                                if ( !( ( s.EditingDto.DensAsiDrugAlcoholUse.EndOfProblematicSubstanceAbstinenceMonthCount.NonResponse != null &&
                                          s.EditingDto.DensAsiDrugAlcoholUse.EndOfProblematicSubstanceAbstinenceMonthCount.NonResponse.WellKnownName == DensAsiNonResponse.NotApplicable )
                                        ||
                                        s.EditingDto.DensAsiDrugAlcoholUse.EndOfProblematicSubstanceAbstinenceMonthCount.Value == 0 ) )
                                {
                                    return true;
                                }
                            }
                            return false;
                        } )
                .ThenReportRuleViolation ( endOfProblematicSubstanceAbstinenceMonthCountMustBeNError.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( endOfProblematicSubstanceAbstinenceMonthCountMustBeNError ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( endOfProblematicSubstanceAbstinenceMonthCountMustBeNError ) );

            var alcoholDtCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.AlcoholDtCount ) );

            NewPropertyRule ( () => AlcoholDtCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AlcoholDtCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( alcoholDtCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( alcoholDtCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( alcoholDtCountLessThan99Error ) );

            var overdosedOnDrugsCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.OverdosedOnDrugsCount ) );

            NewPropertyRule ( () => OverdosedOnDrugsCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.OverdosedOnDrugsCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( overdosedOnDrugsCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( overdosedOnDrugsCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( overdosedOnDrugsCountLessThan99Error ) );

            var alcoholAbuseTreatmentCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.AlcoholAbuseTreatmentCount ) );

            NewPropertyRule ( () => AlcoholAbuseTreatmentCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AlcoholAbuseTreatmentCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( alcoholAbuseTreatmentCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( alcoholAbuseTreatmentCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( alcoholAbuseTreatmentCountLessThan99Error ) );

            var alcoholDetoxTreatmentOnlyCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.AlcoholDetoxTreatmentOnlyCount ) );

            NewPropertyRule ( () => AlcoholDetoxTreatmentOnlyCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AlcoholDetoxTreatmentOnlyCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( alcoholDetoxTreatmentOnlyCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( alcoholDetoxTreatmentOnlyCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( alcoholDetoxTreatmentOnlyCountLessThan99Error ) );

            var moneySpentOnAlcoholInLastThirtyDaysAmountLessThan99999Error = new DataErrorInfo (
                LessThan99999ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.MoneySpentOnAlcoholInLastThirtyDaysAmount ) );

            NewPropertyRule ( () => MoneySpentOnAlcoholInLastThirtyDaysAmountLessThan99999Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.MoneySpentOnAlcoholInLastThirtyDaysAmount )
                .GreaterThan ( 99999 )
                .ThenReportRuleViolation ( moneySpentOnAlcoholInLastThirtyDaysAmountLessThan99999Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( moneySpentOnAlcoholInLastThirtyDaysAmountLessThan99999Error ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( moneySpentOnAlcoholInLastThirtyDaysAmountLessThan99999Error ) );

            var drugAbuseTreatmentCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.DrugAbuseTreatmentCount ) );

            NewPropertyRule ( () => DrugAbuseTreatmentCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.DrugAbuseTreatmentCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( drugAbuseTreatmentCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( drugAbuseTreatmentCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( drugAbuseTreatmentCountLessThan99Error ) );

            var drugDetoxTreatmentOnlyCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.DrugDetoxTreatmentOnlyCount ) );

            NewPropertyRule ( () => DrugDetoxTreatmentOnlyCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.DrugDetoxTreatmentOnlyCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( drugDetoxTreatmentOnlyCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( drugDetoxTreatmentOnlyCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( drugDetoxTreatmentOnlyCountLessThan99Error ) );

            var moneySpentOnDrugsInLastThirtyDaysAmountLessThan99999Error = new DataErrorInfo (
                LessThan99999ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.MoneySpentOnDrugsInLastThirtyDaysAmount ) );

            NewPropertyRule ( () => MoneySpentOnDrugsInLastThirtyDaysAmountLessThan99999Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.MoneySpentOnDrugsInLastThirtyDaysAmount )
                .GreaterThan ( 99999 )
                .ThenReportRuleViolation ( moneySpentOnDrugsInLastThirtyDaysAmountLessThan99999Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( moneySpentOnDrugsInLastThirtyDaysAmountLessThan99999Error ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( moneySpentOnDrugsInLastThirtyDaysAmountLessThan99999Error ) );

            var anyAlcoholUseInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.AnyAlcoholUseInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => AnyAlcoholUseInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AnyAlcoholUseInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( anyAlcoholUseInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( anyAlcoholUseInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( anyAlcoholUseInLastThirtyDaysDayCountLessThan30Error ) );

            var alcoholIntoxicationInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.AlcoholIntoxicationInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => AlcoholIntoxicationInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AlcoholIntoxicationInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( alcoholIntoxicationInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( alcoholIntoxicationInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( alcoholIntoxicationInLastThirtyDaysDayCountLessThan30Error ) );

            var heroinInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.HeroinInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => HeroinInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.HeroinInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( heroinInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( heroinInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( heroinInLastThirtyDaysDayCountLessThan30Error ) );

            var methadoneInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.MethadoneInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => MethadoneInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.MethadoneInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( methadoneInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( methadoneInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( methadoneInLastThirtyDaysDayCountLessThan30Error ) );

            var otherOpiatesInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.OtherOpiatesInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => OtherOpiatesInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.OtherOpiatesInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( otherOpiatesInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( otherOpiatesInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( otherOpiatesInLastThirtyDaysDayCountLessThan30Error ) );

            var barbituratesInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.BarbituratesInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => BarbituratesInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.BarbituratesInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( barbituratesInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( barbituratesInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( barbituratesInLastThirtyDaysDayCountLessThan30Error ) );

            var otherSedativesInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.OtherSedativesInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => OtherSedativesInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.OtherSedativesInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( otherSedativesInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( otherSedativesInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( otherSedativesInLastThirtyDaysDayCountLessThan30Error ) );

            var cocaineInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.CocaineInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => CocaineInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.CocaineInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( cocaineInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( cocaineInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( cocaineInLastThirtyDaysDayCountLessThan30Error ) );

            var amphetaminesInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.AmphetaminesInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => AmphetaminesInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AmphetaminesInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( amphetaminesInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( amphetaminesInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( amphetaminesInLastThirtyDaysDayCountLessThan30Error ) );

            var cannabisInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.CannabisInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => CannabisInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.CannabisInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( cannabisInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( cannabisInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( cannabisInLastThirtyDaysDayCountLessThan30Error ) );

            var hallucinogensInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.HallucinogensInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => HallucinogensInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.HallucinogensInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( hallucinogensInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( hallucinogensInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( hallucinogensInLastThirtyDaysDayCountLessThan30Error ) );

            var inhalantsInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.InhalantsInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => InhalantsInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.InhalantsInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( inhalantsInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( inhalantsInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( inhalantsInLastThirtyDaysDayCountLessThan30Error ) );

            var moreThanOneSubstancePerDayInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.MoreThanOneSubstancePerDayInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => MoreThanOneSubstancePerDayInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.MoreThanOneSubstancePerDayInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( moreThanOneSubstancePerDayInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( moreThanOneSubstancePerDayInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( moreThanOneSubstancePerDayInLastThirtyDaysDayCountLessThan30Error ) );

            var outpatientTreatmentInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.OutpatientTreatmentInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => OutpatientTreatmentInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.OutpatientTreatmentInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( outpatientTreatmentInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( outpatientTreatmentInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( outpatientTreatmentInLastThirtyDaysDayCountLessThan30Error ) );

            var alcoholProblemInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.AlcoholProblemInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => AlcoholProblemInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.AlcoholProblemInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( alcoholProblemInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( alcoholProblemInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( alcoholProblemInLastThirtyDaysDayCountLessThan30Error ) );

            var drugProblemInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiDrugAlcoholUseDto, object> ( dto => dto.DrugProblemInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => DrugProblemInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiDrugAlcoholUse.DrugProblemInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( drugProblemInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiDrugAlcoholUse.TryAddDataErrorInfo ( drugProblemInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiDrugAlcoholUse.RemoveDataErrorInfo ( drugProblemInLastThirtyDaysDayCountLessThan30Error ) );
        }

        private void BuildDensAsiDsmIvRules ()
        {
        }

        private void BuildDensAsiEmploymentStatusRules ()
        {
            var pensionBenefitsSocialSecurityAmountCannotBeZeroError = new DataErrorInfo (
                "E15 cannot be zero if M5 is Yes.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.PensionBenefitsSocialSecurityAmount ) );

            NewRule ( () => PensionBenefitsSocialSecurityAmountCannotBeZeroRule )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.PensionBenefitsSocialSecurityAmount )
                .RunForProperty ( s => s.EditingDto.DensAsiMedicalStatus.ReceivePensionForPhysicalDisabilityIndicator )
                .When (
                    s =>
                    s.EditingDto.DensAsiMedicalStatus.ReceivePensionForPhysicalDisabilityIndicator.Value == true &&
                    ( s.EditingDto.DensAsiEmploymentStatus.PensionBenefitsSocialSecurityAmount.Value == null ||
                      s.EditingDto.DensAsiEmploymentStatus.PensionBenefitsSocialSecurityAmount.Value == 0 )
                )
                .ThenReportRuleViolation ( pensionBenefitsSocialSecurityAmountCannotBeZeroError.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( pensionBenefitsSocialSecurityAmountCannotBeZeroError ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( pensionBenefitsSocialSecurityAmountCannotBeZeroError ) );

            var contributionConstituteMajorityOfYourSupportIndicatorError = new DataErrorInfo (
                "If E8 is No, then E9 cannot be Yes. If E8 is Yes, then E9 must be Yes or No.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> (dto => dto.ContributionConstituteMajorityOfYourSupportIndicator ),
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.ContributionOfSomeoneToSupportIndicator ) );

            NewRule ( () => ContributionConstituteMajorityOfYourSupportIndicatorRule )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.ContributionConstituteMajorityOfYourSupportIndicator )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.ContributionOfSomeoneToSupportIndicator )
                .When (
                    s =>
                    ( s.EditingDto.DensAsiEmploymentStatus.ContributionOfSomeoneToSupportIndicator.Value == false &&
                      s.EditingDto.DensAsiEmploymentStatus.ContributionConstituteMajorityOfYourSupportIndicator.Value == true )
                    ||
                    ( s.EditingDto.DensAsiEmploymentStatus.ContributionOfSomeoneToSupportIndicator.Value == true &&
                      s.EditingDto.DensAsiEmploymentStatus.ContributionConstituteMajorityOfYourSupportIndicator.Value == null
                    ) )
                .ThenReportRuleViolation ( contributionConstituteMajorityOfYourSupportIndicatorError.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( contributionConstituteMajorityOfYourSupportIndicatorError ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( contributionConstituteMajorityOfYourSupportIndicatorError ) );

            var netIncomeAmountLessThan99999Error = new DataErrorInfo (
                LessThan99999ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.NetIncomeAmount ) );

            NewPropertyRule ( () => NetIncomeAmountLessThan99999Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.NetIncomeAmount )
                .GreaterThan ( 99999 )
                .ThenReportRuleViolation ( netIncomeAmountLessThan99999Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( netIncomeAmountLessThan99999Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( netIncomeAmountLessThan99999Error ) );

            var unemploymentCompensationAmountLessThan99999Error = new DataErrorInfo (
                LessThan99999ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.UnemploymentCompensationAmount ) );

            NewPropertyRule ( () => UnemploymentCompensationAmountLessThan99999Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.UnemploymentCompensationAmount )
                .GreaterThan ( 99999 )
                .ThenReportRuleViolation ( unemploymentCompensationAmountLessThan99999Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( unemploymentCompensationAmountLessThan99999Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( unemploymentCompensationAmountLessThan99999Error ) );

            var welfareAmountLessThan99999Error = new DataErrorInfo (
                LessThan99999ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.WelfareAmount ) );

            NewPropertyRule ( () => WelfareAmountLessThan99999Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.WelfareAmount )
                .GreaterThan ( 99999 )
                .ThenReportRuleViolation ( welfareAmountLessThan99999Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( welfareAmountLessThan99999Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( welfareAmountLessThan99999Error ) );

            var pensionBenefitsSocialSecurityAmountLessThan99999Error = new DataErrorInfo (
                LessThan99999ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.PensionBenefitsSocialSecurityAmount ) );

            NewPropertyRule ( () => PensionBenefitsSocialSecurityAmountLessThan99999Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.PensionBenefitsSocialSecurityAmount )
                .GreaterThan ( 99999 )
                .ThenReportRuleViolation ( pensionBenefitsSocialSecurityAmountLessThan99999Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( pensionBenefitsSocialSecurityAmountLessThan99999Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( pensionBenefitsSocialSecurityAmountLessThan99999Error ) );

            var mateFamilyFriendsAmountLessThan99999Error = new DataErrorInfo (
                LessThan99999ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.MateFamilyFriendsAmount ) );

            NewPropertyRule ( () => MateFamilyFriendsAmountLessThan99999Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.MateFamilyFriendsAmount )
                .GreaterThan ( 99999 )
                .ThenReportRuleViolation ( mateFamilyFriendsAmountLessThan99999Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( mateFamilyFriendsAmountLessThan99999Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( mateFamilyFriendsAmountLessThan99999Error ) );

            var illegalAmountLessThan99999Error = new DataErrorInfo (
                LessThan99999ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.IllegalAmount ) );

            NewPropertyRule ( () => IllegalAmountLessThan99999Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.IllegalAmount )
                .GreaterThan ( 99999 )
                .ThenReportRuleViolation ( illegalAmountLessThan99999Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( illegalAmountLessThan99999Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( illegalAmountLessThan99999Error ) );

            var dependentPeopleCountLessThan9Error = new DataErrorInfo (
                "The amount must less than 9.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.DependentPeopleCount ) );

            NewPropertyRule ( () => DependentPeopleCountLessThan9Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.DependentPeopleCount )
                .GreaterThan ( 9 )
                .ThenReportRuleViolation ( dependentPeopleCountLessThan9Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( dependentPeopleCountLessThan9Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( dependentPeopleCountLessThan9Error ) );

            var automobileAvailableforUseIndicatorMustBeNoError = new DataErrorInfo (
                "E5 must be No if E4 is No.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.AutomobileAvailableforUseIndicator ) );

            NewRule ( () => AutomobileAvailableforUseIndicatorMustBeNoRule )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.AutomobileAvailableforUseIndicator )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.ValidDriversLicenseIndicator )
                .When (
                    s =>
                    s.EditingDto.DensAsiEmploymentStatus.ValidDriversLicenseIndicator.Value == false
                    && s.EditingDto.DensAsiEmploymentStatus.AutomobileAvailableforUseIndicator.Value != false )
                .ThenReportRuleViolation ( automobileAvailableforUseIndicatorMustBeNoError.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( automobileAvailableforUseIndicatorMustBeNoError ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( automobileAvailableforUseIndicatorMustBeNoError ) );

            var contributionOfSomeoneToSupportIndicatorMustBeYesError = new DataErrorInfo (
                "E8 must be Yes if E16 great than zero.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.ContributionOfSomeoneToSupportIndicator ) );

            NewRule ( () => ContributionOfSomeoneToSupportIndicatorMustBeYesRule )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.ContributionOfSomeoneToSupportIndicator )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.MateFamilyFriendsAmount )
                .When (
                    s =>
                    s.EditingDto.DensAsiEmploymentStatus.ContributionOfSomeoneToSupportIndicator.Value != true
                    && s.EditingDto.DensAsiEmploymentStatus.MateFamilyFriendsAmount.Value > 0 )
                .ThenReportRuleViolation ( contributionOfSomeoneToSupportIndicatorMustBeYesError.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( contributionOfSomeoneToSupportIndicatorMustBeYesError ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( contributionOfSomeoneToSupportIndicatorMustBeYesError ) );

            var yearsAndMonthsEducationCompletedTimeSpanLessThan99YearsError = new DataErrorInfo (
                LessThan99YearsErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.YearsAndMonthsEducationCompletedTimeSpan ) );

            NewRule ( () => YearsAndMonthsEducationCompletedTimeSpanLessThan99YearsRule )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.YearsAndMonthsEducationCompletedTimeSpan )
                .When ( s => TimeSpanOverflow ( s.EditingDto.DensAsiEmploymentStatus.YearsAndMonthsEducationCompletedTimeSpan ) )
                .ThenReportRuleViolation ( yearsAndMonthsEducationCompletedTimeSpanLessThan99YearsError.Message )
                .Then (
                    s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( yearsAndMonthsEducationCompletedTimeSpanLessThan99YearsError ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( yearsAndMonthsEducationCompletedTimeSpanLessThan99YearsError ) );

            var yearsAndMonthsOfLongestFullTimeJobTimeSpanLessThan99YearsError = new DataErrorInfo (
                LessThan99YearsErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.YearsAndMonthsOfLongestFullTimeJobTimeSpan ) );

            NewRule ( () => YearsAndMonthsOfLongestFullTimeJobTimeSpanLessThan99YearsRule )
                .RunForProperty ( s => s.EditingDto.DensAsiEmploymentStatus.YearsAndMonthsOfLongestFullTimeJobTimeSpan )
                .When ( s => TimeSpanOverflow ( s.EditingDto.DensAsiEmploymentStatus.YearsAndMonthsOfLongestFullTimeJobTimeSpan ) )
                .ThenReportRuleViolation ( yearsAndMonthsOfLongestFullTimeJobTimeSpanLessThan99YearsError.Message )
                .Then (
                    s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( yearsAndMonthsOfLongestFullTimeJobTimeSpanLessThan99YearsError ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( yearsAndMonthsOfLongestFullTimeJobTimeSpanLessThan99YearsError ) );

            var workInLastThirtyDaysPaidDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.WorkInLastThirtyDaysPaidDayCount ) );

            NewPropertyRule ( () => WorkInLastThirtyDaysPaidDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.WorkInLastThirtyDaysPaidDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( workInLastThirtyDaysPaidDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( workInLastThirtyDaysPaidDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( workInLastThirtyDaysPaidDayCountLessThan30Error ) );

            var employmentProblemsDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiEmploymentStatusDto, object> ( dto => dto.EmploymentProblemsDayCount ) );

            NewPropertyRule ( () => EmploymentProblemsDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiEmploymentStatus.EmploymentProblemsDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( employmentProblemsDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiEmploymentStatus.TryAddDataErrorInfo ( employmentProblemsDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiEmploymentStatus.RemoveDataErrorInfo ( employmentProblemsDayCountLessThan30Error ) );
        }

        private void BuildDensAsiFamilySocialRelationshipsRules ()
        {
            var seriousFamilyConflictsInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiFamilySocialRelationshipsDto, object> (
                    dto => dto.SeriousFamilyConflictsInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => SeriousFamilyConflictsInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiFamilySocialRelationships.SeriousFamilyConflictsInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( seriousFamilyConflictsInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then (
                    s =>
                    s.EditingDto.DensAsiFamilySocialRelationships.TryAddDataErrorInfo (
                        seriousFamilyConflictsInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen (
                    s =>
                    s.EditingDto.DensAsiFamilySocialRelationships.RemoveDataErrorInfo (
                        seriousFamilyConflictsInLastThirtyDaysDayCountLessThan30Error ) );

            var conflictsWithOthersInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiFamilySocialRelationshipsDto, object> (
                    dto => dto.ConflictsWithOthersInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => ConflictsWithOthersInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiFamilySocialRelationships.ConflictsWithOthersInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( conflictsWithOthersInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then (
                    s =>
                    s.EditingDto.DensAsiFamilySocialRelationships.TryAddDataErrorInfo ( conflictsWithOthersInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen (
                    s =>
                    s.EditingDto.DensAsiFamilySocialRelationships.RemoveDataErrorInfo ( conflictsWithOthersInLastThirtyDaysDayCountLessThan30Error ) );

            var yearsAndMonthsWithMaritalStatusTimeSpanLessThan99YearsError = new DataErrorInfo (
                LessThan99YearsErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiFamilySocialRelationshipsDto, object> ( dto => dto.YearsAndMonthsWithMaritalStatusTimeSpan ) );

            NewRule ( () => YearsAndMonthsWithMaritalStatusTimeSpanLessThan99YearsRule )
                .RunForProperty ( s => s.EditingDto.DensAsiFamilySocialRelationships.YearsAndMonthsWithMaritalStatusTimeSpan )
                .When (
                    s => TimeSpanOverflow ( s.EditingDto.DensAsiFamilySocialRelationships.YearsAndMonthsWithMaritalStatusTimeSpan ) )
                .ThenReportRuleViolation ( yearsAndMonthsWithMaritalStatusTimeSpanLessThan99YearsError.Message )
                .Then (
                    s =>
                    s.EditingDto.DensAsiFamilySocialRelationships.TryAddDataErrorInfo ( yearsAndMonthsWithMaritalStatusTimeSpanLessThan99YearsError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.DensAsiFamilySocialRelationships.RemoveDataErrorInfo ( yearsAndMonthsWithMaritalStatusTimeSpanLessThan99YearsError ) );

            var yearsAndMonthsInLivingArrangementTypeTimeSpanLessThan99YearsError = new DataErrorInfo (
                LessThan99YearsErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiFamilySocialRelationshipsDto, object> (
                    dto => dto.YearsAndMonthsInLivingArrangementTypeTimeSpan ) );

            NewRule ( () => YearsAndMonthsInLivingArrangementTypeTimeSpanLessThan99YearsRule )
                .RunForProperty ( s => s.EditingDto.DensAsiFamilySocialRelationships.YearsAndMonthsInLivingArrangementTypeTimeSpan )
                .When (
                    s => TimeSpanOverflow ( s.EditingDto.DensAsiFamilySocialRelationships.YearsAndMonthsInLivingArrangementTypeTimeSpan ) )
                .ThenReportRuleViolation ( yearsAndMonthsInLivingArrangementTypeTimeSpanLessThan99YearsError.Message )
                .Then (
                    s =>
                    s.EditingDto.DensAsiFamilySocialRelationships.TryAddDataErrorInfo (
                        yearsAndMonthsInLivingArrangementTypeTimeSpanLessThan99YearsError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.DensAsiFamilySocialRelationships.RemoveDataErrorInfo (
                        yearsAndMonthsInLivingArrangementTypeTimeSpanLessThan99YearsError ) );

            var errorMsg =
                "If F1 = Never married - single, the number of years from age 18 should entered in F2, which should be {0} year(s) and {1} month(s).";
            var yearsAndMonthsWithMaritalStatusTimeSpanForSingleError = new DataErrorInfo (
                errorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiFamilySocialRelationshipsDto, object> (
                    dto => dto.YearsAndMonthsWithMaritalStatusTimeSpan ) );

            NewRule ( () => YearsAndMonthsWithMaritalStatusTimeSpanForSingleRule )
                .RunForProperty ( s => s.EditingDto.DensAsiFamilySocialRelationships.DensAsiMaritalStatus )
                .RunForProperty ( s => s.EditingDto.DensAsiFamilySocialRelationships.YearsAndMonthsWithMaritalStatusTimeSpan )
                .When (
                    s =>
                        {
                            if ( s.EditingDto.DensAsiFamilySocialRelationships.DensAsiMaritalStatus.HasValue () &&
                                 s.EditingDto.DensAsiFamilySocialRelationships.DensAsiMaritalStatus.Value.WellKnownName
                                 == DensAsiMaritalStatus.NeverMarriedSingle &&
                                 s.EditingDto.DensAsiPatientProfile.BirthDate.HasValue )
                            {
                                var yearsSince18 = DateTime.Now - s.EditingDto.DensAsiPatientProfile.BirthDate.Value.AddYears ( -18 );
                                var msg = string.Format (
                                    errorMsg, 
                                    ( int )( Math.Floor ( yearsSince18.TotalDays / 365 ) ), 
                                    Math.Round ( ( yearsSince18.TotalDays % 365 ) / 30 ), 
                                    0 );
                                yearsAndMonthsWithMaritalStatusTimeSpanForSingleError.Message = msg;

                                // No value
                                if ( !s.EditingDto.DensAsiFamilySocialRelationships.YearsAndMonthsWithMaritalStatusTimeSpan.HasValue () )
                                {
                                    return true;
                                }

                                // Wrong value
                                var oneMonth = new TimeSpan ( 30, 0, 0, 0 ); // Give a month buffer
                                var value = s.EditingDto.DensAsiFamilySocialRelationships.YearsAndMonthsWithMaritalStatusTimeSpan.Value.Value;
                                if ( yearsSince18.Subtract ( value ) > oneMonth || value.Subtract ( yearsSince18 ) > oneMonth )
                                {
                                    return true;
                                }
                            }
                            return false;
                        }
                )
                .ThenReportRuleViolation ( yearsAndMonthsWithMaritalStatusTimeSpanForSingleError.Message )
                .Then (
                    s => s.EditingDto.DensAsiFamilySocialRelationships.TryAddDataErrorInfo ( yearsAndMonthsWithMaritalStatusTimeSpanForSingleError ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiFamilySocialRelationships.RemoveDataErrorInfo ( yearsAndMonthsWithMaritalStatusTimeSpanForSingleError ) );

            var closeFriendsCountLessThan9Error = new DataErrorInfo (
                "The value must be less than or equal 9.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiFamilySocialRelationshipsDto, object> ( dto => dto.CloseFriendsCount ) );

            NewPropertyRule ( () => CloseFriendsCountLessThan9Rule )
                .WithProperty ( s => s.EditingDto.DensAsiFamilySocialRelationships.CloseFriendsCount )
                .GreaterThan ( 9 )
                .ThenReportRuleViolation ( closeFriendsCountLessThan9Error.Message )
                .Then ( s => s.EditingDto.DensAsiFamilySocialRelationships.TryAddDataErrorInfo ( closeFriendsCountLessThan9Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiFamilySocialRelationships.RemoveDataErrorInfo ( closeFriendsCountLessThan9Error ) );
        }

        private void BuildDensAsiLegalStatusRules ()
        {
            var arrestedChargedShopliftingCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedShopliftingCount ) );

            NewPropertyRule ( () => ArrestedChargedShopliftingCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedShopliftingCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedShopliftingCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedShopliftingCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedShopliftingCountLessThan99Error ) );

            var arrestedChargedProbationParoleViolationCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedProbationParoleViolationCount ) );

            NewPropertyRule ( () => ArrestedChargedProbationParoleViolationCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedProbationParoleViolationCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedProbationParoleViolationCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedProbationParoleViolationCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedProbationParoleViolationCountLessThan99Error ) );

            var arrestedChargedDrugChargesCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedDrugChargesCount ) );

            NewPropertyRule ( () => ArrestedChargedDrugChargesCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedDrugChargesCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedDrugChargesCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedDrugChargesCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedDrugChargesCountLessThan99Error ) );

            var arrestedChargedForgeryCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedForgeryCount ) );

            NewPropertyRule ( () => ArrestedChargedForgeryCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedForgeryCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedForgeryCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedForgeryCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedForgeryCountLessThan99Error ) );

            var arrestedChargedWeaponsOffenseCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedWeaponsOffenseCount ) );

            NewPropertyRule ( () => ArrestedChargedWeaponsOffenseCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedWeaponsOffenseCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedWeaponsOffenseCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedWeaponsOffenseCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedWeaponsOffenseCountLessThan99Error ) );

            var arrestedChargedBurglaryLarcencyCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedBurglaryLarcencyCount ) );

            NewPropertyRule ( () => ArrestedChargedBurglaryLarcencyCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedBurglaryLarcencyCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedBurglaryLarcencyCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedBurglaryLarcencyCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedBurglaryLarcencyCountLessThan99Error ) );

            var arrestedChargedRobberyCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedRobberyCount ) );

            NewPropertyRule ( () => ArrestedChargedRobberyCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedRobberyCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedRobberyCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedRobberyCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedRobberyCountLessThan99Error ) );

            var arrestedChargedAssaultCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedAssaultCount ) );

            NewPropertyRule ( () => ArrestedChargedAssaultCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedAssaultCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedAssaultCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedAssaultCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedAssaultCountLessThan99Error ) );

            var arrestedChargedArsonCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedArsonCount ) );

            NewPropertyRule ( () => ArrestedChargedArsonCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedArsonCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedArsonCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedArsonCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedArsonCountLessThan99Error ) );

            var arrestedChargedRapeCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedRapeCount ) );

            NewPropertyRule ( () => ArrestedChargedRapeCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedRapeCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedRapeCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedRapeCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedRapeCountLessThan99Error ) );

            var arrestedChargedHomicideManslaughterCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedHomicideManslaughterCount ) );

            NewPropertyRule ( () => ArrestedChargedHomicideManslaughterCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedHomicideManslaughterCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedHomicideManslaughterCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedHomicideManslaughterCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedHomicideManslaughterCountLessThan99Error ) );

            var arrestedChargedProstitutionCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedProstitutionCount ) );

            NewPropertyRule ( () => ArrestedChargedProstitutionCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedProstitutionCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedProstitutionCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedProstitutionCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedProstitutionCountLessThan99Error ) );

            var arrestedChargedContemptOfCountCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedContemptOfCountCount ) );

            NewPropertyRule ( () => ArrestedChargedContemptOfCountCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedContemptOfCountCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedContemptOfCountCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedContemptOfCountCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedContemptOfCountCountLessThan99Error ) );

            var arrestedChargedOtherCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestedChargedOtherCount ) );

            NewPropertyRule ( () => ArrestedChargedOtherCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestedChargedOtherCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestedChargedOtherCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestedChargedOtherCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestedChargedOtherCountLessThan99Error ) );

            var arrestChargesResultedInConvictionsCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ArrestChargesResultedInConvictionsCount ) );

            NewPropertyRule ( () => ArrestChargesResultedInConvictionsCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ArrestChargesResultedInConvictionsCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( arrestChargesResultedInConvictionsCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( arrestChargesResultedInConvictionsCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( arrestChargesResultedInConvictionsCountLessThan99Error ) );

            var chargedWithDisorderlyConductCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ChargedWithDisorderlyConductCount ) );

            NewPropertyRule ( () => ChargedWithDisorderlyConductCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ChargedWithDisorderlyConductCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( chargedWithDisorderlyConductCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( chargedWithDisorderlyConductCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( chargedWithDisorderlyConductCountLessThan99Error ) );

            var chargedWithDrivingWhileIntoxicatedCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ChargedWithDrivingWhileIntoxicatedCount ) );

            NewPropertyRule ( () => ChargedWithDrivingWhileIntoxicatedCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ChargedWithDrivingWhileIntoxicatedCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( chargedWithDrivingWhileIntoxicatedCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( chargedWithDrivingWhileIntoxicatedCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( chargedWithDrivingWhileIntoxicatedCountLessThan99Error ) );

            var chargedWithMajorDrivingViolationsCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.ChargedWithMajorDrivingViolationsCount ) );

            NewPropertyRule ( () => ChargedWithMajorDrivingViolationsCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.ChargedWithMajorDrivingViolationsCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( chargedWithMajorDrivingViolationsCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( chargedWithMajorDrivingViolationsCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( chargedWithMajorDrivingViolationsCountLessThan99Error ) );

            var incarcerationInLifeMonthCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.IncarcerationInLifeMonthCount ) );

            NewPropertyRule ( () => IncarcerationInLifeMonthCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.IncarcerationInLifeMonthCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( incarcerationInLifeMonthCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( incarcerationInLifeMonthCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( incarcerationInLifeMonthCountLessThan99Error ) );

            var lastIncarcerationLengthMonthCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.LastIncarcerationLengthMonthCount ) );

            NewPropertyRule ( () => LastIncarcerationLengthMonthCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.LastIncarcerationLengthMonthCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( lastIncarcerationLengthMonthCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( lastIncarcerationLengthMonthCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( lastIncarcerationLengthMonthCountLessThan99Error ) );

            var incarceratedInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.IncarceratedInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => IncarceratedInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.IncarceratedInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( incarceratedInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( incarceratedInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( incarceratedInLastThirtyDaysDayCountLessThan30Error ) );

            var illegalActivityInLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiLegalStatusDto, object> ( dto => dto.IllegalActivityInLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => IllegalActivityInLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiLegalStatus.IllegalActivityInLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( illegalActivityInLastThirtyDaysDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiLegalStatus.TryAddDataErrorInfo ( illegalActivityInLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiLegalStatus.RemoveDataErrorInfo ( illegalActivityInLastThirtyDaysDayCountLessThan30Error ) );
        }

        private void BuildDensAsiMedicalStatusRules ()
        {
            var hopitalizedForMedicalProblemsCountZeroErrorMsg = new DataErrorInfo (
                "M2 must be N/A if M1 equals 0.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiMedicalStatusDto, object> (
                    dto => dto.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan ) );

            NewRule ( () => HopitalizedForMedicalProblemsCountZeroRule )
                .RunForProperty ( s => s.EditingDto.DensAsiMedicalStatus.HopitalizedForMedicalProblemsCount )
                .RunForProperty ( s => s.EditingDto.DensAsiMedicalStatus.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan )
                .When (
                    s =>
                        {
                            if ( s.EditingDto.DensAsiMedicalStatus.HopitalizedForMedicalProblemsCount.Value == 0 )
                            {
                                return s.EditingDto.DensAsiMedicalStatus.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan.NonResponse
                                       == null 
                                       ||
                                       s.EditingDto.DensAsiMedicalStatus.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan.NonResponse.WellKnownName 
                                       != DensAsiNonResponse.NotApplicable;
                            }
                            return false;
                        }
                )
                .ThenReportRuleViolation ( hopitalizedForMedicalProblemsCountZeroErrorMsg.Message )
                .Then ( s => s.EditingDto.DensAsiMedicalStatus.TryAddDataErrorInfo ( hopitalizedForMedicalProblemsCountZeroErrorMsg ) )
                .ElseThen ( s => s.EditingDto.DensAsiMedicalStatus.RemoveDataErrorInfo ( hopitalizedForMedicalProblemsCountZeroErrorMsg ) );

            var medicalProblemsDayCountCannotBeZeroErrorMsg = new DataErrorInfo (
                "M6 cannot be 0 if M7 is greater than zero.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiMedicalStatusDto, object> ( dto => dto.MedicalProblemsDayCount ) );

            NewRule ( () => MedicalProblemsDayCountCannotBeZeroRule )
                .RunForProperty ( s => s.EditingDto.DensAsiMedicalStatus.MedicalProblemsDayCount )
                .RunForProperty ( s => s.EditingDto.DensAsiMedicalStatus.TroubledByMedicalProblemsDensAsiPatientRating )
                .When (
                    s =>
                        {
                            if ( s.EditingDto.DensAsiMedicalStatus.MedicalProblemsDayCount.Value == 0 )
                            {
                                int selectedValue;
                                int.TryParse (
                                    s.EditingDto.DensAsiMedicalStatus.TroubledByMedicalProblemsDensAsiPatientRating.Value.WellKnownName, 
                                    out selectedValue );
                                return selectedValue >= int.Parse ( DensAsiPatientRating.Slightly );
                            }
                            return false;
                        }
                )
                .ThenReportRuleViolation ( medicalProblemsDayCountCannotBeZeroErrorMsg.Message )
                .Then ( s => s.EditingDto.DensAsiMedicalStatus.TryAddDataErrorInfo ( medicalProblemsDayCountCannotBeZeroErrorMsg ) )
                .ElseThen ( s => s.EditingDto.DensAsiMedicalStatus.RemoveDataErrorInfo ( medicalProblemsDayCountCannotBeZeroErrorMsg ) );

            var yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanLessThan99YearsError = new DataErrorInfo (
                LessThan99YearsErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiMedicalStatusDto, object> (
                    dto => dto.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan ) );

            NewRule ( () => YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanLessThan99YearsRule )
                .RunForProperty ( s => s.EditingDto.DensAsiMedicalStatus.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan )
                .When (
                    s => TimeSpanOverflow ( s.EditingDto.DensAsiMedicalStatus.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan ) )
                .ThenReportRuleViolation ( yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanLessThan99YearsError.Message )
                .Then (
                    s =>
                    s.EditingDto.DensAsiMedicalStatus.TryAddDataErrorInfo (
                        yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanLessThan99YearsError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.DensAsiMedicalStatus.RemoveDataErrorInfo (
                        yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanLessThan99YearsError ) );

            var medicalProblemsDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiMedicalStatusDto, object> ( dto => dto.MedicalProblemsDayCount ) );

            NewPropertyRule ( () => MedicalProblemsDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiMedicalStatus.MedicalProblemsDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( medicalProblemsDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiMedicalStatus.TryAddDataErrorInfo ( medicalProblemsDayCountLessThan30Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiMedicalStatus.RemoveDataErrorInfo ( medicalProblemsDayCountLessThan30Error ) );
        }

        private void BuildDensAsiPatientProfileRules ()
        {
            NewRule ( () => OtheReligionRule )
                .RunForProperty ( s => s.EditingDto.DensAsiPatientProfile.PreferredDensAsiReligion )
                .When (
                    s => s.EditingDto.DensAsiPatientProfile.PreferredDensAsiReligion.Value != null &&
                         s.EditingDto.DensAsiPatientProfile.PreferredDensAsiReligion.Value.WellKnownName == DensAsiReligion.Other )
                .Then (
                    s =>
                        {
                            s.EditingDto.DensAsiPatientProfile.Show ( dto => dto.ChristianReligionIndicator );
                            s.EditingDto.DensAsiPatientProfile.Show ( dto => dto.BuddhismReligionIndicator );
                            s.EditingDto.DensAsiPatientProfile.Show ( dto => dto.NoParticularReligiousSectIndicator );
                            s.EditingDto.DensAsiPatientProfile.Show ( dto => dto.ChristianReligionIndicatorNote );
                            s.EditingDto.DensAsiPatientProfile.Show ( dto => dto.BuddhismReligionIndicatorNote );
                            s.EditingDto.DensAsiPatientProfile.Show ( dto => dto.NoParticularReligiousSectIndicatorNote );
                        } )
                .ElseThen (
                    s =>
                        {
                            s.EditingDto.DensAsiPatientProfile.Hide ( dto => dto.ChristianReligionIndicator );
                            s.EditingDto.DensAsiPatientProfile.Hide ( dto => dto.BuddhismReligionIndicator );
                            s.EditingDto.DensAsiPatientProfile.Hide ( dto => dto.NoParticularReligiousSectIndicator );
                            s.EditingDto.DensAsiPatientProfile.Hide ( dto => dto.ChristianReligionIndicatorNote );
                            s.EditingDto.DensAsiPatientProfile.Hide ( dto => dto.BuddhismReligionIndicatorNote );
                            s.EditingDto.DensAsiPatientProfile.Hide ( dto => dto.NoParticularReligiousSectIndicatorNote );
                        } );

            var lastThirtyDaysControlledEnvironmentDayCountMustBeNError = new DataErrorInfo (
                "G20 must be N/A if G19 is No.", 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiPatientProfileDto, object> ( dto => dto.LastThirtyDaysControlledEnvironmentDayCount ) );

            NewRule ( () => LastThirtyDaysControlledEnvironmentDayCountMustBeNRule )
                .RunForProperty ( s => s.EditingDto.DensAsiPatientProfile.LastThirtyDaysControlledEnvironmentDayCount )
                .RunForProperty ( s => s.EditingDto.DensAsiPatientProfile.LastThirtyDaysDensAsiControlledEnvironment )
                .When (
                    s => (s.EditingDto.DensAsiPatientProfile.LastThirtyDaysDensAsiControlledEnvironment.Value != null &&
                        s.EditingDto.DensAsiPatientProfile.LastThirtyDaysDensAsiControlledEnvironment.Value.WellKnownName == DensAsiNonResponse.NotApplicable )
                    && ( s.EditingDto.DensAsiPatientProfile.LastThirtyDaysControlledEnvironmentDayCount.DensAsiNonResponse == null ||
                         s.EditingDto.DensAsiPatientProfile.LastThirtyDaysControlledEnvironmentDayCount.DensAsiNonResponse.WellKnownName
                         != DensAsiNonResponse.NotApplicable ) )
                .ThenReportRuleViolation ( lastThirtyDaysControlledEnvironmentDayCountMustBeNError.Message )
                .Then ( s => s.EditingDto.DensAsiPatientProfile.TryAddDataErrorInfo ( lastThirtyDaysControlledEnvironmentDayCountMustBeNError ) )
                .ElseThen ( s => s.EditingDto.DensAsiPatientProfile.RemoveDataErrorInfo ( lastThirtyDaysControlledEnvironmentDayCountMustBeNError ) );

            var lastThirtyDaysControlledEnvironmentDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiPatientProfileDto, object> ( dto => dto.LastThirtyDaysControlledEnvironmentDayCount ) );

            NewPropertyRule ( () => LastThirtyDaysControlledEnvironmentDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiPatientProfile.LastThirtyDaysControlledEnvironmentDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( lastThirtyDaysControlledEnvironmentDayCountLessThan30Error.Message )
                .Then ( s => s.EditingDto.DensAsiPatientProfile.TryAddDataErrorInfo ( lastThirtyDaysControlledEnvironmentDayCountLessThan30Error ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiPatientProfile.RemoveDataErrorInfo ( lastThirtyDaysControlledEnvironmentDayCountLessThan30Error ) );

            var yearsAndMonthsAtCurrentAddressTimeSpanLessThan99YearsError = new DataErrorInfo (
                LessThan99YearsErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiPatientProfileDto, object> ( dto => dto.YearsAndMonthsAtCurrentAddressTimeSpan ) );

            NewRule ( () => YearsAndMonthsAtCurrentAddressTimeSpanLessThan99YearsRule )
                .RunForProperty ( s => s.EditingDto.DensAsiPatientProfile.YearsAndMonthsAtCurrentAddressTimeSpan )
                .When (
                    s => TimeSpanOverflow ( s.EditingDto.DensAsiPatientProfile.YearsAndMonthsAtCurrentAddressTimeSpan ) )
                .ThenReportRuleViolation ( yearsAndMonthsAtCurrentAddressTimeSpanLessThan99YearsError.Message )
                .Then (
                    s =>
                    s.EditingDto.DensAsiPatientProfile.TryAddDataErrorInfo (
                        yearsAndMonthsAtCurrentAddressTimeSpanLessThan99YearsError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.DensAsiPatientProfile.RemoveDataErrorInfo (
                        yearsAndMonthsAtCurrentAddressTimeSpanLessThan99YearsError ) );
        }

        private void BuildDensAsiPsychiatricStatusRules ()
        {
            var psychologicalTreatmentInHospitalCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiPsychiatricStatusDto, object> ( dto => dto.PsychologicalTreatmentInHospitalCount ) );

            NewPropertyRule ( () => PsychologicalTreatmentInHospitalCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiPsychiatricStatus.PsychologicalTreatmentInHospitalCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( psychologicalTreatmentInHospitalCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiPsychiatricStatus.TryAddDataErrorInfo ( psychologicalTreatmentInHospitalCountLessThan99Error ) )
                .ElseThen ( s => s.EditingDto.DensAsiPsychiatricStatus.RemoveDataErrorInfo ( psychologicalTreatmentInHospitalCountLessThan99Error ) );

            var psychologicalTreatmentAsOutpatientCountLessThan99Error = new DataErrorInfo (
                LessThan99ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiPsychiatricStatusDto, object> ( dto => dto.PsychologicalTreatmentAsOutpatientCount ) );

            NewPropertyRule ( () => PsychologicalTreatmentAsOutpatientCountLessThan99Rule )
                .WithProperty ( s => s.EditingDto.DensAsiPsychiatricStatus.PsychologicalTreatmentAsOutpatientCount )
                .GreaterThan ( 99 )
                .ThenReportRuleViolation ( psychologicalTreatmentAsOutpatientCountLessThan99Error.Message )
                .Then ( s => s.EditingDto.DensAsiPsychiatricStatus.TryAddDataErrorInfo ( psychologicalTreatmentAsOutpatientCountLessThan99Error ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiPsychiatricStatus.RemoveDataErrorInfo ( psychologicalTreatmentAsOutpatientCountLessThan99Error ) );

            var psychologicalProblemsLastThirtyDaysDayCountLessThan30Error = new DataErrorInfo (
                LessThan30ErrorMsg, 
                ErrorLevel.Error, 
                PropertyUtil.ExtractPropertyName<DensAsiPsychiatricStatusDto, object> ( dto => dto.PsychologicalProblemsLastThirtyDaysDayCount ) );

            NewPropertyRule ( () => PsychologicalProblemsLastThirtyDaysDayCountLessThan30Rule )
                .WithProperty ( s => s.EditingDto.DensAsiPsychiatricStatus.PsychologicalProblemsLastThirtyDaysDayCount )
                .GreaterThan ( 30 )
                .ThenReportRuleViolation ( psychologicalProblemsLastThirtyDaysDayCountLessThan30Error.Message )
                .Then (
                    s => s.EditingDto.DensAsiPsychiatricStatus.TryAddDataErrorInfo ( psychologicalProblemsLastThirtyDaysDayCountLessThan30Error ) )
                .ElseThen (
                    s => s.EditingDto.DensAsiPsychiatricStatus.RemoveDataErrorInfo ( psychologicalProblemsLastThirtyDaysDayCountLessThan30Error ) );
        }

        #endregion
    }
}
