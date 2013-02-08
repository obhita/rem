namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", TypeName="DownloadDetail")]
    public partial class DownloadDetail
    {
        
        private string latestDownloadMonthField;
        
        private string latestDownloadDayField;
        
        private string latestDownloadYearField;
        
        private string latestDownloadSizeField;
        
        private string latestDownloadUrlField;
        
        private string cchitDrugDatabaseDateField;
        
        private string commentsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="LatestDownloadMonth")]
        public virtual string LatestDownloadMonth
        {
            get
            {
                return this.latestDownloadMonthField;
            }
            set
            {
                this.latestDownloadMonthField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="LatestDownloadDay")]
        public virtual string LatestDownloadDay
        {
            get
            {
                return this.latestDownloadDayField;
            }
            set
            {
                this.latestDownloadDayField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="LatestDownloadYear")]
        public virtual string LatestDownloadYear
        {
            get
            {
                return this.latestDownloadYearField;
            }
            set
            {
                this.latestDownloadYearField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="LatestDownloadSize")]
        public virtual string LatestDownloadSize
        {
            get
            {
                return this.latestDownloadSizeField;
            }
            set
            {
                this.latestDownloadSizeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="LatestDownloadUrl")]
        public virtual string LatestDownloadUrl
        {
            get
            {
                return this.latestDownloadUrlField;
            }
            set
            {
                this.latestDownloadUrlField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="CchitDrugDatabaseDate")]
        public virtual string CchitDrugDatabaseDate
        {
            get
            {
                return this.cchitDrugDatabaseDateField;
            }
            set
            {
                this.cchitDrugDatabaseDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName="Comments")]
        public virtual string Comments
        {
            get
            {
                return this.commentsField;
            }
            set
            {
                this.commentsField = value;
            }
        }
    }
}