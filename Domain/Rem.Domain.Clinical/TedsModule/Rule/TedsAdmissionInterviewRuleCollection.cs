using System;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.TedsModule.Rule
{
    /// <summary>
    /// Business rules for TEDS Admission interview.
    /// </summary>
    public class TedsAdmissionInterviewRuleCollection : AbstractRuleCollection<TedsAdmissionRecord>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionInterviewRuleCollection"/> class.
        /// </summary>
        public TedsAdmissionInterviewRuleCollection ()
        {
            //Business rule: field validation
            NewPropertyRule ( () => BirthDateCannotBeFutureDate ).WithProperty ( ai => ai.BirthDate.Value ).LessThen ( DateTime.Now );
        }

        /// <summary>
        /// Gets the revise system data set rule set.
        /// </summary>
        public IRuleSet ReviseSystemDataSetRuleSet { get; private set; }

        /// <summary>
        /// Gets the system transaction type cannot be null.
        /// </summary>
        public IRule SystemTransactionTypeCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the state province cannot be null.
        /// </summary>
        public IRule StateProvinceCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the reporting date cannot be null.
        /// </summary>
        public IRule ReportingDateCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the birth date cannot be future date.
        /// </summary>
        public IPropertyRule BirthDateCannotBeFutureDate { get; private set; }

        /// <summary>
        /// Gets the substance2 and route2 are same as sub1 and route1.
        /// Substance 2 and Route 2 are same as Sub 1 and Route 1, and Detailed Drug Code does not distinguish them.
        /// </summary>
        public IRule Substance2AndRoute2AreSameAsSub1AndRoute1 { get; private set; }
    }
}
