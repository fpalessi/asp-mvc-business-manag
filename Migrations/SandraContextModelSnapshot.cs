﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SandraConfecciones.Models;

#nullable disable

namespace SandraConfecciones.Migrations
{
    [DbContext(typeof(SandraContext))]
    partial class SandraContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SandraConfecciones.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("clienteId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Apellido")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("apellido");

                    b.Property<string>("Direccion")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("direccion");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("telefono");

                    b.HasKey("ClienteId")
                        .HasName("PK__Clientes__C2FF245D9FD3AC3D");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("SandraConfecciones.Models.Factura", b =>
                {
                    b.Property<int>("FacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("facturaId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacturaId"));

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int")
                        .HasColumnName("clienteId");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("total");

                    b.HasKey("FacturaId")
                        .HasName("PK__Facturas__AAF90221D5752801");

                    b.HasIndex("ClienteId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("SandraConfecciones.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("usuarioId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.HasKey("UsuarioId")
                        .HasName("PK__Usuarios__A5B1AB8E0BD2B007");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SandraConfecciones.Models.Factura", b =>
                {
                    b.HasOne("SandraConfecciones.Models.Cliente", "Cliente")
                        .WithMany("Facturas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Facturas__client__398D8EEE");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("SandraConfecciones.Models.Cliente", b =>
                {
                    b.Navigation("Facturas");
                });
#pragma warning restore 612, 618
        }
    }
}