namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="RenewalDetail")]
    public partial class RenewalDetail
    {
        
        private string externalLocationIdField;
        
        private string externalDoctorIdField;
        
        private NCRenewal renewalField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ExternalLocationId")]
        public virtual string ExternalLocationId
        {
            get
            {
                return this.externalLocationIdField;
            }
            set
            {
                this.externalLocationIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ExternalDoctorId")]
        public virtual string ExternalDoctorId
        {
            get
            {
                return this.externalDoctorIdField;
            }
            set
            {
                this.externalDoctorIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="renewal")]
        public virtual NCRenewal Renewal
        {
            get
            {
                return this.renewalField;
            }
            set
            {
                this.renewalField = value;
            }
        }
    }
}