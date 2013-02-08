using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Common.Utility;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.StaffEditor
{
    /// <summary>
    /// View Model for UploadPhoto class.
    /// </summary>
    public class UploadPhotoViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private static readonly long MaxFileSize = 4000000;
        private readonly IEventAggregator _eventAggregator;
        private readonly ObservableCollection<FileInfo> _selectedFiles;
        private readonly IUserDialogService _userDialogService;
        private StaffPhotoDto _staffPhotoDto;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadPhotoViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public UploadPhotoViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;

            _selectedFiles = new ObservableCollection<FileInfo> ();
            SelectedFiles = new ReadOnlyObservableCollection<FileInfo> ( _selectedFiles );

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            UploadPhotoCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => UploadPhotoCommand, ExecuteUploadPhotoCommand );
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
        /// Gets the selected files.
        /// </summary>
        public ReadOnlyObservableCollection<FileInfo> SelectedFiles { get; private set; }

        /// <summary>
        /// Gets or sets the staff photo dto.
        /// </summary>
        /// <value>The staff photo dto.</value>
        public StaffPhotoDto StaffPhotoDto
        {
            get { return _staffPhotoDto; }
            set { ApplyPropertyChange ( ref _staffPhotoDto, () => StaffPhotoDto, value ); }
        }

        /// <summary>
        /// Gets or sets the upload photo command.
        /// </summary>
        /// <value>The upload photo command.</value>
        public ICommand UploadPhotoCommand { get; set; }

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
            StaffPhotoDto = new StaffPhotoDto ();
            StaffPhotoDto.Key = parameters.GetValue<long> ( "StaffKey" );
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

        private void ExecuteUploadPhotoCommand ( object obj )
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
                        var buffer = ImageHelper.GetCompressedImage ( stream, 250, 250 );
                        stream.Dispose ();
                        stream.Close ();

                        var staffPhotoUploadedEvent = new StaffPhotoUploadedEventArgs
                            {
                                Picture = buffer
                            };

                        _eventAggregator.GetEvent<StaffPhotoUploadedEvent> ().Publish ( staffPhotoUploadedEvent );
                        RaiseViewClosing ();
                    }
                    catch ( IOException e )
                    {
                        IsLoading = false;
                        _userDialogService.ShowDialog ( e.Message, "Upload File", UserDialogServiceOptions.Ok );
                    }
                }
            }

            IsLoading = false;
        }

        #endregion
    }
}
