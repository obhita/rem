// Guids.cs
// MUST match guids.h
using System;

namespace FEI.StyleCopPackage
{
    static class GuidList
    {
        public const string guidStyleCopPackagePkgString = "d46335ce-2859-4a11-8a0a-2be7b528cee8";
        public const string guidStyleCopPackageCmdSetString = "37879ba6-5644-4002-a994-af8d7e16192e";
        public const string guidStyleCopPackageProjectCmdSetString = "37879ba6-5644-4002-a994-af8d7e16192f";
        
        public static readonly Guid guidStyleCopPackageCmdSet = new Guid(guidStyleCopPackageCmdSetString);
        public static readonly Guid guidStyleCopPackageProjectCmdSet = new Guid(guidStyleCopPackageProjectCmdSetString);
    };
}