namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "NewPrescriptionWithImagesType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "NewPrescriptionWithImagesType")]
    public partial class NewPrescriptionWithImagesType
    {

        private string pharmacyIdentifierField;

        private string drugNameField;

        private string drugStrengthField;

        private string drugStrengthUOMField;

        private string drugRouteField;

        private string drugFormField;

        private string drugIdentifierField;

        private DrugDatabaseType drugIdentifierTypeField;

        private bool drugIdentifierTypeFieldSpecified;

        private string dispenseNumberField;

        private string dosageField;

        private string refillCountField;

        private DrugSubstitutionType substitutionField;

        private bool substitutionFieldSpecified;

        private string pharmacistMessageField;

        private System.Collections.Generic.List<NewPrescriptionImageType> imagesField;

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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugName")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugStrength")]
        public virtual string DrugStrength
        {
            get
            {
                return this.drugStrengthField;
            }
            set
            {
                this.drugStrengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugStrengthUOM")]
        public virtual string DrugStrengthUOM
        {
            get
            {
                return this.drugStrengthUOMField;
            }
            set
            {
                this.drugStrengthUOMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugRoute")]
        public virtual string DrugRoute
        {
            get
            {
                return this.drugRouteField;
            }
            set
            {
                this.drugRouteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugForm")]
        public virtual string DrugForm
        {
            get
            {
                return this.drugFormField;
            }
            set
            {
                this.drugFormField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugIdentifier")]
        public virtual string DrugIdentifier
        {
            get
            {
                return this.drugIdentifierField;
            }
            set
            {
                this.drugIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugIdentifierType")]
        public virtual DrugDatabaseType DrugIdentifierType
        {
            get
            {
                return this.drugIdentifierTypeField;
            }
            set
            {
                this.drugIdentifierTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool DrugIdentifierTypeSpecified
        {
            get
            {
                return this.drugIdentifierTypeFieldSpecified;
            }
            set
            {
                this.drugIdentifierTypeFieldSpecified = value;
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "refillCount")]
        public virtual string RefillCount
        {
            get
            {
                return this.refillCountField;
            }
            set
            {
                this.refillCountField = value;
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool SubstitutionSpecified
        {
            get
            {
                return this.substitutionFieldSpecified;
            }
            set
            {
                this.substitutionFieldSpecified = value;
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
        [System.Xml.Serialization.XmlElementAttribute("images")]
        public virtual System.Collections.Generic.List<NewPrescriptionImageType> Images
        {
            get
            {
                return this.imagesField;
            }
            set
            {
                this.imagesField = value;
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