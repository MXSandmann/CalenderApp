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
    [Migration("20230404211728_Add property Done")]
    partial class AddpropertyDone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationCore.Models.Entities.RecurrencyRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

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

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("HasRecurrency")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("InstructorId")
                        .HasColumnType("uuid");

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
                            Id = new Guid("4d342bd5-99d9-42c3-b214-649872d6554c"),
                            AdditionalInfo = "test additionalInfo from seed",
                            Category = "test category from seed",
                            Date = new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4188),
                            Description = "test description from seed",
                            Done = false,
                            EndTime = new DateTime(2023, 4, 4, 23, 17, 28, 368, DateTimeKind.Utc).AddTicks(4162),
                            HasRecurrency = true,
                            ImageUrl = "test image url from seed",
                            LastDate = new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4188),
                            Name = "Test name from seed",
                            Place = "test place from seed",
                            StartTime = new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4160)
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
