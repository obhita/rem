namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "NewPrescriptionType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "NewPrescriptionType")]
    public partial class NewPrescriptionType
    {

        private string pharmacyIdentifierField;

        private string drugIdentifierField;

        private DrugDatabaseType drugIdentifierTypeField;

        private string dispenseNumberField;

        private string dosageField;

        private string refillCountField;

        private DrugSubstitutionType substitutionField;

        private string pharmacistMessageField;

        private ExternalDrugOverrideType externalOverrideDrugField;

        private string renewalRequestIdentifierField;

        private CodifiedSigType codifiedSigTypeField;

        private DrugTakeAsNeededType prnField;

        private bool prnFieldSpecified;

        private string dispenseNumberQualifierField;

        private string daysSupplyField;

        private PrescriptionAuditDeliveryType prescriptionAuditDeliveryField;

        private bool prescriptionAuditDeliveryFieldSpecified;

        private string refillQuantityQualifierField;

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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalOverrideDrug")]
        public virtual ExternalDrugOverrideType ExternalOverrideDrug
        {
            get
            {
                return this.externalOverrideDrugField;
            }
            set
            {
                this.externalOverrideDrugField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "renewalRequestIdentifier")]
        public virtual string RenewalRequestIdentifier
        {
            get
            {
                return this.renewalRequestIdentifierField;
            }
            set
            {
                this.renewalRequestIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "codifiedSigType")]
        public virtual CodifiedSigType CodifiedSigType
        {
            get
            {
                return this.codifiedSigTypeField;
            }
            set
            {
                this.codifiedSigTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prn")]
        public virtual DrugTakeAsNeededType Prn
        {
            get
            {
                return this.prnField;
            }
            set
            {
                this.prnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrnSpecified
        {
            get
            {
                return this.prnFieldSpecified;
            }
            set
            {
                this.prnFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "dispenseNumberQualifier")]
        public virtual string DispenseNumberQualifier
        {
            get
            {
                return this.dispenseNumberQualifierField;
            }
            set
            {
                this.dispenseNumberQualifierField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriptionAuditDelivery")]
        public virtual PrescriptionAuditDeliveryType PrescriptionAuditDelivery
        {
            get
            {
                return this.prescriptionAuditDeliveryField;
            }
            set
            {
                this.prescriptionAuditDeliveryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrescriptionAuditDeliverySpecified
        {
            get
            {
                return this.prescriptionAuditDeliveryFieldSpecified;
            }
            set
            {
                this.prescriptionAuditDeliveryFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "refillQuantityQualifier")]
        public virtual string RefillQuantityQualifier
        {
            get
            {
                return this.refillQuantityQualifierField;
            }
            set
            {
                this.refillQuantityQualifierField = value;
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