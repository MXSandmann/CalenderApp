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
    [Migration("20230130234656_Update RecurrencyRule 2")]
    partial class UpdateRecurrencyRule2
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

                    b.Property<int>("EvenOdd")
                        .HasColumnType("integer");

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
                            Id = new Guid("12ba54ed-980f-41f4-a296-b74e6509caf3"),
                            AdditionalInfo = "test additionalInfo from seed",
                            Category = "test category from seed",
                            Date = new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1754),
                            Description = "test description from seed",
                            EndTime = new DateTime(2023, 1, 31, 1, 46, 56, 470, DateTimeKind.Utc).AddTicks(1733),
                            HasRecurrency = "Yes",
                            ImageUrl = "test image url from seed",
                            LastDate = new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1755),
                            Name = "Test name from seed",
                            Place = "test place from seed",
                            StartTime = new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1728)
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
