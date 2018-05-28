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
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Piwerko.Api.Models.DB.Beer", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("added_by");

                    b.Property<double>("alcohol");

                    b.Property<int>("breweryId");

                    b.Property<string>("description");

                    b.Property<double>("ibu");

                    b.Property<bool>("isConfirmed");

                    b.Property<string>("name");

                    b.Property<string>("photo_URL");

                    b.Property<double>("servingTemp");

                    b.Property<string>("type");

                    b.HasKey("id");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("Piwerko.Api.Models.DB.Brewery", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("city");

                    b.Property<string>("description");

                    b.Property<bool>("isConfirmed");

                    b.Property<string>("name");

                    b.Property<string>("photo_URL");

                    b.Property<string>("street");

                    b.Property<string>("streetNumber");

                    b.Property<string>("web_Url");

                    b.HasKey("id");

                    b.ToTable("Breweries");
                });

            modelBuilder.Entity("Piwerko.Api.Models.DB.Comment", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DateTime");

                    b.Property<int>("beerId");

                    b.Property<int>("breweryId");

                    b.Property<string>("content");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Piwerko.Api.Models.DB.FavoriteBeer", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("beer_id");

                    b.Property<int>("user_id");

                    b.HasKey("id");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Piwerko.Api.Models.DB.Rate", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("beerId");

                    b.Property<int>("userId");

                    b.Property<double>("value");

                    b.HasKey("id");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("Piwerko.Api.Models.DB.User", b =>
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

                    b.Property<string>("salt");

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
