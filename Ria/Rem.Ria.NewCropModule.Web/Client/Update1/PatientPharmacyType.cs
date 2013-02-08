namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://secure.newcropaccounts.com/interfaceV7", TypeName="PatientPharmacyType")]
    public partial class PatientPharmacyType
    {
        
        private string pharmacyIdentifierField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="pharmacyIdentifier")]
        public virtual string PharmacyIdentifier
        {
            get
            {
                return this.pharmacyIdentifierField;
            }
            set
            {
                this.pharmacyIdentifierField = value;
            }
        }
    }
}