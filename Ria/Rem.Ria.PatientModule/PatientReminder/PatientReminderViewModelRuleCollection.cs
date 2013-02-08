using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.PatientReminder;

namespace Rem.Ria.PatientModule.PatientReminder
{
    /// <summary>
    /// Rule collection for PatientReminderViewModel class.
    /// </summary>
    public class PatientReminderViewModelRuleCollection : AbstractRuleCollection<PatientReminderViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientReminderViewModelRuleCollection"/> class.
        /// </summary>
        public PatientReminderViewModelRuleCollection()
        {
            AutoValidatePropertyRules = true;

            var ageRageDataError = new DataErrorInfo (
                "Age must be between 0 and 200.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PatientReminderCriteriaDto, int?> ( dto => dto.Age ) );
            NewPropertyRule ( () => AgeInRange )
                .WithProperty ( s => s.PatientReminderCriteria.Age )
                .InclusiveBetweenOrNull ( 0, 200 )
                .ElseThen ( ( s, ctx ) => s.PatientReminderCriteria.TryAddDataErrorInfo ( ageRageDataError ) )
                .Then ( s => s.PatientReminderCriteria.RemoveDataErrorInfo ( ageRageDataError ) );

            NewRuleSet ( () => SearchCommandRuleSet, AgeInRange );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the age in range rule.
        /// </summary>
        /// <value>The age in range rule.</value>
        /// <remarks>This rule validates Age being searched is between 0 and 200.</remarks>
        public IPropertyRule AgeInRange { get; set; }

        /// <summary>
        /// Gets or sets the search command rule set.
        /// </summary>
        /// <value>The search command rule set.</value>
        public IRuleSet SearchCommandRuleSet { get; set; }

        #endregion
    }
}
