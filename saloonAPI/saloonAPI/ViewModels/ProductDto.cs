using saloonAPI.Models;
using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.ViewModels
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quentity { get; set; }
        public string Description { get; set; }
        public virtual ICollection<LikeDto> Likes { get; set; } = new List<LikeDto>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public string CreatedUserId { get; set; }
    }
}
