namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://secure.newcropaccounts.com/interfaceV7", TypeName="PatientCharacteristicsType")]
    public partial class PatientCharacteristicsType
    {
        
        private string dobField;
        
        private GenderType genderField;
        
        private bool genderFieldSpecified;
        
        private string heightField;
        
        private string heightUnitsField;
        
        private string weightField;
        
        private string weightUnitsField;
        
        private LanguageType languageField;
        
        private bool languageFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="dob")]
        public virtual string Dob
        {
            get
            {
                return this.dobField;
            }
            set
            {
                this.dobField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="gender")]
        public virtual GenderType Gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool GenderSpecified
        {
            get
            {
                return this.genderFieldSpecified;
            }
            set
            {
                this.genderFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="height")]
        public virtual string Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="heightUnits")]
        public virtual string HeightUnits
        {
            get
            {
                return this.heightUnitsField;
            }
            set
            {
                this.heightUnitsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="weight")]
        public virtual string Weight
        {
            get
            {
                return this.weightField;
            }
            set
            {
                this.weightField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="weightUnits")]
        public virtual string WeightUnits
        {
            get
            {
                return this.weightUnitsField;
            }
            set
            {
                this.weightUnitsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="language")]
        public virtual LanguageType Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool LanguageSpecified
        {
            get
            {
                return this.languageFieldSpecified;
            }
            set
            {
                this.languageFieldSpecified = value;
            }
        }
    }
}