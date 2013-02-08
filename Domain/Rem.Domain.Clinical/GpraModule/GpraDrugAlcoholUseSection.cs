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

using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraDrugAlcoholUseSection contains patient drug and alcohol information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraDrugAlcoholUseSection : GpraInterviewSectionBase
    {
        private readonly GpraNonResponseType<int?> _alcoholIntoxicationFivePlusDrinksDayCount;
        private readonly GpraNonResponseType<int?> _alcoholIntoxicationFourOrFewerDrinksDayCount;
        private readonly GpraNonResponseType<int?> _anyAlcoholDayCount;
        private readonly GpraNonResponseType<int?> _barbituratesDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _barbituratesGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _benzondiazepinesDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _benzondiazepinesGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _cocaineCrackDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _cocaineCrackGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _codeineDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _codeineGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _darvonDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _darvonGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _dermerolDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _dermerolGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _diluadidDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _diluadidGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _hallucinogensDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _hallucinogensGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _heroinDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _heroinGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _illegalDrugsDayCount;
        private readonly GpraNonResponseType<int?> _inhalantsDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _inhalantsGpraDrugRoute;
        private readonly GpraNonResponseType<bool?> _injectedDrugsIndicator;
        private readonly GpraNonResponseType<GpraFrequencyOfUseOfUsedItems> _injectionGpraFrequencyOfUseOfUsedItems;
        private readonly GpraNonResponseType<int?> _ketamineDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _ketamineGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _marijuanaHashishDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _marijuanaHashishGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _methamphetamineDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _methamphetamineGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _morphineDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _morphineGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _nonPrescriptionGhbDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _nonPrescriptionGhbGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _nonPrescriptionMethadoneDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _nonPrescriptionMethodoneGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _otherIllegalDrugsDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _otherIllegalDrugsGpraDrugRoute;
        private readonly string _otherIllegalDrugsSpecificationNote;
        private readonly GpraNonResponseType<int?> _oxycontinOxycodoneDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _oxycontinOxycodoneGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _percocetDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _percocetGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _sameDayAlcoholDrugsDayCount;
        private readonly GpraNonResponseType<int?> _tranquilizersDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _tranquilizersGpraDrugRoute;
        private readonly GpraNonResponseType<int?> _tylenolDayCount;
        private readonly GpraNonResponseType<GpraDrugRoute> _tylenolGpraDrugRoute;

        private GpraDrugAlcoholUseSection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraDrugAlcoholUseSection"/> class.
        /// </summary>
        /// <param name="alcoholIntoxicationFivePlusDrinksDayCount">The alcohol intoxication five plus drinks day count.</param>
        /// <param name="alcoholIntoxicationFourOrFewerDrinksDayCount">The alcohol intoxication four or fewer drinks day count.</param>
        /// <param name="anyAlcoholDayCount">Any alcohol day count.</param>
        /// <param name="barbituratesDayCount">The barbiturates day count.</param>
        /// <param name="barbituratesGpraDrugRoute">The barbiturates gpra drug route.</param>
        /// <param name="benzondiazepinesDayCount">The benzondiazepines day count.</param>
        /// <param name="benzondiazepinesGpraDrugRoute">The benzondiazepines gpra drug route.</param>
        /// <param name="cocaineCrackDayCount">The cocaine crack day count.</param>
        /// <param name="cocaineCrackGpraDrugRoute">The cocaine crack gpra drug route.</param>
        /// <param name="codeineDayCount">The codeine day count.</param>
        /// <param name="codeineGpraDrugRoute">The codeine gpra drug route.</param>
        /// <param name="darvonDayCount">The darvon day count.</param>
        /// <param name="darvonGpraDrugRoute">The darvon gpra drug route.</param>
        /// <param name="dermerolDayCount">The dermerol day count.</param>
        /// <param name="dermerolGpraDrugRoute">The dermerol gpra drug route.</param>
        /// <param name="diluadidDayCount">The diluadid day count.</param>
        /// <param name="diluadidGpraDrugRoute">The diluadid gpra drug route.</param>
        /// <param name="hallucinogensDayCount">The hallucinogens day count.</param>
        /// <param name="hallucinogensGpraDrugRoute">The hallucinogens gpra drug route.</param>
        /// <param name="heroinDayCount">The heroin day count.</param>
        /// <param name="heroinGpraDrugRoute">The heroin gpra drug route.</param>
        /// <param name="illegalDrugsDayCount">The illegal drugs day count.</param>
        /// <param name="inhalantsDayCount">The inhalants day count.</param>
        /// <param name="inhalantsGpraDrugRoute">The inhalants gpra drug route.</param>
        /// <param name="injectedDrugsIndicator">The injected drugs indicator.</param>
        /// <param name="injectionGpraFrequencyOfUseOfUsedItems">The injection gpra frequency of use of used items.</param>
        /// <param name="ketamineDayCount">The ketamine day count.</param>
        /// <param name="ketamineGpraDrugRoute">The ketamine gpra drug route.</param>
        /// <param name="marijuanaHashishDayCount">The marijuana hashish day count.</param>
        /// <param name="marijuanaHashishGpraDrugRoute">The marijuana hashish gpra drug route.</param>
        /// <param name="methamphetamineDayCount">The methamphetamine day count.</param>
        /// <param name="methamphetamineGpraDrugRoute">The methamphetamine gpra drug route.</param>
        /// <param name="morphineDayCount">The morphine day count.</param>
        /// <param name="morphineGpraDrugRoute">The morphine gpra drug route.</param>
        /// <param name="nonPrescriptionGhbDayCount">The non prescription GHB day count.</param>
        /// <param name="nonPrescriptionGhbGpraDrugRoute">The non prescription GHB gpra drug route.</param>
        /// <param name="nonPrescriptionMethadoneDayCount">The non prescription methadone day count.</param>
        /// <param name="nonPrescriptionMethodoneGpraDrugRoute">The non prescription methodone gpra drug route.</param>
        /// <param name="otherIllegalDrugsDayCount">The other illegal drugs day count.</param>
        /// <param name="otherIllegalDrugsGpraDrugRoute">The other illegal drugs gpra drug route.</param>
        /// <param name="otherIllegalDrugsSpecificationNote">The other illegal drugs specification note.</param>
        /// <param name="oxycontinOxycodoneDayCount">The oxycontin oxycodone day count.</param>
        /// <param name="oxycontinOxycodoneGpraDrugRoute">The oxycontin oxycodone gpra drug route.</param>
        /// <param name="percocetDayCount">The percocet day count.</param>
        /// <param name="percocetGpraDrugRoute">The percocet gpra drug route.</param>
        /// <param name="sameDayAlcoholDrugsDayCount">The same day alcohol drugs day count.</param>
        /// <param name="tranquilizersDayCount">The tranquilizers day count.</param>
        /// <param name="tranquilizersGpraDrugRoute">The tranquilizers gpra drug route.</param>
        /// <param name="tylenolDayCount">The tylenol day count.</param>
        /// <param name="tylenolGpraDrugRoute">The tylenol gpra drug route.</param>
        public GpraDrugAlcoholUseSection(GpraNonResponseType<int?> alcoholIntoxicationFivePlusDrinksDayCount,
                                         GpraNonResponseType<int?> alcoholIntoxicationFourOrFewerDrinksDayCount,
                                         GpraNonResponseType<int?> anyAlcoholDayCount,
                                         GpraNonResponseType<int?> barbituratesDayCount,
                                         GpraNonResponseType<GpraDrugRoute> barbituratesGpraDrugRoute,
                                         GpraNonResponseType<int?> benzondiazepinesDayCount,
                                         GpraNonResponseType<GpraDrugRoute> benzondiazepinesGpraDrugRoute,
                                         GpraNonResponseType<int?> cocaineCrackDayCount,
                                         GpraNonResponseType<GpraDrugRoute> cocaineCrackGpraDrugRoute,
                                         GpraNonResponseType<int?> codeineDayCount,
                                         GpraNonResponseType<GpraDrugRoute> codeineGpraDrugRoute,
                                         GpraNonResponseType<int?> darvonDayCount,
                                         GpraNonResponseType<GpraDrugRoute> darvonGpraDrugRoute,
                                         GpraNonResponseType<int?> dermerolDayCount,
                                         GpraNonResponseType<GpraDrugRoute> dermerolGpraDrugRoute,
                                         GpraNonResponseType<int?> diluadidDayCount,
                                         GpraNonResponseType<GpraDrugRoute> diluadidGpraDrugRoute,
                                         GpraNonResponseType<int?> hallucinogensDayCount,
                                         GpraNonResponseType<GpraDrugRoute> hallucinogensGpraDrugRoute,
                                         GpraNonResponseType<int?> heroinDayCount,
                                         GpraNonResponseType<GpraDrugRoute> heroinGpraDrugRoute,
                                         GpraNonResponseType<int?> illegalDrugsDayCount,
                                         GpraNonResponseType<int?> inhalantsDayCount,
                                         GpraNonResponseType<GpraDrugRoute> inhalantsGpraDrugRoute,
                                         GpraNonResponseType<bool?> injectedDrugsIndicator,
                                         GpraNonResponseType<GpraFrequencyOfUseOfUsedItems> injectionGpraFrequencyOfUseOfUsedItems,
                                         GpraNonResponseType<int?> ketamineDayCount,
                                         GpraNonResponseType<GpraDrugRoute> ketamineGpraDrugRoute,
                                         GpraNonResponseType<int?> marijuanaHashishDayCount,
                                         GpraNonResponseType<GpraDrugRoute> marijuanaHashishGpraDrugRoute,
                                         GpraNonResponseType<int?> methamphetamineDayCount,
                                         GpraNonResponseType<GpraDrugRoute> methamphetamineGpraDrugRoute,
                                         GpraNonResponseType<int?> morphineDayCount,
                                         GpraNonResponseType<GpraDrugRoute> morphineGpraDrugRoute,
                                         GpraNonResponseType<int?> nonPrescriptionGhbDayCount,
                                         GpraNonResponseType<GpraDrugRoute> nonPrescriptionGhbGpraDrugRoute,
                                         GpraNonResponseType<int?> nonPrescriptionMethadoneDayCount,
                                         GpraNonResponseType<GpraDrugRoute> nonPrescriptionMethodoneGpraDrugRoute,
                                         GpraNonResponseType<int?> otherIllegalDrugsDayCount,
                                         GpraNonResponseType<GpraDrugRoute> otherIllegalDrugsGpraDrugRoute,
                                         string otherIllegalDrugsSpecificationNote,
                                         GpraNonResponseType<int?> oxycontinOxycodoneDayCount,
                                         GpraNonResponseType<GpraDrugRoute> oxycontinOxycodoneGpraDrugRoute,
                                         GpraNonResponseType<int?> percocetDayCount,
                                         GpraNonResponseType<GpraDrugRoute> percocetGpraDrugRoute,
                                         GpraNonResponseType<int?> sameDayAlcoholDrugsDayCount,
                                         GpraNonResponseType<int?> tranquilizersDayCount,
                                         GpraNonResponseType<GpraDrugRoute> tranquilizersGpraDrugRoute,
                                         GpraNonResponseType<int?> tylenolDayCount,
                                         GpraNonResponseType<GpraDrugRoute> tylenolGpraDrugRoute
            )
        {
            _alcoholIntoxicationFivePlusDrinksDayCount = alcoholIntoxicationFivePlusDrinksDayCount;
            _alcoholIntoxicationFourOrFewerDrinksDayCount = alcoholIntoxicationFourOrFewerDrinksDayCount;
            _anyAlcoholDayCount = anyAlcoholDayCount;
            _barbituratesDayCount = barbituratesDayCount;
            _barbituratesGpraDrugRoute = barbituratesGpraDrugRoute;
            _benzondiazepinesDayCount = benzondiazepinesDayCount;
            _benzondiazepinesGpraDrugRoute = benzondiazepinesGpraDrugRoute;
            _cocaineCrackDayCount = cocaineCrackDayCount;
            _cocaineCrackGpraDrugRoute = cocaineCrackGpraDrugRoute;
            _codeineDayCount = codeineDayCount;
            _codeineGpraDrugRoute = codeineGpraDrugRoute;
            _darvonDayCount = darvonDayCount;
            _darvonGpraDrugRoute = darvonGpraDrugRoute;
            _dermerolDayCount = dermerolDayCount;
            _dermerolGpraDrugRoute = dermerolGpraDrugRoute;
            _diluadidDayCount = diluadidDayCount;
            _diluadidGpraDrugRoute = diluadidGpraDrugRoute;
            _hallucinogensDayCount = hallucinogensDayCount;
            _hallucinogensGpraDrugRoute = hallucinogensGpraDrugRoute;
            _heroinDayCount = heroinDayCount;
            _heroinGpraDrugRoute = heroinGpraDrugRoute;
            _illegalDrugsDayCount = illegalDrugsDayCount;
            _inhalantsDayCount = inhalantsDayCount;
            _inhalantsGpraDrugRoute = inhalantsGpraDrugRoute;
            _injectedDrugsIndicator = injectedDrugsIndicator;
            _injectionGpraFrequencyOfUseOfUsedItems = injectionGpraFrequencyOfUseOfUsedItems;
            _ketamineDayCount = ketamineDayCount;
            _ketamineGpraDrugRoute = ketamineGpraDrugRoute;
            _marijuanaHashishDayCount = marijuanaHashishDayCount;
            _marijuanaHashishGpraDrugRoute = marijuanaHashishGpraDrugRoute;
            _methamphetamineDayCount = methamphetamineDayCount;
            _methamphetamineGpraDrugRoute = methamphetamineGpraDrugRoute;
            _morphineDayCount = morphineDayCount;
            _morphineGpraDrugRoute = morphineGpraDrugRoute;
            _nonPrescriptionGhbDayCount = nonPrescriptionGhbDayCount;
            _nonPrescriptionGhbGpraDrugRoute = nonPrescriptionGhbGpraDrugRoute;
            _nonPrescriptionMethadoneDayCount = nonPrescriptionMethadoneDayCount;
            _nonPrescriptionMethodoneGpraDrugRoute = nonPrescriptionMethodoneGpraDrugRoute;
            _otherIllegalDrugsDayCount = otherIllegalDrugsDayCount;
            _otherIllegalDrugsGpraDrugRoute = otherIllegalDrugsGpraDrugRoute;
            _otherIllegalDrugsSpecificationNote = otherIllegalDrugsSpecificationNote;
            _oxycontinOxycodoneDayCount = oxycontinOxycodoneDayCount;
            _oxycontinOxycodoneGpraDrugRoute = oxycontinOxycodoneGpraDrugRoute;
            _percocetDayCount = percocetDayCount;
            _percocetGpraDrugRoute = percocetGpraDrugRoute;
            _sameDayAlcoholDrugsDayCount = sameDayAlcoholDrugsDayCount;
            _tranquilizersDayCount = tranquilizersDayCount;
            _tranquilizersGpraDrugRoute = tranquilizersGpraDrugRoute;
            _tylenolDayCount = tylenolDayCount;
            _tylenolGpraDrugRoute = tylenolGpraDrugRoute;
        }

        /// <summary>
        /// Gets any alcohol day count.
        /// Question 1: During the past 30 days, how many days have you used the following:  A. Any alchol
        /// </summary>
        public virtual GpraNonResponseType<int?> AnyAlcoholDayCount
        {
            get { return _anyAlcoholDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the alcohol intoxication five plus drinks day count.
        /// Question 1(contd): B1. Alcohol to intoxication (5+ drinks in one sitting)
        /// </summary>
        public virtual GpraNonResponseType<int?> AlcoholIntoxicationFivePlusDrinksDayCount
        {
            get { return _alcoholIntoxicationFivePlusDrinksDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the alcohol intoxication four or fewer drinks day count.
        /// Question 1(contd): B2. Alcohol to intoxication (4 or fewer drinks in one sitting and felt high)
        /// </summary>
        public virtual GpraNonResponseType<int?> AlcoholIntoxicationFourOrFewerDrinksDayCount
        {
            get { return _alcoholIntoxicationFourOrFewerDrinksDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the illegal drugs day count.
        /// Question 1(contd): C. Illegal drugs
        /// </summary>
        public virtual GpraNonResponseType<int?> IllegalDrugsDayCount
        {
            get { return _illegalDrugsDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the same day alcohol drugs day count.
        /// Question 1(contd): D. Both alcohol and drugs (on the same day)
        /// </summary>
        public virtual GpraNonResponseType<int?> SameDayAlcoholDrugsDayCount
        {
            get { return _sameDayAlcoholDrugsDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the cocaine crack day count.
        /// Question 2: During the past 30 days, how many days have you used any of the following: A. Cocaine/Crack
        /// </summary>
        public virtual GpraNonResponseType<int?> CocaineCrackDayCount
        {
            get { return _cocaineCrackDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2: During the past 30 days, how many days have you used any of the following: A. Cocaine/Crack Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> CocaineCrackGpraDrugRoute
        {
            get { return _cocaineCrackGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the marijuana hashish day count.
        /// Question 2B Marijuana/Hashish
        /// </summary>
        public virtual GpraNonResponseType<int?> MarijuanaHashishDayCount
        {
            get { return _marijuanaHashishDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2B Marijuana/Hashish Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> MarijuanaHashishGpraDrugRoute
        {
            get { return _marijuanaHashishGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the heroin day count.
        /// Question 2(contd): C. Opiates 1. Heroin
        /// </summary>
        public virtual GpraNonResponseType<int?> HeroinDayCount
        {
            get { return _heroinDayCount; }
            private set { }
        }


        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): C. Opiates 1. Heroin Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> HeroinGpraDrugRoute
        {
            get { return _heroinGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the morphine day count.
        /// Question 2(contd): C2 Morphine
        /// </summary>
        public virtual GpraNonResponseType<int?> MorphineDayCount
        {
            get { return _morphineDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): C2 Morphine Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> MorphineGpraDrugRoute
        {
            get { return _morphineGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the diluadid day count.
        /// Question 2(contd): C3 Diluadid
        /// </summary>
        public virtual GpraNonResponseType<int?> DiluadidDayCount
        {
            get { return _diluadidDayCount; }
            private set { }
        }


        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): C3 Diluadid Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> DiluadidGpraDrugRoute
        {
            get { return _diluadidGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the dermerol day count.
        /// Question 2(contd): C4 Dermerol
        /// </summary>
        public virtual GpraNonResponseType<int?> DermerolDayCount
        {
            get { return _dermerolDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): C4 Dermerol Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> DermerolGpraDrugRoute
        {
            get { return _dermerolGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the percocet day count.
        /// Question 2(contd): C5 Percocet
        /// </summary>
        public virtual GpraNonResponseType<int?> PercocetDayCount
        {
            get { return _percocetDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): C5 Percocet Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> PercocetGpraDrugRoute
        {
            get { return _percocetGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the darvon day count.
        /// Question 2(contd): C6 Darvon
        /// </summary>
        public virtual GpraNonResponseType<int?> DarvonDayCount
        {
            get { return _darvonDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denotin
        /// Question 2(contd): C6 Darvon Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> DarvonGpraDrugRoute
        {
            get { return _darvonGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the codeine day count.
        /// Question 2(contd): C7 Codeine
        /// </summary>
        public virtual GpraNonResponseType<int?> CodeineDayCount
        {
            get { return _codeineDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): C7 Codeine Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> CodeineGpraDrugRoute
        {
            get { return _codeineGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the tylenol day count.
        /// Question 2(contd): C8 Tylenol 2,3,4
        /// </summary>
        public virtual GpraNonResponseType<int?> TylenolDayCount
        {
            get { return _tylenolDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): C8 Tylenol 2,3,4 Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> TylenolGpraDrugRoute
        {
            get { return _tylenolGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the oxycontin oxycodone day count.
        /// Question 2(contd): C9 Oxycontin/Oxycodone
        /// </summary>
        public virtual GpraNonResponseType<int?> OxycontinOxycodoneDayCount
        {
            get { return _oxycontinOxycodoneDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): C9 Oxycontin/Oxycodone Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> OxycontinOxycodoneGpraDrugRoute
        {
            get { return _oxycontinOxycodoneGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the non prescription methadone day count.
        /// Question 2(contd): D. Non-prescription methadone
        /// </summary>
        public virtual GpraNonResponseType<int?> NonPrescriptionMethadoneDayCount
        {
            get { return _nonPrescriptionMethadoneDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): D. Non-prescription methadone Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> NonPrescriptionMethodoneGpraDrugRoute
        {
            get { return _nonPrescriptionMethodoneGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the hallucinogens day count.
        /// Question 2(contd): E. Hallucinogens/psychedelics, PCP, MDMA, LSD, Mushrooms or Mescaline
        /// </summary>
        public virtual GpraNonResponseType<int?> HallucinogensDayCount
        {
            get { return _hallucinogensDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): E. Hallucinogens/psychedelics, PCP, MDMA, LSD, Mushrooms or Mescaline Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> HallucinogensGpraDrugRoute
        {
            get { return _hallucinogensGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the methamphetamine day count.
        /// Question 2(contd): F. Methamphetamine or other amphetamines
        /// </summary>
        public virtual GpraNonResponseType<int?> MethamphetamineDayCount
        {
            get { return _methamphetamineDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): F. Methamphetamine or other amphetamines Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> MethamphetamineGpraDrugRoute
        {
            get { return _methamphetamineGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the benzondiazepines day count.
        /// Question 2(contd): G1 Benzodiazepines: Diazepam, Alprazolam, Triazolam, and Estasolam 
        /// </summary>
        public virtual GpraNonResponseType<int?> BenzondiazepinesDayCount
        {
            get { return _benzondiazepinesDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): G1 Benzodiazepines: Diazepam, Alprazolam, Triazolam, and Estasolam Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> BenzondiazepinesGpraDrugRoute
        {
            get { return _benzondiazepinesGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the barbiturates day count.
        /// Question 2(contd): G2 Barbiturates: Mephobarbital and pentobarbital sodium
        /// </summary>
        public virtual GpraNonResponseType<int?> BarbituratesDayCount
        {
            get { return _barbituratesDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): G2 Barbiturates: Mephobarbital and pentobarbital sodium Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> BarbituratesGpraDrugRoute
        {
            get { return _barbituratesGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the non prescription GHB day count.
        /// Question 2(contd): G3 Non-prescription GHB
        /// </summary>
        public virtual GpraNonResponseType<int?> NonPrescriptionGhbDayCount
        {
            get { return _nonPrescriptionGhbDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): G3 Non-prescription GHB Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> NonPrescriptionGhbGpraDrugRoute
        {
            get { return _nonPrescriptionGhbGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the ketamine day count.
        /// Question 2(contd): G4 Ketamine
        /// </summary>
        public virtual GpraNonResponseType<int?> KetamineDayCount
        {
            get { return _ketamineDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): G4 Ketamine Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> KetamineGpraDrugRoute
        {
            get { return _ketamineGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the tranquilizers day count.
        /// Question 2(contd): G5 Other tranquilizers, downers, sedatives or hypnotics
        /// </summary>
        public virtual GpraNonResponseType<int?> TranquilizersDayCount
        {
            get { return _tranquilizersDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): G5 Other tranquilizers, downers, sedatives or hypnotics Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> TranquilizersGpraDrugRoute
        {
            get { return _tranquilizersGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the inhalants day count.
        /// Question 2(contd): H. Inhalants
        /// </summary>
        public virtual GpraNonResponseType<int?> InhalantsDayCount
        {
            get { return _inhalantsDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): H. Inhalants Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> InhalantsGpraDrugRoute
        {
            get { return _inhalantsGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the other illegal drugs day count.
        /// Question 2(contd): I. Other illegal drugs (Specify)
        /// </summary>
        public virtual GpraNonResponseType<int?> OtherIllegalDrugsDayCount
        {
            get { return _otherIllegalDrugsDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraDrugRoute">GpraDrugRoute</see>
        /// denoting
        /// Question 2(contd): I. Other illegal drugs (Specify) Drug Route
        /// </summary>
        public virtual GpraNonResponseType<GpraDrugRoute> OtherIllegalDrugsGpraDrugRoute
        {
            get { return _otherIllegalDrugsGpraDrugRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the other illegal drugs specification note.
        /// Question 2(contd): I. Other illegal drugs (Specify) specification
        /// </summary>
        public virtual string OtherIllegalDrugsSpecificationNote
        {
            get { return _otherIllegalDrugsSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating injected drugs.
        /// Question 3: In the past 30 days, have you injected drugs?
        /// </summary>
        public virtual GpraNonResponseType<bool?> InjectedDrugsIndicator
        {
            get { return _injectedDrugsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraFrequencyOfUseOfUsedItems">GpraFrequencyOfUseOfUsedItems</see>
        /// denoting
        /// Question 4: In the past 30 days, how often did you use a syringe/needle, cooker, cotton, or water that someone else used?
        /// </summary>
        public virtual GpraNonResponseType<GpraFrequencyOfUseOfUsedItems> InjectionGpraFrequencyOfUseOfUsedItems
        {
            get { return _injectionGpraFrequencyOfUseOfUsedItems; }
            private set { }
        }
    }
}