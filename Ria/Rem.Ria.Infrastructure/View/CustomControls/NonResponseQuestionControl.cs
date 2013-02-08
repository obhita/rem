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
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// NonResponseQuestionControl class.
    /// </summary>
    public class NonResponseQuestionControl : ValidationControl, IReadOnly
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( new CornerRadius ( 0, 0, 0, 0 ) ) );

        /// <summary>
        /// Dependency Property for DefaultNonResponseFiltersProperty Property.
        /// </summary>
        public static readonly DependencyProperty DefaultNonResponseFiltersProperty =
            DependencyProperty.Register (
                "DefaultNonResponseFilters",
                typeof( IEnumerable<string> ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null, NonResponseFiltersChanged ) );

        /// <summary>
        /// Dependency Property for HintProperty Property.
        /// </summary>
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register (
                "Hint",
                typeof( string ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( string.Empty ) );

        /// <summary>
        /// Dependency Property for HorizontalContentAlignmentProperty Property.
        /// </summary>
        public static readonly new DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register (
                "HorizontalContentAlignment",
                typeof( HorizontalAlignment ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( HorizontalAlignment.Stretch, OnHorizontalContentAlignmentChanged ) );

        /// <summary>
        /// Dependency Property for IsReadOnlyProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register (
                "IsReadOnly",
                typeof( bool ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( true, IsReadOnlyChanged ) );

        /// <summary>
        /// Dependency Property for NonResponseColumnWidthProperty Property.
        /// </summary>
        public static readonly DependencyProperty NonResponseColumnWidthProperty =
            DependencyProperty.Register (
                "NonResponseColumnWidth",
                typeof( double ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for NonResponseDtoProperty Property.
        /// </summary>
        public static readonly DependencyProperty NonResponseDtoProperty =
            DependencyProperty.Register (
                "NonResponseDto",
                typeof( INonResponseDto ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null, NonResponseDtoChanged ) );

        /// <summary>
        /// Dependency Property for NonResponseFiltersProperty Property.
        /// </summary>
        public static readonly DependencyProperty NonResponseFiltersProperty =
            DependencyProperty.Register (
                "NonResponseFilters",
                typeof( IEnumerable<string> ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null, NonResponseFiltersChanged ) );

        /// <summary>
        /// Dependency Property for NonResponseItemTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty NonResponseItemTemplateProperty =
            DependencyProperty.Register (
                "NonResponseItemTemplate",
                typeof( DataTemplate ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for NonResponseItemsProperty Property.
        /// </summary>
        public static readonly DependencyProperty NonResponseItemsProperty =
            DependencyProperty.Register (
                "NonResponseItems",
                typeof( CollectionViewSource ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for NonResponseItemsSourceProperty Property.
        /// </summary>
        public static readonly DependencyProperty NonResponseItemsSourceProperty =
            DependencyProperty.Register (
                "NonResponseItemsSource",
                typeof( IList<LookupValueDto> ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null, NonResponseItemsSourceChanged ) );

        /// <summary>
        /// Dependency Property for QuestionProperty Property.
        /// </summary>
        public static readonly DependencyProperty QuestionProperty =
            DependencyProperty.Register (
                "Question",
                typeof( object ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for QuestionTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty QuestionTemplateProperty =
            DependencyProperty.Register (
                "QuestionTemplate",
                typeof( DataTemplate ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ReadOnlyValueTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ReadOnlyValueTemplateProperty =
            DependencyProperty.Register (
                "ReadOnlyValueTemplate",
                typeof( DataTemplate ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ValueTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ValueTemplateProperty =
            DependencyProperty.Register (
                "ValueTemplate",
                typeof( DataTemplate ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SharedGridLengthGroupNameProperty Property.
        /// </summary>
        public static readonly DependencyProperty SharedGridLengthGroupNameProperty =
            DependencyProperty.Register (
                "SharedGridLengthGroupName",
                typeof( string ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for MaxQuestionColumnWidthProperty Property.
        /// </summary>
        public static readonly DependencyProperty MaxQuestionColumnWidthProperty =
            DependencyProperty.Register (
                "MaxQuestionColumnWidth",
                typeof( double ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( double.PositiveInfinity ) );

        /// <summary>
        /// Dependency Property for NonResponseItemsPanelProperty Property.
        /// </summary>
        public static readonly DependencyProperty NonResponseItemsPanelProperty =
            DependencyProperty.Register (
                "NonResponseItemsPanel",
                typeof( ItemsPanelTemplate ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for FullNonResponseTextVisibilityProperty Property.
        /// </summary>
        public static readonly DependencyProperty FullNonResponseTextVisibilityProperty =
            DependencyProperty.Register (
                "FullNonResponseTextVisibility",
                typeof( Visibility ),
                typeof( NonResponseQuestionControl ),
                new PropertyMetadata ( Visibility.Visible ) );

        /// <summary>
        /// Dependency Property for LookupValueOverrideProperty Property.
        /// </summary>
        public static readonly DependencyProperty LookupValueOverrideProperty =
            DependencyProperty.Register("LookupValueOverride", typeof(string), typeof(NonResponseQuestionControl), new PropertyMetadata(null));

        private readonly object _isFilteryingSync = new object ();
        private bool _isFiltering;
        private ListBox _nonResponseListBox;
        private ColumnDefinition _questionColumn;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NonResponseQuestionControl"/> class.
        /// </summary>
        public NonResponseQuestionControl ()
        {
            DefaultStyleKey = typeof( NonResponseQuestionControl );
            NonResponseItems = new CollectionViewSource ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the lookup value override.
        /// </summary>
        /// <value>The lookup value override.</value>
        public string LookupValueOverride
        {
            get { return (string) GetValue(LookupValueOverrideProperty); }
            set { SetValue(LookupValueOverrideProperty, value); }
        }

        /// <summary>
        /// Gets or sets the full non response text visibility.
        /// </summary>
        /// <value>The full non response text visibility.</value>
        public Visibility FullNonResponseTextVisibility
        {
            get { return (Visibility) GetValue(FullNonResponseTextVisibilityProperty); }
            set { SetValue(FullNonResponseTextVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public CornerRadius CornerRadius
        {
            get { return ( CornerRadius )GetValue ( CornerRadiusProperty ); }
            set { SetValue ( CornerRadiusProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the default non response filters.
        /// </summary>
        /// <value>The default non response filters.</value>
        public IEnumerable<string> DefaultNonResponseFilters
        {
            get { return ( IEnumerable<string> )GetValue ( DefaultNonResponseFiltersProperty ); }
            set { SetValue ( DefaultNonResponseFiltersProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the hint.
        /// </summary>
        /// <value>The hint of the question.</value>
        public string Hint
        {
            get { return ( string )GetValue ( HintProperty ); }
            set { SetValue ( HintProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the horizontal alignment of the control's content.
        /// </summary>
        /// <value>The horizontal content alignment.</value>
        /// <returns>One of the <see cref="T:System.Windows.HorizontalAlignment"/> values. The default is <see cref="F:System.Windows.HorizontalAlignment.Center"/>.</returns>
        public new HorizontalAlignment HorizontalContentAlignment
        {
            get { return ( HorizontalAlignment )GetValue ( HorizontalContentAlignmentProperty ); }
            set { SetValue ( HorizontalContentAlignmentProperty, value ); }
        }

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
        /// Gets or sets the width of the non response column.
        /// </summary>
        /// <value>The width of the non response column.</value>
        public double NonResponseColumnWidth
        {
            get { return ( double )GetValue ( NonResponseColumnWidthProperty ); }
            set { SetValue ( NonResponseColumnWidthProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the non response dto.
        /// </summary>
        /// <value>The non response dto.</value>
        public INonResponseDto NonResponseDto
        {
            get { return ( INonResponseDto )GetValue ( NonResponseDtoProperty ); }
            set { SetValue ( NonResponseDtoProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the non response filters.
        /// </summary>
        /// <value>The non response filters.</value>
        public IEnumerable<string> NonResponseFilters
        {
            get { return ( IEnumerable<string> )GetValue ( NonResponseFiltersProperty ); }
            set { SetValue ( NonResponseFiltersProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the non response item template.
        /// </summary>
        /// <value>The non response item template.</value>
        public DataTemplate NonResponseItemTemplate
        {
            get { return ( DataTemplate )GetValue ( NonResponseItemTemplateProperty ); }
            set { SetValue ( NonResponseItemTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets the non response items.
        /// </summary>
        public CollectionViewSource NonResponseItems
        {
            get { return ( CollectionViewSource )GetValue ( NonResponseItemsProperty ); }
            private set { SetValue ( NonResponseItemsProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the non response items source.
        /// </summary>
        /// <value>The non response items source.</value>
        public IList<LookupValueDto> NonResponseItemsSource
        {
            get { return ( IList<LookupValueDto> )GetValue ( NonResponseItemsSourceProperty ); }
            set { SetValue ( NonResponseItemsSourceProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        public object Question
        {
            get { return GetValue ( QuestionProperty ); }
            set { SetValue ( QuestionProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the question template.
        /// </summary>
        /// <value>The question template.</value>
        public DataTemplate QuestionTemplate
        {
            get { return ( DataTemplate )GetValue ( QuestionTemplateProperty ); }
            set { SetValue ( QuestionTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the read only value template.
        /// </summary>
        /// <value>The read only value template.</value>
        public DataTemplate ReadOnlyValueTemplate
        {
            get { return ( DataTemplate )GetValue ( ReadOnlyValueTemplateProperty ); }
            set { SetValue ( ReadOnlyValueTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the value template.
        /// </summary>
        /// <value>The value template.</value>
        public DataTemplate ValueTemplate
        {
            get { return ( DataTemplate )GetValue ( ValueTemplateProperty ); }
            set { SetValue ( ValueTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the shared grid length group.
        /// </summary>
        /// <value>The name of the shared grid length group.</value>
        public string SharedGridLengthGroupName
        {
            get { return (string) GetValue(SharedGridLengthGroupNameProperty); }
            set { SetValue(SharedGridLengthGroupNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the max question column.
        /// </summary>
        /// <value>The width of the max question column.</value>
        public double MaxQuestionColumnWidth
        {
            get { return (double) GetValue(MaxQuestionColumnWidthProperty); }
            set { SetValue(MaxQuestionColumnWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the non response items panel.
        /// </summary>
        /// <value>The non response items panel.</value>
        public ItemsPanelTemplate NonResponseItemsPanel
        {
            get { return (ItemsPanelTemplate) GetValue(NonResponseItemsPanelProperty); }
            set { SetValue(NonResponseItemsPanelProperty, value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when [apply template].
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();

            _nonResponseListBox = GetTemplateChild ( "PART_NonResponse" ) as ListBox;
            _questionColumn = GetTemplateChild ( "PART_QuestionColumn" ) as ColumnDefinition;

            FixQuestionColumn ();
            UpdateSelectedItemBinding ();
        }

        #endregion

        #region Methods

        private static void IsReadOnlyChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var nonResponseContentControl = d as NonResponseQuestionControl;
            if ( nonResponseContentControl != null )
            {
                nonResponseContentControl.LayoutUpdated += nonResponseContentControl.ReadOnlyLayoutUpdated;
            }
        }

        private static void NonResponseDtoChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var nonResponseContentControl = d as NonResponseQuestionControl;
            if ( nonResponseContentControl != null )
            {
                nonResponseContentControl.HandleNonResponseDtoChanged ( e.OldValue as INonResponseDto );
            }
        }

        private static void NonResponseFiltersChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var nonResponseContentControl = d as NonResponseQuestionControl;
            if ( nonResponseContentControl != null )
            {
                lock ( nonResponseContentControl._isFilteryingSync )
                {
                    if ( nonResponseContentControl.DefaultNonResponseFilters == null && nonResponseContentControl.NonResponseFilters == null )
                    {
                        nonResponseContentControl.RemoveFilter ();
                        nonResponseContentControl.RefreshView ();
                    }
                    else if ( !nonResponseContentControl._isFiltering )
                    {
                        nonResponseContentControl.AddFilter ();
                        nonResponseContentControl._isFiltering = true;
                        nonResponseContentControl.RefreshView ();
                    }
                }
            }
        }

        private static void NonResponseItemsSourceChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var nonResponseContentControl = d as NonResponseQuestionControl;
            if ( nonResponseContentControl != null )
            {
                nonResponseContentControl.NonResponseItems.Source = nonResponseContentControl.NonResponseItemsSource;
                if ( nonResponseContentControl.NonResponseItems.View != null )
                {
                    nonResponseContentControl.RefreshView ();
                }
            }
        }

        private static void OnHorizontalContentAlignmentChanged ( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {
            var control = sender as NonResponseQuestionControl;
            if ( control != null )
            {
                control.FixQuestionColumn ();
                control.SetHorizontalContentAlignmentBase ();
            }
        }

        private void FixQuestionColumn()
        {
            if (_questionColumn != null)
            {
                switch ( HorizontalContentAlignment )
                {
                    case HorizontalAlignment.Stretch:
                        _questionColumn.Width = new GridLength ( 1, GridUnitType.Star );
                        break;
                    default:
                        _questionColumn.Width = new GridLength ( 1, GridUnitType.Auto );
                        break;
                }
            }
        }

        private void SetHorizontalContentAlignmentBase()
        {
            base.HorizontalContentAlignment = HorizontalContentAlignment;
        }

        private void AddFilter ()
        {
            UpdateSelectedItemBinding ( true );
            NonResponseItems.Filter += NonResponseItemsFilter;
            UpdateSelectedItemBinding ();
        }

        private void HandleNonResponseDtoChanged ( INonResponseDto oldValue )
        {
            if ( oldValue != null )
            {
                oldValue.PropertyChanged -= HandleNonResponseDtoPropertyChanged;
            }
            if ( NonResponseDto != null )
            {
                NonResponseDto.PropertyChanged += HandleNonResponseDtoPropertyChanged;
            }
        }

        private void HandleNonResponseDtoPropertyChanged ( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => NonResponseDto.ValueObject ) )
            {
                if ( NonResponseDto.ValueObject != null )
                {
                    NonResponseDto.NonResponse = null;
                }
            }
            else if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => NonResponseDto.NonResponse ) )
            {
                if ( NonResponseDto.NonResponse != null )
                {
                    NonResponseDto.ValueObject = NonResponseDto.IsNullable ? null : NonResponseDto.ValueObject.GetType ().GetDefault ();
                }
            }
        }

        private void NonResponseItemsFilter ( object sender, FilterEventArgs e )
        {
            if ( NonResponseFilters == null || NonResponseFilters.Count () == 0 )
            {
                e.Accepted = ( DefaultNonResponseFilters == null || DefaultNonResponseFilters.Count () == 0
                               || DefaultNonResponseFilters.Contains ( ( e.Item as LookupValueDto ).WellKnownName ) );
            }
            else
            {
                e.Accepted = NonResponseFilters.Contains ( ( e.Item as LookupValueDto ).WellKnownName );
            }
        }

        private void ReadOnlyLayoutUpdated ( object sender, EventArgs e )
        {
            LayoutUpdated -= ReadOnlyLayoutUpdated;
            UpdateLayout ();
        }

        private void RefreshView ()
        {
            if ( NonResponseItems != null && NonResponseItems.View != null )
            {
                UpdateSelectedItemBinding ( true );
                NonResponseItems.View.Refresh ();
                UpdateSelectedItemBinding ();
            }
        }

        private void RemoveFilter ()
        {
            UpdateSelectedItemBinding ( true );
            NonResponseItems.Filter -= NonResponseItemsFilter;
            UpdateSelectedItemBinding ();
        }

        private void UpdateSelectedItemBinding ( bool remove = false )
        {
            if ( _nonResponseListBox != null )
            {
                if ( remove )
                {
                    _nonResponseListBox.ClearValue ( Selector.SelectedItemProperty );
                }
                else
                {
                    var nonResponseSelectedBinding = new Binding ();
                    nonResponseSelectedBinding.Source = this;
                    nonResponseSelectedBinding.Path = new PropertyPath ( "NonResponseDto.NonResponse" );
                    nonResponseSelectedBinding.Mode = BindingMode.TwoWay;
                    _nonResponseListBox.SetBinding ( Selector.SelectedItemProperty, nonResponseSelectedBinding );
                }
            }
        }

        #endregion
    }
}
