﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RandomJokes.Data;

namespace RandomJokes.Data.Migrations
{
    [DbContext(typeof(JokesDataContext))]
    partial class JokesDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RandomJokes.Data.Joke", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Punchline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Setup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SourceID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Jokes");
                });

            modelBuilder.Entity("RandomJokes.Data.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RandomJokes.Data.UserLikedJoke", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("JokeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.HasKey("UserID", "JokeID");

                    b.HasIndex("JokeID");

                    b.ToTable("UserLikedJokes");
                });

            modelBuilder.Entity("RandomJokes.Data.UserLikedJoke", b =>
                {
                    b.HasOne("RandomJokes.Data.Joke", "Joke")
                        .WithMany("UserLikedLikes")
                        .HasForeignKey("JokeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RandomJokes.Data.User", "User")
                        .WithMany("UserLikedJokes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Joke");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RandomJokes.Data.Joke", b =>
                {
                    b.Navigation("UserLikedLikes");
                });

            modelBuilder.Entity("RandomJokes.Data.User", b =>
                {
                    b.Navigation("UserLikedJokes");
                });
#pragma warning restore 612, 618
        }
    }
}