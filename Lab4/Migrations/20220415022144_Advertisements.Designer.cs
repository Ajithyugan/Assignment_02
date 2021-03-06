// <auto-generated />
using System;
using Assignment02NET.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment02NET.Migrations
{
    [DbContext(typeof(MarketDbContext))]
    [Migration("20220415022144_Advertisements")]
    partial class Advertisements
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Assignment_02.Models.Advertisements", b =>
                {
                    b.Property<int>("advertisementID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("advertisementID"), 1L, 1);

                    b.Property<string>("BrokerageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("advertisementID");

                    b.HasIndex("BrokerageId");

                    b.ToTable("Advertisements", (string)null);
                });

            modelBuilder.Entity("Assignment02NET.Models.Brokerage", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Fee")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Brokerages", (string)null);
                });

            modelBuilder.Entity("Assignment02NET.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("Assignment02NET.Models.Subscription", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("BrokerageId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClientId", "BrokerageId");

                    b.HasIndex("BrokerageId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Assignment_02.Models.Advertisements", b =>
                {
                    b.HasOne("Assignment02NET.Models.Brokerage", "Brokerage")
                        .WithMany("Advertisements")
                        .HasForeignKey("BrokerageId");

                    b.Navigation("Brokerage");
                });

            modelBuilder.Entity("Assignment02NET.Models.Subscription", b =>
                {
                    b.HasOne("Assignment02NET.Models.Brokerage", "Brokerage")
                        .WithMany("Subscriptions")
                        .HasForeignKey("BrokerageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment02NET.Models.Client", "Client")
                        .WithMany("Subscriptions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brokerage");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Assignment02NET.Models.Brokerage", b =>
                {
                    b.Navigation("Advertisements");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("Assignment02NET.Models.Client", b =>
                {
                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
