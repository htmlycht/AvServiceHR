using AvServiceHR.infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvServiceHR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly AdventureWorks2017Context _context;

        public PersonController(AdventureWorks2017Context context)
        {
            _context = context;
        }

        //todo
        //GET
        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            // Recupera tutte le persone dal database
            return _context.Person.ToList();

        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var person = _context.Person.FirstOrDefault(p => p.BusinessEntityId == id);
            return person != null ? $"Found: {person.FirstName} {person.LastName}" : "Not Found";
        }

        // POST api/<PersonController>
        // per far andare a buon fine la richiesta inserire una nuova entry in Person.BusinessEntity su AdventureWorks
        /*
         {
              "BusinessEntityId": 20778,
              "PersonType": "IN",
              "NameStyle": false,
              "Title": "Mr.",
              "FirstName": "John",
              "MiddleName": "A.",
              "LastName": "Doe",
              "Suffix": null,
              "EmailPromotion": 1,
              "AdditionalContactInfo": null,
              "Demographics": null,
              "Rowguid": "d271c1a0-f47e-4e15-8f68-2d6271cc8e12",
              "ModifiedDate": "2024-01-01T10:00:00"
           }
         
         */
        [HttpPost]
        // volendo poteva essere gestito che alla post di una person si inserisse anche una entry in BusinessEntity
        public void Post([FromBody] Person person)
        {
            _context.Person.Add(person);
            _context.SaveChanges();
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string name)
        {
            var person = _context.Person.FirstOrDefault(p => p.BusinessEntityId == id);
            if (person != null)
            {
                person.FirstName = name;

                _context.SaveChanges();
            }
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _context.Person.FirstOrDefault(p => p.BusinessEntityId == id);
            if (person != null)
            {
                try
                {
                    _context.Person.Remove(person);
                    _context.SaveChanges();
                    return NoContent(); // 204 No Content
                }
                catch (Exception ex)
                {
                    return Conflict(new { message = "Impossibile eliminare. Esistono record correlati." });
                }
            }
            return NotFound(); // 404 Not Found
        }

    }
}
