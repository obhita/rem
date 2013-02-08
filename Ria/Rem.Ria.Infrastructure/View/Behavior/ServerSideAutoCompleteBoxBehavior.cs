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
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.ViewModel;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing server side auto complete box.
    /// </summary>
    public class ServerSideAutoCompleteBoxBehavior : Behavior<AutoCompleteBox>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ItemsSourcePropertyNameProperty Property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourcePropertyNameProperty =
            DependencyProperty.Register (
                "ItemsSourcePropertyName",
                typeof( string ),
                typeof( ServerSideAutoCompleteBoxBehavior ),
                new PropertyMetadata ( string.Empty ) );

        /// <summary>
        /// Dependency Property for SearchCriteriaPropertyNameProperty Property.
        /// </summary>
        public static readonly DependencyProperty SearchCriteriaPropertyNameProperty =
            DependencyProperty.Register (
                "SearchCriteriaPropertyName",
                typeof( string ),
                typeof( ServerSideAutoCompleteBoxBehavior ),
                new PropertyMetadata ( string.Empty ) );

        /// <summary>
        /// Dependency Property for ViewModelProperty Property.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register (
                "ViewModel",
                typeof( NotificationObject ),
                typeof( ServerSideAutoCompleteBoxBehavior ),
                new PropertyMetadata ( null ) );

        private AutoCompleteBox _autoCompleteTextBox;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the items source property.
        /// </summary>
        /// <value>The name of the items source property.</value>
        public string ItemsSourcePropertyName
        {
            get { return ( string )GetValue ( ItemsSourcePropertyNameProperty ); }
            set { SetValue ( ItemsSourcePropertyNameProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the search criteria property.
        /// </summary>
        /// <value>The name of the search criteria property.</value>
        public string SearchCriteriaPropertyName
        {
            get { return ( string )GetValue ( SearchCriteriaPropertyNameProperty ); }
            set { SetValue ( SearchCriteriaPropertyNameProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public NotificationObject ViewModel
        {
            get { return ( NotificationObject )GetValue ( ViewModelProperty ); }
            set { SetValue ( ViewModelProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            _autoCompleteTextBox = AssociatedObject;
            _autoCompleteTextBox.Populating += AssociatedObjectPopulating;
        }

        private void AssociatedObjectPopulating ( object sender, PopulatingEventArgs e )
        {
            e.Cancel = true;

            if ( string.IsNullOrWhiteSpace ( SearchCriteriaPropertyName ) )
            {
                return;
            }

            var searchCriteria = _autoCompleteTextBox.Text;

            // Removing subscription just in case 
            ViewModel.PropertyChanged -= ViewModelPropertyChanged;
            ViewModel.PropertyChanged += ViewModelPropertyChanged;

            var prop = ViewModel.GetType ().GetProperty ( SearchCriteriaPropertyName );
            if ( prop != null )
            {
                prop.SetValue ( ViewModel, searchCriteria, null );
            }
        }

        private void ViewModelPropertyChanged ( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == ItemsSourcePropertyName && _autoCompleteTextBox != null )
            {
                _autoCompleteTextBox.PopulateComplete ();
            }
        }

        #endregion
    }
}
