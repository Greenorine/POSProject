using System;
using System.ComponentModel.DataAnnotations;

namespace POSProject.Models
{
    public class Offer : BaseAudit, ICloneable
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Activity { get; set; }
        public string Delivery { get; set; }
        
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}