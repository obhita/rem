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
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using Rem.Ria.Infrastructure.Common.Extension;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing indicate add.
    /// </summary>
    public class IndicateAddBehavior : Behavior<ItemsControl>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AnimationDurationProperty Property.
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register (
                "AnimationDuration",
                typeof( int ),
                typeof( IndicateAddBehavior ),
                new PropertyMetadata ( 3 ) );

        /// <summary>
        /// Dependency Property for AnimationFadeInTimeProperty Property.
        /// </summary>
        public static readonly DependencyProperty AnimationFadeInTimeProperty =
            DependencyProperty.Register (
                "AnimationFadeInTime",
                typeof( int ),
                typeof( IndicateAddBehavior ),
                new PropertyMetadata ( 1 ) );

        /// <summary>
        /// Dependency Property for AnimationFadeOutTimeProperty Property.
        /// </summary>
        public static readonly DependencyProperty AnimationFadeOutTimeProperty =
            DependencyProperty.Register (
                "AnimationFadeOutTime",
                typeof( int ),
                typeof( IndicateAddBehavior ),
                new PropertyMetadata ( 1 ) );

        /// <summary>
        /// Dependency Property for DropShadowEffectProperty Property.
        /// </summary>
        public static readonly DependencyProperty DropShadowEffectProperty =
            DependencyProperty.Register (
                "DropShadowEffect",
                typeof( DropShadowEffect ),
                typeof( IndicateAddBehavior ),
                new PropertyMetadata ( Application.Current.Resources["IndicateAddDropShadowEffect"] as DropShadowEffect ) );

        /// <summary>
        /// Dependency Property for ScrollAnimationDurationProperty Property.
        /// </summary>
        public static readonly DependencyProperty ScrollAnimationDurationProperty =
            DependencyProperty.Register (
                "ScrollAnimationDuration",
                typeof( int ),
                typeof( IndicateAddBehavior ),
                new PropertyMetadata ( 1 ) );

        private Effect _oldEffect;
        private FrameworkElement _runningItem;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the duration of the animation.
        /// </summary>
        /// <value>The duration of the animation.</value>
        public int AnimationDuration
        {
            get { return ( int )GetValue ( AnimationDurationProperty ); }
            set { SetValue ( AnimationDurationProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the animation fade in time.
        /// </summary>
        /// <value>The animation fade in time.</value>
        public int AnimationFadeInTime
        {
            get { return ( int )GetValue ( AnimationFadeInTimeProperty ); }
            set { SetValue ( AnimationFadeInTimeProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the animation fade out time.
        /// </summary>
        /// <value>The animation fade out time.</value>
        public int AnimationFadeOutTime
        {
            get { return ( int )GetValue ( AnimationFadeOutTimeProperty ); }
            set { SetValue ( AnimationFadeOutTimeProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the drop shadow effect.
        /// </summary>
        /// <value>The drop shadow effect.</value>
        public DropShadowEffect DropShadowEffect
        {
            get { return ( DropShadowEffect )GetValue ( DropShadowEffectProperty ); }
            set { SetValue ( DropShadowEffectProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the duration of the scroll animation.
        /// </summary>
        /// <value>The duration of the scroll animation.</value>
        public int ScrollAnimationDuration
        {
            get { return ( int )GetValue ( ScrollAnimationDurationProperty ); }
            set { SetValue ( ScrollAnimationDurationProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            ( ( INotifyCollectionChanged )AssociatedObject.Items ).CollectionChanged += CollectionChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            ( ( INotifyCollectionChanged )AssociatedObject.Items ).CollectionChanged -= CollectionChanged;
        }

        private void CollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == NotifyCollectionChangedAction.Add && e.NewItems.Count == 1 )
            {
                var item = e.NewItems[0];
                var itemContainer = AssociatedObject.ItemContainerGenerator.ContainerFromItem ( item ) as FrameworkElement;
                if ( itemContainer != null )
                {
                    RoutedEventHandler eventHandler = null;
                    eventHandler = ( eventsender, args ) =>
                        {
                            itemContainer.Loaded -= eventHandler;
                            itemContainer.BringIntoView ( true );

                            _oldEffect = itemContainer.Effect;
                            itemContainer.Effect = DropShadowEffect;
                            _runningItem = itemContainer;
                            CreateStoryBoard ( itemContainer ).Begin ();
                        };
                    itemContainer.Loaded += eventHandler;
                }
            }
        }

        private Storyboard CreateScrollStoryBoard ( ItemsControl itemsControl )
        {
            var storyboard = new Storyboard ();
            var animation = new DoubleAnimationUsingKeyFrames { Duration = new Duration ( new TimeSpan ( 0, 0, AnimationDuration ) ) };
            animation.KeyFrames.Add ( new DiscreteDoubleKeyFrame { KeyTime = KeyTime.FromTimeSpan ( new TimeSpan ( 0, 0, 0 ) ), Value = 0 } );
            animation.KeyFrames.Add (
                new LinearDoubleKeyFrame { KeyTime = KeyTime.FromTimeSpan ( new TimeSpan ( 0, 0, AnimationFadeInTime ) ), Value = 1 } );
            animation.KeyFrames.Add (
                new DiscreteDoubleKeyFrame
                    {
                        KeyTime = KeyTime.FromTimeSpan ( new TimeSpan ( 0, 0, AnimationDuration - AnimationFadeOutTime ) ), Value = 1
                    } );
            animation.KeyFrames.Add (
                new LinearDoubleKeyFrame { KeyTime = KeyTime.FromTimeSpan ( new TimeSpan ( 0, 0, AnimationDuration ) ), Value = 0 } );
            storyboard.Children.Add ( animation );
            Storyboard.SetTarget ( animation, itemsControl );
            Storyboard.SetTargetProperty ( animation, new PropertyPath ( "(Effect).Opacity" ) );
            storyboard.Completed += Storyboard_Completed;
            return storyboard;
        }

        private Storyboard CreateStoryBoard ( FrameworkElement frameworkElement )
        {
            var storyboard = new Storyboard ();
            var animation = new DoubleAnimationUsingKeyFrames { Duration = new Duration ( new TimeSpan ( 0, 0, AnimationDuration ) ) };
            animation.KeyFrames.Add ( new DiscreteDoubleKeyFrame { KeyTime = KeyTime.FromTimeSpan ( new TimeSpan ( 0, 0, 0 ) ), Value = 0 } );
            animation.KeyFrames.Add (
                new LinearDoubleKeyFrame { KeyTime = KeyTime.FromTimeSpan ( new TimeSpan ( 0, 0, AnimationFadeInTime ) ), Value = 1 } );
            animation.KeyFrames.Add (
                new DiscreteDoubleKeyFrame
                    {
                        KeyTime = KeyTime.FromTimeSpan ( new TimeSpan ( 0, 0, AnimationDuration - AnimationFadeOutTime ) ), Value = 1
                    } );
            animation.KeyFrames.Add (
                new LinearDoubleKeyFrame { KeyTime = KeyTime.FromTimeSpan ( new TimeSpan ( 0, 0, AnimationDuration ) ), Value = 0 } );
            storyboard.Children.Add ( animation );
            Storyboard.SetTarget ( animation, frameworkElement );
            Storyboard.SetTargetProperty ( animation, new PropertyPath ( "(Effect).Opacity" ) );
            storyboard.Completed += Storyboard_Completed;
            return storyboard;
        }

        private void Storyboard_Completed ( object sender, EventArgs e )
        {
            _runningItem.Effect = _oldEffect;
        }

        #endregion
    }
}
