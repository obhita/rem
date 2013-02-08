using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.PatientModule.Rule
{
    /// <summary>
    /// The PatientContactRuleCollection defines rules/rule sets for <see cref="PatientContact">PatientContact</see> entity.
    /// </summary>
    public class PatientContactRuleCollection : AbstractRuleCollection<PatientContact>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContactRuleCollection"/> class.
        /// </summary>
        public PatientContactRuleCollection ()
        {
            NewPropertyRule ( () => FirstNameNotNull ).WithProperty ( pc => pc.FirstName ).NotNull ();
            NewPropertyRule ( () => LastNameNotNull ).WithProperty ( pc => pc.LastName ).NotNull ();

            NewRuleSet ( () => CreatePatientContactRuleSet, FirstNameNotNull, LastNameNotNull );

            NewRule ( () => PatientContactRelationshipTypeNotNull ).When (
                ( s, ctx ) =>
                    {
                        var patietnContactRelationshipType = ctx.WorkingMemory.GetContextObject<PatientContactRelationshipType> ();
                        return patietnContactRelationshipType == null;
                    } ).ThenReportRuleViolation ( "Patient Contact Relationship Type is required." );

            NewRuleSet ( () => RevisePatientContactRelationshipTypeRuleSet, PatientContactRelationshipTypeNotNull );
        }

        /// <summary>
        /// Gets the create patient contact rule set.
        /// </summary>
        public IRuleSet CreatePatientContactRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise patient contact relationship type rule set.
        /// </summary>
        public IRuleSet RevisePatientContactRelationshipTypeRuleSet { get; private set; }

        /// <summary>
        /// Gets the patient contact relationship type not null.
        /// </summary>
        public IRule PatientContactRelationshipTypeNotNull { get; private set; }

        /// <summary>
        /// Gets the first name not null.
        /// </summary>
        public IPropertyRule FirstNameNotNull { get; private set; }

        /// <summary>
        /// Gets the last name not null.
        /// </summary>
        public IPropertyRule LastNameNotNull { get; private set; }
    }
}
