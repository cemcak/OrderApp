using OrderApp.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApp.Entities.Concrete
{
    public class Customer : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;

        public List<Order> Orders { get; set; }
    }
}
