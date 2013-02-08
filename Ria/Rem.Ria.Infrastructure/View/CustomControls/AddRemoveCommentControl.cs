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
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// AddRemoveCommentControl class.
    /// </summary>
    [TemplatePart ( Name = CommentTextBoxName, Type = typeof( RadWatermarkTextBox ) )]
    [TemplatePart ( Name = AddRemoveButtonName, Type = typeof( Button ) )]
    [TemplateVisualState ( GroupName = "EditStates", Name = "ReadOnly" )]
    [TemplateVisualState ( GroupName = "EditStates", Name = "Editable" )]
    [TemplateVisualState ( GroupName = "CommentStates", Name = "Show" )]
    [TemplateVisualState ( GroupName = "CommentStates", Name = "Hide" )]
    public class AddRemoveCommentControl : Control
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for TextProperty Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register (
                "Text",
                typeof( string ),
                typeof( AddRemoveCommentControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for WatermarkTextProperty Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.Register (
                "WatermarkText",
                typeof( string ),
                typeof( AddRemoveCommentControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for IsReadOnlyProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register (
                "IsReadOnly",
                typeof( bool ),
                typeof( AddRemoveCommentControl ),
                new PropertyMetadata ( true, OnReadOnlyPropertyChanged ) );

        private const string AddRemoveButtonName = "PART_AddRemoveButton";
        private const string AddText = "Add Comment";
        private const string CommentTextBoxName = "PART_CommentTextBox";
        private const string RemoveText = "Remove Comment";

        private Button _addRemoveButton;
        private RadWatermarkTextBox _commentTextBox;
        private bool _templatedApplied;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRemoveCommentControl"/> class.
        /// </summary>
        public AddRemoveCommentControl ()
        {
            DefaultStyleKey = typeof( AddRemoveCommentControl );
            WatermarkText = "Enter comments";
        }

        #endregion

        #region Public Properties

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
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text of the comment.</value>
        public string Text
        {
            get
            {
                var textValue = ( string )GetValue ( TextProperty );

                if ( string.IsNullOrWhiteSpace ( textValue ) )
                {
                    SetValue ( TextProperty, null );
                    textValue = null;
                }

                return textValue;
            }

            set { SetValue ( TextProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the watermark text.
        /// </summary>
        /// <value>The watermark text.</value>
        public string WatermarkText
        {
            get { return ( string )GetValue ( WatermarkTextProperty ); }
            set { SetValue ( WatermarkTextProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();

            _commentTextBox = GetTemplateChild ( CommentTextBoxName ) as RadWatermarkTextBox;
            _addRemoveButton = GetTemplateChild ( AddRemoveButtonName ) as Button;

            if ( _addRemoveButton != null )
            {
                _addRemoveButton.Click += AddRemoveButtonClick;
            }

            if ( _commentTextBox != null )
            {
                _commentTextBox.TextChanged += CommentTextBoxTextChanged;
            }

            _templatedApplied = true;

            if ( _addRemoveButton != null )
            {
                _addRemoveButton.Content = string.IsNullOrWhiteSpace ( Text ) ? AddText : RemoveText;
            }
            HandleReadOnlyPropertyChanged ();
            UpdateCommentState ();
        }

        #endregion

        #region Methods

        private static void OnReadOnlyPropertyChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var addRemoveCommentControl = d as AddRemoveCommentControl;

            if ( addRemoveCommentControl != null )
            {
                addRemoveCommentControl.HandleReadOnlyPropertyChanged ();
            }
        }

        private void AddRemoveButtonClick ( object sender, RoutedEventArgs e )
        {
            if ( ( string )_addRemoveButton.Content == AddText )
            {
                #region Remove Comment

                _addRemoveButton.Content = RemoveText;
                Text = string.Empty;

                if ( _commentTextBox != null )
                {
                    _commentTextBox.Visibility = Visibility.Visible;
                }

                #endregion

                VisualStateManager.GoToState ( this, "Show", true );
            }
            else
            {
                #region Add Comment

                _addRemoveButton.Content = AddText;
                Text = null;

                if ( _commentTextBox != null )
                {
                    _commentTextBox.Visibility = Visibility.Collapsed;
                }

                #endregion

                VisualStateManager.GoToState ( this, "Hide", true );
            }
        }

        private void CommentTextBoxTextChanged ( object sender, TextChangedEventArgs e )
        {
            var bindingExpression = ( ( TextBox )sender ).GetBindingExpression ( TextBox.TextProperty );

            if ( bindingExpression != null )
            {
                bindingExpression.UpdateSource ();
            }
            UpdateCommentState ();
        }

        private void HandleReadOnlyPropertyChanged ()
        {
            if ( _templatedApplied )
            {
                VisualStateManager.GoToState ( this, IsReadOnly ? "ReadOnly" : "Editable", true );
            }
        }

        private void UpdateCommentState ()
        {
            VisualStateManager.GoToState ( this, string.IsNullOrEmpty ( Text ) ? "Hide" : "Show", true );
        }

        #endregion
    }
}
