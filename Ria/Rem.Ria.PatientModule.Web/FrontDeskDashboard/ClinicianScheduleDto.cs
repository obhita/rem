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
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Data transfer object for ClinicianSchedule class.
    /// </summary>
    [DataContract]
    public class ClinicianScheduleDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private int _availableAppointment;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicianScheduleDto"/> class.
        /// </summary>
        public ClinicianScheduleDto ()
        {
            ClinicianAppointments = new ObservableCollection<ClinicianAppointmentDto> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the available appointments.
        /// </summary>
        /// <value>The available appointments.</value>
        [DataMember]
        public int AvailableAppointments
        {
            get { return _availableAppointment; }
            set { ApplyPropertyChange ( ref _availableAppointment, () => AvailableAppointments, value ); }
        }

        /// <summary>
        /// Gets or sets the clinician appointments.
        /// </summary>
        /// <value>The clinician appointments.</value>
        [DataMember]
        public ObservableCollection<ClinicianAppointmentDto> ClinicianAppointments { get; set; }

        /// <summary>
        /// Gets or sets the first name of the clinician.
        /// </summary>
        /// <value>The first name of the clinician.</value>
        [DataMember]
        public string ClinicianFirstName { get; set; }

        /// <summary>
        /// Gets the full name of the clinician.
        /// </summary>
        public string ClinicianFullName
        {
            get { return string.Format ( "{0} {1}", ClinicianFirstName, ClinicianLastName ); }
        }

        /// <summary>
        /// Gets or sets the clinician key.
        /// </summary>
        /// <value>The clinician key.</value>
        [DataMember]
        public long ClinicianKey { get; set; }

        /// <summary>
        /// Gets or sets the last name of the clinician.
        /// </summary>
        /// <value>The last name of the clinician.</value>
        [DataMember]
        public string ClinicianLastName { get; set; }

        /// <summary>
        /// Gets or sets the total appointments.
        /// </summary>
        /// <value>The total appointments.</value>
        [DataMember]
        public int TotalAppointments { get; set; }

        #endregion
    }
}
