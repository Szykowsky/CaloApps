using CaloApps.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CaloApps.Data
{
    public class CaloContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Diet> Diets { get; set; }

        public CaloContext(DbContextOptions<CaloContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(t => t.Id);
                user.Property(t => t.Id).ValueGeneratedOnAdd();
                user.HasIndex(t => t.Login).IsUnique(true);
            });

            modelBuilder.Entity<Meal>(meal =>
            {
                meal.HasKey(t => t.Id);
                meal.Property(t => t.Id).ValueGeneratedOnAdd();
                meal.HasOne(t => t.Diet).WithMany().HasForeignKey(t => t.DietId);
            });

            modelBuilder.Entity<Diet>(diet =>
            {
                diet.HasKey(t => t.Id);
                diet.Property(t => t.Id).ValueGeneratedOnAdd();
                diet.HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId);
            });
        }
    }
}
