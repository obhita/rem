namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://secure.newcropaccounts.com/interfaceV7", TypeName="PatientHealthplanType")]
    public partial class PatientHealthplanType
    {
        
        private string healthplanIDField;
        
        private HealthplanType healthplanTypeIDField;
        
        private string groupField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="healthplanID")]
        public virtual string HealthplanID
        {
            get
            {
                return this.healthplanIDField;
            }
            set
            {
                this.healthplanIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="healthplanTypeID")]
        public virtual HealthplanType HealthplanTypeID
        {
            get
            {
                return this.healthplanTypeIDField;
            }
            set
            {
                this.healthplanTypeIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="group")]
        public virtual string Group
        {
            get
            {
                return this.groupField;
            }
            set
            {
                this.groupField = value;
            }
        }
    }
}