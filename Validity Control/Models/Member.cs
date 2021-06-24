using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Validity_Control.Validation;

namespace Validity_Control.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Name")]
        [CheckName]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Surname")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Personal ID")]
        [Remote(action: "VerifyPersonalNumber", controller: "Members")]
        [SwedishPersonalNumber]
        public string PersonalNo { get; set; }

        [Required]
        [DisplayName("Date of birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]
        public DateTime DateOfBirth { get; set; }

    }
}
