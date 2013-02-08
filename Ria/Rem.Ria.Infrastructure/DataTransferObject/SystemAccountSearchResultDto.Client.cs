﻿#region License

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
using System.Text;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.Infrastructure.Web.DataTransferObject
{
    /// <summary>
    /// Data transfer object for SystemAccountSearchResult class.
    /// </summary>
    public partial class SystemAccountSearchResultDto : ISearchResultDto
    {
        #region Public Properties

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get { return FormatFullName (); }
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        public string SelectedText
        {
            get
            {
                if ( !string.IsNullOrEmpty ( FullName ) )
                {
                    return FullName;
                }
                return AccountName;
            }
        }

        #endregion

        #region Methods

        private string FormatFullName ()
        {
            //TODO:Format the name based on a preference; possibly something defined globally.
            var names = new[] { FirstName, MiddleName, LastName, SuffixName };
            var separator = " ";

            var formattedName = new StringBuilder ();
            foreach ( var name in names )
            {
                formattedName.Append ( !string.IsNullOrWhiteSpace ( name ) ? name + separator : string.Empty );
            }
            return formattedName.ToString ();
        }

        #endregion
    }
}
