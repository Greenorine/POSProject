using System;

namespace POSProject.Models;

public abstract class BaseAudit
{
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}