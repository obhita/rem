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
using Rem.Ria.Infrastructure.ViewModel;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.TemplateSelectors
{
    /// <summary>
    /// Class for selecting workspace type template.
    /// </summary>
    public class WorkspaceTypeTemplateSelector : DataTemplateSelector
    {
        #region Constants and Fields

        /// <summary>
        /// Workspace Template Name Format
        /// </summary>
        public const string WorkspaceTemplateNameFormat = "Workspace{0}Template";

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, returns a DataTemplate based on custom logic.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>A <see cref="System.Windows.DataTemplate"/></returns>
        public override DataTemplate SelectTemplate ( object item, DependencyObject container )
        {
            DataTemplate template;
            if ( item is IWorkspaceHeaderContextProvider )
            {
                var type = ( item as IWorkspaceHeaderContextProvider ).Type;
                template = ( DataTemplate )Application.Current.Resources[string.Format ( WorkspaceTemplateNameFormat, type )];
            }
            else
            {
                template = ( DataTemplate )Application.Current.Resources[string.Format ( WorkspaceTemplateNameFormat, "Default" )];
            }
            return template;
        }

        #endregion
    }
}
