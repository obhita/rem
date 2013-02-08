namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="DrugDiseaseDetail")]
    public partial class DrugDiseaseDetail
    {
        
        private string drugNameField;
        
        private string iCD9Field;
        
        private string directConditionField;
        
        private string relatedConditionField;
        
        private string severityLevelField;
        
        private string severityLevelTextField;
        
        private string severityLevelShortTextField;
        
        private string diseaseRelationField;
        
        private string diseaseRelationTextField;
        
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="ICD9")]
        public virtual string ICD9
        {
            get
            {
                return this.iCD9Field;
            }
            set
            {
                this.iCD9Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DirectCondition")]
        public virtual string DirectCondition
        {
            get
            {
                return this.directConditionField;
            }
            set
            {
                this.directConditionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="RelatedCondition")]
        public virtual string RelatedCondition
        {
            get
            {
                return this.relatedConditionField;
            }
            set
            {
                this.relatedConditionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SeverityLevel")]
        public virtual string SeverityLevel
        {
            get
            {
                return this.severityLevelField;
            }
            set
            {
                this.severityLevelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SeverityLevelText")]
        public virtual string SeverityLevelText
        {
            get
            {
                return this.severityLevelTextField;
            }
            set
            {
                this.severityLevelTextField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SeverityLevelShortText")]
        public virtual string SeverityLevelShortText
        {
            get
            {
                return this.severityLevelShortTextField;
            }
            set
            {
                this.severityLevelShortTextField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DiseaseRelation")]
        public virtual string DiseaseRelation
        {
            get
            {
                return this.diseaseRelationField;
            }
            set
            {
                this.diseaseRelationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DiseaseRelationText")]
        public virtual string DiseaseRelationText
        {
            get
            {
                return this.diseaseRelationTextField;
            }
            set
            {
                this.diseaseRelationTextField = value;
            }
        }
    }
}