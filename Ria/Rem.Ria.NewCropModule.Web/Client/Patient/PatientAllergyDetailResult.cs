using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Patient
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="PatientAllergyDetailResult")]
    public partial class PatientAllergyDetailResult
    {
        
        private Result resultField;
        
        private System.Collections.Generic.List<PatientAllergyDetail> patientAllergyDetailField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="result")]
        public virtual Result Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(ElementName="patientAllergyDetail")]
        public virtual System.Collections.Generic.List<PatientAllergyDetail> PatientAllergyDetail
        {
            get
            {
                return this.patientAllergyDetailField;
            }
            set
            {
                this.patientAllergyDetailField = value;
            }
        }
    }
}