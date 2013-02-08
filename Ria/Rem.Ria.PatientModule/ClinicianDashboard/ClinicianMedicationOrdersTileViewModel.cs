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
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.PatientModule.PatientDashboard;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.PatientModule;

namespace Rem.Ria.PatientModule.ClinicianDashboard
{
    /// <summary>
    /// View Model for ClinicianMedicationOrdersTile class.
    /// </summary>
    public class ClinicianMedicationOrdersTileViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly Predicate<object> _filter;
        private readonly PagedCollectionViewWrapper<MedicationDto> _pagedCollectionViewWrapper;
        private readonly DelegateCommand _showActiveOnlyCommand;
        private readonly DelegateCommand _showAllCommand;
        private IList<MedicationDto> _medicationList;

        private MedicationListViewModel.ShowOption _showOption;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicianMedicationOrdersTileViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public ClinicianMedicationOrdersTileViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<MedicationDto> ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper (
                this, commandFactory );

            _showAllCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowAllCommand, ExecuteShowAll );
            _showActiveOnlyCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowActiveOnlyCommand, ExecuteShowActiveOnly );

            _showOption = MedicationListViewModel.ShowOption.ShowAll;
            _filter = FilterByActiveStatus;

            InitializeGroupingDescriptions ();

            //TODO: this needs to be removed and real data needs to be loaded.
            InitTempData ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<MedicationDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets the show active only command.
        /// </summary>
        public ICommand ShowActiveOnlyCommand
        {
            get { return _showActiveOnlyCommand; }
        }

        /// <summary>
        /// Gets the show all command.
        /// </summary>
        public ICommand ShowAllCommand
        {
            get { return _showAllCommand; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/></returns>
        protected override ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory )
        {
            return CommandFactoryHelper.CreateHelper ( this, commandFactory );
        }

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
        }

        private void ExecuteShowActiveOnly ()
        {
            if ( _showOption != MedicationListViewModel.ShowOption.ShowActive )
            {
                _showOption = MedicationListViewModel.ShowOption.ShowActive;

                PagedCollectionViewWrapper.SetFilter ( _filter );
            }
        }

        private void ExecuteShowAll ()
        {
            if ( _showOption != MedicationListViewModel.ShowOption.ShowAll )
            {
                _showOption = MedicationListViewModel.ShowOption.ShowAll;

                PagedCollectionViewWrapper.SetFilter ( _filter );
            }
        }

        private bool FilterByActiveStatus ( object obj )
        {
            var returnValue = true;

            var medicationDto = obj as MedicationDto;

            if ( medicationDto != null )
            {
                if ( _showOption == MedicationListViewModel.ShowOption.ShowActive )
                {
                    returnValue = medicationDto.MedicationStatus.WellKnownName == MedicationStatus.Active;
                }
            }

            return returnValue;
        }

        private void InitTempData ()
        {
            _medicationList = new List<MedicationDto> ();
            _medicationList.Add (
                new MedicationOrderDto
                    {
                        PatientName = "HarveyBell, Samuel",
                        MedicationCodeCodedConcept = new CodedConceptDto
                            {
                                CodedConceptCode = "12345",
                                DisplayName = "Abilify"
                            },
                        StartDate = new DateTime ( 2010, 8, 8 ),
                        MedicationStatus = new LookupValueDto
                            {
                                Name = "Active"
                            }
                    } );
            _medicationList.Add (
                new MedicationOrderDto
                    {
                        PatientName = "Young, Tad",
                        MedicationCodeCodedConcept = new CodedConceptDto
                            {
                                CodedConceptCode = "123456",
                                DisplayName = "Flonase"
                            },
                        StartDate = new DateTime ( 2010, 8, 8 ),
                        MedicationStatus = new LookupValueDto
                            {
                                Name = "Active"
                            }
                    } );
            _pagedCollectionViewWrapper.WrapInPagedCollectionView ( _medicationList, _filter );
        }

        private void InitializeGroupingDescriptions ()
        {
            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<MedicationDto, object> ( p => p.MedicationCodeCodedConcept ), "Medication Code" ) );
        }

        #endregion
    }
}
