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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.PatientDashboard;
using Rem.Ria.PatientModule.Web.TedsInterview;

namespace Rem.Ria.PatientModule.TedsInterview
{
    /// <summary>
    /// View Model for Teds Admission Interview View.
    /// </summary>
    public class TedsAdmissionInterviewViewModel : ActivityViewModelBase<TedsAdmissionInterviewDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;

        private IList<DetailedDrugCodeDto> _detailedDrugCodeList;
        private ObservableCollection<DetailedDrugCodeDto> _primaryDetailedDrugCodeList;
        private ObservableCollection<DetailedDrugCodeDto> _secondaryDetailedDrugCodeList;
        private ObservableCollection<DetailedDrugCodeDto> _tertiaryDetailedDrugCodeList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionInterviewViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="commandFactory">The command factory.</param>
        public TedsAdmissionInterviewViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, eventAggregator, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            _primaryDetailedDrugCodeList = new ObservableCollection<DetailedDrugCodeDto> ();
            _secondaryDetailedDrugCodeList = new ObservableCollection<DetailedDrugCodeDto> ();
            _tertiaryDetailedDrugCodeList = new ObservableCollection<DetailedDrugCodeDto> ();
            
            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            RemoveSubstanceUsageCommand = commandFactoryHelper.BuildDelegateCommand<string> (
                () => RemoveSubstanceUsageCommand, ExecuteRemoveSubstanceUsage);

            var ruleExecutor = new NotifyPropertyChangedRuleExecutor<TedsAdmissionInterviewViewModel, IDataTransferObject>();
            ruleExecutor.AddRunAllRulesProperty(vm => vm.EditingDto);
            ruleExecutor.WatchSubject(this);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the lookup value lists.
        /// </summary>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            private set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets the primary detailed drug code list.
        /// </summary>
        public ObservableCollection<DetailedDrugCodeDto> PrimaryDetailedDrugCodeList
        {
            get { return _primaryDetailedDrugCodeList; }
            private set { ApplyPropertyChange(ref _primaryDetailedDrugCodeList, () => PrimaryDetailedDrugCodeList, value); }
        }

        /// <summary>
        /// Gets the secondary detailed drug code list.
        /// </summary>
        public ObservableCollection<DetailedDrugCodeDto> SecondaryDetailedDrugCodeList
        {
            get { return _secondaryDetailedDrugCodeList; }
            private set { ApplyPropertyChange(ref _secondaryDetailedDrugCodeList, () => SecondaryDetailedDrugCodeList, value); }
        }

        /// <summary>
        /// Gets the tertiary detailed drug code list.
        /// </summary>
        public ObservableCollection<DetailedDrugCodeDto> TertiaryDetailedDrugCodeList
        {
            get { return _tertiaryDetailedDrugCodeList; }
            private set { ApplyPropertyChange(ref _tertiaryDetailedDrugCodeList, () => TertiaryDetailedDrugCodeList, value); }
        }

        /// <summary>
        /// Gets or sets the remove substance usage command.
        /// </summary>
        /// <value>
        /// The remove substance usage command.
        /// </value>
        public ICommand RemoveSubstanceUsageCommand { get; set; }
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
        /// Loads the activity.
        /// </summary>
        /// <param name="activityKey">The activity key.</param>
        protected override void LoadActivity ( long activityKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

            requestDispatcher.Add ( new GetDtoRequest<TedsAdmissionInterviewDto> ( activityKey ) );

            requestDispatcher.AddLookupValuesRequest("TedsNonResponse");
            requestDispatcher.AddLookupValuesRequest("TedsGender");
            requestDispatcher.AddLookupValuesRequest("TedsRace");
            requestDispatcher.AddLookupValuesRequest("TedsEthnicity");
            requestDispatcher.AddLookupValuesRequest("TedsEmploymentStatus");
            requestDispatcher.AddLookupValuesRequest("DetailedNotInLaborForce");

            requestDispatcher.AddLookupValuesRequest("SubstanceProblemType");
            requestDispatcher.AddLookupValuesRequest("UseFrequencyType");
            requestDispatcher.AddLookupValuesRequest("UsualAdministrationRouteType");

            requestDispatcher.AddLookupValuesRequest("LivingArrangementsType");
            requestDispatcher.AddLookupValuesRequest("IncomeSourceType");
            requestDispatcher.AddLookupValuesRequest("PrimaryPaymentSourceType");
            requestDispatcher.AddLookupValuesRequest("MaritalStatus");
            requestDispatcher.AddLookupValuesRequest("ParticipatedSelfHelpGroupInPastThirtyDaysType");

            requestDispatcher.Add(new GetDetailedDrugCodeListRequest());

            IsLoading = true;
            requestDispatcher.ProcessRequests(HandleInitializationCompleted, HandleRequestDispatcherException);
        }

        private void HandleInitializationCompleted(ReceivedResponses receivedResponses)
        {
            IsLoading = false;

            GetLookupValuesCompleted(receivedResponses);

            var response = receivedResponses.Get<DtoResponse<TedsAdmissionInterviewDto>> ();
            EditingDto = response.DataTransferObject;

            // Set DetailedDrugCode lists
            var detailedDrugCodeResponse = receivedResponses.Get<GetDetailedDrugCodeListResponse>();
            _detailedDrugCodeList = detailedDrugCodeResponse.DetailedDrugCodeList;

            if (EditingDto.PrimarySubstanceProblemType.HasValue())
            {
                PrimaryDetailedDrugCodeList.AddRange(_detailedDrugCodeList.Where(p => p.SubstanceProblemTypeKey == EditingDto.PrimarySubstanceProblemType.Response.Key));
            }

            if (EditingDto.SecondarySubstanceProblemType.HasValue())
            {
                SecondaryDetailedDrugCodeList.AddRange(_detailedDrugCodeList.Where(p => p.SubstanceProblemTypeKey == EditingDto.SecondarySubstanceProblemType.Response.Key));
            }

            if (EditingDto.TertiarySubstanceProblemType.HasValue())
            {
                TertiaryDetailedDrugCodeList.AddRange(_detailedDrugCodeList.Where(p => p.SubstanceProblemTypeKey == EditingDto.TertiarySubstanceProblemType.Response.Key));
            }

            EditingDto.PrimarySubstanceProblemType.PropertyChanged += PrimarySubstanceProblemTypePropertyChanged;
            EditingDto.SecondarySubstanceProblemType.PropertyChanged += SecondarySubstanceProblemTypePropertyChanged;
            EditingDto.TertiarySubstanceProblemType.PropertyChanged += TertiarySubstanceProblemTypePropertyChanged;
        }

        private void PrimarySubstanceProblemTypePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PrimaryDetailedDrugCodeList.Clear();
            if (EditingDto.PrimarySubstanceProblemType.HasValue())
            {
                PrimaryDetailedDrugCodeList.AddRange(_detailedDrugCodeList.Where(p => p.SubstanceProblemTypeKey == EditingDto.PrimarySubstanceProblemType.Response.Key));
            }
        }

        private void SecondarySubstanceProblemTypePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SecondaryDetailedDrugCodeList.Clear();
            if (EditingDto.SecondarySubstanceProblemType.HasValue())
            {
                SecondaryDetailedDrugCodeList.AddRange(_detailedDrugCodeList.Where(p => p.SubstanceProblemTypeKey == EditingDto.SecondarySubstanceProblemType.Response.Key));
            }
        }

        private void TertiarySubstanceProblemTypePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TertiaryDetailedDrugCodeList.Clear();
            if (EditingDto.TertiarySubstanceProblemType.HasValue())
            {
                TertiaryDetailedDrugCodeList.AddRange(_detailedDrugCodeList.Where(p => p.SubstanceProblemTypeKey == EditingDto.TertiarySubstanceProblemType.Response.Key));
            }
        }

        private void HandleRequestDispatcherException(ExceptionInfo exceptionInfo)
        {
            IsLoading = false;
            _userDialogService.ShowDialog(exceptionInfo.Message, "Error Loading TEDS Admission Interview.", UserDialogServiceOptions.Ok);
        }

        private void GetLookupValuesCompleted(ReceivedResponses receivedResponses)
        {
            var responses = from response in receivedResponses.Responses
                            where typeof(GetLookupValuesResponse).IsAssignableFrom(response.GetType())
                            select response;

            LookupValueLists = responses.Cast<GetLookupValuesResponse>().ToDictionary(
                response => response.Name, response => response.LookupValues);
        }

        /// <summary>
        /// Executes the remove substance usage.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ExecuteRemoveSubstanceUsage(string parameter)
        {
            if (parameter == "Secondary")
            {
               EditingDto.SecondarySubstanceProblemType.SetAsNotAnswered(); 
               EditingDto.SecondaryUseFrequencyType.SetAsNotAnswered(); 
               EditingDto.SecondaryUsualAdministrationRouteType.SetAsNotAnswered(); 
               EditingDto.SecondaryFirstUseAge.SetAsNotAnswered(); 
               EditingDto.SecondaryDetailedDrugCode.SetAsNotAnswered();
            }
            else if (parameter == "Tertiary")
            {
                EditingDto.TertiarySubstanceProblemType.SetAsNotAnswered();
                EditingDto.TertiaryUseFrequencyType.SetAsNotAnswered();
                EditingDto.TertiaryUsualAdministrationRouteType.SetAsNotAnswered();
                EditingDto.TertiaryFirstUseAge.SetAsNotAnswered();
                EditingDto.TertiaryDetailedDrugCode.SetAsNotAnswered();
            }
        }

        #endregion
    }
}
