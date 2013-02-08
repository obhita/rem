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
    /// CommunicationObjectExtension class.
    /// </summary>
    public static class CommunicationObjectExtension
    {
        #region Public Methods

        /// <summary>
        /// Safely closes a service client connection.
        /// </summary>
        /// <param name="serviceClient">The service client.</param>
        public static void CloseConnection ( this ICommunicationObject serviceClient )
        {
            if ( serviceClient == null )
            {
                return;
            }

            try
            {
                if ( serviceClient.State == CommunicationState.Opened )
                {
                    serviceClient.Close ();
                }
                else
                {
                    serviceClient.Abort ();
                }
            }
            catch ( CommunicationException )
            {
                // Logging.Logger.Log(ex);
                try
                {
                    serviceClient.Abort ();
                }
                catch
                {
                } //nasty but nothing useful can be found by 
                //logging this exception as secondary issue
            }
            catch ( TimeoutException )
            {
                //Logging.Logger.Log(ex);
                try
                {
                    serviceClient.Abort ();
                }
                catch
                {
                } //nasty but nothing useful can be found by 
                //logging this exception as secondary issue
            }
            catch ( Exception )
            {
                // Logging.Logger.Log(ex);
                try
                {
                    serviceClient.Abort ();
                }
                catch
                {
                } //nasty but nothing useful can be found by 
                //logging this exception as secondary issue
                throw;
            }
        }

        #endregion
    }
}
