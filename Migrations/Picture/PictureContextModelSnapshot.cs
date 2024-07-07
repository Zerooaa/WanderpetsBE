﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wanderpets.Models;

#nullable disable

namespace Wanderpets.Migrations.Picture
{
    [DbContext(typeof(PictureContext))]
    partial class PictureContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Wanderpets.Models.PostPicture", b =>
                {
                    b.Property<int>("pictureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("pictureID"));

                    b.Property<byte[]>("Images")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("pictureID");

                    b.ToTable("PostImages");
                });

            modelBuilder.Entity("Wanderpets.Models.ProfilePicture", b =>
                {
                    b.Property<int>("ProfileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProfileID"));

                    b.Property<byte[]>("ProfilePic")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("ProfileID");

                    b.ToTable("ProfilePicture");
                });
#pragma warning restore 612, 618
        }
    }
}
