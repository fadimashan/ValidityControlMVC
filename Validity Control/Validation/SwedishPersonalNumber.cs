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
                    int lastDigit = (int)Char.GetNumericValue(pesNum[11]);
                    var checksum = CheckPN(pesNum);
                    if (pesNum == personNumer)
                    {
                        if (checksum == lastDigit)
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                             return new ValidationResult("The personal ID not correct!");

                        }

                    }
                    else return new ValidationResult("The personal ID doesn't match the age of birth");

                }
                return new ValidationResult("Please check the personalcode format, ex. '19901102xxxx'");

            }
            return new ValidationResult("Please check the personalcode format, ex. '19901102xxxx'");
        }

        public static int CheckPN(string str)
        {
            int[] arryOfPN = new int[12];
            int totalOfDigits = 0;
            arryOfPN = Array.ConvertAll(str.ToCharArray(), x => (int)x - 48);

            for (int i = 0; i < 11; i++)
            {
                int[] tempUpTen = new int[2];

                if (i >= 2 && i % 2 == 0)
                {

                    if (arryOfPN[i] * 2 < 10)
                    {
                        totalOfDigits = totalOfDigits + arryOfPN[i] * 2;
                    }
                    else
                    {
                        tempUpTen[0] = 1;
                        tempUpTen[1] = arryOfPN[i] * 2 - 10;
                        totalOfDigits = totalOfDigits + tempUpTen[0] + tempUpTen[1];
                    }
                }

                if (i >= 2 && i % 2 != 0)
                {
                    totalOfDigits = totalOfDigits + arryOfPN[i];
                }
            }

            int final = ((totalOfDigits % 10) - 10) * (-1) % 10;
            Console.WriteLine($"Last digit: {arryOfPN[11]} = Total: {final} ");
            return final;

        }
    }
}

