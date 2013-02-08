using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// This class deinfines a Collapsing Panel Region Adapter
    /// </summary>
    public class CollapsingPanelRegionAdapter : RegionAdapterBase<CollapsingPanel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollapsingPanelRegionAdapter"/> class.
        /// </summary>
        /// <param name="regionBehaviorFactory">The region behavior factory.</param>
        public CollapsingPanelRegionAdapter ( IRegionBehaviorFactory regionBehaviorFactory )
            : base ( regionBehaviorFactory )
        {
        }

        /// <summary>
        /// Adapts the specified region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The region target.</param>
        protected override void Adapt ( IRegion region, CollapsingPanel regionTarget )
        {
            regionTarget.Children.Clear();

            region.Views.CollectionChanged += (s, e) => OnViewsCollectionChanged(e, regionTarget);

            // Add each View
            region.ActiveViews.ToList().ForEach(x => regionTarget.Children.Add(x as UIElement));
        }

        /// <summary>
        /// Creates the region.
        /// </summary>
        /// <returns>An IRegion.</returns>
        protected override IRegion CreateRegion ()
        {
            return new AllActiveRegion ();
        }

        private static void OnViewsCollectionChanged(NotifyCollectionChangedEventArgs e, CollapsingPanel collapsingPanel)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    foreach (var item in e.NewItems)
                    {
                        var fe = (FrameworkElement)item;

                        if (fe != null)
                        {
                            collapsingPanel.Children.Add(fe);
                        }
                        else
                        {
                            throw new Exception("Cannot add a Non-FrameworkElement object to the Items inside a Collapsing Panel");
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (FrameworkElement item in e.OldItems)
                    {
                        collapsingPanel.Children.Remove(item);
                    }

                    break;
            }
        }
    }
}
