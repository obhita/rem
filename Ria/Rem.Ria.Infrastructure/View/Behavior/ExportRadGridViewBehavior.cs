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
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// ExportRadGridViewTriggerAction class.
    /// </summary>
    public class ExportRadGridViewTriggerAction : TargetedTriggerAction<FrameworkElement>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AddLineProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddLineProperty =
            DependencyProperty.Register (
                "AddLine",
                typeof( string ),
                typeof( ExportRadGridViewTriggerAction ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ExportFormatProperty Property.
        /// </summary>
        public static readonly DependencyProperty ExportFormatProperty =
            DependencyProperty.Register (
                "ExportFormat",
                typeof( ExportFormat ),
                typeof( ExportRadGridViewTriggerAction ),
                new PropertyMetadata ( ExportFormat.ExcelML ) );

        /// <summary>
        /// Dependency Property for RadGridViewInstanceProperty Property.
        /// </summary>
        public static readonly DependencyProperty RadGridViewInstanceProperty =
            DependencyProperty.Register (
                "RadGridViewInstance",
                typeof( RadGridView ),
                typeof( ExportRadGridViewTriggerAction ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the add line.
        /// </summary>
        /// <value>The add line.</value>
        public string AddLine
        {
            get { return ( string )GetValue ( AddLineProperty ); }
            set { SetValue ( AddLineProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the export format.
        /// </summary>
        /// <value>The export format.</value>
        public ExportFormat ExportFormat
        {
            get { return ( ExportFormat )GetValue ( ExportFormatProperty ); }
            set { SetValue ( ExportFormatProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the RAD grid view instance.
        /// </summary>
        /// <value>The RAD grid view instance.</value>
        public RadGridView RadGridViewInstance
        {
            get { return ( RadGridView )GetValue ( RadGridViewInstanceProperty ); }
            set { SetValue ( RadGridViewInstanceProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the Action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke ( object parameter )
        {
            if ( RadGridViewInstance != null )
            {
                var extension = "xls";
                var fileTypeName = "Excel";
                switch ( ExportFormat )
                {
                    case ExportFormat.Text:
                        extension = "txt";
                        fileTypeName = "Text";
                        break;
                    case ExportFormat.Html:
                        extension = "html";
                        fileTypeName = "HTML";
                        break;
                    case ExportFormat.Csv:
                        extension = "csv";
                        fileTypeName = "CSV";
                        break;
                    case ExportFormat.ExcelML:
                        extension = "xls";
                        fileTypeName = "Excel";
                        break;
                }
                var saveFileDialog = new SaveFileDialog
                    {
                        DefaultExt = extension,
                        Filter =
                            string.Format (
                                "{1} files (*.{0})|*.{0}|All files (*.*)|*.*",
                                extension,
                                fileTypeName ),
                        FilterIndex = 1
                    };
                if ( saveFileDialog.ShowDialog () == true )
                {
                    using ( var stream = saveFileDialog.OpenFile () )
                    {
                        using ( var writer = new StreamWriter ( stream ) )
                        {
                            if ( AddLine != null )
                            {
                                writer.WriteLine ( AddLine );
                            }
                            RadGridViewInstance.Export (
                                stream,
                                new GridViewExportOptions
                                    {
                                        Format = ExportFormat,
                                        ShowColumnHeaders = true,
                                        ShowColumnFooters = true,
                                        ShowGroupFooters = false,
                                    } );
                        }
                    }
                }
            }
        }

        #endregion
    }
}
