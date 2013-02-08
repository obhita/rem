namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7:NCStandard", TypeName = "RoleType")]
    public enum RoleType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "doctor")]
        Doctor,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "nurse")]
        Nurse,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "admin")]
        Admin,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "manager")]
        Manager,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "nurseNoRx")]
        NurseNoRx,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "doctorNoRx")]
        DoctorNoRx,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "doctorReadOnly")]
        DoctorReadOnly,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "nurseReadOnly")]
        NurseReadOnly,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "interestedPartyReadOnly")]
        InterestedPartyReadOnly,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "supervisingDoctor")]
        SupervisingDoctor,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "midlevelPrescriber")]
        MidlevelPrescriber,
    }
}