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

using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Data transfer object for GpraNonResponseType class.
    /// </summary>
    /// <typeparam name="T">The type of non response value.</typeparam>
    public partial class TedsAnswerDto<T> : INonResponseDto
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAnswerDto&lt;T&gt;"/> class.
        /// </summary>
        public TedsAnswerDto()
        {
            PropertyChanged += ( s, e ) =>
                {
                    if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => TedsNonResponse ) )
                    {
                        RaisePropertyChanged ( () => NonResponse );
                    }
                    else if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => Response ) )
                    {
                        RaisePropertyChanged ( () => ValueObject );
                    }
                };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is nullable.
        /// </summary>
        public bool IsNullable
        {
            get { return typeof( T ).IsNullable (); }
        }

        /// <summary>
        /// Gets or sets the non response.
        /// </summary>
        /// <value>The non response.</value>
        public LookupValueDto NonResponse
        {
            get { return TedsNonResponse; }
            set { TedsNonResponse = value; }
        }

        /// <summary>
        /// Gets or sets the value object.
        /// </summary>
        /// <value>The value object.</value>
        public object ValueObject
        {
            get { return Response; }
            set { Response = (T)value; }
        }

        #endregion

        /// <summary>
        /// Sets as not answered.
        /// </summary>
        public void SetAsNotAnswered()
        {
            Response = default(T);
            NonResponse = null;
        }
    }
}
