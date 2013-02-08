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

using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing no overlap drag drop.
    /// </summary>
    public class NoOverlapDragDropBehavior : ScheduleViewDragDropBehavior
    {
        #region Public Methods

        /// <summary>
        /// Gets the value specifying whether the drag operation can be finished, or not.
        /// </summary>
        /// <param name="state">DragDropState identifying the current drag operation.</param>
        /// <returns>True when the drag operation can be finished, otherwise false.</returns>
        public override bool CanDrop ( DragDropState state )
        {
            var draggedAppointment = state.Appointment as IAppointment;
            if ( draggedAppointment == null )
            {
                return false;
            }
            var overlap = state.DestinationAppointmentsSource
                .OfType<IAppointment> ()
                .Where ( a => !state.DraggedAppointments.Contains ( a ) )
                .All ( a => state.DestinationSlots.All ( s => !AreOverlapping ( a, s ) ) );
            return overlap;
        }

        /// <summary>
        /// Gets the value specifying whether the resize operation can be finished, or not.
        /// </summary>
        /// <param name="state">DragDropState identifying the current resize operation.</param>
        /// <returns>True when the resize operation can be finished, otherwise false.</returns>
        public override bool CanResize ( DragDropState state )
        {
            var overlap = state.DestinationAppointmentsSource
                .OfType<IAppointment> ()
                .Where ( a => a != state.Appointment )
                .All ( a => state.DestinationSlots.All ( s => !AreOverlapping ( a, s ) ) );
            return overlap;
        }

        #endregion

        #region Methods

        private static bool AreOverlapping ( IAppointment appointment, Slot slot )
        {
            return appointment.IntersectsWith ( slot );
        }

        #endregion
    }
}
