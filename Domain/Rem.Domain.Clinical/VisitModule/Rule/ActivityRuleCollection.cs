using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.VisitModule.Rule
{
    /// <summary>
    /// The ActivityRuleCollection defines rules/rule sets for <see cref="Activity">Activity</see> entity.
    /// </summary>
    public class ActivityRuleCollection : AbstractRuleCollection<Activity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityRuleCollection"/> class.
        /// </summary>
        public ActivityRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            ShouldRunWhen (
                ( activity, datetimeRange ) => activity.Visit != null,
                () =>
                    {
                        NewPropertyRule ( () => ActivityRangeMustBeInSideVisitRange )
                            .WithProperty ( a => a.Visit.AppointmentDateTimeRange )
                            .ContextObjectConstrain ( ( p, ctxObj ) => p.Includes ( ctxObj ) );
                    }
                );
            NewRuleSet ( () => ReviseActivityDateTimeRangeRuleSet, ActivityRangeMustBeInSideVisitRange );
        }

        /// <summary>
        /// Gets the revise activity date time range rule set.
        /// </summary>
        public IRuleSet ReviseActivityDateTimeRangeRuleSet { get; private set; }

        /// <summary>
        /// Gets the activity range must be in side visit range.
        /// </summary>
        public IPropertyRule ActivityRangeMustBeInSideVisitRange { get; private set; }
    }
}
