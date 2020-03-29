using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly15.Models;

namespace Vidly15.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; } //implicitly marked as required by ASP.NET

        [Required(ErrorMessage = "Please enter Customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        public byte MembershipTypeId { get; set; }
    }
}