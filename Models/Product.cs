using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeyGem.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name{ get; set; }
        
        [Required]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        
        public Catagories Catagory { get; set; }
        [Required]
        [Display(Name = "Catagory")]
        public int CatagoriesId { get; set; }
        public List<Variant> Variants { get; set; } 
        public int TotalStock { get; set; }
    }
}