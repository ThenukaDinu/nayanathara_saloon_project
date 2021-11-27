using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class UserProduct
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
