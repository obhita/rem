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
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using TerminologyService.Client.SL;
using TerminologyService.Infrastructure.SL;
using TerminologyService.WebService;

namespace Rem.Ria.PatientModule.InteroperabilityWorkspace
{
    /// <summary>
    /// View Model for Vocabulary for terminology.
    /// </summary>
    public class TerminologyVocabularyViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly ITerminologyProxy _proxy;
        private readonly UserDialogService _userDialogService;
        private ObservableCollection<TerminologyVocabulary> _namespaces;
        private ObservableCollection<TerminologyConcept> _rootConcepts;
        private ObservableCollection<TerminologyConcept> _searchResults;
        private TerminologyVocabulary _selectedNamespace;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminologyVocabularyViewModel"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public TerminologyVocabularyViewModel (
            ITerminologyProxy proxy, UserDialogService userDialogService, IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _proxy = proxy;
            _userDialogService = userDialogService;
            _proxy.GetVocabularyListCompleted += GetVocabularyListCompleted;
            _proxy.FindConceptsWithNameMatchingCompleted += FindConceptsWithNameMatchingCompleted;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            NamespaceChangedCommand = commandFactoryHelper.BuildDelegateCommand<TerminologyVocabulary> (
                () => NamespaceChangedCommand, ExecuteNamespaceChanged );
            PerformSearchCommand = commandFactoryHelper.BuildDelegateCommand<string> ( () => PerformSearchCommand, ExecutePerformSearch );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the namespace changed command.
        /// </summary>
        /// <value>The namespace changed command.</value>
        public ICommand NamespaceChangedCommand { get; set; }

        /// <summary>
        /// Gets or sets the namespaces.
        /// </summary>
        /// <value>The namespaces.</value>
        public ObservableCollection<TerminologyVocabulary> Namespaces
        {
            get { return _namespaces; }

            set
            {
                _namespaces = value;
                RaisePropertyChanged ( () => Namespaces );
            }
        }

        /// <summary>
        /// Gets or sets the perform search command.
        /// </summary>
        /// <value>The perform search command.</value>
        public ICommand PerformSearchCommand { get; set; }

        /// <summary>
        /// Gets or sets the root concepts.
        /// </summary>
        /// <value>The root concepts.</value>
        public ObservableCollection<TerminologyConcept> RootConcepts
        {
            get { return _rootConcepts; }
            set
            {
                _rootConcepts = value;
                RaisePropertyChanged ( () => RootConcepts );
            }
        }

        /// <summary>
        /// Gets or sets the search criteria.
        /// </summary>
        /// <value>The search criteria.</value>
        public string SearchCriteria { get; set; }

        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        /// <value>The search results.</value>
        public ObservableCollection<TerminologyConcept> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                RaisePropertyChanged ( () => SearchResults );
            }
        }

        /// <summary>
        /// Gets or sets the selected namespace.
        /// </summary>
        /// <value>The selected namespace.</value>
        public TerminologyVocabulary SelectedNamespace
        {
            get { return _selectedNamespace; }
            set
            {
                _selectedNamespace = value;
                RaisePropertyChanged ( () => SelectedNamespace );
            }
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
            _proxy.GetVocabularyListAsync ();
        }

        private void ExecuteNamespaceChanged ( TerminologyVocabulary namespaceDto )
        {
        }

        private void ExecutePerformSearch ( string obj )
        {
            _proxy.FindConceptsWithNameMatchingAsync ( obj, _selectedNamespace.Id, null );
        }

        private void FindConceptsWithNameMatchingCompleted ( object sender, ProxyEventArgs e )
        {
            SearchResults = e.LoadResult<ObservableCollection<TerminologyConcept>> ();
        }

        private void GetVocabularyListCompleted ( object sender, ProxyEventArgs e )
        {
            if ( e.Exception == null )
            {
                var list = e.LoadResult<ObservableCollection<TerminologyVocabulary>> ();

                var snomed = list.Where ( n => n.BusinessCode == "SNOMED" ).FirstOrDefault ();

                Namespaces = list;
                SelectedNamespace = snomed;
            }
            else
            {
                _userDialogService
                    .ShowDialog ( e.Exception.Message, "Could not retrieve the Vocabulary List", UserDialogServiceOptions.Ok );
            }
        }

        #endregion
    }
}
