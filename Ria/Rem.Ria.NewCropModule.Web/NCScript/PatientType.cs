namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "PatientType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "PatientType")]
    public partial class PatientType
    {

        private PersonNameType patientNameField;

        private string medicalRecordNumberField;

        private string socialSecurityNumberField;

        private string memoField;

        private AddressOptionalType patientAddressField;

        private ContactType patientContactField;

        private PatientCharacteristicsType patientCharacteristicsField;

        private System.Collections.Generic.List<PatientAllergyType> patientAllergiesField;

        private System.Collections.Generic.List<PatientHealthplanType> patientHealthplansField;

        private System.Collections.Generic.List<PatientDiagnosisType> patientDiagnosisField;

        private string patientDiagnosisSearchField;

        private System.Collections.Generic.List<PatientIdentifierType> patientIdentifierField;

        private System.Collections.Generic.List<PatientHealthplanFreeformType> patientFreeformHealthplansField;

        private string episodeIdentifierField;

        private string encounterIdentifierField;

        private System.Collections.Generic.List<PatientAllergyFreeformType> patientFreeformAllergyField;

        private PatientFormularyType patientFormularyField;

        private System.Collections.Generic.List<PatientPharmacyType> patientPharmaciesField;

        private string drugSetIDField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PatientName")]
        public virtual PersonNameType PatientName
        {
            get
            {
                return this.patientNameField;
            }
            set
            {
                this.patientNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "medicalRecordNumber")]
        public virtual string MedicalRecordNumber
        {
            get
            {
                return this.medicalRecordNumberField;
            }
            set
            {
                this.medicalRecordNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "socialSecurityNumber")]
        public virtual string SocialSecurityNumber
        {
            get
            {
                return this.socialSecurityNumberField;
            }
            set
            {
                this.socialSecurityNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "memo")]
        public virtual string Memo
        {
            get
            {
                return this.memoField;
            }
            set
            {
                this.memoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PatientAddress")]
        public virtual AddressOptionalType PatientAddress
        {
            get
            {
                return this.patientAddressField;
            }
            set
            {
                this.patientAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PatientContact")]
        public virtual ContactType PatientContact
        {
            get
            {
                return this.patientContactField;
            }
            set
            {
                this.patientContactField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PatientCharacteristics")]
        public virtual PatientCharacteristicsType PatientCharacteristics
        {
            get
            {
                return this.patientCharacteristicsField;
            }
            set
            {
                this.patientCharacteristicsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PatientAllergies")]
        public virtual System.Collections.Generic.List<PatientAllergyType> PatientAllergies
        {
            get
            {
                return this.patientAllergiesField;
            }
            set
            {
                this.patientAllergiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PatientHealthplans")]
        public virtual System.Collections.Generic.List<PatientHealthplanType> PatientHealthplans
        {
            get
            {
                return this.patientHealthplansField;
            }
            set
            {
                this.patientHealthplansField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PatientDiagnosis")]
        public virtual System.Collections.Generic.List<PatientDiagnosisType> PatientDiagnosis
        {
            get
            {
                return this.patientDiagnosisField;
            }
            set
            {
                this.patientDiagnosisField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PatientDiagnosisSearch")]
        public virtual string PatientDiagnosisSearch
        {
            get
            {
                return this.patientDiagnosisSearchField;
            }
            set
            {
                this.patientDiagnosisSearchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PatientIdentifier")]
        public virtual System.Collections.Generic.List<PatientIdentifierType> PatientIdentifier
        {
            get
            {
                return this.patientIdentifierField;
            }
            set
            {
                this.patientIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PatientFreeformHealthplans")]
        public virtual System.Collections.Generic.List<PatientHealthplanFreeformType> PatientFreeformHealthplans
        {
            get
            {
                return this.patientFreeformHealthplansField;
            }
            set
            {
                this.patientFreeformHealthplansField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "EpisodeIdentifier")]
        public virtual string EpisodeIdentifier
        {
            get
            {
                return this.episodeIdentifierField;
            }
            set
            {
                this.episodeIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "EncounterIdentifier")]
        public virtual string EncounterIdentifier
        {
            get
            {
                return this.encounterIdentifierField;
            }
            set
            {
                this.encounterIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PatientFreeformAllergy")]
        public virtual System.Collections.Generic.List<PatientAllergyFreeformType> PatientFreeformAllergy
        {
            get
            {
                return this.patientFreeformAllergyField;
            }
            set
            {
                this.patientFreeformAllergyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PatientFormulary")]
        public virtual PatientFormularyType PatientFormulary
        {
            get
            {
                return this.patientFormularyField;
            }
            set
            {
                this.patientFormularyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PatientPharmacies")]
        public virtual System.Collections.Generic.List<PatientPharmacyType> PatientPharmacies
        {
            get
            {
                return this.patientPharmaciesField;
            }
            set
            {
                this.patientPharmaciesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "DrugSetID")]
        public virtual string DrugSetID
        {
            get
            {
                return this.drugSetIDField;
            }
            set
            {
                this.drugSetIDField = value;
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