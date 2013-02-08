namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "PatientHealthplanFreeformType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "PatientHealthplanFreeformType")]
    public partial class PatientHealthplanFreeformType
    {

        private string healthplanNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "healthplanName")]
        public virtual string HealthplanName
        {
            get
            {
                return this.healthplanNameField;
            }
            set
            {
                this.healthplanNameField = value;
            }
        }
    }
}