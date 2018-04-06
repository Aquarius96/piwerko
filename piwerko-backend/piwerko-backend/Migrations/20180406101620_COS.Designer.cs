﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Piwerko.Api.Repo;
using System;

namespace Piwerko.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180406101620_COS")]
    partial class COS
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Piwerko.Api.Models.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConfirmationCode");

                    b.Property<string>("avatar_URL");

                    b.Property<string>("email");

                    b.Property<string>("firstname");

                    b.Property<bool>("isAdmin");

                    b.Property<bool>("isConfirmed");

                    b.Property<string>("lastname");

                    b.Property<string>("password");

                    b.Property<string>("phone");

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
