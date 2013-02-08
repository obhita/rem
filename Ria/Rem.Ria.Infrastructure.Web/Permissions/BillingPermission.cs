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
    ///   Billing Permission class.
    /// </summary>
    public class BillingPermission
    {
        #region Constants and Fields

        /// <summary>
        ///   Report Access Permission
        /// </summary>
        public static readonly Permission BillingWorkspaceViewPermission = new Permission { Name = "billingmodule/billingworkspaceview" };

        /// <summary>
        /// Edit Billing Office Permission
        /// </summary>
        public static readonly Permission BillingOfficeEditPermission = new Permission { Name = "billingmodule/billingofficeedit" };

        /// <summary>
        /// View Billing Office Permission
        /// </summary>
        public static readonly Permission BillingOfficeViewPermission = new Permission { Name = "billingmodule/billingofficeview" };

        /// <summary>
        /// View Payor Permission
        /// </summary>
        public static readonly Permission PayorViewPermission = new Permission { Name = "billingmodule/payorview" };

        /// <summary>
        /// Edit Payor Permission
        /// </summary>
        public static readonly Permission PayorEditPermission = new Permission { Name = "billingmodule/payoredit" };

        /// <summary>
        /// View Payor Permission
        /// </summary>
        public static readonly Permission ClaimsViewPermission = new Permission { Name = "billingmodule/claimsview" };

        /// <summary>
        /// Edit Payor Permission
        /// </summary>
        public static readonly Permission ClaimsEditPermission = new Permission { Name = "billingmodule/claimsedit" };

        #endregion
    }
}
