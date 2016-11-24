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

namespace AdresseKartotekWebAPI.Controllers
{
    public class AlternativAdressesController : ApiController
    {
        private AdresseKartotekContext db = new AdresseKartotekContext();

        // GET: api/AlternativAdresses
        public IQueryable<AlternativAdresse> GetAlternativAdresses()
        {
            return db.AlternativAdresses;
        }

        // GET: api/AlternativAdresses/5
        [ResponseType(typeof(AlternativAdresse))]
        public IHttpActionResult GetAlternativAdresse(long id)
        {
            AlternativAdresse alternativAdresse = db.AlternativAdresses.Find(id);
            if (alternativAdresse == null)
            {
                return NotFound();
            }

            return Ok(alternativAdresse);
        }

        // PUT: api/AlternativAdresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlternativAdresse(long id, AlternativAdresse alternativAdresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alternativAdresse.PersonID)
            {
                return BadRequest();
            }

            db.Entry(alternativAdresse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlternativAdresseExists(id))
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

        // POST: api/AlternativAdresses
        [ResponseType(typeof(AlternativAdresse))]
        public IHttpActionResult PostAlternativAdresse(AlternativAdresse alternativAdresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AlternativAdresses.Add(alternativAdresse);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AlternativAdresseExists(alternativAdresse.PersonID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = alternativAdresse.PersonID }, alternativAdresse);
        }

        // DELETE: api/AlternativAdresses/5
        [ResponseType(typeof(AlternativAdresse))]
        public IHttpActionResult DeleteAlternativAdresse(long id)
        {
            AlternativAdresse alternativAdresse = db.AlternativAdresses.Find(id);
            if (alternativAdresse == null)
            {
                return NotFound();
            }

            db.AlternativAdresses.Remove(alternativAdresse);
            db.SaveChanges();

            return Ok(alternativAdresse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlternativAdresseExists(long id)
        {
            return db.AlternativAdresses.Count(e => e.PersonID == id) > 0;
        }
    }
}