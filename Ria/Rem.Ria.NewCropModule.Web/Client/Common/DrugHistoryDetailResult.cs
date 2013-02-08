namespace Rem.Ria.NewCropModule.Web.Client.Common
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="DrugHistoryDetailResult")]
    public partial class DrugHistoryDetailResult
    {
        
        private Result resultField;
        
        private System.Collections.Generic.List<DrugHistoryDetail> drugHistoryDetailArrayField;
        
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
        [System.Xml.Serialization.XmlArrayAttribute(ElementName="drugHistoryDetailArray")]
        public virtual System.Collections.Generic.List<DrugHistoryDetail> DrugHistoryDetailArray
        {
            get
            {
                return this.drugHistoryDetailArrayField;
            }
            set
            {
                this.drugHistoryDetailArrayField = value;
            }
        }
    }
}