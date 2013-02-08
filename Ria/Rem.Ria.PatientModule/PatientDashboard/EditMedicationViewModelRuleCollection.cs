#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.RuleSelectors;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.PatientModule;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// EditMedicationViewModelRuleCollection class.
    /// </summary>
    public class EditMedicationViewModelRuleCollection : AbstractRuleCollection<EditMedicationViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditMedicationViewModelRuleCollection"/> class.
        /// </summary>
        public EditMedicationViewModelRuleCollection ()
        {
            AutoValidatePropertyRules = true;
            ShouldRunWhen (
                ( s, ctx ) => s.Medication != null,
                () =>
                    {
                        var medicationStatusShouldNoBeActiveWhenUsageEndDateIsInThePastError =
                            new DataErrorInfo (
                                "Medication Status should not be active when date discontinued is in the past.",
                                ErrorLevel.Error,
                                PropertyUtil.ExtractPropertyName<MedicationDto, object> ( dto => dto.MedicationStatus ) );
                        NewPropertyRule ( () => MedicationStatusShouldNotBeActiveWhenUsageEndDateIsInThePast )
                            .WithProperty ( s => s.Medication.MedicationStatus )
                            .Constrain (
                                new RelationshipConstrain<LookupValueDto, EditMedicationViewModel> (
                                    ( status, vm ) =>
                                        {
                                            if ( vm.Medication.EndDate.HasValue && vm.Medication.EndDate.Value < DateTime.Today )
                                            {
                                                if ( status != null && status.WellKnownName == MedicationStatus.Active )
                                                {
                                                    return false;
                                                }
                                            }

                                            return true;
                                        } ) )
                            .ElseThen (
                                ( s, ctx ) =>
                                    {
                                        if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                                        {
                                            s.Medication.TryAddDataErrorInfo ( medicationStatusShouldNoBeActiveWhenUsageEndDateIsInThePastError );
                                        }
                                    } )
                            .Then ( s => s.Medication.RemoveDataErrorInfo ( medicationStatusShouldNoBeActiveWhenUsageEndDateIsInThePastError ) );

                        var usageEndDateShouldNotBeInTheFutureWhenMedicationStatusIsInactiveError =
                            new DataErrorInfo (
                                "Date discontinued should not be in the future when medication status is inactive.",
                                ErrorLevel.Error,
                                PropertyUtil.ExtractPropertyName<MedicationDto, object> ( dto => dto.EndDate ) );
                        NewPropertyRule ( () => UsageEndDateShouldNoBeInTheFutureWhenMedicationStatusIsInactive )
                            .WithProperty ( s => s.Medication.EndDate )
                            .Constrain (
                                new RelationshipConstrain<DateTime?, EditMedicationViewModel> (
                                    ( endDate, vm ) =>
                                        {
                                            if ( vm.Medication.MedicationStatus != null
                                                 && vm.Medication.MedicationStatus.WellKnownName == MedicationStatus.Inactive )
                                            {
                                                if ( vm.Medication.EndDate.HasValue && vm.Medication.EndDate.Value >= DateTime.Today )
                                                {
                                                    return false;
                                                }
                                            }

                                            return true;
                                        } ) )
                            .ElseThen (
                                ( s, ctx ) =>
                                    {
                                        if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                                        {
                                            s.Medication.TryAddDataErrorInfo ( usageEndDateShouldNotBeInTheFutureWhenMedicationStatusIsInactiveError );
                                        }
                                    } )
                            .Then ( s => s.Medication.RemoveDataErrorInfo ( usageEndDateShouldNotBeInTheFutureWhenMedicationStatusIsInactiveError ) );
                    }
                );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the medication status should not be active when usage end date is in the past.
        /// </summary>
        /// <value>The medication status should not be active when usage end date is in the past.</value>
        public IPropertyRule MedicationStatusShouldNotBeActiveWhenUsageEndDateIsInThePast { get; set; }

        /// <summary>
        /// Gets or sets the usage end date should no be in the future when medication status is inactive.
        /// </summary>
        /// <value>The usage end date should no be in the future when medication status is inactive.</value>
        public IPropertyRule UsageEndDateShouldNoBeInTheFutureWhenMedicationStatusIsInactive { get; set; }

        #endregion
    }
}
