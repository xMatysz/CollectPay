﻿// <auto-generated />
using System;
using CollectPay.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CollectPay.Persistence.Migrations
{
    [DbContext(typeof(CollectPayDbContext))]
    [Migration("20240225130746_AddBillsAndPaymentsTables")]
    partial class AddBillsAndPaymentsTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CollectPay.Domain.BillAggregate.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Bills", (string)null);
                });

            modelBuilder.Entity("CollectPay.Domain.BillAggregate.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BillId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsCreatorIncluded")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("CollectPay.Domain.BillAggregate.Entities.Payment", b =>
                {
                    b.HasOne("CollectPay.Domain.BillAggregate.Bill", null)
                        .WithMany("Payments")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CollectPay.Domain.BillAggregate.ValueObjects.Amount", "Amount", b1 =>
                        {
                            b1.Property<Guid>("PaymentId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)")
                                .HasColumnName("Currency");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("Amount");

                            b1.HasKey("PaymentId");

                            b1.ToTable("Payments");

                            b1.WithOwner()
                                .HasForeignKey("PaymentId");
                        });

                    b.Navigation("Amount");
                });

            modelBuilder.Entity("CollectPay.Domain.BillAggregate.Bill", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
