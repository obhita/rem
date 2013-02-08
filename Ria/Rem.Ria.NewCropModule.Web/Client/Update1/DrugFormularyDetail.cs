namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="DrugFormularyDetail")]
    public partial class DrugFormularyDetail
    {
        
        private DrugDetail drugDetailField;
        
        private string formularyCoverageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="drugDetail")]
        public virtual DrugDetail DrugDetail
        {
            get
            {
                return this.drugDetailField;
            }
            set
            {
                this.drugDetailField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="formularyCoverage")]
        public virtual string FormularyCoverage
        {
            get
            {
                return this.formularyCoverageField;
            }
            set
            {
                this.formularyCoverageField = value;
            }
        }
    }
}