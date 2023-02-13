using System;
using System.Collections.Generic;
using GolSkola.Models;
using Microsoft.EntityFrameworkCore;

namespace GolSkola.Data;

public partial class GoranpiralicContext : DbContext
{
    public GoranpiralicContext()
    {
    }

    public GoranpiralicContext(DbContextOptions<GoranpiralicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Galerija> Galerijas { get; set; }

    public virtual DbSet<Golmani> Golmanis { get; set; }

    public virtual DbSet<Vijesti> Vijestis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);initial catalog=goranpiralic; TrustServerCertificate=True; trusted_connection=yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Galerija>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__galerija__3213E83F5241C0D6");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Golmani>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__golmani__3213E83F40D54D8B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Vijesti>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vijesti__3213E83FDC62FAAF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
