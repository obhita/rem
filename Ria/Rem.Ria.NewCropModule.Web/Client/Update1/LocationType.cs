namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://secure.newcropaccounts.com/interfaceV7", TypeName="LocationType")]
    public partial class LocationType
    {
        
        private string locationNameField;
        
        private string locationShortNameField;
        
        private AddressType locationAddressField;
        
        private string primaryPhoneNumberField;
        
        private string primaryFaxNumberField;
        
        private string pharmacyContactNumberField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="locationName")]
        public virtual string LocationName
        {
            get
            {
                return this.locationNameField;
            }
            set
            {
                this.locationNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="locationShortName")]
        public virtual string LocationShortName
        {
            get
            {
                return this.locationShortNameField;
            }
            set
            {
                this.locationShortNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="LocationAddress")]
        public virtual AddressType LocationAddress
        {
            get
            {
                return this.locationAddressField;
            }
            set
            {
                this.locationAddressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="primaryPhoneNumber")]
        public virtual string PrimaryPhoneNumber
        {
            get
            {
                return this.primaryPhoneNumberField;
            }
            set
            {
                this.primaryPhoneNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="primaryFaxNumber")]
        public virtual string PrimaryFaxNumber
        {
            get
            {
                return this.primaryFaxNumberField;
            }
            set
            {
                this.primaryFaxNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="pharmacyContactNumber")]
        public virtual string PharmacyContactNumber
        {
            get
            {
                return this.pharmacyContactNumberField;
            }
            set
            {
                this.pharmacyContactNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName="ID")]
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