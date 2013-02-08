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

using System.Runtime.Serialization;

namespace Rem.Infrastructure.Service.DataTransferObject
{
    /// <summary>
    /// The <see cref="KeyedDataTransferObject"/> is base class of all the dtos that have key, to uniquely identity themselves.
    /// </summary>
    [DataContract]
    public abstract partial class KeyedDataTransferObject : AbstractDataTransferObject
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="KeyedDataTransferObject" /> class. Initializes the Key to '0'.
        /// </summary>
        protected KeyedDataTransferObject ()
        {
            Key = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the key.
        /// </summary>
        /// <value> The key that uniquely identifies this dto. </value>
        [DataMember]
        public long Key { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks if this dto is equal to  .
        /// </summary>
        /// <param name="keyedDto">
        /// The keyed dto. 
        /// </param>
        /// <returns>
        /// Returns true if this dto is equal to 
        /// </returns>
        public virtual bool Equals ( KeyedDataTransferObject keyedDto )
        {
            if ( ReferenceEquals ( null, keyedDto ) )
            {
                return false;
            }

            if ( ReferenceEquals ( this, keyedDto ) )
            {
                return true;
            }

            if ( GetType () != keyedDto.GetType () )
            {
                return false;
            }

            var otherIsTransient = Equals ( keyedDto.Key, ( long )0 );
            var thisIsTransient = Equals ( Key, ( long )0 );

            if ( otherIsTransient && thisIsTransient )
            {
                return ReferenceEquals ( keyedDto, this );
            }

            return keyedDto.Key.Equals ( Key );
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with this instance. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c> . 
        /// </returns>
        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }

            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }

            return GetType () == obj.GetType () && Equals ( obj as KeyedDataTransferObject );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode ()
        {
            return ( Key != 0 ? Key.GetHashCode () : string.Empty.GetHashCode () * 397 ) ^ GetType ().GetHashCode ();
        }

        #endregion
    }
}
