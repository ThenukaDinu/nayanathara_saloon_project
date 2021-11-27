using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<CustomerTelNumber> TelNumbers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<QR> QRs { get; set; }
        public DbSet<Appoinment> Appoinments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<UserCoupon> UserCoupons { get; set; }
        public DbSet<CustomerSetting> CustomerSettings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
