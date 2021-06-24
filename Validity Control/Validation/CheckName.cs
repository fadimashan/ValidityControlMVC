using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Validity_Control.Models;

namespace Validity_Control.Validation
{
    public class CheckName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            const string nullErrorMessage = "Name should not be empty!";
            const string matchErrorMessage = "First name and last name should not match!";
            if (value is string input && !String.IsNullOrWhiteSpace(input))
            {
                var model = (Member)validationContext.ObjectInstance;
                
                 if (model.LastName.ToLower() == input.ToLower())                
                    return new ValidationResult(matchErrorMessage);
                
                if (model.LastName != input)
                    return ValidationResult.Success;


            }
            return new ValidationResult(nullErrorMessage);
        }
    }
}
