using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AdresseKartotekWebAPI;
using AdresseKartotekWebAPI.Models;
using Antlr.Runtime.Misc;


namespace AdresseKartotekWebAPI.Controllers
{
    public class PeopleController : ApiController
    {
        private AdresseKartotekContext db = new AdresseKartotekContext();

        // GET: api/People
        public IQueryable<PersonDTO> GetPeople()
        {
            var people = from b in db.People
                select new PersonDTO()
                {
                   ID = b.PersonID,
                   Fornavn = b.Fornavn,
                   Mellemnavn = b.Mellemnavn,
                   Efternavn = b.Efternavn,

                };
            return people;
        }

        // GET: api/People/5
        [ResponseType(typeof(ExtendedPersonDTO))]
        public IHttpActionResult GetPerson(long id)
        {
            var person =
                db.People.Where(p => p.PersonID == id).Include(a => a.Adresse).
                Include(a => a.Telefons).FirstOrDefault();

            var PersonDTO = new ExtendedPersonDTO();

            PersonDTO.Fornavn = person.Fornavn;
            PersonDTO.Mellemnavn = person.Mellemnavn;
            PersonDTO.Efternavn = person.Efternavn;

            PersonDTO.Telefons = new List<TelefonDTO>();
            PersonDTO.alternativer = new List<AdresseDTO>();

            foreach (var phone in person.Telefons)
            {
                PersonDTO.Telefons.Add(new TelefonDTO()
                {
                   Telefonnummer = phone.Telefonnummer,
                   TelefonType = phone.TelefonType            
                });
            }
            foreach (var adresse in person.AlternativAdresses)
            {
                PersonDTO.alternativer.Add(new AdresseDTO()
                {
                    Bynavn = adresse.Adresse.Bynavn,
                    Husnummer = adresse.Adresse.Husnummer,
                    Postnummer = adresse.Adresse.Postnummer,
                    Vejnavn = adresse.Adresse.Vejnavn,              
                });
            }      

            return Ok(PersonDTO);
        }
            
        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(long id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.PersonID)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/People
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);
            db.SaveChanges();
          
            return CreatedAtRoute("DefaultApi", new { id = person.PersonID }, person);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(long id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(long id)
        {
            return db.People.Count(e => e.PersonID == id) > 0;
        }
    }
}