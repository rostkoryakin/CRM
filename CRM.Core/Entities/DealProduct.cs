﻿namespace CRM.Core.Entities
{
    public class DealProduct : BaseEntity
    {
        public int DealId { get; set; }
        public Deal Deal { get; set; }
        public int ServiceId { get; set; }
        public Product Product { get; set; }
    }
}