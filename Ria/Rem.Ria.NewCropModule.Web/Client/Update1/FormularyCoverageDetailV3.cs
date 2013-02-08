namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="FormularyCoverageDetailV3")]
    public partial class FormularyCoverageDetailV3
    {
        
        private string drugConceptField;
        
        private string formularyStatusField;
        
        private string formularyStatusDescField;
        
        private string therapeuticClassDescField;
        
        private string therapeuticSubClassDescField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="drugConcept")]
        public virtual string DrugConcept
        {
            get
            {
                return this.drugConceptField;
            }
            set
            {
                this.drugConceptField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="formularyStatus")]
        public virtual string FormularyStatus
        {
            get
            {
                return this.formularyStatusField;
            }
            set
            {
                this.formularyStatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="formularyStatusDesc")]
        public virtual string FormularyStatusDesc
        {
            get
            {
                return this.formularyStatusDescField;
            }
            set
            {
                this.formularyStatusDescField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="therapeuticClassDesc")]
        public virtual string TherapeuticClassDesc
        {
            get
            {
                return this.therapeuticClassDescField;
            }
            set
            {
                this.therapeuticClassDescField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="therapeuticSubClassDesc")]
        public virtual string TherapeuticSubClassDesc
        {
            get
            {
                return this.therapeuticSubClassDescField;
            }
            set
            {
                this.therapeuticSubClassDescField = value;
            }
        }
    }
}