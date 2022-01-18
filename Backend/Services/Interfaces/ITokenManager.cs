using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using POSProject.Backend.Models;

namespace POSProject.Backend.Services.Interfaces;

public interface ITokenManager
{
    public Task<string> GenerateToken(string login, string password, DateTime expiresAt, List<Role> requiredRoles = null);
    public ClaimsPrincipal GetPrincipal(string token);
}