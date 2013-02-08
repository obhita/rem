// Guids.cs
// MUST match guids.h
using System;

namespace FEI.GhostDocPackage
{
    static class GuidList
    {
        public const string guidGhostDocPackagePkgString = "06a08dc5-f28e-4b31-bdbb-62da51cb7597";
        public const string guidGhostDocPackageCmdSetString = "5e7cbece-ae51-4d2f-b6b1-940c5a5d4137";
        public const string guidGhostDocPackageProjectCmdSetString = "5e7cbece-ae51-4d2f-b6b1-940c5a5d4136";
        public const string guidGhostDocPackageFolderCmdSetString = "5e7cbece-ae51-4d2f-b6b1-940c5a5d4135";

        public static readonly Guid guidGhostDocPackageCmdSet = new Guid(guidGhostDocPackageCmdSetString);
        public static readonly Guid guidGhostDocPackageProjectCmdSet = new Guid(guidGhostDocPackageProjectCmdSetString);
        public static readonly Guid guidGhostDocPackageFolderCmdSet = new Guid(guidGhostDocPackageFolderCmdSetString);
    };
}