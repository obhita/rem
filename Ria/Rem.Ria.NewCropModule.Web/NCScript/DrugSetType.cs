namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "DrugSetType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "DrugSetType")]
    public partial class DrugSetType
    {

        private string drugSetNameField;

        private DrugSetType1 drugSetTypeField;

        private DrugSetStatusType drugSetStatusField;

        private System.Collections.Generic.List<NewPrescriptionType> drugField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugSetName")]
        public virtual string DrugSetName
        {
            get
            {
                return this.drugSetNameField;
            }
            set
            {
                this.drugSetNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugSetType")]
        public virtual DrugSetType1 DrugSetType1
        {
            get
            {
                return this.drugSetTypeField;
            }
            set
            {
                this.drugSetTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugSetStatus")]
        public virtual DrugSetStatusType DrugSetStatus
        {
            get
            {
                return this.drugSetStatusField;
            }
            set
            {
                this.drugSetStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("drug")]
        public virtual System.Collections.Generic.List<NewPrescriptionType> Drug
        {
            get
            {
                return this.drugField;
            }
            set
            {
                this.drugField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "ID")]
        public virtual string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
}