using System;
using System.Collections.Generic;
using System.Linq;
using POSProject.Backend.Models;
using static System.Enum;

namespace POSProject.Backend.Extensions;

public static class UserExtensions
{
    public static void SetRoles(this User user, IEnumerable<Role> roles) =>
        user.Roles = string.Join(',', roles.Select(x => x.ToString()));

    public static IEnumerable<Role> GetRoles(this User user)
    {
        var result = new List<Role>();
        foreach (var roleString in user.Roles.Split(","))
        {
            if (TryParse<Role>(roleString, out var role))
                result.Add(role);
        }

        return result;
    }
}