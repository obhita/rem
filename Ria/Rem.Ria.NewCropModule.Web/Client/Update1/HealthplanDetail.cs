namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="HealthplanDetail")]
    public partial class HealthplanDetail
    {
        
        private string orgIdField;
        
        private string orgNameField;
        
        private string orgTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="orgId")]
        public virtual string OrgId
        {
            get
            {
                return this.orgIdField;
            }
            set
            {
                this.orgIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="orgName")]
        public virtual string OrgName
        {
            get
            {
                return this.orgNameField;
            }
            set
            {
                this.orgNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="orgType")]
        public virtual string OrgType
        {
            get
            {
                return this.orgTypeField;
            }
            set
            {
                this.orgTypeField = value;
            }
        }
    }
}