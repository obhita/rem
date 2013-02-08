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

using Agatha.Common;
using Pillar.Common.Commands;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.QuickPickers
{
    /// <summary>
    /// View Model for picking payor cache quick.
    /// </summary>
    public class PayorCacheQuickPickerViewModel : QuickPickerViewModelBase
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCacheQuickPickerViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PayorCacheQuickPickerViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            ICommandFactory commandFactory )
            : base ( commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;

            ApplyContextChanges = true;
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
        /// Does the search.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        protected override void DoSearch ( string searchCriteria, int pageIndex, int pageSize )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new GetPayorCacheByKeywordRequest
                    {
                        SearchCriteria = searchCriteria,
                        PageIndex = pageIndex,
                        PageSize = pageSize
                    } );
            requestDispatcher.ProcessRequests ( HandleGetPayorCacheByKeywordCompleted, HandleGetPayorCacheByKeywordException );
        }

        private void HandleGetPayorCacheByKeywordCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetPayorCacheByKeywordResponse> ();
            var searchedCriteria = response.SearchCriteria;
            var result = response.PagedPayorCache;

            if ( searchedCriteria == QuickSearchCriteria )
            {
                ResultsReceived ( result.PagedList, result.PageIndex, result.PageSize, result.TotalCount );
            }
        }

        private void HandleGetPayorCacheByKeywordException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Search Failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
