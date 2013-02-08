namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "ExternalDrugOverrideType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "ExternalDrugOverrideType")]
    public partial class ExternalDrugOverrideType
    {

        private string externalDrugConceptField;

        private string externalDrugNameField;

        private string externalDrugStrengthField;

        private string externalDrugStrengthUOMField;

        private string externalDrugStrengthWithUOMField;

        private string externalDrugDosageFormField;

        private string externalDrugRouteField;

        private string externalDrugIdentifierField;

        private string externalDrugIdentifierTypeField;

        private DrugScheduleType externalDrugScheduleField;

        private DrugOTCType externalDrugOverTheCounterField;

        private bool externalDrugOverTheCounterFieldSpecified;

        private string externalDrugNdcField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugConcept")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugName")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugStrength")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugStrengthUOM")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugStrengthWithUOM")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugDosageForm")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugRoute")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugIdentifier")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugIdentifierType")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugSchedule")]
        public virtual DrugScheduleType ExternalDrugSchedule
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugOverTheCounter")]
        public virtual DrugOTCType ExternalDrugOverTheCounter
        {
            get
            {
                return this.externalDrugOverTheCounterField;
            }
            set
            {
                this.externalDrugOverTheCounterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool ExternalDrugOverTheCounterSpecified
        {
            get
            {
                return this.externalDrugOverTheCounterFieldSpecified;
            }
            set
            {
                this.externalDrugOverTheCounterFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "externalDrugNdc")]
        public virtual string ExternalDrugNdc
        {
            get
            {
                return this.externalDrugNdcField;
            }
            set
            {
                this.externalDrugNdcField = value;
            }
        }
    }
}