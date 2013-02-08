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
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.ClinicianDashboard
{
    /// <summary>
    /// View Model for ClinicianLabResultsTile class.
    /// </summary>
    public class ClinicianLabResultsTileViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private ObservableCollection<LabResultDto> _labResults;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicianLabResultsTileViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public ClinicianLabResultsTileViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            //TODO:remove and get real data
            _labResults = new ObservableCollection<LabResultDto> ();
            _labResults.Add (
                new LabOrderDto
                    {
                        PatientName = "Young, Tad",
                        LabTestResultNameCodedConcept = new CodedConceptDto
                            {
                                DisplayName = "Blood Glucose"
                            },
                        ReferenceRange = "34-75",
                        Value = 50,
                        UnitOfMeasureCode = "mg"
                    } );
            _labResults.Add (
                new LabOrderDto
                    {
                        PatientName = "Smith, Albert",
                        LabTestResultNameCodedConcept = new CodedConceptDto
                            {
                                DisplayName = "Heart Rate"
                            },
                        ReferenceRange = "75-120",
                        Value = 90,
                        UnitOfMeasureCode = "bpm"
                    } );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the lab results.
        /// </summary>
        /// <value>The lab results.</value>
        public ObservableCollection<LabResultDto> LabResults
        {
            get { return _labResults; }
            set { ApplyPropertyChange ( ref _labResults, () => LabResults, value ); }
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

        #endregion
    }
}
