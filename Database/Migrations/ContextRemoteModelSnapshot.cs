﻿// <auto-generated />
using System;
using Database.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(ContextRemote))]
    partial class ContextRemoteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Database.Models.Component.DictDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Audio")
                        .HasColumnType("longtext");

                    b.Property<string>("Bnc")
                        .HasColumnType("longtext");

                    b.Property<string>("Collins")
                        .HasColumnType("longtext");

                    b.Property<string>("Definition")
                        .HasColumnType("longtext");

                    b.Property<string>("Detail")
                        .HasColumnType("longtext");

                    b.Property<string>("Exchange")
                        .HasColumnType("longtext");

                    b.Property<string>("Frq")
                        .HasColumnType("longtext");

                    b.Property<string>("Oxford")
                        .HasColumnType("longtext");

                    b.Property<string>("Phonetic")
                        .HasColumnType("longtext");

                    b.Property<string>("Pos")
                        .HasColumnType("longtext");

                    b.Property<string>("Tag")
                        .HasColumnType("longtext");

                    b.Property<string>("Translation")
                        .HasColumnType("longtext");

                    b.Property<string>("Word")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("DictDbs");
                });

            modelBuilder.Entity("Database.Models.DoList.Thing", b =>
                {
                    b.Property<int>("ThingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("CreateTimeStamp")
                        .HasColumnType("bigint");

                    b.Property<bool>("Done")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FinishedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Remind")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("RemindTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UpdateTimeStamp")
                        .HasColumnType("bigint");

                    b.HasKey("ThingId");

                    b.ToTable("Things");
                });
#pragma warning restore 612, 618
        }
    }
}