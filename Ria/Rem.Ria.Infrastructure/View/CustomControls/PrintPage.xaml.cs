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

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// PrintPage class.
    /// </summary>
    public partial class PrintPage : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ChildProperty Property.
        /// </summary>
        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.Register (
                "Child",
                typeof( UIElement ),
                typeof( PrintPage ),
                new PropertyMetadata ( null, OnChildChanged ) );

        /// <summary>
        /// Dependency Property for CoverOffsetProperty Property.
        /// </summary>
        public static readonly DependencyProperty CoverOffsetProperty =
            DependencyProperty.Register (
                "CoverOffset",
                typeof( GridLength ),
                typeof( PrintPage ),
                new PropertyMetadata ( new GridLength ( 0 ) ) );

        /// <summary>
        /// Dependency Property for DateProperty Property.
        /// </summary>
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register (
                "Date",
                typeof( DateTime ),
                typeof( PrintPage ),
                new PropertyMetadata ( DateTime.Now ) );

        /// <summary>
        /// Dependency Property for FooterNoteProperty Property.
        /// </summary>
        public static readonly DependencyProperty FooterNoteProperty =
            DependencyProperty.Register (
                "FooterNote",
                typeof( string ),
                typeof( PrintPage ),
                new PropertyMetadata ( string.Empty ) );

        /// <summary>
        /// Dependency Property for FooterTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty FooterTemplateProperty =
            DependencyProperty.Register (
                "FooterTemplate",
                typeof( DataTemplate ),
                typeof( PrintPage ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for HeaderTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register (
                "HeaderTemplate",
                typeof( DataTemplate ),
                typeof( PrintPage ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for PageProperty Property.
        /// </summary>
        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register (
                "Page",
                typeof( int ),
                typeof( PrintPage ),
                new PropertyMetadata ( 0 ) );

        /// <summary>
        /// Dependency Property for TitleProperty Property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register (
                "Title",
                typeof( string ),
                typeof( PrintPage ),
                new PropertyMetadata ( string.Empty ) );

        /// <summary>
        /// Dependency Property for TotalPageCountProperty Property.
        /// </summary>
        public static readonly DependencyProperty TotalPageCountProperty =
            DependencyProperty.Register (
                "TotalPageCount",
                typeof( int ),
                typeof( PrintPage ),
                new PropertyMetadata ( 0 ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintPage"/> class.
        /// </summary>
        public PrintPage ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the child.
        /// </summary>
        /// <value>The child.</value>
        public UIElement Child
        {
            get { return ( UIElement )GetValue ( ChildProperty ); }
            set { SetValue ( ChildProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the cover offset.
        /// </summary>
        /// <value>The cover offset.</value>
        public GridLength CoverOffset
        {
            get { return ( GridLength )GetValue ( CoverOffsetProperty ); }
            set { SetValue ( CoverOffsetProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date printed.</value>
        public DateTime Date
        {
            get { return ( DateTime )GetValue ( DateProperty ); }
            set { SetValue ( DateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the footer note.
        /// </summary>
        /// <value>The footer note.</value>
        public string FooterNote
        {
            get { return ( string )GetValue ( FooterNoteProperty ); }
            set { SetValue ( FooterNoteProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the footer template.
        /// </summary>
        /// <value>The footer template.</value>
        public DataTemplate FooterTemplate
        {
            get { return ( DataTemplate )GetValue ( FooterTemplateProperty ); }
            set { SetValue ( FooterTemplateProperty, value ); }
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
        /// Gets or sets the page.
        /// </summary>
        /// <value>The current page.</value>
        public int Page
        {
            get { return ( int )GetValue ( PageProperty ); }
            set { SetValue ( PageProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return ( string )GetValue ( TitleProperty ); }
            set { SetValue ( TitleProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the total page count.
        /// </summary>
        /// <value>The total page count.</value>
        public int TotalPageCount
        {
            get { return ( int )GetValue ( TotalPageCountProperty ); }
            set { SetValue ( TotalPageCountProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public void ChangeOffset ( double offset )
        {
            Canvas.SetTop ( PrintViewboxContent, -1 * offset );
        }

        /// <summary>
        /// A method for getting the Footer hieght. It is recommended to use this method instead of the just the Property for the Footer object
        /// because when printing we are working out size the scope of the visual automatic update tree, so this guarentees the size returned
        /// has been recalculated and is up to date.
        /// </summary>
        /// <returns>Height of Footer</returns>
        public double GetFooterHeight ()
        {
            PrintFooter.Measure ( new Size ( double.MaxValue, double.MaxValue ) );
            return PrintFooter.DesiredSize.Height;
        }

        /// <summary>
        /// A method for getting the header hieght. It is recommended to use this method instead of the just the Property for the Header object
        /// because when printing we are working out size the scope of the visual automatic update tree, so this guarentees the size returned
        /// has been recalculated and is up to date.
        /// </summary>
        /// <returns>Height of Header</returns>
        public double GetHeaderHeight ()
        {
            PrintHeader.Measure ( new Size ( double.MaxValue, double.MaxValue ) );
            return PrintHeader.DesiredSize.Height;
        }

        /// <summary>
        /// Calculates the Available Height left on the page after the header and footer height is taken into account.
        /// </summary>
        /// <returns>A <see cref="System.Double"/></returns>
        public double GetHeightAvailable ()
        {
            return Height - ( GetFooterHeight () + GetHeaderHeight () );
        }

        #endregion

        #region Methods

        private static void OnChildChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( PrintPage )d ).OnChildChanged ();
        }

        private void OnChildChanged ()
        {
            PrintViewboxContent.Child = Child;
        }

        #endregion

        //Changes the offset of the child control in the viewport.
    }
}
