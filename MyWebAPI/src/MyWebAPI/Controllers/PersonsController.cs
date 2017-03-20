using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Controllers
{
  

    public class PersonsController : Controller
    {
        private PersonsDb Pdb;

        

        public PersonsController(PersonsDb pdb)
        {
            Pdb = pdb;
        }

        [HttpGet]
        [Route("persons/createdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }

        [HttpGet("persons/showdatabase")]

        public IActionResult ShowDB()
        {
            //var Persons = from p in Pdb
            //              select p;

            return Ok(Pdb.Persons.ToList());
        }

        [HttpGet("persons/{id}")]

        public IActionResult SelectPerson(int id)
        {
            var personId = Pdb.Persons.FirstOrDefault(p=> p.JMGB == id);

            if (personId == null)
            {
                return NotFound();
            }

            return Ok(personId);

        }

        [HttpGet("persons/{searchString}")]

        public async Task<IActionResult> searchPerson(string searchString)
            {
            var persons = from p in Pdb.Persons
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                persons = persons.Where(s => s.FirstName.Contains(searchString));
            }
            return View(await persons.ToListAsync());

        }








    }
}
