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
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Xml;
using System.Xml.Xsl;
using C32Gen;
using Pillar.Common.Configuration;
using Pillar.Common.InversionOfControl;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Domain.Repository;
using Rem.Infrastructure.Mail;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.PatientModule;

namespace Rem.Ria.PatientModule.Web
{
    /// <summary>
    /// Class for handling HTTP.
    /// </summary>
    internal class HttpHandler : IHttpHandler, IReadOnlySessionState
    {
        #region Constants and Fields

        private static readonly string C32XslStylesheetPropertyName = "C32XsltStylesheet";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest ( HttpContext context )
        {
            var request = context.Request;
            var response = context.Response;

            var requestName = request.QueryString[HttpHandlerQueryStrings.RequestName];

            if ( requestName == HttpHandlerRequestNames.DownloadPatientDocument )
            {
                long patientDocumentKey;

                if ( long.TryParse ( request.QueryString[HttpHandlerQueryStrings.PatientDocumentKey], out patientDocumentKey ) )
                {
                    ProcessDownloadPatientDocumentRequest ( patientDocumentKey, response );
                }
                else
                {
                    response.Write ( "Patient Document Key is in incorrect format." );
                }
            }
            else if ( requestName == HttpHandlerRequestNames.ViewC32Document )
            {
                long patientKey;

                if ( long.TryParse ( request.QueryString[HttpHandlerQueryStrings.PatientKey], out patientKey ) )
                {
                    ProcessC32ToHtmlDocumentRequest ( patientKey, response );
                }
                else
                {
                    response.Write ( "Patient Key is in incorrect format." );
                }
            }
            else if ( requestName == HttpHandlerRequestNames.DownloadC32Document )
            {
                long patientKey;

                if ( long.TryParse ( request.QueryString[HttpHandlerQueryStrings.PatientKey], out patientKey ) )
                {
                    ProcessDownloadC32DocumentRequest ( patientKey, response );
                }
                else
                {
                    response.Write ( "Patient Key is in incorrect format." );
                }
            }
            else if ( requestName == HttpHandlerRequestNames.DownloadGreenC32Document )
            {
                long patientKey;

                if ( long.TryParse ( request.QueryString[HttpHandlerQueryStrings.PatientKey], out patientKey ) )
                {
                    ProcessDownloadGreenC32DocumentRequest ( patientKey, response );
                }
                else
                {
                    response.Write ( "Patient Key is in incorrect format." );
                }
            }
            else if ( requestName == HttpHandlerRequestNames.DownloadHl7ImmunizationDocument )
            {
                long visitKey, activitykey;

                if ( long.TryParse ( request.QueryString[HttpHandlerQueryStrings.VisitKey], out visitKey )
                     && long.TryParse ( request.QueryString[HttpHandlerQueryStrings.ActivityKey], out activitykey ) )
                {
                    ProcessDownloadHl7ImmunizationDocumentRequest ( activitykey, response );
                }
                else
                {
                    response.Write ( "Visit Key/Activity Key is in incorrect format." );
                }
            }
            else if ( requestName == HttpHandlerRequestNames.DownloadHl7SyndromicSurveillanceDocument )
            {
                long problemKey;

                if ( long.TryParse ( request.QueryString[HttpHandlerQueryStrings.ProblemKey], out problemKey ) )
                {
                    ProcessDownloadHl7SyndromicSurveillanceDocumentRequest ( problemKey, response );
                }
                else
                {
                    response.Write ( "Problem Key is in incorrect format." );
                }
            }
            else if (requestName == HttpHandlerRequestNames.DownloadMailAttachment)
            {
                int mailId;
                var mailFolderName = request.QueryString[HttpHandlerQueryStrings.MailFolderName];
                var mailAttachmentName = request.QueryString[HttpHandlerQueryStrings.MailAttachmentName];
                if (!string.IsNullOrWhiteSpace(mailFolderName) && !string.IsNullOrWhiteSpace(mailAttachmentName))
                {
                    if (int.TryParse(request.QueryString[HttpHandlerQueryStrings.MailId], out mailId))
                    {
                        ProcessDownloadMailAttachmentRequest ( mailId, mailFolderName, mailAttachmentName, response );
                    }
                }
                else
                {
                    response.Write("Mail folder name or attachment file name for the mail are not specified.");
                }
            }
            else
            {
                response.Write(string.Format("No {0} request is allowed.", requestName));
            }
        }

        #endregion

        #region Methods

        private static void ProcessC32ToHtmlDocumentRequest ( long patientKey, HttpResponse response )
        {
            var c32Builder = IoC.CurrentContainer.Resolve<IC32Builder>();
            var c32Xml = c32Builder.BuildC32Xml ( patientKey, false );

            if ( c32Xml != null )
            {
                var configProvider = IoC.CurrentContainer.Resolve<IConfigurationPropertiesProvider>();
                var stylesheetPath = configProvider.GetProperty ( C32XslStylesheetPropertyName );

                if ( string.IsNullOrWhiteSpace ( stylesheetPath ) )
                {
                    response.Write ( "The Config Store does not contain a setting for " + C32XslStylesheetPropertyName );
                }
                else
                {
                    var xmlDoc = new XmlDocument ();
                    var stringWriter = new StringWriter ();
                    var xslTransform = new XslCompiledTransform ();

                    xmlDoc.LoadXml ( c32Xml );
                    xslTransform.Load ( HttpRuntime.AppDomainAppPath + stylesheetPath );

                    xslTransform.Transform ( xmlDoc.CreateNavigator (), new XsltArgumentList (), stringWriter );
                    response.Clear ();

                    //response.ContentType = "application/octet-stream";
                    // response.AppendHeader ( "content-disposition", "attachment; filename=c32.xml" );
                    response.ContentType = "text/html";
                    response.Flush ();
                    response.Write ( stringWriter.ToString () );
                    response.End ();
                }
            }
            else
            {
                response.Write ( "No patient C32 document is found." );
            }
        }

        private static void ProcessDownloadC32DocumentRequest ( long patientKey, HttpResponse response )
        {
            var c32Builder = IoC.CurrentContainer.Resolve<IC32Builder> ();
            var c32Xml = c32Builder.BuildC32Xml ( patientKey, false );

            if ( c32Xml != null )
            {
                response.Clear ();
                response.ContentType = "application/octet-stream";
                response.AppendHeader ( "content-disposition", "attachment; filename=c32.xml" );
                response.Flush ();
                response.Write ( c32Xml );
                response.End ();
            }
            else
            {
                response.Write ( "No patient C32 document is found." );
            }
        }

        private static void ProcessDownloadGreenC32DocumentRequest ( long patientKey, HttpResponse response )
        {
            var c32DtoFactory = IoC.CurrentContainer.Resolve<IC32DtoFactory> ();
            var c32DtoSerializer = IoC.CurrentContainer.Resolve<IC32DtoSerializer> ();
            var greenC32Xml = c32DtoSerializer.GenerateGreenCdaXml ( c32DtoFactory.CreateC32Dto ( patientKey ) );

            if ( greenC32Xml != null )
            {
                response.Clear ();
                response.ContentType = "application/octet-stream";
                response.AppendHeader ( "content-disposition", "attachment; filename=greenC32.xml" );
                response.Flush ();
                response.Write ( greenC32Xml );
                response.End ();
            }
            else
            {
                response.Write ( "No patient C32 document is found." );
            }
        }

        private static void ProcessDownloadHl7ImmunizationDocumentRequest ( long activityKey, HttpResponse response )
        {
            var keyValues = new Dictionary<string, long> { { HttpHandlerQueryStrings.ActivityKey, activityKey } };

            var hl7Factory = IoC.CurrentContainer.Resolve<Hl7ImmunizationFactory> ();
            var hl7Message = hl7Factory.GetHl7Message ( keyValues );
            if ( string.IsNullOrWhiteSpace ( hl7Message ) )
            {
                return;
            }
            response.Clear ();
            response.ContentType = "application/octet-stream";
            response.AppendHeader ( "content-disposition", "attachment; filename=hl7_immunization.er7" );
            response.Flush ();
            response.Write ( hl7Message );
            response.End ();
        }

        private static void ProcessDownloadHl7SyndromicSurveillanceDocumentRequest ( long problemKey, HttpResponse response )
        {
            var keyValues = new Dictionary<string, long> { { HttpHandlerQueryStrings.ProblemKey, problemKey } };
            var hl7Factory = IoC.CurrentContainer.Resolve<Hl7SyndromicSurveillanceFactory> ();
            var hl7Message = hl7Factory.GetHl7Message ( keyValues );
            if ( hl7Message == null )
            {
                return;
            }
            response.Clear ();
            response.ContentType = "application/octet-stream";
            response.AppendHeader ( "content-disposition", "attachment; filename=hl7_SyndromicSurveillance.er7" );
            response.Flush ();
            response.Write ( hl7Message );
            response.End ();
        }

        private static void ProcessDownloadPatientDocumentRequest ( long patientDocumentKey, HttpResponse response )
        {
            PatientDocument patientDocument;

            var sessionProvider = IoC.CurrentContainer.Resolve<ISessionProvider> ();

            using ( var session = sessionProvider.GetSession () )
            {
                using ( var tran = session.BeginTransaction () )
                {
                    var patientDocumentRepository = IoC.CurrentContainer.Resolve<IPatientDocumentRepository> ();
                    patientDocument = patientDocumentRepository.GetByKey ( patientDocumentKey );
                    tran.Commit ();
                }
            }

            if ( patientDocument != null && patientDocument.Document != null )
            {
                response.AddHeader ( "Content-Disposition", "attachment; filename=" + patientDocument.FileName );
                response.AddHeader ( "Content-Length", patientDocument.Document.Length.ToString ( CultureInfo.InvariantCulture ) );
                response.OutputStream.Write ( patientDocument.Document, 0, patientDocument.Document.Length );
            }
            else
            {
                response.Write ( "No patient document is found." );
            }
        }

        private static void ProcessDownloadMailAttachmentRequest(int mailId, string mailFolderName, string mailAttachmentName, HttpResponse response)
        {
            MailMessage<ImapMailMessageHeader> message = null;

            var sessionProvider = IoC.CurrentContainer.Resolve<ISessionProvider>();
            using (sessionProvider.GetSession())
            {
                var imapMessageFetcher = IoC.CurrentContainer.Resolve<IImapMailMessageFetcher>();
                message = imapMessageFetcher.FetchMessage(mailFolderName, mailId);
            }

            if (message != null && message.Attachments.Count > 0)
            {
                var document = message.Attachments[0].ContentBytes;

                response.AddHeader("Content-Disposition", "attachment; filename=" + mailAttachmentName);
                response.AddHeader("Content-Length", document.Length.ToString( CultureInfo.InvariantCulture ));
                response.OutputStream.Write(document, 0, document.Length);
            }
            else
            {
                response.Write("No mail attachment is found.");
            }
        }

        #endregion
    }
}
