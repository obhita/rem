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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// CollapsingUserControl class.
    /// </summary>
    public class CollapsingUserControl : UserControl, ICollapsingControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for LargeContentProperty Property.
        /// </summary>
        public static readonly DependencyProperty LargeContentProperty =
            DependencyProperty.Register (
                "LargeContent",
                typeof( UIElement ),
                typeof( CollapsingUserControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for NormalContentProperty Property.
        /// </summary>
        public static readonly DependencyProperty NormalContentProperty =
            DependencyProperty.Register (
                "NormalContent",
                typeof( UIElement ),
                typeof( CollapsingUserControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for PriorityProperty Property.
        /// </summary>
        public static readonly DependencyProperty PriorityProperty =
            DependencyProperty.Register (
                "Priority",
                typeof( int ),
                typeof( CollapsingUserControl ),
                new PropertyMetadata ( 1 ) );

        /// <summary>
        /// Dependency Property for SmallContentProperty Property.
        /// </summary>
        public static readonly DependencyProperty SmallContentProperty =
            DependencyProperty.Register (
                "SmallContent",
                typeof( UIElement ),
                typeof( CollapsingUserControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for StateProperty Property.
        /// </summary>
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register (
                "State",
                typeof( CollapsingState ),
                typeof( CollapsingUserControl ),
                new PropertyMetadata ( CollapsingState.Large, StateChanged ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CollapsingUserControl"/> class.
        /// </summary>
        public CollapsingUserControl ()
        {
            GoToSmallCommand = new DelegateCommand ( ExecuteGoToSmallCommand );
            GoToNormalCommand = new DelegateCommand ( ExecuteGoToNormalCommand );
            GoToLargeCommand = new DelegateCommand ( ExecuteGoToLargeCommand );
            GetSmallerCommand = new DelegateCommand ( ExecuteGetSmallerCommand );
            GetLargerCommand = new DelegateCommand ( ExecuteGetLargerCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the get larger command.
        /// </summary>
        public ICommand GetLargerCommand { get; private set; }

        /// <summary>
        /// Gets the get smaller command.
        /// </summary>
        public ICommand GetSmallerCommand { get; private set; }

        /// <summary>
        /// Gets the go to large command.
        /// </summary>
        public ICommand GoToLargeCommand { get; private set; }

        /// <summary>
        /// Gets the go to normal command.
        /// </summary>
        public ICommand GoToNormalCommand { get; private set; }

        /// <summary>
        /// Gets the go to small command.
        /// </summary>
        public ICommand GoToSmallCommand { get; private set; }

        /// <summary>
        /// Gets or sets the content of the large.
        /// </summary>
        /// <value>The content of the large.</value>
        public UIElement LargeContent
        {
            get { return ( UIElement )GetValue ( LargeContentProperty ); }
            set { SetValue ( LargeContentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the content of the normal.
        /// </summary>
        /// <value>The content of the normal.</value>
        public UIElement NormalContent
        {
            get { return ( UIElement )GetValue ( NormalContentProperty ); }
            set { SetValue ( NormalContentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority
        {
            get { return ( int )GetValue ( PriorityProperty ); }
            set { SetValue ( PriorityProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the content of the small.
        /// </summary>
        /// <value>The content of the small.</value>
        public UIElement SmallContent
        {
            get { return ( UIElement )GetValue ( SmallContentProperty ); }
            set { SetValue ( SmallContentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public CollapsingState State
        {
            get { return ( CollapsingState )GetValue ( StateProperty ); }
            set { SetValue ( StateProperty, value ); }
        }

        #endregion

        #region Methods

        private static void StateChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var collapsingUserControl = d as CollapsingUserControl;
            if ( collapsingUserControl != null )
            {
                collapsingUserControl.UpdateContent ();
            }
        }

        private void ExecuteGetLargerCommand ()
        {
            switch ( State )
            {
                case CollapsingState.Small:
                    State = CollapsingState.Normal;
                    break;
                case CollapsingState.Normal:
                    State = CollapsingState.Large;
                    break;
            }
        }

        private void ExecuteGetSmallerCommand ()
        {
            switch ( State )
            {
                case CollapsingState.Large:
                    State = CollapsingState.Normal;
                    break;
                case CollapsingState.Normal:
                    State = CollapsingState.Small;
                    break;
            }
        }

        private void ExecuteGoToLargeCommand ()
        {
            State = CollapsingState.Large;
        }

        private void ExecuteGoToNormalCommand ()
        {
            State = CollapsingState.Normal;
        }

        private void ExecuteGoToSmallCommand ()
        {
            State = CollapsingState.Small;
        }

        private void UpdateContent ()
        {
            switch ( State )
            {
                case CollapsingState.Small:
                    Content = SmallContent;
                    break;
                case CollapsingState.Normal:
                    Content = NormalContent;
                    break;
                case CollapsingState.Large:
                    Content = LargeContent;
                    break;
            }
        }

        #endregion
    }
}
