using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.FluentRuleEngine.RuleSelectors;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.AgencyDashboard;

namespace Rem.Ria.AgencyModule.ProgramEditor
{
    /// <summary>
    /// EditProgramViewModelRuleCollection class.
    /// </summary>
    public class EditProgramViewModelRuleCollection : AbstractRuleCollection<EditProgramViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProgramViewModelRuleCollection"/> class.
        /// </summary>
        public EditProgramViewModelRuleCollection ()
        {
            AutoValidatePropertyRules = true;
            var nameError = new DataErrorInfo (
                "Name is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<ProgramDto, object> ( dto => dto.Name ) );
            NewPropertyRule ( () => NameIsRequired )
                .WithProperty ( s => s.EditingDto.Name )
                .NotEmpty ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.TryAddDataErrorInfo ( nameError );
                            }
                        } )
                .Then ( s => s.EditingDto.RemoveDataErrorInfo ( nameError ) );

            var startDateError = new DataErrorInfo (
                "Start Date is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<ProgramDto, object> ( dto => dto.StartDate ) );
            NewPropertyRule ( () => StartDateRequired )
                .WithProperty ( s => s.EditingDto.StartDate )
                .NotNull ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.TryAddDataErrorInfo ( startDateError );
                            }
                        } )
                .Then ( s => s.EditingDto.RemoveDataErrorInfo ( startDateError ) );

            var category = new DataErrorInfo (
                "Category is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<ProgramDto, object> ( dto => dto.ProgramCategory ) );
            NewPropertyRule ( () => CategoryRequired )
                .WithProperty ( s => s.EditingDto.ProgramCategory )
                .NotNull ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.TryAddDataErrorInfo ( category );
                            }
                        } )
                .Then ( s => s.EditingDto.RemoveDataErrorInfo ( category ) );

            var approach = new DataErrorInfo (
                "Approach is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<ProgramDto, object> ( dto => dto.TreatmentApproach ) );
            NewPropertyRule ( () => ApproachRequired )
                .WithProperty ( s => s.EditingDto.TreatmentApproach )
                .NotNull ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.TryAddDataErrorInfo ( approach );
                            }
                        } )
                .Then ( s => s.EditingDto.RemoveDataErrorInfo ( approach ) );

            var genderError = new DataErrorInfo (
                "Gender Specification is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<ProgramDto, object> ( dto => dto.GenderSpecification ) );
            NewPropertyRule ( () => GenderRequired )
                .WithProperty ( s => s.EditingDto.GenderSpecification )
                .NotNull ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.TryAddDataErrorInfo ( genderError );
                            }
                        } )
                .Then ( s => s.EditingDto.RemoveDataErrorInfo ( genderError ) );

            var ageError = new DataErrorInfo (
                "Age Group is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<ProgramDto, object> ( dto => dto.AgeGroup ) );
            NewPropertyRule ( () => AgeGroupRequired )
                .WithProperty ( s => s.EditingDto.AgeGroup )
                .NotNull ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.TryAddDataErrorInfo ( ageError );
                            }
                        } )
                .Then ( s => s.EditingDto.RemoveDataErrorInfo ( ageError ) );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the age group required.
        /// </summary>
        /// <value>The age group required.</value>
        public IPropertyRule AgeGroupRequired { get; set; }

        /// <summary>
        /// Gets or sets the approach required.
        /// </summary>
        /// <value>The approach required.</value>
        public IPropertyRule ApproachRequired { get; set; }

        /// <summary>
        /// Gets or sets the category required.
        /// </summary>
        /// <value>The category required.</value>
        public IPropertyRule CategoryRequired { get; set; }

        /// <summary>
        /// Gets or sets the gender required.
        /// </summary>
        /// <value>The gender required.</value>
        public IPropertyRule GenderRequired { get; set; }

        /// <summary>
        /// Gets or sets the name is required.
        /// </summary>
        /// <value>The name is required.</value>
        public IPropertyRule NameIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the start date required.
        /// </summary>
        /// <value>The start date required.</value>
        public IPropertyRule StartDateRequired { get; set; }

        #endregion
    }
}
