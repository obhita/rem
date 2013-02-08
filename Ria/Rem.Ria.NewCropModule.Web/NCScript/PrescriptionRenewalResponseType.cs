namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "PrescriptionRenewalResponseType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "PrescriptionRenewalResponseType")]
    public partial class PrescriptionRenewalResponseType
    {

        private string renewalRequestIdentifierField;

        private ResponseCodeType responseCodeField;

        private string refillCountField;

        private DrugScheduleType drugScheduleField;

        private bool drugScheduleFieldSpecified;

        private ResponseDenyCodeType responseDenyCodeField;

        private bool responseDenyCodeFieldSpecified;

        private string messageToPharmacistField;

        private string refillQuantityQualifierField;

        private string idField;

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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "responseCode")]
        public virtual ResponseCodeType ResponseCode
        {
            get
            {
                return this.responseCodeField;
            }
            set
            {
                this.responseCodeField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "drugSchedule")]
        public virtual DrugScheduleType DrugSchedule
        {
            get
            {
                return this.drugScheduleField;
            }
            set
            {
                this.drugScheduleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool DrugScheduleSpecified
        {
            get
            {
                return this.drugScheduleFieldSpecified;
            }
            set
            {
                this.drugScheduleFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "responseDenyCode")]
        public virtual ResponseDenyCodeType ResponseDenyCode
        {
            get
            {
                return this.responseDenyCodeField;
            }
            set
            {
                this.responseDenyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool ResponseDenyCodeSpecified
        {
            get
            {
                return this.responseDenyCodeFieldSpecified;
            }
            set
            {
                this.responseDenyCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "messageToPharmacist")]
        public virtual string MessageToPharmacist
        {
            get
            {
                return this.messageToPharmacistField;
            }
            set
            {
                this.messageToPharmacistField = value;
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