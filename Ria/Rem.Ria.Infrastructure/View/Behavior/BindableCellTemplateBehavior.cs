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

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing bindable cell template.
    /// </summary>
    public class BindableCellTemplateBehavior : Behavior<RadGridView>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CellTemplatesProperty Property.
        /// </summary>
        public static readonly DependencyProperty CellTemplatesProperty =
            DependencyProperty.Register (
                "CellTemplates",
                typeof( ObservableCollection<DataTemplate> ),
                typeof( BindableCellTemplateBehavior ),
                new PropertyMetadata ( CellTemplateChanged ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the cell templates.
        /// </summary>
        /// <value>The cell templates.</value>
        public ObservableCollection<DataTemplate> CellTemplates
        {
            get { return ( ObservableCollection<DataTemplate> )GetValue ( CellTemplatesProperty ); }
            set { SetValue ( CellTemplatesProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            CreateColumns ();
        }

        private static void CellTemplateChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if ( e.OldValue != null )
            {
                var collection = e.OldValue as ObservableCollection<DataTemplate>;
                collection.CollectionChanged += Collection_CollectionChanged;
            }
            if ( e.NewValue != null )
            {
                var collection = e.NewValue as ObservableCollection<DataTemplate>;
                collection.CollectionChanged += Collection_CollectionChanged;
            }
        }

        private static void Collection_CollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            var behavior = sender as BindableCellTemplateBehavior;
            if ( behavior != null )
            {
                behavior.CreateColumns ();
            }
        }

        private void CreateColumns ()
        {
            AssociatedObject.Columns.Clear ();
            foreach ( var cellTemplate in CellTemplates )
            {
                if ( CellTemplates.IndexOf ( cellTemplate ) == CellTemplates.Count - 1 )
                {
                    AssociatedObject.Columns.Add (
                        new GridViewDataColumn { CellTemplate = cellTemplate, Width = new GridViewLength ( 1, GridViewLengthUnitType.Star ) } );
                }
                else
                {
                    AssociatedObject.Columns.Add ( new GridViewDataColumn { CellTemplate = cellTemplate } );
                }
            }
        }

        #endregion
    }
}
