namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://secure.newcropaccounts.com/interfaceV7", TypeName="PatientAllergyFreeformType")]
    public partial class PatientAllergyFreeformType
    {
        
        private string allergyNameField;
        
        private AllergySeverityType allergySeverityTypeIDField;
        
        private bool allergySeverityTypeIDFieldSpecified;
        
        private string allergyCommentField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="allergyName")]
        public virtual string AllergyName
        {
            get
            {
                return this.allergyNameField;
            }
            set
            {
                this.allergyNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="allergySeverityTypeID")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="allergyComment")]
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName="ID")]
        public virtual string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
}