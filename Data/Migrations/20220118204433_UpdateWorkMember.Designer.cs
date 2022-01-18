﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagement.Data;

namespace ProjectManagement.Migrations.Projects
{
    [DbContext(typeof(ProjectsContext))]
    [Migration("20220118204433_UpdateWorkMember")]
    partial class UpdateWorkMember
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectManagement.Models.Board", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoardDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BoardId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("ProjectManagement.Models.List", b =>
                {
                    b.Property<int>("ListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TemplateId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ListId");

                    b.HasIndex("BoardId");

                    b.HasIndex("TemplateId");

                    b.ToTable("List");
                });

            modelBuilder.Entity("ProjectManagement.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("MemberId");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("ProjectManagement.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ProjectId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectManagement.Models.ProjectMember", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("ProjectMember");
                });

            modelBuilder.Entity("ProjectManagement.Models.Template", b =>
                {
                    b.Property<int>("TemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TemplateId");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("ProjectManagement.Models.ViewModels.AddTemplateViewModel", b =>
                {
                    b.Property<int?>("TemplateId")
                        .HasColumnType("int");

                    b.HasIndex("TemplateId");

                    b.ToTable("AddTemplateViewModel");
                });

            modelBuilder.Entity("ProjectManagement.Models.Work", b =>
                {
                    b.Property<int>("WorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<int>("ListId")
                        .HasColumnType("int");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("WorkId");

                    b.HasIndex("ListId");

                    b.HasIndex("MemberId");

                    b.ToTable("Work");
                });

            modelBuilder.Entity("ProjectManagement.Models.WorkMember", b =>
                {
                    b.Property<int>("WorkId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("WorkId", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("WorkMember");
                });

            modelBuilder.Entity("ProjectManagement.Models.Board", b =>
                {
                    b.HasOne("ProjectManagement.Models.Project", "Project")
                        .WithMany("Boards")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectManagement.Models.List", b =>
                {
                    b.HasOne("ProjectManagement.Models.Board", "Boards")
                        .WithMany("Lists")
                        .HasForeignKey("BoardId");

                    b.HasOne("ProjectManagement.Models.Template", "Template")
                        .WithMany("Lists")
                        .HasForeignKey("TemplateId");

                    b.Navigation("Boards");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("ProjectManagement.Models.Project", b =>
                {
                    b.HasOne("ProjectManagement.Models.Member", "Manager")
                        .WithMany("Projects")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("ProjectManagement.Models.ProjectMember", b =>
                {
                    b.HasOne("ProjectManagement.Models.Member", "Member")
                        .WithMany("ProjectMembers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectManagement.Models.Project", "Project")
                        .WithMany("ProjectMembers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectManagement.Models.ViewModels.AddTemplateViewModel", b =>
                {
                    b.HasOne("ProjectManagement.Models.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("ProjectManagement.Models.Work", b =>
                {
                    b.HasOne("ProjectManagement.Models.List", "List")
                        .WithMany("Works")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectManagement.Models.Member", null)
                        .WithMany("Work")
                        .HasForeignKey("MemberId");

                    b.Navigation("List");
                });

            modelBuilder.Entity("ProjectManagement.Models.WorkMember", b =>
                {
                    b.HasOne("ProjectManagement.Models.Member", "Member")
                        .WithMany("WorkMembers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectManagement.Models.Work", "Work")
                        .WithMany("WorkMembers")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("ProjectManagement.Models.Board", b =>
                {
                    b.Navigation("Lists");
                });

            modelBuilder.Entity("ProjectManagement.Models.List", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("ProjectManagement.Models.Member", b =>
                {
                    b.Navigation("ProjectMembers");

                    b.Navigation("Projects");

                    b.Navigation("Work");

                    b.Navigation("WorkMembers");
                });

            modelBuilder.Entity("ProjectManagement.Models.Project", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("ProjectMembers");
                });

            modelBuilder.Entity("ProjectManagement.Models.Template", b =>
                {
                    b.Navigation("Lists");
                });

            modelBuilder.Entity("ProjectManagement.Models.Work", b =>
                {
                    b.Navigation("WorkMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
