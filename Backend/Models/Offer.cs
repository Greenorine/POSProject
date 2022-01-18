using System;

namespace POSProject.Backend.Models
{
    public class Offer : BaseAudit, ICloneable
    {
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