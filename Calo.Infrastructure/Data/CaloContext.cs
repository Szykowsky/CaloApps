﻿using Calo.Core.Entities;
using Calo.Domain.Entities;
using Calo.Domain.Entities.MetabolicRate;
using Microsoft.EntityFrameworkCore;

namespace Calo.Data;

public class CaloContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Meal> Meals { get; set; }
    public virtual DbSet<Diet> Diets { get; set; }
    public virtual DbSet<MetabolicRate> MetabolicRate { get; set; }
    public virtual DbSet<DailyStatus> DailyStatuses { get; set; }

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
            meal.HasOne(t => t.DailyStatus).WithMany(x => x.Meals).HasForeignKey(t => t.DailyStatusId);
            meal.Property(t => t.DailyStatusId).HasDefaultValue(null);
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
            metabolicRate.Property(t => t.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<DailyStatus>(dailyStatus =>
        {
            dailyStatus.HasKey(t => t.Id);
            dailyStatus.Property(t => t.Id).ValueGeneratedNever();
            dailyStatus.HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId);
        });
    }
}
