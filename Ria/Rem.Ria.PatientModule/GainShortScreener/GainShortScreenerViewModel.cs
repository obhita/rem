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
using System.Linq;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.PatientDashboard;
using Rem.Ria.PatientModule.Web.GainShortScreener;

namespace Rem.Ria.PatientModule.GainShortScreener
{
    /// <summary>
    /// View Model for screening gain short.
    /// </summary>
    public class GainShortScreenerViewModel : ActivityViewModelBase<GainShortScreenerDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GainShortScreenerViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="commandFactory">The command factory.</param>
        public GainShortScreenerViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, eventAggregator, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
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
            requestDispatcher.Add ( new GetDtoRequest<GainShortScreenerDto> { Key = activityKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleRequestDispatcherException );
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            LookupValueLists = responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                response => response.Name, response => response.LookupValues );
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            GetLookupValuesCompleted ( receivedResponses );

            IsLoading = false;
            var response = receivedResponses.Get<DtoResponse<GainShortScreenerDto>> ();
            EditingDto = response.DataTransferObject;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Error Loading Gain Short Screener", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
