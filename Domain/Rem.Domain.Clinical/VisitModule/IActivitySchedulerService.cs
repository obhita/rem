using System;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// IActivityFactory interface defines services for an <see cref="ActivitySchedulerService">ActivitySchedulerService</see>.
    /// </summary>
    public interface IActivitySchedulerService
    {
        /// <summary>
        /// Registers the type of the factory for activity.
        /// </summary>
        /// <param name="activityFactoryType">Type of the activity factory.</param>
        /// <param name="activityTypeWellKnowName">Name of the activity type well know.</param>
        void RegisterFactoryForActivityType ( Type activityFactoryType, string activityTypeWellKnowName );

        /// <summary>
        /// Schedules the activity.
        /// </summary>
        /// <param name="visitKey">The visit key.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <returns>An Activity.</returns>
        Activity ScheduleActivity ( long visitKey, ActivityType activityType );
    }
}
