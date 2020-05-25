using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopRock.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name ="Order Date")]
        public DateTime OrderDate { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name ="User ID")]
        public string UserId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(10)]
        public string Province { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name ="Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
