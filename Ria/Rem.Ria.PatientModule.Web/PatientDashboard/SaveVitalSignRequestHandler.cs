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

using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save vital sign request.
    /// </summary>
    public class SaveVitalSignRequestHandler : SaveDtoRequestHandlerBase<SaveDtoRequest<VitalSignDto>, DtoResponse<VitalSignDto>, VitalSignDto>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveVitalSignRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveVitalSignRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the specified vital sign dto.
        /// </summary>
        /// <param name="vitalSignDto">The vital sign dto.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool Process ( VitalSignDto vitalSignDto )
        {
            var vitalSign = Session.Get<VitalSign> ( vitalSignDto.Key );

            var vitalSignPhysicalExamNotDoneReason =
                _mappingHelper.MapLookupField<VitalSignPhysicalExamNotDoneReason> ( vitalSignDto.VitalSignPhysicalExamNotDoneReason );

            vitalSign.ReviseHeight ( new Height ( vitalSignDto.HeightFeetMeasure, vitalSignDto.HeightInchesMeasure ) );
            vitalSign.ReviseWeight ( vitalSignDto.WeightLbsMeasure );
            vitalSign.ReviseDietaryConsultationOrderIndicator ( vitalSignDto.DietaryConsultationOrderIndicator );
            vitalSign.ReviseBmiFollowUpPlanIndicator ( vitalSignDto.BmiFollowUpPlanIndicator );
            vitalSign.ReviseVitalSignPhysicalExamNotDoneReason ( vitalSignPhysicalExamNotDoneReason );

            _mappingResult &=
                new AggregateNodeCollectionMapper<BloodPressureDto, VitalSign, BloodPressure> (
                    vitalSignDto.BloodPressures, vitalSign, vitalSign.BloodPressures )
                    .MapRemovedItem ( RemoveBloodPressure )
                    .MapAddedItem ( AddBloodPressure )
                    .MapChangedItem ( ChangeBloodPressure )
                    .Map ();

            _mappingResult &=
                new AggregateNodeCollectionMapper<HeartRateDto, VitalSign, HeartRate> ( vitalSignDto.HeartRates, vitalSign, vitalSign.HeartRates )
                    .MapRemovedItem ( RemoveHeartRate )
                    .MapAddedItem ( AddHeartRate )
                    .MapChangedItem ( ChangeHeartRate )
                    .Map ();

            return true;
        }

        private static void AddBloodPressure ( BloodPressureDto bloodPressureDto, VitalSign vitalSign )
        {
            vitalSign.AddBloodPressure (
                new BloodPressure ( bloodPressureDto.SystollicMeasure.GetValueOrDefault (), bloodPressureDto.DiastollicMeasure.GetValueOrDefault () ) );
        }

        private static void AddHeartRate ( HeartRateDto heartRateDto, VitalSign vitalSign )
        {
            vitalSign.AddHeartRate ( new HeartRate ( heartRateDto.BeatsPerMinuteMeasure.GetValueOrDefault () ) );
        }

        private static void ChangeBloodPressure ( BloodPressureDto bloodPressureDto, VitalSign vitalSign, BloodPressure bloodPressure )
        {
            RemoveBloodPressure ( bloodPressureDto, vitalSign, bloodPressure );
            AddBloodPressure ( bloodPressureDto, vitalSign );
        }

        private static void ChangeHeartRate ( HeartRateDto heartRateDto, VitalSign vitalSign, HeartRate heartRate )
        {
            RemoveHeartRate ( heartRateDto, vitalSign, heartRate );
            AddHeartRate ( heartRateDto, vitalSign );
        }

        private static void RemoveBloodPressure ( BloodPressureDto bloodPressureDto, VitalSign vitalSign, BloodPressure bloodPressure )
        {
            vitalSign.RemoveBloodPressure ( bloodPressure );
        }

        private static void RemoveHeartRate ( HeartRateDto heartRateDto, VitalSign vitalSign, HeartRate heartRate )
        {
            vitalSign.RemoveHeartRate ( heartRate );
        }

        #endregion
    }
}
