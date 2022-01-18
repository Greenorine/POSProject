using System;
using System.ComponentModel.DataAnnotations;

namespace POSProject.Backend.Models;

public abstract class BaseAudit
{
    [Key] public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsActive { get; set; } = true;
}