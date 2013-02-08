namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://secure.newcropaccounts.com/interfaceV7", TypeName="PatientIdentifierType")]
    public partial class PatientIdentifierType
    {
        
        private string patientIDField;
        
        private string patientIDTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="patientID")]
        public virtual string PatientID
        {
            get
            {
                return this.patientIDField;
            }
            set
            {
                this.patientIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="patientIDType")]
        public virtual string PatientIDType
        {
            get
            {
                return this.patientIDTypeField;
            }
            set
            {
                this.patientIDTypeField = value;
            }
        }
    }
}