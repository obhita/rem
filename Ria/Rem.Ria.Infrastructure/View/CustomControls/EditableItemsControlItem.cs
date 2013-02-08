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

using System.Windows;
using System.Windows.Controls;
using Rem.Ria.Infrastructure.Common.Extension;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// EditableItemsControlItem class.
    /// </summary>
    public class EditableItemsControlItem : ContentControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( EditableItemsControlItem ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for IsReadOnlyProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register (
                "IsReadOnly",
                typeof( bool ),
                typeof( EditableItemsControlItem ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for ParentProperty Property.
        /// </summary>
        public static readonly DependencyProperty ParentProperty =
            DependencyProperty.Register (
                "Parent",
                typeof( object ),
                typeof( EditableItemsControlItem ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableItemsControlItem"/> class.
        /// </summary>
        public EditableItemsControlItem ()
        {
            DefaultStyleKey = typeof( EditableItemsControlItem );
        }

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
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly
        {
            get { return ( bool )GetValue ( IsReadOnlyProperty ); }
            set { SetValue ( IsReadOnlyProperty, value ); }
        }

        /// <summary>
        /// Gets the parent object of this <see cref="T:System.Windows.FrameworkElement"/> in the object tree.
        /// </summary>
        /// <value>The parent.</value>
        /// <returns>The parent object of this object in the object tree.</returns>
        public new object Parent
        {
            get { return GetValue ( ParentProperty ); }
            set { SetValue ( ParentProperty, value ); }
        }

        #endregion
    }
}
