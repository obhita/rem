namespace Rem.Ria.NewCropModule.Web.Client.Common
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="DrugHistoryDetail")]
    public partial class DrugHistoryDetail
    {
        
        private string drugInfoField;
        
        private string drugNdcField;
        
        private string doctorLastNameField;
        
        private string doctorFirstNameField;
        
        private string doctorContactNumberField;
        
        private string pharmacyNameField;
        
        private string pharmacyContactNumberField;
        
        private string fillDateField;
        
        private string sourceField;
        
        private string healthplanNameField;
        
        private string drugIdField;
        
        private string drugQuantityField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DrugInfo")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DrugNdc")]
        public virtual string DrugNdc
        {
            get
            {
                return this.drugNdcField;
            }
            set
            {
                this.drugNdcField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DoctorLastName")]
        public virtual string DoctorLastName
        {
            get
            {
                return this.doctorLastNameField;
            }
            set
            {
                this.doctorLastNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DoctorFirstName")]
        public virtual string DoctorFirstName
        {
            get
            {
                return this.doctorFirstNameField;
            }
            set
            {
                this.doctorFirstNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DoctorContactNumber")]
        public virtual string DoctorContactNumber
        {
            get
            {
                return this.doctorContactNumberField;
            }
            set
            {
                this.doctorContactNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PharmacyName")]
        public virtual string PharmacyName
        {
            get
            {
                return this.pharmacyNameField;
            }
            set
            {
                this.pharmacyNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PharmacyContactNumber")]
        public virtual string PharmacyContactNumber
        {
            get
            {
                return this.pharmacyContactNumberField;
            }
            set
            {
                this.pharmacyContactNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="FillDate")]
        public virtual string FillDate
        {
            get
            {
                return this.fillDateField;
            }
            set
            {
                this.fillDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Source")]
        public virtual string Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="HealthplanName")]
        public virtual string HealthplanName
        {
            get
            {
                return this.healthplanNameField;
            }
            set
            {
                this.healthplanNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DrugId")]
        public virtual string DrugId
        {
            get
            {
                return this.drugIdField;
            }
            set
            {
                this.drugIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="DrugQuantity")]
        public virtual string DrugQuantity
        {
            get
            {
                return this.drugQuantityField;
            }
            set
            {
                this.drugQuantityField = value;
            }
        }
    }
}