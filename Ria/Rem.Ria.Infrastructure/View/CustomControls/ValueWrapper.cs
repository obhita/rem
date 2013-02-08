using System.Windows;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for wrapping value.
    /// </summary>
    public class ValueWrapper : DependencyObject
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ValueProperty Property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register (
                "Value",
                typeof( object ),
                typeof( ValueWrapper ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return GetValue ( ValueProperty ); }
            set { SetValue ( ValueProperty, value ); }
        }

        #endregion
    }
}