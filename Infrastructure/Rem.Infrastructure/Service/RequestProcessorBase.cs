using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using Agatha.Common.Caching;
using Agatha.Common.InversionOfControl;
using Agatha.ServiceLayer;
using NLog;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// This class processes requests.
    /// Most of code here is bought from Agatha source code <see cref="Agatha.ServiceLayer.RequestProcessor"/>.
    /// Change is made to easily clean up resources.
    /// </summary>
    public abstract class RequestProcessorBase : Disposable, IRequestProcessor
    {
        private readonly ServiceLayerConfiguration _serviceLayerConfiguration;
        private readonly ICacheManager _cacheManager;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestProcessorBase"/> class.
        /// </summary>
        /// <param name="serviceLayerConfiguration">The service layer configuration.</param>
        /// <param name="cacheManager">The cache manager.</param>
        protected RequestProcessorBase(ServiceLayerConfiguration serviceLayerConfiguration, ICacheManager cacheManager)
        {
            _serviceLayerConfiguration = serviceLayerConfiguration;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Disposes the managed resources.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            // empty by default but you should override this in derived classes so you can clean up your resources
        }

        /// <summary>
        /// Befores the processing.
        /// </summary>
        /// <param name="requests">The requests.</param>
        protected virtual void BeforeProcessing(IEnumerable<Request> requests) { }

        /// <summary>
        /// Afters the processing.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <param name="responses">The responses.</param>
        protected virtual void AfterProcessing(IEnumerable<Request> requests, IEnumerable<Response> responses) { }

        /// <summary>
        /// Befores the handle.
        /// </summary>
        /// <param name="request">The request.</param>
        protected virtual void BeforeHandle(Request request) { }

        /// <summary>
        /// Afters the handle.
        /// </summary>
        /// <param name="request">The request.</param>
        protected virtual void AfterHandle(Request request) { }

        /// <summary>
        /// Afters the handle.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected virtual void AfterHandle(Request request, Response response) { }

        /// <summary>
        /// Befores the resolving request handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected virtual void BeforeResolvingRequestHandler(Request request) { }

        /// <summary>
        /// Processes the specified requests.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns>An Response Array.</returns>
        public Response[] Process(params Request[] requests)
        {
            try
            {
                if (requests == null)
                {
                    return null;
                }

                var responses = new List<Response>(requests.Length);

                BeforeProcessing(requests);
                DispatchRequestsToHandlers(responses, requests);
                AfterProcessing(requests, responses);
                return responses.ToArray();
            }
            catch (Exception exc)
            {
                _logger.Error(exc);
                throw;
            }
            finally
            {
                DisposeUnmanagedResources();
            } 
        }

        private void DispatchRequestsToHandlers(List<Response> responses, params Request[] requests)
        {
            bool exceptionsPreviouslyOccurred = false;

            foreach (var request in requests)
            {
                var requestProcessingState = new RequestProcessingContext(request);
                IList<IRequestHandlerInterceptor> interceptors = new List<IRequestHandlerInterceptor>();
                try
                {
                    IList<IRequestHandlerInterceptor> invokedInterceptors = new List<IRequestHandlerInterceptor>();
                    if (!exceptionsPreviouslyOccurred)
                    {
                        interceptors = ResolveInterceptors();

                        foreach (var interceptor in interceptors)
                        {
                            interceptor.BeforeHandlingRequest(requestProcessingState);
                            invokedInterceptors.Add(interceptor);
                            if (requestProcessingState.IsProcessed)
                            {
                                responses.Add(requestProcessingState.Response);
                                break;
                            }
                        }
                    }

                    if (!requestProcessingState.IsProcessed)
                    {
                        var skipHandler = false;
                        var cachingIsEnabledForThisRequest = _cacheManager.IsCachingEnabledFor(request.GetType());

                        if (cachingIsEnabledForThisRequest)
                        {
                            var cachedResponse = _cacheManager.GetCachedResponseFor(request);

                            if (cachedResponse != null)
                            {
                                if (exceptionsPreviouslyOccurred)
                                {
                                    var dummyResponse = Activator.CreateInstance(cachedResponse.GetType()) as Response;
                                    responses.Add(SetStandardExceptionInfoWhenEarlierRequestsFailed(dummyResponse));
                                    requestProcessingState.MarkAsProcessed(dummyResponse);
                                }
                                else
                                {
                                    responses.Add(cachedResponse);
                                    requestProcessingState.MarkAsProcessed(cachedResponse);
                                }

                                skipHandler = true;
                            }
                        }
                        if (!skipHandler)
                        {
                            BeforeResolvingRequestHandler(request);

                            using (var handler = (IRequestHandler)IoC.Container.Resolve(GetRequestHandlerTypeFor(request)))
                            {
                                try
                                {
                                    if (!exceptionsPreviouslyOccurred)
                                    {
                                        var response = GetResponseFromHandler(request, handler);
                                        exceptionsPreviouslyOccurred = response.ExceptionType != ExceptionType.None;
                                        responses.Add(response);
                                        requestProcessingState.MarkAsProcessed(response);

                                        if (response.ExceptionType == ExceptionType.None && cachingIsEnabledForThisRequest)
                                        {
                                            _cacheManager.StoreInCache(request, response);
                                        }
                                    }
                                    else
                                    {
                                        var response = handler.CreateDefaultResponse();
                                        responses.Add(SetStandardExceptionInfoWhenEarlierRequestsFailed(response));
                                        requestProcessingState.MarkAsProcessed(response);
                                    }
                                }
                                finally
                                {
                                    IoC.Container.Release(handler);
                                }
                            }
                        }
                    }

                    foreach (var interceptor in invokedInterceptors.Reverse())
                    {
                        interceptor.AfterHandlingRequest(requestProcessingState);
                    }
                    invokedInterceptors.Clear();
                }
                finally
                {
                    SaveDisposeInterceptors(interceptors);
                }
            }
        }

        private void SaveDisposeInterceptors(IList<IRequestHandlerInterceptor> interceptors)
        {
            foreach (var interceptor in interceptors.Reverse())
            {
                try
                {
                    IoC.Container.Release(interceptor);
                    interceptor.Dispose();
                }
                catch (Exception exc)
                {
                    _logger.Error("error disposing " + interceptor, exc);
                }
            }
        }

        private IList<IRequestHandlerInterceptor> ResolveInterceptors()
        {
            return _serviceLayerConfiguration.GetRegisteredInterceptorTypes()
                .Select(t => (IRequestHandlerInterceptor)IoC.Container.Resolve(t)).ToList();
        }

        private Response SetStandardExceptionInfoWhenEarlierRequestsFailed(Response response)
        {
            response.ExceptionType = ExceptionType.EarlierRequestAlreadyFailed;
            response.Exception = new ExceptionInfo(new Exception(ExceptionType.EarlierRequestAlreadyFailed.ToString()));
            return response;
        }

        private static Type GetRequestHandlerTypeFor(Request request)
        {
            // get a type reference to IRequestHandler<ThisSpecificRequestType>
            return typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        }

        private Response GetResponseFromHandler(Request request, IRequestHandler handler)
        {
            try
            {
                BeforeHandle(request);
                var response = handler.Handle(request);
                AfterHandle(request);
                AfterHandle(request, response);
                return response;
            }
            catch (Exception e)
            {
                OnHandlerException(request, e);
                return CreateExceptionResponse(handler, e);
            }
        }

        /// <summary>
        /// Called when [handler exception].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="exception">The exception.</param>
        protected virtual void OnHandlerException(Request request, Exception exception)
        {
            _logger.Error(string.Format("{0}: unhandled exception while handling request!", GetType().Name), exception);
        }

        /// <summary>
        /// Creates the exception response.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>A Response.</returns>
        protected virtual Response CreateExceptionResponse(IRequestHandler handler, Exception exception)
        {
            var response = handler.CreateDefaultResponse();
            response.Exception = new ExceptionInfo(exception);
            SetExceptionType(response, exception);
            return response;
        }

        private void SetExceptionType(Response response, Exception exception)
        {
            var exceptionType = exception.GetType();

            if (exceptionType == _serviceLayerConfiguration.BusinessExceptionType)
            {
                response.ExceptionType = ExceptionType.Business;

                SetExceptionFaultCode(exception, response.Exception);

                return;
            }

            if (exceptionType == _serviceLayerConfiguration.SecurityExceptionType)
            {
                response.ExceptionType = ExceptionType.Security;
                return;
            }

            response.ExceptionType = ExceptionType.Unknown;
        }

        private void SetExceptionFaultCode(Exception exception, ExceptionInfo exceptionInfo)
        {
            var businessExceptionType = exception.GetType();

            var faultCodeProperty = businessExceptionType.GetProperty("FaultCode");

            if (faultCodeProperty != null
                && faultCodeProperty.CanRead
                && faultCodeProperty.PropertyType == typeof(string))
            {
                exceptionInfo.FaultCode = (string)faultCodeProperty.GetValue(exception, null);
            }
        }

        /// <summary>
        /// Processes the one way requests.
        /// </summary>
        /// <param name="requests">The requests.</param>
        public void ProcessOneWayRequests(params OneWayRequest[] requests)
        {
            try
            {
                if (requests == null) 
                {
                    return;
                }

                BeforeProcessing(requests);
                DispatchRequestsToHandlers(requests);
                AfterProcessing(requests, null);
            }
            catch (Exception exc)
            {
                _logger.Error(exc);
                throw;
            }
            finally
            {
                DisposeUnmanagedResources();
            } 
        }

        private void DispatchRequestsToHandlers(OneWayRequest[] requests)
        {
            foreach (var request in requests)
            {
                BeforeResolvingRequestHandler(request);

                using (var handler = (IOneWayRequestHandler)IoC.Container.Resolve(GetOneWayRequestHandlerTypeFor(request)))
                {
                    try
                    {
                        ExecuteHandler(request, handler);
                    }
                    finally
                    {
                        IoC.Container.Release(handler);
                    }
                }   
            }
        }

        private static Type GetOneWayRequestHandlerTypeFor(Request request)
        {
            return typeof(IOneWayRequestHandler<>).MakeGenericType(request.GetType());
        }

        private void ExecuteHandler(OneWayRequest request, IOneWayRequestHandler handler)
        {
            try
            {
                BeforeHandle(request);
                handler.Handle(request);
                AfterHandle(request);
            }
            catch (Exception e)
            {
                OnHandlerException(request, e);
            }
        }

        /// <summary>
        /// Called when [handler exception].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="exception">The exception.</param>
        protected virtual void OnHandlerException(OneWayRequest request, Exception exception)
        {
            _logger.Error(string.Format("{0}: unhandled exception while handling request!", GetType().Name), exception);
        }
    }
}
