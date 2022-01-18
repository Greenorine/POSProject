using System;

namespace POSProject.Shared;

public class ClientModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Roles { get; set; } = "Client";
    public bool IsPasswordInitial { get; set; } = true;
    public string FullName { get; set; }
    public string StudentId { get; set; }
    public string UnionistId { get; set; }
    public string GroupId { get; set; }
    public string Institute { get; set; }
    public uint Coins { get; set; }
    public DateTime? BirthDay { get; set; }
    public DateTime? SubscriptionStartDate { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }
}