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
    /// Data transfer object for GpraDrugAlcoholUse class.
    /// </summary>
    public class GpraDrugAlcoholUseDto : GpraDtoBase
    {
        #region Constants and Fields

        private GpraNonResponseTypeDto<int?> _alcoholIntoxicationFivePlusDrinksDayCount;
        private GpraNonResponseTypeDto<int?> _alcoholIntoxicationFourOrFewerDrinksDayCount;
        private GpraNonResponseTypeDto<int?> _anyAlcoholDayCount;
        private GpraNonResponseTypeDto<int?> _barbituratesDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _barbituratesGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _benzodiazepinesDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _benzondiazepinesGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _cocaineCrackDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _cocaineCrackGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _codeineDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _codeineGpraFrugRoute;
        private GpraNonResponseTypeDto<int?> _darvonDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _darvonGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _dermerolDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _dermerolGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _diluadidDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _diluadidGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _hallucinogensDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _hallucinogensGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _heroinDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _heroinGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _illegalDrugsDayCount;
        private GpraNonResponseTypeDto<int?> _inhalantsDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _inhalantsGpraDrugRoute;
        private GpraNonResponseTypeDto<bool?> _injectedDrugsIndicator;
        private GpraNonResponseTypeDto<LookupValueDto> _injectionGpraFrequencyOfUseOfUsedItems;
        private GpraNonResponseTypeDto<int?> _ketamineDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _ketamineGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _marijuanaHashishDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _marijuanaHashishGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _methamphetamineDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _methamphetamineGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _morphineDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _morphineGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _nonPrescriptionGhbDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _nonPrescriptionGhbGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _nonPrescriptionMethadoneDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _nonPrescriptionMethodoneGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _otherIllegalDrugsDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _otherIllegalDrugsGpraDrugRoute;
        private string _otherIllegalDrugsSpecificationNote;
        private GpraNonResponseTypeDto<int?> _oxycontinOxycodoneDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _oxycontinOxycodoneGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _percocetDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _percocetGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _sameDayAlcoholDrugsDayCount;
        private GpraNonResponseTypeDto<int?> _tranquilizersDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _tranquilizersGpraDrugRoute;
        private GpraNonResponseTypeDto<int?> _tylenolDayCount;
        private GpraNonResponseTypeDto<LookupValueDto> _tylenolGpraDrugRoute;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question 1(contd): B1. Alcohol to intoxication (5+ drinks in one sitting)
        /// </summary>
        /// <value>The alcohol intoxication five plus drinks day count.</value>
        public virtual GpraNonResponseTypeDto<int?> AlcoholIntoxicationFivePlusDrinksDayCount
        {
            get { return _alcoholIntoxicationFivePlusDrinksDayCount; }
            set { ApplyPropertyChange ( ref _alcoholIntoxicationFivePlusDrinksDayCount, () => AlcoholIntoxicationFivePlusDrinksDayCount, value ); }
        }

        /// <summary>
        /// Question 1(contd): B2. Alcohol to intoxication (4 or fewer drinks in one sitting and felt high)
        /// </summary>
        /// <value>The alcohol intoxication four or fewer drinks day count.</value>
        public virtual GpraNonResponseTypeDto<int?> AlcoholIntoxicationFourOrFewerDrinksDayCount
        {
            get { return _alcoholIntoxicationFourOrFewerDrinksDayCount; }
            set { ApplyPropertyChange ( ref _alcoholIntoxicationFourOrFewerDrinksDayCount, () => AlcoholIntoxicationFourOrFewerDrinksDayCount, value ); }
        }

        /// <summary>
        /// Question 1: During the past 30 days, how many days have you used the following:  A. Any alchol
        /// </summary>
        /// <value>Any alcohol day count.</value>
        public virtual GpraNonResponseTypeDto<int?> AnyAlcoholDayCount
        {
            get { return _anyAlcoholDayCount; }
            set { ApplyPropertyChange ( ref _anyAlcoholDayCount, () => AnyAlcoholDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): G2 Barbiturates: Mephobarbital and pentobarbital sodium
        /// </summary>
        /// <value>The barbiturates day count.</value>
        public virtual GpraNonResponseTypeDto<int?> BarbituratesDayCount
        {
            get { return _barbituratesDayCount; }
            set { ApplyPropertyChange ( ref _barbituratesDayCount, () => BarbituratesDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): G2 Barbiturates: Mephobarbital and pentobarbital sodium Drug Route
        /// </summary>
        /// <value>The barbiturates gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> BarbituratesGpraDrugRoute
        {
            get { return _barbituratesGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _barbituratesGpraDrugRoute, () => BarbituratesGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): G1 Benzodiazepines: Diazepam, Alprazolam, Triazolam, and Estasolam
        /// </summary>
        /// <value>The benzondiazepines day count.</value>
        public virtual GpraNonResponseTypeDto<int?> BenzondiazepinesDayCount
        {
            get { return _benzodiazepinesDayCount; }
            set { ApplyPropertyChange ( ref _benzodiazepinesDayCount, () => BenzondiazepinesDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): G1 Benzodiazepines: Diazepam, Alprazolam, Triazolam, and Estasolam Drug Route
        /// </summary>
        /// <value>The benzondiazepines gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> BenzondiazepinesGpraDrugRoute
        {
            get { return _benzondiazepinesGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _benzondiazepinesGpraDrugRoute, () => BenzondiazepinesGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2: During the past 30 days, how many days have you used any of the following: A. Cocaine/Crack
        /// </summary>
        /// <value>The cocaine crack day count.</value>
        public virtual GpraNonResponseTypeDto<int?> CocaineCrackDayCount
        {
            get { return _cocaineCrackDayCount; }
            set { ApplyPropertyChange ( ref _cocaineCrackDayCount, () => CocaineCrackDayCount, value ); }
        }

        /// <summary>
        /// Question 2: During the past 30 days, how many days have you used any of the following: A. Cocaine/Crack Drug Route
        /// </summary>
        /// <value>The cocaine crack gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> CocaineCrackGpraDrugRoute
        {
            get { return _cocaineCrackGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _cocaineCrackGpraDrugRoute, () => CocaineCrackGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): C7 Codeine
        /// </summary>
        /// <value>The codeine day count.</value>
        public virtual GpraNonResponseTypeDto<int?> CodeineDayCount
        {
            get { return _codeineDayCount; }
            set { ApplyPropertyChange ( ref _codeineDayCount, () => CodeineDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C7 Codeine Drug Route
        /// </summary>
        /// <value>The codeine gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> CodeineGpraDrugRoute
        {
            get { return _codeineGpraFrugRoute; }
            set { ApplyPropertyChange ( ref _codeineGpraFrugRoute, () => CodeineGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): C6 Darvon
        /// </summary>
        /// <value>The darvon day count.</value>
        public virtual GpraNonResponseTypeDto<int?> DarvonDayCount
        {
            get { return _darvonDayCount; }
            set { ApplyPropertyChange ( ref _darvonDayCount, () => DarvonDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C6 Darvon Drug Route
        /// </summary>
        /// <value>The darvon gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> DarvonGpraDrugRoute
        {
            get { return _darvonGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _darvonGpraDrugRoute, () => DarvonGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): C4 Dermerol
        /// </summary>
        /// <value>The dermerol day count.</value>
        public virtual GpraNonResponseTypeDto<int?> DermerolDayCount
        {
            get { return _dermerolDayCount; }
            set { ApplyPropertyChange ( ref _dermerolDayCount, () => DermerolDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C4 Dermerol Drug Route
        /// </summary>
        /// <value>The dermerol gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> DermerolGpraDrugRoute
        {
            get { return _dermerolGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _dermerolGpraDrugRoute, () => DermerolGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): C3 Diluadid
        /// </summary>
        /// <value>The diluadid day count.</value>
        public virtual GpraNonResponseTypeDto<int?> DiluadidDayCount
        {
            get { return _diluadidDayCount; }
            set { ApplyPropertyChange ( ref _diluadidDayCount, () => DiluadidDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C3 Diluadid Drug Route
        /// </summary>
        /// <value>The diluadid gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> DiluadidGpraDrugRoute
        {
            get { return _diluadidGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _diluadidGpraDrugRoute, () => DiluadidGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): E. Hallucinogens/psychedelics, PCP, MDMA, LSD, Mushrooms or Mescaline
        /// </summary>
        /// <value>The hallucinogens day count.</value>
        public virtual GpraNonResponseTypeDto<int?> HallucinogensDayCount
        {
            get { return _hallucinogensDayCount; }
            set { ApplyPropertyChange ( ref _hallucinogensDayCount, () => HallucinogensDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): E. Hallucinogens/psychedelics, PCP, MDMA, LSD, Mushrooms or Mescaline Drug Route
        /// </summary>
        /// <value>The hallucinogens gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> HallucinogensGpraDrugRoute
        {
            get { return _hallucinogensGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _hallucinogensGpraDrugRoute, () => HallucinogensGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): C. Opiates 1. Heroin
        /// </summary>
        /// <value>The heroin day count.</value>
        public virtual GpraNonResponseTypeDto<int?> HeroinDayCount
        {
            get { return _heroinDayCount; }
            set { ApplyPropertyChange ( ref _heroinDayCount, () => HeroinDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C. Opiates 1. Heroin Drug Route
        /// </summary>
        /// <value>The heroin gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> HeroinGpraDrugRoute
        {
            get { return _heroinGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _heroinGpraDrugRoute, () => HeroinGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 1(contd): C. Illegal drugs
        /// </summary>
        /// <value>The illegal drugs day count.</value>
        public virtual GpraNonResponseTypeDto<int?> IllegalDrugsDayCount
        {
            get { return _illegalDrugsDayCount; }
            set { ApplyPropertyChange ( ref _illegalDrugsDayCount, () => IllegalDrugsDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): H. Inhalants
        /// </summary>
        /// <value>The inhalants day count.</value>
        public virtual GpraNonResponseTypeDto<int?> InhalantsDayCount
        {
            get { return _inhalantsDayCount; }
            set { ApplyPropertyChange ( ref _inhalantsDayCount, () => InhalantsDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): H. Inhalants Drug Route
        /// </summary>
        /// <value>The inhalants gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> InhalantsGpraDrugRoute
        {
            get { return _inhalantsGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _inhalantsGpraDrugRoute, () => InhalantsGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 3: In the past 30 days, have you injected drugs?
        /// </summary>
        /// <value>The injected drugs indicator.</value>
        public virtual GpraNonResponseTypeDto<bool?> InjectedDrugsIndicator
        {
            get { return _injectedDrugsIndicator; }
            set { ApplyPropertyChange ( ref _injectedDrugsIndicator, () => InjectedDrugsIndicator, value ); }
        }

        /// <summary>
        /// Question 4: In the past 30 days, how often did you use a syringe/needle, cooker, cotton, or water that someone else used?
        /// </summary>
        /// <value>The injection gpra frequency of use of used items.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> InjectionGpraFrequencyOfUseOfUsedItems
        {
            get { return _injectionGpraFrequencyOfUseOfUsedItems; }
            set { ApplyPropertyChange ( ref _injectionGpraFrequencyOfUseOfUsedItems, () => InjectionGpraFrequencyOfUseOfUsedItems, value ); }
        }

        /// <summary>
        /// Question 2(contd): G4 Ketamine
        /// </summary>
        /// <value>The ketamine day count.</value>
        public virtual GpraNonResponseTypeDto<int?> KetamineDayCount
        {
            get { return _ketamineDayCount; }
            set { ApplyPropertyChange ( ref _ketamineDayCount, () => KetamineDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): G4 Ketamine Drug Route
        /// </summary>
        /// <value>The ketamine gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> KetamineGpraDrugRoute
        {
            get { return _ketamineGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _ketamineGpraDrugRoute, () => KetamineGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2B Marijuana/Hashish
        /// </summary>
        /// <value>The marijuana hashish day count.</value>
        public virtual GpraNonResponseTypeDto<int?> MarijuanaHashishDayCount
        {
            get { return _marijuanaHashishDayCount; }
            set { ApplyPropertyChange ( ref _marijuanaHashishDayCount, () => MarijuanaHashishDayCount, value ); }
        }

        /// <summary>
        /// Question 2B Marijuana/Hashish Drug Route
        /// </summary>
        /// <value>The marijuana hashish gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> MarijuanaHashishGpraDrugRoute
        {
            get { return _marijuanaHashishGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _marijuanaHashishGpraDrugRoute, () => MarijuanaHashishGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): F. Methamphetamine or other amphetamines
        /// </summary>
        /// <value>The methamphetamine day count.</value>
        public virtual GpraNonResponseTypeDto<int?> MethamphetamineDayCount
        {
            get { return _methamphetamineDayCount; }
            set { ApplyPropertyChange ( ref _methamphetamineDayCount, () => MethamphetamineDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): F. Methamphetamine or other amphetamines Drug Route
        /// </summary>
        /// <value>The methamphetamine gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> MethamphetamineGpraDrugRoute
        {
            get { return _methamphetamineGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _methamphetamineGpraDrugRoute, () => MethamphetamineGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): C2 Morphine
        /// </summary>
        /// <value>The morphine day count.</value>
        public virtual GpraNonResponseTypeDto<int?> MorphineDayCount
        {
            get { return _morphineDayCount; }
            set { ApplyPropertyChange ( ref _morphineDayCount, () => MorphineDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C2 Morphine Drug Route
        /// </summary>
        /// <value>The morphine gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> MorphineGpraDrugRoute
        {
            get { return _morphineGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _morphineGpraDrugRoute, () => MorphineGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): G3 Non-prescription GHB
        /// </summary>
        /// <value>The non prescription GHB day count.</value>
        public virtual GpraNonResponseTypeDto<int?> NonPrescriptionGhbDayCount
        {
            get { return _nonPrescriptionGhbDayCount; }
            set { ApplyPropertyChange ( ref _nonPrescriptionGhbDayCount, () => NonPrescriptionGhbDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): G3 Non-prescription GHB Drug Route
        /// </summary>
        /// <value>The non prescription GHB gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> NonPrescriptionGhbGpraDrugRoute
        {
            get { return _nonPrescriptionGhbGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _nonPrescriptionGhbGpraDrugRoute, () => NonPrescriptionGhbGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): D. Non-prescription methadone
        /// </summary>
        /// <value>The non prescription methadone day count.</value>
        public virtual GpraNonResponseTypeDto<int?> NonPrescriptionMethadoneDayCount
        {
            get { return _nonPrescriptionMethadoneDayCount; }
            set { ApplyPropertyChange ( ref _nonPrescriptionMethadoneDayCount, () => NonPrescriptionMethadoneDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): D. Non-prescription methadone Drug Route
        /// </summary>
        /// <value>The non prescription methodone gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> NonPrescriptionMethodoneGpraDrugRoute
        {
            get { return _nonPrescriptionMethodoneGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _nonPrescriptionMethodoneGpraDrugRoute, () => NonPrescriptionMethodoneGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): I. Other illegal drugs (Specify)
        /// </summary>
        /// <value>The other illegal drugs day count.</value>
        public virtual GpraNonResponseTypeDto<int?> OtherIllegalDrugsDayCount
        {
            get { return _otherIllegalDrugsDayCount; }
            set { ApplyPropertyChange ( ref _otherIllegalDrugsDayCount, () => OtherIllegalDrugsDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): I. Other illegal drugs (Specify) Drug Route
        /// </summary>
        /// <value>The other illegal drugs gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> OtherIllegalDrugsGpraDrugRoute
        {
            get { return _otherIllegalDrugsGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _otherIllegalDrugsGpraDrugRoute, () => OtherIllegalDrugsGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): I. Other illegal drugs (Specify) specification
        /// </summary>
        /// <value>The other illegal drugs specification note.</value>
        public virtual string OtherIllegalDrugsSpecificationNote
        {
            get { return _otherIllegalDrugsSpecificationNote; }
            set { ApplyPropertyChange ( ref _otherIllegalDrugsSpecificationNote, () => OtherIllegalDrugsSpecificationNote, value ); }
        }

        /// <summary>
        /// Question 2(contd): C9 Oxycontin/Oxycodone
        /// </summary>
        /// <value>The oxycontin oxycodone day count.</value>
        public virtual GpraNonResponseTypeDto<int?> OxycontinOxycodoneDayCount
        {
            get { return _oxycontinOxycodoneDayCount; }
            set { ApplyPropertyChange ( ref _oxycontinOxycodoneDayCount, () => OxycontinOxycodoneDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C9 Oxycontin/Oxycodone Drug Route
        /// </summary>
        /// <value>The oxycontin oxycodone gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> OxycontinOxycodoneGpraDrugRoute
        {
            get { return _oxycontinOxycodoneGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _oxycontinOxycodoneGpraDrugRoute, () => OxycontinOxycodoneGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): C5 Percocet
        /// </summary>
        /// <value>The percocet day count.</value>
        public virtual GpraNonResponseTypeDto<int?> PercocetDayCount
        {
            get { return _percocetDayCount; }
            set { ApplyPropertyChange ( ref _percocetDayCount, () => PercocetDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C5 Percocet Drug Route
        /// </summary>
        /// <value>The percocet gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> PercocetGpraDrugRoute
        {
            get { return _percocetGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _percocetGpraDrugRoute, () => PercocetGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 1(contd): D. Both alcohol and drugs (on the same day)
        /// </summary>
        /// <value>The same day alcohol drugs day count.</value>
        public virtual GpraNonResponseTypeDto<int?> SameDayAlcoholDrugsDayCount
        {
            get { return _sameDayAlcoholDrugsDayCount; }
            set { ApplyPropertyChange ( ref _sameDayAlcoholDrugsDayCount, () => SameDayAlcoholDrugsDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): G5 Other tranquilizers, downers, sedatives or hypnotics
        /// </summary>
        /// <value>The tranquilizers day count.</value>
        public virtual GpraNonResponseTypeDto<int?> TranquilizersDayCount
        {
            get { return _tranquilizersDayCount; }
            set { ApplyPropertyChange ( ref _tranquilizersDayCount, () => TranquilizersDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): G5 Other tranquilizers, downers, sedatives or hypnotics Drug Route
        /// </summary>
        /// <value>The tranquilizers gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> TranquilizersGpraDrugRoute
        {
            get { return _tranquilizersGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _tranquilizersGpraDrugRoute, () => TranquilizersGpraDrugRoute, value ); }
        }

        /// <summary>
        /// Question 2(contd): C8 Tylenol 2,3,4
        /// </summary>
        /// <value>The tylenol day count.</value>
        public virtual GpraNonResponseTypeDto<int?> TylenolDayCount
        {
            get { return _tylenolDayCount; }
            set { ApplyPropertyChange ( ref _tylenolDayCount, () => TylenolDayCount, value ); }
        }

        /// <summary>
        /// Question 2(contd): C8 Tylenol 2,3,4 Drug Route
        /// </summary>
        /// <value>The tylenol gpra drug route.</value>
        public virtual GpraNonResponseTypeDto<LookupValueDto> TylenolGpraDrugRoute
        {
            get { return _tylenolGpraDrugRoute; }
            set { ApplyPropertyChange ( ref _tylenolGpraDrugRoute, () => TylenolGpraDrugRoute, value ); }
        }

        #endregion
    }
}
