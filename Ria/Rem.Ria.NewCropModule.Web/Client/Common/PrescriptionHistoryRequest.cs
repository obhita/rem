namespace Rem.Ria.NewCropModule.Web.Client.Common
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="PrescriptionHistoryRequest")]
    public partial class PrescriptionHistoryRequest
    {
        
        private System.DateTime startHistoryField;
        
        private System.DateTime endHistoryField;
        
        private string prescriptionStatusField;
        
        private string prescriptionSubStatusField;
        
        private string prescriptionArchiveStatusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="StartHistory")]
        public virtual System.DateTime StartHistory
        {
            get
            {
                return this.startHistoryField;
            }
            set
            {
                this.startHistoryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="EndHistory")]
        public virtual System.DateTime EndHistory
        {
            get
            {
                return this.endHistoryField;
            }
            set
            {
                this.endHistoryField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PrescriptionArchiveStatus")]
        public virtual string PrescriptionArchiveStatus
        {
            get
            {
                return this.prescriptionArchiveStatusField;
            }
            set
            {
                this.prescriptionArchiveStatusField = value;
            }
        }
    }
}