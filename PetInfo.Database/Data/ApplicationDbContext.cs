using Microsoft.EntityFrameworkCore;
using PetInfo.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(e => e.AnimalType)
                .WithMany(e => e.Pets)
                .HasForeignKey(e => e.AnimalTypeId)
                .OnDelete(DeleteBehavior.Cascade);  //One to many relationship between Pet and AnimalType class
        }
    }
}
