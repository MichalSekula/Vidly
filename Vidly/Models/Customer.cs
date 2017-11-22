using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name="Date of Birth")]
        [Min18YearsIsAMember]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToCustomer { get; set; }

        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; }
        public MembershipTypes MembershipType { get; set; }
    }
}