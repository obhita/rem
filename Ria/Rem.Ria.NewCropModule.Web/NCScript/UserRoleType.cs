namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", TypeName = "UserRoleType")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7", IsNullable = true, ElementName = "UserRoleType")]
    public partial class UserRoleType
    {

        private UserType userField;

        private RoleType roleField;

        private string nameField;

        private string passwordField;

        private System.Collections.Generic.List<CurrentFeatureType> featuresField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "user")]
        public virtual UserType User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "role")]
        public virtual RoleType Role
        {
            get
            {
                return this.roleField;
            }
            set
            {
                this.roleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "name")]
        public virtual string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "password")]
        public virtual string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Features")]
        public virtual System.Collections.Generic.List<CurrentFeatureType> Features
        {
            get
            {
                return this.featuresField;
            }
            set
            {
                this.featuresField = value;
            }
        }
    }
}