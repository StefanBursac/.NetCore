using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyWebAPI.Models;

namespace MyWebAPI.Migrations
{
    [DbContext(typeof(PersonsDb))]
    [Migration("20170320162814_PersonDbMigration")]
    partial class PersonDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyWebAPI.Models.Person", b =>
                {
                    b.Property<int>("JMGB")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Occupation");

                    b.HasKey("JMGB");

                    b.ToTable("Persons");
                });
        }
    }
}
