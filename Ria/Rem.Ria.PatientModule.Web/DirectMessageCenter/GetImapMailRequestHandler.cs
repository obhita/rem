using Agatha.Common;
using Agatha.ServiceLayer;
using Rem.Infrastructure.Mail;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Agatha request handler for GetImapMailRequest.
    /// </summary>
    public class GetImapMailRequestHandler : RequestHandler<GetImapMailRequest, GetImapMailResponse>
    {
        private readonly ImapMailMessageFetcher _imapMessageReceiver;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetImapMailRequestHandler"/> class.
        /// </summary>
        /// <param name="imapMessageReceiver">The imap message receiver.</param>
        public GetImapMailRequestHandler (ImapMailMessageFetcher imapMessageReceiver)
        {
            _imapMessageReceiver = imapMessageReceiver;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A response.</returns>
        public override Response Handle(GetImapMailRequest request)
        {
            var response = CreateTypedResponse ();

            var message = _imapMessageReceiver.FetchMessage(request.FolderName, request.MailId);

            var mailDto = new DirectMailDto
                {
                    From = message.Header.FromAddress,
                    To = message.Header.ToAddress,
                    Sent = message.Header.SentDateTime,
                    Subject = message.Header.Subject,
                    Id = message.Header.Id,
                    Message = message.BodyText,
                    IsRead = message.Header.IsRead,
                    FolderName = message.Header.FolderName,
                    AttachmentName = message.Attachments.Count == 0 ? null : message.Attachments[0].FileName,
                    HeadersOnly = false
                };

            response.Messsage = mailDto;

            return response;
        }
    }
}