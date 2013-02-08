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

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Rem.Ria.Infrastructure.Service
{
    /// <summary>
    /// Represents a basic http binding with binary encoding.
    /// </summary>
    public class BasicHttpBinaryBinding : BasicHttpBinding
    {
        #region Constructors and Destructors

        /// <summary>
        ///  Initializes a new instance of the BasicHttpBinaryBinding class.
        /// </summary>
        public BasicHttpBinaryBinding ()
            : this ( BasicHttpSecurityMode.None )
        {
        }

        /// <summary>
        /// Initializes a new instance of the BasicHttpBinaryBinding class.
        /// </summary>
        /// <param name="securityMode">
        /// The value of System.ServiceModel.BasicHttpSecurityMode that specifies 
        /// the type of security that is used with the SOAP message and for the client.
        /// </param>
        public BasicHttpBinaryBinding ( BasicHttpSecurityMode securityMode )
            : this ( securityMode, true )
        {
        }

        /// <summary>
        /// Initializes a new instance of the BasicHttpBinaryBinding class.
        /// </summary>
        /// <param name="securityMode">
        /// The value of System.ServiceModel.BasicHttpSecurityMode that specifies 
        /// the type of security that is used with the SOAP message and for the client.
        /// </param>
        /// <param name="binaryEncoding">
        /// Indicates whether the binary encoding is enabled or not
        /// </param>
        public BasicHttpBinaryBinding ( BasicHttpSecurityMode securityMode, bool binaryEncoding )
            : base ( securityMode )
        {
            BinaryEncoding = true;
            BinaryEncoding = binaryEncoding;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value that indicates whether the binary encoding is enabled or not. Default is true.
        /// </summary>
        public bool BinaryEncoding { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an ordered collection of binding elements contained in the current binding.
        /// </summary>
        /// <returns>The <see cref="T:System.ServiceModel.Channels.BindingElementCollection"/> that contains the ordered stack of binding elements described by the <see cref="T:System.ServiceModel.BasicHttpBinding"/>.</returns>
        public override BindingElementCollection CreateBindingElements ()
        {
            var elements = base.CreateBindingElements ();

            if ( BinaryEncoding )
            {
                // search the existing message encoding element (Text or MTOM) and replace it
                // note: the search must be done with the base type of text and mtom binding element, because this code is compiled against silverlight also 
                // and there is no mtom encoding available
                for ( var i = elements.Count - 1; i >= 0; i-- )
                {
                    var element = elements[i];
                    if ( element.GetType ().IsSubclassOf ( typeof( MessageEncodingBindingElement ) ) )
                    {
                        elements.RemoveAt ( i );
                        var bindingElement = new BinaryMessageEncodingBindingElement ();
                        elements.Insert ( i, bindingElement );
                        break;
                    }
                }
            }

            return elements;
        }

        #endregion
    }
}
