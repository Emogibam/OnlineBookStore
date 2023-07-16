using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
