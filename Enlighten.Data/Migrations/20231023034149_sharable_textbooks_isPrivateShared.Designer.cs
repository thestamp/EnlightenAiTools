﻿// <auto-generated />
using Enlighten.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enlighten.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231023034149_sharable_textbooks_isPrivateShared")]
    partial class sharable_textbooks_isPrivateShared
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Enlighten.Data.Models.Configuration.GptDataSettingsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InquirePrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InquireSystemMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizAnswerPrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizQuestionPrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizSystemMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GptDataSettings");
                });

            modelBuilder.Entity("Enlighten.Data.Models.Textbook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InquirePrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InquireSystemMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrivateShared")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrivateShareId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizAnswerPrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizQuestionPrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizSystemMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Textbooks");
                });

            modelBuilder.Entity("Enlighten.Data.Models.TextbookUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InquirePrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InquireSystemMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizAnswerPrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizQuestionPrompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizSystemMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TextbookId")
                        .HasColumnType("int");

                    b.Property<string>("TopicList")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TextbookId");

                    b.ToTable("TextbookUnits");
                });

            modelBuilder.Entity("Enlighten.Data.Models.TextbookUnit", b =>
                {
                    b.HasOne("Enlighten.Data.Models.Textbook", "Textbook")
                        .WithMany("Units")
                        .HasForeignKey("TextbookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Textbook");
                });

            modelBuilder.Entity("Enlighten.Data.Models.Textbook", b =>
                {
                    b.Navigation("Units");
                });
#pragma warning restore 612, 618
        }
    }
}
