﻿// <auto-generated />
using System;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(UserEventDataContext))]
    [Migration("20230129221307_Update RecurrencyRule")]
    partial class UpdateRecurrencyRule
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationCore.Models.Entities.RecurrencyRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte>("CertainDays")
                        .HasColumnType("smallint");

                    b.Property<string>("MonthOfYear")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Recurrency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserEventId")
                        .HasColumnType("uuid");

                    b.Property<string>("WeekOfMonth")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserEventId")
                        .IsUnique();

                    b.ToTable("RecurrencyRules");
                });

            modelBuilder.Entity("ApplicationCore.Models.Entities.UserEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalInfo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HasRecurrency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("UserEvents");

                    b.HasData(
                        new
                        {
                            Id = new Guid("14881e12-e2f5-45f1-992b-698525eb628c"),
                            AdditionalInfo = "test additionalInfo from seed",
                            Category = "test category from seed",
                            Date = new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1143),
                            Description = "test description from seed",
                            EndTime = new DateTime(2023, 1, 30, 0, 13, 7, 572, DateTimeKind.Utc).AddTicks(1113),
                            HasRecurrency = "Yes",
                            ImageUrl = "test image url from seed",
                            LastDate = new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1143),
                            Name = "Test name from seed",
                            Place = "test place from seed",
                            StartTime = new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1109)
                        });
                });

            modelBuilder.Entity("ApplicationCore.Models.Entities.RecurrencyRule", b =>
                {
                    b.HasOne("ApplicationCore.Models.Entities.UserEvent", "UserEvent")
                        .WithOne("RecurrencyRule")
                        .HasForeignKey("ApplicationCore.Models.Entities.RecurrencyRule", "UserEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEvent");
                });

            modelBuilder.Entity("ApplicationCore.Models.Entities.UserEvent", b =>
                {
                    b.Navigation("RecurrencyRule");
                });
#pragma warning restore 612, 618
        }
    }
}
