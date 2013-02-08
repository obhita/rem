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
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for picking editable date.
    /// </summary>
    public class EditableDatePicker : RadDatePicker
    {
        #region Constants and Fields

        private static readonly DependencyProperty IsReadOnlyWatcherProperty =
            DependencyProperty.RegisterAttached (
                "IsReadOnlyWatcher",
                typeof( bool ),
                typeof( EditableDatePicker ),
                new PropertyMetadata ( false, OnIsReadOnlyWatcherChanged ) );

        private Border _background;
        private RadDropDownButton _dropDown;
        private RadWatermarkTextBox _textBox;

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code
        /// or internal processes (such as a rebuilding layout pass) call System.Windows.Controls.Control.ApplyTemplate().
        /// In simplest terms, this means the method is called just before a UI element
        /// displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            _background = GetTemplateChild ( "BackgroundVisual" ) as Border;
            _dropDown = GetTemplateChild("PART_DropDownButton") as RadDropDownButton;
            _textBox = GetTemplateChild("PART_DateTimeInput") as RadWatermarkTextBox;
            SetBinding ( IsReadOnlyWatcherProperty, new Binding { Source = this, Path = new PropertyPath ( "IsReadOnly" ) } );

            _textBox.Style = ( Style )Application.Current.Resources["EditableWatermarkTextBoxStyle"];
        }

        #endregion

        #region Methods

        private static void OnIsReadOnlyWatcherChanged ( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {
            var datePicker = ( sender as EditableDatePicker );
            if ( datePicker != null )
            {
                datePicker.HandleReadOnlyChanged ();
            }
        }

        private void HandleReadOnlyChanged ()
        {
            if ( IsReadOnly )
            {
                IsHitTestVisible = false;
                _background.Visibility = Visibility.Collapsed;
                _dropDown.Visibility = Visibility.Collapsed;
            }
            else
            {
                IsHitTestVisible = true;
                _background.Visibility = Visibility.Visible;
                _dropDown.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}
