namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "DestinationType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "DestinationType")]
    public partial class DestinationType
    {

        private RequestedPageType requestedPageField;

        private string logoutPageField;

        private string sessionTimeoutInMinutesField;

        private string messageTransactionIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "requestedPage")]
        public virtual RequestedPageType RequestedPage
        {
            get
            {
                return this.requestedPageField;
            }
            set
            {
                this.requestedPageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "logoutPage")]
        public virtual string LogoutPage
        {
            get
            {
                return this.logoutPageField;
            }
            set
            {
                this.logoutPageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "sessionTimeoutInMinutes")]
        public virtual string SessionTimeoutInMinutes
        {
            get
            {
                return this.sessionTimeoutInMinutesField;
            }
            set
            {
                this.sessionTimeoutInMinutesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "messageTransactionID")]
        public virtual string MessageTransactionID
        {
            get
            {
                return this.messageTransactionIDField;
            }
            set
            {
                this.messageTransactionIDField = value;
            }
        }
    }
}