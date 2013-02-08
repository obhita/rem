namespace Rem.Ria.NewCropModule.Web.Client.Common
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="Result")]
    public partial class Result
    {
        
        private StatusType statusField;
        
        private string messageField;
        
        private string xmlResponseField;
        
        private int rowCountField;
        
        private int timingField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Status")]
        public virtual StatusType Status
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
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Message")]
        public virtual string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="XmlResponse")]
        public virtual string XmlResponse
        {
            get
            {
                return this.xmlResponseField;
            }
            set
            {
                this.xmlResponseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="RowCount")]
        public virtual int RowCount
        {
            get
            {
                return this.rowCountField;
            }
            set
            {
                this.rowCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Timing")]
        public virtual int Timing
        {
            get
            {
                return this.timingField;
            }
            set
            {
                this.timingField = value;
            }
        }
    }
}