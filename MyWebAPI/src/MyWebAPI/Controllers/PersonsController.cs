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

        [HttpGet("person/{id}")]

        public IActionResult SelectPerson(long id)
        {
            var personId = Pdb.Persons.FirstOrDefault(p => p.JMGB == id);

            if (personId == null)
            {
                return NotFound();
            }

            return Ok(personId);
        }

        [HttpGet("persons")]

        public IActionResult SearchAndSortPersons([FromQuery]string searchString, [FromQuery]string orderBy, [FromQuery]int page, [FromQuery]int personsPerPage)
        {
            var persons = from p in Pdb.Persons
                          select p;

            if (searchString != null)
            {
                persons = persons.Where(s => s.FirstName.Contains(searchString));
            }

            if (orderBy == "ascending")
            {
                persons = persons.OrderBy(p => p.LastName);
            }
            else if (orderBy == "descending")
            {
                persons = persons.OrderByDescending(p => p.LastName);
            }

            personsPerPage = 2;
            if (page > 0)
            {
                persons = persons.Skip((page - 1) * personsPerPage).Take(personsPerPage);
            }
            return Ok(persons);
        }

        [HttpPost("persons/login")]

        public JsonResult Login(string user, string pass)
        {
            return new JsonResult(null);
        }

        [HttpPost("persons/Add")]
        public JsonResult Add(Person person)

        {
        }

    }
    

}