using OrderApp.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrderApp.Entities.Concrete
{
    public class Order : ITable
    {
        public enum OrderStatus
        {
            Approved = 1,
            Rejected = 2,
            WaitingForApproval = 3
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
