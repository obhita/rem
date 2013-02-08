namespace Rem.Ria.NewCropModule.Web.Client.Patient
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="PatientDrugNameDetail")]
    public partial class PatientDrugNameDetail
    {
        
        private string drugIDField;
        
        private string drugTypeIDField;
        
        private string drugNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DrugID")]
        public virtual string DrugID
        {
            get
            {
                return this.drugIDField;
            }
            set
            {
                this.drugIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DrugTypeID")]
        public virtual string DrugTypeID
        {
            get
            {
                return this.drugTypeIDField;
            }
            set
            {
                this.drugTypeIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DrugName")]
        public virtual string DrugName
        {
            get
            {
                return this.drugNameField;
            }
            set
            {
                this.drugNameField = value;
            }
        }
    }
}