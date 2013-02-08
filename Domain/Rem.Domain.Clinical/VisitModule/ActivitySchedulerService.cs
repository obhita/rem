using System;
using System.Collections.Generic;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// The ActivitySchedulerService implements a factory that creates an <see cref="Activity">Activity</see> for a <see cref="Visit">Visit</see> based 
    /// on an requested <see cref="ActivityType">ActivityType</see>.
    /// </summary>
    public class ActivitySchedulerService : IActivitySchedulerService
    {
        private readonly Dictionary<string, Type> _activityTypeFactoryDictionary;
        private readonly IVisitRepository _visitRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitySchedulerService"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        public ActivitySchedulerService ( IVisitRepository visitRepository )
        {
            _activityTypeFactoryDictionary = new Dictionary<string, Type> ();
            _visitRepository = visitRepository;
        }

        #region IActivitySchedulerService Members

        /// <summary>
        /// Registers and associates an ActivityType wellknown name to the factory interface for the Activity.
        /// </summary>
        /// <param name="activityFactoryType">Type of the activity factory.</param>
        /// <param name="activityTypeWellKnowName">Name of the activity type well know.</param>
        public void RegisterFactoryForActivityType ( Type activityFactoryType, string activityTypeWellKnowName )
        {
            _activityTypeFactoryDictionary.Add ( activityTypeWellKnowName, activityFactoryType );
        }

        /// <summary>
        /// Schedules the activity.
        /// </summary>
        /// <param name="visitKey">The visit key.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <returns>An <see cref="Activity"/>. </returns>
        public Activity ScheduleActivity ( long visitKey, ActivityType activityType )
        {
            var visit = _visitRepository.GetByKey ( visitKey );

            Check.IsNotNull ( visit, "Visit was not found to schedule activity of type: " + activityType.Name );
    
            Activity activity = null;

            if (_activityTypeFactoryDictionary.ContainsKey(activityType.WellKnownName))
            {
                var activityFactoryType = _activityTypeFactoryDictionary[activityType.WellKnownName];
                var activityFactory = (IActivityFactory) IoC.CurrentContainer.Resolve ( activityFactoryType );
                activity = activityFactory.CreateActivity ( visit );
            }

            return activity;
        }

        #endregion
    }
}