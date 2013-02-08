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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using Rem.Ria.Infrastructure.Common.Extension;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// SharedGridLength class.
    /// <remarks>If you set the GroupName on a Grid that never gets loaded it will cause a memory leak because by setting the GroupName this will register for the Loaded Event.</remarks>
    /// </summary>
    public class SharedGridLength : Behavior<Grid>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for GroupNameProperty Property.
        /// <remarks>If you set the GroupName on a Grid that never gets loaded it will cause a memory leak because by setting the GroupName this will register for the Loaded Event.</remarks>
        /// </summary>
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.RegisterAttached (
                "GroupName",
                typeof( string ),
                typeof( SharedGridLength ),
                new PropertyMetadata ( null, OnGroupNameChanged ) );

        /// <summary>
        /// Dependency Property for NameProperty Property.
        /// </summary>
        public static readonly DependencyProperty SharedNameProperty =
            DependencyProperty.RegisterAttached (
                "SharedName",
                typeof( string ),
                typeof( SharedGridLength ),
                new PropertyMetadata ( null, OnSharedNameChanged ) );

        private static readonly Dictionary<string, SizeHolder> GroupColumnSize = new Dictionary<string, SizeHolder> ();
        private static readonly Dictionary<string, SizeHolder> GroupRowSize = new Dictionary<string, SizeHolder> ();

        private static readonly DependencyProperty IsBoundProperty =
            DependencyProperty.RegisterAttached (
                "IsBound",
                typeof( bool ),
                typeof( SharedGridLength ),
                new PropertyMetadata ( false ) );

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public static string GetGroupName ( DependencyObject obj )
        {
            return ( string )obj.GetValue ( GroupNameProperty );
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public static string GetSharedName ( DependencyObject obj )
        {
            return ( string )obj.GetValue ( SharedNameProperty );
        }

        /// <summary>
        /// Sets the name of the group.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetGroupName ( DependencyObject obj, string value )
        {
            obj.SetValue ( GroupNameProperty, value );
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetSharedName ( DependencyObject obj, string value )
        {
            obj.SetValue ( SharedNameProperty, value );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.SizeChanged += GridSizeChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            GridDetached ();
        }

        private static bool GetIsBound ( DependencyObject obj )
        {
            return ( bool )obj.GetValue ( IsBoundProperty );
        }

        private static void GridSizeChanged ( object sender, EventArgs e )
        {
            var grid = sender as Grid;

            foreach ( var rowDefinition in grid.RowDefinitions )
            {
                var name = GetGroupName ( grid ) + GetSharedName ( rowDefinition );
                var height = rowDefinition.ActualHeight;
                if ( GetSharedName ( rowDefinition ) != null )
                {
                    if ( !GroupRowSize.ContainsKey ( name ) )
                    {
                        GroupRowSize.Add ( name, new SizeHolder () );
                    }

                    var sizeHolder = GroupRowSize[name];

                    lock ( sizeHolder )
                    {
                        if ( height > sizeHolder.Size )
                        {
                            sizeHolder.Size = height;
                        }

                        if ( !GetIsBound ( rowDefinition ) )
                        {
                            SetIsBound ( rowDefinition, true );
                            sizeHolder.BindingCount++;
                            var binding = new Binding
                                {
                                    Source = sizeHolder,
                                    Path = new PropertyPath ( "Size" ),
                                    Mode = BindingMode.OneWay
                                };

                            BindingOperations.SetBinding ( rowDefinition, RowDefinition.MinHeightProperty, binding );
                        }
                    }
                }
            }

            foreach ( var columnDefinition in grid.ColumnDefinitions )
            {
                var name = GetGroupName ( grid ) + GetSharedName ( columnDefinition );
                var width = columnDefinition.ActualWidth;
                if ( GetSharedName ( columnDefinition ) != null )
                {
                    if ( !GroupColumnSize.ContainsKey ( name ) )
                    {
                        GroupColumnSize.Add ( name, new SizeHolder () );
                    }

                    var sizeHolder = GroupColumnSize[name];

                    lock ( sizeHolder )
                    {
                        if ( width > sizeHolder.Size )
                        {
                            sizeHolder.Size = width;
                        }

                        if ( !GetIsBound ( columnDefinition ) )
                        {
                            SetIsBound ( columnDefinition, true );
                            sizeHolder.BindingCount++;
                            var binding = new Binding
                                {
                                    Source = sizeHolder,
                                    Path = new PropertyPath ( "Size" ),
                                    Mode = BindingMode.OneWay
                                };

                            BindingOperations.SetBinding ( columnDefinition, ColumnDefinition.MinWidthProperty, binding );
                        }
                    }
                }
            }
        }

        private static void OnGroupNameChanged ( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {
            if ( e.OldValue != null && e.NewValue != null )
            {
                throw new InvalidOperationException ( "Cannot changed the Group name of a grid after it has been set." );
            }

            if ( e.NewValue == null )
            {
                var behaviors = ( sender as FrameworkElement ).GetBehaviors<SharedGridLength> ();
                foreach ( var sharedGridLength in behaviors )
                {
                    sharedGridLength.Detach ();
                }
            }
            else
            {
                var sharedLengthBehavior = new SharedGridLength ();
                sharedLengthBehavior.Attach ( sender );
            }
        }

        private static void OnSharedNameChanged ( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {
            if ( e.OldValue != null )
            {
                throw new InvalidOperationException ( "Cannot changed the name of a shared size after it has been set." );
            }
        }

        private static void SetIsBound ( DependencyObject obj, bool value )
        {
            obj.SetValue ( IsBoundProperty, value );
        }

        private void GridDetached ()
        {
            AssociatedObject.SizeChanged -= GridSizeChanged;

            foreach ( var rowDefinition in AssociatedObject.RowDefinitions )
            {
                var name = GetGroupName ( AssociatedObject ) + GetSharedName ( rowDefinition );
                if ( GetSharedName ( rowDefinition ) != null )
                {
                    if ( GroupRowSize.ContainsKey ( name ) )
                    {
                        var sizeHolder = GroupRowSize[name];
                        lock ( sizeHolder )
                        {
                            if ( sizeHolder.BindingCount <= 1 )
                            {
                                GroupRowSize.Remove ( name );
                            }
                            else
                            {
                                sizeHolder.BindingCount--;
                            }
                        }
                    }
                }
            }

            foreach ( var columnDefinition in AssociatedObject.ColumnDefinitions )
            {
                var name = GetGroupName ( AssociatedObject ) + GetSharedName ( columnDefinition );
                if ( GetSharedName ( columnDefinition ) != null )
                {
                    if ( GroupColumnSize.ContainsKey ( name ) )
                    {
                        var sizeHolder = GroupColumnSize[name];
                        lock ( sizeHolder )
                        {
                            if ( sizeHolder.BindingCount <= 1 )
                            {
                                GroupColumnSize.Remove ( name );
                            }
                            else
                            {
                                sizeHolder.BindingCount--;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Class for holding size.
        /// </summary>
        public class SizeHolder : DependencyObject
        {
            #region Constants and Fields

            /// <summary>
            /// Dependency Property for SizeProperty Property.
            /// </summary>
            public static readonly DependencyProperty SizeProperty =
                DependencyProperty.Register (
                    "Size",
                    typeof( double ),
                    typeof( SizeHolder ),
                    new PropertyMetadata ( 0.0 ) );

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets the binding count.
            /// </summary>
            /// <value>The binding count.</value>
            public int BindingCount { get; set; }

            /// <summary>
            /// Gets or sets the size.
            /// </summary>
            /// <value>The size.</value>
            public double Size
            {
                get { return ( double )GetValue ( SizeProperty ); }
                set { SetValue ( SizeProperty, value ); }
            }

            #endregion
        }
    }
}
