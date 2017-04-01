using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MyWebAPI.Controllers
{


    public class PersonsController : Controller
    {
        private PersonsDb Pdb;
        private UserManager<User> _userManager;
        public PersonsController(PersonsDb pdb, UserManager<User> userManager)
        {
            Pdb = pdb;
            _userManager = userManager;
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

        [HttpGet("persons/person/{id}")]

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

        [HttpPost("persons/addperson")]
        public JsonResult AddPerson([FromBody]Person person)
        {
            Pdb.Add(person);
            Pdb.SaveChanges();

            return new JsonResult(person);
        }

        [HttpDelete("persons/deleteperson/{id}")]
        public IActionResult DeletePerson(long id)
        {
            var personId = Pdb.Persons.FirstOrDefault(p => p.JMGB == id);

            if (personId == null)
            {
                return NotFound();
            }
            
            Pdb.Remove(personId);
            Pdb.SaveChanges();

            return Ok("Person is deleted");
        }

        [HttpPut("persons/updateperson/{id}")]
        public ActionResult UpdatePerson(long id, [FromBody] Person person)
        {
            if (person == null || person.JMGB != id)
            {
                return BadRequest();
            }

            var updatedPerson = Pdb.Persons.Find(id);
            if (person == null)
            {
                return NotFound();
            }
            updatedPerson.FirstName = person.FirstName;
            updatedPerson.LastName = person.LastName;
            updatedPerson.Gender = person.Gender;
            updatedPerson.Occupation = person.Occupation;

            Pdb.Update(updatedPerson);
            Pdb.SaveChanges();

            return Ok(updatedPerson);
        }

        [HttpPut("persons/updateperson2")]
        public ActionResult UpdatePerson2([FromBody] Person person)     
        {

            var updatedPerson = Pdb.Persons.Find(person.JMGB);

            updatedPerson.JMGB = person.JMGB;
            updatedPerson.FirstName = person.FirstName;
            updatedPerson.LastName = person.LastName;
            updatedPerson.Gender = person.Gender;
            updatedPerson.Occupation = person.Occupation;

            Pdb.Update(updatedPerson);
            Pdb.SaveChanges();

            return Ok(updatedPerson);

        }

//----------------- ------------------AddUser------------------------------------------//

        [HttpPost("adduser")]
        public async Task AddUser()
        {
            var user = new User();
            {
                user.UserName = "StefanBursac";
                user.Email = "stefan@stefan.com";
            };

            await _userManager.CreateAsync(user, "Passw0rd!");

        }
//-------------------------------------------------------------------------------------//
 
    }


}