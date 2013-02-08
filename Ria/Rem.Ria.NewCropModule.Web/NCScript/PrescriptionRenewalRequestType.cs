namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "PrescriptionRenewalRequestType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "PrescriptionRenewalRequestType")]
    public partial class PrescriptionRenewalRequestType
    {

        private string pharmacyIdentifierField;

        private string drugNDCField;

        private string drugField;

        private string dispenseNumberField;

        private string dosageField;

        private string lastFillDateField;

        private string writtenDateField;

        private string daysSupplyField;

        private DrugSubstitutionType substitutionField;

        private string refillsField;

        private string pharmacistMessageField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "pharmacyIdentifier")]
        public virtual string PharmacyIdentifier
        {
            get
            {
                return this.pharmacyIdentifierField;
            }
            set
            {
                this.pharmacyIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugNDC")]
        public virtual string DrugNDC
        {
            get
            {
                return this.drugNDCField;
            }
            set
            {
                this.drugNDCField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drug")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "dispenseNumber")]
        public virtual string DispenseNumber
        {
            get
            {
                return this.dispenseNumberField;
            }
            set
            {
                this.dispenseNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "dosage")]
        public virtual string Dosage
        {
            get
            {
                return this.dosageField;
            }
            set
            {
                this.dosageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "lastFillDate")]
        public virtual string LastFillDate
        {
            get
            {
                return this.lastFillDateField;
            }
            set
            {
                this.lastFillDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "writtenDate")]
        public virtual string WrittenDate
        {
            get
            {
                return this.writtenDateField;
            }
            set
            {
                this.writtenDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "daysSupply")]
        public virtual string DaysSupply
        {
            get
            {
                return this.daysSupplyField;
            }
            set
            {
                this.daysSupplyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "substitution")]
        public virtual DrugSubstitutionType Substitution
        {
            get
            {
                return this.substitutionField;
            }
            set
            {
                this.substitutionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "refills")]
        public virtual string Refills
        {
            get
            {
                return this.refillsField;
            }
            set
            {
                this.refillsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "pharmacistMessage")]
        public virtual string PharmacistMessage
        {
            get
            {
                return this.pharmacistMessageField;
            }
            set
            {
                this.pharmacistMessageField = value;
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