namespace Rem.Ria.NewCropModule.Web.Client.Common
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="PatientRequest")]
    public partial class PatientRequest
    {
        
        private string patientIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PatientId")]
        public virtual string PatientId
        {
            get
            {
                return this.patientIdField;
            }
            set
            {
                this.patientIdField = value;
            }
        }
    }
}