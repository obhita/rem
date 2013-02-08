using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Data Transfer Object class for mail attachment patient document.
    /// </summary>
    public partial class MailAttachmentPatientDocumentDto
    {
        private PatientSearchResultDto _patient;

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>
        /// The patient.
        /// </value>
        public PatientSearchResultDto Patient
        {
            get { return _patient; }
            set
            {
                ApplyPropertyChange(ref _patient, () => Patient, value);
                if (Key == 0)
                {
                    this.PatientKey = _patient != null ? _patient.Key : 0;
                }
            }
        }
    }
}
