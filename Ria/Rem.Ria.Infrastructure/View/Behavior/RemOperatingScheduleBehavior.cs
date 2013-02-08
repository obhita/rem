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

using System.Collections;
using System.ComponentModel;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing rem operating schedule.
    /// </summary>
    public class RemOperatingScheduleBehavior : Behavior<RadScheduleView>
    {
        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.AppointmentCreating += AssociatedObject_AppointmentCreating;
            AssociatedObject.ShowDialog += AssociatedObject_ShowDialog;
            AssociatedObject.DragDropBehavior = new NoOverlapDragDropBehavior ();
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.AppointmentCreating -= AssociatedObject_AppointmentCreating;
            AssociatedObject.ShowDialog -= AssociatedObject_ShowDialog;
        }

        private void AssociatedObject_AppointmentCreating ( object sender, AppointmentCreatingEventArgs e )
        {
            e.Cancel = true;
            var newAppointment = e.Appointment.Copy ();
            newAppointment.End = e.Appointment.Start.AddHours ( 1 );
            var collection = ( AssociatedObject.AppointmentsSource as IList );
            if ( collection == null && AssociatedObject.AppointmentsSource is ICollectionView )
            {
                collection = ( AssociatedObject.AppointmentsSource as ICollectionView ).SourceCollection as IList;
            }
            if ( collection != null )
            {
                collection.Add ( newAppointment );
            }
        }

        private void AssociatedObject_ShowDialog ( object sender, ShowDialogEventArgs e )
        {
            var viewModel = ( e.DialogViewModel as AppointmentDialogViewModel );
            if ( viewModel != null )
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}
