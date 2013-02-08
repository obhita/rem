using Pillar.Common.Utility;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// Factory for creation of DensAsiInterview.
    /// </summary>
    public class DensAsiInterviewFactory : IDensAsiInterviewFactory
    {
        private readonly IDensAsiInterviewRepository _densAsiInterviewRepository;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiInterviewFactory"/> class.
        /// </summary>
        /// <param name="densAsiInterviewRepository">The DensAsi interview repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public DensAsiInterviewFactory ( IDensAsiInterviewRepository densAsiInterviewRepository, ILookupValueRepository lookupValueRepository )
        {
            _lookupValueRepository = lookupValueRepository;
            _densAsiInterviewRepository = densAsiInterviewRepository;
        }

        #region IDensAsiInterviewFactory Members

        /// <summary>
        /// Destroys the DensAsi interview.
        /// </summary>
        /// <param name="densAsiInterview">The DensAsi interview.</param>
        public void DestroyDensAsiInterview ( DensAsiInterview densAsiInterview )
        {
            Check.IsNotNull ( densAsiInterview, "DensAsiInterview is required." );

            _densAsiInterviewRepository.MakeTransient ( densAsiInterview );
        }

        /// <summary>
        /// Creates the DensAsi interview.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A DensAsiInterview.</returns>
        public DensAsiInterview CreateDensAsiInterview ( Visit visit )
        {
            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType>(WellKnownNames.VisitModule.ActivityType.DensAsiInterview);
            var densAsiInterview = new DensAsiInterview ( visit, activityType );

            _densAsiInterviewRepository.MakePersistent ( densAsiInterview );

            return densAsiInterview;
        }

        #endregion

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateDensAsiInterview(visit);
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyDensAsiInterview((DensAsiInterview)activity);
        }
    }
}