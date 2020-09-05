﻿using System.Collections.Generic;

namespace CRM.Core.Entities
{
    public class Deal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal TotalAmount { get; set; }

        public enum DealStage
        {
            New,
            Ongoing,
            Won,
            Lost
        }

        public DealStage Stage { get; set; }

        public string Description { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual int ContactId { get; set; }

        public virtual Company Company { get; set; }

        public virtual int CompanyId { get; set; }

        public virtual Salesman Salesman { get; set; }

        public virtual int SalesmanId { get; set; }

        public virtual List<DealProduct> DealsProducts { get; set; }
    }
}
