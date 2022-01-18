using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using POSProject.Backend.Extensions;
using POSProject.Backend.Models;
using POSProject.Backend.Services.Interfaces;

namespace POSProject.Backend.Services;

public class TokenManager : ITokenManager
{
    private readonly IDataService<User> userService;
    private readonly IConfiguration configuration;
    private readonly IPasswordHasher passwordHasher;
    private readonly IHostEnvironment environment;

    public TokenManager(IDataService<User> userService, IConfiguration configuration, IPasswordHasher passwordHasher,
        IHostEnvironment environment)
    {
        this.userService = userService;
        this.configuration = configuration;
        this.passwordHasher = passwordHasher;
        this.environment = environment;
    }

    public async Task<string> GenerateToken(string login, string password, DateTime expiresAt,
        List<Role> requiredRoles = null)
    {
        var user = await userService.GetFirstOrDefaultByQueryAsync(x => x.Email == login);
        if (user == null ||
            !environment.IsDevelopment() && !passwordHasher.VerifyHashedPassword(user.Password, password)) return null;
        var userRoles = user.GetRoles().ToList();
        if (requiredRoles != null)
        {
            if (requiredRoles.Any(requiredRole => !userRoles.Contains(requiredRole)))
                return null;
        }

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Expired, expiresAt.ToString("s"))
        };
        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole.ToString())));
        var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:SecretKey"]));
        var token = new JwtSecurityToken(
            null,
            null,
            expires: expiresAt,
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipal(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = (JwtSecurityToken) handler.ReadToken(token);
            if (jwtToken == null) return null;

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:SecretKey"]));
            var parameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = securityKey
            };
            var principal = handler.ValidateToken(token, parameters, out _);
            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }
}