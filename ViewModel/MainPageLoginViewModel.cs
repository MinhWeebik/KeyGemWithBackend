using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class MainPageLoginViewModel
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public Person Person { get; set; }
        public string CurrentEmail { get; set; }
    }
}