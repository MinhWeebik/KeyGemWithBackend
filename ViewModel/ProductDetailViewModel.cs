using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class ProductDetailViewModel
    {
        public List<Product> RecommendProducts { get; set; }
        public Product Product { get; set; }
    }
}