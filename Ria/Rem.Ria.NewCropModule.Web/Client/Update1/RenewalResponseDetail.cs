namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="RenewalResponseDetail")]
    public partial class RenewalResponseDetail
    {
        
        private string renewalRequestIdentifierField;
        
        private string statusField;
        
        private string messageField;
        
        private string sentTimestampField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="RenewalRequestIdentifier")]
        public virtual string RenewalRequestIdentifier
        {
            get
            {
                return this.renewalRequestIdentifierField;
            }
            set
            {
                this.renewalRequestIdentifierField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Status")]
        public virtual string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Message")]
        public virtual string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SentTimestamp")]
        public virtual string SentTimestamp
        {
            get
            {
                return this.sentTimestampField;
            }
            set
            {
                this.sentTimestampField = value;
            }
        }
    }
}