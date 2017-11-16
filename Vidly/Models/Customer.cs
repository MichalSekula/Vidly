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
        public string Name { get; set; }
        public bool IsSubscribedToCustomer { get; set; }
        public byte MembershipTypeId { get; set; }
        public MembershipTypes MembershipType { get; set; }
    }
}