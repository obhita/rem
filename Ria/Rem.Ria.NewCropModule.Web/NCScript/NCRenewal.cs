namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "NCRenewal")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = false, ElementName = "NCRenewal")]
    public partial class NCRenewal
    {

        private CredentialsType credentialsField;

        private AccountType accountField;

        private LocationType locationField;

        private LicensedPrescriberType licensedPrescriberField;

        private StaffType staffField;

        private PatientType patientField;

        private PrescriptionRenewalRequestType prescriptionRenewalRequestField;

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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PrescriptionRenewalRequest")]
        public virtual PrescriptionRenewalRequestType PrescriptionRenewalRequest
        {
            get
            {
                return this.prescriptionRenewalRequestField;
            }
            set
            {
                this.prescriptionRenewalRequestField = value;
            }
        }
    }
}