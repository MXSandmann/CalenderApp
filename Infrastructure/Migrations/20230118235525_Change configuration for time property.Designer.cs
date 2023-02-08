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
    [Migration("20230118235525_Change configuration for time property")]
    partial class Changeconfigurationfortimeproperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationCore.Models.UserEvent", b =>
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Recurrency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("UserEvents");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6646226a-2501-4fd0-83f6-7417d404de9b"),
                            AdditionalInfo = "test additionalInfo from seed",
                            Category = "test category from seed",
                            Description = "test description from seed",
                            EndDateTime = new DateTime(2023, 1, 19, 1, 55, 25, 27, DateTimeKind.Utc).AddTicks(5855),
                            ImageUrl = "test image url from seed",
                            Name = "Test name from seed",
                            Place = "test place from seed",
                            Recurrency = "Weekly",
                            StartDateTime = new DateTime(2023, 1, 18, 23, 55, 25, 27, DateTimeKind.Utc).AddTicks(5853)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
