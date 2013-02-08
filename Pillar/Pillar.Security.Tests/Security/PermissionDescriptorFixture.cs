using Pillar.Security.AccessControl;

namespace Pillar.Security.Tests.Security
{
    public class PermissionDescriptorFixture : IPermissionDescriptor
    {
        public static Permission PatientPermission = new Permission { Name = "Patient Permission" };
        public static Permission RenamePatientPermission = new Permission { Name = "Rename Patient Permission" };
        public static Permission AddPatientAddressPermission = new Permission { Name = "Add Patient Address Permission" };

        public static Permission ClinicalCasePermission = new Permission { Name = "Clinical Case Permission" };
        public static Permission AdmitPatientPermission = new Permission { Name = "Admit Patient Permission" };
        public static Permission DischargePatientAddressPermission = new Permission { Name = "Discharge Patient Address Permission" };

        #region Implementation of IPermissionDescriptor

        private readonly ResourceList _resources = new ResourceListBuilder ()
            .AddResource ( "Rem.Domain.Patient", PatientPermission,
                           rlb => rlb
                                      .AddResource ( "RenamePatient", RenamePatientPermission )
                                      .AddResource ( "AddAddress", AddPatientAddressPermission )
            )
            .AddResource ( "Rem.Domain.ClinicalCase", ClinicalCasePermission,
                           rlb =>
                           rlb
                               .AddResource ( "AdmitPatient", AdmitPatientPermission )
                               .AddResource ( "DischargePatient", DischargePatientAddressPermission )
            );

        public ResourceList Resources
        {
            get { return _resources; }
        }

        #endregion
    }
}