﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.Infrastructure.Persistence;

#nullable disable

namespace Server.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240401201657_Create_Database")]
    partial class Create_Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Server.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("image");

                    b.Property<bool>("IsSmart")
                        .HasColumnType("boolean")
                        .HasColumnName("is_smart");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("NextMaintenanceDate")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("next_maintenance_date");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date")
                        .HasColumnName("release_date");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("Products", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
