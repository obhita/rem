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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Telerik.Windows.Zip;

namespace Rem.Infrastructure.Utility
{
    /// <summary>
    /// The compressor.
    /// </summary>
    public static class Compressor
    {
        #region Public Methods

        /// <summary>
        /// The deserialize json.
        /// </summary>
        /// <param name="json">
        /// The json string. 
        /// </param>
        /// <typeparam name="T">
        /// Type to which json need to be deserialize. 
        /// </typeparam>
        /// <returns>
        /// Returns object of type T. 
        /// </returns>
        public static T DeserializeJson<T> ( string json )
        {
            using ( var ms = new MemoryStream ( Encoding.Unicode.GetBytes ( json ) ) )
            {
                var serializer = new DataContractJsonSerializer ( typeof( T ) );
                var obj = ( T )serializer.ReadObject ( ms );
                return obj;
            }
        }

        /// <summary>
        /// The from base 64 compressed string.
        /// </summary>
        /// <param name="base64CompressedString">
        /// The base 64 compressed string. 
        /// </param>
        /// <typeparam name="T">
        /// Type to which base 64 compressed string need to be read. 
        /// </typeparam>
        /// <returns>
        /// Returns object of type T. 
        /// </returns>
        public static T FromBase64CompressedString<T> ( string base64CompressedString )
        {
            var bytes = Convert.FromBase64String ( base64CompressedString );
            var memoryStream = new MemoryStream ( bytes );
            return ReadObject<T> ( memoryStream );
        }

        /// <summary>
        /// Reads given stream into object of given type.
        /// </summary>
        /// <param name="stream">
        /// The stream. 
        /// </param>
        /// <typeparam name="T">
        /// Object Type to which given stream need to be read. 
        /// </typeparam>
        /// <returns>
        /// Returns object of type T. 
        /// </returns>
        public static T ReadObject<T> ( Stream stream )
        {
            var inputStream = new ZipInputStream ( stream );
            string jsonString;
            using ( var reader = new StreamReader ( inputStream, Encoding.UTF8 ) )
            {
                inputStream.BaseStream.Position = 0L;
                jsonString = reader.ReadToEnd ();
            }

            return DeserializeJson<T> ( jsonString );
        }

        /// <summary>
        /// Converts given object to base 64 string.
        /// </summary>
        /// <param name="obj">
        /// An object that has to be converted to base 64 string. 
        /// </param>
        /// <returns>
        /// Returns to base 64 string. 
        /// </returns>
        public static string ToBase64String ( object obj )
        {
            var memoryStream = new MemoryStream ();
            WriteObject ( memoryStream, obj );
            var base64Encodedstring = Convert.ToBase64String ( memoryStream.ToArray () );
            return base64Encodedstring;
        }

        /// <summary>
        /// Writes given object to given stream serializing it into json.
        /// </summary>
        /// <param name="stream">
        /// The stream. 
        /// </param>
        /// <param name="obj">
        /// An object that has to be serialized to json and written to given stream. 
        /// </param>
        public static void WriteObject ( Stream stream, object obj )
        {
            var zipOutputStream = new ZipOutputStream ( stream );
            using ( var writer = new StreamWriter ( zipOutputStream ) )
            {
                var jsonString = SerializeJson ( obj );
                writer.Write ( jsonString );
                writer.Flush ();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes given object to json.
        /// </summary>
        /// <param name="obj">
        /// An object that has to be serialized to json. 
        /// </param>
        /// <returns>
        /// Returns given object as json. 
        /// </returns>
        private static string SerializeJson ( object obj )
        {
            var serializer = new DataContractJsonSerializer ( obj.GetType () );
            using ( var ms = new MemoryStream () )
            {
                serializer.WriteObject ( ms, obj );
                ms.Position = 0;

                using ( var reader = new StreamReader ( ms ) )
                {
                    return reader.ReadToEnd ();
                }
            }
        }

        #endregion
    }
}
