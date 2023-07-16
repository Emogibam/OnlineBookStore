using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DTOs
{
    public class OrderItemDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public BookDTO Book { get; set; }
    }
}
