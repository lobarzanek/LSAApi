﻿// <auto-generated />
using System;
using LSAApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LSAApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230624153458_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LSAApi.Models.ConfigStatus", b =>
                {
                    b.Property<int>("ConfigStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConfigStatusId"));

                    b.Property<string>("ConfigStatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConfigStatusId");

                    b.ToTable("ConfigStatuses");
                });

            modelBuilder.Entity("LSAApi.Models.Configuration", b =>
                {
                    b.Property<int>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConfigurationId"));

                    b.Property<int>("ConfigStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SwitchId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ConfigurationId");

                    b.HasIndex("ConfigStatusId");

                    b.HasIndex("SwitchId");

                    b.HasIndex("UserId");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("LSAApi.Models.ConfigurationVlan", b =>
                {
                    b.Property<int>("ConfigurationVlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConfigurationVlanId"));

                    b.Property<int>("ConfigurationId")
                        .HasColumnType("int");

                    b.Property<int>("VlanId")
                        .HasColumnType("int");

                    b.Property<int>("portNumber")
                        .HasColumnType("int");

                    b.HasKey("ConfigurationVlanId");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("VlanId");

                    b.ToTable("ConfigurationVlans");
                });

            modelBuilder.Entity("LSAApi.Models.Model", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelId"));

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelPortNumber")
                        .HasColumnType("int");

                    b.Property<int>("ProducentId")
                        .HasColumnType("int");

                    b.HasKey("ModelId");

                    b.HasIndex("ProducentId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("LSAApi.Models.Producent", b =>
                {
                    b.Property<int>("ProducentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProducentId"));

                    b.Property<string>("ProducentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProducentId");

                    b.ToTable("Producents");
                });

            modelBuilder.Entity("LSAApi.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LSAApi.Models.Section", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SectionId"));

                    b.Property<string>("SectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SectionId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("LSAApi.Models.Switch", b =>
                {
                    b.Property<int>("SwitchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SwitchId"));

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int?>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("SwitchIpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SwitchLogin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SwitchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SwitchNetbox")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SwitchPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SwitchStatusId")
                        .HasColumnType("int");

                    b.HasKey("SwitchId");

                    b.HasIndex("ModelId");

                    b.HasIndex("SectionId");

                    b.HasIndex("SwitchStatusId");

                    b.ToTable("Switches");
                });

            modelBuilder.Entity("LSAApi.Models.SwitchStatus", b =>
                {
                    b.Property<int>("SwitchStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SwitchStatusId"));

                    b.Property<string>("SwitchStatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SwitchStatusId");

                    b.ToTable("SwitchStatuses");
                });

            modelBuilder.Entity("LSAApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LSAApi.Models.Vlan", b =>
                {
                    b.Property<int>("VlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VlanId"));

                    b.Property<string>("VlanIpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VlanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VlanTag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VlanId");

                    b.ToTable("Vlans");
                });

            modelBuilder.Entity("LSAApi.Models.Configuration", b =>
                {
                    b.HasOne("LSAApi.Models.ConfigStatus", "ConfigStatus")
                        .WithMany("Configurations")
                        .HasForeignKey("ConfigStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSAApi.Models.Switch", "Switch")
                        .WithMany("Configurations")
                        .HasForeignKey("SwitchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSAApi.Models.User", "User")
                        .WithMany("Configurations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConfigStatus");

                    b.Navigation("Switch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LSAApi.Models.ConfigurationVlan", b =>
                {
                    b.HasOne("LSAApi.Models.Configuration", "Configuration")
                        .WithMany("ConfigurationVlans")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSAApi.Models.Vlan", "Vlan")
                        .WithMany("ConfigurationVlans")
                        .HasForeignKey("VlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");

                    b.Navigation("Vlan");
                });

            modelBuilder.Entity("LSAApi.Models.Model", b =>
                {
                    b.HasOne("LSAApi.Models.Producent", "Producent")
                        .WithMany("Models")
                        .HasForeignKey("ProducentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producent");
                });

            modelBuilder.Entity("LSAApi.Models.Switch", b =>
                {
                    b.HasOne("LSAApi.Models.Model", "Model")
                        .WithMany("Switchs")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSAApi.Models.Section", "Section")
                        .WithMany("Switchs")
                        .HasForeignKey("SectionId");

                    b.HasOne("LSAApi.Models.SwitchStatus", "SwitchStatus")
                        .WithMany("Switches")
                        .HasForeignKey("SwitchStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");

                    b.Navigation("Section");

                    b.Navigation("SwitchStatus");
                });

            modelBuilder.Entity("LSAApi.Models.User", b =>
                {
                    b.HasOne("LSAApi.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LSAApi.Models.ConfigStatus", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("LSAApi.Models.Configuration", b =>
                {
                    b.Navigation("ConfigurationVlans");
                });

            modelBuilder.Entity("LSAApi.Models.Model", b =>
                {
                    b.Navigation("Switchs");
                });

            modelBuilder.Entity("LSAApi.Models.Producent", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("LSAApi.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("LSAApi.Models.Section", b =>
                {
                    b.Navigation("Switchs");
                });

            modelBuilder.Entity("LSAApi.Models.Switch", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("LSAApi.Models.SwitchStatus", b =>
                {
                    b.Navigation("Switches");
                });

            modelBuilder.Entity("LSAApi.Models.User", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("LSAApi.Models.Vlan", b =>
                {
                    b.Navigation("ConfigurationVlans");
                });
#pragma warning restore 612, 618
        }
    }
}
