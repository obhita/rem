using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="DrugAllergyDetailResultV2")]
    public partial class DrugAllergyDetailResultV2
    {
        
        private Result resultField;
        
        private System.Collections.Generic.List<DrugAllergyDetailV2> drugAllergyDetailArrayV2Field;
        
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
        [System.Xml.Serialization.XmlArrayAttribute(ElementName="drugAllergyDetailArrayV2")]
        public virtual System.Collections.Generic.List<DrugAllergyDetailV2> DrugAllergyDetailArrayV2
        {
            get
            {
                return this.drugAllergyDetailArrayV2Field;
            }
            set
            {
                this.drugAllergyDetailArrayV2Field = value;
            }
        }
    }
}