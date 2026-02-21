using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TopImmo.Api.Data;

#nullable disable

namespace TopImmo.Api.Data.Migrations
{
    [DbContext(typeof(TopImmoDbContext))]
    partial class TopImmoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TopImmo.Api.Domain.Agent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<string>("Phone")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("agents", (string)null);
                });

            modelBuilder.Entity("TopImmo.Api.Domain.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AgentId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("AreaSqm")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<int>("Bathrooms")
                        .HasColumnType("integer");

                    b.Property<int>("Bedrooms")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<int>("OfferType")
                        .HasColumnType("integer");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("character varying(180)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("character varying(180)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(220)
                        .HasColumnType("character varying(220)");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("City");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("properties", (string)null);
                });

            modelBuilder.Entity("TopImmo.Api.Domain.Property", b =>
                {
                    b.HasOne("TopImmo.Api.Domain.Agent", "Agent")
                        .WithMany("Properties")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Agent");
                });

            modelBuilder.Entity("TopImmo.Api.Domain.Agent", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
