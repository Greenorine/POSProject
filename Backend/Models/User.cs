using System;
using System.Collections.Generic;
using System.Linq;

namespace POSProject.Backend.Models;

public class User : BaseAudit
{
    public string Email { get; set; }
    public string Password{ get; set; }
    public string Roles{ get; set; }
    public bool IsPasswordInitial{ get; set; }
}