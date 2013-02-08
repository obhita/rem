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
using Rem.Ria.Infrastructure.Common.Extension;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// EditableGrid class.
    /// </summary>
    public class EditableGrid : Grid, IReadOnly
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for IsReadOnlyProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register (
                "IsReadOnly",
                typeof( bool ),
                typeof( EditableGrid ),
                new PropertyMetadata ( false, IsReadOnlyChanged ) );

        /// <summary>
        /// Dependency Property for PaddingProperty Property.
        /// </summary>
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register (
                "Padding",
                typeof( Thickness ),
                typeof( EditableGrid ),
                new PropertyMetadata ( new Thickness ( 0 ), PaddingChanged ) );

        private bool _isLoaded;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableGrid"/> class.
        /// </summary>
        public EditableGrid()
        {
            Loaded += EditableGrid_Loaded;
            Unloaded += ( sender, args ) => _isLoaded = false;
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly
        {
            get { return ( bool )GetValue ( IsReadOnlyProperty ); }
            set { SetValue ( IsReadOnlyProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        public Thickness Padding
        {
            get { return ( Thickness )GetValue ( PaddingProperty ); }
            set { SetValue ( PaddingProperty, value ); }
        }

        #endregion

        #region Methods

        private void EditableGrid_Loaded( object sender, EventArgs e)
        {
            HandlePadding ( Padding );
            _isLoaded = true;
        }

        private void HandlePadding( Thickness padding )
        {
            foreach (var child in Children)
            {
                if (child is FrameworkElement)
                {
                    var frameworkElement = child as FrameworkElement;
                    var row = GetRow ( frameworkElement );
                    var rowSpan = GetRowSpan ( frameworkElement );
                    var column = GetColumn ( frameworkElement );
                    var columnSpan = GetColumnSpan ( frameworkElement );
                    var newMargin = new Thickness (
                        frameworkElement.Margin.Left + ( ( column == 0 ) ? padding.Left : 0 ),
                        frameworkElement.Margin.Top + ( ( row == 0 ) ? padding.Top : 0 ),
                        frameworkElement.Margin.Right + ( ( ( row + rowSpan ) >= RowDefinitions.Count - 1 ) ? padding.Right : 0 ),
                        frameworkElement.Margin.Bottom + ( ( ( column + columnSpan ) >= ColumnDefinitions.Count - 1 ) ? padding.Bottom : 0 ) );
                    frameworkElement.Margin = newMargin;
                }
            }
        }

        private static void IsReadOnlyChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var readOnlyGrid = d as EditableGrid;
            if ( readOnlyGrid != null )
            {
                foreach ( FrameworkElement child in readOnlyGrid.Children )
                {
                    if ( readOnlyGrid.IsReadOnly )
                    {
                        child.IsReadonly ();
                    }
                    else
                    {
                        child.IsNotReadonly ();
                    }
                }
            }
        }

        private static void PaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as EditableGrid;
            if(grid != null)
            {
                if(grid._isLoaded)
                {
                    var oldPadding = ( Thickness )e.OldValue;
                    var newPadding = ( Thickness )e.NewValue;
                    grid.HandlePadding (
                        new Thickness (
                            newPadding.Left - oldPadding.Left,
                            newPadding.Top - oldPadding.Top,
                            newPadding.Right - oldPadding.Right,
                            newPadding.Bottom - oldPadding.Bottom ) );
                }
            }
        }

        #endregion
    }
}
