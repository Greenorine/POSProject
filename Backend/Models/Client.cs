using System;

namespace POSProject.Backend.Models
{
    public class Client : BaseAudit
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string StudentId { get; set; } // Номер студ. билета
        public string UnionistId { get; set; } // Номер профсоюзника
        public string GroupId { get; set; } // Номер группы
        public string Institute { get; set; }
        public uint Coins { get; set; } // Баллы или монетки
        public DateTime? BirthDay { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
    }
}