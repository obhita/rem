using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Claims;
using Pillar.Common.InversionOfControl;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Security;
using ClaimTypes = Microsoft.IdentityModel.Claims.ClaimTypes;

namespace Rem.Web
{
    public class LoginHandler : IHttpHandler
    {
        private readonly ICurrentClaimsPrincipalService _currentClaimsPrincipalService;
        private readonly IStaffRepository _staffRepository;
        private readonly ISystemAccountRepository _accountRepository;
        private readonly ISignOnService _signOnService;

        public LoginHandler ()
        {
            _currentClaimsPrincipalService = IoC.CurrentContainer.Resolve<ICurrentClaimsPrincipalService>();
            _staffRepository = IoC.CurrentContainer.Resolve<IStaffRepository>();
            _accountRepository = IoC.CurrentContainer.Resolve<ISystemAccountRepository> ();
            _signOnService = IoC.CurrentContainer.Resolve<ISignOnService>();
        }

        public void ProcessRequest(HttpContext context)
        {
           // var identity = _currentClaimsPrincipalService.GetCurrentPrincipal ().Identity;
            var identity = _currentClaimsPrincipalService.GetCurrentPrincipal().Identity as IClaimsIdentity;
            var nameIdentifier = identity.Claims.First(c => c.ClaimType == ClaimTypes.NameIdentifier).Value;

            // check this for security reason
            if (identity.IsAuthenticated)
            {
                var staffKeyString = context.Request[ "staffKey" ];
                var staffKey = string.IsNullOrEmpty ( staffKeyString ) ? 0 : long.Parse ( staffKeyString );
                var account = _accountRepository.GetByIdentifier(nameIdentifier);

                // check this for security reason
                if (account.StaffMembers.Any(x => x.Key == staffKey)) 
                {
                    var staff = _staffRepository.GetByKey ( staffKey );
                    _signOnService.LoginAs ( staff );

                    context.Response.Redirect ( "~/Client.aspx" );
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}