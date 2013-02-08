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

using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using Rem.Infrastructure.Mail;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Class for handling send C32 request.
    /// </summary>
    public class GetImapFolderItemsRequestHandler : NHibernateSessionRequestHandler<GetImapFolderItemsRequest, GetImapFolderItemsResponse>
    {
        #region Constants and Fields

        private readonly IImapMailMessageFetcher _imapMessageFetcher;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetImapFolderItemsRequestHandler"/> class.
        /// </summary>
        /// <param name="imapMessageFetcher">The imap message fetcher.</param>
        public GetImapFolderItemsRequestHandler(IImapMailMessageFetcher imapMessageFetcher)
        {
            _imapMessageFetcher = imapMessageFetcher;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetImapFolderItemsRequest request )
        {
            GetImapFolderItemsResponse response = CreateTypedResponse ();

            var newMessages = _imapMessageFetcher.FetchMessageList ( request.FolderName, request.LastId );

            var mailList = newMessages.Select (
                messageHeader => new DirectMailDto
                    {
                        From = messageHeader.FromAddress,
                        FromName = messageHeader.FromName,
                        To = messageHeader.ToAddress,
                        ToName = messageHeader.ToName,
                        Sent = messageHeader.SentDateTime,
                        Subject = messageHeader.Subject,
                        Id = messageHeader.Id,
                        IsRead = messageHeader.IsRead,
                        FolderName = messageHeader.FolderName,
                        HeadersOnly = true
                    } ).ToList ();

            response.Messsages = mailList;

            return response;
        }

        #endregion
    }
}
