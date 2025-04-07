using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderContent> OrderContent { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PromoCode> PromoCode { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserProduct> UserProduct { get; set; }
        public DbSet<Faq> Faq { get; set; }
    }
}