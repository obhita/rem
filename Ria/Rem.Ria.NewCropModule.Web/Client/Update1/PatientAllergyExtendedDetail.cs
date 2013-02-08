namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="PatientAllergyExtendedDetail")]
    public partial class PatientAllergyExtendedDetail
    {
        
        private string nameField;
        
        private string compositeIDField;
        
        private string conceptIDField;
        
        private string sourceField;
        
        private string conceptTypeIDField;
        
        private string statusField;
        
        private string severityTypeIDField;
        
        private string severityNameField;
        
        private string notesField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="CompositeID")]
        public virtual string CompositeID
        {
            get
            {
                return this.compositeIDField;
            }
            set
            {
                this.compositeIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ConceptID")]
        public virtual string ConceptID
        {
            get
            {
                return this.conceptIDField;
            }
            set
            {
                this.conceptIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Source")]
        public virtual string Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ConceptTypeID")]
        public virtual string ConceptTypeID
        {
            get
            {
                return this.conceptTypeIDField;
            }
            set
            {
                this.conceptTypeIDField = value;
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