namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "OutsidePrescriptionType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "OutsidePrescriptionType")]
    public partial class OutsidePrescriptionType
    {

        private string externalIdField;

        private string pharmacyIdentifierField;

        private string pharmacyPhoneField;

        private string pharmacyFaxField;

        private string dateField;

        private string doctorNameField;

        private string drugField;

        private string dosageField;

        private string dispenseNumberField;

        private string sigField;

        private string refillCountField;

        private DrugSubstitutionType substitutionField;

        private bool substitutionFieldSpecified;

        private string pharmacistMessageField;

        private string drugIdentifierField;

        private DrugDatabaseType drugIdentifierTypeField;

        private bool drugIdentifierTypeFieldSpecified;

        private string prescriptionTypeField;

        private ExternalDrugOverrideType externalOverrideDrugField;

        private string renewalRequestIdentifierField;

        private CodifiedSigType codifiedSigTypeField;

        private DrugTakeAsNeededType prnField;

        private bool prnFieldSpecified;

        private PrescriptionStatusType prescriptionStatusField;

        private bool prescriptionStatusFieldSpecified;

        private PrescriptionSubStatusType prescriptionSubStatusField;

        private bool prescriptionSubStatusFieldSpecified;

        private PrescriptionArchiveType prescriptionArchiveStatusField;

        private bool prescriptionArchiveStatusFieldSpecified;

        private string dispenseNumberQualifierField;

        private string daysSupplyField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalId")]
        public virtual string ExternalId
        {
            get
            {
                return this.externalIdField;
            }
            set
            {
                this.externalIdField = value;
            }
        }

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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "pharmacyPhone")]
        public virtual string PharmacyPhone
        {
            get
            {
                return this.pharmacyPhoneField;
            }
            set
            {
                this.pharmacyPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "pharmacyFax")]
        public virtual string PharmacyFax
        {
            get
            {
                return this.pharmacyFaxField;
            }
            set
            {
                this.pharmacyFaxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "date")]
        public virtual string Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "doctorName")]
        public virtual string DoctorName
        {
            get
            {
                return this.doctorNameField;
            }
            set
            {
                this.doctorNameField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "sig")]
        public virtual string Sig
        {
            get
            {
                return this.sigField;
            }
            set
            {
                this.sigField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriptionType")]
        public virtual string PrescriptionType
        {
            get
            {
                return this.prescriptionTypeField;
            }
            set
            {
                this.prescriptionTypeField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriptionStatus")]
        public virtual PrescriptionStatusType PrescriptionStatus
        {
            get
            {
                return this.prescriptionStatusField;
            }
            set
            {
                this.prescriptionStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrescriptionStatusSpecified
        {
            get
            {
                return this.prescriptionStatusFieldSpecified;
            }
            set
            {
                this.prescriptionStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriptionSubStatus")]
        public virtual PrescriptionSubStatusType PrescriptionSubStatus
        {
            get
            {
                return this.prescriptionSubStatusField;
            }
            set
            {
                this.prescriptionSubStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrescriptionSubStatusSpecified
        {
            get
            {
                return this.prescriptionSubStatusFieldSpecified;
            }
            set
            {
                this.prescriptionSubStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "prescriptionArchiveStatus")]
        public virtual PrescriptionArchiveType PrescriptionArchiveStatus
        {
            get
            {
                return this.prescriptionArchiveStatusField;
            }
            set
            {
                this.prescriptionArchiveStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrescriptionArchiveStatusSpecified
        {
            get
            {
                return this.prescriptionArchiveStatusFieldSpecified;
            }
            set
            {
                this.prescriptionArchiveStatusFieldSpecified = value;
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