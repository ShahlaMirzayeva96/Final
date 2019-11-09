using HandmadeFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.DAL
{
    public class AppDbContext:IdentityDbContext<UserRegister>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
                
        }
        public DbSet<HomeBanner> HomeBanners { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }
        public DbSet<BrandIcon> BrandIcons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<About> Abouts { get; set; }
    }
}
