﻿// <auto-generated />
using System;
using FactOfWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FactOfWork.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    partial class SqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("FactOfWork.Models.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.Property<string>("PathArhive")
                        .HasColumnType("longtext");

                    b.Property<string>("PathLoad")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FactOfWork.Models.LogTable", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateAction")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescriptionAction")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LogTables");
                });

            modelBuilder.Entity("FactOfWork.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Arhive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Category")
                        .HasColumnType("longtext");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<bool>("HaveOverPayments")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("HaveWorkBook")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumberArea")
                        .HasColumnType("int");

                    b.Property<string>("Patr")
                        .HasColumnType("longtext");

                    b.Property<int>("Pay_Month")
                        .HasColumnType("int");

                    b.Property<int>("Pay_Year")
                        .HasColumnType("int");

                    b.Property<int>("Report_Month")
                        .HasColumnType("int");

                    b.Property<int>("Report_Year")
                        .HasColumnType("int");

                    b.Property<string>("Snils")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("ZaprosId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("FactOfWork.Models.Zapros", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AddressOrganization")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DataQuery")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataResponse")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateProtocol")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NameOrganization")
                        .HasColumnType("longtext");

                    b.Property<string>("Note")
                        .HasColumnType("longtext");

                    b.Property<string>("NumberProtocol")
                        .HasColumnType("longtext");

                    b.Property<string>("PathFileName")
                        .HasColumnType("longtext");

                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<string>("RegNumberOrganization")
                        .HasColumnType("longtext");

                    b.Property<string>("UserNameModify")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Zaproses");
                });

            modelBuilder.Entity("FactOfWork.Models.Person", b =>
                {
                    b.HasOne("FactOfWork.Models.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("FactOfWork.Models.Zapros", b =>
                {
                    b.HasOne("FactOfWork.Models.Person", "Person")
                        .WithMany("Zaproses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("FactOfWork.Models.Person", b =>
                {
                    b.Navigation("Zaproses");
                });
#pragma warning restore 612, 618
        }
    }
}