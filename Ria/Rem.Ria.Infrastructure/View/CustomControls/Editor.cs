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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for editing.
    /// </summary>
    [TemplatePart ( Name = "PART_ItemsHost", Type = typeof( ItemsControl ) )]
    [TemplatePart ( Name = "PART_MaximizeGrid", Type = typeof( Grid ) )]
    [TemplatePart ( Name = "PART_RootGrid", Type = typeof( Grid ) )]
    public class Editor : Control
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AccessControlManagerProperty Property.
        /// </summary>
        public static readonly DependencyProperty AccessControlManagerProperty =
            DependencyProperty.Register (
                "AccessControlManager",
                typeof( IAccessControlManager ),
                typeof( Editor ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CanMaximizeProperty Property.
        /// </summary>
        public static readonly DependencyProperty CanMaximizeProperty =
            DependencyProperty.Register (
                "CanMaximize",
                typeof( bool ),
                typeof( Editor ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for CancelCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register (
                "CancelCommand",
                typeof( ICommand ),
                typeof( Editor ),
                new PropertyMetadata ( null, EditCommandChanged ) );

        /// <summary>
        /// Dependency Property for ExpandersProperty Property.
        /// </summary>
        public static readonly DependencyProperty ExpandersProperty =
            DependencyProperty.Register (
                "Expanders",
                typeof( ObservableCollection<EditableExpander> ),
                typeof( Editor ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for FreezeFocusedProperty Property.
        /// </summary>
        public static readonly DependencyProperty FreezeFocusedProperty =
            DependencyProperty.Register (
                "FreezeFocused",
                typeof( bool ),
                typeof( Editor ),
                new PropertyMetadata ( false, FreezeFocusedChanged ) );

        /// <summary>
        /// Dependency Property for HeaderProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register (
                "Header",
                typeof( string ),
                typeof( Editor ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for HeaderTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register (
                "HeaderTemplate",
                typeof( DataTemplate ),
                typeof( Editor ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for MaximizeHeaderProperty Property.
        /// </summary>
        public static readonly DependencyProperty MaximizeHeaderProperty =
            DependencyProperty.Register (
                "MaximizeHeader",
                typeof( string ),
                typeof( Editor ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SaveCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register (
                "SaveCommand",
                typeof( ICommand ),
                typeof( Editor ),
                new PropertyMetadata ( null, EditCommandChanged ) );

        /// <summary>
        /// Dependency Property for StartOpenProperty Property.
        /// </summary>
        public static readonly DependencyProperty StartOpenProperty =
            DependencyProperty.Register (
                "StartOpen",
                typeof( bool ),
                typeof( Editor ),
                new PropertyMetadata ( false ) );

        private readonly PopupWindow _popupWindow;
        private readonly object _setCommandsSync = new object ();
        private Grid _maximizeGrid;
        private ItemsControl _partItemsHost;
        private Grid _rootGrid;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Editor"/> class.
        /// </summary>
        public Editor ()
        {
            DefaultStyleKey = typeof( Editor );

            Expanders = new ObservableCollection<EditableExpander> ();
            Expanders.CollectionChanged += Expanders_CollectionChanged;

            ExpandAllCommand = new DelegateCommand ( ExecuteExpandAllCommand );
            CollapseAllCommand = new DelegateCommand ( ExecuteCollapseAllCommand );
            MaximizeCommand = new DelegateCommand ( ExecuteMaximizeCommand );
            
                _popupWindow = new PopupWindow();
                _popupWindow.Closed += PopupClosed;
            
            Loaded += Editor_Loaded;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the access control manager.
        /// </summary>
        /// <value>The access control manager.</value>
        public IAccessControlManager AccessControlManager
        {
            get { return ( IAccessControlManager )GetValue ( AccessControlManagerProperty ); }
            set { SetValue ( AccessControlManagerProperty, value ); }
        }

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
        /// Gets the collapse all command.
        /// </summary>
        public ICommand CollapseAllCommand { get; private set; }

        /// <summary>
        /// Gets the expand all command.
        /// </summary>
        public ICommand ExpandAllCommand { get; private set; }

        /// <summary>
        /// Gets or sets the expanders.
        /// </summary>
        /// <value>The expanders.</value>
        public ObservableCollection<EditableExpander> Expanders
        {
            get { return ( ObservableCollection<EditableExpander> )GetValue ( ExpandersProperty ); }
            set { SetValue ( ExpandersProperty, value ); }
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
        public string Header
        {
            get { return ( string )GetValue ( HeaderProperty ); }
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
        /// Gets the maximize command.
        /// </summary>
        public ICommand MaximizeCommand { get; private set; }

        /// <summary>
        /// Gets or sets the maximize header.
        /// </summary>
        /// <value>The maximize header.</value>
        public string MaximizeHeader
        {
            get { return ( string )GetValue ( MaximizeHeaderProperty ); }
            set { SetValue ( MaximizeHeaderProperty, value ); }
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
        /// Gets or sets a value indicating whether [start open].
        /// </summary>
        /// <value><c>true</c> if [start open]; otherwise, <c>false</c>.</value>
        public bool StartOpen
        {
            get { return ( bool )GetValue ( StartOpenProperty ); }
            set { SetValue ( StartOpenProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            _partItemsHost = GetTemplateChild ( "PART_ItemsHost" ) as ItemsControl;
            _maximizeGrid = GetTemplateChild ( "PART_MaximizeGrid" ) as Grid;
            _rootGrid = GetTemplateChild ( "PART_RootGrid" ) as Grid;
        }

        #endregion

        #region Methods

        private static void EditCommandChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var editor = d as Editor;
            if ( editor != null )
            {
                lock ( editor._setCommandsSync )
                {
                    foreach ( var expander in editor.Expanders )
                    {
                        editor.InitializeExpander ( expander );
                    }
                }
            }
        }

        private static void FreezeFocusedChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var editor = d as Editor;
            if ( editor != null )
            {
                editor.UpdateFreezeFocused ();
            }
        }

        private void EditableExpanderVisibilityChanged ( object sender, EventArgs e )
        {
            //Need to invode this on a seperate thread because the current thread is updating layout so it throws error if you do not do this
            Dispatcher.BeginInvoke ( UpdateForCanAccess );
        }

        private void Editor_Loaded ( object sender, RoutedEventArgs e )
        {
            UpdateStartOpened ();
        }

        private void ExecuteCollapseAllCommand ()
        {
            if ( Expanders != null )
            {
                foreach ( var expander in Expanders )
                {
                    expander.IsExpanded = false;
                }
            }
        }

        private void ExecuteExpandAllCommand ()
        {
            if ( Expanders != null )
            {
                foreach ( var expander in Expanders )
                {
                    expander.IsExpanded = true;
                }
            }
        }

        private void ExecuteMaximizeCommand ()
        {
            CanMaximize = false;
            _rootGrid.Children.Remove ( _maximizeGrid );
            _maximizeGrid.DataContext = _rootGrid.DataContext;
            _popupWindow.SubContent = _maximizeGrid;
            _popupWindow.IsMaximized = true;
            _popupWindow.Title = MaximizeHeader;
            _popupWindow.Show ();
        }

        private void ExecuteNextCommand ( EditableExpander editableExpander )
        {
            var index = Expanders.IndexOf ( editableExpander );
            var nextIndex = index + 1;
            if ( nextIndex == Expanders.Count )
            {
                nextIndex = 0;
            }
            EditableExpander next;
            while ( ( next = Expanders[nextIndex] ) != editableExpander )
            {
                if ( next.CanAccess )
                {
                    next.SetAsNextCommand.Execute ( null );
                    break;
                }
                nextIndex++;
                if ( nextIndex == Expanders.Count )
                {
                    nextIndex = 0;
                }
            }
        }

        private void Expanders_CollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.NewItems != null )
            {
                lock ( _setCommandsSync )
                {
                    foreach ( EditableExpander newItem in e.NewItems )
                    {
                        InitializeExpander ( newItem );
                        newItem.CanAccessChanged += EditableExpanderVisibilityChanged;
                    }
                }
            }
            if ( e.OldItems != null )
            {
                foreach ( EditableExpander item in e.OldItems )
                {
                    item.CanAccessChanged -= EditableExpanderVisibilityChanged;
                }
            }
        }

        private void InitializeExpander ( EditableExpander editableExpander )
        {
            var accessManagerBinding = new Binding ();
            accessManagerBinding.Source = this;
            accessManagerBinding.Path = new PropertyPath ( PropertyUtil.ExtractPropertyName ( () => AccessControlManager ) );
            editableExpander.SetBinding ( SecureControl.AccessControlManagerProperty, accessManagerBinding );

            if ( SaveCommand != null )
            {
                var saveBinding = new Binding ();
                saveBinding.Source = this;
                saveBinding.Path = new PropertyPath ( PropertyUtil.ExtractPropertyName ( () => SaveCommand ) );
                editableExpander.SetBinding ( EditableExpander.SaveCommandProperty, saveBinding );
            }

            if ( CancelCommand != null )
            {
                var cancelBinding = new Binding ();
                cancelBinding.Source = this;
                cancelBinding.Path = new PropertyPath ( PropertyUtil.ExtractPropertyName ( () => CancelCommand ) );
                editableExpander.SetBinding ( EditableExpander.CancelCommandProperty, cancelBinding );
            }

            if ( Expanders.Count () > 1 )
            {
                editableExpander.NextCommand = new DelegateCommand<EditableExpander> ( ExecuteNextCommand );
            }
        }

        private void PopupClosed ( object sender, EventArgs e )
        {
            CanMaximize = true;
            _popupWindow.SubContent = null;
            _rootGrid.Children.Add ( _maximizeGrid );
        }

        private void UpdateForCanAccess ()
        {
            //This forces a refresh of the UI for some reason UpdateLayout was not working.
            _partItemsHost.ItemsSource = null;
            _partItemsHost.ItemsSource = Expanders;
        }

        private void UpdateFreezeFocused ()
        {
            if ( Expanders != null )
            {
                foreach ( var expander in Expanders )
                {
                    expander.FreezeFocused = FreezeFocused;
                }
            }
        }

        private void UpdateStartOpened ()
        {
            if ( StartOpen )
            {
                ExecuteExpandAllCommand ();
                if ( Expanders != null && Expanders.Count () > 0 )
                {
                    var expander = Expanders.ElementAt ( 0 );
                    expander.SetAsNextCommand.Execute ( null );
                }
            }
            else
            {
                ExecuteCollapseAllCommand ();
                if ( Expanders != null && Expanders.Count () > 0 )
                {
                    var expander = Expanders.ElementAt ( 0 );
                    expander.IsExpanded = true;
                }
            }
        }

        #endregion
    }
}
