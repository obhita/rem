namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, TypeName = "NewDataSet")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "NewDataSet")]
    public partial class AllergyNewDataSet
    {

        private System.Collections.Generic.List<AllergyNewDataSetTable> itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Table", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public virtual System.Collections.Generic.List<AllergyNewDataSetTable> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, TypeName = "NewDataSetTable")]
    public partial class AllergyNewDataSetTable
    {

        private int allergyTypeField;

        private bool allergyTypeFieldSpecified;

        private int compositeAllergyIDField;

        private bool compositeAllergyIDFieldSpecified;

        private string allergySourceIDField;

        private string allergyIdField;

        private int allergyConceptIdField;

        private bool allergyConceptIdFieldSpecified;

        private string conceptTypeField;

        private string allergyNameField;

        private string statusField;

        private int allergySeverityTypeIdField;

        private bool allergySeverityTypeIdFieldSpecified;

        private string allergySeverityNameField;

        private string allergyNotesField;

        private string conceptIDField;

        private int conceptTypeIdField;

        private bool conceptTypeIdFieldSpecified;

        private string rxcuiField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AllergyType")]
        public virtual int AllergyType
        {
            get
            {
                return this.allergyTypeField;
            }
            set
            {
                this.allergyTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool AllergyTypeSpecified
        {
            get
            {
                return this.allergyTypeFieldSpecified;
            }
            set
            {
                this.allergyTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "CompositeAllergyID")]
        public virtual int CompositeAllergyID
        {
            get
            {
                return this.compositeAllergyIDField;
            }
            set
            {
                this.compositeAllergyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool CompositeAllergyIDSpecified
        {
            get
            {
                return this.compositeAllergyIDFieldSpecified;
            }
            set
            {
                this.compositeAllergyIDFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AllergySourceID")]
        public virtual string AllergySourceID
        {
            get
            {
                return this.allergySourceIDField;
            }
            set
            {
                this.allergySourceIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AllergyId")]
        public virtual string AllergyId
        {
            get
            {
                return this.allergyIdField;
            }
            set
            {
                this.allergyIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AllergyConceptId")]
        public virtual int AllergyConceptId
        {
            get
            {
                return this.allergyConceptIdField;
            }
            set
            {
                this.allergyConceptIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool AllergyConceptIdSpecified
        {
            get
            {
                return this.allergyConceptIdFieldSpecified;
            }
            set
            {
                this.allergyConceptIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ConceptType")]
        public virtual string ConceptType
        {
            get
            {
                return this.conceptTypeField;
            }
            set
            {
                this.conceptTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AllergyName")]
        public virtual string AllergyName
        {
            get
            {
                return this.allergyNameField;
            }
            set
            {
                this.allergyNameField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AllergySeverityTypeId")]
        public virtual int AllergySeverityTypeId
        {
            get
            {
                return this.allergySeverityTypeIdField;
            }
            set
            {
                this.allergySeverityTypeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool AllergySeverityTypeIdSpecified
        {
            get
            {
                return this.allergySeverityTypeIdFieldSpecified;
            }
            set
            {
                this.allergySeverityTypeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AllergySeverityName")]
        public virtual string AllergySeverityName
        {
            get
            {
                return this.allergySeverityNameField;
            }
            set
            {
                this.allergySeverityNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "AllergyNotes")]
        public virtual string AllergyNotes
        {
            get
            {
                return this.allergyNotesField;
            }
            set
            {
                this.allergyNotesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ConceptID")]
        public virtual string ConceptID
        {
            get
            {
                return this.conceptIDField;
            }
            set
            {
                this.conceptIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "ConceptTypeId")]
        public virtual int ConceptTypeId
        {
            get
            {
                return this.conceptTypeIdField;
            }
            set
            {
                this.conceptTypeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool ConceptTypeIdSpecified
        {
            get
            {
                return this.conceptTypeIdFieldSpecified;
            }
            set
            {
                this.conceptTypeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "rxcui")]
        public virtual string Rxcui
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
    }

}
