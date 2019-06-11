using System;
using System.Collections.Generic;
using System.Text;
using Artesanas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Artesanas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<SubTipo> SubTipo { get; set; }
        public DbSet<Maker> Maker { get; set; }
        public DbSet<StoreItem> StoreItem { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }


    }
}
