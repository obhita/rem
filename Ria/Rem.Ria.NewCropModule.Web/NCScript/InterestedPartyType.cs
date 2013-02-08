namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "InterestedPartyType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "InterestedPartyType")]
    public partial class InterestedPartyType
    {

        private PersonNameType interestedPartyNameField;

        private AddressOptionalType interestedPartyAddressField;

        private ContactType interestedPartyContactField;

        private string deaField;

        private string upinField;

        private string licenseStateField;

        private string licenseNumberField;

        private string npiField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "InterestedPartyName")]
        public virtual PersonNameType InterestedPartyName
        {
            get
            {
                return this.interestedPartyNameField;
            }
            set
            {
                this.interestedPartyNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "InterestedPartyAddress")]
        public virtual AddressOptionalType InterestedPartyAddress
        {
            get
            {
                return this.interestedPartyAddressField;
            }
            set
            {
                this.interestedPartyAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "InterestedPartyContact")]
        public virtual ContactType InterestedPartyContact
        {
            get
            {
                return this.interestedPartyContactField;
            }
            set
            {
                this.interestedPartyContactField = value;
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