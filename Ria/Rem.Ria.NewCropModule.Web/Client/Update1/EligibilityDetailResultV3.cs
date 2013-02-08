using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="EligibilityDetailResultV3")]
    public partial class EligibilityDetailResultV3
    {
        
        private Result resultField;
        
        private string eligibilityGuidField;
        
        private System.Collections.Generic.List<EligibilityDetailV3> eligibilityDetailArrayField;
        
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
        public virtual System.Collections.Generic.List<EligibilityDetailV3> EligibilityDetailArray
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