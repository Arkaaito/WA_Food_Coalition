using System;
using System.Collections.Generic;
//using System.Data;
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
			Pickup currentPickup = db.Pickups.Find(id);

			// Hack!  Because I can't figure out how to access the raw JSON, I'm checking against default values for now.
			// If the Pickup constructed from the request body has the default value for a given attribute, we assume it
			// was not set in the JSON, and the value gets copied in from the existing record.
			if (pickup.Id == 0)
				pickup.Id = currentPickup.Id;
			if (pickup.DonorId == 0)
				pickup.DonorId = currentPickup.DonorId;
			if (pickup.Items == null)
				pickup.Items = currentPickup.Items;
			if (pickup.Notes == null)
				pickup.Notes = currentPickup.Notes;
			if (pickup.Schedule == null)
				pickup.Schedule = currentPickup.Schedule;
			if (pickup.Address == null)
				pickup.Address = currentPickup.Address;
			if (pickup.City == null)
				pickup.City = currentPickup.City;
			if (pickup.Zip == null)
				pickup.Zip = currentPickup.Zip;
			if (pickup.Latitude == 0)
				pickup.Latitude = currentPickup.Latitude;
			if (pickup.Longitude == 0)
				pickup.Longitude = currentPickup.Longitude;
			if (pickup.Status == 0)
				pickup.Status = currentPickup.Status;

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
                if (pickup.DonorId != 0)
                {
                    var donor = db.Donors.Find(pickup.DonorId);
                    pickup.Donor = donor;
                }
                else if (pickup.Donor != null)
                {
                    var donors = from d in db.Donors
                                 where d.FirstName == pickup.Donor.FirstName &&
                                       d.LastName == pickup.Donor.LastName
                                 select d;

                    if (donors.Any())
                    {
                        pickup.Donor = donors.First();
                    }
                    else
                    {
                        if (pickup.Donor.Email == null && pickup.Donor.Phone == null)
                            return BadRequest("Donors must provide either a phone or an email");

                        db.Donors.Add(new Donor()
                        {
                            DeviceId = pickup.Donor.DeviceId,
                            FirstName = pickup.Donor.FirstName,
                            LastName = pickup.Donor.LastName,
                            Email = pickup.Donor.Email,
                            Phone = pickup.Donor.Phone,
                            OptIn = pickup.Donor.OptIn
                        });

                        db.SaveChanges();
                    }
                }
                else
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