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
using System.ServiceModel;

namespace Rem.Ria.Infrastructure.Web.WebService
{
    /// <summary>
    /// Dynamically create a WCF proxy class using the given interface.
    /// </summary>
    /// <typeparam name="TServiceContract">The type of the service contract.</typeparam>
    public class DynamicWebServiceProxy<TServiceContract> : IDisposable, IDynamicWebServiceProxy
    {
        #region Constants and Fields

        private ChannelFactory<TServiceContract> _channel;

        private TServiceContract _client;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicWebServiceProxy&lt;TServiceContract&gt;"/> class.
        /// </summary>
        public DynamicWebServiceProxy ()
        {
            EndpointName = typeof( TServiceContract ).Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicWebServiceProxy&lt;TServiceContract&gt;"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        public DynamicWebServiceProxy ( string endpointConfigurationName )
        {
            EndpointName = endpointConfigurationName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        public TServiceContract Client
        {
            get
            {
                if ( _client == null )
                {
                    if ( !typeof( TServiceContract ).IsInterface )
                    {
                        throw new NotSupportedException ( "TServiceContract must be an interface!" );
                    }
                    if ( string.IsNullOrEmpty ( EndpointName ) )
                    {
                        throw new NotSupportedException ( "EndpointName must be set prior to use!" );
                    }
                    _channel = new ChannelFactory<TServiceContract> ( EndpointName );
                    _client = _channel.CreateChannel ();
                }
                return _client;
            }
        }

        /// <summary>
        /// Gets or sets the name of the endpoint.
        /// </summary>
        /// <value>The name of the endpoint.</value>
        public string EndpointName { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose ()
        {
            if ( _channel != null )
            {
                _channel.CloseConnection ();
            }
            GC.SuppressFinalize ( this );
        }

        #endregion
    }
}
