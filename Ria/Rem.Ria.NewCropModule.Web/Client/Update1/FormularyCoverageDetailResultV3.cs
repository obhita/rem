using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="FormularyCoverageDetailResultV3")]
    public partial class FormularyCoverageDetailResultV3
    {
        
        private Result resultField;
        
        private System.Collections.Generic.List<FormularyCoverageDetailV3> formularyCoverageDetailV3ArrayField;
        
        private System.Collections.Generic.List<FormularyCoverageDetailV3> formularyCoverageAlternativesDetailV3ArrayField;
        
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
        [System.Xml.Serialization.XmlArrayAttribute(ElementName="formularyCoverageDetailV3Array")]
        public virtual System.Collections.Generic.List<FormularyCoverageDetailV3> FormularyCoverageDetailV3Array
        {
            get
            {
                return this.formularyCoverageDetailV3ArrayField;
            }
            set
            {
                this.formularyCoverageDetailV3ArrayField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(ElementName="formularyCoverageAlternativesDetailV3Array")]
        public virtual System.Collections.Generic.List<FormularyCoverageDetailV3> FormularyCoverageAlternativesDetailV3Array
        {
            get
            {
                return this.formularyCoverageAlternativesDetailV3ArrayField;
            }
            set
            {
                this.formularyCoverageAlternativesDetailV3ArrayField = value;
            }
        }
    }
}