namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="PatientFreeFormAllergyExtendedDetail")]
    public partial class PatientFreeFormAllergyExtendedDetail
    {
        
        private string externalIdField;
        
        private string nameField;
        
        private string severityTypeIDField;
        
        private string severityNameField;
        
        private string notesField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Name")]
        public virtual string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SeverityTypeID")]
        public virtual string SeverityTypeID
        {
            get
            {
                return this.severityTypeIDField;
            }
            set
            {
                this.severityTypeIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SeverityName")]
        public virtual string SeverityName
        {
            get
            {
                return this.severityNameField;
            }
            set
            {
                this.severityNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Notes")]
        public virtual string Notes
        {
            get
            {
                return this.notesField;
            }
            set
            {
                this.notesField = value;
            }
        }
    }
}