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
using Microsoft.Practices.ObjectBuilder2;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// CollapsingPanel class.
    /// </summary>
    public class CollapsingPanel : Panel
    {
        #region Constants and Fields

        private Size _oldSize = new Size ( double.MaxValue, double.MaxValue );

        #endregion

        #region Methods

        /// <summary>
        /// Provides the behavior for the Arrange pass of Silverlight layout. Classes can override this method to define their own Arrange pass behavior.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children.</param>
        /// <returns>The actual size that is used after the element is arranged in layout.</returns>
        protected override Size ArrangeOverride ( Size finalSize )
        {
            var ret = new Size ( 0, finalSize.Height );

            Children.ForEach (
                c =>
                    {
                        c.Arrange ( new Rect ( ret.Width, 0, c.DesiredSize.Width, finalSize.Height ) );
                        ret.Width += c.DesiredSize.Width;
                    } );

            return ret;
        }

        /// <summary>
        /// Provides the behavior for the Measure pass of Silverlight layout. Classes can override this method to define their own Measure pass behavior.
        /// </summary>
        /// <param name="availableSize">The available size that this object can give to child objects. Infinity (<see cref="F:System.Double.PositiveInfinity"/>) can be specified as a value to indicate that the object will size to whatever content is available.</param>
        /// <returns>The size that this object determines it needs during layout, based on its calculations of the allocated sizes for child objects; or based on other considerations, such as a fixed container size.</returns>
        protected override Size MeasureOverride ( Size availableSize )
        {
            var collapsableChildren = Children.Where ( c => c is ICollapsingControl );

            double totalChildWidth = 0;
            double maxChildHeight = 0;
            if ( collapsableChildren.Count () > 0 )
            {
                if ( _oldSize.Width > availableSize.Width )
                {
                    do
                    {
                        if ( totalChildWidth != 0 )
                        {
                            var orderedCollapsableChildren =
                                Children.Where ( c => c is ICollapsingControl ).OrderByDescending (
                                    c => ( c as ICollapsingControl ).Priority ).OrderByDescending ( c => Children.IndexOf ( c ) )
                                    .OfType<ICollapsingControl> ();
                            var childToCollapse =
                                orderedCollapsableChildren.Where ( c => c.State == CollapsingState.Large ).FirstOrDefault () ??
                                orderedCollapsableChildren.Where ( c => c.State == CollapsingState.Normal ).FirstOrDefault ();
                            if ( childToCollapse == null )
                            {
                                break;
                            }
                            childToCollapse.GetSmallerCommand.Execute ( null );
                        }
                        totalChildWidth = Children.Sum (
                            c =>
                                {
                                    c.Measure ( new Size ( double.MaxValue, availableSize.Height ) );
                                    maxChildHeight = Math.Max ( maxChildHeight, c.DesiredSize.Height );
                                    return c.DesiredSize.Width;
                                } );
                    }
                    while ( totalChildWidth > availableSize.Width );
                }
                else
                {
                    ICollapsingControl childToExpand = null;
                    var skip = 0;
                    do
                    {
                        if ( totalChildWidth != 0 )
                        {
                            var orderedCollapsableChildren =
                                Children.Where ( c => c is ICollapsingControl ).OrderBy ( c => ( c as ICollapsingControl ).Priority ).OrderBy (
                                    c => Children.IndexOf ( c ) ).OfType<ICollapsingControl> ();
                            childToExpand =
                                orderedCollapsableChildren.Where ( c => c.State == CollapsingState.Small ).Skip ( skip ).Take ( 1 ).FirstOrDefault () ??
                                orderedCollapsableChildren.Where ( c => c.State == CollapsingState.Normal ).FirstOrDefault ();
                            if ( childToExpand == null )
                            {
                                totalChildWidth = Children.Sum (
                                    c =>
                                        {
                                            c.Measure ( new Size ( double.MaxValue, availableSize.Height ) );
                                            maxChildHeight = Math.Max ( maxChildHeight, c.DesiredSize.Height );
                                            return c.DesiredSize.Width;
                                        } );
                                break;
                            }
                            childToExpand.GetLargerCommand.Execute ( null );
                        }
                        totalChildWidth = Children.Sum (
                            c =>
                                {
                                    c.Measure ( new Size ( double.MaxValue, availableSize.Height ) );
                                    maxChildHeight = Math.Max ( maxChildHeight, c.DesiredSize.Height );
                                    return c.DesiredSize.Width;
                                } );

                        if ( totalChildWidth > availableSize.Width )
                        {
                            if ( childToExpand != null )
                            {
                                childToExpand.GetSmallerCommand.Execute ( null );
                                skip++;
                            }
                        }
                    }
                    while ( true );
                }
            }
            else
            {
                Children.ForEach (
                    c =>
                        {
                            c.Measure ( new Size ( double.MaxValue, availableSize.Height ) );
                            totalChildWidth += c.DesiredSize.Width;
                            maxChildHeight = Math.Max ( maxChildHeight, c.DesiredSize.Height );
                        } );
            }
            _oldSize = availableSize;
            return new Size ( totalChildWidth, Math.Min ( maxChildHeight, availableSize.Height ) );
        }

        #endregion
    }
}
