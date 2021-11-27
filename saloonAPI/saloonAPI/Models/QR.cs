using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class QR
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
