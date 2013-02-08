#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using Pillar.Security.AccessControl;

namespace Rem.Ria.Infrastructure.Web.Permissions
{
    /// <summary>
    /// AgencyPermission class.
    /// </summary>
    public static class AgencyPermission
    {
        #region Constants and Fields

        /// <summary>
        /// Agency Edit Permission
        /// </summary>
        public static readonly Permission AgencyEditPermission = new Permission { Name = "agencymodule/agencyedit" };

        /// <summary>
        /// Agency View Permission
        /// </summary>
        public static readonly Permission AgencyViewPermission = new Permission { Name = "agencymodule/agencyview" };

        /// <summary>
        /// Lab Resul tEdit Permission
        /// </summary>
        public static readonly Permission LabResultEditPermission = new Permission { Name = "agencymodule/labresultedit" };

        /// <summary>
        /// Lab Result View Permission
        /// </summary>
        public static readonly Permission LabResultViewPermission = new Permission { Name = "agencymodule/labresultview" };

        /// <summary>
        /// Location Edit Permission
        /// </summary>
        public static readonly Permission LocationEditPermission = new Permission { Name = "agencymodule/locationedit" };

        /// <summary>
        /// Location View Permission
        /// </summary>
        public static readonly Permission LocationViewPermission = new Permission { Name = "agencymodule/locationview" };

        /// <summary>
        /// Login Information View Permission
        /// </summary>
        public static readonly Permission LoginInformationViewPermission = new Permission { Name = "agencymodule/logininformationview" };

        /// <summary>
        /// Meaningful Use Objectives Edit Permission
        /// </summary>
        public static readonly Permission MeaningfulUseObjectivesEditPermission = new Permission { Name = "agencymodule/meaningfuluseedit" };

        /// <summary>
        /// MeaningfulUse Objectives View Permission
        /// </summary>
        public static readonly Permission MeaningfulUseObjectivesViewPermission = new Permission { Name = "agencymodule/meaningfuluseview" };

        /// <summary>
        /// Staff Edit Permission
        /// </summary>
        public static readonly Permission StaffEditPermission = new Permission { Name = "agencymodule/staffedit" };

        /// <summary>
        /// Staff View Permission
        /// </summary>
        public static readonly Permission StaffViewPermission = new Permission { Name = "agencymodule/staffview" };

        /// <summary>
        /// Upload LabResult Permission
        /// </summary>
        public static readonly Permission UploadLabResultPermission = new Permission { Name = "agencymodule/labresultupload" };

        #endregion
    }
}
