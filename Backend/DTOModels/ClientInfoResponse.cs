using System;

namespace POSProject.Backend.DTOModels;

public class ClientInfoResponse
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string StudentId { get; set; }
    public string UnionistId { get; set; }
    public string GroupId { get; set; }
    public string Institute { get; set; }
    public uint Coins { get; set; }
    public DateTime? BirthDay { get; set; }
    public DateTime? SubscriptionStartDate { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }
}