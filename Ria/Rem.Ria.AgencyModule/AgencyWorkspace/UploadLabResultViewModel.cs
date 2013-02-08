using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.AgencyWorkspace
{
    /// <summary>
    /// View Model for UploadLabResult class.
    /// </summary>
    public class UploadLabResultViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private static readonly long MaxFileSize = 4000000;
        private readonly INavigationService _navigationService;
        private readonly ObservableCollection<FileInfo> _selectedFiles;
        private readonly IUserDialogService _userDialogService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadLabResultViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public UploadLabResultViewModel (
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _navigationService = navigationService;
            _selectedFiles = new ObservableCollection<FileInfo> ();
            SelectedFiles = new ReadOnlyObservableCollection<FileInfo> ( _selectedFiles );

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            NextCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => NextCommand, ExecuteNextCommand );
            AddFilesCommand = commandFactoryHelper.BuildDelegateCommand<IEnumerable<FileInfo>> ( () => AddFilesCommand, ExecuteAddFilesCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the add files command.
        /// </summary>
        /// <value>The add files command.</value>
        public ICommand AddFilesCommand { get; set; }

        /// <summary>
        /// Gets or sets the next command.
        /// </summary>
        /// <value>The next command.</value>
        public ICommand NextCommand { get; set; }

        /// <summary>
        /// Gets the selected files.
        /// </summary>
        public ReadOnlyObservableCollection<FileInfo> SelectedFiles { get; private set; }

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
        }

        private void ExecuteAddFilesCommand ( IEnumerable<FileInfo> files )
        {
            if ( files.Count () > 1 )
            {
                _userDialogService.ShowDialog (
                    "You can only add one file.",
                    "Error adding file",
                    UserDialogServiceOptions.Ok );
            }
            else if ( files.Count () < 1 )
            {
                _userDialogService.ShowDialog (
                    "You must add at least one file.",
                    "Error adding file",
                    UserDialogServiceOptions.Ok );
            }
            else
            {
                var fileInfo = files.ElementAt ( 0 );
                if ( string.IsNullOrEmpty ( fileInfo.Extension ) )
                {
                    _userDialogService.ShowDialog (
                        "You cannot add a directory.",
                        "Error adding file",
                        UserDialogServiceOptions.Ok );
                }
                else
                {
                    _selectedFiles.Clear ();
                    _selectedFiles.Add ( fileInfo );
                }
            }
        }

        private void ExecuteNextCommand ( object obj )
        {
            if ( SelectedFiles.Count () == 0 )
            {
                _userDialogService.ShowDialog ( "There are no files to upload. Please select a file.", "File Upload", UserDialogServiceOptions.Ok );
            }

            IsLoading = true;

            foreach ( var selectedFile in SelectedFiles )
            {
                if ( selectedFile.Length > MaxFileSize )
                {
                    _userDialogService.ShowDialog ( "The file greater than 4MB cannot be uploaded.", "File Upload", UserDialogServiceOptions.Ok );
                }
                else
                {
                    try
                    {
                        Stream stream = selectedFile.OpenRead ();
                        var buffer = new byte[stream.Length];
                        stream.Read ( buffer, 0, ( int )stream.Length );
                        stream.Close ();
                        stream.Dispose ();

                        _navigationService.Navigate (
                            "ModalPopupRegion",
                            "SaveLabResultView",
                            null,
                            new[]
                                {
                                    new KeyValuePair<string, string> (
                                        "Message", Encoding.GetEncoding ( "utf-8" ).GetString ( buffer, 0, buffer.Length ) )
                                } );
                    }
                    catch ( Exception e )
                    {
                        IsLoading = false;
                        _userDialogService.ShowDialog ( e.Message, "Upload Lab Result", UserDialogServiceOptions.Ok );
                    }
                }
            }

            IsLoading = false;
        }

        #endregion
    }
}
