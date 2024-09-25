﻿// <auto-generated />
using System;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240711095726_frist_migration")]
    partial class frist_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityLayer.Entites.MatchDates", b =>
                {
                    b.Property<int>("MatchDatesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchDatesID"));

                    b.Property<int>("Displacement")
                        .HasColumnType("int");

                    b.Property<int>("DisplacementScore")
                        .HasColumnType("int");

                    b.Property<int>("HomeOwner")
                        .HasColumnType("int");

                    b.Property<int>("HomeOwnerScore")
                        .HasColumnType("int");

                    b.Property<string>("MatchDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WinRate")
                        .HasColumnType("int");

                    b.Property<int>("Winning")
                        .HasColumnType("int");

                    b.HasKey("MatchDatesID");

                    b.ToTable("MatchDates");
                });

            modelBuilder.Entity("EntityLayer.Entites._pointsService", b =>
                {
                    b.Property<int>("_pointsServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_pointsServiceID"));

                    b.Property<int>("Draws")
                        .HasColumnType("int");

                    b.Property<int>("GoalDifference")
                        .HasColumnType("int");

                    b.Property<int>("GoalsAgainst")
                        .HasColumnType("int");

                    b.Property<int>("GoalsFor")
                        .HasColumnType("int");

                    b.Property<string>("League")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<int>("Played")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<double>("WinRate")
                        .HasColumnType("float");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("_pointsServiceID");

                    b.ToTable("_pointsService");
                });

            modelBuilder.Entity("EntityLayer.Entites.Teams", b =>
                {
                    b.Property<int>("TeamsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamsID"));

                    b.Property<DateTime>("FoundingYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MatchDatesID")
                        .HasColumnType("int");

                    b.Property<string>("TeamColours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("_pointsServiceID")
                        .HasColumnType("int");

                    b.HasKey("TeamsID");

                    b.HasIndex("MatchDatesID");

                    b.HasIndex("_pointsServiceID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("EntityLayer.Entites.Teams", b =>
                {
                    b.HasOne("EntityLayer.Entites.MatchDates", "MatchDates")
                        .WithMany("Teams")
                        .HasForeignKey("MatchDatesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Entites._pointsService", "_pointsService")
                        .WithMany("Teams")
                        .HasForeignKey("_pointsServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MatchDates");

                    b.Navigation("_pointsService");
                });

            modelBuilder.Entity("EntityLayer.Entites.MatchDates", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("EntityLayer.Entites._pointsService", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
