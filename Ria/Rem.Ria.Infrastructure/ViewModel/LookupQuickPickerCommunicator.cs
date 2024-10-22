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

using System.Windows;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// Class for communicating lookup quick picker.
    /// </summary>
    public class LookupQuickPickerCommunicator : QuickPickerCommunicator
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for EmptyTextProperty Property.
        /// </summary>
        public static readonly DependencyProperty EmptyTextProperty =
            DependencyProperty.Register (
                "EmptyText",
                typeof( string ),
                typeof( LookupQuickPickerCommunicator ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for LookupNameProperty Property.
        /// </summary>
        public static readonly DependencyProperty LookupNameProperty =
            DependencyProperty.Register (
                "LookupName",
                typeof( string ),
                typeof( LookupQuickPickerCommunicator ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the empty text.
        /// </summary>
        /// <value>The empty text.</value>
        public string EmptyText
        {
            get { return ( string )GetValue ( EmptyTextProperty ); }
            set { SetValue ( EmptyTextProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the lookup.
        /// </summary>
        /// <value>The name of the lookup.</value>
        public string LookupName
        {
            get { return ( string )GetValue ( LookupNameProperty ); }
            set { SetValue ( LookupNameProperty, value ); }
        }

        #endregion
    }
}
