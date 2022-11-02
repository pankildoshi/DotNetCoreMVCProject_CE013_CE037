using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Billlocal> Billlocal { get; set; }

        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillItem> BillItem { get; set; }

        public DbSet<Admin> Admin { get; set; }
    }

}
