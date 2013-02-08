namespace Rem.Ria.NewCropModule.Web.Client.Common
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="EligibilityDetailResult")]
    public partial class EligibilityDetailResult
    {
        
        private Result resultField;
        
        private string eligibilityGuidField;
        
        private System.Collections.Generic.List<EligibilityDetail> eligibilityDetailArrayField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="eligibilityGuid")]
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
        [System.Xml.Serialization.XmlArrayAttribute(ElementName="eligibilityDetailArray")]
        public virtual System.Collections.Generic.List<EligibilityDetail> EligibilityDetailArray
        {
            get
            {
                return this.eligibilityDetailArrayField;
            }
            set
            {
                this.eligibilityDetailArrayField = value;
            }
        }
    }
}