﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(RollerDbContext))]
    [Migration("20220329011549_RollHistoryInit")]
    partial class RollHistoryInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("Domain.Entities.DiceValue", b =>
                {
                    b.Property<Guid>("DiceValueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Pip")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RollHistoryId")
                        .HasColumnType("TEXT");

                    b.HasKey("DiceValueId");

                    b.HasIndex("RollHistoryId");

                    b.ToTable("DiceValues");
                });

            modelBuilder.Entity("Domain.Entities.RollHistory", b =>
                {
                    b.Property<Guid>("RollHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ChangedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiceNotation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RollHistoryId");

                    b.ToTable("RollHistories");
                });

            modelBuilder.Entity("Domain.Entities.DiceValue", b =>
                {
                    b.HasOne("Domain.Entities.RollHistory", "RollHistory")
                        .WithMany("DiceValues")
                        .HasForeignKey("RollHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RollHistory");
                });

            modelBuilder.Entity("Domain.Entities.RollHistory", b =>
                {
                    b.Navigation("DiceValues");
                });
#pragma warning restore 612, 618
        }
    }
}
