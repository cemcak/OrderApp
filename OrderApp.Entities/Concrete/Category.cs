using OrderApp.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApp.Entities.Concrete
{
    public class Category : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;

        public List<Product> Products { get; set; }
    }
}
