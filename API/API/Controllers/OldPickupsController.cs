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
    public class OldPickupsController : ApiController
    {
    //    private FoodCoalitionAppContext db = new FoodCoalitionAppContext();
    //    private ILocationService locationService = new LocationService();
    //    private EmailService emailService = new EmailService();

    //    // GET api/Pickups
    //    public IQueryable<Pickup> GetPickups()
    //    {
    //        var rv = db.Pickups;
    //        return rv;
    //    }

    //    // GET api/Pickups/5
    //    [ResponseType(typeof(Pickup))]
    //    public IHttpActionResult GetPickups(int id)
    //    {
    //        Pickup pickup = db.Pickups.Find(id);
    //        if (pickup == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(pickup);
    //    }

    //    // Get Pickups associated to the ** FoodBank ID ** that are pending
    //    public IQueryable<Pickup> Get(int foodBankId, string status) {
    //        return db.Pickups.Where(d => d.FoodBankID == foodBankId && d.Status.Equals(status)).AsQueryable();
    //    }

    //    // Get Pickups that are near the food bank
    //    public IQueryable<Pickup> Get(int foodBankId) {
    //        Member foodBank = db.FoodBanks.Find(foodBankId);
    //        if (foodBank == null)
    //            throw new HttpResponseException(HttpStatusCode.NotFound);

    //        return locationService.GetNearbyPickups(foodBank.Latitude, foodBank.Longitude).AsQueryable();
    //    }

    //    // PUT api/Pickups/5
    //    public IHttpActionResult PutPickups(int id, Pickup pickup)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        if (id != pickup.ID)
    //        {
    //            return BadRequest();
    //        }

    //        db.Entry(pickup).State = EntityState.Modified;

    //        try
    //        {
    //            db.SaveChanges();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!PickupExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return Ok();
    //    }

    //    [ActionName("Status")]
    //    // PUT api/Pickups/Status/7?status=open&foodbankid=1
    //    public IHttpActionResult PutStatus(int id, [FromUri]string status, [FromUri]int foodbankid)
    //    {
    //        Pickup pickup = db.Pickups.Find(id);
    //        if (pickup == null)
    //        {
    //            return NotFound();
    //        }
    //        pickup.Status = status;
    //        pickup.FoodBankID = foodbankid;

    //        db.Entry(pickup).State = EntityState.Modified;

    //        try
    //        {
    //            db.SaveChanges();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            return NotFound();
    //        }

    //        return Ok();
    //    }

    //    [ActionName("CloseExpiredPickups")]
    //    // POST api/Pickups/CloseExpiredPickups
    //    public IHttpActionResult PostCloseExpiredPickups()
    //    {
    //        expirationService.CloseExpiredPickups();
    //        return Ok();
    //    }

    //    // POST api/Pickups
    //    [ResponseType(typeof(Pickup))]
    //    public IHttpActionResult PostPickups(Pickup pickup)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        /* Validate coordinates are within Washington */
    //        if (pickup.Latitude  > 44.5 &&
    //            pickup.Latitude  < 49.2 &&
    //            pickup.Longitude > -125.43 &&
    //            pickup.Longitude < -116.8)
    //        {

    //            db.Pickups.Add(pickup);
    //            db.SaveChanges();
                
    //            List<Member> foodbanks = locationService.GetNearbyFoodBanks(pickup.Latitude, pickup.Longitude, 5).ConvertAll(fbd => (Member)fbd);
    //            emailService.SendNearbyPickupsEmail(pickup, foodbanks);
    //            return CreatedAtRoute("DefaultApi", new { id = pickup.ID }, pickup);
    //        }

    //        else
    //        {
    //            return BadRequest("Coordinates out of range.");
    //        }   
    //    }


    //    // DELETE api/Pickups/5
    //    [ResponseType(typeof(Pickup))]
    //    public IHttpActionResult DeletePickup(int id)
    //    {
    //        Pickups pickup = db.Pickups.Find(id);
    //        if (pickup == null)
    //        {
    //            return NotFound();
    //        }

    //        db.Pickups.Remove(Pickup);
    //        db.SaveChanges();

    //        return Ok(pickup);
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        db.Dispose();
    //        base.Dispose(disposing);
    //    }

    //    private bool PickupExists(int id)
    //    {
    //        return db.Pickups.Count(e => e.ID == id) > 0;
    //    }
    }
}