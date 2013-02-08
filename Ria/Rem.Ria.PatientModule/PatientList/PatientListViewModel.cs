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
using System.Linq;
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
using Rem.Ria.PatientModule.Web.PatientList;
using Rem.WellKnownNames.CommonModule;

namespace Rem.Ria.PatientModule.PatientList
{
    /// <summary>
    /// View Model for PatientList class.
    /// </summary>
    public class PatientListViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private const int DEFAULTPAGESIZE = 50;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private PatientListCriteriaDto _patientListCriteria;
        private PatientListResultsDto _patientListResults;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientListViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PatientListViewModel (
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

            PatientListCriteria = new PatientListCriteriaDto ();
            PatientListCriteria.PageSize = DEFAULTPAGESIZE;
            PatientListCriteria.LabResultFilterModifier = FilterModifier.EqualTo;
            PatientListCriteria.AgeFilterModifier = FilterModifier.EqualTo;

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
            get { return "Patient List"; }
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
        /// Gets or sets the patient list.
        /// </summary>
        /// <value>The patient list.</value>
        public PatientListResultsDto PatientList
        {
            get { return _patientListResults; }
            set { ApplyPropertyChange ( ref _patientListResults, () => PatientList, value ); }
        }

        /// <summary>
        /// Gets or sets the patient list criteria.
        /// </summary>
        /// <value>The patient list criteria.</value>
        public PatientListCriteriaDto PatientListCriteria
        {
            get { return _patientListCriteria; }
            set { ApplyPropertyChange ( ref _patientListCriteria, () => PatientListCriteria, value ); }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        public ICommand SearchCommand { get; private set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.PatientList; }
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
            var oldSize = PatientListCriteria.PageSize;
            PatientListCriteria = new PatientListCriteriaDto
                {
                    PageSize = oldSize,
                    LabResultFilterModifier = FilterModifier.EqualTo,
                    AgeFilterModifier = FilterModifier.EqualTo
                };
        }

        private void ExecuteSearchCommand ()
        {
            IsLoading = true;
            var asyncRequestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            asyncRequestDispatcher.Add ( new PatientListSearchRequest { PatientListCriteriaDto = PatientListCriteria } );
            asyncRequestDispatcher.ProcessRequests ( PatientListSearchRequestComplete, HandleRequestDispatcherException );
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Loading Patient List", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void LookupValuesRequestComplete ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            var lookupValueLists =
                responses.Cast<GetLookupValuesResponse> ().ToDictionary ( response => response.Name, response => response.LookupValues );

            LookupValueLists = lookupValueLists;
        }

        private void PatientListSearchRequestComplete ( ReceivedResponses responses )
        {
            var response = responses.Get<PatientListSearchResponse> ();
            PatientList = response.PatientList;
            IsLoading = false;
        }

        #endregion
    }
}
