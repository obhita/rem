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
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// ExtendedCollectionViewSource class.
    /// </summary>
    public class ExtendedCollectionViewSource : CollectionViewSource
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CanFilterProperty Property.
        /// </summary>
        public static readonly DependencyProperty CanFilterProperty =
            DependencyProperty.Register (
                "CanFilter",
                typeof( bool ),
                typeof( ExtendedCollectionViewSource ),
                new PropertyMetadata ( false, OnCanFilterChanged ) );

        /// <summary>
        /// Dependency Property for FilterCompareValueProperty Property.
        /// </summary>
        public static readonly DependencyProperty FilterCompareValueProperty =
            DependencyProperty.Register (
                "FilterCompareValue",
                typeof( object ),
                typeof( ExtendedCollectionViewSource ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for FilterPropertyNameProperty Property.
        /// </summary>
        public static readonly DependencyProperty FilterPropertyNameProperty =
            DependencyProperty.Register (
                "FilterPropertyName",
                typeof( string ),
                typeof( ExtendedCollectionViewSource ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for RadGridViewProperty Property.
        /// </summary>
        public static readonly DependencyProperty RadGridViewProperty =
            DependencyProperty.Register (
                "RadGridView",
                typeof( RadGridView ),
                typeof( ExtendedCollectionViewSource ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SelectedGroupDescriptionProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedGroupDescriptionProperty =
            DependencyProperty.Register (
                "SelectedGroupDescription",
                typeof( GroupDescription ),
                typeof( ExtendedCollectionViewSource ),
                new PropertyMetadata ( null, OnSelectedGroupDescriptionChanged ) );

        private static readonly DependencyProperty FilterPropertyValueProperty =
            DependencyProperty.Register (
                "FilterPropertyValue",
                typeof( object ),
                typeof( ExtendedCollectionViewSource ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance can filter.
        /// </summary>
        /// <value><c>true</c> if this instance can filter; otherwise, <c>false</c>.</value>
        public bool CanFilter
        {
            get { return ( bool )GetValue ( CanFilterProperty ); }
            set { SetValue ( CanFilterProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the filter compare value.
        /// </summary>
        /// <value>The filter compare value.</value>
        public object FilterCompareValue
        {
            get { return GetValue ( FilterCompareValueProperty ); }
            set { SetValue ( FilterCompareValueProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the filter property.
        /// </summary>
        /// <value>The name of the filter property.</value>
        public string FilterPropertyName
        {
            get { return ( string )GetValue ( FilterPropertyNameProperty ); }
            set { SetValue ( FilterPropertyNameProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the RAD grid view.
        /// </summary>
        /// <value>The RAD grid view.</value>
        public RadGridView RadGridView
        {
            get { return ( RadGridView )GetValue ( RadGridViewProperty ); }
            set { SetValue ( RadGridViewProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selected group description.
        /// </summary>
        /// <value>The selected group description.</value>
        public GroupDescription SelectedGroupDescription
        {
            get { return ( GroupDescription )GetValue ( SelectedGroupDescriptionProperty ); }
            set { SetValue ( SelectedGroupDescriptionProperty, value ); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the filter property value.
        /// </summary>
        /// <value>The filter property value.</value>
        private object FilterPropertyValue
        {
            get { return GetValue ( FilterPropertyValueProperty ); }
            set { SetValue ( FilterPropertyValueProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:CanFilterChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCanFilterChanged ( DependencyPropertyChangedEventArgs e )
        {
            if ( ( bool )e.OldValue )
            {
                Filter -= HandleFilter;
            }
            if ( ( bool )e.NewValue )
            {
                Filter += HandleFilter;
            }
            if ( View != null )
            {
                View.Refresh ();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:SelectedGroupDescriptionChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSelectedGroupDescriptionChanged ( DependencyPropertyChangedEventArgs e )
        {
            if ( RadGridView != null )
            {
                using ( RadGridView.DeferRefresh () )
                {
                    UpdateGrouping ( e.NewValue as GroupDescription );
                }
            }
            else
            {
                UpdateGrouping ( e.NewValue as GroupDescription );
            }
        }

        private static void OnCanFilterChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( ExtendedCollectionViewSource )d ).OnCanFilterChanged ( e );
        }

        private static void OnSelectedGroupDescriptionChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( ExtendedCollectionViewSource )d ).OnSelectedGroupDescriptionChanged ( e );
        }

        private void HandleFilter ( object sender, FilterEventArgs e )
        {
            var filterPropertyBinding = new Binding ();
            filterPropertyBinding.Source = e.Item;
            if ( !string.IsNullOrWhiteSpace ( FilterPropertyName ) )
            {
                filterPropertyBinding.Path = new PropertyPath ( FilterPropertyName );
            }
            ClearValue ( FilterPropertyValueProperty );
            BindingOperations.SetBinding ( this, FilterPropertyValueProperty, filterPropertyBinding );
            e.Accepted = FilterCompareValue == FilterPropertyValue;
        }

        private void UpdateGrouping ( GroupDescription groupDescription )
        {
            GroupDescriptions.Clear ();
            if ( groupDescription != null )
            {
                GroupDescriptions.Add ( groupDescription );
            }
            if ( View != null )
            {
                View.Refresh ();
            }
        }

        #endregion
    }
}
