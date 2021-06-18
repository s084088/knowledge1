﻿// <auto-generated />
using System;
using LiteEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LiteEntity.Migrations
{
    [DbContext(typeof(LiteDB))]
    [Migration("20201028020229_a2")]
    partial class a2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LiteEntity.Tables.EKP", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4")
                        .HasMaxLength(63);

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DeleteFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EntityID")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4");

                    b.Property<string>("KPID")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("EntityID");

                    b.HasIndex("KPID");

                    b.ToTable("EKP");
                });

            modelBuilder.Entity("LiteEntity.Tables.Entity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4")
                        .HasMaxLength(63);

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DeleteFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4")
                        .HasMaxLength(63);

                    b.Property<string>("Text")
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.ToTable("Entity");
                });

            modelBuilder.Entity("LiteEntity.Tables.KP", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4")
                        .HasMaxLength(63);

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DeleteFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAbstract")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4")
                        .HasMaxLength(63);

                    b.Property<string>("Text")
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.ToTable("KP");
                });

            modelBuilder.Entity("LiteEntity.Tables.KPR", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4")
                        .HasMaxLength(63);

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DeleteFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OriginID")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4");

                    b.Property<string>("TargetID")
                        .HasColumnType("varchar(63) CHARACTER SET utf8mb4");

                    b.Property<int>("Tightness")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("OriginID");

                    b.HasIndex("TargetID");

                    b.ToTable("KPR");
                });

            modelBuilder.Entity("LiteEntity.Tables.EKP", b =>
                {
                    b.HasOne("LiteEntity.Tables.Entity", "Entity")
                        .WithMany("EKPs")
                        .HasForeignKey("EntityID");

                    b.HasOne("LiteEntity.Tables.KP", "KP")
                        .WithMany("EKPs")
                        .HasForeignKey("KPID");
                });

            modelBuilder.Entity("LiteEntity.Tables.KPR", b =>
                {
                    b.HasOne("LiteEntity.Tables.KP", "Origin")
                        .WithMany("Extends")
                        .HasForeignKey("OriginID");

                    b.HasOne("LiteEntity.Tables.KP", "Target")
                        .WithMany("Preconditions")
                        .HasForeignKey("TargetID");
                });
#pragma warning restore 612, 618
        }
    }
}
