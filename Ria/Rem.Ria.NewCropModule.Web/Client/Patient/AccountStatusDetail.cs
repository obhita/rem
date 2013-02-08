namespace Rem.Ria.NewCropModule.Web.Client.Patient
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="AccountStatusDetail")]
    public partial class AccountStatusDetail
    {
        
        private string pendingRxCountField;
        
        private string alertCountField;
        
        private string faxCountField;
        
        private string pharmComCountField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PendingRxCount")]
        public virtual string PendingRxCount
        {
            get
            {
                return this.pendingRxCountField;
            }
            set
            {
                this.pendingRxCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="AlertCount")]
        public virtual string AlertCount
        {
            get
            {
                return this.alertCountField;
            }
            set
            {
                this.alertCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="FaxCount")]
        public virtual string FaxCount
        {
            get
            {
                return this.faxCountField;
            }
            set
            {
                this.faxCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PharmComCount")]
        public virtual string PharmComCount
        {
            get
            {
                return this.pharmComCountField;
            }
            set
            {
                this.pharmComCountField = value;
            }
        }
    }
}