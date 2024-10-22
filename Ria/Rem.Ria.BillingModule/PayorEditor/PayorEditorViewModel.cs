﻿#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using Pillar.Common.Collections;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.BillingModule.Web.Common;
using Rem.Ria.BillingModule.Web.PayorEditor;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.BillingModule.PayorEditor
{
    /// <summary>
    /// View Model for editing payor.
    /// </summary>
    public class PayorEditorViewModel : PanelEditorViewModel<PayorDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;

        private bool _isCreateMode;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private IEnumerable<PayorTypeDto> _payorTypeList;
        private long _payorKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorEditorViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">
        /// The user dialog service.
        /// </param>
        /// <param name="asyncRequestDispatcherFactory">
        /// The async request dispatcher factory.
        /// </param>
        /// <param name="accessControlManager">
        /// The access control manager.
        /// </param>
        /// <param name="commandFactory">
        /// The command factory.
        /// </param>
        public PayorEditorViewModel (
            IUserDialogService userDialogService, 
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory, 
            IAccessControlManager accessControlManager, 
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

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

            var ruleExecutor = new NotifyPropertyChangedRuleExecutor<PayorEditorViewModel, IDataTransferObject> ();
            ruleExecutor.AddRunAllRulesProperty ( vm => vm.EditingDto );
            ruleExecutor.WatchSubject ( this );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is create mode.
        /// </summary>
        public bool IsCreateMode
        {
            get { return _isCreateMode; }
            private set { ApplyPropertyChange ( ref _isCreateMode, () => IsCreateMode, value ); }
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
        /// Gets the payor types.
        /// </summary>
        public IEnumerable<PayorTypeDto> PayorTypeList
        {
            get { return _payorTypeList; }
            private set { ApplyPropertyChange ( ref _payorTypeList, () => PayorTypeList, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">
        /// The received responses.
        /// </param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );
            if ( IsCreateMode )
            {
                if ( EditingDto.Profile != null && EditingDto.Profile.Key > 0 )
                {
                    var payorKey = EditingDto.Profile.Key;
                    EditingDto.Key = payorKey;
                    EditingDto.Addresses.Key = payorKey;
                    EditingDto.PhoneNumbers.Key = payorKey;
                    IsCreateMode = false;
                }
            }
        }

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// <c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.
        /// </returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var payorKey = parameters.GetValue<long> ( "PayorKey" );
            return _payorKey == payorKey;
        }

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">
        /// The command factory.
        /// </param>
        /// <returns>
        /// A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/>
        /// </returns>
        protected override ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory )
        {
            return CommandFactoryHelper.CreateHelper ( this, commandFactory );
        }

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            IsCreateMode = parameters.GetValue<bool> ( "IsCreate" );
            var billingOfficeKey = parameters.GetValue<long> ( "BillingOfficeKey" );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            if ( IsCreateMode )
            {
                EditingDto = new PayorDto
                    {
                        Addresses = new PayorAddressesDto { Addresses = new SoftDeleteObservableCollection<PayorAddressDto> () }, 
                        PhoneNumbers = new PayorPhoneNumbersDto { PhoneNumbers = new SoftDeleteObservableCollection<PayorPhoneDto> () }, 
                        Profile = new PayorProfileDto
                            {
                                BillingOfficeKey = billingOfficeKey, 
                                PayorTypes = new SoftDeleteObservableCollection<PayorTypeDto> ()
                            }
                    };
            }
            else
            {
                _payorKey = parameters.GetValue<long> ( "PayorKey" );
                requestDispatcher.Add ( new GetDtoRequest<PayorDto> { Key = _payorKey } );
            }
            requestDispatcher.Add ( new GetPayorTypesByBillingOfficeKeyRequest { BillingOfficeKey = billingOfficeKey } );
            requestDispatcher
                .AddLookupValuesRequest ( "PayorPhoneType" )
                .AddLookupValuesRequest ( "PayorAddressType" )
                .AddLookupValuesRequest ( "Country" )
                .AddLookupValuesRequest ( "CountyArea" )
                .AddLookupValuesRequest ( "StateProvince" );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                            where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                            select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            LookupValueLists = lookupValueLists;
        }

        private void GetPayorByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<PayorDto>> ();
            EditingDto = response.DataTransferObject;
        }

        private void GetPayorTypeListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetPayorTypesByBillingOfficeKeyResponse> ();
            PayorTypeList = response.PayorTypes;
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            if ( !IsCreateMode )
            {
                GetPayorByKeyCompleted ( receivedResponses );
            }
            GetLookupValuesCompleted ( receivedResponses );
            GetPayorTypeListCompleted ( receivedResponses );
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Payor editor initialization failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
