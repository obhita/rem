namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://secure.newcropaccounts.com/interfaceV7", TypeName="PersonNameType")]
    public partial class PersonNameType
    {
        
        private string lastField;
        
        private string firstField;
        
        private string middleField;
        
        private PersonNamePrefix prefixField;
        
        private bool prefixFieldSpecified;
        
        private PersonNameSuffix suffixField;
        
        private bool suffixFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="last")]
        public virtual string Last
        {
            get
            {
                return this.lastField;
            }
            set
            {
                this.lastField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="first")]
        public virtual string First
        {
            get
            {
                return this.firstField;
            }
            set
            {
                this.firstField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="middle")]
        public virtual string Middle
        {
            get
            {
                return this.middleField;
            }
            set
            {
                this.middleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="prefix")]
        public virtual PersonNamePrefix Prefix
        {
            get
            {
                return this.prefixField;
            }
            set
            {
                this.prefixField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrefixSpecified
        {
            get
            {
                return this.prefixFieldSpecified;
            }
            set
            {
                this.prefixFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="suffix")]
        public virtual PersonNameSuffix Suffix
        {
            get
            {
                return this.suffixField;
            }
            set
            {
                this.suffixField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool SuffixSpecified
        {
            get
            {
                return this.suffixFieldSpecified;
            }
            set
            {
                this.suffixFieldSpecified = value;
            }
        }
    }
}