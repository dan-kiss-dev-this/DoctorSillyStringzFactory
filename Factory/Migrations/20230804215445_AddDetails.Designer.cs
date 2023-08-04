﻿// <auto-generated />
using Factory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Factory.Migrations
{
    [DbContext(typeof(FactoryContext))]
    [Migration("20230804215445_AddDetails")]
    partial class AddDetails
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Factory.Models.AuthorizationJoin", b =>
                {
                    b.Property<int>("AuthorizationJoinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EngineerId")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.HasKey("AuthorizationJoinId");

                    b.HasIndex("EngineerId");

                    b.HasIndex("MachineId");

                    b.ToTable("AuthorizationJoins");
                });

            modelBuilder.Entity("Factory.Models.Engineer", b =>
                {
                    b.Property<int>("EngineerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("EngineerId");

                    b.ToTable("Engineers");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("MachineId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Factory.Models.AuthorizationJoin", b =>
                {
                    b.HasOne("Factory.Models.Engineer", "Engineer")
                        .WithMany("AuthorizationJoins")
                        .HasForeignKey("EngineerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.Models.Machine", "Machine")
                        .WithMany("AuthorizationJoins")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Engineer");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Factory.Models.Engineer", b =>
                {
                    b.Navigation("AuthorizationJoins");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.Navigation("AuthorizationJoins");
                });
#pragma warning restore 612, 618
        }
    }
}
