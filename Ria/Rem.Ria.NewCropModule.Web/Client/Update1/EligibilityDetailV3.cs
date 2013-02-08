namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="EligibilityDetailV3")]
    public partial class EligibilityDetailV3
    {
        
        private string sourceField;
        
        private string pBMField;
        
        private string planField;
        
        private string cardHolderField;
        
        private string cardHolderIDField;
        
        private string pharmacyBenefitField;
        
        private string mailOrderBenefitField;
        
        private string specialtyPharmacyBenefitField;
        
        private string lTCBenefitField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PBM")]
        public virtual string PBM
        {
            get
            {
                return this.pBMField;
            }
            set
            {
                this.pBMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Plan")]
        public virtual string Plan
        {
            get
            {
                return this.planField;
            }
            set
            {
                this.planField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="CardHolder")]
        public virtual string CardHolder
        {
            get
            {
                return this.cardHolderField;
            }
            set
            {
                this.cardHolderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="CardHolderID")]
        public virtual string CardHolderID
        {
            get
            {
                return this.cardHolderIDField;
            }
            set
            {
                this.cardHolderIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="PharmacyBenefit")]
        public virtual string PharmacyBenefit
        {
            get
            {
                return this.pharmacyBenefitField;
            }
            set
            {
                this.pharmacyBenefitField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="MailOrderBenefit")]
        public virtual string MailOrderBenefit
        {
            get
            {
                return this.mailOrderBenefitField;
            }
            set
            {
                this.mailOrderBenefitField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="SpecialtyPharmacyBenefit")]
        public virtual string SpecialtyPharmacyBenefit
        {
            get
            {
                return this.specialtyPharmacyBenefitField;
            }
            set
            {
                this.specialtyPharmacyBenefitField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="LTCBenefit")]
        public virtual string LTCBenefit
        {
            get
            {
                return this.lTCBenefitField;
            }
            set
            {
                this.lTCBenefitField = value;
            }
        }
    }
}