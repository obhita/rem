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
using System.Windows.Data;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// BasicScreenQuestionControl class.
    /// </summary>
    public class BasicScreenQuestionControl : ValidationControl, IReadOnly
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( new CornerRadius ( 0, 0, 0, 0 ) ) );

        /// <summary>
        /// Dependency Property for HintProperty Property.
        /// </summary>
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register (
                "Hint",
                typeof( string ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( string.Empty ) );

        /// <summary>
        /// Dependency Property for IsReadOnlyProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register (
                "IsReadOnly",
                typeof( bool ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( true, IsReadOnlyChanged ) );

        /// <summary>
        /// Dependency Property for QuestionProperty Property.
        /// </summary>
        public static readonly DependencyProperty QuestionProperty =
            DependencyProperty.Register (
                "Question",
                typeof( object ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for QuestionTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty QuestionTemplateProperty =
            DependencyProperty.Register (
                "QuestionTemplate",
                typeof( DataTemplate ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ReadOnlyValueTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ReadOnlyValueTemplateProperty =
            DependencyProperty.Register (
                "ReadOnlyValueTemplate",
                typeof( DataTemplate ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ValueProperty Property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register (
                "Value",
                typeof( object ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ValueTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ValueTemplateProperty =
            DependencyProperty.Register (
                "ValueTemplate",
                typeof( DataTemplate ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ValueWrapperProperty Property.
        /// </summary>
        public static readonly DependencyProperty ValueWrapperProperty =
            DependencyProperty.Register (
                "ValueWrapper",
                typeof( ValueWrapper ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for LookupValueOverrideProperty Property.
        /// </summary>
        public static readonly DependencyProperty LookupValueOverrideProperty =
            DependencyProperty.Register (
                "LookupValueOverride",
                typeof( string ),
                typeof( BasicScreenQuestionControl ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicScreenQuestionControl"/> class.
        /// </summary>
        public BasicScreenQuestionControl ()
        {
            DefaultStyleKey = typeof( BasicScreenQuestionControl );

            ValueWrapper = new ValueWrapper ();
            var valueBinding = new Binding ();
            valueBinding.Source = this;
            valueBinding.Path = new PropertyPath ( "Value" );
            valueBinding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding ( ValueWrapper, ValueWrapper.ValueProperty, valueBinding );
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
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public CornerRadius CornerRadius
        {
            get { return ( CornerRadius )GetValue ( CornerRadiusProperty ); }
            set { SetValue ( CornerRadiusProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the hint.
        /// </summary>
        /// <value>The hint for the question.</value>
        public string Hint
        {
            get { return ( string )GetValue ( HintProperty ); }
            set { SetValue ( HintProperty, value ); }
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
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return GetValue ( ValueProperty ); }
            set { SetValue ( ValueProperty, value ); }
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
        /// Gets or sets the value wrapper.
        /// </summary>
        /// <value>The value wrapper.</value>
        public ValueWrapper ValueWrapper
        {
            get { return ( ValueWrapper )GetValue ( ValueWrapperProperty ); }
            set { SetValue ( ValueWrapperProperty, value ); }
        }

        #endregion

        #region Methods

        private static void IsReadOnlyChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var basicScreenQuestionControl = d as BasicScreenQuestionControl;
            if ( basicScreenQuestionControl != null )
            {
                basicScreenQuestionControl.LayoutUpdated += basicScreenQuestionControl.ReadOnlyLayoutUpdated;
            }
        }

        private void ReadOnlyLayoutUpdated ( object sender, EventArgs e )
        {
            LayoutUpdated -= ReadOnlyLayoutUpdated;
            UpdateLayout ();
        }

        #endregion
    }
}
