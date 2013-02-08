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
using System.Windows;
using System.Windows.Controls;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;
using Rem.WellKnownNames.VisitModule;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace Rem.Ria.PatientModule
{
    /// <summary>
    /// Class for selecting appointment style.
    /// </summary>
    public class AppointmentStyleSelector : OrientedAppointmentItemStyleSelector
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the fixed horizontal appointment base style.
        /// </summary>
        /// <value>The fixed horizontal appointment base style.</value>
        public Style FixedHorizontalAppointmentBaseStyle { get; set; }

        /// <summary>
        /// Gets or sets the fixed vertical appointment base style.
        /// </summary>
        /// <value>The fixed vertical appointment base style.</value>
        public Style FixedVerticalAppointmentBaseStyle { get; set; }

        /// <summary>
        /// Gets or sets the horizontal appointment base style.
        /// </summary>
        /// <value>The horizontal appointment base style.</value>
        public Style HorizontalAppointmentBaseStyle { get; set; }

        /// <summary>
        /// Gets or sets the vertical appointment base style.
        /// </summary>
        /// <value>The vertical appointment base style.</value>
        public Style VerticalAppointmentBaseStyle { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Selects the style.
        /// </summary>
        /// <param name="item">The item to select style for.</param>
        /// <param name="container">The container.</param>
        /// <param name="activeViewDefinition">The active view definition.</param>
        /// <returns>A <see cref="System.Windows.Style"/></returns>
        public override Style SelectStyle ( object item, DependencyObject container, ViewDefinitionBase activeViewDefinition )
        {
            var appointment = item as IAppointment;
            if ( appointment == null )
            {
                return base.SelectStyle ( item, container, activeViewDefinition );
            }

            if ( activeViewDefinition.GetOrientation () == Orientation.Vertical )
            {
                return IsReadOnly ( appointment ) ? FixedVerticalAppointmentBaseStyle : VerticalAppointmentBaseStyle;
            }
            return IsReadOnly ( appointment ) ? FixedHorizontalAppointmentBaseStyle : HorizontalAppointmentBaseStyle;
        }

        #endregion

        #region Methods

        private static bool IsReadOnly ( IAppointment appointment )
        {
            var appointmentDto = appointment as ClinicianAppointmentDto;

            if ( appointmentDto == null )
            {
                return false;
            }

            return
                !( appointmentDto.VisitStatus == null
                   || !appointmentDto.VisitStatus.WellKnownName.Equals ( VisitStatus.CheckedIn, StringComparison.InvariantCultureIgnoreCase ) );
        }

        #endregion
    }
}
