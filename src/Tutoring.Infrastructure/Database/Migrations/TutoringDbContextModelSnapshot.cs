﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tutoring.Infrastructure.Database;

#nullable disable

namespace Tutoring.Infrastructure.Database.Migrations
{
    [DbContext(typeof(TutoringDbContext))]
    partial class TutoringDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tutoring.Domain.Availabilities.Availability", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("From")
                        .HasColumnType("time without time zone");

                    b.Property<TimeOnly>("To")
                        .HasColumnType("time without time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Availability");
                });

            modelBuilder.Entity("Tutoring.Domain.Competences.Competence", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompetenceGroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("DetailedName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CompetenceGroupId");

                    b.ToTable("Competences", (string)null);
                });

            modelBuilder.Entity("Tutoring.Domain.Competences.CompetenceGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CompetencesGroups");
                });

            modelBuilder.Entity("Tutoring.Domain.Reviews.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews", (string)null);
                });

            modelBuilder.Entity("Tutoring.Domain.Subjects.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Subjects", (string)null);
                });

            modelBuilder.Entity("Tutoring.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Type").IsComplete(false).HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Tutoring.Domain.Users.BackOfficeUser", b =>
                {
                    b.HasBaseType("Tutoring.Domain.Users.User");

                    b.HasDiscriminator().HasValue("BackOfficeUser");
                });

            modelBuilder.Entity("Tutoring.Domain.Users.Student", b =>
                {
                    b.HasBaseType("Tutoring.Domain.Users.User");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Tutoring.Domain.Users.Tutor", b =>
                {
                    b.HasBaseType("Tutoring.Domain.Users.User");

                    b.HasDiscriminator().HasValue("Tutor");
                });

            modelBuilder.Entity("Tutoring.Domain.Availabilities.Availability", b =>
                {
                    b.HasOne("Tutoring.Domain.Users.User", null)
                        .WithMany("Availabilities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tutoring.Domain.Competences.Competence", b =>
                {
                    b.HasOne("Tutoring.Domain.Competences.CompetenceGroup", null)
                        .WithMany("Competences")
                        .HasForeignKey("CompetenceGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tutoring.Domain.Reviews.Review", b =>
                {
                    b.HasOne("Tutoring.Domain.Users.User", null)
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tutoring.Domain.Subjects.Subject", b =>
                {
                    b.HasOne("Tutoring.Domain.Users.Student", null)
                        .WithMany("Subjects")
                        .HasForeignKey("StudentId");

                    b.OwnsMany("Tutoring.Domain.Competences.CompetenceId", "CompetenceIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("SubjectId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("CompetenceId");

                            b1.HasKey("Id");

                            b1.HasIndex("SubjectId");

                            b1.ToTable("SubjectCompetenceIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SubjectId");
                        });

                    b.Navigation("CompetenceIds");
                });

            modelBuilder.Entity("Tutoring.Domain.Users.Tutor", b =>
                {
                    b.OwnsMany("Tutoring.Domain.Competences.CompetenceId", "CompetenceIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("TutorId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("CompetenceId");

                            b1.HasKey("Id");

                            b1.HasIndex("TutorId");

                            b1.ToTable("TutorCompetenceIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TutorId");
                        });

                    b.Navigation("CompetenceIds");
                });

            modelBuilder.Entity("Tutoring.Domain.Competences.CompetenceGroup", b =>
                {
                    b.Navigation("Competences");
                });

            modelBuilder.Entity("Tutoring.Domain.Users.User", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Tutoring.Domain.Users.Student", b =>
                {
                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
