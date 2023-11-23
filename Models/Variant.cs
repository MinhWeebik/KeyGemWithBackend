using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyGem.Models
{
    public class Variant
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int Stock { get; set; }
        public int? ProductId { get; set; }
        public String ImageLink { get; set; }
        public double Price { get; set; }
    }
}