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
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// Class for adapting RAD tab control region.
    /// </summary>
    public class RadTabControlRegionAdapter : RegionAdapterBase<RadTabControl>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RadTabControlRegionAdapter"/> class.
        /// </summary>
        /// <param name="regionBehaviorFactory">The region behavior factory.</param>
        public RadTabControlRegionAdapter ( IRegionBehaviorFactory regionBehaviorFactory )
            : base ( regionBehaviorFactory )
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adapts a <see cref="RadTabControl"/> to an <see cref="IRegion"/>.
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void Adapt ( IRegion region, RadTabControl regionTarget )
        {
            if ( regionTarget == null )
            {
                throw new ArgumentNullException ( "regionTarget" );
            }

            var itemsSourceIsSet = regionTarget.ItemsSource != null;

            if ( itemsSourceIsSet )
            {
                throw new InvalidOperationException ( "Items control cannot have set Items Source if is region." );
            }
        }

        /// <summary>
        /// Attach new behaviors.
        /// </summary>
        /// <param name="region">The region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void AttachBehaviors ( IRegion region, RadTabControl regionTarget )
        {
            if ( region == null )
            {
                throw new ArgumentNullException ( "region" );
            }
            base.AttachBehaviors ( region, regionTarget );
            if ( !region.Behaviors.ContainsKey ( RadTabControlRegionSyncBehavior.BehaviorKey ) )
            {
                region.Behaviors.Add (
                    RadTabControlRegionSyncBehavior.BehaviorKey, new RadTabControlRegionSyncBehavior { HostControl = regionTarget } );
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="Region"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="Region"/>.</returns>
        protected override IRegion CreateRegion ()
        {
            return new SingleActiveRegion ();
        }

        #endregion
    }
}
