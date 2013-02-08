using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="DrugFormularyDetailResult")]
    public partial class DrugFormularyDetailResult
    {
        
        private Result resultField;
        
        private System.Collections.Generic.List<DrugFormularyDetail> drugFormularyDetailArrayField;
        
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
        [System.Xml.Serialization.XmlArrayAttribute(ElementName="drugFormularyDetailArray")]
        public virtual System.Collections.Generic.List<DrugFormularyDetail> DrugFormularyDetailArray
        {
            get
            {
                return this.drugFormularyDetailArrayField;
            }
            set
            {
                this.drugFormularyDetailArrayField = value;
            }
        }
    }
}