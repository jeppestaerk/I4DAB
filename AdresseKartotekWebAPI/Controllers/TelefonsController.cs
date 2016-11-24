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

namespace AdresseKartotekWebAPI.Controllers
{
    public class TelefonsController : ApiController
    {
        private AdresseKartotekContext db = new AdresseKartotekContext();

        // GET: api/Telefons
        public IQueryable<TelefonDTO> GetTelefons()
        {
            var telefons = from p in db.Telefons
                         select new TelefonDTO()
                         {
                            TelefonType = p.TelefonType,
                            Telefonnummer = p.Telefonnummer
                         };

            return telefons;
        }

        // GET: api/Telefons/5
        [ResponseType(typeof(ExtendedTelefonDTO))]
        public IHttpActionResult GetTelefon(long id)
        {
            var telefon =
               db.Telefons.Where(p => p.TelefonID == id).Include(a => a.Person).
               Include(a => a.Person.Adresse).FirstOrDefault();

            if (telefon == null)
            {
                return NotFound();
            }

            var telefonDTO = new ExtendedTelefonDTO();

            telefonDTO.PersonDto = new PersonDTO
            {
                ID = telefon.Person.PersonID,
                Fornavn = telefon.Person.Fornavn,
                Mellemnavn = telefon.Person.Mellemnavn,
                Efternavn = telefon.Person.Efternavn
            };

            telefonDTO.TelefonType = telefon.TelefonType;
            telefonDTO.Telefonnummer = telefon.Telefonnummer;       

            return Ok(telefonDTO);
        }

        // PUT: api/Telefons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTelefon(long id, Telefon telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != telefon.TelefonID)
            {
                return BadRequest();
            }

            db.Entry(telefon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonExists(id))
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

        // POST: api/Telefons
        [ResponseType(typeof(Telefon))]
        public IHttpActionResult PostTelefon(Telefon telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Telefons.Add(telefon);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = telefon.TelefonID }, telefon);
        }

        // DELETE: api/Telefons/5
        [ResponseType(typeof(Telefon))]
        public IHttpActionResult DeleteTelefon(long id)
        {
            Telefon telefon = db.Telefons.Find(id);
            if (telefon == null)
            {
                return NotFound();
            }

            db.Telefons.Remove(telefon);
            db.SaveChanges();

            return Ok(telefon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TelefonExists(long id)
        {
            return db.Telefons.Count(e => e.TelefonID == id) > 0;
        }
    }
}