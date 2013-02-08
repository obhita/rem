using System.Linq;
using Pillar.Domain;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Core.SecurityModule.Rule
{
    /// <summary>
    /// The SystemRoleRuleCollection defines rules/rule sets for <see cref="SystemRole">SystemRole</see> entity.
    /// </summary>
    public class SystemRoleRuleCollection : AbstractRuleCollection<SystemRole>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRoleRuleCollection"/> class.
        /// </summary>
        public SystemRoleRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            BuildGrantSystemRoleRuleSet ();
            BuildGrantSystemPermissionRuleSet ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the cannot grant job function to task.
        /// </summary>
        public IRule CannotGrantJobFunctionToTask { get; private set; }

        /// <summary>
        /// Gets the cannot grant job function to task group.
        /// </summary>
        public IRule CannotGrantJobFunctionToTaskGroup { get; private set; }

        /// <summary>
        /// Gets the cannot grant system permission to job function.
        /// </summary>
        public IRule CannotGrantSystemPermissionToJobFunction { get; private set; }

        /// <summary>
        /// Gets the cannot grant system permission to task group.
        /// </summary>
        public IRule CannotGrantSystemPermissionToTaskGroup { get; private set; }

        /// <summary>
        /// Gets the cannot grant task group to task.
        /// </summary>
        public IRule CannotGrantTaskGroupToTask { get; private set; }

        /// <summary>
        /// Gets the grant system permission rule set.
        /// </summary>
        public IRuleSet GrantSystemPermissionRuleSet { get; private set; }

        /// <summary>
        /// Gets the grant system role rule set.
        /// </summary>
        public IRuleSet GrantSystemRoleRuleSet { get; private set; }

        /// <summary>
        /// Gets the no duplicate grant system permission with context.
        /// </summary>
        public IRule NoDuplicateGrantSystemPermissionWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate grant system role with context.
        /// </summary>
        public IRule NoDuplicateGrantSystemRoleWithContext { get; private set; }

        /// <summary>
        /// Gets the cannot grant A role to itself.
        /// </summary>
        public IRule CannotGrantARoleToItself { get; private set; }

        #endregion

        #region Methods

        private void BuildGrantSystemPermissionRuleSet ()
        {
            NewRule(() => NoDuplicateGrantSystemPermissionWithContext)
                .OnContextObject<SystemPermission>()
                .When(
                (r, ctx) => Enumerable.Select<SystemRolePermission, string> ( r.GrantedSystemPermissions, gp => gp.SystemPermission.WellKnownName).Contains(ctx.WorkingMemory.GetContextObject<SystemPermission>().WellKnownName) )
                .ThenReportRuleViolation("Cannot grant a permission within a task that has a duplicate name.");

            NewRule ( () => CannotGrantSystemPermissionToTaskGroup ).When (
                ( r, ctx ) => r.SystemRoleType == SystemRoleType.TaskGroup )
                .ThenReportRuleViolation ( "Cannot grant a permission to a task group." );

            NewRule ( () => CannotGrantSystemPermissionToJobFunction ).When (
                ( r, ctx ) => r.SystemRoleType == SystemRoleType.JobFunction )
                .ThenReportRuleViolation ( "Cannot grant a permission to a job function." );

            NewRuleSet (
                () => GrantSystemPermissionRuleSet, 
                NoDuplicateGrantSystemPermissionWithContext, 
                CannotGrantSystemPermissionToTaskGroup, 
                CannotGrantSystemPermissionToJobFunction
                );
        }

        private void BuildGrantSystemRoleRuleSet ()
        {
            NewRule(() => NoDuplicateGrantSystemRoleWithContext)
            .OnContextObject<SystemRole>()
            .NoDuplicates(ctx => Enumerable.Select<SystemRoleRelationship, SystemRole> ( ctx.Subject.GrantedSystemRoleRelationships, gr => gr.GrantedSystemRole));

            NewRule(() => CannotGrantTaskGroupToTask).When(
                (r, ctx) =>
                {
                    var systemRole = ctx.WorkingMemory.GetContextObject<SystemRole>();
                    return r.SystemRoleType == SystemRoleType.Task && systemRole.SystemRoleType == SystemRoleType.TaskGroup;
                })
                .ThenReportRuleViolation("Cannot grant a task group to a task.");

            NewRule(() => CannotGrantJobFunctionToTaskGroup).When(
                (r, ctx) =>
                {
                    var systemRole = ctx.WorkingMemory.GetContextObject<SystemRole>();
                    return r.SystemRoleType == SystemRoleType.TaskGroup && systemRole.SystemRoleType == SystemRoleType.JobFunction;
                })
                .ThenReportRuleViolation("Cannot grant a job function to a task group.");

            NewRule(() => CannotGrantJobFunctionToTask).When(
                (r, ctx) =>
                {
                    var systemRole = ctx.WorkingMemory.GetContextObject<SystemRole> ();
                    return r.SystemRoleType == SystemRoleType.Task && systemRole.SystemRoleType == SystemRoleType.JobFunction;
                })
                .ThenReportRuleViolation("Cannot grant a job function to a task.");

            NewRule(() => CannotGrantARoleToItself).When(
              (r, ctx) =>
              {
                  var systemRole = ctx.WorkingMemory.GetContextObject<SystemRole>();
                  return r.Key == systemRole.Key;
              })
              .ThenReportRuleViolation("Cannot grant a role to itself.");

            NewRuleSet (
                () => GrantSystemRoleRuleSet, 
                NoDuplicateGrantSystemRoleWithContext, 
                CannotGrantTaskGroupToTask, 
                CannotGrantJobFunctionToTaskGroup, 
                CannotGrantJobFunctionToTask,
                CannotGrantARoleToItself);
        }

        #endregion
    }
}
