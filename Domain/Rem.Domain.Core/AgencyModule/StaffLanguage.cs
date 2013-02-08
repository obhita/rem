// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaffLanguage.cs" company="">
//   
// </copyright>
// <summary>
//   StaffLanguage defines a language that a staff has some level of proficiency in speaking.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// StaffLanguage defines a language that a staff has some level of proficiency in speaking.
    /// </summary>
    public class StaffLanguage : StaffAggregateNodeBase, IAggregateNodeValueObject
    {
        private Language _language;
        private LanguageFluency _languageFluency;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffLanguage"/> class.
        /// </summary>
        protected internal StaffLanguage ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffLanguage"/> class.
        /// </summary>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <param name="languageFluency">
        /// The language fluency.
        /// </param>
        public StaffLanguage ( Language language, LanguageFluency languageFluency )
        {
            Check.IsNotNull ( language, "Language cannot be null" );
            Check.IsNotNull ( languageFluency, "Language Fluency cannot be null" );

            _language = language;
            _languageFluency = languageFluency;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the language.
        /// </summary>
        [NotNull]
        public virtual Language Language
        {
            get { return _language; }
            private set { ApplyPropertyChange ( ref _language, () => Language, value ); }
        }

        /// <summary>
        /// Gets the language fluency.
        /// </summary>
        [NotNull]
        public virtual LanguageFluency LanguageFluency
        {
            get
            {
                return _languageFluency;
            }

            private set
            {
                Check.IsNotNull ( value, () => LanguageFluency );
                ApplyPropertyChange ( ref _languageFluency, () => LanguageFluency, value );
            }
        }

        #endregion

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="staffLanguage">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>
        public virtual bool ValuesEqual ( StaffLanguage staffLanguage )
        {
            if ( staffLanguage == null )
            {
                return false;
            }

            var valuesEqual = Equals ( _language.Key, staffLanguage._language.Key )
                              && Equals ( _languageFluency.Key, staffLanguage._languageFluency.Key );
            return valuesEqual;
        }
    }
}
