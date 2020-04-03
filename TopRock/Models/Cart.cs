using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopRock.Models
{
    public partial class Cart
    {
        [StringLength(100)]
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("Cart")]
        public virtual Product Product { get; set; }
    }
}
