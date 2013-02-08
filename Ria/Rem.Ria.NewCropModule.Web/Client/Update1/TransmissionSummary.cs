namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="TransmissionSummary")]
    public partial class TransmissionSummary
    {
        
        private string externalIdField;
        
        private System.Guid prescriptionGuidField;
        
        private string prescriptionStatusField;
        
        private string prescriptionSubStatusField;
        
        private string prescriptionArchiveField;
        
        private string summaryMessageField;
        
        private string summaryXmlResponseField;
        
        private int transmissionDetailCountField;
        
        private PatientDrugDetail5 drugDetailField;
        
        private System.Collections.Generic.List<TransmissionDetail> transmissionDetailArrayField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ExternalId")]
        public virtual string ExternalId
        {
            get
            {
                return this.externalIdField;
            }
            set
            {
                this.externalIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PrescriptionGuid")]
        public virtual System.Guid PrescriptionGuid
        {
            get
            {
                return this.prescriptionGuidField;
            }
            set
            {
                this.prescriptionGuidField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PrescriptionStatus")]
        public virtual string PrescriptionStatus
        {
            get
            {
                return this.prescriptionStatusField;
            }
            set
            {
                this.prescriptionStatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PrescriptionSubStatus")]
        public virtual string PrescriptionSubStatus
        {
            get
            {
                return this.prescriptionSubStatusField;
            }
            set
            {
                this.prescriptionSubStatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PrescriptionArchive")]
        public virtual string PrescriptionArchive
        {
            get
            {
                return this.prescriptionArchiveField;
            }
            set
            {
                this.prescriptionArchiveField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SummaryMessage")]
        public virtual string SummaryMessage
        {
            get
            {
                return this.summaryMessageField;
            }
            set
            {
                this.summaryMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SummaryXmlResponse")]
        public virtual string SummaryXmlResponse
        {
            get
            {
                return this.summaryXmlResponseField;
            }
            set
            {
                this.summaryXmlResponseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="TransmissionDetailCount")]
        public virtual int TransmissionDetailCount
        {
            get
            {
                return this.transmissionDetailCountField;
            }
            set
            {
                this.transmissionDetailCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="drugDetail")]
        public virtual PatientDrugDetail5 DrugDetail
        {
            get
            {
                return this.drugDetailField;
            }
            set
            {
                this.drugDetailField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(ElementName="transmissionDetailArray")]
        public virtual System.Collections.Generic.List<TransmissionDetail> TransmissionDetailArray
        {
            get
            {
                return this.transmissionDetailArrayField;
            }
            set
            {
                this.transmissionDetailArrayField = value;
            }
        }
    }
}