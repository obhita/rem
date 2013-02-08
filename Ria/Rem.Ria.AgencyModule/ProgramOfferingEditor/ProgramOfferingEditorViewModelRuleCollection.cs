using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.FluentRuleEngine.RuleSelectors;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.ProgramOfferingEditor;

namespace Rem.Ria.AgencyModule.ProgramOfferingEditor
{
    /// <summary>
    /// ProgramOfferingEditorViewModelRuleCollection class.
    /// </summary>
    public class ProgramOfferingEditorViewModelRuleCollection : AbstractRuleCollection<ProgramOfferingEditorViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingEditorViewModelRuleCollection"/> class.
        /// </summary>
        public ProgramOfferingEditorViewModelRuleCollection ()
        {
            AutoValidatePropertyRules = true;
            ShouldRunWhen (
                ( s, ctx ) => s.EditingDto.Profile != null,
                () =>
                    {
                        var nameError = new DataErrorInfo (
                            "Program is required.",
                            ErrorLevel.Error,
                            PropertyUtil.ExtractPropertyName<ProgramOfferingProfileDto, object> ( dto => dto.Program ) );
                        NewPropertyRule ( () => ProgramRequired )
                            .WithProperty ( s => s.EditingDto.Profile.Program )
                            .NotEmpty ()
                            .ElseThen (
                                ( s, ctx ) =>
                                    {
                                        if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                                        {
                                            s.EditingDto.Profile.TryAddDataErrorInfo ( nameError );
                                        }
                                    } )
                            .Then ( s => s.EditingDto.Profile.RemoveDataErrorInfo ( nameError ) );

                        var startDateError = new DataErrorInfo (
                            "Start Date is required.",
                            ErrorLevel.Error,
                            PropertyUtil.ExtractPropertyName<ProgramOfferingProfileDto, object> ( dto => dto.StartDate ) );
                        NewPropertyRule ( () => StartDateRequired )
                            .WithProperty ( s => s.EditingDto.Profile.StartDate )
                            .NotNull ()
                            .ElseThen (
                                ( s, ctx ) =>
                                    {
                                        if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                                        {
                                            s.EditingDto.Profile.TryAddDataErrorInfo ( startDateError );
                                        }
                                    } )
                            .Then ( s => s.EditingDto.Profile.RemoveDataErrorInfo ( startDateError ) );
                    }
                );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the program required.
        /// </summary>
        /// <value>The program required.</value>
        public IPropertyRule ProgramRequired { get; set; }

        /// <summary>
        /// Gets or sets the start date required.
        /// </summary>
        /// <value>The start date required.</value>
        public IPropertyRule StartDateRequired { get; set; }

        #endregion
    }
}
