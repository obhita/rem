namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="MessageTransactionStatusDetail")]
    public partial class MessageTransactionStatusDetail
    {
        
        private string messageIdField;
        
        private string messageTransactionSourceField;
        
        private string messageTransactionSubSourceField;
        
        private string messageTransactionStateField;
        
        private string messageTimestampField;
        
        private string externalPatientIdField;
        
        private string externalUserIdField;
        
        private string externalUserIdTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="MessageId")]
        public virtual string MessageId
        {
            get
            {
                return this.messageIdField;
            }
            set
            {
                this.messageIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="MessageTransactionSource")]
        public virtual string MessageTransactionSource
        {
            get
            {
                return this.messageTransactionSourceField;
            }
            set
            {
                this.messageTransactionSourceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="MessageTransactionSubSource")]
        public virtual string MessageTransactionSubSource
        {
            get
            {
                return this.messageTransactionSubSourceField;
            }
            set
            {
                this.messageTransactionSubSourceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="MessageTransactionState")]
        public virtual string MessageTransactionState
        {
            get
            {
                return this.messageTransactionStateField;
            }
            set
            {
                this.messageTransactionStateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="MessageTimestamp")]
        public virtual string MessageTimestamp
        {
            get
            {
                return this.messageTimestampField;
            }
            set
            {
                this.messageTimestampField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ExternalPatientId")]
        public virtual string ExternalPatientId
        {
            get
            {
                return this.externalPatientIdField;
            }
            set
            {
                this.externalPatientIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ExternalUserId")]
        public virtual string ExternalUserId
        {
            get
            {
                return this.externalUserIdField;
            }
            set
            {
                this.externalUserIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ExternalUserIdType")]
        public virtual string ExternalUserIdType
        {
            get
            {
                return this.externalUserIdTypeField;
            }
            set
            {
                this.externalUserIdTypeField = value;
            }
        }
    }
}