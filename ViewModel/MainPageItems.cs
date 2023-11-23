using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class MainPageItems
    {
        public List<Product> NewItemFirstPage {  get; set; }
        public List<Product> NewItemSecondPage { get; set; }
        public List<Product> BestSellerFirstPage { get; set; }
        public List<Product> BestSellerSecondPage { get; set; }
    }
}