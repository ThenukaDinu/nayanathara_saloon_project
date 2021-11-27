using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<CustomerTelNumber> TelNumbers { get; set; } = new List<CustomerTelNumber>();
        public ICollection<Appoinment> Appoinments { get; set; } = new List<Appoinment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<ProductComment> ProductComments { get; set; } = new List<ProductComment>();
        public ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
        public ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
    }
}
