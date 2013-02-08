namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://secure.newcropaccounts.com/interfaceV7", TypeName="AccountType")]
    public partial class AccountType
    {
        
        private string accountNameField;
        
        private string siteIDField;
        
        private AddressType accountAddressField;
        
        private string accountPrimaryPhoneNumberField;
        
        private string accountPrimaryFaxNumberField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="accountName")]
        public virtual string AccountName
        {
            get
            {
                return this.accountNameField;
            }
            set
            {
                this.accountNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="siteID")]
        public virtual string SiteID
        {
            get
            {
                return this.siteIDField;
            }
            set
            {
                this.siteIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="AccountAddress")]
        public virtual AddressType AccountAddress
        {
            get
            {
                return this.accountAddressField;
            }
            set
            {
                this.accountAddressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="accountPrimaryPhoneNumber")]
        public virtual string AccountPrimaryPhoneNumber
        {
            get
            {
                return this.accountPrimaryPhoneNumberField;
            }
            set
            {
                this.accountPrimaryPhoneNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="accountPrimaryFaxNumber")]
        public virtual string AccountPrimaryFaxNumber
        {
            get
            {
                return this.accountPrimaryFaxNumberField;
            }
            set
            {
                this.accountPrimaryFaxNumberField = value;
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