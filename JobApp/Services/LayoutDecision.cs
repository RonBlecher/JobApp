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
        string Layout = "";

        var identity = (ClaimsIdentity)User.Identity;
        IEnumerable<Claim> claims = identity.Claims;
        Claim roleClaim = claims.Where(claim => claim.Type == ClaimTypes.Role).First();
        if (roleClaim.Value == "Seeker")
        {
            Layout = "_Layout_seekers";

        }
        else if (roleClaim.Value.ToString() == "Publisher")
        {
            Layout = "_Layout_publishers";

        }
        else if (roleClaim.Value.ToString() == "Admin")
        {
            Layout = "_Layout_admins";
        }

        return Layout;
    }
}