namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "CurrentFeatureType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "CurrentFeatureType")]
    public partial class CurrentFeatureType
    {

        private FeatureType featureField;

        private FeatureStatusType featureStatusField;

        private FeatureExpirationType featureExpirationField;

        private bool featureExpirationFieldSpecified;

        private string featureExpirationDateField;

        private string featureClientSpecified1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "feature")]
        public virtual FeatureType Feature
        {
            get
            {
                return this.featureField;
            }
            set
            {
                this.featureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "featureStatus")]
        public virtual FeatureStatusType FeatureStatus
        {
            get
            {
                return this.featureStatusField;
            }
            set
            {
                this.featureStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "featureExpiration")]
        public virtual FeatureExpirationType FeatureExpiration
        {
            get
            {
                return this.featureExpirationField;
            }
            set
            {
                this.featureExpirationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool FeatureExpirationSpecified
        {
            get
            {
                return this.featureExpirationFieldSpecified;
            }
            set
            {
                this.featureExpirationFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "featureExpirationDate")]
        public virtual string FeatureExpirationDate
        {
            get
            {
                return this.featureExpirationDateField;
            }
            set
            {
                this.featureExpirationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("featureClientSpecified1")]
        public virtual string FeatureClientSpecified1
        {
            get
            {
                return this.featureClientSpecified1Field;
            }
            set
            {
                this.featureClientSpecified1Field = value;
            }
        }
    }
}