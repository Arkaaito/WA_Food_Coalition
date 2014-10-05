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
using API.Models;
using API.Services;

namespace API.Controllers
{
    public class PickupsController : ApiController
    {
        private FoodCoalitionAppContext db = new FoodCoalitionAppContext();

        // GET: api/Pickups
        public IQueryable<Pickup> GetPickups()
        {
            return db.Pickups;
        }

        // GET: api/Pickups/5
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult GetPickup(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return NotFound();
            }

            return Ok(pickup);
        }

        // GET: api/Pickups/{lat}/{lng}/{range}
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult GetPickup(double latitude, double longitude, double range)
        {
            var pickups = db.Pickups.Where(p => Distance.Meters(p.Latitude, p.Longitude, latitude, longitude) <= range);
            return Ok(pickups);
        }

        // PUT: api/Pickups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPickup(int id, Pickup pickup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pickup.Id)
            {
                return BadRequest();
            }

            db.Entry(pickup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PickupExists(id))
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

        // POST: api/Pickups
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult PostPickup(Pickup pickup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pickups.Add(pickup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pickup.Id }, pickup);
        }

        // DELETE: api/Pickups/5
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult DeletePickup(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return NotFound();
            }

            db.Pickups.Remove(pickup);
            db.SaveChanges();

            return Ok(pickup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PickupExists(int id)
        {
            return db.Pickups.Count(e => e.Id == id) > 0;
        }
    }
}