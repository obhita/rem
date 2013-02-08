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
using System.Windows.Input;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.PatientModule.Web.DroolsTest;

namespace Rem.Ria.PatientModule.InteroperabilityWorkspace
{
    /// <summary>
    /// View Model for DroolsRestTest class.
    /// </summary>
    public class DroolsRestTestViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;

        private string _c32Text;
        private string _droolsServerAddress;
        private string _results;
        private string _xacmlText;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DroolsRestTestViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        public DroolsRestTestViewModel (
            IAccessControlManager accessControlManager, ICommandFactory commandFactory, IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            XacmlText = "<decision>PERMIT</decision>\n" +
                        "<obligation>REMOVE_ILLICIT_DRUGS</obligation>";
            C32Text = "<clinicalDocument>\n" +
                      "  <structuredBody>\n" +
                      "    <component>\n" +
                      "      <condition>\n" +
                      "        <problemName>Alcoholism</problemName>\n" +
                      "        <problemCode>D1234</problemCode>\n" +
                      "      </condition>\n" +
                      "    </component>\n" +
                      "    <component>\n" +
                      "      <medications>\n" +
                      "        <code>307782</code>\n" +
                      "        <displayName>Albuterol</displayName>\n" +
                      "        <dose>0.09 MG</dose>\n" +
                      "      </medications>\n" +
                      "      <medications>\n" +
                      "        <code>307789</code>\n" +
                      "        <displayName>Codeine</displayName>\n" +
                      "        <dose>13 ng/ml</dose>\n" +
                      "      </medications>\n" +
                      "    </component>\n" +
                      "    <component>\n" +
                      "      <results>\n" +
                      "        <testName>CBC with Differential</testName>\n" +
                      "        <loincCode>43789009</loincCode>\n" +
                      "        <result>13.2</result>\n" +
                      "      </results>\n" +
                      "    </component>\n" +
                      "  </structuredBody>\n" +
                      "</clinicalDocument>";

            DroolsServerAddress = "http://172.16.100.83:8080/drools2/kservice/rest/execute";

            ICommandFactoryHelper commandFactoryHelper = CreateCommandFactoryHelper ( commandFactory );

            TestC32Command = commandFactoryHelper.BuildDelegateCommand ( () => TestC32Command, ExecuteTestC32Command );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the C32 text.
        /// </summary>
        /// <value>The C32 text.</value>
        public string C32Text
        {
            get { return _c32Text; }
            set { ApplyPropertyChange ( ref _c32Text, () => C32Text, value ); }
        }

        /// <summary>
        /// Gets or sets the drools server address.
        /// </summary>
        /// <value>The drools server address.</value>
        public string DroolsServerAddress
        {
            get { return _droolsServerAddress; }
            set { ApplyPropertyChange ( ref _droolsServerAddress, () => DroolsServerAddress, value ); }
        }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public string Results
        {
            get { return _results; }
            set { ApplyPropertyChange ( ref _results, () => Results, value ); }
        }

        /// <summary>
        /// Gets or sets the test C32 command.
        /// </summary>
        /// <value>The test C32 command.</value>
        public ICommand TestC32Command { get; set; }

        /// <summary>
        /// Gets or sets the xacml text.
        /// </summary>
        /// <value>The xacml text.</value>
        public string XacmlText
        {
            get { return _xacmlText; }
            set { ApplyPropertyChange ( ref _xacmlText, () => XacmlText, value ); }
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
        }

        private void ExecuteTestC32Command ()
        {
            Results = string.Empty;
            IAsyncRequestDispatcher requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SendDroolsTestRequest { XacmlText = XacmlText, C32Text = C32Text, DroolsServerAddress = DroolsServerAddress } );
            requestDispatcher.ProcessRequests ( HandleSentTestResponse, HandleError );
            IsLoading = true;
        }

        private void HandleError ( ExceptionInfo exceptionInfo )
        {
            Results = exceptionInfo.Message;
            IsLoading = false;
        }

        private void HandleSentTestResponse ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DroolsTestResponse> ();
            Results = response.C32Reponse;
            IsLoading = false;
        }

        #endregion
    }
}
