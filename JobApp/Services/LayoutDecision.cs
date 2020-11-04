using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

public class LayoutDecision
{
    public static string LocateLayout(ClaimsPrincipal User)
    {
        string layout = "";

        var identity = (ClaimsIdentity) User.Identity;
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