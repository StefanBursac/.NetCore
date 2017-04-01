using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPI.Models
{
    public class PersonsDb : IdentityDbContext<User>
    {
        public PersonsDb(DbContextOptions<PersonsDb> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> Persons { get; set; }

    }
}
