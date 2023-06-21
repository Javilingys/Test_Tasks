﻿// <auto-generated />
using System;
using DynSun.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DynSun.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DynSun.DbModels.WeatherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<short>("AirHumidity")
                        .HasColumnType("smallint");

                    b.Property<short?>("Cloudy")
                        .HasColumnType("smallint");

                    b.Property<DateOnly>("DateOnly")
                        .HasColumnType("date");

                    b.Property<short?>("H")
                        .HasColumnType("smallint");

                    b.Property<string>("HorizontalView")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Other")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Pressure")
                        .HasColumnType("smallint");

                    b.Property<float>("Td")
                        .HasColumnType("real");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<TimeOnly>("TimeOnly")
                        .HasColumnType("time without time zone");

                    b.Property<string>("WindDirection")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short?>("WindSpeed")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("WeatherModels");
                });
#pragma warning restore 612, 618
        }
    }
}
