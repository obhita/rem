namespace Rem.Ria.NewCropModule.Web.NCScript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://secure.newcropaccounts.com/interfaceV7:NCStandard", TypeName = "ResponseDenyCodeType")]
    public enum ResponseDenyCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientUnknownToThePrescriber")]
        PatientUnknownToThePrescriber,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientNeverUnderPrescriberCare")]
        PatientNeverUnderPrescriberCare,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientNoLongerUnderPrescriberCare")]
        PatientNoLongerUnderPrescriberCare,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientHasRequestedRefillTooSoon")]
        PatientHasRequestedRefillTooSoon,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "MedicationNeverPrescribedForThePatient")]
        MedicationNeverPrescribedForThePatient,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientShouldContactPrescriberFirst")]
        PatientShouldContactPrescriberFirst,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "RefillNotAppropriate")]
        RefillNotAppropriate,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientHasPickedUpPrescription")]
        PatientHasPickedUpPrescription,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientHasPickedUpPartialFillOfPrescription")]
        PatientHasPickedUpPartialFillOfPrescription,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientHasNotPickedUpPrescriptionDrugReturnedToStock")]
        PatientHasNotPickedUpPrescriptionDrugReturnedToStock,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "ChangeNotAppropriate")]
        ChangeNotAppropriate,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PatientNeedsAppointment")]
        PatientNeedsAppointment,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "PrescriberNotAssociatedWithThisPracticeOrLocation")]
        PrescriberNotAssociatedWithThisPracticeOrLocation,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "NoAttemptWillBeMadeToObtainPriorAuthorization")]
        NoAttemptWillBeMadeToObtainPriorAuthorization,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "DeniedNewPrescriptionToFollow")]
        DeniedNewPrescriptionToFollow,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(Name = "RequestAlreadyRespondedToByOtherMeans")]
        RequestAlreadyRespondedToByOtherMeans,
    }
}