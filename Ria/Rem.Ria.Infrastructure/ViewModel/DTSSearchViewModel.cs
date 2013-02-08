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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Service;
using TerminologyService.Client.SL;
using TerminologyService.Infrastructure.SL;
using TerminologyService.WebService;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// View Model for DTSSearch class.
    /// </summary>
    public abstract class DTSSearchViewModel : SearchViewModelBase
    {
        #region Constants and Fields

        private readonly IUserDialogService _userDialogService;
        private readonly string _namespaceToUse;
        private readonly ITerminologyProxy _proxy;
        private readonly string _subSetName;
        private TerminologyVocabulary _namspace;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DTSSearchViewModel"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="namespaceToUse">The namespace to use.</param>
        protected DTSSearchViewModel (
            ITerminologyProxy proxy,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            string namespaceToUse )
            : this ( proxy, userDialogService, accessControlManager, commandFactory, namespaceToUse, null, null )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTSSearchViewModel"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="namespaceToUse">The namespace to use.</param>
        /// <param name="subSetName">Name of the sub set.</param>
        protected DTSSearchViewModel (
            ITerminologyProxy proxy,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            string namespaceToUse,
            string subSetName )
            : this ( proxy, userDialogService, accessControlManager, commandFactory, namespaceToUse, subSetName, null )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTSSearchViewModel"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="namespaceToUse">The namespace to use.</param>
        /// <param name="subsetName">Name of the subset.</param>
        /// <param name="dtsAdvancedSearchCriteria">The DTS advanced search criteria.</param>
        protected DTSSearchViewModel (
            ITerminologyProxy proxy,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            string namespaceToUse,
            string subsetName,
            DtsAdvancedSearchCriteria dtsAdvancedSearchCriteria )
            : base ( accessControlManager, commandFactory, dtsAdvancedSearchCriteria ?? new DtsAdvancedSearchCriteria (), false )
        {
            _proxy = proxy;
            _userDialogService = userDialogService;

            _subSetName = subsetName;

            _proxy.FindConceptsWithNameMatchingCompleted += FindConceptsWithNameMatchingCompleted;
            _proxy.GetConceptByCodeSystemCodeCompleted += GetConceptByCodeSystemCodeCompleted;

            _proxy.GetVocabularyListAsync ();
            _proxy.GetVocabularyListCompleted += GetVocabularyListCompleted;

            _namespaceToUse = namespaceToUse;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the advanced search criteria.
        /// </summary>
        public DtsAdvancedSearchCriteria AdvancedSearchCriteria
        {
            get { return ( DtsAdvancedSearchCriteria )_advancedSearchCriteria; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can execute manual search command].
        /// </summary>
        /// <returns><c>true</c> if this instance [can execute manual search command]; otherwise, <c>false</c>.</returns>
        protected override bool CanExecuteManualSearchCommand ()
        {
            return _namspace != null && QuickSearchCriteria != null && QuickSearchCriteria.Length >= MinimumQuickSearchCriteriaLength;
        }

        /// <summary>
        /// Does the advanced search.
        /// </summary>
        /// <param name="isPageChange">If set to <c>true</c> [is page change].</param>
        protected override void DoAdvancedSearch ( bool isPageChange )
        {
            _proxy.GetConceptByCodeSystemCodeAsync ( AdvancedSearchCriteria.CodeSystemCode, _namspace.Id );
        }

        /// <summary>
        /// Does the quick search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="isPageChange">If set to <c>true</c> [is page change].</param>
        protected override void DoQuickSearch ( string search, bool isPageChange )
        {
            _proxy.FindConceptsWithNameMatchingAsync ( search, _namspace.Id, _subSetName );
        }

        /// <summary>
        /// Maps the terminology concepts to search result.
        /// </summary>
        /// <param name="concepts">The concepts.</param>
        /// <returns>A <see cref="ObservableCollection&lt;ISearchResultDto&gt;"/></returns>
        protected abstract ObservableCollection<ISearchResultDto> MapTerminolgyConceptsToSearchResult (
            IEnumerable<TerminologyConcept> concepts );

        private void FindConceptsWithNameMatchingCompleted ( object sender, ProxyEventArgs e )
        {
            if ( e.Exception == null )
            {
                var result =
                    e.LoadResult<ObservableCollection<TerminologyConcept>> ();

                if ( result != null )
                {
                    if ( result.Count > 0 )
                    {
                        ChangeSearchState (
                            SearchState.QuickSearchFoundState,
                            new PagedCollectionView ( MapTerminolgyConceptsToSearchResult ( result ) ),
                            1,
                            100,
                            result.Count );
                    }
                    else
                    {
                        ChangeSearchState (
                            SearchState.QuickSearchNotFoundState,
                            new PagedCollectionView ( MapTerminolgyConceptsToSearchResult ( result ) ),
                            1,
                            100,
                            result.Count );
                    }
                }
                else
                {
                    ChangeSearchState ( SearchState.QuickSearchNotFoundState, null );
                }
            }
            else
            {
                _userDialogService.ShowDialog ( e.Exception.Message, "Search Failed", UserDialogServiceOptions.Ok );
            }
        }

        private void GetConceptByCodeSystemCodeCompleted ( object sender, ProxyEventArgs e )
        {
            if ( e.Exception == null )
            {
                var result = e.LoadResult<TerminologyConcept> ();

                if ( result != null )
                {
                    ChangeSearchState (
                        SearchState.AdvancedSearchFoundState,
                        new PagedCollectionView ( MapTerminolgyConceptsToSearchResult ( new List<TerminologyConcept> { result } ) ),
                        1,
                        100,
                        1 );
                }
                else
                {
                    ChangeSearchState ( SearchState.AdvancedSearchNotFoundState, null );
                }
            }
            else
            {
                _userDialogService.ShowDialog ( e.Exception.Message, "Search Failed", UserDialogServiceOptions.Ok );
            }
        }

        private void GetVocabularyListCompleted ( object sender, ProxyEventArgs e )
        {
            if ( e.Exception == null )
            {
                var results = e.LoadResult<ObservableCollection<TerminologyVocabulary>> ();
                if ( results != null )
                {
                    _namspace = results.FirstOrDefault ( tv => tv.BusinessCode == _namespaceToUse );
                    if ( _namspace == null )
                    {
                        throw new Exception ( "Dts Vocabulary not found." );
                    }
                    ( ManualSearchCommand as DelegateCommand ).RaiseCanExecuteChanged ();
                }
            }
            else
            {
                _userDialogService.ShowDialog (
                    "An error occurred when retrieving the Terminology Vocabularies. Error: " + e.Exception.Message,
                    "An error has occurred",
                    UserDialogServiceOptions.Ok );
            }
        }

        #endregion
    }
}
