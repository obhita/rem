namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, TypeName = "NewDataSetTable")]
    public partial class MedicationTable
    {

        private string accountGuidField;

        private string accountNameField;

        private string externalAccountIDField;

        private string siteIDField;

        private string patientGUIDField;

        private string fullNameField;

        private string externalPatientIDField;

        private System.DateTime prescriptionDateField;

        private bool prescriptionDateFieldSpecified;

        private string drugIDField;

        private string drugTypeIDField;

        private string drugNameField;

        private string drugInfoField;

        private string strengthField;

        private string strengthUOMField;

        private string dosageNumberDescriptionField;

        private string dosageFormField;

        private string routeField;

        private string dosageFrequencyDescriptionField;

        private string dispenseField;

        private string takeAsNeededField;

        private string dispenseAsWrittenField;

        private byte refillsField;

        private bool refillsFieldSpecified;

        private string statusField;

        private string subStatusField;

        private string archiveField;

        private string prescriptionGuidField;

        private string orderGUIDField;

        private string prescriptionNotesField;

        private string pharmacistNotesField;

        private string externalPhysicianIDField;

        private string physicianNameField;

        private string dateMovedToPreviousMedicationsField;

        private string formularyTypeField;

        private string formularyTypeIDField;

        private string formularyMemberField;

        private string formularyIdField;

        private string formularyStatusField;

        private bool modifiedSigField;

        private bool modifiedSigFieldSpecified;

        private string modifiedSigStatusField;

        private string externalPrescriptionIDField;

        private string episodeIdentifierField;

        private string encounterIdentifierField;

        private string externalSourceField;

        private string externalDrugConceptField;

        private string externalDrugNameField;

        private string externalDrugStrengthField;

        private string externalDrugStrengthUOMField;

        private string externalDrugStrengthWithUOMField;

        private string externalDrugDosageFormField;

        private string externalDrugRouteField;

        private string externalDrugIdentifierField;

        private string externalDrugIdentifierTypeField;

        private string externalDrugScheduleField;

        private string externalDrugOTCField;

        private short dosageNumberTypeIDField;

        private bool dosageNumberTypeIDFieldSpecified;

        private byte dosageFormTypeIdField;

        private bool dosageFormTypeIdFieldSpecified;

        private byte dosageRouteTypeIdField;

        private bool dosageRouteTypeIdFieldSpecified;

        private short dosageFrequencyTypeIDField;

        private bool dosageFrequencyTypeIDFieldSpecified;

        private int daysSupplyField;

        private bool daysSupplyFieldSpecified;

        private System.DateTime prescriptionTimestampField;

        private bool prescriptionTimestampFieldSpecified;

        private string originalPrescriptionGuidField;

        private string externalUserIDField;

        private string externalUserTypeField;

        private string deaGenericNamedCodeField;

        private string diagnosisField;

        private string diagnosisSourceField;

        private string diagnosisNameField;

        private string dispenseNumberQualifierField;

        private string dispenseNumberQualifierDescriptionField;

        private string locationNameField;

        private string genericNameField;

        private string patientFriendlySIGField;

        private string printLeafletField;

        private string deaClassCodeField;

        private int pharmacyTypeField;

        private bool pharmacyTypeFieldSpecified;

        private byte pharmacyDetailTypeField;

        private bool pharmacyDetailTypeFieldSpecified;

        private byte finalDestinationTypeField;

        private bool finalDestinationTypeFieldSpecified;

        private byte finalStatusTypeField;

        private bool finalStatusTypeFieldSpecified;

        private string pharmacyNCPDPField;

        private string pharmacyFullInfoField;

        private string sourcePrescriptionGuidField;

        private string patientIDField;

        private string patientIDTypeField;

        private decimal rxcuiField;

        private bool rxcuiFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AccountGuid")]
        public virtual string AccountGuid
        {
            get
            {
                return this.accountGuidField;
            }
            set
            {
                this.accountGuidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AccountName")]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalAccountID")]
        public virtual string ExternalAccountID
        {
            get
            {
                return this.externalAccountIDField;
            }
            set
            {
                this.externalAccountIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "SiteID")]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PatientGUID")]
        public virtual string PatientGUID
        {
            get
            {
                return this.patientGUIDField;
            }
            set
            {
                this.patientGUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "FullName")]
        public virtual string FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalPatientID")]
        public virtual string ExternalPatientID
        {
            get
            {
                return this.externalPatientIDField;
            }
            set
            {
                this.externalPatientIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PrescriptionDate")]
        public virtual System.DateTime PrescriptionDate
        {
            get
            {
                return this.prescriptionDateField;
            }
            set
            {
                this.prescriptionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrescriptionDateSpecified
        {
            get
            {
                return this.prescriptionDateFieldSpecified;
            }
            set
            {
                this.prescriptionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DrugID")]
        public virtual string DrugID
        {
            get
            {
                return this.drugIDField;
            }
            set
            {
                this.drugIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DrugTypeID")]
        public virtual string DrugTypeID
        {
            get
            {
                return this.drugTypeIDField;
            }
            set
            {
                this.drugTypeIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DrugName")]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DrugInfo")]
        public virtual string DrugInfo
        {
            get
            {
                return this.drugInfoField;
            }
            set
            {
                this.drugInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Strength")]
        public virtual string Strength
        {
            get
            {
                return this.strengthField;
            }
            set
            {
                this.strengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "StrengthUOM")]
        public virtual string StrengthUOM
        {
            get
            {
                return this.strengthUOMField;
            }
            set
            {
                this.strengthUOMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DosageNumberDescription")]
        public virtual string DosageNumberDescription
        {
            get
            {
                return this.dosageNumberDescriptionField;
            }
            set
            {
                this.dosageNumberDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DosageForm")]
        public virtual string DosageForm
        {
            get
            {
                return this.dosageFormField;
            }
            set
            {
                this.dosageFormField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Route")]
        public virtual string Route
        {
            get
            {
                return this.routeField;
            }
            set
            {
                this.routeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DosageFrequencyDescription")]
        public virtual string DosageFrequencyDescription
        {
            get
            {
                return this.dosageFrequencyDescriptionField;
            }
            set
            {
                this.dosageFrequencyDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Dispense")]
        public virtual string Dispense
        {
            get
            {
                return this.dispenseField;
            }
            set
            {
                this.dispenseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "TakeAsNeeded")]
        public virtual string TakeAsNeeded
        {
            get
            {
                return this.takeAsNeededField;
            }
            set
            {
                this.takeAsNeededField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DispenseAsWritten")]
        public virtual string DispenseAsWritten
        {
            get
            {
                return this.dispenseAsWrittenField;
            }
            set
            {
                this.dispenseAsWrittenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Refills")]
        public virtual byte Refills
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool RefillsSpecified
        {
            get
            {
                return this.refillsFieldSpecified;
            }
            set
            {
                this.refillsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Status")]
        public virtual string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "SubStatus")]
        public virtual string SubStatus
        {
            get
            {
                return this.subStatusField;
            }
            set
            {
                this.subStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Archive")]
        public virtual string Archive
        {
            get
            {
                return this.archiveField;
            }
            set
            {
                this.archiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PrescriptionGuid")]
        public virtual string PrescriptionGuid
        {
            get
            {
                return this.prescriptionGuidField;
            }
            set
            {
                this.prescriptionGuidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "OrderGUID")]
        public virtual string OrderGUID
        {
            get
            {
                return this.orderGUIDField;
            }
            set
            {
                this.orderGUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PrescriptionNotes")]
        public virtual string PrescriptionNotes
        {
            get
            {
                return this.prescriptionNotesField;
            }
            set
            {
                this.prescriptionNotesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PharmacistNotes")]
        public virtual string PharmacistNotes
        {
            get
            {
                return this.pharmacistNotesField;
            }
            set
            {
                this.pharmacistNotesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalPhysicianID")]
        public virtual string ExternalPhysicianID
        {
            get
            {
                return this.externalPhysicianIDField;
            }
            set
            {
                this.externalPhysicianIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PhysicianName")]
        public virtual string PhysicianName
        {
            get
            {
                return this.physicianNameField;
            }
            set
            {
                this.physicianNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DateMovedToPreviousMedications")]
        public virtual string DateMovedToPreviousMedications
        {
            get
            {
                return this.dateMovedToPreviousMedicationsField;
            }
            set
            {
                this.dateMovedToPreviousMedicationsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "FormularyType")]
        public virtual string FormularyType
        {
            get
            {
                return this.formularyTypeField;
            }
            set
            {
                this.formularyTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "FormularyTypeID")]
        public virtual string FormularyTypeID
        {
            get
            {
                return this.formularyTypeIDField;
            }
            set
            {
                this.formularyTypeIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "FormularyMember")]
        public virtual string FormularyMember
        {
            get
            {
                return this.formularyMemberField;
            }
            set
            {
                this.formularyMemberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "FormularyId")]
        public virtual string FormularyId
        {
            get
            {
                return this.formularyIdField;
            }
            set
            {
                this.formularyIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "FormularyStatus")]
        public virtual string FormularyStatus
        {
            get
            {
                return this.formularyStatusField;
            }
            set
            {
                this.formularyStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ModifiedSig")]
        public virtual bool ModifiedSig
        {
            get
            {
                return this.modifiedSigField;
            }
            set
            {
                this.modifiedSigField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool ModifiedSigSpecified
        {
            get
            {
                return this.modifiedSigFieldSpecified;
            }
            set
            {
                this.modifiedSigFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ModifiedSigStatus")]
        public virtual string ModifiedSigStatus
        {
            get
            {
                return this.modifiedSigStatusField;
            }
            set
            {
                this.modifiedSigStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalPrescriptionID")]
        public virtual string ExternalPrescriptionID
        {
            get
            {
                return this.externalPrescriptionIDField;
            }
            set
            {
                this.externalPrescriptionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "EpisodeIdentifier")]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "EncounterIdentifier")]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalSource")]
        public virtual string ExternalSource
        {
            get
            {
                return this.externalSourceField;
            }
            set
            {
                this.externalSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugConcept")]
        public virtual string ExternalDrugConcept
        {
            get
            {
                return this.externalDrugConceptField;
            }
            set
            {
                this.externalDrugConceptField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugName")]
        public virtual string ExternalDrugName
        {
            get
            {
                return this.externalDrugNameField;
            }
            set
            {
                this.externalDrugNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugStrength")]
        public virtual string ExternalDrugStrength
        {
            get
            {
                return this.externalDrugStrengthField;
            }
            set
            {
                this.externalDrugStrengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugStrengthUOM")]
        public virtual string ExternalDrugStrengthUOM
        {
            get
            {
                return this.externalDrugStrengthUOMField;
            }
            set
            {
                this.externalDrugStrengthUOMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugStrengthWithUOM")]
        public virtual string ExternalDrugStrengthWithUOM
        {
            get
            {
                return this.externalDrugStrengthWithUOMField;
            }
            set
            {
                this.externalDrugStrengthWithUOMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugDosageForm")]
        public virtual string ExternalDrugDosageForm
        {
            get
            {
                return this.externalDrugDosageFormField;
            }
            set
            {
                this.externalDrugDosageFormField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugRoute")]
        public virtual string ExternalDrugRoute
        {
            get
            {
                return this.externalDrugRouteField;
            }
            set
            {
                this.externalDrugRouteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugIdentifier")]
        public virtual string ExternalDrugIdentifier
        {
            get
            {
                return this.externalDrugIdentifierField;
            }
            set
            {
                this.externalDrugIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugIdentifierType")]
        public virtual string ExternalDrugIdentifierType
        {
            get
            {
                return this.externalDrugIdentifierTypeField;
            }
            set
            {
                this.externalDrugIdentifierTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugSchedule")]
        public virtual string ExternalDrugSchedule
        {
            get
            {
                return this.externalDrugScheduleField;
            }
            set
            {
                this.externalDrugScheduleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalDrugOTC")]
        public virtual string ExternalDrugOTC
        {
            get
            {
                return this.externalDrugOTCField;
            }
            set
            {
                this.externalDrugOTCField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DosageNumberTypeID")]
        public virtual short DosageNumberTypeID
        {
            get
            {
                return this.dosageNumberTypeIDField;
            }
            set
            {
                this.dosageNumberTypeIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool DosageNumberTypeIDSpecified
        {
            get
            {
                return this.dosageNumberTypeIDFieldSpecified;
            }
            set
            {
                this.dosageNumberTypeIDFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DosageFormTypeId")]
        public virtual byte DosageFormTypeId
        {
            get
            {
                return this.dosageFormTypeIdField;
            }
            set
            {
                this.dosageFormTypeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool DosageFormTypeIdSpecified
        {
            get
            {
                return this.dosageFormTypeIdFieldSpecified;
            }
            set
            {
                this.dosageFormTypeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DosageRouteTypeId")]
        public virtual byte DosageRouteTypeId
        {
            get
            {
                return this.dosageRouteTypeIdField;
            }
            set
            {
                this.dosageRouteTypeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool DosageRouteTypeIdSpecified
        {
            get
            {
                return this.dosageRouteTypeIdFieldSpecified;
            }
            set
            {
                this.dosageRouteTypeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DosageFrequencyTypeID")]
        public virtual short DosageFrequencyTypeID
        {
            get
            {
                return this.dosageFrequencyTypeIDField;
            }
            set
            {
                this.dosageFrequencyTypeIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool DosageFrequencyTypeIDSpecified
        {
            get
            {
                return this.dosageFrequencyTypeIDFieldSpecified;
            }
            set
            {
                this.dosageFrequencyTypeIDFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DaysSupply")]
        public virtual int DaysSupply
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool DaysSupplySpecified
        {
            get
            {
                return this.daysSupplyFieldSpecified;
            }
            set
            {
                this.daysSupplyFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PrescriptionTimestamp")]
        public virtual System.DateTime PrescriptionTimestamp
        {
            get
            {
                return this.prescriptionTimestampField;
            }
            set
            {
                this.prescriptionTimestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PrescriptionTimestampSpecified
        {
            get
            {
                return this.prescriptionTimestampFieldSpecified;
            }
            set
            {
                this.prescriptionTimestampFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "OriginalPrescriptionGuid")]
        public virtual string OriginalPrescriptionGuid
        {
            get
            {
                return this.originalPrescriptionGuidField;
            }
            set
            {
                this.originalPrescriptionGuidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalUserID")]
        public virtual string ExternalUserID
        {
            get
            {
                return this.externalUserIDField;
            }
            set
            {
                this.externalUserIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ExternalUserType")]
        public virtual string ExternalUserType
        {
            get
            {
                return this.externalUserTypeField;
            }
            set
            {
                this.externalUserTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DeaGenericNamedCode")]
        public virtual string DeaGenericNamedCode
        {
            get
            {
                return this.deaGenericNamedCodeField;
            }
            set
            {
                this.deaGenericNamedCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Diagnosis")]
        public virtual string Diagnosis
        {
            get
            {
                return this.diagnosisField;
            }
            set
            {
                this.diagnosisField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DiagnosisSource")]
        public virtual string DiagnosisSource
        {
            get
            {
                return this.diagnosisSourceField;
            }
            set
            {
                this.diagnosisSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DiagnosisName")]
        public virtual string DiagnosisName
        {
            get
            {
                return this.diagnosisNameField;
            }
            set
            {
                this.diagnosisNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DispenseNumberQualifier")]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DispenseNumberQualifierDescription")]
        public virtual string DispenseNumberQualifierDescription
        {
            get
            {
                return this.dispenseNumberQualifierDescriptionField;
            }
            set
            {
                this.dispenseNumberQualifierDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "LocationName")]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "GenericName")]
        public virtual string GenericName
        {
            get
            {
                return this.genericNameField;
            }
            set
            {
                this.genericNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PatientFriendlySIG")]
        public virtual string PatientFriendlySIG
        {
            get
            {
                return this.patientFriendlySIGField;
            }
            set
            {
                this.patientFriendlySIGField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PrintLeaflet")]
        public virtual string PrintLeaflet
        {
            get
            {
                return this.printLeafletField;
            }
            set
            {
                this.printLeafletField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "DeaClassCode")]
        public virtual string DeaClassCode
        {
            get
            {
                return this.deaClassCodeField;
            }
            set
            {
                this.deaClassCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PharmacyType")]
        public virtual int PharmacyType
        {
            get
            {
                return this.pharmacyTypeField;
            }
            set
            {
                this.pharmacyTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PharmacyTypeSpecified
        {
            get
            {
                return this.pharmacyTypeFieldSpecified;
            }
            set
            {
                this.pharmacyTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PharmacyDetailType")]
        public virtual byte PharmacyDetailType
        {
            get
            {
                return this.pharmacyDetailTypeField;
            }
            set
            {
                this.pharmacyDetailTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PharmacyDetailTypeSpecified
        {
            get
            {
                return this.pharmacyDetailTypeFieldSpecified;
            }
            set
            {
                this.pharmacyDetailTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "FinalDestinationType")]
        public virtual byte FinalDestinationType
        {
            get
            {
                return this.finalDestinationTypeField;
            }
            set
            {
                this.finalDestinationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool FinalDestinationTypeSpecified
        {
            get
            {
                return this.finalDestinationTypeFieldSpecified;
            }
            set
            {
                this.finalDestinationTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "FinalStatusType")]
        public virtual byte FinalStatusType
        {
            get
            {
                return this.finalStatusTypeField;
            }
            set
            {
                this.finalStatusTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool FinalStatusTypeSpecified
        {
            get
            {
                return this.finalStatusTypeFieldSpecified;
            }
            set
            {
                this.finalStatusTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PharmacyNCPDP")]
        public virtual string PharmacyNCPDP
        {
            get
            {
                return this.pharmacyNCPDPField;
            }
            set
            {
                this.pharmacyNCPDPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PharmacyFullInfo")]
        public virtual string PharmacyFullInfo
        {
            get
            {
                return this.pharmacyFullInfoField;
            }
            set
            {
                this.pharmacyFullInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "SourcePrescriptionGuid")]
        public virtual string SourcePrescriptionGuid
        {
            get
            {
                return this.sourcePrescriptionGuidField;
            }
            set
            {
                this.sourcePrescriptionGuidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PatientID")]
        public virtual string PatientID
        {
            get
            {
                return this.patientIDField;
            }
            set
            {
                this.patientIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "PatientIDType")]
        public virtual string PatientIDType
        {
            get
            {
                return this.patientIDTypeField;
            }
            set
            {
                this.patientIDTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "rxcui")]
        public virtual decimal Rxcui
        {
            get
            {
                return this.rxcuiField;
            }
            set
            {
                this.rxcuiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool RxcuiSpecified
        {
            get
            {
                return this.rxcuiFieldSpecified;
            }
            set
            {
                this.rxcuiFieldSpecified = value;
            }
        }
    }
}
