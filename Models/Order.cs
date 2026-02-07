using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cart_King.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
} 