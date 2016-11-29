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
    public class AdressesController : ApiController
    {
        private AdresseKartotekContext db = new AdresseKartotekContext();

        // GET: api/Adresses
        public IQueryable<AdresseDTO> GetAdresses()
        {

            var adresse = from p in db.Adresses
                           select new AdresseDTO()
                           {
                               Husnummer = p.Husnummer,
                               Bynavn = p.Bynavn,
                               Postnummer = p.Postnummer,
                               Vejnavn = p.Vejnavn
                           };

            return adresse;
        }

        // GET: api/Adresses/5
        [ResponseType(typeof(ExtendedAdressDTO))]
        public IHttpActionResult GetAdresse(long id)
        {
            var adresse = db.Adresses.Where(p => p.AdresseID == id).Include(a => a.People).Include(a =>
                    a.AlternativAdresses).FirstOrDefault();
            if (adresse == null)
            {
                return NotFound();
            }
            var AdresseDTO = new ExtendedAdressDTO();
            AdresseDTO.Person = new List<PersonDTO>();
            AdresseDTO.Adresses = new List<AdresseDTO>();

            foreach (var person in adresse.People)
            {
             AdresseDTO.Person.Add(new PersonDTO()
             {
                 Efternavn = person.Efternavn,
                 Mellemnavn = person.Mellemnavn,
                 Fornavn = person.Fornavn
             });   
            }
            //foreach (var ad in adresse.AlternativAdresses)
            //{
            //    AdresseDTO.Adresses.Add(new AdresseDTO
            //    {
            //        Husnummer = ad.Adresse.Husnummer,
            //        Bynavn = ad.Adresse.Bynavn,
            //        Postnummer = ad.Adresse.Postnummer,
            //        Vejnavn = ad.Adresse.Vejnavn
            //    });
            //}
            AdresseDTO.Husnummer = adresse.Husnummer;
            AdresseDTO.Bynavn = adresse.Bynavn;
            AdresseDTO.Postnummer = adresse.Postnummer;
            AdresseDTO.Vejnavn = adresse.Vejnavn;


            return Ok(AdresseDTO);
        }

        // PUT: api/Adresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdresse(long id, Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adresse.AdresseID)
            {
                return BadRequest();
            }

            db.Entry(adresse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresseExists(id))
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

        // POST: api/Adresses
        [ResponseType(typeof(Adresse))]
        public IHttpActionResult PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Adresses.Add(adresse);
            db.SaveChanges();

            db.Entry(adresse).Reference(x => x.People).Load();

            var AdresseDTO = new AdresseDTO()
            {
                Vejnavn = adresse.Vejnavn,
                Bynavn = adresse.Bynavn,
                Husnummer = adresse.Husnummer,
                Postnummer = adresse.Postnummer
            };


            return CreatedAtRoute("DefaultApi", new { id = adresse.AdresseID }, AdresseDTO);
        }

        // DELETE: api/Adresses/5
        [ResponseType(typeof(Adresse))]
        public IHttpActionResult DeleteAdresse(long id)
        {
            Adresse adresse = db.Adresses.Find(id);
            if (adresse == null)
            {
                return NotFound();
            }

            db.Adresses.Remove(adresse);
            db.SaveChanges();

            return Ok(adresse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdresseExists(long id)
        {
            return db.Adresses.Count(e => e.AdresseID == id) > 0;
        }
    }
}