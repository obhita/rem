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

using System.Collections.Generic;
using System.Windows;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.GpraInterview;

namespace Rem.Ria.PatientModule.GpraInterview
{
    /// <summary>
    /// GpraDrugAlcoholBindingObject class.
    /// </summary>
    public class GpraDrugAlcoholBindingObject : DependencyObject
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CountFiltersProperty Property.
        /// </summary>
        public static readonly DependencyProperty CountFiltersProperty =
            DependencyProperty.Register (
                "CountFilters",
                typeof( IEnumerable<string> ),
                typeof( GpraDrugAlcoholBindingObject ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CountProperty Property.
        /// </summary>
        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register (
                "Count",
                typeof( GpraNonResponseTypeDto<int?> ),
                typeof( GpraDrugAlcoholBindingObject ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for RouteFiltersProperty Property.
        /// </summary>
        public static readonly DependencyProperty RouteFiltersProperty =
            DependencyProperty.Register (
                "RouteFilters",
                typeof( IEnumerable<string> ),
                typeof( GpraDrugAlcoholBindingObject ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for RouteProperty Property.
        /// </summary>
        public static readonly DependencyProperty RouteProperty =
            DependencyProperty.Register (
                "Route",
                typeof( GpraNonResponseTypeDto<LookupValueDto> ),
                typeof( GpraDrugAlcoholBindingObject ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for TextProperty Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register (
                "Text",
                typeof( string ),
                typeof( GpraDrugAlcoholBindingObject ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public GpraNonResponseTypeDto<int?> Count
        {
            get { return ( GpraNonResponseTypeDto<int?> )GetValue ( CountProperty ); }
            set { SetValue ( CountProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the count filters.
        /// </summary>
        /// <value>The count filters.</value>
        public IEnumerable<string> CountFilters
        {
            get { return ( IEnumerable<string> )GetValue ( CountFiltersProperty ); }
            set { SetValue ( CountFiltersProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        public GpraNonResponseTypeDto<LookupValueDto> Route
        {
            get { return ( GpraNonResponseTypeDto<LookupValueDto> )GetValue ( RouteProperty ); }
            set { SetValue ( RouteProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the route filters.
        /// </summary>
        /// <value>The route filters.</value>
        public IEnumerable<string> RouteFilters
        {
            get { return ( IEnumerable<string> )GetValue ( RouteFiltersProperty ); }
            set { SetValue ( RouteFiltersProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text for the object.</value>
        public string Text
        {
            get { return ( string )GetValue ( TextProperty ); }
            set { SetValue ( TextProperty, value ); }
        }

        #endregion
    }
}
