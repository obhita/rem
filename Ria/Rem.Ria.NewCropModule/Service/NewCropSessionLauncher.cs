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
using System.Text;
using System.Windows;
using System.Windows.Browser;
using Rem.Ria.Infrastructure.Navigation;
using Rem.WellKnownNames;
using Rem.WellKnownNames.NewCropModule;

namespace Rem.Ria.NewCropModule.Service
{
    /// <summary>
    /// Class for launching new crop session.
    /// </summary>
    public class NewCropSessionLauncher : INewCropSessionLauncher
    {
        #region Constants and Fields

        private readonly INavigationService _navigationService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewCropSessionLauncher"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        public NewCropSessionLauncher ( INavigationService navigationService )
        {
            _navigationService = navigationService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <param name="prescriberKey">The prescriber key.</param>
        /// <param name="agencyKey">The agency key.</param>
        /// <param name="locationKey">The location key.</param>
        /// <returns>A <see cref="System.Uri"/></returns>
        public Uri BuildUrl ( long patientKey, long prescriberKey, long agencyKey, long locationKey )
        {
            var relativePathBuilder = new StringBuilder ();

            relativePathBuilder.Append (
                string.Format (
                    "../{0}?{1}={2}",
                    HttpHandlerPaths.NewCropModuleHttpHandlerPath,
                    HttpUtility.UrlEncode ( NewCropHttpHandlerQueryString.RequestName ),
                    HttpUtility.UrlEncode ( NewCropHttpHandlerRequestName.ERxCompose ) ) );

            relativePathBuilder.Append ( string.Format ( "&{0}={1}", NewCropHttpHandlerQueryString.PatientKey, patientKey ) );
            relativePathBuilder.Append ( string.Format ( "&{0}={1}", NewCropHttpHandlerQueryString.StaffKey, prescriberKey ) );
            relativePathBuilder.Append ( string.Format ( "&{0}={1}", NewCropHttpHandlerQueryString.AgencyKey, agencyKey ) );
            relativePathBuilder.Append ( string.Format ( "&{0}={1}", NewCropHttpHandlerQueryString.LocationKey, locationKey ) );

            if ( Application.Current.Host.Source != null )
            {
                var appSource = Application.Current.Host.Source;
                var uri = new Uri ( appSource, relativePathBuilder.ToString () );
                return uri;
            }
            else
            {
                return new Uri ( "blank:" );
            }
        }

        /// <summary>
        /// Launches the session.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <param name="prescriberKey">The prescriber key.</param>
        /// <param name="agencyKey">The agency key.</param>
        /// <param name="locationKey">The location key.</param>
        /// <param name="insideBrowser">If set to <c>true</c> [inside browser].</param>
        public void LaunchSession ( long patientKey, long prescriberKey, long agencyKey, long locationKey, bool insideBrowser = true )
        {
            if ( insideBrowser )
            {
                LaunchInBrowser ( patientKey, prescriberKey, agencyKey, locationKey );
            }
            else
            {
                LaunchInsideSilverlight ( patientKey, prescriberKey, agencyKey, locationKey );
            }
        }

        #endregion

        #region Methods

        private void LaunchInBrowser ( long patientKey, long prescriberKey, long agencyKey, long locationKey )
        {
            var uri = BuildUrl ( patientKey, prescriberKey, agencyKey, locationKey );

            HtmlPage.Window.Navigate ( uri, "_blank" );
        }

        private void LaunchInsideSilverlight ( long patientKey, long prescriberKey, long agencyKey, long locationKey )
        {
            _navigationService.Navigate (
                "ModalPopupRegion",
                "NewCropModalView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", patientKey.ToString () ),
                        new KeyValuePair<string, string> ( "StaffKey", prescriberKey.ToString () ),
                        new KeyValuePair<string, string> ( "AgencyKey", agencyKey.ToString () ),
                        new KeyValuePair<string, string> ( "LocationKey", locationKey.ToString () )
                    } );
        }

        #endregion
    }
}
