namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "PatientFormularyType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "PatientFormularyType")]
    public partial class PatientFormularyType
    {

        private string eligibilityGuidField;

        private string eligibilityIndexField;

        private string drugIdentifierField;

        private DrugDatabaseType drugIdentifierTypeField;

        private bool drugIdentifierTypeFieldSpecified;

        private string statusDisplayedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "eligibilityGuid")]
        public virtual string EligibilityGuid
        {
            get
            {
                return this.eligibilityGuidField;
            }
            set
            {
                this.eligibilityGuidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "eligibilityIndex")]
        public virtual string EligibilityIndex
        {
            get
            {
                return this.eligibilityIndexField;
            }
            set
            {
                this.eligibilityIndexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugIdentifier")]
        public virtual string DrugIdentifier
        {
            get
            {
                return this.drugIdentifierField;
            }
            set
            {
                this.drugIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugIdentifierType")]
        public virtual DrugDatabaseType DrugIdentifierType
        {
            get
            {
                return this.drugIdentifierTypeField;
            }
            set
            {
                this.drugIdentifierTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool DrugIdentifierTypeSpecified
        {
            get
            {
                return this.drugIdentifierTypeFieldSpecified;
            }
            set
            {
                this.drugIdentifierTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "statusDisplayed")]
        public virtual string StatusDisplayed
        {
            get
            {
                return this.statusDisplayedField;
            }
            set
            {
                this.statusDisplayedField = value;
            }
        }
    }
}