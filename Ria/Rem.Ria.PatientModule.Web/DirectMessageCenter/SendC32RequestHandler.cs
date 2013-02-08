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
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using Agatha.Common;
using C32Gen;
using Pillar.Common.Configuration;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Configuration;
using Rem.Infrastructure.Mail;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Class for handling send C32 request.
    /// </summary>
    public class SendC32RequestHandler : NHibernateSessionRequestHandler<SendC32Request, SendC32Response>
    {
        #region Constants and Fields

        private readonly IC32Builder _builder;
        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;
        private readonly IMailMessageSender _mailMessageSender;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SendC32RequestHandler"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        /// <param name="mailMessageSender">The mail message sender.</param>
        public SendC32RequestHandler (
            IC32Builder builder,
            IConfigurationPropertiesProvider configurationPropertiesProvider,
            IMailMessageSender mailMessageSender )
        {
            _builder = builder;
            _configurationPropertiesProvider = configurationPropertiesProvider;
            _mailMessageSender = mailMessageSender;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( SendC32Request request )
        {
            SendC32Response response = CreateTypedResponse ();

            Staff staff = Session.QueryOver<Staff> ().Where ( s => s.Key == request.StaffKey ).SingleOrDefault ();

            string fromAddress = staff.DirectAddressCredential.DirectAddress.Address;
            var fromDisplayName = staff.StaffProfile.StaffName.Complete;

            long patientKey = request.PatientKey;

            // Build Normative C32 Document
            string c32 = _builder.BuildC32Xml ( patientKey, false );

           // Build Html Document representing the C32 
            string c32Html = CreateHtmlC32 ( c32);

            string body = request.Body;

            string toAddress = request.ToDirectEmail;

            var mailMessageBuilder = new MailMessageBuilder ();
            mailMessageBuilder
                .Compose ( fromAddress, fromDisplayName, toAddress, string.Empty, request.Subject, body )
                .WithAttachment ( c32, string.Format ( "{0}{1}", DateTime.Now.Ticks, ".xml" ))
                .WithAttachment ( c32Html, "Preview.html");

            _mailMessageSender.Send(mailMessageBuilder.MailMessage);

            return response;
        }

        #endregion

        #region Methods

        private string CreateHtmlC32 ( string c32Xml)
        {
            // TODO: This could be resused somewhere else.

            string stylesheetPath = _configurationPropertiesProvider.GetProperty(SettingKeyNames.C32XsltStylesheet);

            var xmlDoc = new XmlDocument ();
            var stringWriter = new StringWriter ();
            var xslTransform = new XslCompiledTransform ();

            xmlDoc.LoadXml ( c32Xml );
            xslTransform.Load ( HttpRuntime.AppDomainAppPath + stylesheetPath );
            xslTransform.Transform ( xmlDoc.CreateNavigator (), new XsltArgumentList (), stringWriter );

            string html = stringWriter.ToString ();
            return html;
        }

        #endregion
    }
}
