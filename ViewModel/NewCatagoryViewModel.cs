using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class NewCatagoryViewModel
    {
        public Catagories Catagory { get; set; }
        public string Title
        {
            get
            {
                return Catagory.Id != 0 ? "Edit Catagory" : "New Catagory";
            }
        }
    }
}