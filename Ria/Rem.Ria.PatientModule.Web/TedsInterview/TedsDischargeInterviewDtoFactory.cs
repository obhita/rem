using System;
using AutoMapper;
using Pillar.Domain;
using Rem.Domain.Clinical.TedsModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Factory for teds discharge interview dto.
    /// </summary>
    public class TedsDischargeInterviewDtoFactory : IKeyedDtoFactory<TedsDischargeInterviewDto>
    {
        #region Constants and Fields

        private readonly ITedsDischargeInterviewRepository _tedsDischargeInterviewRepository;
        
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeInterviewDtoFactory"/> class.
        /// </summary>
        /// <param name="tedsDischargeInterviewRepository">The teds discharge interview repository.</param>
        public TedsDischargeInterviewDtoFactory(ITedsDischargeInterviewRepository tedsDischargeInterviewRepository)
        {
            _tedsDischargeInterviewRepository = tedsDischargeInterviewRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the keyed dto.
        /// </summary>
        /// <param name="key">The key of the object.</param>
        /// <returns>A <see cref="Rem.Ria.PatientModule.Web.TedsInterview.TedsDischargeInterviewDto"/></returns>
        public TedsDischargeInterviewDto CreateKeyedDto(long key)
        {
            var tedsDischargeInterview = _tedsDischargeInterviewRepository.GetByKey ( key );

            var tedsDischargeInterviewDto = Mapper.Map<TedsDischargeInterview, TedsDischargeInterviewDto> ( tedsDischargeInterview )
                                            ?? new TedsDischargeInterviewDto ();

            return TedsAnswerPopulator.PopupateTedsAnswers ( tedsDischargeInterviewDto );
        }

        #endregion
    }
}
