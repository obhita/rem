﻿#region License

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
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing double click.
    /// </summary>
    public class DoubleClickBehavior : Behavior<UIElement>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CommandParameterProperty Property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register (
                "CommandParameter",
                typeof( object ),
                typeof( DoubleClickBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for DoubleClickCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.Register (
                "DoubleClickCommand",
                typeof( ICommand ),
                typeof( DoubleClickBehavior ),
                new PropertyMetadata ( null ) );

        private readonly DoubleClickDetector _doubleClickDetector = new DoubleClickDetector ();

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [double click].
        /// </summary>
        public event MouseButtonEventHandler DoubleClick;

        #endregion

        #region Public Properties

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
        /// Gets or sets the double click command.
        /// </summary>
        /// <value>The double click command.</value>
        public ICommand DoubleClickCommand
        {
            get { return ( ICommand )GetValue ( DoubleClickCommandProperty ); }
            set { SetValue ( DoubleClickCommandProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.MouseLeftButtonDown += AssociatedObjectMouseLeftButtonDown;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.MouseLeftButtonDown -= AssociatedObjectMouseLeftButtonDown;
        }

        private void AssociatedObjectMouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
        {
            var element = sender as UIElement;
            var clickTime = DateTime.Now;
            var isDoubleClick = _doubleClickDetector.IsDoubleClick ( element, e, clickTime );
            if ( isDoubleClick )
            {
                if ( DoubleClickCommand != null && DoubleClickCommand.CanExecute ( CommandParameter ) )
                {
                    DoubleClickCommand.Execute ( CommandParameter );
                }
                if ( DoubleClick != null )
                {
                    DoubleClick ( sender, e );
                }
            }
        }

        #endregion
    }
}
