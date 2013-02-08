namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "NewPrescriptionImageType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "NewPrescriptionImageType")]
    public partial class NewPrescriptionImageType
    {

        private string imageDataTypeField;

        private string imageDataFormatField;

        private string imageDataWidthField;

        private string imageDataHeightField;

        private string imageDataSizeField;

        private string imageDataField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "imageDataType")]
        public virtual string ImageDataType
        {
            get
            {
                return this.imageDataTypeField;
            }
            set
            {
                this.imageDataTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "imageDataFormat")]
        public virtual string ImageDataFormat
        {
            get
            {
                return this.imageDataFormatField;
            }
            set
            {
                this.imageDataFormatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "imageDataWidth")]
        public virtual string ImageDataWidth
        {
            get
            {
                return this.imageDataWidthField;
            }
            set
            {
                this.imageDataWidthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "imageDataHeight")]
        public virtual string ImageDataHeight
        {
            get
            {
                return this.imageDataHeightField;
            }
            set
            {
                this.imageDataHeightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "imageDataSize")]
        public virtual string ImageDataSize
        {
            get
            {
                return this.imageDataSizeField;
            }
            set
            {
                this.imageDataSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "imageData")]
        public virtual string ImageData
        {
            get
            {
                return this.imageDataField;
            }
            set
            {
                this.imageDataField = value;
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