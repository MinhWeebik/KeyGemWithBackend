using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyGem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        [Display(Name = "Customer Name")]
        public int PersonId { get; set; }
        public Product Product { get; set; }
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        public Variant Variant { get; set; }
        [Display(Name = "Variant")]
        public int VariantId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be greater than zero.")]
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        [Display(Name = "Select order state")]
        public int OrderStateId { get; set; }
        public OrderState OrderState { get; set; }
    }
}