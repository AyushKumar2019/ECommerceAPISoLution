using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product :BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]  // ✅ Fix precision
        public decimal Price { get; set; }
        public required string PictureUrl { get; set; } 
        public required string Type { get; set; }
        public required string Brand { get; set; }
        public int QuantityInStock { get; set;} = 0; 

    }
}
