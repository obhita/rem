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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// Class for communicating quick picker.
    /// </summary>
    public class QuickPickerCommunicator : DependencyObject
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CanAddProperty Property.
        /// </summary>
        public static readonly DependencyProperty CanAddProperty =
            DependencyProperty.Register (
                "CanAdd",
                typeof( bool ),
                typeof( QuickPickerCommunicator ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for ClearSelectedItemCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty ClearSelectedItemCommandProperty =
            DependencyProperty.Register (
                "ClearSelectedItemCommand",
                typeof( ICommand ),
                typeof( QuickPickerCommunicator ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for HeightProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register (
                "Height",
                typeof( double ),
                typeof( QuickPickerCommunicator ),
                new PropertyMetadata ( 22.0 ) );

        /// <summary>
        /// Dependency Property for ItemAddedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty ItemAddedCommandProperty =
            DependencyProperty.Register (
                "ItemAddedCommand",
                typeof( ICommand ),
                typeof( QuickPickerCommunicator ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for PageSizeProperty Property.
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register (
                "PageSize",
                typeof( int ),
                typeof( QuickPickerCommunicator ),
                new PropertyMetadata ( 10 ) );

        /// <summary>
        /// Dependency Property for SelectedItemChangedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemChangedCommandProperty =
            DependencyProperty.Register (
                "SelectedItemChangedCommand",
                typeof( ICommand ),
                typeof( QuickPickerCommunicator ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SelectedItemProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register (
                "SelectedItem",
                typeof( ISearchResultDto ),
                typeof( QuickPickerCommunicator ),
                new PropertyMetadata ( null, SelectedItemPropertyChanged ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickPickerCommunicator"/> class.
        /// </summary>
        public QuickPickerCommunicator ()
        {
            ClearSelectedItemCommand = new DelegateCommand ( () => { SelectedItem = null; } );
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [selected item changed].
        /// </summary>
        public event SelectionChangedEventHandler SelectedItemChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance can add.
        /// </summary>
        /// <value><c>true</c> if this instance can add; otherwise, <c>false</c>.</value>
        public bool CanAdd
        {
            get { return ( bool )GetValue ( CanAddProperty ); }
            set { SetValue ( CanAddProperty, value ); }
        }

        /// <summary>
        /// Gets the clear selected item command.
        /// </summary>
        public ICommand ClearSelectedItemCommand
        {
            get { return ( ICommand )GetValue ( ClearSelectedItemCommandProperty ); }
            internal set { SetValue ( ClearSelectedItemCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public double Height
        {
            get { return ( double )GetValue ( HeightProperty ); }
            set { SetValue ( HeightProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the item added command.
        /// </summary>
        /// <value>The item added command.</value>
        public ICommand ItemAddedCommand
        {
            get { return ( ICommand )GetValue ( ItemAddedCommandProperty ); }
            set { SetValue ( ItemAddedCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get { return ( int )GetValue ( PageSizeProperty ); }
            set { SetValue ( PageSizeProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public ISearchResultDto SelectedItem
        {
            get { return ( ISearchResultDto )GetValue ( SelectedItemProperty ); }
            set { SetValue ( SelectedItemProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selected item changed command.
        /// </summary>
        /// <value>The selected item changed command.</value>
        public ICommand SelectedItemChangedCommand
        {
            get { return ( ICommand )GetValue ( SelectedItemChangedCommandProperty ); }
            set { SetValue ( SelectedItemChangedCommandProperty, value ); }
        }

        #endregion

        #region Methods

        private static void SelectedItemPropertyChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var qpc = ( d as QuickPickerCommunicator );
            if ( qpc != null && qpc.SelectedItemChangedCommand != null && qpc.SelectedItemChangedCommand.CanExecute ( qpc.SelectedItem ) )
            {
                qpc.SelectedItemChangedCommand.Execute ( qpc.SelectedItem );
            }
            if ( qpc != null && qpc.SelectedItemChanged != null )
            {
                qpc.SelectedItemChanged (
                    qpc,
                    new SelectionChangedEventArgs (
                        new List<ISearchResultDto> { e.OldValue as ISearchResultDto }, new List<ISearchResultDto> { e.NewValue as ISearchResultDto } ) );
            }
        }

        #endregion
    }
}
