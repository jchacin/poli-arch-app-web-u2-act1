using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore; 
using ProductAPI.Repositories.Data;  

namespace ProductAPI.Repositories.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
