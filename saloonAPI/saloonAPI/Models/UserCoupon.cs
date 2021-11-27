﻿using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class UserCoupon
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
    }
}
