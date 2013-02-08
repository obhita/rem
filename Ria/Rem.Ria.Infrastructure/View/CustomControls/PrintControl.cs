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
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Printing;
using Pillar.Common.Utility;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// PrintControl class.
    /// </summary>
    [TemplatePart ( Name = "PART_PrintButton", Type = typeof( Button ) )]
    [TemplatePart ( Name = "PART_GridPrintPageContainer", Type = typeof( Grid ) )]
    public class PrintControl : Control
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ButtonStyleProperty Property.
        /// </summary>
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register (
                "ButtonStyle",
                typeof( Style ),
                typeof( PrintControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ControlToPrintProperty Property.
        /// </summary>
        public static readonly DependencyProperty ControlToPrintProperty =
            DependencyProperty.Register (
                "ControlToPrint",
                typeof( FrameworkElement ),
                typeof( PrintControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for FooterNoteProperty Property.
        /// </summary>
        public static readonly DependencyProperty FooterNoteProperty =
            DependencyProperty.Register (
                "FooterNote",
                typeof( string ),
                typeof( PrintControl ),
                new PropertyMetadata ( string.Empty ) );

        /// <summary>
        /// Dependency Property for FooterTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty FooterTemplateProperty =
            DependencyProperty.Register (
                "FooterTemplate",
                typeof( DataTemplate ),
                typeof( PrintControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for HeaderTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register (
                "HeaderTemplate",
                typeof( DataTemplate ),
                typeof( PrintControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for TitleProperty Property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register (
                "Title",
                typeof( string ),
                typeof( PrintControl ),
                new PropertyMetadata ( string.Empty ) );

        private readonly Dictionary<int, double> _coverOffsetCache = new Dictionary<int, double> ();

        private double _curOffset;
        private int _curentPage;
        private bool _forceStopPagging;
        private Grid _gridContainer;
        private object _parent;
        private PrintPage _printPage;
        private int? _totalPages;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintControl"/> class.
        /// </summary>
        public PrintControl ()
        {
            DefaultStyleKey = typeof( PrintControl );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the button style.
        /// </summary>
        /// <value>The button style.</value>
        public Style ButtonStyle
        {
            get { return ( Style )GetValue ( ButtonStyleProperty ); }
            set { SetValue ( ButtonStyleProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the control to print.
        /// </summary>
        /// <value>The control to print.</value>
        public FrameworkElement ControlToPrint
        {
            get { return ( FrameworkElement )GetValue ( ControlToPrintProperty ); }
            set { SetValue ( ControlToPrintProperty, value ); }
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
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return ( string )GetValue ( TitleProperty ); }
            set { SetValue ( TitleProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            var printButton = GetTemplateChild ( "PART_PrintButton" ) as Button;

            //This container must be there because in order to be able to update the layout correctly for the print page it needs to be part of the visual tree.
            _gridContainer = GetTemplateChild ( "PART_GridPrintPageContainer" ) as Grid;
            _printPage = new PrintPage ();

            //NOTE:If you do not have the print page be part of the visual tree initially the first print job performed will not update correclty and therefore will not look correct.
            _gridContainer.Children.Add ( _printPage );

            printButton.Click += PrintButtonClicked;
            InitializePrintPage ();
        }

        #endregion

        #region Methods

        private double GetIntersectOffset ( double curOffset, double pageWidth )
        {
            //Gets the position of the Control to print on the screen
            var mainTranform = ControlToPrint.TransformToVisual ( Application.Current.RootVisual );
            var mainOffset = mainTranform.Transform ( new Point ( 0, 0 ) );

            //Creates a 1 pixel line where the control is going to be paginated
            var intersectRect = new Rect ( mainOffset.X, curOffset + mainOffset.Y, pageWidth, 1 );

            //This gets all controls that the intersectRect crosses that are inside of the ControlToPrint
            //The coordinates of the intersectRect must be relative to the whole screen not just the control itself
            //all the second parameter does is filter the elements that get returned.
            var elements = VisualTreeHelper.FindElementsInHostCoordinates ( intersectRect, ControlToPrint );
            if ( elements.Count () == 0 )
            {
                _forceStopPagging = true;
            }
            var maxOffset = 0.0;
            foreach ( var uiElement in elements )
            {
                ////Any control that can have children can be paginated (ie. Panels,ContentControls,ItemControls)
                ////Things that dont have children (ie textblock,textbox,Image) cannot be paginated
                if ( VisualTreeHelper.GetChildrenCount ( uiElement ) <= 0 )
                {
                    ////TODO: possibly take into account other types of controls that can have children but are empty
                    ////what if there is a stackpanel with no children but a hieght of 100, this makes no sense in a UI but it is possible
                    if ( uiElement is Border )
                    {
                        ////an empty border can be paginated
                        continue;
                    }

                    //Make sure we have the corect size of the object being cut
                    uiElement.Measure ( new Size ( pageWidth, double.MaxValue ) );

                    //Get the coordinates of the element with respect to the control being printed
                    var gt = uiElement.TransformToVisual ( ControlToPrint );
                    var offset = gt.Transform ( new Point ( 0, 0 ) );

                    //Get the Height of the element that is left on the current page, this is used to know the adjustment needed to the overal offset
                    var elementOffset = ( curOffset + intersectRect.Height ) - offset.Y;

                    //Get the max of all controls that are being cut.
                    maxOffset = Math.Max ( maxOffset, elementOffset );
                }
            }
            return maxOffset;
        }

        private int GetTotalPages ()
        {
            if ( !_totalPages.HasValue )
            {
                _forceStopPagging = false;
                _coverOffsetCache.Clear ();
                var availableSizeOnPage = _printPage.GetHeightAvailable ();
                var pageWidth = _printPage.Width;

                //Calles measure to make sure we know the full height of the Control to Print
                ControlToPrint.Measure ( new Size ( pageWidth, double.MaxValue ) );
                var controlHeight = ControlToPrint.DesiredSize.Height;
                var curOffset = availableSizeOnPage;
                var pageCount = 1;

                //Loops through control hieght to find how many pages there needs to be
                while ( curOffset < controlHeight )
                {
                    var coverOffset = GetIntersectOffset ( curOffset, pageWidth );
                    _coverOffsetCache.Add ( pageCount, coverOffset );
                    curOffset -= coverOffset;
                    if ( _forceStopPagging )
                    {
                        break;
                    }
                    pageCount++;
                    curOffset += availableSizeOnPage;
                }
                if ( !_forceStopPagging )
                {
                    ////for last page, last page was already handled if pagginging was forcably stopped
                    _coverOffsetCache.Add ( pageCount, 0.0 );
                }
                _totalPages = pageCount;
            }
            return _totalPages.Value;
        }

        private void InitializePrintPage ()
        {
            SetBinding ( PrintPage.TitleProperty, PropertyUtil.ExtractPropertyName ( () => Title ) );
            SetBinding ( PrintPage.FooterNoteProperty, PropertyUtil.ExtractPropertyName ( () => FooterNote ) );
            SetBinding ( PrintPage.HeaderTemplateProperty, PropertyUtil.ExtractPropertyName ( () => HeaderTemplate ) );
            SetBinding ( PrintPage.FooterTemplateProperty, PropertyUtil.ExtractPropertyName ( () => FooterTemplate ) );
        }

        private void PrintButtonClicked ( object sender, RoutedEventArgs e )
        {
            _curentPage = 1;

            var printDocument = new PrintDocument ();
            printDocument.BeginPrint += PrintingStartedHandler;
            printDocument.PrintPage += PrintPageHandler;
            printDocument.EndPrint += PrintingEndedHandler;

            printDocument.Print ( string.IsNullOrWhiteSpace ( Title ) ? "Document" : Title );
        }

        private void PrintPageHandler ( object sender, PrintPageEventArgs e )
        {
            if ( _curentPage == 1 )
            {
                ////all pages are same size so only do this the first time.
                _printPage.Width = e.PrintableArea.Width;
                _printPage.Height = e.PrintableArea.Height;
            }

            e.PageVisual = _printPage;
            _printPage.UpdateLayout ();

            _printPage.TotalPageCount = GetTotalPages ();
            _printPage.Page = _curentPage;
            _printPage.ChangeOffset ( _curOffset );

            _curOffset += _printPage.GetHeightAvailable ();

            //The cover offset is used when something that cannot be paginated/split between pages is hit on a page line
            //So the print page covers the thing with white that is being cut and adjusts the overall offset to show it on the next page.
            var coverOffset = _coverOffsetCache[_curentPage];
            _printPage.CoverOffset = new GridLength ( coverOffset );
            _curOffset -= coverOffset;

            _printPage.UpdateLayout ();
            _curentPage++;

            e.HasMorePages = _curentPage <= _totalPages;
        }

        private void PrintingEndedHandler ( object sender, EndPrintEventArgs e )
        {
            var printDocument = sender as PrintDocument;
            if ( printDocument != null )
            {
                printDocument.BeginPrint -= PrintingStartedHandler;
                printDocument.EndPrint -= PrintingEndedHandler;
                printDocument.PrintPage -= PrintPageHandler;
            }
            _printPage.Child = null;

            //Added Control back to UI
            //Note: the removing and adding of the this control happens in such a way that the user never
            //even knows that it happens
            ( _parent as Grid ).Children.Add ( ControlToPrint );
        }

        private void PrintingStartedHandler ( object sender, BeginPrintEventArgs e )
        {
            ////Now that the print page has been initialized it can just be removed from its container,
            ////it does not need to be readed because it only needs to initialize in the visual tree once.
            ////All other layout updates happen in the PrintDocument.
            ////NOTE:If you do not have the print page be part of the visual tree initially the first print job performed will not update correclty and therefore will not look correct.
            _gridContainer.Children.Clear ();

            //Set datacontext so bindings still function correctly
            _printPage.DataContext = ControlToPrint.DataContext;
            _parent = ControlToPrint.Parent;

            //Right now we are assuming the control is in a Grid, possibly change this in the furture
            //The control needs to be removed from the UI because it can only have one parent at any given time.
            ( _parent as Grid ).Children.Remove ( ControlToPrint );
            _printPage.Child = ControlToPrint;

            _curOffset = 0.0;
            _totalPages = null;
        }

        private void SetBinding ( DependencyProperty dp, string propertyName )
        {
            var titleBinding = new Binding ();
            titleBinding.Source = this;
            titleBinding.Path = new PropertyPath ( propertyName );
            _printPage.SetBinding ( dp, titleBinding );
        }

        #endregion
    }
}
