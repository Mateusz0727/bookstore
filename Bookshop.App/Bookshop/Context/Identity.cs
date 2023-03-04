using System.Security.Claims;
using System.Security.Principal;



namespace Bookshop.Context
{
    public static class Identity
    {
        #region Id()
        public static long Id(this IPrincipal principal)
        {
            return Convert.ToInt64((principal as ClaimsPrincipal).FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static long Id(this IIdentity identity)
        {
            return Convert.ToInt64((identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        #endregion
    }
}
