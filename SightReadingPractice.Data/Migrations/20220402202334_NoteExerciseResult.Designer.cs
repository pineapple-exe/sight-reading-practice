// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SightReadingPractice.Data;

namespace SightReadingPractice.Data.Migrations
{
    [DbContext(typeof(SightReadingPracticeDbContext))]
    [Migration("20220402202334_NoteExerciseResult")]
    partial class NoteExerciseResult
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SightReadingPractice.Domain.Entities.NoteExerciseResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualTone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("SeptimaArea")
                        .HasColumnType("int");

                    b.Property<bool>("Success")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("NoteExerciseResults");
                });
#pragma warning restore 612, 618
        }
    }
}
