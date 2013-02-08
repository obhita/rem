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

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// UniformGrid class.
    /// </summary>
    public class UniformGrid : Panel
    {
        #region Constants and Fields

        /// <summary>
        /// DependencyProperty for the Columns property.
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register (
                "Columns",
                typeof( int ),
                typeof( UniformGrid ),
                new PropertyMetadata ( 0, OnIntegerDependencyPropertyChanged ) );

        /// <summary>
        /// The FirstColumnProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty FirstColumnProperty =
            DependencyProperty.Register (
                "FirstColumn",
                typeof( int ),
                typeof( UniformGrid ),
                new PropertyMetadata ( 0, OnIntegerDependencyPropertyChanged ) );

        /// <summary>
        /// The Rows DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register (
                "Rows",
                typeof( int ),
                typeof( UniformGrid ),
                new PropertyMetadata ( 0, OnIntegerDependencyPropertyChanged ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the number of columns in the grid. A value of zero
        /// indicates that the count should be dynamically computed based on the
        /// number of rows and the number of non-collapsed children in the grid.
        /// </summary>
        /// <value>The columns.</value>
        public int Columns
        {
            get { return ( int )GetValue ( ColumnsProperty ); }
            set { SetValue ( ColumnsProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the number of first columns to leave blank.
        /// </summary>
        /// <value>The first column.</value>
        public int FirstColumn
        {
            get { return ( int )GetValue ( FirstColumnProperty ); }
            set { SetValue ( FirstColumnProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the number of rows in the grid. A value of zero
        /// indicates that the row count should be dynamically computed based on
        /// the number of columns and the number of non-collapsed children in
        /// the grid.
        /// </summary>
        /// <value>The rows in the grid.</value>
        public int Rows
        {
            get { return ( int )GetValue ( RowsProperty ); }
            set { SetValue ( RowsProperty, value ); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the computed column value.
        /// </summary>
        /// <value>The computed columns.</value>
        private int ComputedColumns { get; set; }

        /// <summary>
        /// Gets or sets the computed row value.
        /// </summary>
        /// <value>The computed rows.</value>
        private int ComputedRows { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Arrange the children of the UniformGrid by distributing space evenly
        /// among the children, making each child the size equal to a cell
        /// portion of the arrangeSize parameter.
        /// </summary>
        /// <param name="arrangeSize">The arrange size.</param>
        /// <returns>Returns the updated Size.</returns>
        protected override Size ArrangeOverride ( Size arrangeSize )
        {
            var childBounds = new Rect ( 0, 0, arrangeSize.Width / ComputedColumns, arrangeSize.Height / ComputedRows );
            var xStep = childBounds.Width;
            var xBound = arrangeSize.Width - 1.0;
            childBounds.X += childBounds.Width * FirstColumn;

            // Arrange and Position each child to the same cell size
            foreach ( var child in Children )
            {
                child.Arrange ( childBounds );
                if ( child.Visibility != Visibility.Collapsed )
                {
                    childBounds.X += xStep;
                    if ( childBounds.X >= xBound )
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
            }

            return arrangeSize;
        }

        /// <summary>
        /// Compute the desired size of the UniformGrid by measuring all of the
        /// children with a constraint equal to a cell's portion of the given
        /// constraint. The maximum child width and maximum child height are
        /// tracked, and then the desired size is computed by multiplying these
        /// maximums by the row and column count.
        /// </summary>
        /// <param name="constraint">The size constraint.</param>
        /// <returns>Returns the desired size.</returns>
        protected override Size MeasureOverride ( Size constraint )
        {
            UpdateComputedValues ();

            var childConstraint = new Size ( constraint.Width / ComputedColumns, constraint.Height / ComputedRows );
            var maxChildDesiredWidth = 0.0;
            var maxChildDesiredHeight = 0.0;

            //  Measure each child, keeping track of max desired width & height.
            for ( int i = 0, count = Children.Count; i < count; ++i )
            {
                var child = Children[i];
                child.Measure ( childConstraint );
                var childDesiredSize = child.DesiredSize;
                if ( maxChildDesiredWidth < childDesiredSize.Width )
                {
                    maxChildDesiredWidth = childDesiredSize.Width;
                }
                if ( maxChildDesiredHeight < childDesiredSize.Height )
                {
                    maxChildDesiredHeight = childDesiredSize.Height;
                }
            }
            return new Size ( ( maxChildDesiredWidth * ComputedColumns ), ( maxChildDesiredHeight * ComputedRows ) );
        }

        private static void OnIntegerDependencyPropertyChanged ( DependencyObject o, DependencyPropertyChangedEventArgs e )
        {
            // Silently coerce the value back to >= 0 if negative.
            if ( !( e.NewValue is int ) || ( int )e.NewValue < 0 )
            {
                o.SetValue ( e.Property, e.OldValue );
            }
        }

        private void UpdateComputedValues ()
        {
            ComputedColumns = Columns;
            ComputedRows = Rows;

            // Reset the first column. This is the same logic performed by WPF.
            if ( FirstColumn >= ComputedColumns )
            {
                FirstColumn = 0;
            }

            if ( ( ComputedRows == 0 ) || ( ComputedColumns == 0 ) )
            {
                var nonCollapsedCount = 0;
                for ( int i = 0, count = Children.Count; i < count; ++i )
                {
                    var child = Children[i];
                    if ( child.Visibility != Visibility.Collapsed )
                    {
                        nonCollapsedCount++;
                    }
                }
                if ( nonCollapsedCount == 0 )
                {
                    nonCollapsedCount = 1;
                }
                if ( ComputedRows == 0 )
                {
                    if ( ComputedColumns > 0 )
                    {
                        ComputedRows = ( nonCollapsedCount + FirstColumn + ( ComputedColumns - 1 ) ) / ComputedColumns;
                    }
                    else
                    {
                        ComputedRows = ( int )Math.Sqrt ( nonCollapsedCount );
                        if ( ( ComputedRows * ComputedRows ) < nonCollapsedCount )
                        {
                            ComputedRows++;
                        }
                        ComputedColumns = ComputedRows;
                    }
                }
                else if ( ComputedColumns == 0 )
                {
                    ComputedColumns = ( nonCollapsedCount + ( ComputedRows - 1 ) ) / ComputedRows;
                }
            }
        }

        #endregion
    }
}
