namespace Rem.Ria.NewCropModule.Web.Client.Patient
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="PatientAllergyDetail")]
    public partial class PatientAllergyDetail
    {
        
        private string allergyField;
        
        private string allergyIDField;
        
        private string allergyConceptIDField;
        
        private string allergySourceIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Allergy")]
        public virtual string Allergy
        {
            get
            {
                return this.allergyField;
            }
            set
            {
                this.allergyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="AllergyID")]
        public virtual string AllergyID
        {
            get
            {
                return this.allergyIDField;
            }
            set
            {
                this.allergyIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="AllergyConceptID")]
        public virtual string AllergyConceptID
        {
            get
            {
                return this.allergyConceptIDField;
            }
            set
            {
                this.allergyConceptIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="AllergySourceID")]
        public virtual string AllergySourceID
        {
            get
            {
                return this.allergySourceIDField;
            }
            set
            {
                this.allergySourceIDField = value;
            }
        }
    }
}