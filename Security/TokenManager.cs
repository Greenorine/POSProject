using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace POSProject.Security;

public class TokenManager
{
    private static string Secret = "";

    public static string GenerateToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        var expired = DateTime.Now.AddHours(1);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Expired, expired.ToString("s"))
            }),
            Expires = expired,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(descriptor);
        return handler.WriteToken(token);
    }

    public static ClaimsPrincipal GetPrincipal(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = (JwtSecurityToken) handler.ReadToken(token);
            if (jwtToken == null) return null;

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
            var parameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = securityKey
            };
            var principal = handler.ValidateToken(token, parameters, out var securityToken);
            return principal;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}