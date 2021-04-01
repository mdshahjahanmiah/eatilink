using System;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.Security.Handlers
{
    public interface IJwtTokenHandler
    {
        string PrepareTokenFromAccessToekn(string accessToken);
        string GenerateJwtSecurityToken(string userId);
        (bool, string) VerifyJwtSecurityToken(string token);
    }
}
