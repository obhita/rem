using System.Collections.Generic;
using Pillar.Security.AccessControl;

namespace Pillar.Security.Tests.Security
{
    public static class PermissionFixtures
    {
        public static List<Permission> AllPermissionPermissions =
            new List<Permission>
                {
                    PermissionDescriptorFixture.PatientPermission,
                    PermissionDescriptorFixture.RenamePatientPermission,
                    PermissionDescriptorFixture.AddPatientAddressPermission,
                    PermissionDescriptorFixture.ClinicalCasePermission,
                    PermissionDescriptorFixture.AdmitPatientPermission,
                    PermissionDescriptorFixture.DischargePatientAddressPermission
                };

        public static List<Permission> NoPermissionPermissions =
            new List<Permission>
                {
                };

        public static List<Permission> PatientPermissionPermissionOnly = new List<Permission> ();
    }
}