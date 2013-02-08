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

using System.Data;
using Agatha.Common;
using Agatha.ServiceLayer;
using Pillar.Common.Utility;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Encapsulates the ground work needed to execute queries against the database, using Dapper. About Dapper: http://code.google.com/p/dapper-dot-net
    /// </summary>
    /// <typeparam name="TRequest">
    /// The request. 
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The response. 
    /// </typeparam>
    /// <typeparam name="THandler">
    /// The handler. 
    /// </typeparam>
    public abstract class DapperQueryRequestHandlerBase<TRequest, TResponse, THandler> : NHibernateSessionRequestHandler<TRequest, TResponse>
        where TRequest : Request
        where TResponse : Response, new ()
        where THandler : RequestHandler<TRequest, TResponse>
    {
        #region Constants and Fields

        /// <summary>
        ///   The database script.
        /// </summary>
        private string _databaseScript;

        /// <summary>
        ///   The request.
        /// </summary>
        private TRequest _request;

        /// <summary>
        ///   The response.
        /// </summary>
        private TResponse _response;

        /// <summary>
        ///   The transaction.
        /// </summary>
        private IDbTransaction _transaction;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets Connection.
        /// </summary>
        protected IDbConnection Connection
        {
            get { return Session.Connection; }
        }

        /// <summary>
        ///   Gets database script.
        /// </summary>
        protected string DatabaseScript
        {
            get { return _databaseScript; }
        }

        /// <summary>
        ///   Gets Request.
        /// </summary>
        protected TRequest Request
        {
            get { return _request; }
        }

        /// <summary>
        ///   Gets Response.
        /// </summary>
        protected TResponse Response
        {
            get { return _response; }
        }

        /// <summary>
        ///   Gets Transaction.
        /// </summary>
        protected IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The handler.
        /// </summary>
        /// <param name="request">
        /// The request. 
        /// </param>
        /// <returns>
        /// Returns the response. 
        /// </returns>
        public override Response Handle ( TRequest request )
        {
            _request = request;
            _response = CreateTypedResponse ();

            _databaseScript = GetDatabaseScript ();

            if ( _databaseScript != null )
            {
                _transaction = Session.Transaction as IDbTransaction;
                ExecuteQuery ();
            }

            return _response;
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method forms the query using the request, executes the query, and populates the response.
        /// </summary>
        protected abstract void ExecuteQuery ();

        /// <summary>
        /// Gets database script from the .sql file.
        /// </summary>
        /// <returns>
        /// Returns database script. 
        /// </returns>
        private static string GetDatabaseScript ()
        {
            var databaseScriptResourceName = typeof( THandler ).FullName + ".sql";
            return EmbeddedResourceUtil.GetEmbeddedResourceValue ( databaseScriptResourceName, typeof( THandler ).Assembly );
        }

        #endregion
    }
}
