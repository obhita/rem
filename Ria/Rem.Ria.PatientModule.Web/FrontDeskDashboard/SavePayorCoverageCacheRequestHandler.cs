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

using Agatha.Common;
using AutoMapper;
using Pillar.Common.Utility;
using Pillar.Domain.Event;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Extension;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Class for handling save payor coverage cache request.
    /// </summary>
    public class SavePayorCoverageCacheRequestHandler :
        NHibernateSessionRequestHandler<SaveDtoRequest<PayorCoverageCacheDto>, DtoResponse<PayorCoverageCacheDto>>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly IPatientRepository _patientRepository;
        private readonly IPayorCacheRepository _payorCacheRepository;
        private readonly IPayorCoverageCacheFactory _payorCoverageCacheFactory;
        private readonly IPayorCoverageCacheRepository _payorCoverageCacheRepository;
        private bool _validationFailureOccurred;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePayorCoverageCacheRequestHandler"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="payorCoverageCacheRepository">The payor coverage cache repository.</param>
        /// <param name="payorCoverageCacheFactory">The payor coverage cache factory.</param>
        /// <param name="payorCacheRepository">The payor cache repository.</param>
        public SavePayorCoverageCacheRequestHandler (
            IPatientRepository patientRepository,
            IDtoToDomainMappingHelper mappingHelper,
            IPayorCoverageCacheRepository payorCoverageCacheRepository,
            IPayorCoverageCacheFactory payorCoverageCacheFactory,
            IPayorCacheRepository payorCacheRepository )
        {
            _patientRepository = patientRepository;
            _mappingHelper = mappingHelper;
            _payorCoverageCacheRepository = payorCoverageCacheRepository;
            _payorCoverageCacheFactory = payorCoverageCacheFactory;
            _payorCacheRepository = payorCacheRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( SaveDtoRequest<PayorCoverageCacheDto> request )
        {
            var requestDto = request.DataTransferObject;

            DomainEvent.Register<RuleViolationEvent> ( failure => _validationFailureOccurred = true );

            LogicalTreeWalker.Walk<IDataTransferObject> ( requestDto, dto => dto.ClearAllDataErrorInfo () );

            var response = CreateTypedResponse ();

            if ( requestDto.EditStatus == EditStatus.Create || requestDto.EditStatus == EditStatus.Update )
            {
                var patient = _patientRepository.GetByKeyOrThrow ( requestDto.PatientKey, "Patient" );
                var payorCache = _payorCacheRepository.GetByKeyOrThrow ( requestDto.PayorCache.Key, "Payor" );
                var effectiveDateRange = new DateRange ( requestDto.StartDate, requestDto.EndDate );
                var payorCoverageCacheType = _mappingHelper.MapLookupField<PayorCoverageCacheType> ( requestDto.PayorCoverageCacheType );

                var countyArea = _mappingHelper.MapLookupField<CountyArea> ( requestDto.PayorSubscriberCache.Address.CountyArea );
                var stateProvince = _mappingHelper.MapLookupField<StateProvince> ( requestDto.PayorSubscriberCache.Address.StateProvince );
                var country = _mappingHelper.MapLookupField<Country> ( requestDto.PayorSubscriberCache.Address.Country );

                var address = new Address (
                    requestDto.PayorSubscriberCache.Address.FirstStreetAddress,
                    requestDto.PayorSubscriberCache.Address.SecondStreetAddress,
                    requestDto.PayorSubscriberCache.Address.CityName,
                    countyArea,
                    stateProvince,
                    country,
                    new PostalCode ( requestDto.PayorSubscriberCache.Address.PostalCode ) );

                var gender = _mappingHelper.MapLookupField<AdministrativeGender>(requestDto.PayorSubscriberCache.AdministrativeGender);

                var patientName = new PersonNameBuilder ()
                    .WithFirst ( requestDto.PayorSubscriberCache.FirstName )
                    .WithMiddle ( requestDto.PayorSubscriberCache.MiddleName )
                    .WithLast ( requestDto.PayorSubscriberCache.LastName );

                var payorSubscriberRelationshipCacheType =
                    _mappingHelper.MapLookupField<PayorSubscriberRelationshipCacheType> ( requestDto.PayorSubscriberCache.PayorSubscriberRelationshipCacheType );

                var payorSubscriberCache = new PayorSubscriberCache (
                    address, requestDto.PayorSubscriberCache.BirthDate, gender, patientName, payorSubscriberRelationshipCacheType );

                PayorCoverageCache payorCoverageCache;

                if ( requestDto.EditStatus == EditStatus.Create )
                {
                    payorCoverageCache = _payorCoverageCacheFactory.CreatePayorCoverage (
                        patient, payorCache, effectiveDateRange, requestDto.MemberNumber, payorSubscriberCache, payorCoverageCacheType );
                }
                else
                {
                    payorCoverageCache = _payorCoverageCacheRepository.GetByKeyOrThrow ( requestDto.Key, "Payor" );
                    payorCoverageCache.RevisePayorCoverageCacheInfo ( payorCoverageCacheType, effectiveDateRange, requestDto.MemberNumber );
                    payorCoverageCache.RevisePayorCache ( payorCache );
                    payorCoverageCache.RevisePayorSubscriberCache ( payorSubscriberCache );
                }

                response.DataTransferObject = payorCoverageCache == null ? requestDto : Mapper.Map<PayorCoverageCache, PayorCoverageCacheDto>(payorCoverageCache);
            }
            else if ( requestDto.EditStatus == EditStatus.Delete )
            {
                var payorCoverageCache = _payorCoverageCacheRepository.GetByKey ( requestDto.Key );
                if ( payorCoverageCache != null )
                {
                    _payorCoverageCacheFactory.DestroyPayorCoverage ( payorCoverageCache );
                }
                response.DataTransferObject = requestDto;
            }
            else
            {
                var payorCoverageCache = _payorCoverageCacheRepository.GetByKeyOrThrow(requestDto.Key, "Payor");
                response.DataTransferObject = Mapper.Map<PayorCoverageCache, PayorCoverageCacheDto>(payorCoverageCache);
            }

            var processSucceeded = !_validationFailureOccurred;

            if ( processSucceeded )
            {
                Session.Flush ();
                Session.Clear ();
            }
            else
            {
                if (requestDto.EditStatus == EditStatus.Create)
                {
                    response.DataTransferObject.Key = 0;
                }
            }

            return response;
        }

        #endregion
    }
}
