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

using System.Collections.ObjectModel;
using System.Linq;
using Rem.Infrastructure.Service.DataTransferObject;
using TerminologyService.WebService;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for MedicationDtsInfo class.
    /// </summary>
    public class MedicationDtsInfoDto : AbstractDataTransferObject
    {
        #region Constants and Fields

        private ObservableCollection<TerminologyConcept> _forms;
        private TerminologyConcept _selectedForm;
        private TerminologyConcept _selectedStrength;
        private ObservableCollection<TerminologyConcept> _strengths;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the drugs.
        /// </summary>
        /// <value>The drugs.</value>
        public ObservableCollection<TerminologyConcept> Drugs { get; set; }

        /// <summary>
        /// Gets or sets the forms.
        /// </summary>
        /// <value>The forms.</value>
        public ObservableCollection<TerminologyConcept> Forms
        {
            get
            {
                if ( SelectedStrength == null )
                {
                    return _forms;
                }
                return
                    new ObservableCollection<TerminologyConcept> (
                        _forms.Where (
                            f =>
                            Drugs.Where (
                                d =>
                                d.Associations.SelectMany ( a => a.Value ).Contains ( f.Code ) &&
                                d.Associations.SelectMany ( a => a.Value ).Contains ( SelectedStrength.Code ) ).Count () > 0 ) );
            }

            set { _forms = value; }
        }

        /// <summary>
        /// Gets or sets the selected form.
        /// </summary>
        /// <value>The selected form.</value>
        public TerminologyConcept SelectedForm
        {
            get { return _selectedForm; }
            set
            {
                ApplyPropertyChange ( ref _selectedForm, () => SelectedForm, value );
                RaisePropertyChanged ( () => Strengths );
            }
        }

        /// <summary>
        /// Gets or sets the selected strength.
        /// </summary>
        /// <value>The selected strength.</value>
        public TerminologyConcept SelectedStrength
        {
            get { return _selectedStrength; }
            set
            {
                ApplyPropertyChange ( ref _selectedStrength, () => SelectedStrength, value );
                RaisePropertyChanged ( () => Forms );
            }
        }

        /// <summary>
        /// Gets or sets the strengths.
        /// </summary>
        /// <value>The strengths.</value>
        public ObservableCollection<TerminologyConcept> Strengths
        {
            get
            {
                if ( SelectedForm == null )
                {
                    return _strengths;
                }
                return
                    new ObservableCollection<TerminologyConcept> (
                        _strengths.Where (
                            s =>
                            Drugs.Where (
                                d =>
                                d.Associations.SelectMany ( a => a.Value ).Contains ( s.Code ) &&
                                d.Associations.SelectMany ( a => a.Value ).Contains ( SelectedForm.Code ) ).Count () > 0 ) );
            }

            set { _strengths = value; }
        }

        #endregion
    }
}
