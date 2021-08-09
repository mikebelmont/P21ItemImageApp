using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ItemApp.Models;

namespace ItemApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ItemApp.Models.Image> Image { get; set; }
        public DbSet<ItemApp.Models.P21Item> P21Item { get; set; }
    }
}
