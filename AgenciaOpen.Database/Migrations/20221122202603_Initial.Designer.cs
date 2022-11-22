﻿// <auto-generated />
using System;
using AgenciaOpen.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgenciaOpen.Database.Migrations
{
    [DbContext(typeof(AgenciaOpenContext))]
    [Migration("20221122202603_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.11");

            modelBuilder.Entity("AgenciaOpen.Domain.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("AgenciaOpen.Domain.Models.ItemPedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PedidoId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("ItemsPedido");
                });

            modelBuilder.Entity("AgenciaOpen.Domain.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("NumeroPedido")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("AgenciaOpen.Domain.Models.ItemPedido", b =>
                {
                    b.HasOne("AgenciaOpen.Domain.Models.Pedido", null)
                        .WithMany("Items")
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("AgenciaOpen.Domain.Models.Pedido", b =>
                {
                    b.HasOne("AgenciaOpen.Domain.Models.Cliente", null)
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AgenciaOpen.Domain.Models.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("AgenciaOpen.Domain.Models.Pedido", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
