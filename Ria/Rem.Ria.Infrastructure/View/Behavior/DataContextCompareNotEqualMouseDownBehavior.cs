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
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing data context compare not equal mouse down.
    /// </summary>
    public class DataContextCompareNotEqualMouseDownBehavior : Behavior<FrameworkElement>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CommandParameterProperty Property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register (
                "CommandParameter",
                typeof( object ),
                typeof( DataContextCompareNotEqualMouseDownBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register (
                "Command",
                typeof( ICommand ),
                typeof( DataContextCompareNotEqualMouseDownBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CompareValueProperty Property.
        /// </summary>
        public static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register (
                "CompareValue",
                typeof( object ),
                typeof( DataContextCompareNotEqualMouseDownBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SetCompareValueProperty Property.
        /// </summary>
        public static readonly DependencyProperty SetCompareValueProperty =
            DependencyProperty.Register (
                "SetCompareValue",
                typeof( bool ),
                typeof( DataContextCompareNotEqualMouseDownBehavior ),
                new PropertyMetadata ( false ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public ICommand Command
        {
            get { return ( ICommand )GetValue ( CommandProperty ); }
            set { SetValue ( CommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>The command parameter.</value>
        public object CommandParameter
        {
            get { return GetValue ( CommandParameterProperty ); }
            set { SetValue ( CommandParameterProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the compare value.
        /// </summary>
        /// <value>The compare value.</value>
        public object CompareValue
        {
            get { return GetValue ( CompareValueProperty ); }
            set { SetValue ( CompareValueProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [set compare value].
        /// </summary>
        /// <value><c>true</c> if [set compare value]; otherwise, <c>false</c>.</value>
        public bool SetCompareValue
        {
            get { return ( bool )GetValue ( SetCompareValueProperty ); }
            set { SetValue ( SetCompareValueProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.MouseLeftButtonDown += MouseEventHandler;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.MouseLeftButtonDown -= MouseEventHandler;
        }

        private void MouseEventHandler ( object sender, MouseButtonEventArgs e )
        {
            if ( AssociatedObject.DataContext != CompareValue && Command != null )
            {
                if ( SetCompareValue )
                {
                    CompareValue = AssociatedObject.DataContext;
                }
                Command.Execute ( CommandParameter );
            }
        }

        #endregion
    }
}
