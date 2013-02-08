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

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Rem.Infrastructure.Service.DataTransferObject
{
    /// <summary>
    /// The <see cref="DataErrorInfo"/> class stores information about a rule violation.
    /// </summary>
    [DataContract]
    public class DataErrorInfo
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataErrorInfo"/> class.
        /// </summary>
        /// <param name="message">
        /// The message. 
        /// </param>
        /// <param name="errorLevel">
        /// The error level. 
        /// </param>
        public DataErrorInfo ( string message, ErrorLevel errorLevel )
            :
                this ( message, errorLevel, null )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataErrorInfo"/> class.
        /// </summary>
        /// <param name="message">
        /// The message. 
        /// </param>
        /// <param name="errorLevel">
        /// The error level. 
        /// </param>
        /// <param name="propertyNames">
        /// The property names. 
        /// </param>
        public DataErrorInfo (
            string message, 
            ErrorLevel errorLevel, 
            params string[] propertyNames )
        {
            if ( propertyNames != null )
            {
                var emptyPropertyNames = propertyNames.Where ( string.IsNullOrEmpty ).ToList ();
                if ( emptyPropertyNames.Any () )
                {
                    throw new ArgumentException ( "Null or empty property names are not allowed." );
                }
            }

            Message = message;
            ErrorLevel = errorLevel;
            Properties = propertyNames;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the type of error.
        /// </summary>
        /// <value> The type of the error. </value>
        public DataErrorInfoType DataErrorInfoType
        {
            get
            {
                DataErrorInfoType type = DataErrorInfoType.ObjectLevel;

                if ( Properties != null )
                {
                    if ( Properties.Length == 1 )
                    {
                        type = DataErrorInfoType.PropertyLevel;
                    }
                    else if ( Properties.Length > 1 )
                    {
                        type = DataErrorInfoType.CrossPropertyLevel;
                    }
                }

                return type;
            }
        }

        /// <summary>
        ///   Gets the error level.
        /// </summary>
        [DataMember]
        public ErrorLevel ErrorLevel { get; internal set; }

        /// <summary>
        ///   Gets the error message.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        ///   Gets the property names.
        /// </summary>
        [DataMember]
        public string[] Properties { get; internal set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public override string ToString ()
        {
            return Message;
        }

        #endregion
    }
}
