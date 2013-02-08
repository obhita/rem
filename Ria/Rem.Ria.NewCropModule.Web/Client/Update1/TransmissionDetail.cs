namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="TransmissionDetail")]
    public partial class TransmissionDetail
    {
        
        private TransmissionMethodType transmissionMethodField;
        
        private string transmissionNetworkField;
        
        private System.Guid transactionGuidField;
        
        private System.Guid transactionDetailGuidField;
        
        private System.DateTime timestampField;
        
        private string userIdField;
        
        private string statusMessageField;
        
        private string detailMessageField;
        
        private string detailXmlResponseField;
        
        private PharmacyDetail pharmacyDetailField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="TransmissionMethod")]
        public virtual TransmissionMethodType TransmissionMethod
        {
            get
            {
                return this.transmissionMethodField;
            }
            set
            {
                this.transmissionMethodField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="TransmissionNetwork")]
        public virtual string TransmissionNetwork
        {
            get
            {
                return this.transmissionNetworkField;
            }
            set
            {
                this.transmissionNetworkField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="TransactionGuid")]
        public virtual System.Guid TransactionGuid
        {
            get
            {
                return this.transactionGuidField;
            }
            set
            {
                this.transactionGuidField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="TransactionDetailGuid")]
        public virtual System.Guid TransactionDetailGuid
        {
            get
            {
                return this.transactionDetailGuidField;
            }
            set
            {
                this.transactionDetailGuidField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Timestamp")]
        public virtual System.DateTime Timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="UserId")]
        public virtual string UserId
        {
            get
            {
                return this.userIdField;
            }
            set
            {
                this.userIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="StatusMessage")]
        public virtual string StatusMessage
        {
            get
            {
                return this.statusMessageField;
            }
            set
            {
                this.statusMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DetailMessage")]
        public virtual string DetailMessage
        {
            get
            {
                return this.detailMessageField;
            }
            set
            {
                this.detailMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DetailXmlResponse")]
        public virtual string DetailXmlResponse
        {
            get
            {
                return this.detailXmlResponseField;
            }
            set
            {
                this.detailXmlResponseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="pharmacyDetail")]
        public virtual PharmacyDetail PharmacyDetail
        {
            get
            {
                return this.pharmacyDetailField;
            }
            set
            {
                this.pharmacyDetailField = value;
            }
        }
    }
}