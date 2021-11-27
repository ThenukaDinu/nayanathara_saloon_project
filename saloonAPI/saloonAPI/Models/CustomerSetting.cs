﻿using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class CustomerSetting
    {
        public int Id { get; set; }
        public bool MarketingEmails { get; set; }
        public bool AllowProducts { get; set; }
        public bool Reminders { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
