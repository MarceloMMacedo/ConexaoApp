﻿// <auto-generated />
using System;
using ConexaoApp.FirmRegistry.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConexaoApp.FirmRegistry.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240506162109_Atualizacao04")]
    partial class Atualizacao04
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ConexaoApp.FirmRegistry.Models.Empresa", b =>
                {
                    b.Property<Guid>("EmpresaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Abertura")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Bairro")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("CapitalSocial")
                        .HasColumnType("longtext");

                    b.Property<string>("Cep")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Cnpj")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime?>("DataSituacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Logradouro")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Municipio")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NaturezaJuridica")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NomeFantasia")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Numero")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Porte")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Status")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Tipo")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("TipoPessoa")
                        .HasColumnType("int");

                    b.Property<string>("Uf")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime?>("UltimaAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EmpresaId");

                    b.ToTable("Empresas");
                });
#pragma warning restore 612, 618
        }
    }
}
