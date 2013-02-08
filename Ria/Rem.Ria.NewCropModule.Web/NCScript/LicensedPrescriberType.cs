namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "LicensedPrescriberType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "LicensedPrescriberType")]
    public partial class LicensedPrescriberType
    {

        private PersonNameType licensedPrescriberNameField;

        private string deaField;

        private PrescriberStatus prescriberStatusField;

        private bool prescriberStatusFieldSpecified;

        private string upinField;

        private string licenseStateField;

        private string licenseNumberField;

        private PrescriberNetwork prescriberNetworkField;

        private bool prescriberNetworkFieldSpecified;

        private string prescriberStartDateTimeField;

        private string prescriberStopDateTimeField;

        private string npiField;

        private string freeformCredentialsField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "LicensedPrescriberName")]
        public virtual PersonNameType LicensedPrescriberName
        {
            get
            {
                return this.licensedPrescriberNameField;
            }
            set
            {
                this.licensedPrescriberNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "dea")]
        public virtual string Dea
        {
            get
            {
                return this.deaField;
            }
            set
            {
                this.deaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriberStatus")]
        public virtual PrescriberStatus PrescriberStatus
        {
            get
            {
                return this.prescriberStatusField;
            }
            set
            {
                this.prescriberStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrescriberStatusSpecified
        {
            get
            {
                return this.prescriberStatusFieldSpecified;
            }
            set
            {
                this.prescriberStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "upin")]
        public virtual string Upin
        {
            get
            {
                return this.upinField;
            }
            set
            {
                this.upinField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "licenseState")]
        public virtual string LicenseState
        {
            get
            {
                return this.licenseStateField;
            }
            set
            {
                this.licenseStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "licenseNumber")]
        public virtual string LicenseNumber
        {
            get
            {
                return this.licenseNumberField;
            }
            set
            {
                this.licenseNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriberNetwork")]
        public virtual PrescriberNetwork PrescriberNetwork
        {
            get
            {
                return this.prescriberNetworkField;
            }
            set
            {
                this.prescriberNetworkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrescriberNetworkSpecified
        {
            get
            {
                return this.prescriberNetworkFieldSpecified;
            }
            set
            {
                this.prescriberNetworkFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriberStartDateTime")]
        public virtual string PrescriberStartDateTime
        {
            get
            {
                return this.prescriberStartDateTimeField;
            }
            set
            {
                this.prescriberStartDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriberStopDateTime")]
        public virtual string PrescriberStopDateTime
        {
            get
            {
                return this.prescriberStopDateTimeField;
            }
            set
            {
                this.prescriberStopDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "npi")]
        public virtual string Npi
        {
            get
            {
                return this.npiField;
            }
            set
            {
                this.npiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "freeformCredentials")]
        public virtual string FreeformCredentials
        {
            get
            {
                return this.freeformCredentialsField;
            }
            set
            {
                this.freeformCredentialsField = value;
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