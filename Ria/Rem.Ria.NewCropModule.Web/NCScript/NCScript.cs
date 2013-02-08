namespace Rem.Ria.NewCropModule.Web.NCScript
{


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "NCScript")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = false, ElementName = "NCScript")]
    public partial class NCScript
    {

        private CredentialsType credentialsField;

        private UserRoleType userRoleField;

        private DestinationType destinationField;

        private AccountType accountField;

        private LocationType locationField;

        private System.Collections.Generic.List<LocationTreeType> locationTreeField;

        private LicensedPrescriberType licensedPrescriberField;

        private StaffType staffField;

        private LicensedPrescriberType supervisingDoctorField;

        private LocationType supervisingDoctorLocationField;

        private MidlevelPrescriberType midlevelPrescriberField;

        private PatientType patientField;

        private NewPrescriptionType newPrescriptionField;

        private PrescriptionRenewalResponseType prescriptionRenewalResponseField;

        private System.Collections.Generic.List<OutsidePrescriptionType> outsidePrescriptionField;

        private NewPrescriptionWithImagesType newPrescriptionWithImagesField;

        private DrugSetType drugSetField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "Credentials")]
        public virtual CredentialsType Credentials
        {
            get
            {
                return this.credentialsField;
            }
            set
            {
                this.credentialsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "UserRole")]
        public virtual UserRoleType UserRole
        {
            get
            {
                return this.userRoleField;
            }
            set
            {
                this.userRoleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "Destination")]
        public virtual DestinationType Destination
        {
            get
            {
                return this.destinationField;
            }
            set
            {
                this.destinationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "Account")]
        public virtual AccountType Account
        {
            get
            {
                return this.accountField;
            }
            set
            {
                this.accountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "Location")]
        public virtual LocationType Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LocationTree")]
        public virtual System.Collections.Generic.List<LocationTreeType> LocationTree
        {
            get
            {
                return this.locationTreeField;
            }
            set
            {
                this.locationTreeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "LicensedPrescriber")]
        public virtual LicensedPrescriberType LicensedPrescriber
        {
            get
            {
                return this.licensedPrescriberField;
            }
            set
            {
                this.licensedPrescriberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "Staff")]
        public virtual StaffType Staff
        {
            get
            {
                return this.staffField;
            }
            set
            {
                this.staffField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "SupervisingDoctor")]
        public virtual LicensedPrescriberType SupervisingDoctor
        {
            get
            {
                return this.supervisingDoctorField;
            }
            set
            {
                this.supervisingDoctorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "SupervisingDoctorLocation")]
        public virtual LocationType SupervisingDoctorLocation
        {
            get
            {
                return this.supervisingDoctorLocationField;
            }
            set
            {
                this.supervisingDoctorLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "MidlevelPrescriber")]
        public virtual MidlevelPrescriberType MidlevelPrescriber
        {
            get
            {
                return this.midlevelPrescriberField;
            }
            set
            {
                this.midlevelPrescriberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "Patient")]
        public virtual PatientType Patient
        {
            get
            {
                return this.patientField;
            }
            set
            {
                this.patientField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "NewPrescription")]
        public virtual NewPrescriptionType NewPrescription
        {
            get
            {
                return this.newPrescriptionField;
            }
            set
            {
                this.newPrescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PrescriptionRenewalResponse")]
        public virtual PrescriptionRenewalResponseType PrescriptionRenewalResponse
        {
            get
            {
                return this.prescriptionRenewalResponseField;
            }
            set
            {
                this.prescriptionRenewalResponseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OutsidePrescription")]
        public virtual System.Collections.Generic.List<OutsidePrescriptionType> OutsidePrescription
        {
            get
            {
                return this.outsidePrescriptionField;
            }
            set
            {
                this.outsidePrescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "NewPrescriptionWithImages")]
        public virtual NewPrescriptionWithImagesType NewPrescriptionWithImages
        {
            get
            {
                return this.newPrescriptionWithImagesField;
            }
            set
            {
                this.newPrescriptionWithImagesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "DrugSet")]
        public virtual DrugSetType DrugSet
        {
            get
            {
                return this.drugSetField;
            }
            set
            {
                this.drugSetField = value;
            }
        }
    }
}
