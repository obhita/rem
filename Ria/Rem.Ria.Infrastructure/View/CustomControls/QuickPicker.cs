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
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for picking quick.
    /// </summary>
    public class QuickPicker : RadComboBox
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AddContentProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddContentProperty =
            DependencyProperty.Register (
                "AddContent",
                typeof( object ),
                typeof( QuickPicker ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for AddContentTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddContentTemplateProperty =
            DependencyProperty.Register (
                "AddContentTemplate",
                typeof( DataTemplate ),
                typeof( QuickPicker ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CanAddProperty Property.
        /// </summary>
        public static readonly DependencyProperty CanAddProperty =
            DependencyProperty.Register (
                "CanAdd",
                typeof( bool ),
                typeof( QuickPicker ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for CanClosePopupProperty Property.
        /// </summary>
        public static readonly DependencyProperty CanClosePopupProperty =
            DependencyProperty.Register (
                "CanClosePopup",
                typeof( bool? ),
                typeof( QuickPicker ),
                new PropertyMetadata ( null, ClosePopupChanged ) );

        /// <summary>
        /// Dependency Property for IsAddOpenProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsAddOpenProperty =
            DependencyProperty.Register (
                "IsAddOpen",
                typeof( bool ),
                typeof( QuickPicker ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for PageIndexProperty Property.
        /// </summary>
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register (
                "PageIndex",
                typeof( int ),
                typeof( QuickPicker ),
                new PropertyMetadata ( 0 ) );

        /// <summary>
        /// Dependency Property for PageSizeProperty Property.
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register (
                "PageSize",
                typeof( int ),
                typeof( QuickPicker ),
                new PropertyMetadata ( 0 ) );

        /// <summary>
        /// Dependency Property for ShowListButtonVisibilityProperty Property.
        /// </summary>
        public static readonly DependencyProperty ShowListButtonVisibilityProperty =
            DependencyProperty.Register (
                "ShowListButtonVisibility",
                typeof( Visibility ),
                typeof( QuickPicker ),
                new PropertyMetadata ( Visibility.Visible ) );

        /// <summary>
        /// Dependency Property for ShowListCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty ShowListCommandProperty =
            DependencyProperty.Register (
                "ShowListCommand",
                typeof( ICommand ),
                typeof( QuickPicker ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for TotalItemCountProperty Property.
        /// </summary>
        public static readonly DependencyProperty TotalItemCountProperty =
            DependencyProperty.Register (
                "TotalItemCount",
                typeof( int ),
                typeof( QuickPicker ),
                new PropertyMetadata ( 0 ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickPicker"/> class.
        /// </summary>
        public QuickPicker ()
        {
            DefaultStyleKey = typeof( QuickPicker );
            IsEditable = true;
            StaysOpenOnEdit = true;
            OpenDropDownOnFocus = true;
            CanAutocompleteSelectItems = false;
            CanKeyboardNavigationSelectItems = false;
            IsTextSearchEnabled = false;

            CloseAddPopupCommand = new DelegateCommand ( ExecuteCloseAddPopupCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the content of the add.
        /// </summary>
        /// <value>The content of the add.</value>
        public object AddContent
        {
            get { return GetValue ( AddContentProperty ); }
            set { SetValue ( AddContentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the add content template.
        /// </summary>
        /// <value>The add content template.</value>
        public DataTemplate AddContentTemplate
        {
            get { return ( DataTemplate )GetValue ( AddContentTemplateProperty ); }
            set { SetValue ( AddContentTemplateProperty, value ); }
        }

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
        /// Gets or sets the can close popup.
        /// </summary>
        /// <value>The can close popup.</value>
        public bool? CanClosePopup
        {
            get { return ( bool? )GetValue ( CanClosePopupProperty ); }
            set { SetValue ( CanClosePopupProperty, value ); }
        }

        /// <summary>
        /// Gets the close add popup command.
        /// </summary>
        public ICommand CloseAddPopupCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is add open.
        /// </summary>
        /// <value><c>true</c> if this instance is add open; otherwise, <c>false</c>.</value>
        public bool IsAddOpen
        {
            get { return ( bool )GetValue ( IsAddOpenProperty ); }
            set { SetValue ( IsAddOpenProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex
        {
            get { return ( int )GetValue ( PageIndexProperty ); }
            set { SetValue ( PageIndexProperty, value ); }
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
        /// Gets or sets the show list button visibility.
        /// </summary>
        /// <value>The show list button visibility.</value>
        public Visibility ShowListButtonVisibility
        {
            get { return ( Visibility )GetValue ( ShowListButtonVisibilityProperty ); }
            set { SetValue ( ShowListButtonVisibilityProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the show list command.
        /// </summary>
        /// <value>The show list command.</value>
        public ICommand ShowListCommand
        {
            get { return ( ICommand )GetValue ( ShowListCommandProperty ); }
            set { SetValue ( ShowListCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        public int TotalItemCount
        {
            get { return ( int )GetValue ( TotalItemCountProperty ); }
            set { SetValue ( TotalItemCountProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called before the <see cref="E:System.Windows.UIElement.KeyUp"/> event occurs.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnKeyUp ( KeyEventArgs e )
        {
            base.OnKeyUp ( e );
            if ( !IsDropDownOpen )
            {
                IsDropDownOpen = true;
            }
            if ( e.Key == Key.Enter )
            {
                if ( Items.Count () == 1 )
                {
                    SelectedIndex = 0;
                }
                else
                {
                    SelectedValuePath = DisplayMemberPath;
                    SelectedValue = Text;
                }
            }
        }

        private static void ClosePopupChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ee = d as QuickPicker;
            if ( ee != null && ee.CanClosePopup != null && !ee.CanClosePopup.Value )
            {
                ee.HandleClosePopup ();
            }
        }

        private void ExecuteCloseAddPopupCommand ( object obj )
        {
            IsAddOpen = false;
        }

        private void HandleClosePopup ()
        {
            IsAddOpen = false;
        }

        #endregion
    }
}
