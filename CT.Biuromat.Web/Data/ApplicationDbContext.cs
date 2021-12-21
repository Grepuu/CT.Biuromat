using System;
using System.Collections.Generic;
using System.Text;
using CT.Biuromat.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CT.Biuromat.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BuildingEntity> Buildings { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BuildingEntity>()
                .HasMany(n => n.Rooms)
                .WithOne(n => n.Building)
                .HasForeignKey(n => n.BuildingId);
        }
    }
}