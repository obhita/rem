namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="CounselingMessageDetail")]
    public partial class CounselingMessageDetail
    {
        
        private string displayOrderField;
        
        private string professionalMessageField;
        
        private string patientMessageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="displayOrder")]
        public virtual string DisplayOrder
        {
            get
            {
                return this.displayOrderField;
            }
            set
            {
                this.displayOrderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="professionalMessage")]
        public virtual string ProfessionalMessage
        {
            get
            {
                return this.professionalMessageField;
            }
            set
            {
                this.professionalMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="patientMessage")]
        public virtual string PatientMessage
        {
            get
            {
                return this.patientMessageField;
            }
            set
            {
                this.patientMessageField = value;
            }
        }
    }
}