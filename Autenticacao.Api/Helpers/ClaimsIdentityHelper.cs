using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Autenticacao.Api.Helpers
{
    public static class ClaimsIdentityHelper
    {
        internal static ClaimsIdentity ClaimsUsuario => (ClaimsIdentity)System.Web.HttpContext.Current.User.Identity;

        internal static Claim ObterClaim(string claimType) => ClaimsUsuario.Claims.FirstOrDefault(c => c.Type == claimType);

        internal static IEnumerable<Claim> ObterClaims(string claimType) => ClaimsUsuario.Claims.Where(c => c.Type == claimType);

        internal static string LoginUsuario => ObterClaim(ClaimTypes.Name).Value;

        internal static int CodigoUsuario
        {
            get
            {
                int i;
                int.TryParse(ObterClaim(ClaimTypes.NameIdentifier)?.Value, out i);
                return i;
            }
        }

        internal static string NomeUsuario => ObterClaim(ClaimTypesCustom.FullName).Value;
        internal static string EmailUsuario => ObterClaim(ClaimTypes.Email).Value;
        internal static List<string> PerfisUsuario => ObterClaims(ClaimTypes.Role).Select(c => c.Value).ToList();
    }
}