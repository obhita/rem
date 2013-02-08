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
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Utility;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.DataTransferObject;
using Telerik.Windows.Controls;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for expanding editable.
    /// </summary>
    [TemplatePart(Name = "Part_Focus", Type = typeof(Grid))]
    [TemplatePart ( Name = "Part_CancelButton", Type = typeof( Button ) )]
    [TemplatePart ( Name = "Part_SaveButton", Type = typeof( Button ) )]
    [TemplatePart ( Name = "Part_NextButton", Type = typeof( Button ) )]
    [TemplatePart ( Name = "PART_ContentPresenter", Type = typeof( ContentPresenter ) )]
    [TemplatePart ( Name = "PART_MaximizeGrid", Type = typeof( Grid ) )]
    [TemplatePart ( Name = "PART_RootGrid", Type = typeof( Grid ) )]
    [TemplateVisualState ( Name = "SuccessState", GroupName = "UpdateStateGroup" )]
    [TemplateVisualState ( Name = "ErrorState", GroupName = "UpdateStateGroup" )]
    [TemplateVisualState ( Name = "ReadyState", GroupName = "UpdateStateGroup" )]
    [TemplateVisualState ( Name = "EditState", GroupName = "EditingStateGroup" )]
    [TemplateVisualState ( Name = "NonEditState", GroupName = "EditingStateGroup" )]
    [TemplateVisualState ( Name = "HideState", GroupName = "ExpanderStateGroup" )]
    [TemplateVisualState ( Name = "RevealState", GroupName = "ExpanderStateGroup" )]
    public class EditableExpander : SecureControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CanMaximizeProperty Property.
        /// </summary>
        public static readonly DependencyProperty CanMaximizeProperty =
            DependencyProperty.Register (
                "CanMaximize",
                typeof( bool ),
                typeof( EditableExpander ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for CancelCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register (
                "CancelCommand",
                typeof( ICommand ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ContentEditTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ContentEditTemplateProperty =
            DependencyProperty.Register (
                "ContentEditTemplate",
                typeof( DataTemplate ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null, ContentEditTemplateChanged ) );

        /// <summary>
        /// Dependency Property for ContentProperty Property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register (
                "Content",
                typeof( object ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null, ContentChanged ) );

        /// <summary>
        /// Dependency Property for ContentTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register (
                "ContentTemplate",
                typeof( DataTemplate ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for EditableContentTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty EditableContentTemplateProperty =
            DependencyProperty.Register (
                "EditableContentTemplate",
                typeof( DataTemplate ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null, EditableContentTemplateChanged ) );

        /// <summary>
        /// Dependency Property for EditableWrapperProperty Property.
        /// </summary>
        public static readonly DependencyProperty EditableWrapperProperty =
            DependencyProperty.Register (
                "EditableWrapper",
                typeof( IEditableDtoWrapper ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for FreezeFocusedProperty Property.
        /// </summary>
        public static readonly DependencyProperty FreezeFocusedProperty =
            DependencyProperty.Register (
                "FreezeFocused",
                typeof( bool ),
                typeof( EditableExpander ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for HeaderProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register (
                "Header",
                typeof( object ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for HeaderTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register (
                "HeaderTemplate",
                typeof( DataTemplate ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for IsEditingProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsEditingProperty =
            DependencyProperty.Register (
                "IsEditing",
                typeof( bool ),
                typeof( EditableExpander ),
                new PropertyMetadata ( false, IsEditingChanged ) );

        /// <summary>
        /// Dependency Property for IsExpandedProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register (
                "IsExpanded",
                typeof( bool ),
                typeof( EditableExpander ),
                new PropertyMetadata ( false, IsExpandedChanged ) );

        /// <summary>
        /// Dependency Property for NextCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty NextCommandProperty =
            DependencyProperty.Register (
                "NextCommand",
                typeof( ICommand ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null, NextCommandChanged ) );

        /// <summary>
        /// Dependency Property for SaveCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register (
                "SaveCommand",
                typeof( ICommand ),
                typeof( EditableExpander ),
                new PropertyMetadata ( null, SaveCommandChanged ) );

        /// <summary>
        /// Dependency Property for ShowNextAndSaveButtonProperty Property.
        /// </summary>
        public static readonly DependencyProperty ShowNextAndSaveButtonProperty =
            DependencyProperty.Register (
                "ShowNextAndSaveButton",
                typeof( bool ),
                typeof( EditableExpander ),
                new PropertyMetadata ( true ) );

        /// <summary>
        /// Dependency Property for ShowNextProperty Property.
        /// </summary>
        public static readonly DependencyProperty ShowNextProperty =
            DependencyProperty.Register (
                "ShowNext",
                typeof( bool ),
                typeof( EditableExpander ),
                new PropertyMetadata ( false ) );

        private readonly PopupWindow _popupWindow;
        private bool _afterSaveCommandIntialized;

        private Button _btnCancel;
        private Button _btnNext;
        private Button _btnSave;
        private List<EditableItemsControl> _containedEditableItemsControls;
        private List<RadScheduleView> _containedScheduleViews;
        private ContentPresenter _contentPresenter;
        private Grid _focusElement;
        private bool _internalFreezeFocused;
        private Grid _maximizeGrid;
        private DataTemplate _nonEditTemplateCache;
        private Grid _rootGrid;
        private CompositeCommand _saveCompositeCommand;
        private bool _templateApplied;
        private bool _contentInTree;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableExpander"/> class.
        /// </summary>
        public EditableExpander ()
        {
            DefaultStyleKey = typeof( EditableExpander );
            SetAsNextCommand = new DelegateCommand ( ExecuteSetAsNextCommand );
            MaximizeCommand = new DelegateCommand ( ExecuteMaximizeCommand );
            _popupWindow = new PopupWindow ();
            _popupWindow.Closed += PopupClosed;
            _contentInTree = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance can maximize.
        /// </summary>
        /// <value><c>true</c> if this instance can maximize; otherwise, <c>false</c>.</value>
        public bool CanMaximize
        {
            get { return ( bool )GetValue ( CanMaximizeProperty ); }
            set { SetValue ( CanMaximizeProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCommand
        {
            get { return ( ICommand )GetValue ( CancelCommandProperty ); }
            set { SetValue ( CancelCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public object Content
        {
            get { return GetValue ( ContentProperty ); }
            set { SetValue ( ContentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the content edit template.
        /// </summary>
        /// <value>The content edit template.</value>
        public DataTemplate ContentEditTemplate
        {
            get { return ( DataTemplate )GetValue ( ContentEditTemplateProperty ); }
            set { SetValue ( ContentEditTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the content template.
        /// </summary>
        /// <value>The content template.</value>
        public DataTemplate ContentTemplate
        {
            get { return ( DataTemplate )GetValue ( ContentTemplateProperty ); }
            set { SetValue ( ContentTemplateProperty, value ); }
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
        /// Gets or sets the editable content template.
        /// </summary>
        /// <value>The editable content template.</value>
        public DataTemplate EditableContentTemplate
        {
            get { return ( DataTemplate )GetValue ( EditableContentTemplateProperty ); }
            set { SetValue ( EditableContentTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets the editable wrapper.
        /// </summary>
        public IEditableDtoWrapper EditableWrapper
        {
            get { return ( IEditableDtoWrapper )GetValue ( EditableWrapperProperty ); }
            private set { SetValue ( EditableWrapperProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [freeze focused].
        /// </summary>
        /// <value><c>true</c> if [freeze focused]; otherwise, <c>false</c>.</value>
        public bool FreezeFocused
        {
            get { return ( bool )GetValue ( FreezeFocusedProperty ); }
            set { SetValue ( FreezeFocusedProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public object Header
        {
            get { return GetValue ( HeaderProperty ); }
            set { SetValue ( HeaderProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
        public DataTemplate HeaderTemplate
        {
            get { return ( DataTemplate )GetValue ( HeaderTemplateProperty ); }
            set { SetValue ( HeaderTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editing.
        /// </summary>
        /// <value><c>true</c> if this instance is editing; otherwise, <c>false</c>.</value>
        public bool IsEditing
        {
            get { return ( bool )GetValue ( IsEditingProperty ); }
            set { SetValue ( IsEditingProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value><c>true</c> if this instance is expanded; otherwise, <c>false</c>.</value>
        public bool IsExpanded
        {
            get { return ( bool )GetValue ( IsExpandedProperty ); }
            set { SetValue ( IsExpandedProperty, value ); }
        }

        /// <summary>
        /// Gets the maximize command.
        /// </summary>
        public ICommand MaximizeCommand { get; private set; }

        /// <summary>
        /// If set this command will fire when next is clicked
        /// </summary>
        /// <value>The next command.</value>
        public ICommand NextCommand
        {
            get { return ( ICommand )GetValue ( NextCommandProperty ); }
            set { SetValue ( NextCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the save command.
        /// </summary>
        /// <value>The save command.</value>
        public ICommand SaveCommand
        {
            get { return ( ICommand )GetValue ( SaveCommandProperty ); }
            set { SetValue ( SaveCommandProperty, value ); }
        }

        /// <summary>
        /// Gets the set as next command.
        /// </summary>
        public ICommand SetAsNextCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show next].
        /// </summary>
        /// <value><c>true</c> if [show next]; otherwise, <c>false</c>.</value>
        public bool ShowNext
        {
            get { return ( bool )GetValue ( ShowNextProperty ); }
            set { SetValue ( ShowNextProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show next and save button].
        /// </summary>
        /// <value><c>true</c> if [show next and save button]; otherwise, <c>false</c>.</value>
        public bool ShowNextAndSaveButton
        {
            get { return ( bool )GetValue ( ShowNextAndSaveButtonProperty ); }
            set { SetValue ( ShowNextAndSaveButtonProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when [apply template].
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            _btnSave = GetTemplateChild ( "Part_SaveButton" ) as Button;
            _btnNext = GetTemplateChild ( "Part_NextButton" ) as Button;
            _btnCancel = GetTemplateChild ( "Part_CancelButton" ) as Button;
            _focusElement = GetTemplateChild ( "Part_Focus" ) as Grid;
            _contentPresenter = GetTemplateChild ( "PART_ContentPresenter" ) as ContentPresenter;
            _maximizeGrid = GetTemplateChild ( "PART_MaximizeGrid" ) as Grid;
            _rootGrid = GetTemplateChild ( "PART_RootGrid" ) as Grid;

            _saveCompositeCommand = new CompositeCommand ();
            _saveCompositeCommand.RegisterCommand ( new DelegateCommand ( ExecuteSaveCommand ) );
            if ( SaveCommand != null )
            {
                _saveCompositeCommand.RegisterCommand ( SaveCommand );
                if ( !_afterSaveCommandIntialized )
                {
                    _afterSaveCommandIntialized = true;
                    _saveCompositeCommand.RegisterCommand ( new DelegateCommand ( AfterSaveCommandExecute ) );
                }
            }
            _btnSave.Command = _saveCompositeCommand;
            var contentBinding = new Binding ();
            contentBinding.Source = this;
            contentBinding.Path = new PropertyPath ( PropertyUtil.ExtractPropertyName ( () => Content ) );
            _btnSave.SetBinding ( ButtonBase.CommandParameterProperty, contentBinding );
            _btnNext.Click += NextClicked;
            _btnCancel.Click += CancelClick;
            LostFocus += Content_LostFocus;
            _focusElement.MouseLeftButtonDown += Content_MouseLeftButtonDown;
            AddHandler ( MouseLeftButtonDownEvent, new MouseButtonEventHandler ( EditableExpander_MouseLeftButtonDown ), true );
            MouseLeftButtonDown += EditableExpander_MouseLeftButtonDown;

            if ( IsExpanded )
            {
                VisualStateManager.GoToState ( this, "RevealState", true );
            }

            if ( UsingEditableContentTemplate () )
            {
                ContentTemplate = EditableContentTemplate;
            }

            _templateApplied = true;

            UpdateContentPresenter();

            if ( IsEditing )
            {
                TurnOnEditing ();
            }
            else
            {
                TurnOffEditing ();
            }
        }

        #endregion

        #region Methods

        private static void ContentChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ee = d as EditableExpander;
            if ( ee != null )
            {
                ee.HandleContentChanged ( e.OldValue );
            }
        }

        private static void ContentEditTemplateChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ee = d as EditableExpander;
            if ( ee != null )
            {
                if ( ee.ReadLocalValue ( EditableContentTemplateProperty ) != DependencyProperty.UnsetValue && e.NewValue != null )
                {
                    throw new NotSupportedException ( "Cannot Set ContentEditTemplate if EditableContentTemplate is already set." );
                }
                ee.UnSubsribeToChildEvents ();
                ee._containedEditableItemsControls = null;
            }
        }

        private static void EditableContentTemplateChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ee = d as EditableExpander;
            if ( ee != null )
            {
                if ( ( ( ee.ReadLocalValue ( ContentTemplateProperty ) != DependencyProperty.UnsetValue
                         && ee.ReadLocalValue ( ContentTemplateProperty ) != e.OldValue )
                       || ee.ReadLocalValue ( ContentEditTemplateProperty ) != DependencyProperty.UnsetValue ) && e.NewValue != null
                     && HtmlPage.IsEnabled )
                {
                    throw new NotSupportedException (
                        "Cannot Set EditableContentTemplate if ContentTemplate or ContentEditTemplate are already set." );
                }
                ee.UnSubsribeToChildEvents ();
                ee._containedEditableItemsControls = null;
            }
        }

        private static void EditableExpander_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
        {
            e.Handled = true;
        }

        private static void IsEditingChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ee = d as EditableExpander;
            if ( ee != null )
            {
                if ( ee.IsEditing )
                {
                    ee.TurnOnEditing ();
                }
                else
                {
                    ee.TurnOffEditing ();
                }
            }
        }

        private static void IsExpandedChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ee = d as EditableExpander;
            if ( ee != null )
            {
                ee.HandleIsExpandedChanged ();
            }
        }

        private static void NextCommandChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ee = d as EditableExpander;
            if ( ee != null )
            {
                ee.ShowNext = ee.NextCommand != null;
            }
        }

        private static void SaveCommandChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ee = d as EditableExpander;
            if ( ee != null && ee._saveCompositeCommand != null )
            {
                var oldValue = e.OldValue as ICommand;
                if ( oldValue != null )
                {
                    ee._saveCompositeCommand.UnregisterCommand ( oldValue );
                }
                var newValue = e.NewValue as ICommand;
                if ( newValue != null )
                {
                    ee._saveCompositeCommand.RegisterCommand ( newValue );
                    if ( !ee._afterSaveCommandIntialized )
                    {
                        ee._afterSaveCommandIntialized = true;
                        ee._saveCompositeCommand.RegisterCommand ( new DelegateCommand ( ee.AfterSaveCommandExecute ) );
                    }
                }
            }
        }

        private void AddingItemIsDirtyHandler ( object sender, EventArgs e )
        {
            if ( EditableWrapper != null )
            {
                EditableWrapper.AddingItem ();
            }
        }

        private void AfterSaveCommandExecute ()
        {
            var setLoad = true;

            if ( SaveCommand is IExecutionManager )
            {
                setLoad = ( SaveCommand as IExecutionManager ).Executed;
            }
        }

        private void CancelClick ( object sender, RoutedEventArgs e )
        {
            VisualStateManager.GoToState ( this, "ReadyState", true );

            foreach ( var editableItemsControl in _containedEditableItemsControls )
            {
                editableItemsControl.AddingItem = Activator.CreateInstance ( editableItemsControl.AddingItem.GetType () );
            }

            if ( CancelCommand != null && CancelCommand.CanExecute ( Content ) )
            {
                CancelCommand.Execute ( Content );
            }
        }

        private void Content_LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( !IsKeyboardFocusWithin () && ( _nonEditTemplateCache != null || UsingEditableContentTemplate () ) && !FreezeFocused
                 && !_internalFreezeFocused )
            {
                if ( !TryTurnOffEditing () )
                {
                    SetFocusWithin ();
                }
            }
        }

        private void Content_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
        {
            if ( ( ContentEditTemplate != null || EditableContentTemplate != null ) && ContentTemplate != ContentEditTemplate && !FreezeFocused
                 && !_internalFreezeFocused )
            {
                IsEditing = true;
            }
        }

        private void EditableExpander_LayoutUpdated ( object sender, EventArgs e )
        {
            LayoutUpdated -= EditableExpander_LayoutUpdated;
            SetFocusWithin ();
            if ( _containedEditableItemsControls == null )
            {
                _containedEditableItemsControls = new List<EditableItemsControl> ();
                this.FindVisualChildren ( true, ref _containedEditableItemsControls );
            }
            if ( _containedScheduleViews == null )
            {
                _containedScheduleViews = new List<RadScheduleView> ();
                this.FindVisualChildren ( true, ref _containedScheduleViews );
            }
            if ( UsingEditableContentTemplate () )
            {
                var child = VisualTreeHelper.GetChild ( _contentPresenter, 0 ) as FrameworkElement;
                if ( child != null )
                {
                    child.IsNotReadonly ();
                }
            }
            SubsribeToChildEvents ();
            VisualStateManager.GoToState ( this, IsEditing ? "EditState" : "NonEditState", true );
        }

        private void EditableExpander_LayoutUpdatedAfterContentChange ( object sender, EventArgs e )
        {
            LayoutUpdated -= EditableExpander_LayoutUpdatedAfterContentChange;
            UnSubsribeToChildEvents ();
            _containedEditableItemsControls = new List<EditableItemsControl> ();
            this.FindVisualChildren ( true, ref _containedEditableItemsControls );
            SubsribeToChildEvents ();
        }

        private void ExecuteMaximizeCommand ()
        {
            CanMaximize = false;
            _rootGrid.Children.Remove ( _maximizeGrid );
            _maximizeGrid.DataContext = _rootGrid.DataContext;
            _popupWindow.SubContent = _maximizeGrid;
            _popupWindow.IsMaximized = true;
            _popupWindow.Show ();
        }

        private void ExecuteSaveCommand ()
        {
            VisualStateManager.GoToState ( this, "ReadyState", true );
            foreach ( var editableItemsControl in _containedEditableItemsControls )
            {
                if ( editableItemsControl.IsAddingDirty )
                {
                    editableItemsControl.AddAddingItemCommand.Execute ( null );
                }
            }

            if ( SaveCommand is IExecutionManager )
            {
                ( SaveCommand as IExecutionManager ).Executed = true;
            }
        }

        private void ExecuteSetAsNextCommand ()
        {
            IsExpanded = true;
            if ( ContentEditTemplate != null )
            {
                IsEditing = true;
            }
            else
            {
                SetFocusWithin ();
            }
        }

        private void FreezeInternalFocus ( object sender, ShowDialogEventArgs args )
        {
            if ( !args.Cancel )
            {
                _internalFreezeFocused = true;
            }
        }

        private void HandleContentChanged ( object oldValue )
        {
            var oldEditableWrapper = EditableWrapper;
            if ( IsEditing )
            {
                SetFocusWithin ();
            }
            if ( Content is IEditableDtoWrapper )
            {
                EditableWrapper = Content as IEditableDtoWrapper;
            }
            else
            {
                var abstractdto = Content as AbstractDataTransferObject;
                EditableWrapper = abstractdto != null ? new EditableDtoWrapper { EditableDto = abstractdto } : new EditableDtoWrapper ();
                EditableWrapper.IsLoading = false;
            }
            if ( EditableWrapper != null )
            {
                if ( EditableWrapper.HasErrors )
                {
                    VisualStateManager.GoToState ( this, "ErrorState", true );
                }
                else if ( oldValue != null && ( oldEditableWrapper == null || oldEditableWrapper != EditableWrapper ) )
                {
                    VisualStateManager.GoToState ( this, "SuccessState", true );
                }
            }
            if ( IsEditing )
            {
                //This handles the case where an editable items control is part of a template that will get refreshed when the content changes.
                LayoutUpdated += EditableExpander_LayoutUpdatedAfterContentChange;
            }

            if (oldEditableWrapper != null && oldEditableWrapper != EditableWrapper)
            {
                oldEditableWrapper.Dispose();
            }
        }

        private void HandleIsExpandedChanged ()
        {
            if ( _templateApplied )
            {
                if ( IsEditing && !IsExpanded && !FreezeFocused && !_internalFreezeFocused )
                {
                    IsExpanded = !TryTurnOffEditing ();
                }
                VisualStateManager.GoToState ( this, IsExpanded ? "RevealState" : "HideState", true );
                UpdateContentPresenter ();
            }
        }

        private void UpdateContentPresenter()
        {
            if(_focusElement != null)
            {
                if (!IsExpanded)
                {
                    _contentInTree = false;
                    _focusElement.Children.Remove(_contentPresenter);
                    UnSubsribeToChildEvents();
                }
                else if (!_focusElement.Children.Contains(_contentPresenter))
                {
                    _focusElement.Children.Add(_contentPresenter);
                    _contentInTree = true;
                    _contentPresenter.Loaded += EditableExpander_LayoutUpdatedAfterPutBackInTree;
                }
            }
        }

        private void EditableExpander_LayoutUpdatedAfterPutBackInTree(object sender, EventArgs e)
        {
            _contentPresenter.Loaded -= EditableExpander_LayoutUpdatedAfterPutBackInTree;
            if (UsingEditableContentTemplate())
            {
                var child = VisualTreeHelper.GetChild(_contentPresenter, 0) as FrameworkElement;
                if (child != null)
                {
                    if(IsEditing)
                    {
                        child.IsNotReadonly (); 
                    }
                    else
                    {
                        child.IsReadonly();
                    }
                }
            }
            SubsribeToChildEvents();
            VisualStateManager.GoToState(this, IsEditing ? "EditState" : "NonEditState", true);
        }

        private bool IsKeyboardFocusWithin ()
        {
            var curFocus = FocusManager.GetFocusedElement ();
            return this == curFocus || curFocus is ChildWindow || this.IsChild ( curFocus );
        }

        private void NextClicked ( object sender, RoutedEventArgs e )
        {
            if ( EditableWrapper == null || EditableWrapper.IsDirty )
            {
                _saveCompositeCommand.Execute ( Content );
                IsExpanded = false;
            }
        }

        private void PopupClosed ( object sender, EventArgs e )
        {
            CanMaximize = true;
            _popupWindow.SubContent = null;
            _rootGrid.Children.Add ( _maximizeGrid );
        }

        private void SetFocusWithin ()
        {
            var focus = _focusElement.FindVisualChild<Control> ( child => child.IsTabStop );
            if ( focus != null )
            {
                focus.Focus ();
            }
        }

        private void SubsribeToChildEvents ()
        {
            if ( _containedEditableItemsControls != null )
            {
                foreach ( var editableItemsControl in _containedEditableItemsControls )
                {
                    editableItemsControl.AddingItemIsDirty += AddingItemIsDirtyHandler;
                }
            }
            if ( _containedScheduleViews != null )
            {
                foreach ( var control in _containedScheduleViews )
                {
                    control.ShowDialog += FreezeInternalFocus;
                    control.AppointmentDeleted += UnFreezeInternalFocusOnDeleted;
                }
            }
        }

        private bool TryTurnOffEditing ()
        {
            var ret = false;
            if ( EditableWrapper != null && !EditableWrapper.IsLoading && EditableWrapper.IsDirty )
            {
                var result = MessageBox.Show ( "Would you like to save your changes?", "Pending Changes:", MessageBoxButton.OKCancel );
                if ( result == MessageBoxResult.OK )
                {
                    SaveCommand.Execute ( Content );
                    IsEditing = false;
                    ret = true;
                }
                else
                {
                    SetFocusWithin ();
                }
            }
            else
            {
                IsEditing = false;
                ret = true;
            }
            return ret;
        }

        private void TurnOffEditing ()
        {
            if ( _templateApplied )
            {
                if ( !UsingEditableContentTemplate () && _nonEditTemplateCache != null )
                {
                    ContentTemplate = _nonEditTemplateCache;
                }
                _nonEditTemplateCache = null;
                UnSubsribeToChildEvents ();
                _containedEditableItemsControls = null;
                _containedScheduleViews = null;
                VisualStateManager.GoToState ( this, IsEditing ? "EditState" : "NonEditState", true );
                LayoutUpdated += TurnOffEditing_LayoutUpdated;
            }
        }

        private void TurnOffEditing_LayoutUpdated ( object sender, EventArgs e )
        {
            LayoutUpdated -= TurnOffEditing_LayoutUpdated;
            if ( HtmlPage.IsEnabled && UsingEditableContentTemplate () && _contentInTree )
            {
                var child = VisualTreeHelper.GetChild ( _contentPresenter, 0 ) as FrameworkElement;
                if ( child != null )
                {
                    child.IsReadonly ();
                }
            }
        }

        private void TurnOnEditing ()
        {
            IsExpanded = true;
            if ( _templateApplied )
            {
                if ( !UsingEditableContentTemplate () )
                {
                    _nonEditTemplateCache = ContentTemplate;
                    ContentTemplate = ContentEditTemplate;
                }
                LayoutUpdated += EditableExpander_LayoutUpdated;
            }
        }

        private void UnFreezeInternalFocusOnDeleted ( object sender, AppointmentDeletedEventArgs args )
        {
            _internalFreezeFocused = false;
        }

        private void UnSubsribeToChildEvents ()
        {
            if ( _containedEditableItemsControls != null )
            {
                foreach ( var editableItemsControl in _containedEditableItemsControls )
                {
                    editableItemsControl.AddingItemIsDirty -= AddingItemIsDirtyHandler;
                }
            }
            if ( _containedScheduleViews != null )
            {
                foreach ( var control in _containedScheduleViews )
                {
                    control.ShowDialog -= FreezeInternalFocus;
                    control.AppointmentDeleted -= UnFreezeInternalFocusOnDeleted;
                }
            }
        }

        private bool UsingEditableContentTemplate ()
        {
            return ReadLocalValue ( EditableContentTemplateProperty ) != DependencyProperty.UnsetValue;
        }

        #endregion
    }
}
