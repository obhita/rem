using System;
using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing RAD grid view beginning edit.
    /// </summary>
    public class RadGridViewBeginningEditBehavior : Behavior<RadGridView>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CanCancelProperty Property.
        /// </summary>
        public static readonly DependencyProperty CanCancelProperty = DependencyProperty.Register(
            "CanCancel", typeof(Func<bool>), typeof(RadGridViewBeginningEditBehavior), new PropertyMetadata(null, null));

        #endregion        

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance can cancel.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can cancel; otherwise, <c>false</c>.
        /// </value>
        public Func<bool> CanCancel
        {
            get { return GetValue(CanCancelProperty) as Func<bool>; }

            set { SetValue(CanCancelProperty, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.BeginningEdit += AssociatedObjectOnBeginningEdit;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.BeginningEdit -= AssociatedObjectOnBeginningEdit;
        }

        private void AssociatedObjectOnBeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            e.Cancel = false;
            if (CanCancel != null)
            {
                e.Cancel = CanCancel ();
            }
        }

        #endregion
    }
}