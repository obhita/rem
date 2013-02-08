namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "PatientAllergyType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "PatientAllergyType")]
    public partial class PatientAllergyType
    {

        private string allergyIDField;

        private DrugDatabaseType allergyTypeIDField;

        private AllergySeverityType allergySeverityTypeIDField;

        private bool allergySeverityTypeIDFieldSpecified;

        private string allergyCommentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "allergyID")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "allergyTypeID")]
        public virtual DrugDatabaseType AllergyTypeID
        {
            get
            {
                return this.allergyTypeIDField;
            }
            set
            {
                this.allergyTypeIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "allergySeverityTypeID")]
        public virtual AllergySeverityType AllergySeverityTypeID
        {
            get
            {
                return this.allergySeverityTypeIDField;
            }
            set
            {
                this.allergySeverityTypeIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool AllergySeverityTypeIDSpecified
        {
            get
            {
                return this.allergySeverityTypeIDFieldSpecified;
            }
            set
            {
                this.allergySeverityTypeIDFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "allergyComment")]
        public virtual string AllergyComment
        {
            get
            {
                return this.allergyCommentField;
            }
            set
            {
                this.allergyCommentField = value;
            }
        }
    }
}