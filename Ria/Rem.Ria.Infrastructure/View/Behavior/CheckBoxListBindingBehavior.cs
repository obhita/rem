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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Common.Extension;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing check box list binding.
    /// </summary>
    public class CheckBoxListBindingBehavior : Behavior<ItemsControl>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for BoundListProperty Property.
        /// </summary>
        public static readonly DependencyProperty BoundListProperty =
            DependencyProperty.Register (
                "BoundList",
                typeof( IList ),
                typeof( CheckBoxListBindingBehavior ),
                null );

        /// <summary>
        /// Dependency Property for ConverterProperty Property.
        /// </summary>
        public static readonly DependencyProperty ConverterProperty =
            DependencyProperty.Register (
                "Converter",
                typeof( IValueConverter ),
                typeof( CheckBoxListBindingBehavior ),
                null );

        private readonly List<CheckBox> _attachedCheckBoxes = new List<CheckBox> ();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the bound list.
        /// </summary>
        /// <value>The bound list.</value>
        public IList BoundList
        {
            get { return ( IList )GetValue ( BoundListProperty ); }
            set { SetValue ( BoundListProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        public IValueConverter Converter
        {
            get { return ( IValueConverter )GetValue ( ConverterProperty ); }
            set { SetValue ( ConverterProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();

            AssociatedObject.LayoutUpdated += AssociatedObject_LayoutUpdated;
        }

        private void AssociatedObject_LayoutUpdated ( object sender, EventArgs e )
        {
            var checkBoxes = new List<CheckBox> ();
            AssociatedObject.FindVisualChildren ( true, ref checkBoxes );

            foreach ( var checkBox in checkBoxes.Where ( p => !_attachedCheckBoxes.Contains ( p ) ) )
            {
                _attachedCheckBoxes.Add ( checkBox );
            }

            foreach ( var attachedCheckBox in _attachedCheckBoxes )
            {
                attachedCheckBox.Checked -= CheckBox_Checked;
                attachedCheckBox.Unchecked -= CheckBox_Checked;

                attachedCheckBox.IsChecked = false;
                SetIsChecked ( attachedCheckBox );

                attachedCheckBox.Checked += CheckBox_Checked;
                attachedCheckBox.Unchecked += CheckBox_Checked;
            }
        }

        private void SetIsChecked ( CheckBox checkBox )
        {
            var name = ( checkBox.Content as TextBlock ).Text; // as string;

            foreach ( var item in BoundList )
            {
                var i = item as LookupValueDto;

                if ( i != null && i.Name == name )
                {
                    checkBox.IsChecked = true;
                }
            }
        }

        private void CheckBox_Checked ( object sender, RoutedEventArgs e )
        {
            // update the bound list according to this event
            // 1: cast to checkbox and get the DataContext
            // 2: update the bound list using that model/DataContext

            var cb = sender as CheckBox;

            if ( cb == null )
            {
                throw new ArgumentException ( "This only works with CheckBoxes." );
            }

            var model = cb.DataContext;

            if ( model == null )
            {
                throw new ArgumentException ( "DataContext came back as null" );
            }

            if ( cb.IsChecked != null && cb.IsChecked.Value )
            {
                // if is checked, add it
                if ( !BoundList.Contains ( model ) )
                {
                    BoundList.Add ( model );
                }
            }
            else if ( cb.IsChecked == null || !cb.IsChecked.Value )
            {
                if ( BoundList.Contains ( model ) )
                {
                    BoundList.Remove ( model );
                }
            }
        }

        #endregion
    }
}
