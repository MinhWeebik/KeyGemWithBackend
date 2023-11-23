using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class CheckoutViewModel
    {
        public ApplicationUser User { get; set; }
        public Person Person { get; set; }
    }
}