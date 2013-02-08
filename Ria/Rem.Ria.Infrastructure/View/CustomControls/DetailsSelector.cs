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
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Utility;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for selecting details.
    /// </summary>
    public class DetailsSelector : Control
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for DetailsLevel1Property Property.
        /// </summary>
        public static readonly DependencyProperty DetailsLevel1Property =
            DependencyProperty.Register (
                "DetailsLevel1",
                typeof( DataTemplate ),
                typeof( DetailsSelector ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for DetailsLevel2Property Property.
        /// </summary>
        public static readonly DependencyProperty DetailsLevel2Property =
            DependencyProperty.Register (
                "DetailsLevel2",
                typeof( DataTemplate ),
                typeof( DetailsSelector ),
                new PropertyMetadata ( DetailsLevel2Changed ) );

        /// <summary>
        /// Dependency Property for DetailsLevel3Property Property.
        /// </summary>
        public static readonly DependencyProperty DetailsLevel3Property =
            DependencyProperty.Register (
                "DetailsLevel3",
                typeof( DataTemplate ),
                typeof( DetailsSelector ),
                new PropertyMetadata ( DetailsLevel3Changed ) );

        /// <summary>
        /// Dependency Property for DetailsLevelChangedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DetailsLevelChangedCommandProperty =
            DependencyProperty.Register (
                "DetailsLevelChangedCommand",
                typeof( ICommand ),
                typeof( DetailsSelector ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for HasLevel2Property Property.
        /// </summary>
        public static readonly DependencyProperty HasLevel2Property =
            DependencyProperty.Register (
                "HasLevel2",
                typeof( bool ),
                typeof( DetailsSelector ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for HasLevel3Property Property.
        /// </summary>
        public static readonly DependencyProperty HasLevel3Property =
            DependencyProperty.Register (
                "HasLevel3",
                typeof( bool ),
                typeof( DetailsSelector ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for SelectedDetailsProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedDetailsProperty =
            DependencyProperty.Register (
                "SelectedDetails",
                typeof( DataTemplate ),
                typeof( DetailsSelector ),
                new PropertyMetadata ( SelectedDetailsChanged ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsSelector"/> class.
        /// </summary>
        public DetailsSelector ()
        {
            DefaultStyleKey = typeof( DetailsSelector );

            var initSelectedBind = new Binding ( PropertyUtil.ExtractPropertyName ( () => DetailsLevel1 ) );
            initSelectedBind.Source = this;
            initSelectedBind.Mode = BindingMode.OneWay;
            SetBinding ( SelectedDetailsProperty, initSelectedBind );

            SelectDetailsCommand = new DelegateCommand<DataTemplate> ( ExecuteSelectDetailsCommand );
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [selection changed].
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the details level1.
        /// </summary>
        /// <value>The details level1.</value>
        public DataTemplate DetailsLevel1
        {
            get { return ( DataTemplate )GetValue ( DetailsLevel1Property ); }
            set { SetValue ( DetailsLevel1Property, value ); }
        }

        /// <summary>
        /// Gets or sets the details level2.
        /// </summary>
        /// <value>The details level2.</value>
        public DataTemplate DetailsLevel2
        {
            get { return ( DataTemplate )GetValue ( DetailsLevel2Property ); }
            set { SetValue ( DetailsLevel2Property, value ); }
        }

        /// <summary>
        /// Gets or sets the details level3.
        /// </summary>
        /// <value>The details level3.</value>
        public DataTemplate DetailsLevel3
        {
            get { return ( DataTemplate )GetValue ( DetailsLevel3Property ); }
            set { SetValue ( DetailsLevel3Property, value ); }
        }

        /// <summary>
        /// Gets or sets the details level changed command.
        /// </summary>
        /// <value>The details level changed command.</value>
        public ICommand DetailsLevelChangedCommand
        {
            get { return ( ICommand )GetValue ( DetailsLevelChangedCommandProperty ); }
            set { SetValue ( DetailsLevelChangedCommandProperty, value ); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has level2.
        /// </summary>
        public bool HasLevel2
        {
            get { return ( bool )GetValue ( HasLevel2Property ); }
            private set { SetValue ( HasLevel2Property, value ); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has level3.
        /// </summary>
        public bool HasLevel3
        {
            get { return ( bool )GetValue ( HasLevel3Property ); }
            private set { SetValue ( HasLevel3Property, value ); }
        }

        /// <summary>
        /// Gets the select details command.
        /// </summary>
        public ICommand SelectDetailsCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected details.
        /// </summary>
        /// <value>The selected details.</value>
        public DataTemplate SelectedDetails
        {
            get { return ( DataTemplate )GetValue ( SelectedDetailsProperty ); }
            set { SetValue ( SelectedDetailsProperty, value ); }
        }

        #endregion

        #region Methods

        private static void DetailsLevel2Changed ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var db = d as DetailsSelector;
            if ( db != null )
            {
                if ( db.DetailsLevel2 == null )
                {
                    db.HasLevel2 = false;
                }
                else
                {
                    db.HasLevel2 = true;
                }
            }
        }

        private static void DetailsLevel3Changed ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var db = d as DetailsSelector;
            if ( db != null )
            {
                if ( db.DetailsLevel3 == null )
                {
                    db.HasLevel3 = false;
                }
                else
                {
                    db.HasLevel3 = true;
                }
            }
        }

        private static void SelectedDetailsChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var db = d as DetailsSelector;
            if ( db != null )
            {
                if ( db.SelectionChanged != null )
                {
                    db.SelectionChanged ( db, new SelectionChangedEventArgs ( new List<object> { e.OldValue }, new List<object> { e.NewValue } ) );
                }
                if ( db.DetailsLevelChangedCommand != null )
                {
                    db.DetailsLevelChangedCommand.Execute ( e.NewValue );
                }
            }
        }

        private void ExecuteSelectDetailsCommand ( DataTemplate dataTemplate )
        {
            SelectedDetails = dataTemplate;
        }

        #endregion
    }
}
