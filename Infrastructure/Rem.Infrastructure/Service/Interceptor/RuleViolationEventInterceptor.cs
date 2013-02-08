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
using System.Collections.Generic;
using Agatha.Common;
using NLog;
using Pillar.Domain.Event;
using Pillar.FluentRuleEngine;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Service.Interceptor
{
    /// <summary>
    /// This class is used to handle RuleViolationEvent and map failures to the data transfer object.
    ///   <remarks>
    /// This only works when the Respone is IDtoResponse.
    ///   </remarks>
    /// </summary>
    public class RuleViolationEventInterceptor : IRequestHandlerInterceptor
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        private readonly List<RuleViolation> _validationFailures = new List<RuleViolation> ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes after the handling request.
        /// </summary>
        /// <param name="context">
        /// The context. 
        /// </param>
        public void AfterHandlingRequest ( RequestProcessingContext context )
        {
            if (context!= null  && context.Response != null)
            {
                try
                {
                    if ( context.Response.Exception == null )
                    {
                        if ( _validationFailures.Count > 0 )
                        {
                            if ( context.Response is IDtoResponse )
                            {
                                var dto = ( context.Response as IDtoResponse ).GetDto ();
                                MapFailures ( _validationFailures, dto );
                            }
                            else
                            {
                                Logger.Error (
                                    "Validation failed but no failures mapped to data transfer object, because the response type ({0}) is not derived from {1}",
                                    context.Response.GetType ().Name,
                                    typeof( IDtoResponse ).Name );
                            }
                        }
                    }
                }
                catch ( Exception e )
                {
                    var response = context.Response;
                    response.Exception = new ExceptionInfo ( e );
                    response.ExceptionType = ExceptionType.Unknown;
                }
            }
        }

        /// <summary>
        /// Executes before the handling request.
        /// </summary>
        /// <param name="context">
        /// The context. 
        /// </param>
        public void BeforeHandlingRequest ( RequestProcessingContext context )
        {
            DomainEvent.Register<RuleViolationEvent> ( failure => _validationFailures.AddRange ( failure.RuleViolations ) );
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose ()
        {
        }

        #endregion

        #region Methods

        private static void MapFailures ( IEnumerable<RuleViolation> validationFailures, IValidatedObject validatedObject )
        {
            foreach ( var validationFailure in validationFailures )
            {
                validatedObject.AddDataErrorInfo (
                    new DataErrorInfo (
                        validationFailure.Message,
                        ErrorLevel.Error,
                        PropertyNameMapper.GetDestinationPropertyNames (validationFailure.OffendingObject.GetType (), validatedObject.GetType (), validationFailure.PropertyNames ) ) );
            }
        }

        #endregion
    }
}
