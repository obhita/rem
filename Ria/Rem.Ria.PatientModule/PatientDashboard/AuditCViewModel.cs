﻿#region License

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
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for AuditC class.
    /// </summary>
    public class AuditCViewModel : ActivityViewModelBase<AuditCDto>
    {
        #region Constants and Fields

        private readonly Dictionary<int, string> _alcoholicDrinksPerDayChoices = new Dictionary<int, string>
            {
                { 0, "1 or 2" }, { 1, "3 or 4" }, { 2, "5 or 6" }, { 3, "7 or 9" }, { 4, "10 or more" }
            };

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;

        private readonly Dictionary<int, string> _howOftenYouDrinkChoices = new Dictionary<int, string>
            {
                { 0, "Never" }, { 1, "Monthly or less" }, { 2, "2-4 times a month" }, { 3, "2-3 times a week" }, { 4, "4 or more times a week" }
            };

        private readonly Dictionary<int, string> _howOftenYouHaveSixOrMoreDrinksChoices = new Dictionary<int, string>
            {
                { 0, "Never" }, { 1, "Less than monthly" }, { 2, "Monthly" }, { 3, "Weekly" }, { 4, "Daily or almost daily" }
            };

        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditCViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="commandFactory">The command factory.</param>
        public AuditCViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            IPopupService popupService,
            IEventAggregator eventAggregator,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, eventAggregator, commandFactory )
        {
            _popupService = popupService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ShowGuidanceCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => ShowGuidanceCommand, ExecuteShowGuidanceCommand, CanExecuteShowGuidanceCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the alcoholic drinks per day choices.
        /// </summary>
        public Dictionary<int, string> AlcoholicDrinksPerDayChoices
        {
            get { return _alcoholicDrinksPerDayChoices; }
        }

        /// <summary>
        /// Gets the how often you drink choices.
        /// </summary>
        public Dictionary<int, string> HowOftenYouDrinkChoices
        {
            get { return _howOftenYouDrinkChoices; }
        }

        /// <summary>
        /// Gets the how often you have six or more drinks choices.
        /// </summary>
        public Dictionary<int, string> HowOftenYouHaveSixOrMoreDrinksChoices
        {
            get { return _howOftenYouHaveSixOrMoreDrinksChoices; }
        }

        /// <summary>
        /// Gets the show guidance command.
        /// </summary>
        public ICommand ShowGuidanceCommand { get; private set; }

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
            requestDispatcher.Add ( new GetDtoRequest<AuditCDto> { Key = activityKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( NavigatedToRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );
            var responseAuditDto = receivedResponses.Get<DtoResponse<AuditCDto>> ();
            EditingDto = responseAuditDto.DataTransferObject;
        }

        private bool CanExecuteShowGuidanceCommand ( object value )
        {
            return EditingDto != null;
        }

        private void ExecuteShowGuidanceCommand ( object value )
        {
            _popupService.ShowPopup (
                "AuditCResultView",
                null,
                "AUDIT-C Guidance",
                new[]
                    {
                        new KeyValuePair<string, string> ( "Score", EditingDto.AuditCScore.GetValueOrDefault ().ToString () ),
                        new KeyValuePair<string, string> ( "Gender", EditingDto.PatientGender.WellKnownName )
                    } );
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "AUDIT-C operation failed.", UserDialogServiceOptions.Ok );
        }

        private void NavigatedToRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            var responseAuditDto = receivedResponses.Get<DtoResponse<AuditCDto>> ();
            EditingDto = responseAuditDto.DataTransferObject;
            IsLoading = false;
        }

        #endregion
    }
}
