using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

public class LayoutDecision
{
    public static string LocateLayout(ClaimsPrincipal User)
    {
        string layout = "_Layout_home";
        ClaimsIdentity identity = (ClaimsIdentity)User.Identity;

        // check if user is not signed in
        if (identity.IsAuthenticated == false)
        {
            return layout;
        }

        IEnumerable<Claim> claims = identity.Claims;
        Claim roleClaim = claims.Where(claim => claim.Type == ClaimTypes.Role).First();

        switch (roleClaim.Value)
        {
            case "Seeker":
                layout = "_Layout_seekers";
                break;

            case "Publisher":
                layout = "_Layout_publishers";
                break;

            case "Admin":
                layout = "_Layout_admins";
                break;

            default:
                break;
        }
        return layout;
    }
}
