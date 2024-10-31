﻿// <auto-generated />
using System;
using BusReservation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusReservation.Migrations
{
    [DbContext(typeof(BusReservationContext))]
    [Migration("20241031170005_Pablo1")]
    partial class Pablo1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusReservation.Models.Boleto", b =>
                {
                    b.Property<int>("BoletoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoletoId"));

                    b.Property<string>("CodigoQR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.HasKey("BoletoId");

                    b.HasIndex("ReservaId");

                    b.ToTable("Boleto");
                });

            modelBuilder.Entity("BusReservation.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("BusReservation.Models.Horario", b =>
                {
                    b.Property<int>("HorarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HorarioId"));

                    b.Property<int>("AsientosDisponibles")
                        .HasColumnType("int");

                    b.Property<int>("AsientosOcupados")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaHoraSalida")
                        .HasColumnType("datetime2");

                    b.Property<int>("RutaId")
                        .HasColumnType("int");

                    b.HasKey("HorarioId");

                    b.HasIndex("RutaId");

                    b.ToTable("Horario");
                });

            modelBuilder.Entity("BusReservation.Models.Reserva", b =>
                {
                    b.Property<int>("ReservaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservaId"));

                    b.Property<string>("Asiento")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<bool>("EstadoReserva")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaReserva")
                        .HasColumnType("datetime2");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.Property<int>("RutaId")
                        .HasColumnType("int");

                    b.HasKey("ReservaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("RutaId");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("BusReservation.Models.Ruta", b =>
                {
                    b.Property<int>("RutaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RutaId"));

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<TimeSpan>("Duracion")
                        .HasColumnType("time");

                    b.Property<string>("Origen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RutaId");

                    b.ToTable("Ruta");
                });

            modelBuilder.Entity("BusReservation.Models.Boleto", b =>
                {
                    b.HasOne("BusReservation.Models.Reserva", "Reserva")
                        .WithMany()
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("BusReservation.Models.Horario", b =>
                {
                    b.HasOne("BusReservation.Models.Ruta", "Ruta")
                        .WithMany("Horarios")
                        .HasForeignKey("RutaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ruta");
                });

            modelBuilder.Entity("BusReservation.Models.Reserva", b =>
                {
                    b.HasOne("BusReservation.Models.Cliente", "Cliente")
                        .WithMany("Reservas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusReservation.Models.Ruta", "Ruta")
                        .WithMany()
                        .HasForeignKey("RutaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Ruta");
                });

            modelBuilder.Entity("BusReservation.Models.Cliente", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("BusReservation.Models.Ruta", b =>
                {
                    b.Navigation("Horarios");
                });
#pragma warning restore 612, 618
        }
    }
}