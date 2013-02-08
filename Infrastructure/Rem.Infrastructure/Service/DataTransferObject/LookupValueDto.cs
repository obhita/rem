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
    /// The <see cref="LookupValueDto"/> is a single value from a 'range of values', also known as 'lookup'.
    /// </summary>
    [DataContract]
    public partial class LookupValueDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private string _name;

        private string _wellKnownName;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the name of the lookup value.
        /// </summary>
        /// <value> The name of the lookup value. </value>
        [DataMember]
        public string Name
        {
            get { return _name; }

            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        ///   Gets or sets the short name of the lookup value.
        /// </summary>
        /// <value> The short name of the lookup value. </value>
        [DataMember]
        public string ShortName { get; set; }

        /// <summary>
        ///   Gets or sets the sort order number among the rest of its lookup values.
        /// </summary>
        /// <value> The sort order number among the rest of its lookup values. </value>
        [DataMember]
        public int? SortOrderNumber { get; set; }

        /// <summary>
        ///   Gets or sets the well known name of the lookup value.
        /// </summary>
        /// <value> The well known name of the lookup value. </value>
        [DataMember]
        public string WellKnownName
        {
            get { return _wellKnownName; }

            set { ApplyPropertyChange ( ref _wellKnownName, () => WellKnownName, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">
        /// The other. 
        /// </param>
        /// <returns>
        /// Returns true if this dto is equal to the given dto.
        /// </returns>
        public bool Equals ( LookupValueDto other )
        {
            return other != null && other.Equals ( this );
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public override string ToString ()
        {
            return _name;
        }

        #endregion
    }
}
