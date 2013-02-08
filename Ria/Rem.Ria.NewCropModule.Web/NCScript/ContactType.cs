namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "ContactType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "ContactType")]
    public partial class ContactType
    {

        private string homeTelephoneField;

        private string workTelephoneField;

        private string cellularTelephoneField;

        private string pagerTelephoneField;

        private string faxField;

        private string emailField;

        private string backOfficeTelephoneField;

        private string backOfficeFaxField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "homeTelephone")]
        public virtual string HomeTelephone
        {
            get
            {
                return this.homeTelephoneField;
            }
            set
            {
                this.homeTelephoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "workTelephone")]
        public virtual string WorkTelephone
        {
            get
            {
                return this.workTelephoneField;
            }
            set
            {
                this.workTelephoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "cellularTelephone")]
        public virtual string CellularTelephone
        {
            get
            {
                return this.cellularTelephoneField;
            }
            set
            {
                this.cellularTelephoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "pagerTelephone")]
        public virtual string PagerTelephone
        {
            get
            {
                return this.pagerTelephoneField;
            }
            set
            {
                this.pagerTelephoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "fax")]
        public virtual string Fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "email")]
        public virtual string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "backOfficeTelephone")]
        public virtual string BackOfficeTelephone
        {
            get
            {
                return this.backOfficeTelephoneField;
            }
            set
            {
                this.backOfficeTelephoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "backOfficeFax")]
        public virtual string BackOfficeFax
        {
            get
            {
                return this.backOfficeFaxField;
            }
            set
            {
                this.backOfficeFaxField = value;
            }
        }
    }
}