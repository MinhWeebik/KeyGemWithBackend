using KeyGem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KeyGem.ViewModel
{
    public class NewUserViewModel
    {
        public RegisterViewModel RegisterModel { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string Title
        {
            get
            {
                return RegisterModel.Email != "" ? "Edit User" : "New User";
            }
        }
    }
}