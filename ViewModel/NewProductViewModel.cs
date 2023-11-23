using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class NewProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Catagories> Catagory { get; set; }
        public string Title
        {
            get
            {
                return Product.Id != 0 ? "Edit Product" : "New Product";
            }
        }
    }
}