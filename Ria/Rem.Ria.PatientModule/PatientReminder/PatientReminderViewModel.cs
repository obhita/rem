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
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.PatientReminder;
using Rem.WellKnownNames.CommonModule;

namespace Rem.Ria.PatientModule.PatientReminder
{
    /// <summary>
    /// View Model for reminding patient.
    /// </summary>
    public class PatientReminderViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private const int DEFAULTPAGESIZE = 50;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private bool _isDrugAllergy;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private int _pageSize;
        private PatientReminderCriteriaDto _patientReminderCriteria;
        private PagedCollectionView _patientReminderResults;
        private string _sortBy;
        private ListSortDirection _sortDirection;
        private int _totalCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientReminderViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PatientReminderViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SearchCommand = commandFactoryHelper.BuildDelegateCommand ( () => SearchCommand, ExecuteSearchCommand );
            ClearCommand = commandFactoryHelper.BuildDelegateCommand ( () => ClearCommand, ExecuteClearCommand );

            PageSize = DEFAULTPAGESIZE;
            PatientReminderCriteria = new PatientReminderCriteriaDto
                {
                    PageSize = DEFAULTPAGESIZE,
                    LabResultFilterModifier = FilterModifier.EqualTo,
                    AgeFilterModifier = FilterModifier.EqualTo
                };

            FilterModifierList = new List<string>
                {
                    FilterModifier.EqualTo,
                    FilterModifier.GreaterThan,
                    FilterModifier.GreaterThanOrEqualTo,
                    FilterModifier.LessThen,
                    FilterModifier.LessThenOrEqualTo
                };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the clear command.
        /// </summary>
        public ICommand ClearCommand { get; private set; }

        /// <summary>
        /// Gets the filter modifier list.
        /// </summary>
        public List<string> FilterModifierList { get; private set; }

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get { return "Patient Reminder"; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is drug allergy.
        /// </summary>
        /// <value><c>true</c> if this instance is drug allergy; otherwise, <c>false</c>.</value>
        public bool IsDrugAllergy
        {
            get { return _isDrugAllergy; }
            set { ApplyPropertyChange ( ref _isDrugAllergy, () => IsDrugAllergy, value ); }
        }

        /// <summary>
        /// Gets the lookup value lists.
        /// </summary>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            private set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex
        {
            get { return PatientReminderCriteria.PageIndex; }
            set
            {
                if ( PatientReminderCriteria.PageIndex == value )
                {
                    return;
                }

                PatientReminderCriteria.PageIndex = value;
                RaisePropertyChanged ( () => PageIndex );

                ExecuteSearchCommand ();
            }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                ApplyPropertyChange ( ref _pageSize, () => PageSize, value );
            }
        }

        /// <summary>
        /// Gets or sets the patient reminder criteria.
        /// </summary>
        /// <value>The patient reminder criteria.</value>
        public PatientReminderCriteriaDto PatientReminderCriteria
        {
            get { return _patientReminderCriteria; }
            set { ApplyPropertyChange ( ref _patientReminderCriteria, () => PatientReminderCriteria, value ); }
        }

        /// <summary>
        /// Gets or sets the patient reminder results.
        /// </summary>
        /// <value>The patient reminder results.</value>
        public PagedCollectionView PatientReminderResults
        {
            get { return _patientReminderResults; }
            set { ApplyPropertyChange ( ref _patientReminderResults, () => PatientReminderResults, value ); }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        public ICommand SearchCommand { get; private set; }

        /// <summary>
        /// Gets or sets the sort by.
        /// </summary>
        /// <value>The sort by.</value>
        public string SortBy
        {
            get { return _sortBy; }
            set { ApplyPropertyChange ( ref _sortBy, () => SortBy, value ); }
        }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>The sort direction.</value>
        public ListSortDirection SortDirection
        {
            get { return _sortDirection; }
            set { ApplyPropertyChange ( ref _sortDirection, () => SortDirection, value ); }
        }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount
        {
            get { return _totalCount; }
            set { ApplyPropertyChange ( ref _totalCount, () => TotalCount, value ); }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.FrontDesk; }
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
            var asyncRequestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            asyncRequestDispatcher.AddLookupValuesRequest ( "PatientGender" );
            asyncRequestDispatcher.AddLookupValuesRequest ( "LabTestName" );
            asyncRequestDispatcher.ProcessRequests ( LookupValuesRequestComplete, HandleRequestDispatcherException );
        }

        private void ExecuteClearCommand ()
        {
            var oldSize = PatientReminderCriteria.PageSize;
            PatientReminderCriteria = new PatientReminderCriteriaDto
                {
                    PageSize = oldSize,
                    LabResultFilterModifier = FilterModifier.EqualTo,
                    AgeFilterModifier = FilterModifier.EqualTo
                };
            IsLoading = false;
        }

        private void ExecuteSearchCommand ()
        {
            // TODO: Age must be validated as < some reasonable value or sqldate overflow. 1/1/1753
            var asyncRequestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            asyncRequestDispatcher.Add ( new PatientReminderSearchRequest { PatientReminderCriteriaDto = PatientReminderCriteria } );
            asyncRequestDispatcher.ProcessRequests ( PatientReminderSearchRequestComplete, HandleRequestDispatcherException );
            IsLoading = true;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Loading Patient Reminder", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void LookupValuesRequestComplete ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            var lookupValueLists =
                responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                    response => response.Name,
                    response => response.LookupValues );

            LookupValueLists = lookupValueLists;
        }

        private void PatientReminderSearchRequestComplete ( ReceivedResponses responses )
        {
            var response = responses.Get<PatientReminderSearchResponse> ();
            TotalCount = response.PatientReminderResultsDto.TotalCount;
            PatientReminderResults = new PagedCollectionView ( response.PatientReminderResultsDto.Results );
            PatientReminderResults.SortDescriptions.Clear ();

            if ( SortBy != null )
            {
                PatientReminderResults.SortDescriptions.Add ( new SortDescription ( SortBy, SortDirection ) );
            }

            IsLoading = false;
        }

        #endregion
    }
}
