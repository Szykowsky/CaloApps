using Calo.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calo.Data
{
    public class CaloContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Diet> Diets { get; set; }
        public virtual DbSet<MetabolicRate> MetabolicRate { get; set; }

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
                meal.HasOne(t => t.Diet).WithMany(x => x.Meals).HasForeignKey(t => t.DietId);
            });

            modelBuilder.Entity<Diet>(diet =>
            {
                diet.HasKey(t => t.Id);
                diet.Property(t => t.Id).ValueGeneratedOnAdd();
                diet.HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId);
            });

            modelBuilder.Entity<MetabolicRate>(metabolicRate =>
            {
                metabolicRate.HasKey(t => t.Id);
                metabolicRate.Property(t => t.Id).ValueGeneratedOnAdd();
                metabolicRate.HasOne(t => t.User).WithMany(x => x.MetabolicRates).HasForeignKey(t => t.UserId);
                metabolicRate.Property(t => t.Gender).HasConversion(
                        v => v.ToString(),
                        v => (Gender)Enum.Parse(typeof(Gender), v));
                metabolicRate.Property(t => t.Activity).HasConversion(
                        v => v.ToString(),
                        v => (Activity)Enum.Parse(typeof(Activity), v));
                metabolicRate.Property(t => t.Formula).HasConversion(
                        v => v.ToString(),
                        v => (Formula)Enum.Parse(typeof(Formula), v));
            });
        }
    }
}
