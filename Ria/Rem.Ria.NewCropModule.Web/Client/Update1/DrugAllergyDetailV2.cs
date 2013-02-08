namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="DrugAllergyDetailV2")]
    public partial class DrugAllergyDetailV2
    {
        
        private string interactionTextField;
        
        private string drugField;
        
        private string drugIDField;
        
        private string drugTypeField;
        
        private string compositeAllergyIdField;
        
        private string sourceField;
        
        private string conceptIdField;
        
        private string conceptTypeField;
        
        private string descriptionField;
        
        private string performanceField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="InteractionText")]
        public virtual string InteractionText
        {
            get
            {
                return this.interactionTextField;
            }
            set
            {
                this.interactionTextField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Drug")]
        public virtual string Drug
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DrugType")]
        public virtual string DrugType
        {
            get
            {
                return this.drugTypeField;
            }
            set
            {
                this.drugTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="CompositeAllergyId")]
        public virtual string CompositeAllergyId
        {
            get
            {
                return this.compositeAllergyIdField;
            }
            set
            {
                this.compositeAllergyIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Source")]
        public virtual string Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ConceptId")]
        public virtual string ConceptId
        {
            get
            {
                return this.conceptIdField;
            }
            set
            {
                this.conceptIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ConceptType")]
        public virtual string ConceptType
        {
            get
            {
                return this.conceptTypeField;
            }
            set
            {
                this.conceptTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Description")]
        public virtual string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Performance")]
        public virtual string Performance
        {
            get
            {
                return this.performanceField;
            }
            set
            {
                this.performanceField = value;
            }
        }
    }
}