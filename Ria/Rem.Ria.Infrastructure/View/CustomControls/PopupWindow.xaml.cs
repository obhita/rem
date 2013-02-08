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

using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Browser;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// PopupWindow class.
    /// </summary>
    public partial class PopupWindow : ExtendedChildWindow, IPopup
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ClosingCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty ClosingCommandProperty =
            DependencyProperty.Register (
                "ClosingCommand",
                typeof( ICommand ),
                typeof( PopupWindow ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SubContentProperty Property.
        /// </summary>
        public static readonly DependencyProperty SubContentProperty =
            DependencyProperty.Register (
                "SubContent",
                typeof( object ),
                typeof( PopupWindow ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for TitleProperty Property.
        /// </summary>
        public static new readonly DependencyProperty TitleProperty =
            DependencyProperty.Register (
                "Title",
                typeof( string ),
                typeof( PopupWindow ),
                new PropertyMetadata ( null, TitleChanged ) );

        private static readonly DependencyProperty SubContentTitleProperty =
            DependencyProperty.Register (
                "SubContentTitle",
                typeof( string ),
                typeof( PopupWindow ),
                new PropertyMetadata ( null ) );

        private bool _templateApplied;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupWindow"/> class.
        /// </summary>
        public PopupWindow ()
        {
            if (HtmlPage.IsEnabled)
            {
                InitializeComponent ();
                DataContext = this;
                Closing += PopupWindow_Closing;

                SetBinding ( SubContentTitleProperty, new Binding ( "SubContent.DataContext.Title" ) );
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the closing command.
        /// </summary>
        /// <value>The closing command.</value>
        public ICommand ClosingCommand
        {
            get { return ( ICommand )GetValue ( ClosingCommandProperty ); }
            set { SetValue ( ClosingCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the content of the sub.
        /// </summary>
        /// <value>The content of the sub.</value>
        public object SubContent
        {
            get { return GetValue ( SubContentProperty ); }
            set { SetValue ( SubContentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the title of the <see cref="T:System.Windows.Controls.ChildWindow"/>.
        /// </summary>
        /// <value>The title.</value>
        /// <returns>The title of the child window. The default is null.</returns>
        public new string Title
        {
            get
            {
                if ( ReadLocalValue ( TitleProperty ) == DependencyProperty.UnsetValue )
                {
                    return SubContentTitle;
                }
                return ( string )GetValue ( TitleProperty );
            }

            set { SetValue ( TitleProperty, value ); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the sub content title.
        /// </summary>
        /// <value>The sub content title.</value>
        private string SubContentTitle
        {
            get { return ( string )GetValue ( SubContentTitleProperty ); }
            set { SetValue ( SubContentTitleProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds the visual tree for the <see cref="T:System.Windows.Controls.ChildWindow"/> control when a new template is applied.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            if ( IsMaximized )
            {
                MaxRow.Height = new GridLength ( 1, GridUnitType.Star );
            }
            _templateApplied = true;
        }

        void IPopup.Show ()
        {
            Show ();
        }

        void IPopup.Close()
        {
            Close();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Maximizes this instance.
        /// </summary>
        protected override void Maximize ()
        {
            base.Maximize ();
            if ( _templateApplied )
            {
                MaxRow.Height = new GridLength ( 1, GridUnitType.Star );
            }
        }

        /// <summary>
        /// Uns the maximize.
        /// </summary>
        protected override void UnMaximize ()
        {
            base.UnMaximize ();
            if ( _templateApplied )
            {
                MaxRow.Height = new GridLength ( 1, GridUnitType.Auto );
            }
        }

        private static void TitleChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as PopupWindow ).UpdateBaseTitle ();
        }

        private void PopupWindow_Closing ( object sender, CancelEventArgs e )
        {
            Closing -= PopupWindow_Closing;
            if ( ClosingCommand != null && ClosingCommand.CanExecute ( null ) )
            {
                ClosingCommand.Execute ( null );
            }
        }

        private void UpdateBaseTitle ()
        {
            base.Title = Title;
        }

        #endregion
    }
}
