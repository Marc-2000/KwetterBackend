﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TweetService.DAL.Context;

#nullable disable

namespace TweetService.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220614200700_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TweetService.BLL.Models.Hashtag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hashtags");
                });

            modelBuilder.Entity("TweetService.BLL.Models.TaggedUser", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TweetID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserID", "TweetID");

                    b.HasIndex("TweetID");

                    b.ToTable("TaggedUsers");
                });

            modelBuilder.Entity("TweetService.BLL.Models.Tweet", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int>("Retweets")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("Tweets");
                });

            modelBuilder.Entity("TweetService.BLL.Models.TweetHashtag", b =>
                {
                    b.Property<Guid>("TweetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HashtagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TweetId", "HashtagId");

                    b.HasIndex("HashtagId");

                    b.ToTable("TweetHashtags");
                });

            modelBuilder.Entity("TweetService.BLL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TweetService.BLL.Models.TaggedUser", b =>
                {
                    b.HasOne("TweetService.BLL.Models.Tweet", "Tweet")
                        .WithMany("TaggedUsers")
                        .HasForeignKey("TweetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tweet");
                });

            modelBuilder.Entity("TweetService.BLL.Models.TweetHashtag", b =>
                {
                    b.HasOne("TweetService.BLL.Models.Hashtag", "Hashtag")
                        .WithMany("TweetHashtags")
                        .HasForeignKey("HashtagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TweetService.BLL.Models.Tweet", "Tweet")
                        .WithMany("TweetHashtags")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hashtag");

                    b.Navigation("Tweet");
                });

            modelBuilder.Entity("TweetService.BLL.Models.Hashtag", b =>
                {
                    b.Navigation("TweetHashtags");
                });

            modelBuilder.Entity("TweetService.BLL.Models.Tweet", b =>
                {
                    b.Navigation("TaggedUsers");

                    b.Navigation("TweetHashtags");
                });
#pragma warning restore 612, 618
        }
    }
}