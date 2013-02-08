namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "PatientDiagnosisType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "PatientDiagnosisType")]
    public partial class PatientDiagnosisType
    {

        private string diagnosisIDField;

        private DiagnosisType diagnosisTypeField;

        private string onsetDateField;

        private string diagnosisNameField;

        private string recordedDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "diagnosisID")]
        public virtual string DiagnosisID
        {
            get
            {
                return this.diagnosisIDField;
            }
            set
            {
                this.diagnosisIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "diagnosisType")]
        public virtual DiagnosisType DiagnosisType
        {
            get
            {
                return this.diagnosisTypeField;
            }
            set
            {
                this.diagnosisTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "onsetDate")]
        public virtual string OnsetDate
        {
            get
            {
                return this.onsetDateField;
            }
            set
            {
                this.onsetDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "diagnosisName")]
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "recordedDate")]
        public virtual string RecordedDate
        {
            get
            {
                return this.recordedDateField;
            }
            set
            {
                this.recordedDateField = value;
            }
        }
    }
}