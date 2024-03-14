using api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace api.Extensions
{
    public static class RoleClaimExtention
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Roles.Name)
            };

            return result;
        }
    }
}