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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.WellKnownNames.VisitModule;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.ScheduleView;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing rem RAD scheduler.
    /// </summary>
    public class RemRadSchedulerBehavior : Behavior<RadScheduleView>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AddedAppointmentCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddedAppointmentCommandProperty =
            DependencyProperty.Register (
                "AddedAppointmentCommand",
                typeof( ICommand ),
                typeof( RemRadSchedulerBehavior ),
                new PropertyMetadata ( null, AddedAppointmentCommandChanged ) );

        /// <summary>
        /// Dependency Property for AllowCreatingProperty Property.
        /// </summary>
        public static readonly DependencyProperty AllowCreatingProperty =
            DependencyProperty.Register (
                "AllowCreating",
                typeof( bool ),
                typeof( RemRadSchedulerBehavior ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for AllowEditingProperty Property.
        /// </summary>
        public static readonly DependencyProperty AllowEditingProperty =
            DependencyProperty.Register (
                "AllowEditing",
                typeof( bool ),
                typeof( RemRadSchedulerBehavior ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for DateRangeUpdatedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DateRangeUpdatedCommandProperty =
            DependencyProperty.Register (
                "DateRangeUpdatedCommand",
                typeof( ICommand ),
                typeof( RemRadSchedulerBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for DeletedAppointmentCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DeletedAppointmentCommandProperty =
            DependencyProperty.Register (
                "DeletedAppointmentCommand",
                typeof( ICommand ),
                typeof( RemRadSchedulerBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for DoubleClickAppointmentCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DoubleClickAppointmentCommandProperty =
            DependencyProperty.Register (
                "DoubleClickAppointmentCommand",
                typeof( ICommand ),
                typeof( RemRadSchedulerBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for EditedAppointmentCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty EditedAppointmentCommandProperty =
            DependencyProperty.Register (
                "EditedAppointmentCommand",
                typeof( ICommand ),
                typeof( RemRadSchedulerBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for FirstVisibleDateProperty Property.
        /// </summary>
        public static readonly DependencyProperty FirstVisibleDateProperty =
            DependencyProperty.Register (
                "FirstVisibleDate",
                typeof( DateTime ),
                typeof( RemRadSchedulerBehavior ),
                new PropertyMetadata ( FirstVisibleDateChangedCallback ) );

        private RadScheduleViewDragDropBehavior _dragDropBehavior;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the added appointment command.
        /// </summary>
        /// <value>The added appointment command.</value>
        public ICommand AddedAppointmentCommand
        {
            get { return ( ICommand )GetValue ( AddedAppointmentCommandProperty ); }
            set { SetValue ( AddedAppointmentCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow creating].
        /// </summary>
        /// <value><c>true</c> if [allow creating]; otherwise, <c>false</c>.</value>
        public bool AllowCreating
        {
            get { return ( bool )GetValue ( AllowCreatingProperty ); }
            set { SetValue ( AllowCreatingProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow editing].
        /// </summary>
        /// <value><c>true</c> if [allow editing]; otherwise, <c>false</c>.</value>
        public bool AllowEditing
        {
            get { return ( bool )GetValue ( AllowEditingProperty ); }
            set { SetValue ( AllowEditingProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the date range updated command.
        /// </summary>
        /// <value>The date range updated command.</value>
        public ICommand DateRangeUpdatedCommand
        {
            get { return ( ICommand )GetValue ( DateRangeUpdatedCommandProperty ); }
            set { SetValue ( DateRangeUpdatedCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the deleted appointment command.
        /// </summary>
        /// <value>The deleted appointment command.</value>
        public ICommand DeletedAppointmentCommand
        {
            get { return ( ICommand )GetValue ( DeletedAppointmentCommandProperty ); }
            set { SetValue ( DeletedAppointmentCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the double click appointment command.
        /// </summary>
        /// <value>The double click appointment command.</value>
        public ICommand DoubleClickAppointmentCommand
        {
            get { return ( ICommand )GetValue ( DoubleClickAppointmentCommandProperty ); }
            set { SetValue ( DoubleClickAppointmentCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the edited appointment command.
        /// </summary>
        /// <value>The edited appointment command.</value>
        public ICommand EditedAppointmentCommand
        {
            get { return ( ICommand )GetValue ( EditedAppointmentCommandProperty ); }
            set { SetValue ( EditedAppointmentCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the first visible date.
        /// </summary>
        /// <value>The first visible date.</value>
        public DateTime FirstVisibleDate
        {
            get { return ( DateTime )GetValue ( FirstVisibleDateProperty ); }
            set { SetValue ( FirstVisibleDateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the vist status.
        /// </summary>
        /// <value>The vist status.</value>
        public string VistStatus
        {
            get { return ( string )GetValue ( FirstVisibleDateProperty ); }
            set { SetValue ( FirstVisibleDateProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.AppointmentEdited += AssociatedObject_AppointmentEdited;
            AssociatedObject.AppointmentCreating += AssociatedObject_AppointmentCreating;
            AssociatedObject.AppointmentDeleted += AssociatedObject_AppointmentDeleted;
            AssociatedObject.VisibleRangeChanged += AssociatedObject_VisibleRangeChanged;
            AssociatedObject.ShowDialog += AssociatedObject_ShowDialog;
            AssociatedObject.DragDropBehavior =
                _dragDropBehavior = new RadScheduleViewDragDropBehavior { AddedAppointmentCommand = AddedAppointmentCommand };
            AssociatedObject.Loaded += AssociatedObjectLoaded;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.AppointmentEdited -= AssociatedObject_AppointmentEdited;
            AssociatedObject.AppointmentCreating -= AssociatedObject_AppointmentCreating;
            AssociatedObject.AppointmentDeleted -= AssociatedObject_AppointmentDeleted;
            AssociatedObject.VisibleRangeChanged -= AssociatedObject_VisibleRangeChanged;
            AssociatedObject.ShowDialog -= AssociatedObject_ShowDialog;

            var viewModel = AssociatedObject.DataContext;

            if ( viewModel is IViewRefreshable )
            {
                var clinicianScheduleTileViewModel = viewModel as IViewRefreshable;
                clinicianScheduleTileViewModel.RefreshView -= Refresh;
            }
        }

        private static void AddedAppointmentCommandChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var radSchedbehavior = d as RemRadSchedulerBehavior;
            if ( radSchedbehavior != null && radSchedbehavior._dragDropBehavior != null )
            {
                radSchedbehavior._dragDropBehavior.AddedAppointmentCommand = radSchedbehavior.AddedAppointmentCommand;
            }
        }

        private static void FirstVisibleDateChangedCallback ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var radSchedbehavior = d as RemRadSchedulerBehavior;
            if ( radSchedbehavior != null )
            {
                radSchedbehavior.AssociatedObject.CurrentDate = ( DateTime )e.NewValue;
            }
        }

        private void AssociatedObjectLoaded ( object sender, RoutedEventArgs e )
        {
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            var viewModel = AssociatedObject.DataContext;

            if ( viewModel is IViewRefreshable )
            {
                var clinicianScheduleTileViewModel = viewModel as IViewRefreshable;
                clinicianScheduleTileViewModel.RefreshView += Refresh;
            }
        }

        private void AssociatedObject_AppointmentCreating ( object sender, AppointmentCreatingEventArgs e )
        {
            e.Cancel = !AllowCreating;
        }

        private void AssociatedObject_AppointmentDeleted ( object sender, AppointmentDeletedEventArgs e )
        {
            if ( DeletedAppointmentCommand != null && DeletedAppointmentCommand.CanExecute ( e.Appointment ) )
            {
                DeletedAppointmentCommand.Execute ( e.Appointment );
            }
        }

        private void AssociatedObject_AppointmentEdited ( object sender, AppointmentEditedEventArgs e )
        {
            if ( EditedAppointmentCommand != null && EditedAppointmentCommand.CanExecute ( e.Appointment ) )
            {
                EditedAppointmentCommand.Execute ( e.Appointment );
            }
        }

        private void AssociatedObject_ShowDialog ( object sender, ShowDialogEventArgs e )
        {
            var viewModel = ( e.DialogViewModel as AppointmentDialogViewModel );
            if ( viewModel != null && viewModel.ViewMode == AppointmentViewMode.Edit )
            {
                e.Cancel = !AllowEditing;
                if ( DoubleClickAppointmentCommand != null && DoubleClickAppointmentCommand.CanExecute ( viewModel.Occurrence.Appointment ) )
                {
                    DoubleClickAppointmentCommand.Execute ( viewModel.Occurrence.Appointment );
                }
            }
        }

        private void AssociatedObject_VisibleRangeChanged ( object sender, EventArgs e )
        {
            if ( DateRangeUpdatedCommand != null
                 &&
                 DateRangeUpdatedCommand.CanExecute (
                     new DateRange { StartDate = AssociatedObject.VisibleRange.Start, EndDate = AssociatedObject.VisibleRange.End } ) )
            {
                DateRangeUpdatedCommand.Execute (
                    new DateRange { StartDate = AssociatedObject.VisibleRange.Start, EndDate = AssociatedObject.VisibleRange.End } );
            }
        }

        private void Refresh ( object sender, EventArgs eventArgs )
        {
            var appointments = AssociatedObject.ChildrenOfType<AppointmentItem> ();
            foreach ( var appointment in appointments )
            {
                appointment.Style = AssociatedObject.AppointmentStyleSelector.SelectStyle (
                    appointment.Appointment, appointment, AssociatedObject.ActiveViewDefinition );
            }
        }

        #endregion

        /// <summary>
        /// Class for behaviing RAD schedule view drag drop.
        /// </summary>
        private class RadScheduleViewDragDropBehavior : NoOverlapDragDropBehavior
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the added appointment command.
            /// </summary>
            /// <value>The added appointment command.</value>
            public ICommand AddedAppointmentCommand { get; set; }

            #endregion

            #region Public Methods

            /// <summary>
            /// Gets the value specifying whether the resize operation can be finished, or not.
            /// </summary>
            /// <param name="state">DragDropState identifying the current resize operation.</param>
            /// <returns>True when the resize operation can be finished, otherwise false.</returns>
            public override bool CanResize ( DragDropState state )
            {
                return false;
            }

            /// <summary>
            /// This method is called when the drag operation is canceled.
            /// </summary>
            /// <param name="state">The drag drop state.</param>
            public override void DragDropCanceled(DragDropState state)
            {
                base.DragDropCanceled(state);
                RadDragAndDropManager.DragCueOffset = new Point();
            }

            /// <summary>
            /// This method is called when the drag and drop operations are completed.
            /// </summary>
            /// <param name="state">The drag drop state.</param>
            public override void DragDropCompleted(DragDropState state)
            {
                base.DragDropCompleted(state);
                RadDragAndDropManager.DragCueOffset = new Point();
            }

            /// <summary>
            /// Gets the value specifying whether the drag operation can be performed, or not.
            /// </summary>
            /// <param name="state">DragDropState identifying the current drag operation.</param>
            /// <returns>True when the drag operation can be performed, otherwise false.</returns>
            public override bool CanStartDrag ( DragDropState state )
            {
                var appointment = state.Appointment as IAppointment;

                if ( appointment != null && appointment.Resources.Count > 0 && appointment.Resources[0] is LookupValueDto )
                {
                    var visitStatus = ( LookupValueDto )appointment.Resources[0];
                    return visitStatus.WellKnownName.Equals ( VisitStatus.Scheduled, StringComparison.InvariantCultureIgnoreCase );
                }

                return base.CanStartDrag ( state );
            }

            /// <summary>
            /// Performs the drag operation.
            /// </summary>
            /// <param name="state">DragDropState identifying the current drag operation.</param>
            public override void Drop ( DragDropState state )
            {
                var draggedAppointment = state.Appointment as IAppointment;

                if ( draggedAppointment == null )
                {
                    return;
                }
                if ( !state.DestinationAppointmentsSource.OfType<IAppointment> ().Contains ( draggedAppointment ) )
                {
                    draggedAppointment.Start = state.DestinationSlots.ElementAt ( 0 ).Start;
                    draggedAppointment.End = state.DestinationSlots.ElementAt ( 0 ).End;

                    var source = ( state.SourceAppointmentsSource as IList );
                    if ( source == null && state.SourceAppointmentsSource is ICollectionView )
                    {
                        source = ( state.SourceAppointmentsSource as ICollectionView ).SourceCollection as IList;
                    }
                    if ( source != null )
                    {
                        source.Remove ( draggedAppointment );
                    }
                    var destination = ( state.DestinationAppointmentsSource as IList );
                    if ( destination == null && state.DestinationAppointmentsSource is ICollectionView )
                    {
                        destination = ( state.DestinationAppointmentsSource as ICollectionView ).SourceCollection as IList;
                    }
                    if ( destination != null )
                    {
                        destination.Add ( draggedAppointment );
                        if ( AddedAppointmentCommand != null && AddedAppointmentCommand.CanExecute ( draggedAppointment ) )
                        {
                            AddedAppointmentCommand.Execute ( draggedAppointment );
                        }
                    }
                }
                else
                {
                    base.Drop ( state );
                }
            }

            #endregion
        }
    }
}
