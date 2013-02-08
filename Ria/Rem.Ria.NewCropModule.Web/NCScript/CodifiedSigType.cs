namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "CodifiedSigType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "CodifiedSigType")]
    public partial class CodifiedSigType
    {

        private string actionTypeField;

        private string numberTypeField;

        private string formTypeField;

        private string routeTypeField;

        private string frequencyTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "ActionType")]
        public virtual string ActionType
        {
            get
            {
                return this.actionTypeField;
            }
            set
            {
                this.actionTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "NumberType")]
        public virtual string NumberType
        {
            get
            {
                return this.numberTypeField;
            }
            set
            {
                this.numberTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "FormType")]
        public virtual string FormType
        {
            get
            {
                return this.formTypeField;
            }
            set
            {
                this.formTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "RouteType")]
        public virtual string RouteType
        {
            get
            {
                return this.routeTypeField;
            }
            set
            {
                this.routeTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "FrequencyType")]
        public virtual string FrequencyType
        {
            get
            {
                return this.frequencyTypeField;
            }
            set
            {
                this.frequencyTypeField = value;
            }
        }
    }
}