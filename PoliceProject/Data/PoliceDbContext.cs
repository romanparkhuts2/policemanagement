using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PoliceProject.Models;

namespace PoliceProject.Data;

public partial class PoliceDbContext : DbContext
{
    public PoliceDbContext()
    {
    }

    public PoliceDbContext(DbContextOptions<PoliceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<ObjectType> ObjectTypes { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<RecoveredItem> RecoveredItems { get; set; }

    public virtual DbSet<Report> Reports { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ObjectType>(entity =>
        {
            entity.HasKey(e => e.IdObjectType).HasName("PK_ObjectTypes");
        });

        modelBuilder.Entity<RecoveredItem>(entity =>
        {
            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.RecoveredItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecoveredItem_Person");

            entity.HasOne(d => d.IdReportNavigation).WithMany(p => p.RecoveredItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecoveredItem_Report");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasOne(d => d.IdObjectTypeNavigation).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_ObjectType");

            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_Person");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
