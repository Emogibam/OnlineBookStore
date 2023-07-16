using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public UserDTO User { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
