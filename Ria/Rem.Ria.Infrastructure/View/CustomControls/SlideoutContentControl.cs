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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Microsoft.Practices.Prism.Commands;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// SlideoutContentControl class.
    /// </summary>
    [TemplatePart ( Name = "Part_Expander", Type = typeof( Panel ) )]
    [TemplatePart ( Name = "Part_Host", Type = typeof( ContentPresenter ) )]
    [TemplatePart ( Name = "Part_ScrollViewer", Type = typeof( ScrollViewer ) )]
    public class SlideoutContentControl : ContentControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( SlideoutContentControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ExpandDirectionProperty Property.
        /// </summary>
        public static readonly DependencyProperty ExpandDirectionProperty =
            DependencyProperty.Register (
                "ExpandDirection",
                typeof( ExpandDirection ),
                typeof( SlideoutContentControl ),
                new PropertyMetadata ( ExpandDirection.Down, ExpandDirectionChanged ) );

        /// <summary>
        /// Dependency Property for HorizontalScrollBarVisibilityProperty Property.
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
            DependencyProperty.Register (
                "HorizontalScrollBarVisibility",
                typeof( ScrollBarVisibility ),
                typeof( SlideoutContentControl ),
                new PropertyMetadata ( ScrollBarVisibility.Disabled ) );

        /// <summary>
        /// Dependency Property for IsExpandedProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register (
                "IsExpanded",
                typeof( bool ),
                typeof( SlideoutContentControl ),
                new PropertyMetadata ( true, IsExpandedChanged ) );

        /// <summary>
        /// Dependency Property for MaxExpandedHeightProperty Property.
        /// </summary>
        public static readonly DependencyProperty MaxExpandedHeightProperty =
            DependencyProperty.Register (
                "MaxExpandedHeight",
                typeof( double ),
                typeof( SlideoutContentControl ),
                new PropertyMetadata ( double.MaxValue ) );

        /// <summary>
        /// Dependency Property for MaxExpandedWidthProperty Property.
        /// </summary>
        public static readonly DependencyProperty MaxExpandedWidthProperty =
            DependencyProperty.Register (
                "MaxExpandedWidth",
                typeof( double ),
                typeof( SlideoutContentControl ),
                new PropertyMetadata ( double.MaxValue ) );

        /// <summary>
        /// Dependency Property for SlideOutAnimationProperty Property.
        /// </summary>
        public static readonly DependencyProperty SlideOutAnimationProperty =
            DependencyProperty.Register (
                "SlideOutAnimation",
                typeof( DoubleAnimation ),
                typeof( SlideoutContentControl ),
                new PropertyMetadata ( SlideOutAnimationChanged ) );

        /// <summary>
        /// Dependency Property for VerticalScrollBarVisibilityProperty Property.
        /// </summary>
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty =
            DependencyProperty.Register (
                "VerticalScrollBarVisibility",
                typeof( ScrollBarVisibility ),
                typeof( SlideoutContentControl ),
                new PropertyMetadata ( ScrollBarVisibility.Disabled ) );

        private readonly object _animationSync = new object ();

        private readonly double _hideValue;
        private readonly Storyboard _storyBoard;
        private double _expandHeightValue;
        private double _expandWidthValue;
        private Panel _expander;
        private ContentPresenter _host;
        private bool _isAnimating;
        private ScrollViewer _scrollViewer;
        private EventHandler _lastFired;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideoutContentControl"/> class.
        /// </summary>
        public SlideoutContentControl ()
        {
            DefaultStyleKey = typeof( SlideoutContentControl );
            SlideOutAnimation = new DoubleAnimation
                {
                    Duration = new Duration ( TimeSpan.FromSeconds ( 0.7 ) ),
                    EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
                };
            _hideValue = 0.0;
            _storyBoard = new Storyboard ();
            _storyBoard.Duration = SlideOutAnimation.Duration;
            _storyBoard.Children.Add ( SlideOutAnimation );
            _storyBoard.Completed += StoryBoardCompleted;

            ToggleExpand = new DelegateCommand ( ExecuteToggleExpand );
            StartExpanded = false;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [collapsing].
        /// </summary>
        public event EventHandler Collapsing;

        /// <summary>
        /// Occurs when [expanded].
        /// </summary>
        public event EventHandler Expanded;

        /// <summary>
        /// Occurs when [expanding].
        /// </summary>
        public event EventHandler Expanding;

        /// <summary>
        /// Occurs when [hidden].
        /// </summary>
        public event EventHandler Hidden;

        #endregion

        #region Public Properties

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
        /// Gets or sets the expand direction.
        /// </summary>
        /// <value>The expand direction.</value>
        public ExpandDirection ExpandDirection
        {
            get { return ( ExpandDirection )GetValue ( ExpandDirectionProperty ); }
            set { SetValue ( ExpandDirectionProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the horizontal scroll bar visibility.
        /// </summary>
        /// <value>The horizontal scroll bar visibility.</value>
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return ( ScrollBarVisibility )GetValue ( HorizontalScrollBarVisibilityProperty ); }
            set { SetValue ( HorizontalScrollBarVisibilityProperty, value ); }
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
        /// Gets or sets the height of the max expanded.
        /// </summary>
        /// <value>The height of the max expanded.</value>
        public double MaxExpandedHeight
        {
            get { return ( double )GetValue ( MaxExpandedHeightProperty ); }
            set { SetValue ( MaxExpandedHeightProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the width of the max expanded.
        /// </summary>
        /// <value>The width of the max expanded.</value>
        public double MaxExpandedWidth
        {
            get { return ( double )GetValue ( MaxExpandedWidthProperty ); }
            set { SetValue ( MaxExpandedWidthProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the slide out animation.
        /// </summary>
        /// <value>The slide out animation.</value>
        public DoubleAnimation SlideOutAnimation
        {
            get { return ( DoubleAnimation )GetValue ( SlideOutAnimationProperty ); }
            set { SetValue ( SlideOutAnimationProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [start expanded].
        /// </summary>
        /// <value><c>true</c> if [start expanded]; otherwise, <c>false</c>.</value>
        public bool StartExpanded { get; set; }

        /// <summary>
        /// Gets the toggle expand.
        /// </summary>
        public ICommand ToggleExpand { get; private set; }

        /// <summary>
        /// Gets or sets the vertical scroll bar visibility.
        /// </summary>
        /// <value>The vertical scroll bar visibility.</value>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return ( ScrollBarVisibility )GetValue ( VerticalScrollBarVisibilityProperty ); }
            set { SetValue ( VerticalScrollBarVisibilityProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the toggle expand.
        /// </summary>
        public void ExecuteToggleExpand ()
        {
            IsExpanded = !IsExpanded;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();

            _expander = GetTemplateChild ( "Part_Expander" ) as Panel;
            _host = GetTemplateChild ( "Part_Host" ) as ContentPresenter;
            _scrollViewer = GetTemplateChild ( "Part_ScrollViewer" ) as ScrollViewer;

            Storyboard.SetTarget ( SlideOutAnimation, _expander );

            UpdateForExpandDirection ();

            SizeChanged += SlideoutContentControl_SizeChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when the value of the <see cref="P:System.Windows.Controls.ContentControl.Content"/> property changes.
        /// </summary>
        /// <param name="oldContent">The old value of the <see cref="P:System.Windows.Controls.ContentControl.Content"/> property.</param>
        /// <param name="newContent">The new value of the <see cref="P:System.Windows.Controls.ContentControl.Content"/> property.</param>
        protected override void OnContentChanged ( object oldContent, object newContent )
        {
            base.OnContentChanged ( oldContent, newContent );
            if ( oldContent is FrameworkElement )
            {
                ( oldContent as FrameworkElement ).SizeChanged -= ContentSizeChanged;
            }
            if ( newContent is FrameworkElement )
            {
                ( newContent as FrameworkElement ).SizeChanged += ContentSizeChanged;
            }
        }

        private static void ExpandDirectionChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var scc = d as SlideoutContentControl;
            if ( scc != null && scc._expander != null )
            {
                scc.UpdateForExpandDirection ();
            }
        }

        private static void IsExpandedChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var scc = d as SlideoutContentControl;
            if (scc != null)
            {
                lock ( scc._animationSync )
                {
                    scc.RunAnimation ();
                }
            }
        }

        private static void SlideOutAnimationChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var scc = d as SlideoutContentControl;
            if ( scc != null && scc._expander != null )
            {
                Storyboard.SetTarget ( scc.SlideOutAnimation, scc._expander );
                scc._storyBoard.Duration = scc.SlideOutAnimation.Duration;
                scc.UpdateForExpandDirection ();
                scc._storyBoard.Children.Add ( scc.SlideOutAnimation );
            }
        }

        private void ContentSizeChanged ( object sender, SizeChangedEventArgs e )
        {
            lock ( _animationSync )
            {
                var fe = ( Content as FrameworkElement );
                fe.Measure ( new Size ( ActualWidth, double.MaxValue ) );
                if ( ExpandDirection == ExpandDirection.Down || ExpandDirection == ExpandDirection.Up )
                {
                    var height = fe.DesiredSize.Height + fe.Margin.Top + fe.Margin.Bottom + Padding.Top + Padding.Bottom;
                    var oldHeight = _expandHeightValue;
                    if (height <= MaxExpandedHeight)
                    {
                        if (height < ActualHeight && StartExpanded)
                        {
                            height = ActualHeight;
                        }
                        _expandHeightValue = height;
                        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    }
                    else
                    {
                        _expandHeightValue = MaxExpandedHeight;
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    }
                    _host.Height = _expandHeightValue;

                    if (oldHeight != _expandHeightValue)
                    {
                        if ( oldHeight == 0 )
                        {
                            _expander.Height = _expandHeightValue;
                        }
                        else
                        {
                            RunAnimation();
                        }
                    }
                }
                else
                {
                    var width = fe.DesiredSize.Width + fe.Margin.Left + fe.Margin.Right + Padding.Left + Padding.Right;
                    var oldWidth = _expandWidthValue;
                    if ( width <= MaxExpandedWidth )
                    {
                        if ( width < ActualWidth && StartExpanded )
                        {
                            width = ActualWidth;
                        }
                        _expandWidthValue = width;
                    }
                    else
                    {
                        _expandWidthValue = MaxExpandedWidth;
                    }
                    _host.Width = _expandWidthValue;

                    _host.HorizontalAlignment = 
                        ExpandDirection == ExpandDirection.Left ? HorizontalAlignment.Right : HorizontalAlignment.Left;

                    if ( oldWidth != _expandWidthValue )
                    {
                        if ( oldWidth == 0 )
                        {
                            _expander.Width = _expandWidthValue;
                        }
                        else
                        {
                            RunAnimation ();
                        }
                    }
                }
            }
        }

        private bool IsInProperState ()
        {
            var ret = false;
            if ( ( ExpandDirection == ExpandDirection.Left || ExpandDirection == ExpandDirection.Right ) )
            {
                if ( !IsExpanded && _expander.Width == _hideValue )
                {
                    ret = true;
                }
                else if ( IsExpanded && _expander.Width == _expandWidthValue )
                {
                    ret = true;
                }
            }
            else
            {
                if ( !IsExpanded && _expander.Height == _hideValue )
                {
                    ret = true;
                }
                else if ( IsExpanded && _expander.Height == _expandHeightValue )
                {
                    ret = true;
                }
            }
            return ret;
        }

        private void RunAnimation ()
        {
            if ( _expander != null )
            {
                var expandValue = 0.0;
                if (ExpandDirection == ExpandDirection.Down || ExpandDirection == ExpandDirection.Up)
                {
                    expandValue = _expandHeightValue;
                }
                else
                {
                    expandValue = _expandWidthValue;
                }

                if (expandValue > _hideValue)
                {
                    _isAnimating = true;

                    if ( _storyBoard.GetCurrentState () != ClockState.Stopped )
                    {
                        _storyBoard.SkipToFill ();
                    }

                    var toValue = IsExpanded ? expandValue : _hideValue;

                    SlideOutAnimation.To = toValue;

                    if ( !IsInProperState () )
                    {
                        var eventToFire = IsExpanded ? Expanding : Collapsing;
                        if ( ExpandDirection == ExpandDirection.Left || ExpandDirection == ExpandDirection.Right )
                        {
                            eventToFire = IsExpanded ? Expanding : Collapsing;
                        }

                        if ( eventToFire != null )
                        {
                            eventToFire ( this, new EventArgs () );
                        }

                        _storyBoard.Begin ();
                    }
                }
            }
        }

        private void SlideoutContentControl_SizeChanged ( object sender, SizeChangedEventArgs e )
        {
            if ((ExpandDirection == ExpandDirection.Left || ExpandDirection == ExpandDirection.Right) && IsExpanded && !_isAnimating)
            {
                _host.Width = e.NewSize.Width;
                _host.Height = e.NewSize.Height;
            }
        }
        
        private void StoryBoardCompleted(object sender, EventArgs eventArgs)
        {
            _isAnimating = false;
            var eventToFire = _expander.Height == _hideValue ? Hidden : Expanded;
            if ( ExpandDirection == ExpandDirection.Left || ExpandDirection == ExpandDirection.Right )
            {
                eventToFire = _expander.Width == _hideValue ? Hidden : Expanded;
            }
            if ( eventToFire != _lastFired )
            {
                _lastFired = eventToFire;
                if ( eventToFire != null )
                {
                    eventToFire ( this, new EventArgs () );
                }
                if ( Content is UIElement )
                {
                    try
                    {
                        ( Content as UIElement ).UpdateLayout ();
                    }
                    catch ( Exception )
                    {
                        //TODO:Log exception
                    }
                }
            }
        }

        private void UpdateForExpandDirection ()
        {
            if ( ExpandDirection == ExpandDirection.Down || ExpandDirection == ExpandDirection.Up )
            {
                Storyboard.SetTargetProperty ( SlideOutAnimation, new PropertyPath ( "Height" ) );
                _expander.Height = 0;
                _expander.Width = double.NaN;
            }
            else
            {
                Storyboard.SetTargetProperty ( SlideOutAnimation, new PropertyPath ( "Width" ) );
                _expander.Height = double.NaN;
                _expander.Width = 0;
            }

            switch ( ExpandDirection )
            {
                case ExpandDirection.Up:
                    _host.VerticalAlignment = VerticalAlignment.Top;
                    _host.HorizontalAlignment = HorizontalAlignment.Stretch;
                    _scrollViewer.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case ExpandDirection.Down:
                    _host.VerticalAlignment = VerticalAlignment.Bottom;
                    _host.HorizontalAlignment = HorizontalAlignment.Stretch;
                    _scrollViewer.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case ExpandDirection.Left:
                    _host.VerticalAlignment = VerticalAlignment.Stretch;
                    _host.HorizontalAlignment = HorizontalAlignment.Stretch;
                    _scrollViewer.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case ExpandDirection.Right:
                    _host.VerticalAlignment = VerticalAlignment.Stretch;
                    _host.HorizontalAlignment = HorizontalAlignment.Stretch;
                    _scrollViewer.VerticalAlignment = VerticalAlignment.Top;
                    break;
            }
        }

        #endregion
    }
}
