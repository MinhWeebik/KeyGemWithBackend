using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class NewOrderViewModel
    {
        public List<OrderState> OrderStates { get; set; }
        public Order order { get; set; }
    }
}