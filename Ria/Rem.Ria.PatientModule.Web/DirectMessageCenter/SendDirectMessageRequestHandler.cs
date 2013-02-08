using Agatha.Common;
using Agatha.ServiceLayer;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Mail;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    ///  Request Handler for the SendDirectMessageRequest
    /// </summary>
    public class SendDirectMessageRequestHandler : RequestHandler<SendDirectMessageRequest, SendDirectMessageResponse>
    {
        #region Constants and Fields

        private readonly ISessionProvider _sessionProvider;
        private readonly IUserInformationDtoFactory _userInformationDtoFactory;
        private readonly IMailMessageSender _mailMessageSender;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SendDirectMessageRequestHandler"/> class.
        /// </summary>
        /// <param name="statelessSessionProvider">The stateless session provider.</param>
        /// <param name="userInformationDtoFactory">The user information dto factory.</param>
        /// <param name="mailMessageSender">The mail message sender.</param>
        public SendDirectMessageRequestHandler (
           ISessionProvider statelessSessionProvider,
           IUserInformationDtoFactory userInformationDtoFactory,
           IMailMessageSender mailMessageSender)
       {
           _sessionProvider = statelessSessionProvider;
           _userInformationDtoFactory = userInformationDtoFactory;
            _mailMessageSender = mailMessageSender;
       }

        #endregion

        #region Public Methods

       /// <summary>
       /// Handles the specified request.
       /// </summary>
       /// <param name="request">The request.</param>
       /// <returns>Returns a Response object.</returns>
        public override Response Handle ( SendDirectMessageRequest request )
        {
            var response = CreateTypedResponse ();

            var session = _sessionProvider.GetSession ();
            var info = _userInformationDtoFactory.CreateUserInformationDto();
            var staff = session.QueryOver<Staff>().Where(s => s.Key == info.StaffKey).SingleOrDefault();
            
            var fromAddress = staff.DirectAddressCredential.DirectAddress.Address;
            var fromDisplayName =staff.StaffProfile.StaffName.Complete;

            var mailMessageBuilder = new MailMessageBuilder();

            mailMessageBuilder
               .Compose(fromAddress, fromDisplayName, request.ToDirectEmail, string.Empty, request.Subject, request.Body);

            if (request.AttachmentData != null)
            {
                mailMessageBuilder.WithAttachment(request.AttachmentData, request.AttachmentFileName);
            }

            _mailMessageSender.Send(mailMessageBuilder.MailMessage);

            return response;
        }

        #endregion
    }
}
