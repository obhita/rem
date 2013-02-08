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

using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// QuestionItem class.
    /// </summary>
    public class QuestionItem : Control
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ItemTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register (
                "ItemTemplate",
                typeof( DataTemplate ),
                typeof( QuestionItem ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ItemsSourceProperty Property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register (
                "ItemsSource",
                typeof( IEnumerable ),
                typeof( QuestionItem ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for QuestionProperty Property.
        /// </summary>
        public static readonly DependencyProperty QuestionProperty =
            DependencyProperty.Register (
                "Question",
                typeof( object ),
                typeof( QuestionItem ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for QuestionTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty QuestionTemplateProperty =
            DependencyProperty.Register (
                "QuestionTemplate",
                typeof( DataTemplate ),
                typeof( QuestionItem ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ValueProperty Property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register (
                "Value",
                typeof( object ),
                typeof( QuestionItem ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionItem"/> class.
        /// </summary>
        public QuestionItem ()
        {
            DefaultStyleKey = typeof( QuestionItem );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate
        {
            get { return ( DataTemplate )GetValue ( ItemTemplateProperty ); }
            set { SetValue ( ItemTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable ItemsSource
        {
            get { return ( IEnumerable )GetValue ( ItemsSourceProperty ); }
            set { SetValue ( ItemsSourceProperty, value ); }
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
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return GetValue ( ValueProperty ); }
            set { SetValue ( ValueProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();

            var listBox = GetTemplateChild ( "PART_ListBox" ) as ListBox;
            var selectedIndexBinding = new Binding ();
            selectedIndexBinding.Source = this;
            selectedIndexBinding.Path = new PropertyPath ( "Value" );
            selectedIndexBinding.Converter = Application.Current.Resources["IntegerToNullableConverterInstance"] as IValueConverter;
            selectedIndexBinding.Mode = BindingMode.TwoWay;
            listBox.SetBinding ( Selector.SelectedIndexProperty, selectedIndexBinding );
        }

        #endregion
    }
}
