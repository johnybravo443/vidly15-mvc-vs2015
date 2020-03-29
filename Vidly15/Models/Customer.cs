using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly15.Models
{
    public class Customer
    {
        public int Id { get; set; } //implicitly marked as required by ASP.NET

        [Required(ErrorMessage = "Please enter Customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        //this is called a Navigation Property. it allows us to navigate from one type to other.
        //In this case from customer to its memberhip type.
        //Its helpful when want to load an object and its related object from DB.
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

    }
}