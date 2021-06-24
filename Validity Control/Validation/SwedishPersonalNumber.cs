using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validity_Control.Models;

namespace Validity_Control.Validation
{
    public class SwedishPersonalNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string input && input.Length == 12)
            {
                var member = (Member)validationContext.ObjectInstance;
                var dateOfB = (member.DateOfBirth.ToString("yyyy")) + (member.DateOfBirth.ToString("MM")) + (member.DateOfBirth.ToString("dd"));
                string[] subs = dateOfB.Split('-');
                StringBuilder builder = new StringBuilder();
                foreach (string part in subs)
                {
                    builder.Append(part);
                }
                string resultOfBdd = builder.ToString();
                var fourDigits = input.Substring(8);
                if (int.TryParse(fourDigits, out int res))
                {
                    var personNumer = string.Concat(resultOfBdd, res);
                    var pesNum = member.PersonalNo;
                    if (pesNum == personNumer)
                    {
                        return ValidationResult.Success;

                    }
                    else return new ValidationResult("The personal ID doesn't match the age of birth");

                }
                return new ValidationResult("Please check the personalcode format, ex. '19901102xxxx'");

            }
            return new ValidationResult("Please check the personalcode format, ex. '19901102xxxx'");
        }
    }
}
