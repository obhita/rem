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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Primitives;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;
using PropertyMetadata = System.Windows.PropertyMetadata;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;
using SelectionChangedEventHandler = System.Windows.Controls.SelectionChangedEventHandler;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// SearchBox class.
    /// </summary>
    [TemplatePart ( Name = "PART_ResultsGrid", Type = typeof( RadGridView ) )]
    [TemplatePart ( Name = "PART_Popup", Type = typeof( Popup ) )]
    [TemplatePart ( Name = "PART_SearchTextBox", Type = typeof( TextBox ) )]
    [TemplatePart ( Name = "PART_PopupTarget", Type = typeof( Grid ) )]
    [TemplatePart ( Name = "Part_DropDown", Type = typeof( RadToggleButton ) )]
    [TemplateVisualState ( Name = "WatermarkVisible", GroupName = "WatermarkStates" )]
    [TemplateVisualState ( Name = "WatermarkInvisible", GroupName = "WatermarkStates" )]
    [TemplateVisualState ( Name = "Valid", GroupName = "ValidationStates" )]
    [TemplateVisualState ( Name = "InvalidUnfocused", GroupName = "ValidationStates" )]
    public class SearchBox : Control
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AdvancedButtonStyleProperty Property.
        /// </summary>
        public static readonly DependencyProperty AdvancedButtonStyleProperty =
            DependencyProperty.Register (
                "AdvancedButtonStyle",
                typeof( Style ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for AdvancedContentProperty Property.
        /// </summary>
        public static readonly DependencyProperty AdvancedContentProperty =
            DependencyProperty.Register (
                "AdvancedContent",
                typeof( FrameworkElement ),
                typeof( SearchBox ),
                new PropertyMetadata ( AdvancedContentChanged ) );

        /// <summary>
        /// Dependency Property for AdvancedSearchVisibilityProperty Property.
        /// </summary>
        public static readonly DependencyProperty AdvancedSearchVisibilityProperty =
            DependencyProperty.Register (
                "AdvancedSearchVisibility",
                typeof( Visibility ),
                typeof( SearchBox ),
                new PropertyMetadata ( Visibility.Visible ) );

        /// <summary>
        /// Dependency Property for AllowDragProperty Property.
        /// </summary>
        public static readonly DependencyProperty AllowDragProperty =
            DependencyProperty.Register (
                "AllowDrag",
                typeof( bool ),
                typeof( SearchBox ),
                new PropertyMetadata ( false, AllowDragChanged ) );

        /// <summary>
        /// Dependency Property for ClearSelectionWhenAbortedProperty Property.
        /// </summary>
        public static readonly DependencyProperty ClearSelectionWhenAbortedProperty =
            DependencyProperty.Register (
                "ClearSelectionWhenAborted",
                typeof( bool ),
                typeof( SearchBox ),
                new PropertyMetadata ( true ) );

        /// <summary>
        /// Dependency Property for CloseAdvancedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty CloseAdvancedCommandProperty =
            DependencyProperty.Register (
                "CloseAdvancedCommand",
                typeof( ICommand ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CloseOnLostFocusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CloseOnLostFocusProperty =
            DependencyProperty.Register (
                "CloseOnLostFocus",
                typeof( bool ),
                typeof( SearchBox ),
                new PropertyMetadata ( true ) );

        /// <summary>
        /// Dependency Property for ClosePopupCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty ClosePopupCommandProperty =
            DependencyProperty.Register (
                "ClosePopupCommand",
                typeof( ICommand ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( SearchBox ),
                new PropertyMetadata ( new CornerRadius ( 0, 0, 0, 0 ) ) );

        /// <summary>
        /// Dependency Property for CreateNewCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty CreateNewCommandProperty =
            DependencyProperty.Register (
                "CreateNewCommand",
                typeof( ICommand ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CreateNewContentProperty Property.
        /// </summary>
        public static readonly DependencyProperty CreateNewContentProperty =
            DependencyProperty.Register (
                "CreateNewContent",
                typeof( FrameworkElement ),
                typeof( SearchBox ),
                new PropertyMetadata ( CreateNewContentChanged ) );

        /// <summary>
        /// Dependency Property for CreateNewPatientStackPanelVisibilityProperty Property.
        /// </summary>
        public static readonly DependencyProperty CreateNewPatientStackPanelVisibilityProperty =
            DependencyProperty.Register (
                "CreateNewPatientStackPanelVisibility",
                typeof( Visibility ),
                typeof( SearchBox ),
                new PropertyMetadata ( Visibility.Collapsed ) );

        /// <summary>
        /// Dependency Property for DragCompletedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DragCompletedCommandProperty =
            DependencyProperty.Register (
                "DragCompletedCommand",
                typeof( ICommand ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for DragStartingCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DragStartingCommandProperty =
            DependencyProperty.Register (
                "DragStartingCommand",
                typeof( ICommand ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for EmptyTextProperty Property.
        /// </summary>
        public static readonly DependencyProperty EmptyTextProperty =
            DependencyProperty.Register (
                "EmptyText",
                typeof( string ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for FilterTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty FilterTemplateProperty =
            DependencyProperty.Register (
                "FilterTemplate",
                typeof( DataTemplate ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for FiltersProperty Property.
        /// </summary>
        public static readonly DependencyProperty FiltersProperty =
            DependencyProperty.Register (
                "Filters",
                typeof( IEnumerable ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for IsPopUpOpenProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsPopUpOpenProperty =
            DependencyProperty.Register (
                "IsPopUpOpen",
                typeof( bool ),
                typeof( SearchBox ),
                new PropertyMetadata ( false, IsPopUpOpenChanged ) );

        /// <summary>
        /// Dependency Property for ManualSearchCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty ManualSearchCommandProperty =
            DependencyProperty.Register (
                "ManualSearchCommand",
                typeof( ICommand ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for MaxDropDownHeightProperty Property.
        /// </summary>
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register (
                "MaxDropDownHeight",
                typeof( double ),
                typeof( SearchBox ),
                new PropertyMetadata ( double.MaxValue ) );

        /// <summary>
        /// Dependency Property for MinimumQuickSearchCriteriaLengthProperty Property.
        /// </summary>
        public static readonly DependencyProperty MinimumQuickSearchCriteriaLengthProperty =
            DependencyProperty.Register (
                "MinimumQuickSearchCriteriaLength",
                typeof( int ),
                typeof( SearchBox ),
                new PropertyMetadata ( 3 ) );

        /// <summary>
        /// Dependency Property for PageIndexProperty Property.
        /// </summary>
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register (
                "PageIndex",
                typeof( int ),
                typeof( SearchBox ),
                new PropertyMetadata ( 0 ) );

        /// <summary>
        /// Dependency Property for PageSizeProperty Property.
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register (
                "PageSize",
                typeof( int ),
                typeof( SearchBox ),
                new PropertyMetadata ( 0 ) );

        /// <summary>
        /// Dependency Property for PopupAlignmentProperty Property.
        /// </summary>
        public static readonly DependencyProperty PopupAlignmentProperty =
            DependencyProperty.Register (
                "PopupAlignment",
                typeof( HorizontalAlignment ),
                typeof( SearchBox ),
                new PropertyMetadata ( HorizontalAlignment.Center, PopupAlignmentChanged ) );

        /// <summary>
        /// Dependency Property for QuickSearchTextProperty Property.
        /// </summary>
        public static readonly DependencyProperty QuickSearchTextProperty =
            DependencyProperty.Register (
                "QuickSearchText",
                typeof( string ),
                typeof( SearchBox ),
                new PropertyMetadata ( QuickSearchTextChanged ) );

        /// <summary>
        /// Dependency Property for ResultLevel1DetailTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ResultLevel1DetailTemplateProperty =
            DependencyProperty.Register (
                "ResultLevel1DetailTemplate",
                typeof( DataTemplate ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ResultLevel2DetailTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ResultLevel2DetailTemplateProperty =
            DependencyProperty.Register (
                "ResultLevel2DetailTemplate",
                typeof( DataTemplate ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ResultLevel3DetailTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ResultLevel3DetailTemplateProperty =
            DependencyProperty.Register (
                "ResultLevel3DetailTemplate",
                typeof( DataTemplate ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ResultTemplatesProperty Property.
        /// </summary>
        public static readonly DependencyProperty ResultTemplatesProperty =
            DependencyProperty.Register (
                "ResultTemplates",
                typeof( ObservableCollection<DataTemplate> ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ResultsProperty Property.
        /// </summary>
        public static readonly DependencyProperty ResultsProperty =
            DependencyProperty.Register (
                "Results",
                typeof( PagedCollectionView ),
                typeof( SearchBox ),
                new PropertyMetadata ( ResultsChanged ) );

        /// <summary>
        /// Dependency Property for RowStyleProperty Property.
        /// </summary>
        public static readonly DependencyProperty RowStyleProperty =
            DependencyProperty.Register (
                "RowStyle",
                typeof( Style ),
                typeof( SearchBox ),
                new PropertyMetadata ( RowStyleChanged ) );

        /// <summary>
        /// Dependency Property for SelectedFilterProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedFilterProperty =
            DependencyProperty.Register (
                "SelectedFilter",
                typeof( object ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SelectedResultProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedResultProperty =
            DependencyProperty.Register (
                "SelectedResult",
                typeof( object ),
                typeof( SearchBox ),
                new PropertyMetadata ( SelectedResultChanged ) );

        /// <summary>
        /// Dependency Property for SelectionChangedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.Register (
                "SelectionChangedCommand",
                typeof( ICommand ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ShowDetailsButtonProperty Property.
        /// </summary>
        public static readonly DependencyProperty ShowDetailsButtonProperty =
            DependencyProperty.Register (
                "ShowDetailsButton",
                typeof( bool ),
                typeof( SearchBox ),
                new PropertyMetadata ( true ) );

        /// <summary>
        /// Dependency Property for StatusProperty Property.
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register (
                "Status",
                typeof( string ),
                typeof( SearchBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for TotalItemCountProperty Property.
        /// </summary>
        public static readonly DependencyProperty TotalItemCountProperty =
            DependencyProperty.Register (
                "TotalItemCount",
                typeof( int ),
                typeof( SearchBox ),
                new PropertyMetadata ( 0 ) );

        /// <summary>
        /// Dependency Property for UseManualSearchProperty Property.
        /// </summary>
        public static readonly DependencyProperty UseManualSearchProperty =
            DependencyProperty.Register (
                "UseManualSearch",
                typeof( bool ),
                typeof( SearchBox ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for SingleResultEnterCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty SingleResultEnterCommandProperty =
            DependencyProperty.Register("SingleResultEnterCommand", typeof(ICommand), typeof(SearchBox), new PropertyMetadata(null));

        private RadToggleButton _dropDownButton;
        private Popup _popup;
        private Grid _popupTarget;
        private bool _refreshed;
        private RadGridView _resultsGrid;
        private TextBox _searchTextBox;
        private bool _selectionMade;
        private bool _textBoxHasFocus;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBox"/> class.
        /// </summary>
        public SearchBox ()
        {
            DefaultStyleKey = typeof( SearchBox );
            ClosePopupCommand = new DelegateCommand ( ExecuteCloseCommand );
            CreateNewCommand = new DelegateCommand ( ExecuteCreateNewCommand );
            CloseAdvancedCommand = new DelegateCommand ( ExecuteCloseAdvancedCommand );
            DetailLevelChangedCommand = new DelegateCommand<DataTemplate> ( ExecuteDetailLevelChanged );
            ResultTemplates = new ObservableCollection<DataTemplate> ();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [search aborted].
        /// </summary>
        public event EventHandler SearchAborted;

        /// <summary>
        /// Occurs when [selection changed].
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the single result enter command.
        /// </summary>
        /// <value>The single result enter command.</value>
        public ICommand SingleResultEnterCommand
        {
            get { return (ICommand) GetValue(SingleResultEnterCommandProperty); }
            set { SetValue(SingleResultEnterCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the advanced button style.
        /// </summary>
        /// <value>The advanced button style.</value>
        public Style AdvancedButtonStyle
        {
            get { return ( Style )GetValue ( AdvancedButtonStyleProperty ); }
            set { SetValue ( AdvancedButtonStyleProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the content of the advanced.
        /// </summary>
        /// <value>The content of the advanced.</value>
        public FrameworkElement AdvancedContent
        {
            get { return ( FrameworkElement )GetValue ( AdvancedContentProperty ); }
            set { SetValue ( AdvancedContentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the advanced search visibility.
        /// </summary>
        /// <value>The advanced search visibility.</value>
        public Visibility AdvancedSearchVisibility
        {
            get { return ( Visibility )GetValue ( AdvancedSearchVisibilityProperty ); }
            set { SetValue ( AdvancedSearchVisibilityProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow drag].
        /// </summary>
        /// <value><c>true</c> if [allow drag]; otherwise, <c>false</c>.</value>
        public bool AllowDrag
        {
            get { return ( bool )GetValue ( AllowDragProperty ); }
            set { SetValue ( AllowDragProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [clear selection when aborted].
        /// </summary>
        /// <value><c>true</c> if [clear selection when aborted]; otherwise, <c>false</c>.</value>
        public bool ClearSelectionWhenAborted
        {
            get { return ( bool )GetValue ( ClearSelectionWhenAbortedProperty ); }
            set { SetValue ( ClearSelectionWhenAbortedProperty, value ); }
        }

        /// <summary>
        /// Gets the close advanced command.
        /// </summary>
        public ICommand CloseAdvancedCommand
        {
            get { return ( ICommand )GetValue ( CloseAdvancedCommandProperty ); }
            private set { SetValue ( CloseAdvancedCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [close on lost focus].
        /// </summary>
        /// <value><c>true</c> if [close on lost focus]; otherwise, <c>false</c>.</value>
        public bool CloseOnLostFocus
        {
            get { return ( bool )GetValue ( CloseOnLostFocusProperty ); }
            set { SetValue ( CloseOnLostFocusProperty, value ); }
        }

        /// <summary>
        /// Gets the close popup command.
        /// </summary>
        public ICommand ClosePopupCommand
        {
            get { return ( ICommand )GetValue ( ClosePopupCommandProperty ); }
            private set { SetValue ( ClosePopupCommandProperty, value ); }
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
        /// Gets the create new command.
        /// </summary>
        public ICommand CreateNewCommand
        {
            get { return ( ICommand )GetValue ( CreateNewCommandProperty ); }
            private set { SetValue ( CreateNewCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the new content of the create.
        /// </summary>
        /// <value>The new content of the create.</value>
        public FrameworkElement CreateNewContent
        {
            get { return ( FrameworkElement )GetValue ( CreateNewContentProperty ); }
            set { SetValue ( CreateNewContentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the create new patient stack panel visibility.
        /// </summary>
        /// <value>The create new patient stack panel visibility.</value>
        public Visibility CreateNewPatientStackPanelVisibility
        {
            get { return ( Visibility )GetValue ( CreateNewPatientStackPanelVisibilityProperty ); }
            set { SetValue ( CreateNewPatientStackPanelVisibilityProperty, value ); }
        }

        /// <summary>
        /// Gets the detail level changed command.
        /// </summary>
        public ICommand DetailLevelChangedCommand { get; private set; }

        /// <summary>
        /// Takes a parameter of type DragDropEventArgs
        /// </summary>
        public ICommand DragCompletedCommand
        {
            get { return ( ICommand )GetValue ( DragCompletedCommandProperty ); }
            private set { SetValue ( DragCompletedCommandProperty, value ); }
        }

        /// <summary>
        /// Takes a parameter of type DragDropQueryEventArgs
        /// </summary>
        /// <value>The drag starting command.</value>
        public ICommand DragStartingCommand
        {
            get { return ( ICommand )GetValue ( DragStartingCommandProperty ); }
            set { SetValue ( DragStartingCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the empty text.
        /// </summary>
        /// <value>The empty text.</value>
        public string EmptyText
        {
            get { return ( string )GetValue ( EmptyTextProperty ); }
            set { SetValue ( EmptyTextProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the filter template.
        /// </summary>
        /// <value>The filter template.</value>
        public DataTemplate FilterTemplate
        {
            get { return ( DataTemplate )GetValue ( FilterTemplateProperty ); }
            set { SetValue ( FilterTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        /// <value>The filters.</value>
        public IEnumerable Filters
        {
            get { return ( IEnumerable )GetValue ( FiltersProperty ); }
            set { SetValue ( FiltersProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pop up open.
        /// </summary>
        /// <value><c>true</c> if this instance is pop up open; otherwise, <c>false</c>.</value>
        public bool IsPopUpOpen
        {
            get { return ( bool )GetValue ( IsPopUpOpenProperty ); }
            set { SetValue ( IsPopUpOpenProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the manual search command.
        /// </summary>
        /// <value>The manual search command.</value>
        public ICommand ManualSearchCommand
        {
            get { return ( ICommand )GetValue ( ManualSearchCommandProperty ); }
            set { SetValue ( ManualSearchCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the height of the max drop down.
        /// </summary>
        /// <value>The height of the max drop down.</value>
        public double MaxDropDownHeight
        {
            get { return ( double )GetValue ( MaxDropDownHeightProperty ); }
            set { SetValue ( MaxDropDownHeightProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the minimum length of the quick search criteria.
        /// </summary>
        /// <value>The minimum length of the quick search criteria.</value>
        public int MinimumQuickSearchCriteriaLength
        {
            get { return ( int )GetValue ( MinimumQuickSearchCriteriaLengthProperty ); }
            set { SetValue ( MinimumQuickSearchCriteriaLengthProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex
        {
            get { return ( int )GetValue ( PageIndexProperty ); }
            set { SetValue ( PageIndexProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get { return ( int )GetValue ( PageSizeProperty ); }
            set { SetValue ( PageSizeProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the popup alignment.
        /// </summary>
        /// <value>The popup alignment.</value>
        public HorizontalAlignment PopupAlignment
        {
            get { return ( HorizontalAlignment )GetValue ( PopupAlignmentProperty ); }
            set { SetValue ( PopupAlignmentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the quick search text.
        /// </summary>
        /// <value>The quick search text.</value>
        public string QuickSearchText
        {
            get { return ( string )GetValue ( QuickSearchTextProperty ); }
            set { SetValue ( QuickSearchTextProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the result level1 detail template.
        /// </summary>
        /// <value>The result level1 detail template.</value>
        public DataTemplate ResultLevel1DetailTemplate
        {
            get { return ( DataTemplate )GetValue ( ResultLevel1DetailTemplateProperty ); }
            set { SetValue ( ResultLevel1DetailTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the result level2 detail template.
        /// </summary>
        /// <value>The result level2 detail template.</value>
        public DataTemplate ResultLevel2DetailTemplate
        {
            get { return ( DataTemplate )GetValue ( ResultLevel2DetailTemplateProperty ); }
            set { SetValue ( ResultLevel2DetailTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the result level3 detail template.
        /// </summary>
        /// <value>The result level3 detail template.</value>
        public DataTemplate ResultLevel3DetailTemplate
        {
            get { return ( DataTemplate )GetValue ( ResultLevel3DetailTemplateProperty ); }
            set { SetValue ( ResultLevel3DetailTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets the result templates.
        /// </summary>
        public ObservableCollection<DataTemplate> ResultTemplates
        {
            get { return ( ObservableCollection<DataTemplate> )GetValue ( ResultTemplatesProperty ); }
            private set { SetValue ( ResultTemplatesProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public PagedCollectionView Results
        {
            get { return ( PagedCollectionView )GetValue ( ResultsProperty ); }
            set { SetValue ( ResultsProperty, value ); }
        }

        /// <summary>
        /// Must be a RadGridView Row Style
        /// </summary>
        /// <value>The row style.</value>
        public Style RowStyle
        {
            get { return ( Style )GetValue ( RowStyleProperty ); }
            set { SetValue ( RowStyleProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selected filter.
        /// </summary>
        /// <value>The selected filter.</value>
        public object SelectedFilter
        {
            get { return GetValue ( SelectedFilterProperty ); }
            set { SetValue ( SelectedFilterProperty, value ); }
        }

        /// <summary>
        /// Gets the selected result.
        /// </summary>
        public object SelectedResult
        {
            get { return GetValue ( SelectedResultProperty ); }
            private set { SetValue ( SelectedResultProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selection changed command.
        /// </summary>
        /// <value>The selection changed command.</value>
        public ICommand SelectionChangedCommand
        {
            get { return ( ICommand )GetValue ( SelectionChangedCommandProperty ); }
            set { SetValue ( SelectionChangedCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show details button].
        /// </summary>
        /// <value><c>true</c> if [show details button]; otherwise, <c>false</c>.</value>
        public bool ShowDetailsButton
        {
            get { return ( bool )GetValue ( ShowDetailsButtonProperty ); }
            set { SetValue ( ShowDetailsButtonProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status
        {
            get { return ( string )GetValue ( StatusProperty ); }
            set { SetValue ( StatusProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        public int TotalItemCount
        {
            get { return ( int )GetValue ( TotalItemCountProperty ); }
            set { SetValue ( TotalItemCountProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use manual search].
        /// </summary>
        /// <value><c>true</c> if [use manual search]; otherwise, <c>false</c>.</value>
        public bool UseManualSearch
        {
            get { return ( bool )GetValue ( UseManualSearchProperty ); }
            set { SetValue ( UseManualSearchProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            _resultsGrid = GetTemplateChild ( "PART_ResultsGrid" ) as RadGridView;
            _popup = GetTemplateChild ( "PART_Popup" ) as Popup;
            _searchTextBox = GetTemplateChild ( "PART_SearchTextBox" ) as TextBox;
            _popupTarget = GetTemplateChild ( "PART_PopupTarget" ) as Grid;
            _dropDownButton = GetTemplateChild ( "Part_DropDown" ) as RadToggleButton;
            _resultsGrid.SelectionChanged += ResultsGridSelectionChanged;
            _searchTextBox.GotFocus += SearchTextBox_GotFocus;
            _searchTextBox.LostFocus += SearchTextBox_LostFocus;
            _searchTextBox.KeyDown += SearchTextBox_KeyDown;
            _popup.Opened += Popup_Opened;
            RadDragAndDropManager.AddDragQueryHandler ( _resultsGrid, OnDragQuery );
            UpdatePopupAlignment ();
            UpdateRowStyle ();

            if ( !string.IsNullOrEmpty ( QuickSearchText ) )
            {
                VisualStateManager.GoToState ( this, "WatermarkInvisible", true );
            }
            VisualStateManager.GoToState ( this, "KeywordSearch", true );
            BindingValidationError += ( s, e ) => UpdateValidationState ();
            UpdateValidationState ();
        }

        #endregion

        #region Methods

        private static void AdvancedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sbox = d as SearchBox;
            sbox.UpdatePopupWidth ();
        }

        private static void AllowDragChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sbox = d as SearchBox;
            if ( sbox.AllowDrag )
            {
                var dataGridCellStyle = new Style ( typeof( GridViewCell ) );
                dataGridCellStyle.Setters.Add ( new Setter { Property = RadDragAndDropManager.AllowDragProperty, Value = true } );
                sbox.Resources.Add ( typeof( GridViewCell ), dataGridCellStyle );
            }
            else
            {
                sbox.Resources.Remove ( typeof( GridViewCell ) );
            }
        }

        private static void CreateNewContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //TODO:
            var sbox = d as SearchBox;
            sbox.UpdatePopupWidth ();
        }

        private static void IsPopUpOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sbox = d as SearchBox;
            if ( !sbox.IsPopUpOpen )
            {
                sbox.SetValue ( StatusProperty, null );
                if ( !sbox._textBoxHasFocus )
                {
                    if ( sbox.SearchAborted != null && !sbox._selectionMade )
                    {
                        sbox.SearchAborted ( sbox, new EventArgs () );
                    }
                    sbox.RefreshQuickSearchTextBinding ();
                }

                sbox.QuickSearchText = string.Empty;
            }
            else
            {
                sbox._selectionMade = false;
            }
        }

        private static void PopupAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sbox = d as SearchBox;
            sbox.UpdatePopupAlignment ();
        }

        private static void QuickSearchTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sbox = d as SearchBox;
            if ( !sbox.UseManualSearch )
            {
                if ( sbox.QuickSearchText.Trim ().Length >= sbox.MinimumQuickSearchCriteriaLength && !sbox.IsPopUpOpen && sbox._textBoxHasFocus )
                {
                    sbox.IsPopUpOpen = true;
                }
                else if ( sbox.QuickSearchText.Trim ().Length < sbox.MinimumQuickSearchCriteriaLength && sbox.IsPopUpOpen &&
                          !string.IsNullOrEmpty ( sbox.QuickSearchText ) )
                {
                    sbox.IsPopUpOpen = false;
                }

                if ( sbox.QuickSearchText.Trim ().Length < sbox.MinimumQuickSearchCriteriaLength && sbox.ClearSelectionWhenAborted
                     && sbox.Results != null )
                {
                    sbox.Results = null;
                }
            }

            if ( string.IsNullOrEmpty ( sbox.QuickSearchText ) && !sbox._textBoxHasFocus )
            {
                VisualStateManager.GoToState ( sbox, "WatermarkVisible", true );
            }
            else
            {
                VisualStateManager.GoToState ( sbox, "WatermarkInvisible", true );
            }
        }

        private static void ResultsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sbox = d as SearchBox;
            sbox._refreshed = true;
        }

        private static void RowStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sbox = d as SearchBox;
            if ( sbox != null )
            {
                sbox.UpdateRowStyle ();
            }
        }

        private static void SelectedResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sbox = d as SearchBox;

            if ( sbox._resultsGrid != null && sbox._resultsGrid.SelectedItem == sbox.SelectedResult )
            {
                sbox._selectionMade = true;
                sbox._textBoxHasFocus = false;

                if ( sbox.SelectionChanged != null )
                {
                    sbox.SelectionChanged ( sbox, new SelectionChangedEventArgs ( new List<object> { e.NewValue }, new List<object> { e.OldValue } ) );
                }
                if ( sbox.SelectionChangedCommand != null )
                {
                    sbox.SelectionChangedCommand.Execute ( e.NewValue );
                }
            }

            if ( !sbox.AllowDrag && sbox.SelectedResult != null )
            {
                sbox.SetValue ( IsPopUpOpenProperty, false );
            }

            if ( sbox._resultsGrid != null )
            {
                sbox._resultsGrid.SelectedItem = sbox.SelectedResult;
            }

            if ( sbox.SelectedResult == null )
            {
                sbox.QuickSearchText = string.Empty;
            }
            else
            {
                sbox.RefreshQuickSearchTextBinding ();
            }
        }

        private void ExecuteCloseAdvancedCommand()
        {
            VisualStateManager.GoToState ( this, "KeywordSearch", true );
        }

        private void ExecuteCloseCommand()
        {
            SelectedResult = null;
            IsPopUpOpen = false;
            VisualStateManager.GoToState ( this, "KeywordSearch", true );
        }

        private void ExecuteCreateNewCommand()
        {
            VisualStateManager.GoToState ( this, "CreateNew", true );
        }

        private void ExecuteDetailLevelChanged(DataTemplate template)
        {
            _resultsGrid.RowDetailsTemplate = template;
            if ( Results != null )
            {
                Results.Refresh ();
            }
        }

        private void OnDragQuery(object sender, DragDropQueryEventArgs e)
        {
            if ( e.Options.Status == DragStatus.DragQuery )
            {
                e.Options.Payload = _resultsGrid.SelectedItem;
                if ( DragStartingCommand != null )
                {
                    DragStartingCommand.Execute ( e );
                }
                var cue = new Border ();
                cue.Style = Application.Current.Resources["DragBorderStyle"] as Style;
                var content = new ContentControl ();
                content.ContentTemplate = ResultTemplates.Count > 0 ? ResultTemplates[0] : null;
                content.Content = _resultsGrid.SelectedItem;
                cue.Child = content;
                e.Options.DragCue = cue;
                RadDragAndDropManager.DragCueOffset = GetEffectiveDragOffset ( e.Options );
                QuickSearchText = string.Empty;
                ExecuteCloseCommand ();
                SelectedResult = _resultsGrid.SelectedItem;
            }
            e.QueryResult = true;
        }

        private Point GetEffectiveDragOffset(DragDropOptions dragDropOptions)
        {
            var resultPoint = new Point();
            var dragCueElement = (FrameworkElement)dragDropOptions.DragCue;
            dragCueElement.Measure ( new Size(double.MaxValue, double.MaxValue) );
            var transformation = Application.Current.RootVisual.TransformToVisual(dragDropOptions.Source);

            resultPoint = transformation.Transform(dragDropOptions.CurrentDragPoint);

            return new Point(resultPoint.X, resultPoint.Y);
        }

        private void Popup_Opened(object sender, RoutedEventArgs e)
        {
            if ( _popup.Equals ( ( e as RadRoutedEventArgs ).OriginalSource ) )
            {
                if ( _popup.Placement == PlacementMode.Center )
                {
                    _popup.RealPopup.VerticalOffset += ( _popup.RealPopup.Child as FrameworkElement ).ActualHeight / 2.0;
                }
                var objGeneralTransform = _popup.TransformToVisual ( Application.Current.RootVisual );
                var point = objGeneralTransform.Transform ( new Point ( 0, 0 ) );
                MaxDropDownHeight = ( Application.Current.RootVisual as FrameworkElement ).ActualHeight - point.Y;
                UpdatePopupWidth ();
            }
        }

        private void RefreshQuickSearchTextBinding()
        {
            var binding = GetBindingExpression ( QuickSearchTextProperty );
            if ( binding != null )
            {
                var bind = binding.ParentBinding;
                SetBinding ( QuickSearchTextProperty, bind );
            }
        }

        private void ResultsGridSelectionChanged(object sender, EventArgs args)
        {
            var e = args as SelectionChangeEventArgs;
            if ( _refreshed )
            {
                _refreshed = false;
                _resultsGrid.SelectedItem = null;
            }
            else if ( e.AddedItems.Count > 0 && e.AddedItems[0] != null )
            {
                SelectedResult = e.AddedItems[0];
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState ( this, "WatermarkInvisible", true );
            _textBoxHasFocus = true;
            VisualStateManager.GoToState ( this, "KeywordSearch", true );
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Enter && UseManualSearch && ManualSearchCommand != null && ManualSearchCommand.CanExecute ( null ) )
            {
                if ( !IsPopUpOpen )
                {
                    IsPopUpOpen = true;
                }
                ManualSearchCommand.Execute ( null );
            }
            else if (e.Key == Key.Enter && Results != null && Results.Count == 1 && SingleResultEnterCommand != null && SingleResultEnterCommand.CanExecute(Results[0]))
            {
                SingleResultEnterCommand.Execute(Results[0]);
                if(ClosePopupCommand.CanExecute ( null ))
                {
                    ClosePopupCommand.Execute ( null );
                }
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _textBoxHasFocus = false;
            if ( string.IsNullOrEmpty ( _searchTextBox.Text ) )
            {
                VisualStateManager.GoToState ( this, "WatermarkVisible", true );
            }
            else if ( string.IsNullOrEmpty ( QuickSearchText ) || QuickSearchText.Trim ().Length < MinimumQuickSearchCriteriaLength )
            {
                if ( ClearSelectionWhenAborted )
                {
                    SelectedResult = null;
                }
                else if ( SearchAborted != null && !IsPopUpOpen )
                {
                    SearchAborted ( this, new EventArgs () );
                    RefreshQuickSearchTextBinding ();
                }
            }
        }

        private void UpdatePopupAlignment()
        {
            if ( _popupTarget != null && _popup != null )
            {
                switch ( PopupAlignment )
                {
                    case HorizontalAlignment.Left:
                        _popupTarget.HorizontalAlignment = HorizontalAlignment.Left;
                        _popup.Placement = PlacementMode.Left;
                        break;
                    case HorizontalAlignment.Right:
                        _popupTarget.HorizontalAlignment = HorizontalAlignment.Right;
                        _popup.Placement = PlacementMode.Right;
                        break;
                    case HorizontalAlignment.Center:
                    case HorizontalAlignment.Stretch:
                        _popupTarget.HorizontalAlignment = HorizontalAlignment.Center;
                        _popup.Placement = PlacementMode.Center;
                        break;
                }
            }
        }

        private void UpdatePopupWidth()
        {
            if ( _popup != null && AdvancedContent != null )
            {
                if ( AdvancedContent.ActualWidth == 0 )
                {
                    AdvancedContent.Measure ( new Size ( double.MaxValue, double.MaxValue ) );
                    ( _popup.RealPopup.Child as FrameworkElement ).Width = AdvancedContent.DesiredSize.Width;
                }
                else
                {
                    ( _popup.RealPopup.Child as FrameworkElement ).Width = AdvancedContent.ActualWidth;
                }
            }
        }

        private void UpdateRowStyle()
        {
            if ( _resultsGrid != null )
            {
                _resultsGrid.RowStyle = RowStyle;
            }
        }

        private void UpdateValidationState()
        {
            VisualStateManager.GoToState ( this, Validation.GetErrors ( this ).Any () ? "InvalidUnfocused" : "Valid", true );
        }

        #endregion
    }
}
