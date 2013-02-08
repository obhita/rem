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
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for NidaDrugQuestionnaire class.
    /// </summary>
    public class NidaDrugQuestionnaireViewModel : ActivityViewModelBase<NidaDrugQuestionnaireDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;

        private readonly Dictionary<int, string> _lastdrugInjectionTimeChoices = new Dictionary<int, string>
            {
                { 2, "In the past 90 days" }, { 1, "In the past year" }, { 0, "Over an year ago" }
            };

        private readonly IUserDialogService _userDialogService;
        private readonly Dictionary<bool, string> _yesNoChoices = new Dictionary<bool, string> { { true, "Yes" }, { false, "No" } };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NidaDrugQuestionnaireViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="commandFactory">The command factory.</param>
        public NidaDrugQuestionnaireViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, eventAggregator, commandFactory )
        {
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SaveCommand = commandFactoryHelper.BuildDelegateCommand<KeyedDataTransferObject> (
                () => SaveCommand,
                dto =>
                    {
                        var name = PropertyUtil.ExtractPropertyName ( () => EditingDto );
                        if ( dto != null && EditingDto.GetType () != dto.GetType () )
                        {
                            name = EditingDto.GetType ().GetProperties ().First ( pi => pi.PropertyType == dto.GetType () ).Name;
                        }
                        return name;
                    },
                ExecuteSaveCommand,
                CanExecuteSaveCommand );

            var ruleExecutor = new NotifyPropertyChangedRuleExecutor<NidaDrugQuestionnaireViewModel, IDataTransferObject> ();
            ruleExecutor.AddRunAllRulesProperty ( vm => vm.EditingDto );
            ruleExecutor.WatchSubject ( this );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the lastdrug injection time choices.
        /// </summary>
        public Dictionary<int, string> LastdrugInjectionTimeChoices
        {
            get { return _lastdrugInjectionTimeChoices; }
        }

        /// <summary>
        /// Gets the yes no choices.
        /// </summary>
        public Dictionary<bool, string> YesNoChoices
        {
            get { return _yesNoChoices; }
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
        /// Loads the activity.
        /// </summary>
        /// <param name="activityKey">The activity key.</param>
        protected override void LoadActivity ( long activityKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<NidaDrugQuestionnaireDto> { Key = activityKey } );
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

            //Publish visit changed event.
            var visitChangedEventArgs = new VisitChangedEventArgs
                {
                    Sender = this,
                    VisitKey = EditingDto.VisitKey,
                    ClinicianKey = EditingDto.ClinicianKey,
                    PatientKey = EditingDto.PatientKey,
                    VisitStartDateTime = EditingDto.AppointmentStartDateTime
                };
            _eventAggregator.GetEvent<VisitChangedEvent> ().Publish ( visitChangedEventArgs );

            var activityChangedEventArgs = new ActivityChangedEventArgs
                {
                    Sender = this,
                    VisitKey = EditingDto.VisitKey,
                    ClinicianKey = EditingDto.ClinicianKey,
                    PatientKey = EditingDto.PatientKey
                };
            _eventAggregator.GetEvent<ActivityChangedEvent> ().Publish ( activityChangedEventArgs );

            if ( EditingDto.IsDast10ResultChanged )
            {
                _userDialogService.ShowDialog ( "DAST-10 guidance has been changed.", "Notification", UserDialogServiceOptions.Ok );
            }
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "NIDA Drug Questionnaire operation failed.", UserDialogServiceOptions.Ok );
        }

        private void NavigatedToRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            var responseAuditDto = receivedResponses.Get<DtoResponse<NidaDrugQuestionnaireDto>> ();
            EditingDto = responseAuditDto.DataTransferObject;

            IsLoading = false;
        }

        #endregion
    }
}
