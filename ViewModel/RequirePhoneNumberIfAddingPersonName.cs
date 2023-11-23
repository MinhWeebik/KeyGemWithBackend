using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class RequirePhoneNumberIfAddingPersonName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (EditUserModel)validationContext.ObjectInstance;
            if(user.PersonName != null && user.PhoneNumber == null)
            {
                return new ValidationResult("The Phone Number field is required if adding a person");
            }
            return ValidationResult.Success;
        }
    }
}