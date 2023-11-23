using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class NewPersonViewModel
    {
        public Person Person { get; set; }
        public string Title
        {
            get
            {
                return Person.Id != 0 ? "Edit Person" : "New Person";
            }
        }
    }
}