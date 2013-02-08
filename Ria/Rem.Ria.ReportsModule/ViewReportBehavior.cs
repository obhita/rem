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

using System.Windows;
using System.Windows.Browser;
using System.Windows.Interactivity;
using PerpetuumSoft.ReportingServices.Viewer.Client;
using PerpetuumSoft.ReportingServices.Viewer.Client.Utils;
using Rem.Ria.ReportsModule.Web;

namespace Rem.Ria.ReportsModule
{
    /// <summary>
    /// Behavior for view report.
    /// </summary>
    public class ViewReportBehavior : TargetedTriggerAction<FrameworkElement>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ReportDtoProperty Property.
        /// </summary>
        public static readonly DependencyProperty ReportDtoProperty =
            DependencyProperty.Register (
                "ReportDto",
                typeof( ReportDto ),
                typeof( ViewReportBehavior ),
                null );

        /// <summary>
        /// Dependency Property for ReportsRootUriProperty Property.
        /// </summary>
        public static readonly DependencyProperty ReportsRootUriProperty =
            DependencyProperty.Register (
                "ReportsRootUri",
                typeof( string ),
                typeof( ViewReportBehavior ),
                null );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the report dto.
        /// </summary>
        /// <value>The report dto.</value>
        public ReportDto ReportDto
        {
            get { return ( ReportDto )GetValue ( ReportDtoProperty ); }
            set { SetValue ( ReportDtoProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the reports root URI.
        /// </summary>
        /// <value>The reports root URI.</value>
        public string ReportsRootUri
        {
            get { return ( string )GetValue ( ReportsRootUriProperty ); }
            set { SetValue ( ReportsRootUriProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke ( object parameter )
        {
            if ( ReportDto != null && Target != null && Target is ReportViewer && !string.IsNullOrEmpty ( ReportsRootUri ) )
            {
                var reportViewer = ( ReportViewer )Target;
                reportViewer.DebugMode = DebugModeEnum.Full;

                //NOTE: Use HtmlPage.IsEnabled to check for design mode so that design mode will work in Visual Studio as well as Expression Blend.
                if ( HtmlPage.IsEnabled == false )
                {
                    //NOTE: Only used in design-time.
                    reportViewer.ServiceUrl = "https://localhost/Rem.Web/Silverlight/Service/ReportService.svc";
                }
                else
                {
                    //TODO: Find another way, instead of using initParams or make it configurable.
                    var initParamsHelper = new SilverlightInitParamsHelper ( HtmlPage.Plugin.GetProperty ( "initParams" ).ToString () );
                    reportViewer.ServiceUrl = initParamsHelper.ServiceUrl;
                }

                reportViewer.ReportName = "/" + ReportsRootUri + "/" + ReportDto.ResourceName;
                reportViewer.ApplyTemplate ();
                reportViewer.RenderDocument ();
            }
        }

        /// <summary>
        /// Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            ( AssociatedObject as FrameworkElement ).Loaded += ViewReportTargetedTrigger_Loaded;
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            ( AssociatedObject as FrameworkElement ).Loaded -= ViewReportTargetedTrigger_Loaded;
        }

        private void ViewReportTargetedTrigger_Loaded ( object sender, RoutedEventArgs e )
        {
        }

        #endregion
    }
}
