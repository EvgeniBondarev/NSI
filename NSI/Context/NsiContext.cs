using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NSI.Models;
using Attribute = NSI.Models.Attribute;

namespace NSI.Context;

public partial class NsiContext : DbContext
{
    public NsiContext()
    {
    }

    public NsiContext(DbContextOptions<NsiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Attribute> Attributes { get; set; }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<DetailType> DetailTypes { get; set; }

    public virtual DbSet<Normative> Normatives { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-QAU182Q\\SQLEXPRESS;Database=NSI;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attribut__3214EC27C821A995");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Details__3214EC2707F66B1F");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NormativeId).HasColumnName("Normative_ID");

            entity.HasOne(d => d.Normative).WithMany(p => p.Details)
                .HasForeignKey(d => d.NormativeId)
                .HasConstraintName("FK__Details__Normati__3F466844");
        });

        modelBuilder.Entity<DetailType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetailTy__3214EC2708F8CA2D");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Normative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Normativ__3214EC27ACA1579D");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnitOfMeasure)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AttributeNavigation).WithMany(p => p.Normatives)
                .HasForeignKey(d => d.Attribute)
                .HasConstraintName("FK__Normative__Attri__3C69FB99");

            entity.HasOne(d => d.DetailTypeNavigation).WithMany(p => p.Normatives)
                .HasForeignKey(d => d.DetailType)
                .HasConstraintName("FK__Normative__Detai__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
