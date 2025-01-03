﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace insurance_partners_management_system.Models;

public partial class insurancepartnersdbContext : DbContext
{
    public insurancepartnersdbContext(DbContextOptions<insurancepartnersdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Partner> PartnerInfos { get; set; }

    public virtual DbSet<Policy> PolicyInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.IdPartner).HasName("PK__PartnerI__6A42E6696C969925");

            entity.Property(e => e.CreatedAtUtc).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.CroatianPin).IsFixedLength();
            entity.Property(e => e.Gender).IsFixedLength();
            entity.Property(e => e.PartnerNumber).IsFixedLength();
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.IdPolicy).HasName("PK__PolicyIn__8E3FBE8EAA4EC561");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}