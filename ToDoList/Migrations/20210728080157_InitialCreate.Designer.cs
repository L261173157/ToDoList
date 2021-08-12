﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.Db;

namespace ToDoList.Migrations
{
    [DbContext(typeof(ThingsContext))]
    [Migration("20210728080157_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ToDoList.Models.Thing", b =>
                {
                    b.Property<int>("ThingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Done")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FinishedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Remind")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RemindTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ThingId");

                    b.ToTable("Things");
                });
#pragma warning restore 612, 618
        }
    }
}