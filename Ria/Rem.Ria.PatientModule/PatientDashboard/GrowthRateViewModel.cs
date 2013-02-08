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
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for GrowthRate class.
    /// </summary>
    public class GrowthRateViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private bool _dataLoaded;
        private LookupValueDto _gender;
        private long _patientKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GrowthRateViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public GrowthRateViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the collection height data.
        /// </summary>
        /// <value>The collection height data.</value>
        public ObservableCollection<GrowthRateHeightDto> CollectionHeightData { get; set; }

        /// <summary>
        /// Gets or sets the collection weight data.
        /// </summary>
        /// <value>The collection weight data.</value>
        public ObservableCollection<GrowthRateWeightDto> CollectionWeightData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [data loaded].
        /// </summary>
        /// <value><c>true</c> if [data loaded]; otherwise, <c>false</c>.</value>
        public bool DataLoaded
        {
            get { return _dataLoaded; }
            set { ApplyPropertyChange ( ref _dataLoaded, () => DataLoaded, value ); }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public LookupValueDto Gender
        {
            get { return _gender; }
            set { ApplyPropertyChange ( ref _gender, () => Gender, value ); }
        }

        /// <summary>
        /// Gets or sets the height data10th percentile.
        /// </summary>
        /// <value>The height data10th percentile.</value>
        public ObservableCollection<GrowthRateHeightDto> HeightData10thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the height data25th percentile.
        /// </summary>
        /// <value>The height data25th percentile.</value>
        public ObservableCollection<GrowthRateHeightDto> HeightData25thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the height data50th percentile.
        /// </summary>
        /// <value>The height data50th percentile.</value>
        public ObservableCollection<GrowthRateHeightDto> HeightData50thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the height data5th percentile.
        /// </summary>
        /// <value>The height data5th percentile.</value>
        public ObservableCollection<GrowthRateHeightDto> HeightData5thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the height data75th percentile.
        /// </summary>
        /// <value>The height data75th percentile.</value>
        public ObservableCollection<GrowthRateHeightDto> HeightData75thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the height data90th percentile.
        /// </summary>
        /// <value>The height data90th percentile.</value>
        public ObservableCollection<GrowthRateHeightDto> HeightData90thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the height data95th percentile.
        /// </summary>
        /// <value>The height data95th percentile.</value>
        public ObservableCollection<GrowthRateHeightDto> HeightData95thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the weight data10th percentile.
        /// </summary>
        /// <value>The weight data10th percentile.</value>
        public ObservableCollection<GrowthRateWeightDto> WeightData10thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the weight data25th percentile.
        /// </summary>
        /// <value>The weight data25th percentile.</value>
        public ObservableCollection<GrowthRateWeightDto> WeightData25thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the weight data50th percentile.
        /// </summary>
        /// <value>The weight data50th percentile.</value>
        public ObservableCollection<GrowthRateWeightDto> WeightData50thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the weight data5th percentile.
        /// </summary>
        /// <value>The weight data5th percentile.</value>
        public ObservableCollection<GrowthRateWeightDto> WeightData5thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the weight data75th percentile.
        /// </summary>
        /// <value>The weight data75th percentile.</value>
        public ObservableCollection<GrowthRateWeightDto> WeightData75thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the weight data90th percentile.
        /// </summary>
        /// <value>The weight data90th percentile.</value>
        public ObservableCollection<GrowthRateWeightDto> WeightData90thPercentile { get; set; }

        /// <summary>
        /// Gets or sets the weight data95th percentile.
        /// </summary>
        /// <value>The weight data95th percentile.</value>
        public ObservableCollection<GrowthRateWeightDto> WeightData95thPercentile { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var patientKey = parameters.GetValue<long> ( "PatientKey" );
            return _patientKey == 0 || _patientKey == patientKey;
        }

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
            var patientKey = parameters.GetValue<long> ( "PatientKey" );
            _patientKey = patientKey;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetGrowthInformationByPatientKeyRequest { Key = patientKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( RequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        private void GetFemaleHeightStandardGrowth ()
        {
            HeightData5thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24 / 12.0, 79.25981989 / 2.5 ),
                    new GrowthRateHeightDto ( 35.5 / 12.0, 87.26378819 / 2.5 ),
                    new GrowthRateHeightDto ( 102.5 / 12.0, 120.9748035 / 2.5 ),
                    new GrowthRateHeightDto ( 122.5 / 12.0, 128.1839668 / 2.5 ),
                    new GrowthRateHeightDto ( 141.5 / 12.0, 137.3800519 / 2.5 ),
                    new GrowthRateHeightDto ( 158.5 / 12.0, 146.7338197 / 2.5 ),
                    new GrowthRateHeightDto ( 175.5 / 12.0, 150.7628729 / 2.5 ),
                    new GrowthRateHeightDto ( 189.5 / 12.0, 151.8171212 / 2.5 ),
                    new GrowthRateHeightDto ( 204.5 / 12.0, 152.2763929 / 2.5 ),
                    new GrowthRateHeightDto ( 216.5 / 12.0, 152.464695 / 2.5 ),
                    new GrowthRateHeightDto ( 229.5 / 12.0, 152.5872574 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 152.6482187 / 2.5 ),
                };
            HeightData10thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24 / 12.0, 80.52476467 / 2.5 ),
                    new GrowthRateHeightDto ( 35.5 / 12.0, 88.6544988 / 2.5 ),
                    new GrowthRateHeightDto ( 51.5 / 12.0, 97.14071535 / 2.5 ),
                    new GrowthRateHeightDto ( 68.5 / 12.0, 106.4153599 / 2.5 ),
                    new GrowthRateHeightDto ( 83.5 / 12.0, 114.407697 / 2.5 ),
                    new GrowthRateHeightDto ( 102.5 / 12.0, 123.029093 / 2.5 ),
                    new GrowthRateHeightDto ( 122.5 / 12.0, 130.5478595 / 2.5 ),
                    new GrowthRateHeightDto ( 141.5 / 12.0, 140.1160824 / 2.5 ),
                    new GrowthRateHeightDto ( 158.5 / 12.0, 149.2449147 / 2.5 ),
                    new GrowthRateHeightDto ( 175.5 / 12.0, 153.1143251 / 2.5 ),
                    new GrowthRateHeightDto ( 189.5 / 12.0, 154.1591564 / 2.5 ),
                    new GrowthRateHeightDto ( 204.5 / 12.0, 154.6280858 / 2.5 ),
                    new GrowthRateHeightDto ( 216.5 / 12.0, 154.8235077 / 2.5 ),
                    new GrowthRateHeightDto ( 229.5 / 12.0, 154.9515511 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 155.0154449 / 2.5 ),
                };

            HeightData25thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24 / 12.0, 82.63523563 / 2.5 ),
                    new GrowthRateHeightDto ( 35.5 / 12.0, 90.99890839 / 2.5 ),
                    new GrowthRateHeightDto ( 51.5 / 12.0, 99.76009185 / 2.5 ),
                    new GrowthRateHeightDto ( 68.5 / 12.0, 109.3229831 / 2.5 ),
                    new GrowthRateHeightDto ( 83.5 / 12.0, 117.5753884 / 2.5 ),
                    new GrowthRateHeightDto ( 102.5 / 12.0, 126.5396411 / 2.5 ),
                    new GrowthRateHeightDto ( 122.5 / 12.0, 134.5620476 / 2.5 ),
                    new GrowthRateHeightDto ( 141.5 / 12.0, 144.6641345 / 2.5 ),
                    new GrowthRateHeightDto ( 158.5 / 12.0, 153.4233611 / 2.5 ),
                    new GrowthRateHeightDto ( 175.5 / 12.0, 157.0516529 / 2.5 ),
                    new GrowthRateHeightDto ( 189.5 / 12.0, 158.0784088 / 2.5 ),
                    new GrowthRateHeightDto ( 204.5 / 12.0, 158.5577008 / 2.5 ),
                    new GrowthRateHeightDto ( 216.5 / 12.0, 158.7611821 / 2.5 ),
                    new GrowthRateHeightDto ( 229.5 / 12.0, 158.8953111 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 158.9623829 / 2.5 ),
                    new GrowthRateHeightDto ( 240 / 12.0, 158.9651429 / 2.5 ),
                };

            HeightData50thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24 / 12.0, 84.97555512 / 2.5 ),
                    new GrowthRateHeightDto ( 35.5 / 12.0, 93.63382278 / 2.5 ),
                    new GrowthRateHeightDto ( 51.5 / 12.0, 102.7406094 / 2.5 ),
                    new GrowthRateHeightDto ( 68.5 / 12.0, 112.6645943 / 2.5 ),
                    new GrowthRateHeightDto ( 83.5 / 12.0, 121.22102 / 2.5 ),
                    new GrowthRateHeightDto ( 102.5 / 12.0, 130.5573839 / 2.5 ),
                    new GrowthRateHeightDto ( 122.5 / 12.0, 139.1171933 / 2.5 ),
                    new GrowthRateHeightDto ( 141.5 / 12.0, 149.6838943 / 2.5 ),
                    new GrowthRateHeightDto ( 158.5 / 12.0, 158.0411233 / 2.5 ),
                    new GrowthRateHeightDto ( 175.5 / 12.0, 161.4380593 / 2.5 ),
                    new GrowthRateHeightDto ( 189.5 / 12.0, 162.4413513 / 2.5 ),
                    new GrowthRateHeightDto ( 204.5 / 12.0, 162.9238449 / 2.5 ),
                    new GrowthRateHeightDto ( 216.5 / 12.0, 163.1307866 / 2.5 ),
                    new GrowthRateHeightDto ( 229.5 / 12.0, 163.2672954 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 163.3354491 / 2.5 ),
                };

            HeightData75thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24 / 12.0, 87.31121356 / 2.5 ),
                    new GrowthRateHeightDto ( 35.5 / 12.0, 96.30028751 / 2.5 ),
                    new GrowthRateHeightDto ( 51.5 / 12.0, 105.7965086 / 2.5 ),
                    new GrowthRateHeightDto ( 68.5 / 12.0, 116.1278283 / 2.5 ),
                    new GrowthRateHeightDto ( 83.5 / 12.0, 125.0051231 / 2.5 ),
                    new GrowthRateHeightDto ( 102.5 / 12.0, 134.7023324 / 2.5 ),
                    new GrowthRateHeightDto ( 122.5 / 12.0, 143.7734919 / 2.5 ),
                    new GrowthRateHeightDto ( 141.5 / 12.0, 154.6700274 / 2.5 ),
                    new GrowthRateHeightDto ( 158.5 / 12.0, 162.633793 / 2.5 ),
                    new GrowthRateHeightDto ( 175.5 / 12.0, 165.8365737 / 2.5 ),
                    new GrowthRateHeightDto ( 189.5 / 12.0, 166.8129074 / 2.5 ),
                    new GrowthRateHeightDto ( 204.5 / 12.0, 167.2900467 / 2.5 ),
                    new GrowthRateHeightDto ( 216.5 / 12.0, 167.4948296 / 2.5 ),
                    new GrowthRateHeightDto ( 229.5 / 12.0, 167.6292563 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 167.6960062 / 2.5 ),
                    new GrowthRateHeightDto ( 240 / 12.0, 167.6987436 / 2.5 ),
                };

            HeightData90thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24 / 12.0, 89.40951159 / 2.5 ),
                    new GrowthRateHeightDto ( 35.5 / 12.0, 98.72704954 / 2.5 ),
                    new GrowthRateHeightDto ( 51.5 / 12.0, 108.6126601 / 2.5 ),
                    new GrowthRateHeightDto ( 68.5 / 12.0, 119.3530862 / 2.5 ),
                    new GrowthRateHeightDto ( 83.5 / 12.0, 128.5344832 / 2.5 ),
                    new GrowthRateHeightDto ( 102.5 / 12.0, 138.5449986 / 2.5 ),
                    new GrowthRateHeightDto ( 122.5 / 12.0, 148.0517004 / 2.5 ),
                    new GrowthRateHeightDto ( 141.5 / 12.0, 159.1302018 / 2.5 ),
                    new GrowthRateHeightDto ( 158.5 / 12.0, 166.7466859 / 2.5 ),
                    new GrowthRateHeightDto ( 175.5 / 12.0, 169.8055142 / 2.5 ),
                    new GrowthRateHeightDto ( 189.5 / 12.0, 170.7546494 / 2.5 ),
                    new GrowthRateHeightDto ( 204.5 / 12.0, 171.2198128 / 2.5 ),
                    new GrowthRateHeightDto ( 216.5 / 12.0, 171.4179802 / 2.5 ),
                    new GrowthRateHeightDto ( 229.5 / 12.0, 171.5468394 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 171.6102738 / 2.5 ),
                };

            HeightData95thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24 / 12.0, 90.66354871 / 2.5 ),
                    new GrowthRateHeightDto ( 35.5 / 12.0, 100.191497 / 2.5 ),
                    new GrowthRateHeightDto ( 51.5 / 12.0, 110.3283052 / 2.5 ),
                    new GrowthRateHeightDto ( 68.5 / 12.0, 121.3340057 / 2.5 ),
                    new GrowthRateHeightDto ( 83.5 / 12.0, 130.7046982 / 2.5 ),
                    new GrowthRateHeightDto ( 102.5 / 12.0, 140.8967672 / 2.5 ),
                    new GrowthRateHeightDto ( 122.5 / 12.0, 150.6519828 / 2.5 ),
                    new GrowthRateHeightDto ( 141.5 / 12.0, 161.7874182 / 2.5 ),
                    new GrowthRateHeightDto ( 158.5 / 12.0, 169.1990151 / 2.5 ),
                    new GrowthRateHeightDto ( 175.5 / 12.0, 172.1852824 / 2.5 ),
                    new GrowthRateHeightDto ( 189.5 / 12.0, 173.1168298 / 2.5 ),
                    new GrowthRateHeightDto ( 204.5 / 12.0, 173.5716411 / 2.5 ),
                    new GrowthRateHeightDto ( 216.5 / 12.0, 173.7637802 / 2.5 ),
                    new GrowthRateHeightDto ( 229.5 / 12.0, 173.8876774 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 173.9482244 / 2.5 ),
                };
        }

        private void GetFemaleWeightStandardGrowth ()
        {
            WeightData5thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24.0 / 12.0, 10.2102741 * 2.2046 ),
                    new GrowthRateWeightDto ( 33.5 / 12.0, 11.32053778 * 2.2046 ),
                    new GrowthRateWeightDto ( 43.5 / 12.0, 12.46870194 * 2.2046 ),
                    new GrowthRateWeightDto ( 58.5 / 12.0, 14.42947441 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 16.72986243 * 2.2046 ),
                    new GrowthRateWeightDto ( 89.5 / 12.0, 19.00510849 * 2.2046 ),
                    new GrowthRateWeightDto ( 107.5 / 12.0, 22.12277423 * 2.2046 ),
                    new GrowthRateWeightDto ( 124.5 / 12.0, 25.80949245 * 2.2046 ),
                    new GrowthRateWeightDto ( 142.5 / 12.0, 30.58170029 * 2.2046 ),
                    new GrowthRateWeightDto ( 161.5 / 12.0, 36.0210521 * 2.2046 ),
                    new GrowthRateWeightDto ( 179.5 / 12.0, 40.55391906 * 2.2046 ),
                    new GrowthRateWeightDto ( 197.5 / 12.0, 43.65406467 * 2.2046 ),
                    new GrowthRateWeightDto ( 213.5 / 12.0, 45.16728964 * 2.2046 ),
                    new GrowthRateWeightDto ( 229.5 / 12.0, 46.01109826 * 2.2046 ),
                    new GrowthRateWeightDto ( 240 / 12.0, 46.28963394 * 2.2046 )
                };

            WeightData10thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24 / 12.0, 10.57373483 * 2.2046 ),
                    new GrowthRateWeightDto ( 33.5 / 12.0, 11.7380584 * 2.2046 ),
                    new GrowthRateWeightDto ( 43.5 / 12.0, 12.95422865 * 2.2046 ),
                    new GrowthRateWeightDto ( 58.5 / 12.0, 15.02287453 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 17.4558574 * 2.2046 ),
                    new GrowthRateWeightDto ( 89.5 / 12.0, 19.90554403 * 2.2046 ),
                    new GrowthRateWeightDto ( 107.5 / 12.0, 23.32471257 * 2.2046 ),
                    new GrowthRateWeightDto ( 124.5 / 12.0, 27.35847906 * 2.2046 ),
                    new GrowthRateWeightDto ( 142.5 / 12.0, 32.45781366 * 2.2046 ),
                    new GrowthRateWeightDto ( 161.5 / 12.0, 38.06087423 * 2.2046 ),
                    new GrowthRateWeightDto ( 179.5 / 12.0, 42.54805916 * 2.2046 ),
                    new GrowthRateWeightDto ( 197.5 / 12.0, 45.5345486 * 2.2046 ),
                    new GrowthRateWeightDto ( 213.5 / 12.0, 47.03492965 * 2.2046 ),
                    new GrowthRateWeightDto ( 229.5 / 12.0, 48.00447244 * 2.2046 ),
                    new GrowthRateWeightDto ( 240 / 12.0, 48.38346004 * 2.2046 ),
                };

            WeightData25thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24 / 12.0, 11.23356786 * 2.2046 ),
                    new GrowthRateWeightDto ( 33.5 / 12.0, 12.50791249 * 2.2046 ),
                    new GrowthRateWeightDto ( 43.5 / 12.0, 13.86185877 * 2.2046 ),
                    new GrowthRateWeightDto ( 58.5 / 12.0, 16.14971251 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 18.84908224 * 2.2046 ),
                    new GrowthRateWeightDto ( 89.5 / 12.0, 21.64282737 * 2.2046 ),
                    new GrowthRateWeightDto ( 107.5 / 12.0, 25.650463929 * 2.2046 ),
                    new GrowthRateWeightDto ( 124.5 / 12.0, 30.358879713 * 2.2046 ),
                    new GrowthRateWeightDto ( 142.5 / 12.0, 36.097163051 * 2.2046 ),
                    new GrowthRateWeightDto ( 161.5 / 12.0, 42.03435353 * 2.2046 ),
                    new GrowthRateWeightDto ( 179.5 / 12.0, 46.457122282 * 2.2046 ),
                    new GrowthRateWeightDto ( 197.5 / 12.0, 49.24026312 * 2.2046 ),
                    new GrowthRateWeightDto ( 213.5 / 12.0, 50.71801935 * 2.2046 ),
                    new GrowthRateWeightDto ( 229.5 / 12.0, 51.918248591 * 2.2046 ),
                    new GrowthRateWeightDto ( 240 / 12.0, 52.478764331 * 2.2046 ),
                };

            WeightData50thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24 / 12.0, 12.05503983 * 2.2046 ),
                    new GrowthRateWeightDto ( 33.5 / 12.0, 13.48913319 * 2.2046 ),
                    new GrowthRateWeightDto ( 43.5 / 12.0, 15.04340553 * 2.2046 ),
                    new GrowthRateWeightDto ( 58.5 / 12.0, 17.65360733 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 20.74013277 * 2.2046 ),
                    new GrowthRateWeightDto ( 89.5 / 12.0, 24.01917703 * 2.2046 ),
                    new GrowthRateWeightDto ( 107.5 / 12.0, 28.83983907 * 2.2046 ),
                    new GrowthRateWeightDto ( 124.5 / 12.0, 34.47272283 * 2.2046 ),
                    new GrowthRateWeightDto ( 142.5 / 12.0, 41.09701099 * 2.2046 ),
                    new GrowthRateWeightDto ( 161.5 / 12.0, 47.54082604 * 2.2046 ),
                    new GrowthRateWeightDto ( 179.5 / 12.0, 51.94925841 * 2.2046 ),
                    new GrowthRateWeightDto ( 197.5 / 12.0, 54.50920717 * 2.2046 ),
                    new GrowthRateWeightDto ( 213.5 / 12.0, 55.96573022 * 2.2046 ),
                    new GrowthRateWeightDto ( 229.5 / 12.0, 57.44578172 * 2.2046 ),
                    new GrowthRateWeightDto ( 240 / 12.0, 58.21897289 * 2.2046 ),
                };
            WeightData75thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24 / 12.0, 12.98666951 * 2.2046 ),
                    new GrowthRateWeightDto ( 33.5 / 12.0, 14.6349076 * 2.2046 ),
                    new GrowthRateWeightDto ( 43.5 / 12.0, 16.4609302 * 2.2046 ),
                    new GrowthRateWeightDto ( 58.5 / 12.0, 19.51873814 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 23.13975807 * 2.2046 ),
                    new GrowthRateWeightDto ( 89.5 / 12.0, 27.06243533 * 2.2046 ),
                    new GrowthRateWeightDto ( 107.5 / 12.0, 32.92513255 * 2.2046 ),
                    new GrowthRateWeightDto ( 124.5 / 12.0, 39.72676238 * 2.2046 ),
                    new GrowthRateWeightDto ( 142.5 / 12.0, 47.4966064 * 2.2046 ),
                    new GrowthRateWeightDto ( 161.5 / 12.0, 54.69335479 * 2.2046 ),
                    new GrowthRateWeightDto ( 179.5 / 12.0, 59.26135066 * 2.2046 ),
                    new GrowthRateWeightDto ( 197.5 / 12.0, 61.68546339 * 2.2046 ),
                    new GrowthRateWeightDto ( 213.5 / 12.0, 63.14619584 * 2.2046 ),
                    new GrowthRateWeightDto ( 229.5 / 12.0, 64.89303137 * 2.2046 ),
                    new GrowthRateWeightDto ( 240 / 12.0, 65.85237979 * 2.2046 ),
                };

            WeightData90thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24 / 12.0, 13.93766409 * 2.2046 ),
                    new GrowthRateWeightDto ( 33.5 / 12.0, 15.8436466 * 2.2046 ),
                    new GrowthRateWeightDto ( 43.5 / 12.0, 18.00431807 * 2.2046 ),
                    new GrowthRateWeightDto ( 58.5 / 12.0, 21.63372961 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 25.94050589 * 2.2046 ),
                    new GrowthRateWeightDto ( 89.5 / 12.0, 30.65007797 * 2.2046 ),
                    new GrowthRateWeightDto ( 107.5 / 12.0, 37.72376365 * 2.2046 ),
                    new GrowthRateWeightDto ( 124.5 / 12.0, 45.8567607 * 2.2046 ),
                    new GrowthRateWeightDto ( 142.5 / 12.0, 54.97924502 * 2.2046 ),
                    new GrowthRateWeightDto ( 161.5 / 12.0, 63.24087599 * 2.2046 ),
                    new GrowthRateWeightDto ( 179.5 / 12.0, 68.34849595 * 2.2046 ),
                    new GrowthRateWeightDto ( 197.5 / 12.0, 70.95905471 * 2.2046 ),
                    new GrowthRateWeightDto ( 213.5 / 12.0, 72.50824833 * 2.2046 ),
                    new GrowthRateWeightDto ( 229.5 / 12.0, 74.35682105 * 2.2046 ),
                    new GrowthRateWeightDto ( 240 / 12.0, 75.35164989 * 2.2046 ),
                };

            WeightData95thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24 / 12.0, 14.56636479 * 2.2046 ),
                    new GrowthRateWeightDto ( 33.5 / 12.0, 16.66604785 * 2.2046 ),
                    new GrowthRateWeightDto ( 43.5 / 12.0, 19.08474635 * 2.2046 ),
                    new GrowthRateWeightDto ( 58.5 / 12.0, 23.17221677 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 28.03579694 * 2.2046 ),
                    new GrowthRateWeightDto ( 89.5 / 12.0, 33.35697585 * 2.2046 ),
                    new GrowthRateWeightDto ( 107.5 / 12.0, 41.32087531 * 2.2046 ),
                    new GrowthRateWeightDto ( 124.5 / 12.0, 50.41198402 * 2.2046 ),
                    new GrowthRateWeightDto ( 142.5 / 12.0, 60.5487783 * 2.2046 ),
                    new GrowthRateWeightDto ( 161.5 / 12.0, 69.75442235 * 2.2046 ),
                    new GrowthRateWeightDto ( 179.5 / 12.0, 75.5924255 * 2.2046 ),
                    new GrowthRateWeightDto ( 197.5 / 12.0, 78.72189292 * 2.2046 ),
                    new GrowthRateWeightDto ( 213.5 / 12.0, 80.44120111 * 2.2046 ),
                    new GrowthRateWeightDto ( 229.5 / 12.0, 82.12302792 * 2.2046 ),
                    new GrowthRateWeightDto ( 240 / 12.0, 82.95375457 * 2.2046 ),
                };
        }

        private void GetMaleHeightStandardGrowth ()
        {
            HeightData5thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24.0 / 12.0, 80.72977321 / 2.5 ),
                    new GrowthRateHeightDto ( 24.5 / 12.0, 81.08868489 / 2.5 ),
                    new GrowthRateHeightDto ( 30.5 / 12.0, 85.3469433 / 2.5 ),
                    new GrowthRateHeightDto ( 55.5 / 12.0, 99.026537 / 2.5 ),
                    new GrowthRateHeightDto ( 74.5 / 12.0, 108.2946307 / 2.5 ),
                    new GrowthRateHeightDto ( 91.5 / 12.0, 116.5296686 / 2.5 ),
                    new GrowthRateHeightDto ( 109.5 / 12.0, 124.1770596 / 2.5 ),
                    new GrowthRateHeightDto ( 128.5 / 12.0, 130.9535693 / 2.5 ),
                    new GrowthRateHeightDto ( 145.5 / 12.0, 137.7998222 / 2.5 ),
                    new GrowthRateHeightDto ( 165.5 / 12.0, 148.8120346 / 2.5 ),
                    new GrowthRateHeightDto ( 182.5 / 12.0, 157.4906696 / 2.5 ),
                    new GrowthRateHeightDto ( 203.5 / 12.0, 162.9469636 / 2.5 ),
                    new GrowthRateHeightDto ( 221.5 / 12.0, 164.5224406 / 2.5 ),
                    new GrowthRateHeightDto ( 234.5 / 12.0, 164.9430978 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 165.031488 / 2.5 ),
                };
            HeightData10thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24.0 / 12.0, 81.99171445 / 2.5 ),
                    new GrowthRateHeightDto ( 24.5 / 12.0, 82.36400989 / 2.5 ),
                    new GrowthRateHeightDto ( 30.5 / 12.0, 86.64027154 / 2.5 ),
                    new GrowthRateHeightDto ( 55.5 / 12.0, 100.6704587 / 2.5 ),
                    new GrowthRateHeightDto ( 74.5 / 12.0, 110.1628906 / 2.5 ),
                    new GrowthRateHeightDto ( 91.5 / 12.0, 118.5139049 / 2.5 ),
                    new GrowthRateHeightDto ( 109.5 / 12.0, 126.3472542 / 2.5 ),
                    new GrowthRateHeightDto ( 128.5 / 12.0, 133.3714475 / 2.5 ),
                    new GrowthRateHeightDto ( 145.5 / 12.0, 140.409134 / 2.5 ),
                    new GrowthRateHeightDto ( 165.5 / 12.0, 151.8347622 / 2.5 ),
                    new GrowthRateHeightDto ( 182.5 / 12.0, 160.5669211 / 2.5 ),
                    new GrowthRateHeightDto ( 203.5 / 12.0, 165.7213786 / 2.5 ),
                    new GrowthRateHeightDto ( 221.5 / 12.0, 167.1680405 / 2.5 ),
                    new GrowthRateHeightDto ( 234.5 / 12.0, 167.5650814 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 167.6519311 / 2.5 ),
                };

            HeightData25thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24.0 / 12.0, 84.10289217 / 2.5 ),
                    new GrowthRateHeightDto ( 24.5 / 12.0, 84.49470553 / 2.5 ),
                    new GrowthRateHeightDto ( 30.5 / 12.0, 88.83745411 / 2.5 ),
                    new GrowthRateHeightDto ( 55.5 / 12.0, 103.4074438 / 2.5 ),
                    new GrowthRateHeightDto ( 74.5 / 12.0, 113.2789389 / 2.5 ),
                    new GrowthRateHeightDto ( 91.5 / 12.0, 121.8629855 / 2.5 ),
                    new GrowthRateHeightDto ( 109.5 / 12.0, 130.0221657 / 2.5 ),
                    new GrowthRateHeightDto ( 128.5 / 12.0, 137.4596717 / 2.5 ),
                    new GrowthRateHeightDto ( 145.5 / 12.0, 144.8317108 / 2.5 ),
                    new GrowthRateHeightDto ( 165.5 / 12.0, 156.8253238 / 2.5 ),
                    new GrowthRateHeightDto ( 182.5 / 12.0, 165.5513919 / 2.5 ),
                    new GrowthRateHeightDto ( 203.5 / 12.0, 170.280441 / 2.5 ),
                    new GrowthRateHeightDto ( 221.5 / 12.0, 171.5599062 / 2.5 ),
                    new GrowthRateHeightDto ( 234.5 / 12.0, 171.9292179 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 172.0152737 / 2.5 ),
                };

            HeightData50thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24.0 / 12.0, 86.45220101 / 2.5 ),
                    new GrowthRateHeightDto ( 24.5 / 12.0, 86.86160934 / 2.5 ),
                    new GrowthRateHeightDto ( 30.5 / 12.0, 91.33242379 / 2.5 ),
                    new GrowthRateHeightDto ( 55.5 / 12.0, 106.4343146 / 2.5 ),
                    new GrowthRateHeightDto ( 74.5 / 12.0, 116.732925 / 2.5 ),
                    new GrowthRateHeightDto ( 91.5 / 12.0, 125.6331012 / 2.5 ),
                    new GrowthRateHeightDto ( 109.5 / 12.0, 134.1768801 / 2.5 ),
                    new GrowthRateHeightDto ( 128.5 / 12.0, 142.072452 / 2.5 ),
                    new GrowthRateHeightDto ( 145.5 / 12.0, 149.8376391 / 2.5 ),
                    new GrowthRateHeightDto ( 165.5 / 12.0, 162.2865119 / 2.5 ),
                    new GrowthRateHeightDto ( 182.5 / 12.0, 170.881464 / 2.5 ),
                    new GrowthRateHeightDto ( 203.5 / 12.0, 175.2397926 / 2.5 ),
                    new GrowthRateHeightDto ( 221.5 / 12.0, 176.3988725 / 2.5 ),
                    new GrowthRateHeightDto ( 234.5 / 12.0, 176.753807 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 176.8414914 / 2.5 ),
                };
            HeightData75thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24.0 / 12.0, 88.80524943 / 2.5 ),
                    new GrowthRateHeightDto ( 24.5 / 12.0, 89.22804829 / 2.5 ),
                    new GrowthRateHeightDto ( 30.5 / 12.0, 93.88495599 / 2.5 ),
                    new GrowthRateHeightDto ( 55.5 / 12.0, 109.4468665 / 2.5 ),
                    new GrowthRateHeightDto ( 74.5 / 12.0, 120.1785968 / 2.5 ),
                    new GrowthRateHeightDto ( 91.5 / 12.0, 129.4546858 / 2.5 ),
                    new GrowthRateHeightDto ( 109.5 / 12.0, 138.4073433 / 2.5 ),
                    new GrowthRateHeightDto ( 128.5 / 12.0, 146.7593449 / 2.5 ),
                    new GrowthRateHeightDto ( 145.5 / 12.0, 154.9409947 / 2.5 ),
                    new GrowthRateHeightDto ( 165.5 / 12.0, 167.6650085 / 2.5 ),
                    new GrowthRateHeightDto ( 182.5 / 12.0, 176.0145556 / 2.5 ),
                    new GrowthRateHeightDto ( 203.5 / 12.0, 180.0949883 / 2.5 ),
                    new GrowthRateHeightDto ( 221.5 / 12.0, 181.1968103 / 2.5 ),
                    new GrowthRateHeightDto ( 234.5 / 12.0, 181.5537583 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 181.6455954 / 2.5 ),
                };

            HeightData90thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24.0 / 12.0, 90.92619137 / 2.5 ),
                    new GrowthRateHeightDto ( 24.5 / 12.0, 91.35753004 / 2.5 ),
                    new GrowthRateHeightDto ( 30.5 / 12.0, 96.23239427 / 2.5 ),
                    new GrowthRateHeightDto ( 55.5 / 12.0, 112.1464475 / 2.5 ),
                    new GrowthRateHeightDto ( 74.5 / 12.0, 123.272929 / 2.5 ),
                    new GrowthRateHeightDto ( 91.5 / 12.0, 132.9381089 / 2.5 ),
                    new GrowthRateHeightDto ( 109.5 / 12.0, 142.2799654 / 2.5 ),
                    new GrowthRateHeightDto ( 128.5 / 12.0, 151.0410123 / 2.5 ),
                    new GrowthRateHeightDto ( 145.5 / 12.0, 159.6178646 / 2.5 ),
                    new GrowthRateHeightDto ( 165.5 / 12.0, 172.4392666 / 2.5 ),
                    new GrowthRateHeightDto ( 182.5 / 12.0, 180.4820061 / 2.5 ),
                    new GrowthRateHeightDto ( 203.5 / 12.0, 184.3814989 / 2.5 ),
                    new GrowthRateHeightDto ( 221.5 / 12.0, 185.4814718 / 2.5 ),
                    new GrowthRateHeightDto ( 234.5 / 12.0, 185.8535364 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 185.9511757 / 2.5 ),
                };

            HeightData95thPercentile = new ObservableCollection<GrowthRateHeightDto>
                {
                    new GrowthRateHeightDto ( 24.0 / 12.0, 92.19687928 / 2.5 ),
                    new GrowthRateHeightDto ( 24.5 / 12.0, 92.63176749 / 2.5 ),
                    new GrowthRateHeightDto ( 30.5 / 12.0, 97.66026663 / 2.5 ),
                    new GrowthRateHeightDto ( 55.5 / 12.0, 113.7568423 / 2.5 ),
                    new GrowthRateHeightDto ( 74.5 / 12.0, 125.1217272 / 2.5 ),
                    new GrowthRateHeightDto ( 91.5 / 12.0, 135.0426312 / 2.5 ),
                    new GrowthRateHeightDto ( 109.5 / 12.0, 144.6271767 / 2.5 ),
                    new GrowthRateHeightDto ( 128.5 / 12.0, 153.6320904 / 2.5 ),
                    new GrowthRateHeightDto ( 145.5 / 12.0, 162.4548526 / 2.5 ),
                    new GrowthRateHeightDto ( 165.5 / 12.0, 175.2677481 / 2.5 ),
                    new GrowthRateHeightDto ( 182.5 / 12.0, 183.09176 / 2.5 ),
                    new GrowthRateHeightDto ( 203.5 / 12.0, 186.9110296 / 2.5 ),
                    new GrowthRateHeightDto ( 221.5 / 12.0, 188.0309455 / 2.5 ),
                    new GrowthRateHeightDto ( 234.5 / 12.0, 188.4178347 / 2.5 ),
                    new GrowthRateHeightDto ( 239.5 / 12.0, 188.5198484 / 2.5 ),
                };
        }

        private void GetMaleWeightStandardGrowth ()
        {
            WeightData5thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24.0 / 12.0, 10.64009004 * 2.2046 ),
                    new GrowthRateWeightDto ( 29.5 / 12.0, 11.28292555 * 2.2046 ),
                    new GrowthRateWeightDto ( 41.5 / 12.0, 12.71153714 * 2.2046 ),
                    new GrowthRateWeightDto ( 55.5 / 12.0, 14.54592801 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 17.23031439 * 2.2046 ),
                    new GrowthRateWeightDto ( 95.5 / 12.0, 20.49720276 * 2.2046 ),
                    new GrowthRateWeightDto ( 116.5 / 12.0, 24.17115246 * 2.2046 ),
                    new GrowthRateWeightDto ( 130.5 / 12.0, 27.0445704 * 2.2046 ),
                    new GrowthRateWeightDto ( 145.5 / 12.0, 30.83303885 * 2.2046 ),
                    new GrowthRateWeightDto ( 164.5 / 12.0, 37.00633501 * 2.2046 ),
                    new GrowthRateWeightDto ( 187.5 / 12.0, 45.59195858 * 2.2046 ),
                    new GrowthRateWeightDto ( 208.5 / 12.0, 51.73332523 * 2.2046 ),
                    new GrowthRateWeightDto ( 231.5 / 12.0, 55.10327703 * 2.2046 ),
                    new GrowthRateWeightDto ( 236.5 / 12.0, 55.50616614 * 2.2046 ),
                    new GrowthRateWeightDto ( 237.5 / 12.0, 55.56446839 * 2.2046 ),
                    new GrowthRateWeightDto ( 239.5 / 12.0, 55.64806673 * 2.2046 ),
                };
            WeightData10thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24.0 / 12.0, 11.05265567 * 2.2046 ),
                    new GrowthRateWeightDto ( 29.5 / 12.0, 11.7136834 * 2.2046 ),
                    new GrowthRateWeightDto ( 41.5 / 12.0, 13.18922941 * 2.2046 ),
                    new GrowthRateWeightDto ( 55.5 / 12.0, 15.12032345 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 17.96197153 * 2.2046 ),
                    new GrowthRateWeightDto ( 95.5 / 12.0, 21.40189997 * 2.2046 ),
                    new GrowthRateWeightDto ( 116.5 / 12.0, 25.36060368 * 2.2046 ),
                    new GrowthRateWeightDto ( 130.5 / 12.0, 28.51890714 * 2.2046 ),
                    new GrowthRateWeightDto ( 145.5 / 12.0, 32.66834099 * 2.2046 ),
                    new GrowthRateWeightDto ( 164.5 / 12.0, 39.27138168 * 2.2046 ),
                    new GrowthRateWeightDto ( 187.5 / 12.0, 48.11199053 * 2.2046 ),
                    new GrowthRateWeightDto ( 208.5 / 12.0, 54.27254708 * 2.2046 ),
                    new GrowthRateWeightDto ( 231.5 / 12.0, 57.7631489 * 2.2046 ),
                    new GrowthRateWeightDto ( 236.5 / 12.0, 58.2166225 * 2.2046 ),
                    new GrowthRateWeightDto ( 237.5 / 12.0, 58.28594368 * 2.2046 ),
                    new GrowthRateWeightDto ( 239.5 / 12.0, 58.39247268 * 2.2046 ),
                };

            WeightData25thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24.0 / 12.0, 11.78597528 * 2.2046 ),
                    new GrowthRateWeightDto ( 29.5 / 12.0, 12.48489772 * 2.2046 ),
                    new GrowthRateWeightDto ( 41.5 / 12.0, 14.06249859 * 2.2046 ),
                    new GrowthRateWeightDto ( 55.5 / 12.0, 16.18700795 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 19.34310643 * 2.2046 ),
                    new GrowthRateWeightDto ( 95.5 / 12.0, 23.14493249 * 2.2046 ),
                    new GrowthRateWeightDto ( 116.5 / 12.0, 27.67656629 * 2.2046 ),
                    new GrowthRateWeightDto ( 130.5 / 12.0, 31.38911963 * 2.2046 ),
                    new GrowthRateWeightDto ( 145.5 / 12.0, 36.22034231 * 2.2046 ),
                    new GrowthRateWeightDto ( 164.5 / 12.0, 43.6033728 * 2.2046 ),
                    new GrowthRateWeightDto ( 187.5 / 12.0, 52.90338872 * 2.2046 ),
                    new GrowthRateWeightDto ( 208.5 / 12.0, 59.1276417 * 2.2046 ),
                    new GrowthRateWeightDto ( 231.5 / 12.0, 62.8292204 * 2.2046 ),
                    new GrowthRateWeightDto ( 236.5 / 12.0, 63.36835431 * 2.2046 ),
                    new GrowthRateWeightDto ( 237.5 / 12.0, 63.45727001 * 2.2046 ),
                    new GrowthRateWeightDto ( 239.5 / 12.0, 63.6061772 * 2.2046 ),
                };

            WeightData50thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24.0 / 12.0, 12.6707633 * 2.2046 ),
                    new GrowthRateWeightDto ( 29.5 / 12.0, 13.42519408 * 2.2046 ),
                    new GrowthRateWeightDto ( 41.5 / 12.0, 15.16078454 * 2.2046 ),
                    new GrowthRateWeightDto ( 55.5 / 12.0, 17.56096245 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 21.16779192 * 2.2046 ),
                    new GrowthRateWeightDto ( 95.5 / 12.0, 25.52606977 * 2.2046 ),
                    new GrowthRateWeightDto ( 116.5 / 12.0, 30.89230072 * 2.2046 ),
                    new GrowthRateWeightDto ( 130.5 / 12.0, 35.36561737 * 2.2046 ),
                    new GrowthRateWeightDto ( 145.5 / 12.0, 41.08443363 * 2.2046 ),
                    new GrowthRateWeightDto ( 164.5 / 12.0, 49.41810374 * 2.2046 ),
                    new GrowthRateWeightDto ( 187.5 / 12.0, 59.28479948 * 2.2046 ),
                    new GrowthRateWeightDto ( 208.5 / 12.0, 65.66540015 * 2.2046 ),
                    new GrowthRateWeightDto ( 231.5 / 12.0, 69.60925782 * 2.2046 ),
                    new GrowthRateWeightDto ( 236.5 / 12.0, 70.23857482 * 2.2046 ),
                    new GrowthRateWeightDto ( 237.5 / 12.0, 70.35039626 * 2.2046 ),
                    new GrowthRateWeightDto ( 239.5 / 12.0, 70.55252127 * 2.2046 ),
                };
            WeightData75thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24.0 / 12.0, 13.63691949 * 2.2046 ),
                    new GrowthRateWeightDto ( 29.5 / 12.0, 14.46462206 * 2.2046 ),
                    new GrowthRateWeightDto ( 41.5 / 12.0, 16.4215262 * 2.2046 ),
                    new GrowthRateWeightDto ( 55.5 / 12.0, 19.18610119 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 23.39803173 * 2.2046 ),
                    new GrowthRateWeightDto ( 95.5 / 12.0, 28.57479705 * 2.2046 ),
                    new GrowthRateWeightDto ( 116.5 / 12.0, 35.09808092 * 2.2046 ),
                    new GrowthRateWeightDto ( 130.5 / 12.0, 40.53449819 * 2.2046 ),
                    new GrowthRateWeightDto ( 145.5 / 12.0, 47.28721249 * 2.2046 ),
                    new GrowthRateWeightDto ( 164.5 / 12.0, 56.62695959 * 2.2046 ),
                    new GrowthRateWeightDto ( 187.5 / 12.0, 67.13136309 * 2.2046 ),
                    new GrowthRateWeightDto ( 208.5 / 12.0, 73.84675041 * 2.2046 ),
                    new GrowthRateWeightDto ( 231.5 / 12.0, 78.02460556 * 2.2046 ),
                    new GrowthRateWeightDto ( 236.5 / 12.0, 78.72233893 * 2.2046 ),
                    new GrowthRateWeightDto ( 237.5 / 12.0, 78.85649725 * 2.2046 ),
                    new GrowthRateWeightDto ( 239.5 / 12.0, 79.11762417 * 2.2046 ),
                };

            WeightData90thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24.0 / 12.0, 14.58339799 * 2.2046 ),
                    new GrowthRateWeightDto ( 29.5 / 12.0, 15.49610261 * 2.2046 ),
                    new GrowthRateWeightDto ( 41.5 / 12.0, 17.72544898 * 2.2046 ),
                    new GrowthRateWeightDto ( 55.5 / 12.0, 20.92525904 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 25.87919213 * 2.2046 ),
                    new GrowthRateWeightDto ( 95.5 / 12.0, 32.17478161 * 2.2046 ),
                    new GrowthRateWeightDto ( 116.5 / 12.0, 40.19484291 * 2.2046 ),
                    new GrowthRateWeightDto ( 130.5 / 12.0, 46.72511793 * 2.2046 ),
                    new GrowthRateWeightDto ( 145.5 / 12.0, 54.51174297 * 2.2046 ),
                    new GrowthRateWeightDto ( 164.5 / 12.0, 64.72646954 * 2.2046 ),
                    new GrowthRateWeightDto ( 187.5 / 12.0, 75.87958721 * 2.2046 ),
                    new GrowthRateWeightDto ( 208.5 / 12.0, 83.19560496 * 2.2046 ),
                    new GrowthRateWeightDto ( 231.5 / 12.0, 87.54488282 * 2.2046 ),
                    new GrowthRateWeightDto ( 236.5 / 12.0, 88.25614046 * 2.2046 ),
                    new GrowthRateWeightDto ( 237.5 / 12.0, 88.40645399 * 2.2046 ),
                    new GrowthRateWeightDto ( 239.5 / 12.0, 88.72311137 * 2.2046 ),
                };

            WeightData95thPercentile = new ObservableCollection<GrowthRateWeightDto>
                {
                    new GrowthRateWeightDto ( 24.0 / 12.0, 15.18777349 * 2.2046 ),
                    new GrowthRateWeightDto ( 29.5 / 12.0, 16.16176896 * 2.2046 ),
                    new GrowthRateWeightDto ( 41.5 / 12.0, 18.59705318 * 2.2046 ),
                    new GrowthRateWeightDto ( 55.5 / 12.0, 22.12331387 * 2.2046 ),
                    new GrowthRateWeightDto ( 74.5 / 12.0, 27.6500983 * 2.2046 ),
                    new GrowthRateWeightDto ( 95.5 / 12.0, 34.89950266 * 2.2046 ),
                    new GrowthRateWeightDto ( 116.5 / 12.0, 44.14894598 * 2.2046 ),
                    new GrowthRateWeightDto ( 130.5 / 12.0, 51.45751679 * 2.2046 ),
                    new GrowthRateWeightDto ( 145.5 / 12.0, 59.87225739 * 2.2046 ),
                    new GrowthRateWeightDto ( 164.5 / 12.0, 70.53106367 * 2.2046 ),
                    new GrowthRateWeightDto ( 187.5 / 12.0, 82.11385795 * 2.2046 ),
                    new GrowthRateWeightDto ( 208.5 / 12.0, 90.02860798 * 2.2046 ),
                    new GrowthRateWeightDto ( 231.5 / 12.0, 94.43765234 * 2.2046 ),
                    new GrowthRateWeightDto ( 236.5 / 12.0, 95.11343784 * 2.2046 ),
                    new GrowthRateWeightDto ( 237.5 / 12.0, 95.26893942 * 2.2046 ),
                    new GrowthRateWeightDto ( 239.5 / 12.0, 95.61749303 * 2.2046 ),
                };
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "GetGrowthInformationByPatientKeyRequest Failed", UserDialogServiceOptions.Ok );
        }

        private void RequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetGrowthInformationByPatientKeyResponse> ();

            if ( response.GrowthInfoDto.Gender.WellKnownName.Trim ().Equals ( WellKnownNames.CommonModule.Gender.Male.Trim () ) )
            {
                GetMaleWeightStandardGrowth ();
                GetMaleHeightStandardGrowth ();
            }
            else if ( response.GrowthInfoDto.Gender.WellKnownName.Trim ().Equals ( WellKnownNames.CommonModule.Gender.Female.Trim () ) )
            {
                GetFemaleWeightStandardGrowth ();
                GetFemaleHeightStandardGrowth ();
            }

            CollectionWeightData = response.GrowthInfoDto.GrowthRateWeightDtos;
            CollectionHeightData = response.GrowthInfoDto.GrowthRateHeightDtos;

            var error = string.Empty;

            if ( CollectionHeightData.Count == 0 )
            {
                error = "height";
            }

            if ( CollectionWeightData.Count == 0 )
            {
                if ( !string.IsNullOrEmpty ( error ) )
                {
                    error += " and ";
                }
                error += "weight";
            }

            Gender = response.GrowthInfoDto.Gender;

            DataLoaded = true;
            IsLoading = false;

            if ( !string.IsNullOrEmpty ( error ) )
            {
                _userDialogService.ShowDialog ( "Patient has no data from ages 2 - 20 for " + error, "No Data", UserDialogServiceOptions.Ok );
            }
        }

        #endregion
    }
}
