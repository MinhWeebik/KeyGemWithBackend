using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class EditUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
        [Display(Name = "Select Person (can be empty)")]
        public string PersonName { get; set; }
        [RequirePhoneNumberIfAddingPersonName]
        [Display(Name = "Select Phone Number")]
        public string PhoneNumber { get; set; }
        public List<IdentityRole> Roles { get; set; }
        [Required]
        [Display(Name = "Select Role")]
        public string RoleName { get; set; }
    }
}